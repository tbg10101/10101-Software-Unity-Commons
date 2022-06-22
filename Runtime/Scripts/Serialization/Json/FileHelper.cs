using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Software10101.Serialization.Json {
    public static class FileHelper {
        private static readonly HashAlgorithm HashAlgorithm = MD5.Create();

        public static void SaveAsync(JsonDictionary data, string fileName, Action callback = null, bool includeHash = true) {
            ThreadPool.QueueUserWorkItem(stateInfo => {
                try {
                    byte[] payload = GenericJson.Serialize(data).GetBytes(Encoding.UTF8);

                    using (FileStream fileStream = File.Open(fileName, FileMode.Create, FileAccess.Write)) {
                        if (includeHash) {
                            byte[] hash = HashAlgorithm.ComputeHash(payload);
                            byte[] hashStringBytes = hash.ToHexStringBytes();

                            fileStream.Write(hashStringBytes, 0, hashStringBytes.Length);
                            fileStream.WriteByte(10);
                        }

                        fileStream.Write(payload, 0, payload.Length);
                    }

                    callback?.Invoke();
                } catch (Exception e) {
                    Debug.LogException(e);
                }
            });
        }

        public static void LoadAsync(string fileName, Action<JsonDictionary, bool> callback = null, bool requireHash = true) {
            ThreadPool.QueueUserWorkItem(stateInfo => {
                try {
                    int hashLength = HashAlgorithm.HashSize / 8 * 2; // 8 bits per byte but each byte is represented by 2 characters

                    byte[] hashStringBytes = new byte[hashLength];
                    int separatorByte;
                    byte[] buffer = new byte[8192];
                    StringBuilder stringBuilder = new StringBuilder();

                    using (FileStream fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read)) {
                        int readByteCount;

                        int totalHashBytesRead = 0;
                        while ((readByteCount = fileStream.Read(hashStringBytes, totalHashBytesRead, hashLength - totalHashBytesRead)) > 0) {
                            totalHashBytesRead += readByteCount;

                            if (totalHashBytesRead == hashLength) {
                                break;
                            }

                            if (totalHashBytesRead > hashLength) {
                                throw new Exception("Something went terribly wrong while reading the file header.");
                            }
                        }

                        if (totalHashBytesRead != hashLength) {
                            if (requireHash) {
                                throw new Exception("Could not read file hash. (file too short)");
                            }

                            stringBuilder.Append(Encoding.UTF8.GetString(hashStringBytes,0, totalHashBytesRead));
                        }

                        separatorByte = fileStream.ReadByte();
                        if (requireHash) {
                            if (separatorByte < 0) {
                                throw new Exception("Could not read file separator. (file too short)");
                            }

                            if (separatorByte != 10) {
                                throw new Exception("File separator is incorrect.");
                            }
                        }

                        while ((readByteCount = fileStream.Read(buffer, 0, buffer.Length)) > 0) {
                            stringBuilder.Append(Encoding.UTF8.GetString(buffer,0, readByteCount));
                        }
                    }

                    string payloadString = stringBuilder.ToString().Trim();

                    bool payloadIntegrityIntact = false;

                    if (separatorByte == 10 || requireHash) {
                        // there might be a hash, test it out

                        byte[] computedHash = HashAlgorithm.ComputeHash(payloadString.GetBytes(Encoding.UTF8));
                        byte[] readHash = hashStringBytes.FromHexStringBytes();

                        payloadIntegrityIntact = computedHash.SequenceEqual(readHash);
                    }

                    if (!payloadIntegrityIntact && requireHash) {
                        Debug.LogWarningFormat("File failed integrity check during load: {0}", fileName);
                    }

                    if (!payloadIntegrityIntact && (payloadString[0] != '{' || payloadString[payloadString.Length - 1] != '}')) {
                        // try combining all the bytes, maybe it is json without the hash header

                        payloadString = (Encoding.UTF8.GetString(hashStringBytes)
                                         + Encoding.UTF8.GetString(new []{(byte)separatorByte})
                                         + stringBuilder).Trim();
                    }

                    if (payloadString[0] != '{' || payloadString[payloadString.Length - 1] != '}') {
                        throw new Exception("File failed JSON smoke test.");
                    }

                    JsonDictionary payload = GenericJson.Deserialize(payloadString);

                    callback?.Invoke(payload, payloadIntegrityIntact);
                } catch (Exception e) {
                    Debug.LogException(e);
                }
            });
        }
    }
}

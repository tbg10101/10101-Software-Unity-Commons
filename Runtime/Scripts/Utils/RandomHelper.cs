﻿using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Random = System.Random;

namespace Software10101.Utils {
    public static class RandomHelper {
        public static readonly Random SystemRandom = new Random();

        public static UnityEngine.Random.State SeedState = InitSeed();

        private static UnityEngine.Random.State InitSeed () {
            UnityEngine.Random.State prevState = UnityEngine.Random.state;

            UnityEngine.Random.InitState(SystemRandom.Next());

            UnityEngine.Random.State newState = UnityEngine.Random.state;

            UnityEngine.Random.state = prevState;

            return newState;
        }

        public static void ReInitSeed () {
            SeedState = InitSeed();
        }

        public static int NextSeed () {
            UnityEngine.Random.State prevState = UnityEngine.Random.state;

            UnityEngine.Random.state = SeedState;

            int seed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);

            SeedState = UnityEngine.Random.state;

            UnityEngine.Random.state = prevState;

            return seed;
        }

        // [0.0, 1.0)
        public static float Next () {
            float num = UnityEngine.Random.value;

            if (num >= 1.0f) {
                num = 0.0f;
            }

            return num;
        }

        // [0.0, max)
        public static float Next (float max) {
            return Next() * max;
        }

        // [min, max)
        public static float Next (float min, float max) {
            return min + (Next() * max);
        }

        // [0.0, max)
        public static int Next (int max) {
            return Mathf.FloorToInt(Next() * max);
        }

        // [min, max)
        public static int Next (int min, int max) {
            return min + Mathf.FloorToInt(Next() * max);
        }

        public static string GetLetter () {
            char l = (char)('a' + SystemRandom.Next(0, 25));
            return l.ToString();
        }

        public static string GetLetters (int count) {
            string s = "";

            for (int i = 0; i < count; i++) {
                s += GetLetter();
            }

            return s;
        }

        public static string GetNumber () {
            char l = (char)('0' + SystemRandom.Next(0, 9));
            return l.ToString();
        }

        public static string GetNumbers (int count) {
            string s = "";

            for (int i = 0; i < count; i++) {
                s += GetNumber();
            }

            return s;
        }

        public static string GetMaskedString (string mask) {
            string s = "";

            foreach (char ch in mask) {
                if ('#'.Equals(ch)) {
                    s = s + GetNumber().ToUpper();
                } else if ('@'.Equals(ch)) {
                    s = s + GetLetter().ToUpper();
                } else {
                    s = s + ch;
                }
            }

            return s;
        }

        public static Color GetColor () {
            Color c = new Color(UnityEngine.Random.Range(0.0f, 1.0f),
                UnityEngine.Random.Range(0.0f, 1.0f),
                UnityEngine.Random.Range(0.0f, 1.0f));

            return c;
        }

        public static Color GetColorPhysical (int kMin = 1000, int kMax = 40000) {
            return ColorHelper.GetColorPhysical(UnityEngine.Random.Range(kMin, kMax));
        }

        public interface IRandomGenerator<out T> {
            T GetNext ();
        }

        public class Picker<T> : IRandomGenerator<T> {
            private readonly List<PickerEntry<T>> _buckets = new List<PickerEntry<T>>();

            private readonly float _depth;

            public Picker (params PickerEntry<T>[] buckets) {
                float depth = 0.0f;

                foreach (PickerEntry<T> entry in buckets) {
                    if (entry.Proportion > 0.0f) {
                        _buckets.Add(entry);

                        depth += entry.Proportion;
                    }
                }

                _depth = depth;
            }

            public T GetNext () {
                if (_buckets.Count == 0) {
                    return default;
                }

                if (_buckets.Count == 1) {
                    return _buckets[0].BucketType;
                }

                float value = UnityEngine.Random.Range(0.0f, _depth);

                float progress = 0.0f;

                foreach (PickerEntry<T> entry in _buckets) {
                    progress += entry.Proportion;

                    if (value <= progress) {
                        return entry.BucketType;
                    }
                }

                return _buckets[_buckets.Count - 1].BucketType;
            }
        }

        public struct PickerEntry<T> {
            public readonly T BucketType;
            public readonly float Proportion;

            public PickerEntry (T bucketType, float proportion) {
                BucketType = bucketType;
                Proportion = proportion < 0 ? 0 : proportion;
            }
        }

        public static string NewRoomName(int length = 4, string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789") {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < length; i++) {
                builder.Append(chars[Mathf.FloorToInt(UnityEngine.Random.value * chars.Length)]);
            }

            return builder.ToString();
        }
    }
}

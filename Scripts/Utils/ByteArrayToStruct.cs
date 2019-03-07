using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class ByteArrayToStruct : MonoBehaviour {
	private void Start () {
		try {
			var testStructOriginal = new TestStruct {
				sb = -30,
				b = 127,
				s = -4000,
				us = 23444,
				i = -659992,
				ui = 4234244,
				l = -1412341234123412,
				ul = 5787666900578777,
				f = 345.973f,
				d = 99234023953245803495802349852345.3452345632453,
				c = 'x',
				str = "64 characters...64 characters...64 characters...64 characters..."
			};

			Debug.Log($"Struct Size: {Marshal.SizeOf(testStructOriginal)}");

			Debug.Log($"Original Struct: {testStructOriginal}");

			var bytes = testStructOriginal.ToByteArray();
			Debug.Log($"Generated Array: {bytes.ToStringHex()}");

			var testStructReconstructed = bytes.ToStructure<TestStruct>();
			Debug.Log($"Reconstructed Struct: {testStructReconstructed}");

			if (Equals(testStructOriginal, testStructReconstructed)) {
				Debug.Log("Structs are equal.");
			} else {
				Debug.LogError("Structs are NOT equal!");
			}
		} catch (Exception e) {
			Debug.LogError($"Error: ${e.Message}");
		}
	}
}

[StructLayout(LayoutKind.Explicit, Pack = 1)]
public struct TestStruct : IEquatable<TestStruct> {
	[FieldOffset(0)]
	public sbyte sb;

	[FieldOffset(1)]
	public byte b;

	[FieldOffset(2)]
	public short s;

	[FieldOffset(4)]
	public ushort us;

	[FieldOffset(6)]
	public int i;

	[FieldOffset(10)]
	public uint ui;

	[FieldOffset(14)]
	public long l;

	[FieldOffset(22)]
	public ulong ul;

	[FieldOffset(30)]
	public float f;

	[FieldOffset(34)]
	public double d;

	[FieldOffset(42)]
	public char c;

	// 44-47 are unused

	[FieldOffset(48)] // reference types MUST be pointer-aligned, I tried this targeting 64-bit so this needs to be a multiple of 8
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64 + 1)] // plus one is because the string requires a termination byte
	public string str;

	public override string ToString () {
		return $"{nameof(TestStruct)}(" +
		       $"{nameof(sb)}={sb}, " +
		       $"{nameof(b)}={b}, " +
		       $"{nameof(s)}={s}, " +
		       $"{nameof(us)}={us}, " +
		       $"{nameof(i)}={i}, " +
		       $"{nameof(ui)}={ui}, " +
		       $"{nameof(l)}={l}, " +
		       $"{nameof(ul)}={ul}, " +
		       $"{nameof(f)}={f}, " +
		       $"{nameof(d)}={d}, " +
		       $"{nameof(c)}={c}, " +
		       $"{nameof(str)}={str}" +
		       ")";
	}

	public bool Equals (TestStruct other) {
		return sb == other.sb &&
		       b == other.b &&
		       s == other.s &&
		       us == other.us &&
		       i == other.i &&
		       ui == other.ui &&
		       l == other.l &&
		       ul == other.ul &&
		       f.Equals(other.f) &&
		       d.Equals(other.d) &&
		       c == other.c &&
		       str == other.str;
	}

	public override bool Equals (object obj) {
		if (ReferenceEquals(null, obj)) return false;
		return obj is TestStruct other && Equals(other);
	}

	public override int GetHashCode () {
		unchecked {
			var hashCode = sb.GetHashCode();
			hashCode = (hashCode * 397) ^ b.GetHashCode();
			hashCode = (hashCode * 397) ^ s.GetHashCode();
			hashCode = (hashCode * 397) ^ us.GetHashCode();
			hashCode = (hashCode * 397) ^ i.GetHashCode();
			hashCode = (hashCode * 397) ^ ui.GetHashCode();
			hashCode = (hashCode * 397) ^ l.GetHashCode();
			hashCode = (hashCode * 397) ^ ul.GetHashCode();
			hashCode = (hashCode * 397) ^ f.GetHashCode();
			hashCode = (hashCode * 397) ^ d.GetHashCode();
			hashCode = (hashCode * 397) ^ c.GetHashCode();
			hashCode = (hashCode * 397) ^ str.GetHashCode();
			return hashCode;
		}
	}
}

public static class MarshalUtils {
	/// <summary>
	/// https://stackoverflow.com/a/2887/2669980
	/// </summary>
	public static unsafe T ToStructure<T> (this byte[] bytes) where T : struct {
		fixed (byte* ptr = &bytes[0]) {
			return (T) Marshal.PtrToStructure((IntPtr) ptr, typeof(T));
		}
	}

	/// <summary>
	/// https://stackoverflow.com/a/3278956/2669980
	/// </summary>
	public static byte[] ToByteArray<T> (this T structure) where T : struct {
		int size = Marshal.SizeOf(structure);
		byte[] arr = new byte[size];

		IntPtr ptr = Marshal.AllocHGlobal(size);
		Marshal.StructureToPtr(structure, ptr, true);
		Marshal.Copy(ptr, arr, 0, size);
		Marshal.FreeHGlobal(ptr);
		return arr;
	}

	private static string ToStringHexInternal (params byte[] array) {
		return BitConverter.ToString(array);
	}

	public static string ToStringHex (this byte[] array) {
		return "[" + ToStringHexInternal(array).Replace('-', ' ') + "]";
	}

	public static string ToStringHex (this byte b) {
		return ToStringHexInternal(b);
	}
}

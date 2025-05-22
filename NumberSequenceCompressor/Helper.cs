using System;
using System.Collections;
using System.Collections.Generic;

namespace NumberSequenceCompressor {
    class BitHelper {
        public static List<bool> GetBitsFromInt(int input, int length = 0)
        {
            if (length == 0) length = GetMinBitsFromInt(input);
            if (input < 0) input = -input;

            List<bool> bitArray = new List<bool>(length);

            BitArray array = new BitArray(new int[] { input });
            for (int i = 0; i < length; i++) {
                bitArray.Add(array[i]);
            }

            return bitArray;
        }
        public static int GetIntFromBits(List<bool> input, int length, int index) {
            int output = 0;

            for (int i = index; i < index + length; i++) {
                if (input[i]) {
                    output += 1 << i - index;
                }                
            }

            return output;
        }

        public static byte[] PackBoolList(List<bool> input) {
            int len = input.Count;
            int bytes = len >> 3;
            if ((len & 0x07) != 0) ++bytes;

            byte[] output = new byte[bytes];
            for (int i = 0; i < input.Count; i++) {
                if (input[i])
                    output[i >> 3] |= (byte)(1 << (i & 0x07));
            }

            return output;
        }
        public static List<bool> UnpackBytes(byte[] bytes, int index)
        {
            BitArray bits = new BitArray(bytes);

            List<bool> output = new List<bool>();
            for (int i = index * 8; i < bits.Length; i++) {
                output.Add(bits[i]);
            }

            return output;
        }

        public static int GetMinBitsFromInt(int value) {
            return (int)Math.Floor(Math.Log(Math.Abs(value), 2)) + 1;
        }
    }

    public static class Extensions {
        public static bool Match(this int[] array, int[] other) {
            if (array.Length != other.Length) return false;

            for (int i = 0; i < array.Length; i++) {
                if (array[i] != other[i]) return false;
            }

            return true;
        }
    }
}

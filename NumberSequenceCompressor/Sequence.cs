using System;
using System.Collections.Generic;

namespace NumberSequenceCompressor {
    class Sequence {
        public static readonly short DefaultLength = 200;
        internal List<bool> _bitList = new List<bool>();
        public List<bool> BitList { get { return _bitList; } }

        internal int LastValue { get; set; } = 0;

        public Sequence() {
        }

        public static Sequence FromArray(int[] input, int startingValue, ref int index) {
            Sequence sequence = new Sequence();

            int len = Math.Min(input.Length - index, DefaultLength);

            int[] differences = new int[len];
            int length = 1;

            sequence.LastValue = startingValue;
            for (int i = index; i < index + len; i++) {
                int diff = input[i] - sequence.LastValue;
                int inputLength = BitHelper.GetMinBitsFromInt(diff);

                if (inputLength > length) length = inputLength;

                differences[i - index] = diff;
                sequence.LastValue = input[i];
            }
            index += len;

            sequence._bitList.AddRange(BitHelper.GetBitsFromInt(length - 1, 5));
            for (int i = 0; i < len; i++) {
                sequence._bitList.Add(differences[i] < 0 ? false : true);
                sequence._bitList.AddRange(BitHelper.GetBitsFromInt(differences[i], length));
            }

            return sequence;
        }
        public static int[] GetIntArrayFromBits(List<bool> bits, int startingValue, int seqLength, ref int index)
        {
            int length = BitHelper.GetIntFromBits(bits, 5, index) + 1;
            index += 5;

            List<int> values = new List<int>();

            int count = 0;
            int lastValue = startingValue;
            while (bits.Count - index >= length + 1 && count < seqLength) {                
                int value = BitHelper.GetIntFromBits(bits, length, index + 1);
                if (!bits[index]) value = -value;

                value += lastValue;
                lastValue = value;

                values.Add(value);

                index += length + 1;
                count++;
            }

            return values.ToArray();
        }
    }
}

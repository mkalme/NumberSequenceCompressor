using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSequenceCompressor
{
    public class Compressor
    {
        public Compressor() { 
            
        }

        public byte[] Compress(int[] array) {
            int index = 0;

            List<Sequence> sequences = new List<Sequence>();
            int startingValue = array[0];
            while (index < array.Length) {
                Sequence sequence = Sequence.FromArray(array, startingValue, ref index);

                sequences.Add(sequence);

                startingValue = sequence.LastValue;
            }

            List<byte> bytes = new List<byte>();
            bytes.AddRange(BitConverter.GetBytes(sequences.Count));
            bytes.AddRange(BitConverter.GetBytes(Sequence.DefaultLength));
            bytes.AddRange(BitConverter.GetBytes(array[0]));

            List<bool> bits = new List<bool>();
            foreach (var sequence in sequences) {
                bits.AddRange(sequence._bitList);
            }
            bytes.AddRange(BitHelper.PackBoolList(bits));

            return bytes.ToArray();
        }
        public int[] Uncompress(byte[] array) {
            int seqAmount = BitConverter.ToInt32(array, 0);
            short seqLength = BitConverter.ToInt16(array, 4);
            int start = BitConverter.ToInt32(array, 6);

            List<bool> bits = BitHelper.UnpackBytes(array, 10);
            List<int> values = new List<int>();

            int index = 0;
            int lastValue = start;
            for (int i = 0; i < seqAmount; i++) {
                values.AddRange(Sequence.GetIntArrayFromBits(bits, lastValue, seqLength, ref index));

                lastValue = values[values.Count - 1];
            }

            return values.ToArray();
        }

        public static int[] GetDiffBits(int[] input) {
            int average = (int)(input.Sum() / (double)input.Length);

            int[] diff = new int[input.Length];

            int lastValue = input[0];
            for (int i = 0; i < input.Length; i++) {
                diff[i] = (int)Math.Floor(Math.Log(Math.Abs(input[i] - lastValue), 2)) + 1;
                if (diff[i] < 1) diff[i] = 1;

                lastValue = input[i];
            }

            return diff;
        }
    }
}

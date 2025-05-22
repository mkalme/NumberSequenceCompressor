using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NumberSequenceCompressor;

namespace UI {
    class Program {
        private static string FilePath { get; set; } = @"D:\NumberSequenceCompression\" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-fffffff");
        private static Compressor Compressor { get; set; } = new Compressor();

        static void Main(string[] args)
        {
            int[] array = GenerateRandomIntArray(2621440);
            //int[] array = GenerateRandomIntArray(20);

            //DateTime time = DateTime.Now;
            //byte[] bytes = Compressor.Compress(array);
            //Console.WriteLine("Compressing: " + (DateTime.Now - time).TotalSeconds + " seconds");

            //time = DateTime.Now;
            //Compressor.Uncompress(bytes);
            //Console.WriteLine("Uncompressing: " + (DateTime.Now - time).TotalSeconds + " seconds");

            File.WriteAllBytes(FilePath, Compressor.Compress(array));
            int[] uncompressedArray = Compressor.Uncompress(File.ReadAllBytes(FilePath));

            Console.WriteLine(array.Match(uncompressedArray));

            //int[] diff = Compressor.GetDiffBits(GenerateRandomIntArray(20));
            //Console.WriteLine(String.Join("\n", diff));

            Console.ReadLine();
        }

        private static int[] GenerateRandomIntArray(int length) {
            int[] output = new int[length];
            
            Random random = new Random();

            int starting = 3000;
            int range = 128;
            for (int i = 0; i < length; i++) {
                output[i] = starting + random.Next(-range, range);
            }

            return output;
        }
    }
}

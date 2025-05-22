using System.IO;
using System.IO.Compression;
using Ionic.Zlib;

namespace NumberSequenceCompressor {
    class Compression {
        //ZLib
        public static byte[] Zip(byte[] input)
        {
            using (var msi = new MemoryStream(input))
            using (var mso = new MemoryStream()) {
                using (var gs = new ZlibStream(mso, Ionic.Zlib.CompressionMode.Compress)) {
                    CopyTo(msi, gs);
                }

                return mso.ToArray();
            }
        }
        public static byte[] Unzip(byte[] input)
        {
            using (var msi = new MemoryStream(input))
            using (var mso = new MemoryStream()) {
                using (var gs = new ZlibStream(mso, Ionic.Zlib.CompressionMode.Decompress)) {
                    CopyTo(msi, gs);
                }

                return mso.ToArray();
            }
        }

        //GZip
        public static byte[] GZip(byte[] input)
        {
            using (var msi = new MemoryStream(input))
            using (var mso = new MemoryStream()) {
                using (var gs = new System.IO.Compression.GZipStream(mso, System.IO.Compression.CompressionMode.Compress)) {
                    CopyTo(msi, gs);
                }

                return mso.ToArray();
            }
        }
        public static byte[] GUnzip(byte[] input)
        {
            using (var msi = new MemoryStream(input))
            using (var mso = new MemoryStream()) {
                using (var gs = new System.IO.Compression.GZipStream(msi, System.IO.Compression.CompressionMode.Decompress)) {
                    CopyTo(gs, mso);
                }

                return mso.ToArray();
            }
        }

        public static void CopyTo(Stream src, Stream dest)
        {
            byte[] bytes = new byte[4096];

            int cnt;

            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0) {
                dest.Write(bytes, 0, cnt);
            }
        }
    }
}

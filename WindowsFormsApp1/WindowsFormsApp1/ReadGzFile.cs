using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class ReadGzFile
    {
        byte[] file = File.ReadAllBytes("C:\\perlgzips\\~stat.gz");
        byte[] decompressed = Decompress(file);
        //Console.WriteLine(file.Length);
        //Console.WriteLine(decompressed.Length);

        static byte[] Decompress(byte[] gzip)
        {

            using (GZipStream stream = new GZipStream(new MemoryStream(gzip),
            CompressionMode.Decompress))
            {
                const int size = 4096;
                byte[] buffer = new byte[size];
                using (MemoryStream memory = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        count = stream.Read(buffer, 0, size);
                        if (count > 0)
                        {
                            memory.Write(buffer, 0, count);
                        }
                    }
                    while (count > 0);
                    return memory.ToArray();
                }
            }







        }



    }
}

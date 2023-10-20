using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AesCodec.Classes {
    internal class File {

        private string Path;
        public File(string path) {
            this.Path = path;
        }

        public string ReadFile() {
            string textFile = "";

            if (!System.IO.File.Exists(this.Path)) {
                Console.WriteLine("This file do not exist");
                return "";
            }

            textFile = System.IO.File.ReadAllText(this.Path);

            return textFile;
        }

        public void WriteFile(string path, string fileContent) {

            System.IO.File.WriteAllText(path, fileContent);
        }

        //public byte[] GetUTF8(string text) {
        //    //string textFile = ReadFile();

        //    // Convert the string into a byte[].
        //    byte[] utfBytes = Encoding.UTF8.GetBytes(textFile);

        //    return utfBytes;
        //}

        private string GetText(byte[] ciphertext) {

            string textFile = Encoding.ASCII.GetString(ciphertext);

            return textFile;
        }

    }
}

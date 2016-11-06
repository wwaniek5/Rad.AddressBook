using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RAD.AddressBook.Security
{
    //source: http://stackoverflow.com/questions/165808/simple-two-way-encryption-for-c-sharp
    public class AES
    {


        private  byte[] key;
        private  byte[] vector;
        private ICryptoTransform encryptor, decryptor;
        private UTF8Encoding encoder;

        public AES()
        {
     
            encoder = new UTF8Encoding(); 
        }

        public void SetPasword(string password)
        {
            RijndaelManaged rm = new RijndaelManaged();

            key = encoder.GetBytes(password);
            vector = encoder.GetBytes(password);

            Array.Resize(ref key, 32);
            Array.Resize(ref vector, 16);

            encryptor = rm.CreateEncryptor(key, vector);
            decryptor = rm.CreateDecryptor(key, vector);


        }

        public string Encrypt(string unencrypted)
        {
            return Convert.ToBase64String(Encrypt(encoder.GetBytes(unencrypted)));
        }

        public string Decrypt(string encrypted)
        {
            return encoder.GetString(Decrypt(Convert.FromBase64String(encrypted)));
        }

        public byte[] Encrypt(byte[] buffer)
        {
            return Transform(buffer, encryptor);
        }

        public byte[] Decrypt(byte[] buffer)
        {
            return Transform(buffer, decryptor);
        }

        protected byte[] Transform(byte[] buffer, ICryptoTransform transform)
        {
            MemoryStream stream = new MemoryStream();
            using (CryptoStream cs = new CryptoStream(stream, transform, CryptoStreamMode.Write))
            {
                cs.Write(buffer, 0, buffer.Length);
            }
            return stream.ToArray();
        }
    }
}

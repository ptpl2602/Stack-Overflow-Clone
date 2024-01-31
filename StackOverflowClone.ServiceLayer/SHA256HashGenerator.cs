using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace StackOverflowClone.ServiceLayer
{
    public class SHA256HashGenerator
    {
        public static string GenerateHash(string inputData)
        {
            using (SHA256 sha265Hash = SHA256.Create())
            {
                byte[] bytes = sha265Hash.ComputeHash(Encoding.UTF8.GetBytes(inputData));
                StringBuilder builer = new StringBuilder();
                for(int i=0; i<bytes.Length; i++)
                {
                    builer.Append(bytes[i].ToString("x2"));
                }
                return builer.ToString();
            }
        }
    }
}

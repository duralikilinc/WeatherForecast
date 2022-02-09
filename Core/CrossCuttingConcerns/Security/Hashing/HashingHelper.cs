using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Security.Hashing
{
    public class HashingHelper
    {
       public static string MD5Olustur(string input)
        {
            MD5 md5Hasher = MD5.Create();
            string extraKeys = "Sj17KdA.2#R";
            string sifrelenecekVeri = input + extraKeys;
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(sifrelenecekVeri));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
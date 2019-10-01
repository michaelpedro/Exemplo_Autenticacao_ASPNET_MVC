﻿using System.Security.Cryptography;
using System.Text;

namespace AutenticacaoAspNet.Util
{
    public class Hash
    {
        public static string GerarHash (string texto)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(texto);
            byte[] hash = sha256.ComputeHash(bytes);
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X"));
            }

            return result.ToString();
        }        
    }
}
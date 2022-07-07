using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesTests.Services
{
    internal static class RandomString
    {
        public static string Generate(int length)
        {
            Random _rnd = new Random();
            string result = String.Empty;

            for (int i = 0; i < length; i++)
            {
                char letter;
                do
                {
                    letter = (char)_rnd.Next(0, 255);
                }
                while (!char.IsLetterOrDigit(letter));
                result = result + letter;
            }

            return result;
        }
    }
}

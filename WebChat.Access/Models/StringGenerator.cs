using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WebChat.Access.Models
{
    public static class StringGenerator
    {
        public static string Generate(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }
    }
}
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class InviteCodeGenerator : IInviteCodeGenerator
    {
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private const int CodeLength = 15;
        private readonly Random _random = new Random();
        public string GenerateCode()
        {
            return new string(Enumerable.Repeat(Chars, CodeLength)
             .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}

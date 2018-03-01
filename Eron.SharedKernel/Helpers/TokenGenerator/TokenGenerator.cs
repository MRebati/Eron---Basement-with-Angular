using System;
using Eron.Core.AppEnums;
using Eron.Core.ManagementSettings;

namespace Eron.SharedKernel.Helpers.TokenGenerator
{
    public static class TokenGenerator
    {
        public static string Generate(TokenType type)
        {
            var tokenString = GetStringToken(type);
            var currentYear = (DateTime.Now.Year).ToString("YYYY");
            var firstRandomNumber = GetRandomNumber(6);
            var secondRandomNumber = GetRandomNumber(2);
            var heroChars = ApplicationSettings.DefaultHeroChars.ToUpper();

            return heroChars + "-" + currentYear + firstRandomNumber + "-" + tokenString + "-" + secondRandomNumber;
        }

        private static string GetStringToken(TokenType type)
        {
            string tokenString;
            switch (type)
            {
                case TokenType.Transaction:
                    tokenString = "TR";
                    break;
                case TokenType.ProductNumber:
                    tokenString = "TR";
                    break;
                case TokenType.InvoiceNumber:
                    tokenString = "TR";
                    break;
                case TokenType.TariffNumber:
                    tokenString = "TR";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            return tokenString;
        }

        private static string GetRandomNumber(int length)
        {
            var random = new Random();
            var decimalRandom = random.Next(0, 1);
            var randomNumber = (decimalRandom * (10 ^ length)).ToString().PadLeft(length);

            return randomNumber;
        }
    }
}

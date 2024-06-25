using System;
using System.Text.RegularExpressions;
using Bogus;
using DemoQA.Service.Models.Request;

namespace DemoQA.Service.DataProvider
{
    public static class UserDataProvider
    {

        public static string GenerateInvalidToken()
        {
            var faker = new Faker();
            var header = Base64Encode("{\"alg\":\"HS256\",\"typ\":\"JWT\"}");
            var payload = Base64Encode("{\"userName\":\"" + faker.Internet.UserName() + "\",\"password\":\"" + faker.Internet.Password() + "\",\"iat\":" + DateTimeOffset.Now.ToUnixTimeSeconds() + "}");
            var signature = faker.Random.AlphaNumeric(20);
            var token = $"{header}.{payload}.{signature}";
            return $"Bearer {token}";
        }

        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes).TrimEnd('=').Replace('+', '-').Replace('/', '_');
        }
    }
}

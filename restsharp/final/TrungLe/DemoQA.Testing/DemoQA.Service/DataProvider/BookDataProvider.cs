using Bogus;

namespace DemoQA.Service.DataProvider
{
    public static class BookDataProvider
    {
        public static string GenerateInvalidIsbn()
        {
            var faker = new Faker();

            string invalidIsbn = faker.Random.String2(13, "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ");

            while (invalidIsbn.StartsWith("978"))
            {
                invalidIsbn = faker.Random.String2(13, "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            }

            return invalidIsbn;
        }
    }
}

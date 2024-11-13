using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Sale
{
    public class OrderNumberService : IOrderNumberService
    {
        private static string GetMonth()
        {
            return DateTime.UtcNow.ToString("MMM").ToUpper();
        }

        // Generates 11 random numbers
        private static string[] GenerateRandomNumbers()
        {
            Random random = new();
            string[] chars = new string[11];

            for (int index = 0; index < chars.Length; index++)
            {
                chars[index] = Convert.ToString(random.Next(10));
            }

            return chars;
        }

        public string GenerateOrderNumber()
        {
            var dateTime = DateTime.UtcNow;
            var dateTimeString = dateTime.ToString("yyyy-MM-dd");

            var year = $"{dateTimeString[0]}{dateTimeString[1]}{dateTimeString[2]}{dateTimeString[3]}";
            var month = $"{dateTimeString[5]}{dateTimeString[6]}";
            var day = $"{dateTimeString[8]}{dateTimeString[9]}";

            var monthChars = GetMonth();

            // Generates 11 random numbers
            string[] randomNumbers = GenerateRandomNumbers();

            /* Generates a unique order number compounded by dateTimeString, monthChars and randomNumbers (length: 22)
            Format: yyyyMC0mmMC1ddMC2xxxxxxxxxxx (example: 2024J01A27N50346798213)
            yyyy = year (example: 2024)
            mm = month (example: 01)
            dd = day (example: 27)
            MC0 = month first char (example: J)
            MC1 = month second char (example: A)
            MC2 = month third char (example: N)
            x = random number
            */
            string orderNumber = $"{year}{monthChars[0]}{month}{monthChars[1]}{day}{monthChars[2]}{string.Concat(randomNumbers)}";
            return orderNumber;
        }
    }
}

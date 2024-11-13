using Services.Interfaces;

namespace Services.Payment
{
    public class TransactionNumberService : ITransactionNumberService
    {
        private static string GetMonth()
        {
            return DateTime.UtcNow.ToString("MMM").ToUpper();
        }

        // Generates 10 random numbers
        private static string[] GenerateRandomNumbers()
        {
            Random random = new();
            string[] chars = new string[10];

            for (int index = 0; index < chars.Length; index++)
            {
                chars[index] = Convert.ToString(random.Next(10));
            }

            return chars;
        }

        public string GenerateTransactionNumber()
        {
            var dateTime = DateTime.UtcNow;
            var dateTimeString = dateTime.ToString("yyyy-MM-dd");

            var year = $"{dateTimeString[0]}{dateTimeString[1]}{dateTimeString[2]}{dateTimeString[3]}";
            var month = $"{dateTimeString[5]}{dateTimeString[6]}";
            var day = $"{dateTimeString[8]}{dateTimeString[9]}";


            var monthChars = GetMonth();

            // Generates 10 random numbers
            string[] randomNumbers = GenerateRandomNumbers();

            /* Generates a unique transaction number compounded by dateTimeString, monthChars and randomNumbers (length: 22)
            Format: yyyyMC0mmMC1ddMC2xxxxxxxxxxx (example: 2024J01A27N5034679821T)
            yyyy = year (example: 2024)
            mm = month (example: 01)
            dd = day (example: 27)
            MC0 = month first char (example: J)
            MC1 = month second char (example: A)
            MC2 = month third char (example: N)
            x = random number
            T = This is the last char, indicates a transaction with a T letter
            */
            string transactionNumber = $"{year}{monthChars[0]}{month}{monthChars[1]}{day}{monthChars[2]}{string.Concat(randomNumbers)}T";

            return transactionNumber;
        }
    }
}


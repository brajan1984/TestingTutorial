using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingWorkshop.Services
{
    public class TwoDigitsUniqueNumberGenerator : IUniqueNumberGenerator
    {
        public List<int> GenerateUniqueNumbersExcluding(IEnumerable<int> digits, IEnumerable<int> except)
        {
            VerifyRange(except);
            VerifyCount(digits);

            var digitsFiltered = RemoveSingle(digits, except);

            return GenerateUniqueNumbers(digitsFiltered);
        }

        private void VerifyRange(IEnumerable<int> numbers)
        {
            var numbersNotInRange = numbers.Where(n => n > 9 || n < 0);

            if (numbersNotInRange.Count() > 0)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        private IEnumerable<int> RemoveSingle(IEnumerable<int> digits, IEnumerable<int> except)
        {
            var digitsFiltered = digits.ToList();

            except.ToList().ForEach(toRemove =>
            {
                digitsFiltered = digitsFiltered.GroupBy(s => s)
                    .SelectMany(g => g.Key.Equals(toRemove) ? g.Skip(1) : g).ToList();
            });

            return digitsFiltered;
        }

        public List<int> GenerateUniqueNumbers(IEnumerable<int> digits)
        {
            VerifyCount(digits);
            VerifyRange(digits);

            var result = new List<int>();

            var testDigits = digits.ToList();

            foreach (var firstDigit in testDigits)
            {
                var digitsForNext = RemoveSingle(digits, new[] { firstDigit });

                foreach (var secondDigit in digitsForNext)
                {
                    result.Add(int.Parse($"{firstDigit}{secondDigit}"));
                }
            }

            return result;
        }

        private static void VerifyCount(IEnumerable<int> digits)
        {
            if (digits.Count() <= 1)
            {
                throw new ArgumentException("There should be two or more digits");
            }
        }
    }
}

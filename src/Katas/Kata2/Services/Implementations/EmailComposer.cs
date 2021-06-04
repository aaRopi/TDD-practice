using System;
using System.Collections.Generic;
using System.Linq;
using Katas.Kata2.Models;

namespace Katas.Kata2.Implementations
{
    public class EmailComposer : IEmailComposer
    {
        public Email Compose(IReadOnlyList<UnusualSpending> unusualSpendings)
        {
            if (!unusualSpendings.Any())
            {
                throw new InvalidOperationException("List of unusual spendings cannot be empty");
            }

            string subject = $"Unusual spending of €{unusualSpendings.Sum(us => us.TotalSpending)} detected!";

            string body = "Hello card user!\n\n" +
                          "We have detected unusually high spending on your card in these categories:";

            foreach (UnusualSpending spending in unusualSpendings)
            {
                body += $"\n* You spent €{spending.TotalSpending} on {spending.Category.ToString().ToLower()}";
            }
            
            body += "\n\nLove," +
                    "\n\nThe Credit Card Company";

            return new Email(subject, body);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Katas.Kata2.Models;

namespace Katas.Kata2.Implementations
{
    public class EmailBodyComposer : IEmailBodyComposer
    {
        private const string Header = "Hello card user!\n\n" +
                                         "We have detected unusually high spending on your card in these categories:";

        private const string SpendingDetailInfo = "\n* You spent €{totalSpendingAmount} on {spendingCategoryName}";

        private const string Footer = "\n\nLove," +
                                      "\n\nThe Credit Card Company";
        
        public string GetEmailBody(IReadOnlyList<UnusualSpending> unusualSpendings)
        {
            if (!unusualSpendings.Any())
            {
                throw new InvalidOperationException("List of unusual spendings cannot be empty");
            }

            string body = Header;

            foreach (UnusualSpending spending in unusualSpendings)
            {
                body += SpendingDetailInfo
                    .Replace("{totalSpendingAmount}", $"{spending.TotalSpending}")
                    .Replace("{spendingCategoryName}", $"{spending.Category.ToString().ToLower()}"); //$"\n* You spent €{spending.TotalSpending} on {spending.Category.ToString().ToLower()}";
            }

            body += Footer;

            return body;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Katas.Kata2.Models;

namespace Katas.Kata2.Implementations
{
    public class EmailSubjectComposer : IEmailSubjectComposer
    {
        public string GetEmailSubject(IReadOnlyList<UnusualSpending> unusualSpendings)
        {
            if (!unusualSpendings.Any())
            {
                throw new InvalidOperationException("List of unusual spendings cannot be empty");
            }

            return $"Unusual spending of €{unusualSpendings.Sum(us => us.TotalSpending)} detected!";
        }
    }
}
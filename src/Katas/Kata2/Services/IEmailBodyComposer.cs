using System.Collections.Generic;
using Katas.Kata2.Models;

namespace Katas.Kata2
{
    public interface IEmailBodyComposer
    {
        string GetEmailBody(IReadOnlyList<UnusualSpending> unusualSpendings);
    }
}
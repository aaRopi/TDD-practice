using System.Collections.Generic;
using Katas.Kata2.Models;

namespace Katas.Kata2
{
    public interface IUserWarningService
    {
        void WarnUserWithEmail(int userId, IReadOnlyList<UnusualSpending> unusualSpendings);
    }
}
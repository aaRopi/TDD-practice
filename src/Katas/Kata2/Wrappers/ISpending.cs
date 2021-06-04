using System.Collections.Generic;
using Katas.Kata2.Models;

namespace Katas.Kata2
{
    public interface ISpending
    {
        RecentPayments FetchRecentPayments(int userId);
    }
}
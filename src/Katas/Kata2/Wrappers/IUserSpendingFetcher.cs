using System.Collections.Generic;
using Katas.Kata2.Models;

namespace Katas.Kata2
{
    public interface IUserSpendingFetcher
    {
        IReadOnlyList<Payment> Fetch(int userId, int year, int month);
    }
}
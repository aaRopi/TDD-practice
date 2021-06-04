using System.Collections.Generic;
using Katas.Kata2.Models;

namespace Katas.Kata2
{
    public class UserSpendingFetcherProxy : IUserSpendingFetcher
    {
        private readonly UserSpendingFetcher _userSpendingFetcher = UserSpendingFetcher.Instance;
        
        public IReadOnlyList<Payment> Fetch(int userId, int year, int month)
        {
            return _userSpendingFetcher.Fetch(userId, year, month);
        }
    }
}
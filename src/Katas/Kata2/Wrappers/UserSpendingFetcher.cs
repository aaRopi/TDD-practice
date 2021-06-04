using System.Collections.Generic;
using Katas.Kata2.Models;

namespace Katas.Kata2
{
    public class UserSpendingFetcher
    {
        public static UserSpendingFetcher Instance => new UserSpendingFetcher();
        
        private UserSpendingFetcher()
        {
            
        }
        public IReadOnlyList<Payment> Fetch(int userId, int year, int month)
        {
            throw new System.NotImplementedException("This is Somebody Else's Job™");
        }
    }
}
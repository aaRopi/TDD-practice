using System.Collections.Generic;
using System.Linq;
using Katas.Kata2.Models;

namespace Katas.Kata2
{
    public class UnusualSpendingKata
    {
        private readonly ISpending _spending;
        private readonly IUnusualSpendingChecker _spendingChecker;
        private readonly IUserWarningService _userWarningService;

        public UnusualSpendingKata(ISpending spending, IUnusualSpendingChecker spendingChecker, IUserWarningService userWarningService)
        {
            _spending = spending;
            _spendingChecker = spendingChecker;
            _userWarningService = userWarningService;
        }

        public void TriggersUnusualSpendingEmail(int userId)
        {
            RecentPayments recentPayments = _spending.FetchRecentPayments(userId);
            
            IReadOnlyList<UnusualSpending> unusualSpendings = _spendingChecker.CheckUnusualMonthlySpending(recentPayments);

            if (unusualSpendings.Any())
            {
                _userWarningService.WarnUserWithEmail(userId, unusualSpendings); 
            }
        }
    }
}
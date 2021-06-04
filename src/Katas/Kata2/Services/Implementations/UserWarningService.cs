using System;
using System.Collections.Generic;
using System.Linq;
using Katas.Kata2.Models;

namespace Katas.Kata2.Implementations
{
    public class UserWarningService : IUserWarningService
    {
        private readonly IEmailComposer _emailComposer;
        private readonly IEmailService _emailService;

        public UserWarningService(IEmailComposer emailComposer, IEmailService emailService)
        {
            _emailComposer = emailComposer;
            _emailService = emailService;
        }

        public void WarnUserWithEmail(int userId, IReadOnlyList<UnusualSpending> unusualSpendings)
        {
            if (!unusualSpendings.Any())
            {
                throw new InvalidOperationException("List of unusual spendings cannot be empty");
            }

            Email email = _emailComposer.Compose(unusualSpendings);
            
            _emailService.Email(userId, email.Subject, email.Body);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Katas.Kata2.Models;

namespace Katas.Kata2.Implementations
{
    public class EmailComposer : IEmailComposer
    {
        private readonly IEmailSubjectComposer _emailSubjectComposer;
        private readonly IEmailBodyComposer _emailBodyComposer;

        public EmailComposer(IEmailSubjectComposer emailSubjectComposer, IEmailBodyComposer emailBodyComposer)
        {
            _emailSubjectComposer = emailSubjectComposer;
            _emailBodyComposer = emailBodyComposer;
        }
        

        public Email Compose(IReadOnlyList<UnusualSpending> unusualSpendings)
        {
            if (!unusualSpendings.Any())
            {
                throw new InvalidOperationException("List of unusual spendings cannot be empty");
            }

            string subject = _emailSubjectComposer.GetEmailSubject(unusualSpendings);

            string body = _emailBodyComposer.GetEmailBody(unusualSpendings);

            return new Email(subject, body);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Katas.Kata2;
using Katas.Kata2.Implementations;
using Katas.Kata2.Models;
using NSubstitute;
using Xunit;

namespace Katas.Tests.Unit.Kata2
{
    public class EmailComposerTests
    {
        private readonly IEmailSubjectComposer _emailSubjectComposer = Substitute.For<IEmailSubjectComposer>();
        private readonly IEmailBodyComposer _emailBodyComposer = Substitute.For<IEmailBodyComposer>();
        
        private readonly EmailComposer _sut;

        public EmailComposerTests()
        {
            _sut = new EmailComposer(_emailSubjectComposer, _emailBodyComposer);
        }

        [Fact]
        public void Compose_ShouldThrowInvalidOperationException_GivenEmptyListOfUnusualSpendings()
        {
            Action act = () => _sut.Compose(new List<UnusualSpending>());

            act.Should().Throw<InvalidOperationException>().WithMessage("List of unusual spendings cannot be empty");
        }
        
        [Fact]
        public void Compose_ShouldReturnEmailWithValidSubjectAndBody_GivenNonEmptyListOfUnusualSpendings()
        {
            IReadOnlyList<UnusualSpending> unusualSpendings = new List<UnusualSpending>
            {
                new(Category.Groceries, 345.56f, 121.78f),
                new(Category.Golf, 1345.56f, 45.67f)
            };

            string subject = "Test email subject";
            string body = "Test email body";

            _emailSubjectComposer.GetEmailSubject(unusualSpendings).Returns(subject);
            _emailBodyComposer.GetEmailBody(unusualSpendings).Returns(body);

            Email expectedEmail = new (subject, body);
            
            Email email = _sut.Compose(unusualSpendings);

            email.Should().BeEquivalentTo(expectedEmail);
        }
    }
}
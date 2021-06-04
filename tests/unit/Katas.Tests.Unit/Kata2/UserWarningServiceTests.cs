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
    public class UserWarningServiceTests
    {
        private readonly IEmailComposer _emailComposer = Substitute.For<IEmailComposer>();
        private readonly IEmailService _emailService = Substitute.For<IEmailService>();
        
        private readonly UserWarningService _sut;

        public UserWarningServiceTests()
        {
            _sut = new UserWarningService(_emailComposer, _emailService);
        }

        [Fact]
        public void WarnUserWithEmail_ShouldThrowInvalidOperationException_GivenEmptyListOfUnusualSpendings()
        {
            Action act = () => _sut.WarnUserWithEmail(55, new List<UnusualSpending>());
            
            act.Should().Throw<InvalidOperationException>().WithMessage("List of unusual spendings cannot be empty");
        }
        
        [Fact]
        public void WarnUserWithEmail_ShouldComposeEmailAndCallEmailService_GivenNonEmptyListOfUnusualSpendings()
        {
            int userId = 56;
            IReadOnlyList<UnusualSpending> spendings = new List<UnusualSpending> {new(Category.Groceries, 121.55f)}.AsReadOnly();
            Email email = new("Test subject", "Test body");

            _emailComposer.Compose(spendings).Returns(email);
            
            _sut.WarnUserWithEmail(userId, spendings);
            
            _emailComposer.Received().Compose(spendings);
            _emailService.Received().Email(userId, email.Subject, email.Body);
        }
    }
}
using System;
using Xunit;

namespace TDDBank.Tests
{
    public class BankaccountTests
    {
        [Fact]
        public void New_account_should_have_zero_as_balance()
        {
            var ba = new Bankaccount();

            Assert.Equal(0, ba.Balance);
        }

        [Fact]
        public void Deposit_adds_to_the_Balance()
        {
            var ba = new Bankaccount();

            ba.Deposit(3m);
            ba.Deposit(8m);

            Assert.Equal(11m, ba.Balance);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-0.1)]
        public void Deposit_negative_or_zero_amount_throws_ArgumentException(decimal amount)
        {
            var ba = new Bankaccount();

            Assert.Throws<ArgumentException>(() => ba.Deposit(amount));
        }

        [Fact]
        public void Withdraw_subs_from_the_Balance()
        {
            var ba = new Bankaccount();
            ba.Deposit(12m);

            ba.Withdraw(8m);

            Assert.Equal(4m, ba.Balance);

            ba.Withdraw(4m);

            Assert.Equal(0m, ba.Balance);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-0.1)]
        public void Withdraw_negative_or_zero_amount_throws_ArgumentException(decimal amount)
        {
            var ba = new Bankaccount();
            ba.Deposit(12m);

            Assert.Throws<ArgumentException>(() => ba.Withdraw(amount));
        }

        [Fact]
        public void Withdraw_below_balance_throws_InvalidOperationException()
        {
            var ba = new Bankaccount();
            ba.Deposit(12m);

            Assert.Throws<InvalidOperationException>(() => ba.Withdraw(13m));
        }
    }
}

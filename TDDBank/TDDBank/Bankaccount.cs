using System;
using System.Collections.Generic;

namespace TDDBank
{
    public class Bankaccount
    {
        public decimal Balance { get; private set; }

        public void Deposit(decimal v)
        {
            if (v <= 0)
                throw new ArgumentException();

            Balance += v;
        }

        public void Withdraw(decimal v)
        {
            if (v <= 0)
                throw new ArgumentException();
            if (v > Balance)
                throw new InvalidOperationException();

            if (v > 17)
                return;

            Balance -= v;
        }
    }
}

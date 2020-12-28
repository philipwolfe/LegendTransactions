using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Transactions.Abstractions;
using Rhino.Mocks;

namespace Legend.Transactions.Example
{
    /// <summary>
    /// A service that allows for transfering money from an account
    /// to another.
    /// </summary>
    public class MoneyTransferService
    {
        private ITransactionManager transactionManager;

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="transactionManager">The transaction manager to use.</param>
        public MoneyTransferService(ITransactionManager transactionManager)
        {
            if (transactionManager == null) throw new ArgumentNullException("transactionManager");

            this.transactionManager = transactionManager;
        }

        /// <summary>
        /// Transfers the specified amount of money from the specified
        /// fromAccount to the specified toAccount.
        /// </summary>
        /// <param name="amount">The amount to transfer.</param>
        /// <param name="fromAccount">The account to transfer from.</param>
        /// <param name="toAccount">The account to transfer to.</param>
        public void Transfer(decimal amount, IAccount fromAccount, IAccount toAccount)
        {
            if (fromAccount == null) throw new ArgumentNullException("fromAccount");
            if (toAccount == null) throw new ArgumentNullException("toAccount");

            using (var scope = transactionManager.CreateScope())
            {
                var withdrawal = new ChangeAccountBalanceCommand(-amount, fromAccount);
                var insertion = new ChangeAccountBalanceCommand(amount, toAccount);

                withdrawal.EnlistVolatile(this.transactionManager);
                insertion.EnlistVolatile(this.transactionManager);

                withdrawal.Execute();
                insertion.Execute();

                scope.Complete();
            }
        }
    }
}

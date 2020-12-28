using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Transactions.Abstractions;
using Rhino.Mocks;

namespace Legend.Transactions.Example
{
    public class ChangeAccountBalanceCommand : ITransactionParticipant
    {
        private decimal amount;
        private IAccount account;
        private bool executeHasBeenCalled;

        public ChangeAccountBalanceCommand(decimal amount, IAccount account)
        {
            if (account == null) throw new ArgumentNullException("account");

            this.amount = amount;
            this.account = account;
            this.executeHasBeenCalled = false;
        }

        public void Commit(IEnlistment enlistment)
        {
            enlistment.Done();
        }

        public void InDoubt(IEnlistment enlistment)
        {
            this.Rollback(enlistment);
        }

        public void Prepare(IPreparingEnlistment preparingEnlistment)
        {
            if (!this.executeHasBeenCalled)
            {
                preparingEnlistment.ForceRollback();
            }

            preparingEnlistment.Prepared();
        }

        public void Rollback(IEnlistment enlistment)
        {
            if (this.executeHasBeenCalled)
            {
                this.account.Balance = this.account.Balance - this.amount;
            }

            enlistment.Done();
        }

        public void Execute()
        {
            if (this.executeHasBeenCalled)
            {
                throw new InvalidOperationException("Execute may only be called once on a ChangeAccountBalanceCommand.");
            }

            this.account.Balance = this.account.Balance + this.amount;
            this.executeHasBeenCalled = true;
        }
    }
}

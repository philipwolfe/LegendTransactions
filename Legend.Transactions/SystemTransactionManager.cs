using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions.Abstractions;
using System.Transactions;

namespace Legend.Transactions
{
    /// <summary>
    /// A transaction manager implementation that works as wrapper over the System.Transactions-namespace.
    /// </summary>
    public class SystemTransactionManager : ITransactionManager
    {
        private SystemTransactionManager()
        {
        }

        public readonly static SystemTransactionManager Instance = new SystemTransactionManager();

        public ITransaction CurrentTransaction
        {
            get { return Transaction.Current.Cast(); }
        }

        public ITransactionScope CreateScope()
        {
            return new TransactionScope().Cast();
        }

        public ITransactionScope CreateScope(ITransaction transactionToUse)
        {
            return new TransactionScope(transactionToUse.Cast()).Cast();
        }

        public ITransactionScope CreateScope(TransactionScopeOption scopeOption)
        {
            return new TransactionScope(scopeOption).Cast();
        }

        public ITransactionScope CreateScope(ITransaction transactionToUse, TimeSpan scopeTimeout)
        {
            return new TransactionScope(transactionToUse.Cast(), scopeTimeout).Cast();
        }

        public ITransactionScope CreateScope(TransactionScopeOption scopeOption, TimeSpan scopeTimeout)
        {
            return new TransactionScope(scopeOption, scopeTimeout).Cast();
        }

        public ITransactionScope CreateScope(TransactionScopeOption scopeOption, TransactionOptions transactionOptions)
        {
            return new TransactionScope(scopeOption, transactionOptions).Cast();
        }

        public ITransactionScope CreateScope(ITransaction transactionToUse, TimeSpan scopeTimeout, EnterpriseServicesInteropOption interopOption)
        {
            return new TransactionScope(transactionToUse.Cast(), scopeTimeout, interopOption).Cast();
        }

        public ITransactionScope CreateScope(TransactionScopeOption scopeOption, TransactionOptions transactionOptions, EnterpriseServicesInteropOption interopOption)
        {
            return new TransactionScope(scopeOption, transactionOptions, interopOption).Cast();
        }
    }
}
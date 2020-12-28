namespace System.Transactions.Abstractions
{
    public interface ITransactionManager
    {
        /// <summary>
        /// Initializes a new instance of the System.Transactions.ITransactionScope class.
        /// </summary>
        ITransaction CurrentTransaction { get; }

        /// <summary>
        /// Initializes a new instance of the System.Transactions.ITransactionScope class.
        /// </summary>
        /// <returns></returns>
        ITransactionScope CreateScope();

        /// <summary>
        /// Initializes a new instance of the System.Transactions.ITransactionScope class and sets the specified transaction as the ambient transaction, so that transactional work done inside the scope uses this transaction.
        /// </summary>
        /// <param name="transactionToUse">The transaction to be set as the ambient transaction, so that transactional work done inside the scope uses this transaction.</param>
        /// <returns></returns>
        ITransactionScope CreateScope(ITransaction transactionToUse);

        /// <summary>
        /// Initializes a new instance of the System.Transactions.ITransactionScope class with the specified requirements.
        /// </summary>
        /// <param name="scopeOption">An instance of the System.Transactions.TransactionScopeOption enumeration that describes the transaction requirements associated with this transaction scope.</param>
        /// <returns></returns>
        ITransactionScope CreateScope(TransactionScopeOption scopeOption);

        /// <summary>
        /// Initializes a new instance of the System.Transactions.ITransactionScope class with the specified timeout value, and sets the specified transaction as the ambient transaction, so that transactional work done inside the scope uses this transaction.
        /// </summary>
        /// <param name="transactionToUse">The transaction to be set as the ambient transaction, so that transactional work done inside the scope uses this transaction.</param>
        /// <param name="scopeTimeout">The System.TimeSpan after which the transaction scope times out and aborts the transaction.</param>
        /// <returns></returns>
        ITransactionScope CreateScope(ITransaction transactionToUse, TimeSpan scopeTimeout);

        /// <summary>
        /// Initializes a new instance of the System.Transactions.ITransactionScope class with the specified timeout value and requirements.
        /// </summary>
        /// <param name="scopeOption">An instance of the System.Transactions.TransactionScopeOption enumeration that describes the transaction requirements associated with this transaction scope.</param>
        /// <param name="scopeTimeout">The System.TimeSpan after which the transaction scope times out and aborts the transaction.</param>
        /// <returns></returns>
        ITransactionScope CreateScope(TransactionScopeOption scopeOption, TimeSpan scopeTimeout);

        /// <summary>
        /// Initializes a new instance of the System.Transactions.ITransactionScope class with the specified requirements.
        /// </summary>
        /// <param name="scopeOption">An instance of the System.Transactions.TransactionScopeOption enumeration that describes the transaction requirements associated with this transaction scope.</param>
        /// <param name="transactionOptions">A System.Transactions.TransactionOptions structure that describes the transaction options to use if a new transaction is created. If an existing transaction is used, the timeout value in this parameter applies to the transaction scope.  If that time expires before the scope is disposed, the transaction is aborted.</param>
        /// <returns></returns>
        ITransactionScope CreateScope(TransactionScopeOption scopeOption, TransactionOptions transactionOptions);

        /// <summary>
        /// Initializes a new instance of the System.Transactions.ITransactionScope class with the specified timeout value and COM+ interoperability requirements, and sets the specified transaction as the ambient transaction, so that transactional work done inside the scope uses this transaction.
        /// </summary>
        /// <param name="transactionToUse">The transaction to be set as the ambient transaction, so that transactional work done inside the scope uses this transaction.</param>
        /// <param name="scopeTimeout">The System.TimeSpan after which the transaction scope times out and aborts the transaction.</param>
        /// <param name="interopOption">An instance of the System.Transactions.EnterpriseServicesInteropOption enumeration that describes how the associated transaction interacts with COM+ transactions.</param>
        /// <returns></returns>
        ITransactionScope CreateScope(ITransaction transactionToUse, TimeSpan scopeTimeout, EnterpriseServicesInteropOption interopOption);

        /// <summary>
        /// Initializes a new instance of the System.Transactions.ITransactionScope class with the specified scope and COM+ interoperability requirements, and transaction options.
        /// </summary>
        /// <param name="scopeOption">An instance of the System.Transactions.TransactionScopeOption enumeration that describes the transaction requirements associated with this transaction scope.</param>
        /// <param name="transactionOptions">A System.Transactions.TransactionOptions structure that describes the transaction options to use if a new transaction is created. If an existing transaction is used, the timeout value in this parameter applies to the transaction scope.  If that time expires before the scope is disposed, the transaction is aborted.</param>
        /// <param name="interopOption">An instance of the System.Transactions.EnterpriseServicesInteropOption enumeration that describes how the associated transaction interacts with COM+ transactions.</param>
        /// <returns></returns>
        ITransactionScope CreateScope(TransactionScopeOption scopeOption, TransactionOptions transactionOptions, EnterpriseServicesInteropOption interopOption);
    }
}
namespace System.Transactions.Abstractions
{
    public interface IDependentTransaction : ITransaction
    {
        /// <summary>
        /// Attempts to complete the dependent transaction.
        /// </summary>
        /// <exception cref="System.Transactions.TransactionException">Any attempt for additional work on the transaction after this method is called.  These include invoking methods such as System.Transactions.Transaction.EnlistVolatile(), System.Transactions.Transaction.EnlistDurable(), System.Transactions.Transaction.Clone(), System.Transactions.Transaction.DependentClone(System.Transactions.DependentCloneOption), or any serialization operations on the transaction.</exception>
        void Complete();
    }
}
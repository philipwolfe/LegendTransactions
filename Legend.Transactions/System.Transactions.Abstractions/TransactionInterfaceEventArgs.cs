namespace System.Transactions.Abstractions
{
    /// <summary>
    /// Provides data for the following transaction events: System.Transactions.TransactionManager.DistributedTransactionStarted, System.Transactions.Transaction.TransactionCompleted.
    /// </summary>
    public class TransactionInterfaceEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the System.Transactions.TransactionEventArgs class.
        /// </summary>
        /// <param name="transaction"></param>
        public TransactionInterfaceEventArgs(ITransaction transaction)
        {
            this.Transaction = transaction;
        }

        /// <summary>
        /// Gets the transaction for which event status is provided.
        /// </summary>
        /// <returns>A System.Transactions.Abstractions.ITransaction for which event status is provided.</returns>
        public ITransaction Transaction { get; private set; }
    }
}

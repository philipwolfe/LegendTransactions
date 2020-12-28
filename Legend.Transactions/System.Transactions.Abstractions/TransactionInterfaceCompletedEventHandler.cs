namespace System.Transactions.Abstractions
{
    /// <summary>
    ///  Represents the method that handles the System.Transactions.Abstractions.ITransaction.TransactionCompleted event of a System.Transactions.Abstractions.Transaction class.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="eventArgs">The System.Transactions.TransactionEventArgs that contains the event data.</param>
    public delegate void TransactionInterfaceCompletedEventHandler(object sender, TransactionInterfaceEventArgs eventArgs);
}

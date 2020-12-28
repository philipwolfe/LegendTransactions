namespace System.Transactions.Abstractions
{
    /// <summary>
    /// Describes a delegated transaction for an existing transaction that can be escalated to be managed by the MSDTC when needed.
    /// </summary>
    public interface ITransactionPromoterAbstraction
    {
        /// <summary>
        /// Notifies an enlisted object that an escalation of the delegated transaction has been requested.
        /// </summary>
        /// <returns>A transmitter/receiver propagation token that marshals a distributed transaction.  For more information, see System.Transactions.TransactionInterop.GetTransactionFromTransmitterPropagationToken(System.Byte[]).</returns>
        byte[] Promote();
    }
}

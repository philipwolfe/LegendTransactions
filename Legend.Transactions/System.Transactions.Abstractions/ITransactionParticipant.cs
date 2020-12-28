namespace System.Transactions.Abstractions
{
    /// <summary>
    /// An interface that maps to the IEnlistmentNotification-interface.
    /// </summary>
    public interface ITransactionParticipant
    {
        /// <summary>
        /// Notifies an enlisted object that a transaction is being committed.
        /// </summary>:
        /// <param name="enlistment">An <see cref="IEnlistment"/> object used to send a response to the transaction manager.</param>
        void Commit(IEnlistment enlistment);

        /// <summary>
        /// Notifies an enlisted object that the status of a transaction is in doubt.
        /// </summary>
        /// <param name="enlistment">An System.Transactions.Enlistment object used to send a response to the transaction manager.</param>
        void InDoubt(IEnlistment enlistment);

        /// <summary>
        /// Notifies an enlisted object that a transaction is being prepared for commitment.
        /// </summary>
        /// <param name="preparingEnlistment">A System.Transactions.PreparingEnlistment object used to send a response to the transaction manager.</param>
        void Prepare(IPreparingEnlistment preparingEnlistment);

        /// <summary>
        /// Notifies an enlisted object that a transaction is being rolled back (aborted).
        /// </summary>
        /// <param name="enlistment">A System.Transactions.Enlistment object used to send a response to the transaction manager.</param>
        void Rollback(IEnlistment enlistment);
    }
}
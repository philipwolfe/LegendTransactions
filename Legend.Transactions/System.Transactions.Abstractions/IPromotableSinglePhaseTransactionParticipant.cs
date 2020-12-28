namespace System.Transactions.Abstractions
{
    public interface IPromotableSinglePhaseTransactionParticipant : ITransactionPromoterAbstraction
    {
        /// <summary>
        /// Notifies a transaction participant that enlistment has completed successfully.
        /// </summary>
        /// <exception cref="TransactionException">An attempt to enlist or serialize a transaction.</exception>
        void Initialize();

        /// <summary>
        /// Notifies an enlisted object that the transaction is being rolled back.
        /// </summary>
        /// <param name="singlePhaseEnlistment">A System.Transactions.SinglePhaseEnlistment object used to send a response to the transaction manager.</param>
        void Rollback(ISinglePhaseEnlistment singlePhaseEnlistment);

        /// <summary>
        /// Notifies an enlisted object that the transaction is being committed.
        /// </summary>
        /// <param name="singlePhaseEnlistment">A System.Transactions.SinglePhaseEnlistment interface used to send a response to the transaction manager.</param>
        void SinglePhaseCommit(ISinglePhaseEnlistment singlePhaseEnlistment);
    }
}

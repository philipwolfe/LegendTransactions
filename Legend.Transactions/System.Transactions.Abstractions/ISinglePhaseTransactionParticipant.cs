namespace System.Transactions.Abstractions
{
    public interface ISinglePhaseTransactionParticipant : ITransactionParticipant
    {
        /// <summary>
        /// Represents the resource manager's implementation of the callback for the single phase commit optimization.
        /// </summary>
        /// <param name="singlePhaseEnlistment">A System.Transactions.SinglePhaseEnlistment used to send a response to the transaction manager.</param>
        void SinglePhaseCommit(ISinglePhaseEnlistment singlePhaseEnlistment);
    }
}

namespace System.Transactions.Abstractions
{
    public interface ISinglePhaseEnlistment : IEnlistment
    {
        /// <summary>
        /// Represents a callback that is used to indicate to the transaction manager that the transaction should be rolled back.
        /// </summary>
        void Aborted();

        /// <summary>
        /// Represents a callback that is used to indicate to the transaction manager that the transaction should be rolled back, and provides an explanation.
        /// </summary>
        /// <param name="exception">An explanation of why a rollback is initiated.</param>
        void Aborted(Exception exception);

        /// <summary>
        /// Represents a callback that is used to indicate to the transaction manager that the SinglePhaseCommit was successful.
        /// </summary>
        void Committed();

        /// <summary>
        /// Represents a callback that is used to indicate to the transaction manager that the status of the transaction is in doubt.
        /// </summary>
        void InDoubt();

        /// <summary>
        /// Represents a callback that is used to indicate to the transaction manager that the status of the transaction is in doubt, and provides an explanation.
        /// </summary>
        /// <param name="exception">An explanation of why the transaction is in doubt.</param>
        void InDoubt(Exception exception);
    }
}

namespace System.Transactions.Abstractions
{
    public interface ITransaction : IDisposable
    {
        /// <summary>
        /// Gets the isolation level of the transaction.
        /// </summary>
        /// <returns>One of the System.Transactions.IsolationLevel values that indicates the isolation level of the transaction.</returns>
        IsolationLevel IsolationLevel { get; }

        /// <summary>
        /// Retrieves additional information about a transaction.
        /// </summary>
        /// <returns>A System.Transactions.TransactionInformation that contains additional information about the transaction.</returns>
        TransactionInformation TransactionInformation { get; }

        /// <summary>
        /// Indicates that the transaction is completed.
        /// </summary>
        /// <exception cref="ObjectDisposedException">An attempt to subscribe this event on a transaction that has been disposed.</exception>
        event TransactionInterfaceCompletedEventHandler TransactionCompleted;

        /// <summary>
        /// Creates a clone of the transaction.
        /// </summary>
        /// <returns>A System.Transactions.Transaction that is a copy of the current transaction object.</returns>
        ITransaction Clone();

        /// <summary>
        /// Creates a dependent clone of the transaction.
        /// </summary>
        /// <param name="cloneOption"> A System.Transactions.DependentCloneOption that controls what kind of dependent transaction to create.</param>
        /// <returns>A System.Transactions.DependentTransaction that represents the dependent clone.</returns>
        IDependentTransaction DependentClone(DependentCloneOption cloneOption);

        /// <summary>
        /// Enlists a durable resource manager that supports two phase commit to participate in a transaction.
        /// </summary>
        /// <param name="resourceManagerIdentifier">A unique identifier for a resource manager, which should persist across resource manager failure or reboot.</param>
        /// <param name="enlistmentNotification">An object that implements the System.Transactions.IEnlistmentNotification interface to receive two phase commit notifications.</param>
        /// <param name="enlistmentOptions">System.Transactions.EnlistmentOptions.EnlistDuringPrepareRequired if the resource manager wants to perform additional work during the prepare phase.</param>
        /// <returns>An System.Transactions.Enlistment object that describes the enlistment.</returns>
        IEnlistment EnlistDurable(Guid resourceManagerIdentifier, ITransactionParticipant enlistmentNotification, EnlistmentOptions enlistmentOptions);

        /// <summary>
        /// Enlists a durable resource manager that supports single phase commit optimization to participate in a transaction.
        /// </summary>
        /// <param name="resourceManagerIdentifier">A unique identifier for a resource manager, which should persist across resource manager failure or reboot.</param>
        /// <param name="singlePhaseNotification">An object that implements the System.Transactions.ISinglePhaseNotification interface that must be able to receive single phase commit and two phase commit notifications.</param>
        /// <param name="enlistmentOptions">System.Transactions.EnlistmentOptions.EnlistDuringPrepareRequired if the resource manager wants to perform additional work during the prepare phase.</param>
        /// <returns>An System.Transactions.Enlistment object that describes the enlistment.</returns>
        IEnlistment EnlistDurable(Guid resourceManagerIdentifier, ISinglePhaseTransactionParticipant singlePhaseNotification, EnlistmentOptions enlistmentOptions);

        /// <summary>
        /// Enlists a resource manager that has an internal transaction using a promotable single phase enlistment (PSPE).
        /// </summary>
        /// <param name="promotableSinglePhaseNotification">A System.Transactions.IPromotableSinglePhaseNotification interface implemented by the participant.</param>
        /// <returns>A System.Transactions.SinglePhaseEnlistment interface implementation that describes the enlistment.</returns>
        bool EnlistPromotableSinglePhase(IPromotableSinglePhaseTransactionParticipant promotableSinglePhaseNotification);

        /// <summary>
        /// Enlists a volatile resource manager that supports two phase commit to participate in a transaction.
        /// </summary>
        /// <param name="enlistmentNotification">An object that implements the System.Transactions.IEnlistmentNotification interface to receive two phase commit notifications.</param>
        /// <param name="enlistmentOptions">System.Transactions.EnlistmentOptions.EnlistDuringPrepareRequired if the resource manager wants to perform additional work during the prepare phase.</param>
        /// <returns>An System.Transactions.Enlistment object that describes the enlistment.</returns>
        IEnlistment EnlistVolatile(ITransactionParticipant enlistmentNotification, EnlistmentOptions enlistmentOptions);

        /// <summary>
        /// Enlists a volatile resource manager that supports single phase commit optimization to participate in a transaction.
        /// </summary>
        /// <param name="singlePhaseNotification">An object that implements the System.Transactions.ISinglePhaseNotification interface that must be able to receive single phase commit and two phase commit notifications.</param>
        /// <param name="enlistmentOptions">System.Transactions.EnlistmentOptions.EnlistDuringPrepareRequired if the resource manager wants to perform additional work during the prepare phase.</param>
        /// <returns>An System.Transactions.Enlistment object that describes the enlistment.</returns>
        IEnlistment EnlistVolatile(ISinglePhaseTransactionParticipant singlePhaseNotification, EnlistmentOptions enlistmentOptions);

        /// <summary>
        /// Rolls back (aborts) the transaction.
        /// </summary>
        void Rollback();

        /// <summary>
        /// Rolls back (aborts) the transaction.
        /// </summary>
        /// <param name="exception">An explanation of why a rollback occurred.</param>
        void Rollback(Exception exception);
    }
}
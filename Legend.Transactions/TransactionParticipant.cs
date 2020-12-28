using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions.Abstractions;
using System.Transactions;

namespace Legend.Transactions
{
    /// <summary>
    /// Provides helper methods for the <see cref="ITransactionParticipant" />-interface.
    /// </summary>
    public static class TransactionParticipant
    {
        /// <summary>
        /// Enlists the specified <paramref name="transactionParticipant"/> in the current transaction handled by the specified <paramref name="transactionManager" />.
        /// </summary>
        /// <param name="transactionParticipant">The participant to enlist.</param>
        /// <param name="transactionManager">The transaction manager that provides the transaction to enlist in.</param>
        /// <returns>The enlistment returned from the transaction when enlisting.</returns>
        public static IEnlistment EnlistVolatile(this ITransactionParticipant transactionParticipant, ITransactionManager transactionManager)
        {
            Ensure.IsNotNull(transactionParticipant, "transactionParticipant");
            Ensure.IsNotNull(transactionManager, "transactionManager");

            return transactionManager.CurrentTransaction.EnlistVolatile(
                transactionParticipant, EnlistmentOptions.None);
        }
    }
}
using System;
namespace System.Transactions.Abstractions
{
    /// <summary>
    /// Facilitates communication between an enlisted transaction participant and
    /// the transaction manager during the Prepare phase of the transaction.
    /// </summary>
    public interface IPreparingEnlistment : IEnlistment
    {
        /// <summary>
        /// Indicates that the transaction should be rolled back.
        /// </summary>
        void ForceRollback();
        
        /// <summary>
        /// Indicates that the transaction should be rolled back.
        /// </summary>
        /// <param name="e">An explanation of why a rollback is triggered.</param>
        void ForceRollback(Exception e);

        /// <summary>
        /// Indicates that the transaction can be committed.
        /// </summary>
        void Prepared();

        /// <summary>
        /// Gets the recovery information of an enlistment.
        /// </summary>
        /// <returns>Gets the recovery information of an enlistment.</returns>
        /// <exception cref="InvalidOperationException">An attempt to get recovery information inside a volatile enlistment, which
        /// does not generate any recovery information.</exception>
        byte[] RecoveryInformation();
    }
}
namespace System.Transactions.Abstractions
{
    /// <summary>
    /// Facilitates communication between an enlisted transaction participant and
    /// the transaction manager during the final phase of the transaction.
    /// </summary>
    public interface IEnlistment
    {
        /// <summary>
        /// Indicates that the transaction participant has completed its work.
        /// </summary>
        void Done();
    }
}
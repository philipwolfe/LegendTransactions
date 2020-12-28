namespace System.Transactions.Abstractions
{
    public interface ITransactionScope : IDisposable
    {
        /// <summary>
        /// Indicates that all operations within the scope are completed successfully.
        /// </summary>
        /// <exception cref="InvalidOperationException">This method has already been called once.</exception>
        void Complete();
    }
}
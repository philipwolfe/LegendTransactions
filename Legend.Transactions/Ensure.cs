using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions.Abstractions;
using System.Transactions;

namespace Legend.Transactions
{
    /// <summary>
    /// Provides argument guards.
    /// </summary>
    internal static class Ensure
    {
        /// <summary>
        /// Throws an <see cref="ArgumentNullException" /> if the specified value
        /// is null.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="value">The value to check for null.</param>
        /// <param name="argumentName">The name of the argument in the calling method.</param>
        public static void IsNotNull(object value, string argumentName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }
    }
}

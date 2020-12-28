using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Transactions.Abstractions;
using Rhino.Mocks;

namespace Legend.Transactions.Example
{
    /// <summary>
    /// Represents a bank account.
    /// </summary>
    public interface IAccount
    {
        /// <summary>
        /// The current balance of the account.
        /// </summary>
        decimal Balance { get; set; }
    }
}

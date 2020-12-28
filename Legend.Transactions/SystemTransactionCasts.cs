using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Transactions;
using System.Transactions.Abstractions;

namespace Legend.Transactions
{
    public static class SystemTransactionCasts
    {
        [DebuggerStepThrough]
        public static IEnlistment Cast(this Enlistment enlistment)
        {
            return new EnlistmentAdapter(enlistment);
        }

        [DebuggerStepThrough]
        public static IEnlistmentNotification Cast(this ITransactionParticipant participant)
        {
            return new EnlistmentNotificationAdapter(participant);
        }

        [DebuggerStepThrough]
        public static ISinglePhaseNotification Cast(this ISinglePhaseTransactionParticipant participant)
        {
            return new SinglePhaseNotificationAdapter(participant);
        }

        [DebuggerStepThrough]
        public static IPromotableSinglePhaseNotification Cast(this IPromotableSinglePhaseTransactionParticipant participant)
        {
            return new PromotableSinglePhaseNotificationAdapter(participant);
        }

        [DebuggerStepThrough]
        public static ISinglePhaseEnlistment Cast(this SinglePhaseEnlistment enlistment)
        {
            return new SinglePhaseEnlistmentAdapter(enlistment);
        }

        [DebuggerStepThrough]
        public static IPreparingEnlistment Cast(this PreparingEnlistment preparingEnlistment)
        {
            return new PreparingEnlistmentAdapter(preparingEnlistment);
        }

        [DebuggerStepThrough]
        public static IDependentTransaction Cast(this DependentTransaction transaction)
        {
            return new DependentTransactionAdapter(transaction);
        }

        [DebuggerStepThrough]
        public static ITransaction Cast(this Transaction transaction)
        {
            return new TransactionAdapter(transaction);
        }

        [DebuggerStepThrough]
        public static ITransactionScope Cast(this TransactionScope scope)
        {
            return new TransactionScopeAdapter(scope);
        }

        [DebuggerStepThrough]
        public static Transaction Cast(this ITransaction transaction)
        {
            var result = transaction as TransactionAdapter;

            if (result == null)
            {
                throw new InvalidOperationException("Only ITransaction-objects created by the SystemTransactionManager can be cast with this method.");
            }

            return result.Transaction;
        }

        private class DependentTransactionAdapter : TransactionAdapter, IDependentTransaction
        {
            public DependentTransaction DependentTransaction;

            public DependentTransactionAdapter(DependentTransaction transaction)
                : base(transaction)
            {
                this.DependentTransaction = transaction;
            }

            public void Complete()
            {
                this.DependentTransaction.Complete();
            }
        }

        private class SinglePhaseNotificationAdapter : EnlistmentNotificationAdapter, ISinglePhaseNotification
        {
            public ISinglePhaseTransactionParticipant SinglePhaseParticipant;

            public SinglePhaseNotificationAdapter(ISinglePhaseTransactionParticipant participant)
                : base(participant)
            {
                this.SinglePhaseParticipant = participant;
            }

            public void SinglePhaseCommit(SinglePhaseEnlistment singlePhaseEnlistment)
            {
                this.SinglePhaseParticipant.SinglePhaseCommit(singlePhaseEnlistment.Cast());
            }
        }

        private class TransactionScopeAdapter :
            Adapter<TransactionScope>, ITransactionScope
        {
            public TransactionScopeAdapter(TransactionScope scope)
                : base(scope)
            {

            }

            public void Complete()
            {
                this.WrappedObject.Complete();
            }

            public void Dispose()
            {
                this.WrappedObject.Dispose();
            }
        }

        private class Adapter<T>
        {
            public T WrappedObject;

            [DebuggerStepThrough]
            protected Adapter(T wrappedObject)
            {
                this.WrappedObject = wrappedObject;
            }

            [DebuggerStepThrough]
            public override bool Equals(object obj)
            {
                return this.WrappedObject.Equals(obj);
            }

            [DebuggerStepThrough]
            public override int GetHashCode()
            {
                return this.WrappedObject.GetHashCode();
            }

            [DebuggerStepThrough]
            public override string ToString()
            {
                return string.Concat("Wrapper for: ", this.WrappedObject.ToString());
            }
        }

        private class TransactionAdapter : Adapter<Transaction>, ITransaction
        {
            public Transaction Transaction
            {
                get
                {
                    return this.WrappedObject;
                }
            }

            public TransactionAdapter(Transaction transaction)
                : base(transaction)
            {
                this.Transaction.TransactionCompleted += (sender, e) =>
                {
                    if (this.TransactionCompleted != null)
                    {
                        this.TransactionCompleted(this, new TransactionInterfaceEventArgs(this));
                    }
                };
            }

            public IsolationLevel IsolationLevel
            {
                get { return this.Transaction.IsolationLevel; }
            }

            public TransactionInformation TransactionInformation
            {
                get { return this.Transaction.TransactionInformation; }
            }

            public event TransactionInterfaceCompletedEventHandler TransactionCompleted;

            public ITransaction Clone()
            {
                return this.Transaction.Clone().Cast();
            }

            public IDependentTransaction DependentClone(DependentCloneOption cloneOption)
            {
                return this.Transaction.DependentClone(cloneOption).Cast();
            }

            public IEnlistment EnlistDurable(Guid resourceManagerIdentifier, ITransactionParticipant enlistmentNotification, EnlistmentOptions enlistmentOptions)
            {
                return this.Transaction.EnlistDurable(resourceManagerIdentifier, enlistmentNotification.Cast(), enlistmentOptions).Cast();
            }

            public IEnlistment EnlistDurable(Guid resourceManagerIdentifier, ISinglePhaseTransactionParticipant singlePhaseNotification, EnlistmentOptions enlistmentOptions)
            {
                return this.Transaction.EnlistDurable(resourceManagerIdentifier, singlePhaseNotification.Cast(), enlistmentOptions).Cast();
            }

            public bool EnlistPromotableSinglePhase(IPromotableSinglePhaseTransactionParticipant promotableSinglePhaseNotification)
            {
                return this.Transaction.EnlistPromotableSinglePhase(promotableSinglePhaseNotification.Cast());
            }

            public IEnlistment EnlistVolatile(ITransactionParticipant enlistmentNotification, EnlistmentOptions enlistmentOptions)
            {
                return this.Transaction.EnlistVolatile(enlistmentNotification.Cast(), enlistmentOptions).Cast();
            }


            public void Rollback()
            {
                this.Transaction.Rollback();
            }

            public void Rollback(Exception e)
            {
                this.Transaction.Rollback(e);
            }

            public void Dispose()
            {
                this.Transaction.Dispose();
            }

            public IEnlistment EnlistVolatile(ISinglePhaseTransactionParticipant singlePhaseNotification, EnlistmentOptions enlistmentOptions)
            {
                return this.Transaction.EnlistVolatile(singlePhaseNotification.Cast(), enlistmentOptions).Cast();
            }
        }

        private class EnlistmentAdapter
            : Adapter<Enlistment>, IEnlistment
        {
            public EnlistmentAdapter(Enlistment enlistment)
                : base(enlistment)
            {

            }

            public void Done()
            {
                this.WrappedObject.Done();
            }
        }

        private class SinglePhaseEnlistmentAdapter : EnlistmentAdapter, ISinglePhaseEnlistment
        {
            public SinglePhaseEnlistment SinglePhaseEnlistment;

            public SinglePhaseEnlistmentAdapter(SinglePhaseEnlistment enlistment)
                : base(enlistment)
            {
                this.SinglePhaseEnlistment = enlistment;
            }

            public void Aborted()
            {
                this.SinglePhaseEnlistment.Aborted();
            }

            public void Aborted(Exception e)
            {
                this.SinglePhaseEnlistment.Aborted(e);
            }

            public void Committed()
            {
                this.SinglePhaseEnlistment.Committed();
            }

            public void InDoubt()
            {
                this.SinglePhaseEnlistment.InDoubt();
            }

            public void InDoubt(Exception e)
            {
                this.SinglePhaseEnlistment.InDoubt(e);
            }
        }

        private class EnlistmentNotificationAdapter : Adapter<ITransactionParticipant>, IEnlistmentNotification
        {
            public ITransactionParticipant TransactionParticipant
            {
                get
                {
                    return this.WrappedObject;
                }
            }

            public EnlistmentNotificationAdapter(ITransactionParticipant transactionParticipant)
                : base(transactionParticipant)
            {

            }

            public void Commit(Enlistment enlistment)
            {
                this.TransactionParticipant.Commit(enlistment.Cast());
            }

            public void InDoubt(Enlistment enlistment)
            {
                this.TransactionParticipant.InDoubt(enlistment.Cast());
            }

            public void Prepare(PreparingEnlistment preparingEnlistment)
            {
                this.TransactionParticipant.Prepare(preparingEnlistment.Cast());
            }

            public void Rollback(Enlistment enlistment)
            {
                this.TransactionParticipant.Rollback(enlistment.Cast());
            }
        }

        private class PreparingEnlistmentAdapter
            : EnlistmentAdapter, IPreparingEnlistment
        {
            public PreparingEnlistment enlistment;

            public PreparingEnlistmentAdapter(PreparingEnlistment enlistment)
                : base(enlistment)
            {
                this.enlistment = enlistment;
            }

            public void ForceRollback()
            {
                this.enlistment.ForceRollback();
            }

            public void ForceRollback(Exception e)
            {
                this.enlistment.ForceRollback(e);
            }

            public void Prepared()
            {
                this.enlistment.Prepared();
            }

            public byte[] RecoveryInformation()
            {
                return this.enlistment.RecoveryInformation();
            }

        }

        private class PromotableSinglePhaseNotificationAdapter
            : Adapter<IPromotableSinglePhaseTransactionParticipant>, IPromotableSinglePhaseNotification
        {

            public PromotableSinglePhaseNotificationAdapter(IPromotableSinglePhaseTransactionParticipant participant)
                : base(participant) { }

            public void Initialize()
            {
                this.WrappedObject.Initialize();
            }

            public void Rollback(SinglePhaseEnlistment singlePhaseEnlistment)
            {
                this.WrappedObject.Rollback(singlePhaseEnlistment.Cast());
            }

            public void SinglePhaseCommit(SinglePhaseEnlistment singlePhaseEnlistment)
            {
                this.WrappedObject.SinglePhaseCommit(singlePhaseEnlistment.Cast());
            }

            public byte[] Promote()
            {
                return this.WrappedObject.Promote();
            }
        }

    }
}

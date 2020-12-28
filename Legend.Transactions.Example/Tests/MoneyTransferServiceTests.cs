using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Transactions.Abstractions;
using Rhino.Mocks;
using System.Transactions;

namespace Legend.Transactions.Example.Tests
{
    [TestFixture]
    public class MoneyTransferServiceTests
    {
#pragma warning disable 618
        private IAccount fromAccount;
        private IAccount toAccount;
        private MoneyTransferService transferService;
        private ITransactionManager transactionManager;
        private ITransactionScope transactionScope;
        private ITransaction transaction;
        private List<ITransactionParticipant> enlistedParticipants;

        [SetUp]
        public void SetUp()
        {
            this.fromAccount = MockRepository.GenerateMock<IAccount>();
            this.toAccount = MockRepository.GenerateMock<IAccount>();

            this.fromAccount.Stub(x => x.Balance).PropertyBehavior();
            this.toAccount.Stub(x => x.Balance).PropertyBehavior();

            this.fromAccount.Balance = 100;
            this.toAccount.Balance = 100;

            this.transactionScope = MockRepository.GenerateMock<ITransactionScope>();
            this.transaction = MockRepository.GenerateMock<ITransaction>();
            this.transactionManager = MockRepository.GenerateMock<ITransactionManager>();
            this.transactionManager.Stub(x => x.CreateScope()).Return(this.transactionScope);
            this.transactionManager.Stub(x => x.CurrentTransaction).Return(this.transaction);

            this.transferService = new MoneyTransferService(this.transactionManager);

            this.enlistedParticipants = new List<ITransactionParticipant>();
            this.transaction
                .Expect(x => x.EnlistVolatile(
                    Arg<ITransactionParticipant>.Is.Anything,
                    Arg<EnlistmentOptions>.Is.Anything))
                .Callback<ITransactionParticipant, EnlistmentOptions>(
                    (x, y) =>
                        {
                            enlistedParticipants.Add(x);
                            return true;
                        })
                .Return(MockRepository.GenerateStub<IEnlistment>())
                .Repeat.Any();
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_throws_when_transactionManager_is_null()
        {
            var manager = new MoneyTransferService(null);
            Assert.Null(manager);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void Transfer_throws_when_fromAccount_is_null()
        {
            this.transferService.Transfer(100, null, this.toAccount);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void Transfer_throws_when_toAccount_is_null()
        {
            this.transferService.Transfer(100, this.fromAccount, null);
        }

        [Test]
        public void Transfer_starts_a_new_transaction_scope()
        {
            this.MakeTransferBetweenAccounts();

            this.transactionManager.AssertWasCalled(x => x.CreateScope());
        }

        [Test]
        public void Transfer_disposes_of_transaction_scope()
        {
            this.MakeTransferBetweenAccounts();

            this.transactionScope.AssertWasCalled(x => x.Dispose());   
        }

        [Test]
        public void Transfer_completes_scope()
        {
            this.MakeTransferBetweenAccounts();

            this.transactionScope.AssertWasCalled(x => x.Complete());
        }

        [Test]
        public void The_balance_of_accounts_is_changed_when_commit_is_called_by_transaction()
        {
            this.MakeTransferBetweenAccounts(10);

            this.PrepareAllEnlisted();
            this.CommitAllEnlisted();

            Assert.AreEqual(90, this.fromAccount.Balance);
            Assert.AreEqual(110, this.toAccount.Balance);
        }

        [Test]
        public void The_balance_of_accounts_is_not_changed_when_rollback_is_called_by_transactions()
        {
            this.MakeTransferBetweenAccounts(10);

            this.PrepareAllEnlisted();
            this.RollbackAllEnlisted();

            Assert.AreEqual(100, this.fromAccount.Balance);
            Assert.AreEqual(100, this.toAccount.Balance);
        }

        
        private void MakeTransferBetweenAccounts()
        {
            this.MakeTransferBetweenAccounts(10m);
        }

        private void MakeTransferBetweenAccounts(decimal amount)
        {
            this.transferService.Transfer(amount, this.fromAccount, this.toAccount);
        }

        private void PrepareAllEnlisted()
        {
            this.AllEnlisted(x => x.Prepare(MockRepository.GenerateStub<IPreparingEnlistment>()));
        }

        private void CommitAllEnlisted()
        {
            this.AllEnlisted(x => x.Commit(MockRepository.GenerateStub<IEnlistment>()));
        }

        private void RollbackAllEnlisted()
        {
            this.AllEnlisted(x => x.Rollback(MockRepository.GenerateStub<IEnlistment>()));
        }

        private void AllEnlisted(Action<ITransactionParticipant> action)
        {
            foreach (var participant in this.enlistedParticipants)
            {
                action(participant);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Transactions.Abstractions;
using Rhino.Mocks;

namespace Legend.Transactions.Example.Tests
{
#pragma warning disable 618
    [TestFixture]
    public class ChangeAccountBalanceCommandTests
    {
        private IAccount account;
        private ChangeAccountBalanceCommand command;

        [SetUp]
        public void SetUp()
        {
            this.account = MockRepository.GenerateMock<IAccount>();
            this.account.Stub(x => x.Balance).PropertyBehavior();
            this.account.Balance = 100m;

            this.command = new ChangeAccountBalanceCommand(10m, this.account);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_throws_when_account_is_null()
        {
            var command = new ChangeAccountBalanceCommand(10, null);
        }

        [Test]
        public void Balance_of_account_is_changed_when_execute_is_called()
        {
            command.Execute();

            Assert.AreEqual(110m, this.account.Balance);
        }

        [Test, ExpectedException(typeof(InvalidOperationException))]
        public void Execute_throws_when_called_more_than_once()
        {
            command.Execute();
            command.Execute();
        }

        [Test]
        public void Prepare_enlists_as_prepared_if_the_command_has_been_executed()
        {
            command.Execute();

            var enlistment = MockRepository.GenerateMock<IPreparingEnlistment>();
            command.Prepare(enlistment);

            enlistment.AssertWasCalled(x => x.Prepared());
        }

        [Test]
        public void Prepare_forces_rollback_if_the_command_has_not_been_executed()
        {
            var enlistment = MockRepository.GenerateMock<IPreparingEnlistment>();
            command.Prepare(enlistment);

            enlistment.AssertWasCalled(x => x.ForceRollback());
        }

        [Test]
        public void Commit_enlists_as_done()
        {
            var enlistment = MockRepository.GenerateMock<IEnlistment>();
            command.Commit(enlistment);

            enlistment.AssertWasCalled(x => x.Done());
        }

        [Test]
        public void Rollback_resets_balance_of_account_if_execute_has_been_called()
        {
            command.Execute();
            command.Rollback(MockRepository.GenerateStub<IEnlistment>());

            Assert.AreEqual(100m, this.account.Balance);
        }

        [Test]
        public void Rollback_does_not_affect_balance_of_account_if_command_has_not_been_executed()
        {
            command.Rollback(MockRepository.GenerateStub<IEnlistment>());

            Assert.AreEqual(100m, this.account.Balance);
        }

        [Test]
        public void Rollback_enlists_as_done()
        {
            var enlistment = MockRepository.GenerateMock<IEnlistment>();
            command.Rollback(enlistment);

            enlistment.AssertWasCalled(x => x.Done());
        }

        [Test]
        public void InDoubt_resets_balance_of_account_if_execute_has_been_called()
        {
            command.Execute();
            command.InDoubt(MockRepository.GenerateStub<IEnlistment>());

            Assert.AreEqual(100m, this.account.Balance);
        }

        [Test]
        public void InDoubt_does_not_affect_balance_of_account_if_command_has_not_been_executed()
        {
            command.InDoubt(MockRepository.GenerateStub<IEnlistment>());

            Assert.AreEqual(100m, this.account.Balance);
        }

        [Test]
        public void InDoubt_enlists_as_done()
        {
            var enlistment = MockRepository.GenerateMock<IEnlistment>();
            command.InDoubt(enlistment);

            enlistment.AssertWasCalled(x => x.Done());
        }

        private ChangeAccountBalanceCommand CreateChangeAccountBalanceCommand(decimal amount)
        {
            return new ChangeAccountBalanceCommand(amount, this.account);
        }
    }

}

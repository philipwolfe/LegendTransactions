using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using System.Transactions.Abstractions;
using System.Transactions;

namespace Legend.Transactions.Tests
{
#pragma warning disable 618
    [TestFixture]
    public class TransactionParticipantTests
    {
        private ITransactionParticipant participant;
        private ITransactionManager transactionManager;
        private ITransaction transaction;

        [SetUp]
        public void SetUp()
        {
            this.participant = MockRepository.GenerateMock<ITransactionParticipant>();
            this.transactionManager = MockRepository.GenerateMock<ITransactionManager>();
            this.transaction = MockRepository.GenerateMock<ITransaction>();

            this.transactionManager.Stub(x => x.CurrentTransaction).Return(this.transaction);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void EnlistVolatile_throws_when_transactionManager_is_null()
        {
            TransactionParticipant.EnlistVolatile(MockRepository.GenerateMock<ITransactionParticipant>(), null);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void EnlistVolatile_throws_when_transactionParticipant_is_null()
        {
            TransactionParticipant.EnlistVolatile(null, MockRepository.GenerateMock<ITransactionManager>());
        }

        [Test]
        public void EnlistVolatile_enlists_transactionParticipant_in_specified_transactionManager()
        {
            this.participant.EnlistVolatile(this.transactionManager);
        }

        [Test]
        public void EnlistVolatile_returns_enlistment_returned_from_EnlistVolatile_on_transaction()
        {
            var enlistment = MockRepository.GenerateStub<IEnlistment>();
            this.transaction
                .Expect(x => x.EnlistVolatile(this.participant, EnlistmentOptions.None))
                .Return(enlistment);

            var returned = this.participant.EnlistVolatile(this.transactionManager);

            Assert.AreSame(enlistment, returned);
        }
    }
}

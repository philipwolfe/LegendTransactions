# LegendTransactions
Legend Transactions aims to make implementation of applications that uses the System.Transactions namespace easier. The main feature is that it provides an abstraction layer over System.Transactions in order to make your classes dependent on transactions easily testable.

# Author
Patrik Hägne
https://stackoverflow.com/users/46187/patrik-h%c3%a4gne

From legendtransactions.codeplex.com

# Project Description
Legend Transactions aims to make implementation of applications that uses the System.Transactions namespace easier. The main feature is that it provides an abstraction layer over System.Transactions in order to make your classes dependent on transactions easily testable.

# What's next?
As of now the project only contains a thin wrapper that delegates to the System.Transactions-framework in order to make it mockable. Next I will add examples of how it can be used in testing to help with test driven development of classes that depend on the System.Transactions classes.

# Other features I plan on incorportating:
- Base class to make the implementing of IEnlistmentNotification (ITransactionParticipant) easier.
- Transactional setting of variables.

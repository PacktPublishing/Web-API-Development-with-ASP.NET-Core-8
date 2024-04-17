namespace InvoiceApp.UnitTests;

// The code comes from Microsoft sample: https://github.com/dotnet/EntityFramework.Docs/blob/main/samples/core/Testing/TestingWithTheDatabase/TransactionalTestDatabaseFixture.cs
[CollectionDefinition("TransactionalTests")]
public class TransactionTestsCollection : ICollectionFixture<TransactionalTestDatabaseFixture>
{
}

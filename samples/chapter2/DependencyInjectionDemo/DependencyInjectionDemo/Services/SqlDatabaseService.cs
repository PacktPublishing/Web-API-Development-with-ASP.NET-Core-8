namespace DependencyInjectionDemo.Services;

public class SqlDatabaseService : IDataService
{
    public string GetData()
    {
        return "Data from SQL Database";
    }
}
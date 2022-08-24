namespace DependencyInjectionDemo.Services;

public class DemoService : IDemoService
{
    private readonly Guid _serviceId;

    public DemoService()
    {
        _serviceId = Guid.NewGuid();
    }

    public string SayHello()
    {
        return $"Hello! My Id is {_serviceId}.";
    }
}


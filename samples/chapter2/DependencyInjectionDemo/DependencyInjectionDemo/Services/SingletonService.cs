namespace DependencyInjectionDemo.Services;

public class SingletonService : ISingletonService
{
    private readonly Guid _serviceId;

    public SingletonService()
    {
        _serviceId = Guid.NewGuid();
    }

    public string Name => nameof(SingletonService);

    public string SayHello()
    {
        return $"Hello! I am {Name}. My Id is {_serviceId}.";
    }
}


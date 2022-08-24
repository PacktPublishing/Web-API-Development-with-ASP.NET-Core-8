namespace DependencyInjectionDemo.Services;

public class TransientService : ITransientService
{
    private readonly Guid _serviceId;

    public TransientService()
    {
        _serviceId = Guid.NewGuid();
    }

    public string Name => nameof(TransientService);
    public string SayHello()
    {
        return $"Hello! I am {Name}. My Id is {_serviceId}.";
    }
}


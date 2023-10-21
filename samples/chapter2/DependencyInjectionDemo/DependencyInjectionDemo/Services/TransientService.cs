namespace DependencyInjectionDemo.Services;

public class TransientService : ITransientService
{
    private readonly Guid _serviceId;
    private readonly DateTime _createdAt;

    public TransientService()
    {
        _serviceId = Guid.NewGuid();
        _createdAt = DateTime.Now;
    }

    public string Name => nameof(TransientService);
    public string SayHello()
    {
        return $"Hello! I am {Name}. My Id is {_serviceId}. I was created at {_createdAt:yyyy-MM-dd HH:mm:ss}.";
    }
}


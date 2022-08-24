namespace DependencyInjectionDemo.Services;

public class ScopedService : IScopedService
{
    private readonly Guid _serviceId;
    private readonly ITransientService _transientService;
    private readonly ISingletonService _singletonService;

    public ScopedService(ITransientService transientService, ISingletonService singletonService)
    {
        _transientService = transientService;
        _singletonService = singletonService;
        _serviceId = Guid.NewGuid();
    }

    public string Name => nameof(ScopedService);

    public string SayHello()
    {
        var scopedServiceMessage = $"Hello! I am {Name}. My Id is {_serviceId}.";
        var transientServiceMessage = $"{_transientService.SayHello()} I am from {Name}.";
        var singletonServiceMessage = $"{_singletonService.SayHello()} I am from {Name}.";
        return
            $"{scopedServiceMessage}{Environment.NewLine}{transientServiceMessage}{Environment.NewLine}{singletonServiceMessage}";
    }
}


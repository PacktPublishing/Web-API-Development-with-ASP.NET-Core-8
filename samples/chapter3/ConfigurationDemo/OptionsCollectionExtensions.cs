namespace ConfigurationDemo;
public static class OptionsCollectionExtensions
{
    public static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseOption>(configuration.GetSection(DatabaseOption.SectionName));
        services.Configure<DatabaseOptions>(DatabaseOptions.SystemDatabaseSectionName, configuration.GetSection($"{DatabaseOptions.SectionName}:{DatabaseOptions.SystemDatabaseSectionName}"));
        services.Configure<DatabaseOptions>(DatabaseOptions.BusinessDatabaseSectionName, configuration.GetSection($"{DatabaseOptions.SectionName}:{DatabaseOptions.BusinessDatabaseSectionName}"));
        return services;
    }
}

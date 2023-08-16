using AsiTest.Business.Contexts;
using AsiTest.Business.Data.Testing;
using AsiTest.Business.Services;

namespace AsiTest.Http.Extensions;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// A collection of extension methods to help application set up
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Configures various components for data services
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <param name="configuration"></param>
    public static void ConfigureDataServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var databaseName = configuration["database:name"];
        
        serviceCollection.AddDbContext<ApplicationContext>(o => o.UseInMemoryDatabase(databaseName));

        serviceCollection.AddScoped<IContactService, ContactService>();
    }

    /// <summary>
    /// Seeds data for immediate usage of application
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <param name="configuration"></param>
    public static void SeedData(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        using var serviceProvider = serviceCollection.BuildServiceProvider();
        var contactService = serviceProvider.GetRequiredService<IContactService>();
        var applicationDataSeeder = new ApplicationDataSeeder(contactService);
        
        var contactGenerationAmountConfig = configuration["database:contactGenerationAmount"];
        var emailMaxConfig = configuration["database:emailMax"];

        if (int.TryParse(contactGenerationAmountConfig, out var contactGenerationAmount) &&
            int.TryParse(emailMaxConfig, out var emailMax))
        {
            applicationDataSeeder.InitializeData(contactGenerationAmount, emailMax);
        }
        else
        {
            applicationDataSeeder.InitializeData();
        }
    }
}
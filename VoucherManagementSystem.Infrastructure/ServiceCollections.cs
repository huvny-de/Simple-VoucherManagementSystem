using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using VoucherManagementSystem.Domain.Entities;
using VoucherManagementSystem.Domain.Interfaces;
using VoucherManagementSystem.Infrastructure.Configuration;
using VoucherManagementSystem.Infrastructure.Repositories;

namespace VoucherManagementSystem.Infrastructure;

public static class ServiceCollections
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Register MongoDB camelCase convention BEFORE any database operations
        var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
        ConventionRegistry.Register("camelCase", conventionPack, type => true);
        
        // Register class maps
        if (!BsonClassMap.IsClassMapRegistered(typeof(User)))
        {
            BsonClassMap.RegisterClassMap<User>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
        }
        
        if (!BsonClassMap.IsClassMapRegistered(typeof(Promotion)))
        {
            BsonClassMap.RegisterClassMap<Promotion>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
        }
        
        if (!BsonClassMap.IsClassMapRegistered(typeof(Voucher)))
        {
            BsonClassMap.RegisterClassMap<Voucher>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
        }
        
        services.Configure<DatabaseSettings>(configuration.GetSection("DatabaseSettings"));

        var databaseSettings = configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>() ?? new DatabaseSettings();

        services.AddSingleton<IMongoClient>(sp => new MongoClient(databaseSettings.ConnectionString));
        services.AddScoped<IMongoDatabase>(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(databaseSettings.DatabaseName);
        });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPromotionRepository, PromotionRepository>();
        services.AddScoped<IVoucherRepository, VoucherRepository>();

        return services;
    }
}

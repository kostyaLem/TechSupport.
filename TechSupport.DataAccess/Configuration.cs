﻿using Microsoft.Extensions.DependencyInjection;
using TechSupport.DataAccess.Context;

namespace TechSupport.DataAccess;

public static class Configuration
{
    public static void AddDataAccessLayer(this IServiceCollection serviceCollection)
    {
        // Регистрация классов базы данных
        serviceCollection.AddTransient<TechSupportContextFactory>();
        serviceCollection.AddTransient<TechSupportContext>(x =>
        {
            var factory = x.GetRequiredService<TechSupportContextFactory>();
            return factory.CreateDbContext();
        });
    }
}

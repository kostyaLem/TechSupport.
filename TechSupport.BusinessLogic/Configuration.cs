﻿using Microsoft.Extensions.DependencyInjection;
using TechSupport.BusinessLogic.Interfaces;
using TechSupport.BusinessLogic.Services;
using TechSupport.DataAccess;

namespace TechSupport.BusinessLogic;

public static class Configuration
{
    public static void AddBusinessLogicServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IAuthorizationService, AuthorizationService>();

        serviceCollection.AddDataAccessLayer();
    }
}

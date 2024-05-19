using FullPotential.Management.Features.Instances;
using FullPotential.Management.Features.Users;
using FullPotential.Management.Utilities;

namespace FullPotential.Management;

public static class ServicesConfig
{
    public static void ConfigureServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ITimeProvider, SystemTimeProvider>();

        serviceCollection.AddScoped<ICryptoService, CryptoService>();
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<IInstanceService, InstanceService>();
    }
}


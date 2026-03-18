using Application.Interactors.UserInteractors;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssemblyContaining<GetAllUserInteractor>()
            );

            return services;
        }
    }
}

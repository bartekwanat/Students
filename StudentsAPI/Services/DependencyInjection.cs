using System.Reflection;
using MediatR;

namespace StudentsAPI.Services
{
    public static class DependencyInjection
    {
        public static void AddServiceLayer(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }


    }
}

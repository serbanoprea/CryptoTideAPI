using Interfaces.ITableUnitOfWork;
using Interfaces.Services;
using Interfaces.UnitOfWork.IDBUnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using Services;
using UnitsOfWork.DatabaseUnitOfWork;
using UnitsOfWork.TableUnitOfWork;

namespace IoC
{
    public class DependencyInjection
    {
        public static void Bind(IServiceCollection services)
        {
            services.AddTransient<IDBUnitOfWork, DBUnitOfWork>();
            services.AddTransient<IIngressService, IngressService>();
            services.AddTransient<ITableUnitOfWork, TableUnitOfWork>();
            services.AddScoped<IUsersService, UsersService>();
        }
    }
}

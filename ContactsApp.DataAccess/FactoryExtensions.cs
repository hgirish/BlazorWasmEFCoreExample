using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ContactsApp.DataAccess
{
    public static class FactoryExtensions
    {
        public static IServiceCollection AddDbContextFactoryWithLifespan<TContext>(
            this IServiceCollection collection,
            Action<DbContextOptionsBuilder> optionsAction = null,
            ServiceLifetime contextAndOptionsLifetime = ServiceLifetime.Singleton)
            where TContext : DbContext
        {
            collection.Add(new ServiceDescriptor(
                typeof(DbContextOptions<TContext>),
                sp => GetOptions<TContext>(optionsAction, sp),
                contextAndOptionsLifetime));

            return collection;
        }

        private static DbContextOptions<TContext> GetOptions<TContext>(
            Action<DbContextOptionsBuilder> action, 
            IServiceProvider sp = null) where TContext : DbContext
        {
            var optionsBuilder = new DbContextOptionsBuilder<TContext>();

            if (sp != null)
            {
                optionsBuilder.UseApplicationServiceProvider(sp);
            }

            action?.Invoke(optionsBuilder);
            return optionsBuilder.Options;
        }
    }
}

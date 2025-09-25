using Application.Interfaces;
using Application.Users.Mapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Settings
{
    public static class AddEntityMappersExtensions
    {
        /// <summary>
        /// Registers all concrete classes that implement the generic
        /// <c>IEntityMapper&lt;TEntity, TCreateDto, TUpdateDto, TResponseDto&gt;</c>
        /// interface from the assembly where those classes are defined.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to register mappers with.</param>
        /// <returns>The updated <see cref="IServiceCollection"/> for chaining.</returns>
        /// <remarks>
        /// This method scans the assembly containing the mapper implementations (not the extension method itself),
        /// and registers each mapper as a scoped service for its corresponding interface.
        /// Useful for auto-registering feature-specific mappers like <c>UserMapper</c>, <c>CustomerMapper</c>, etc.,
        /// without manual DI setup.
        /// </remarks>

        public static IServiceCollection AddEntityMappers(this IServiceCollection services)
        {
            var assembly = typeof(UserMapper).Assembly;

            var mapperTypes = assembly.GetTypes()
                .Where(type => !type.IsAbstract && !type.IsInterface)
                .SelectMany(type => type.GetInterfaces(), (type, iface) => new { type, iface })
                .Where(x => x.iface.IsGenericType &&
                            x.iface.GetGenericTypeDefinition() == typeof(IEntityMapper<,,,>));

            foreach (var mapper in mapperTypes)
            {
                services.AddScoped(mapper.iface, mapper.type);
            }

            return services;
        }
    }

}

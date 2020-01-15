using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RegisterCoresExtensions
    {
        public static void AddCores (this IServiceCollection services, Assembly assembly)
        {

            var types = assembly.GetTypes ();

            types.Where (t => !t.GetTypeInfo ().IsAbstract &&
                    t.Name.EndsWith ("Core", StringComparison.CurrentCultureIgnoreCase) &&
                    t.GetInterfaces ().Length == 1 &&
                    t.GetInterfaces () [0].Name.EndsWith ("Core", StringComparison.CurrentCultureIgnoreCase))
                .ToList ()
                .ForEach (t =>
                {
                    services.AddScoped (t.GetInterfaces () [0], t);
                });

        }
    }
}
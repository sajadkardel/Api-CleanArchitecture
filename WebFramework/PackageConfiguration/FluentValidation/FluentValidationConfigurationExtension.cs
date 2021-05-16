using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;

namespace WebFramework.PackageConfiguration.FluentValidation
{
    public static class FluentValidationConfigurationExtension
    {
        /// <summary>
        /// this method dos not modelBuilder method. this is register all dto validator for Fluent Validation Library
        /// </summary>
        /// <typeparam name="TInterFace"></typeparam>
        /// <param name="configuration"></param>
        /// <param name="assemblies"></param>
        public static void RegisterAllDtoValidators<TInterFace>(this FluentValidationMvcConfiguration configuration, params Assembly[] assemblies)
        {
            IEnumerable<Type> types = assemblies.SelectMany(a => a.GetExportedTypes())
                .Where(c => c.IsClass && !c.IsAbstract && c.IsPublic && typeof(TInterFace).IsAssignableFrom(c));

            foreach (Type type in types)
            {
                configuration.RegisterValidatorsFromAssemblyContaining(type);
            }

        }
    }
}

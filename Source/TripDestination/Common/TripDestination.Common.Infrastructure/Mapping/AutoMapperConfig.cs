namespace TripDestination.Common.Infrastructure.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using AutoMapper;

    public static class AutoMapperConfig
    {
        public static IMapper Mapper { get; private set; }

        public static MapperConfiguration Configuration { get; private set; }

        private static object syncRoot = new Object();

        public static void Register(Assembly assembly)
        {
            Configuration = new MapperConfiguration(
                cfg =>
                {
                    var types = assembly.GetExportedTypes();
                    LoadStandardMappings(types, cfg);
                    LoadReverseMappings(types, cfg);
                    LoadCustomMappings(types, cfg);
                });

            Mapper = Configuration.CreateMapper();
        }

        private static void LoadStandardMappings(IEnumerable<Type> types, IMapperConfiguration mapperConfiguration)
        {
            var maps = types
                .Where(t => !t.IsInterface && !t.IsAbstract && t.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>))
                )
                .Select(t => new
                {
                    Source = t.GetInterfaces()
                                .Where(i => i.GetGenericTypeDefinition() == typeof(IMapFrom<>))
                                .FirstOrDefault()
                                .GetGenericArguments()[0],
                    Destination = t
                })
                .ToList();

            maps.ForEach(m => mapperConfiguration.CreateMap(m.Source, m.Destination));
        }

        private static void LoadReverseMappings(IEnumerable<Type> types, IMapperConfiguration mapperConfiguration)
        {
            var maps = types
                .Where(t => !t.IsInterface && !t.IsAbstract && t.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapTo<>))
                )
                .Select(t => new
                {
                    Source = t,
                    Destination = t.GetInterfaces()
                                .Where(i => i.GetGenericTypeDefinition() == typeof(IMapTo<>))
                                .FirstOrDefault()
                                .GetGenericArguments()[0]
                })
                .ToList();

            maps.ForEach(m => mapperConfiguration.CreateMap(m.Source, m.Destination));
        }

        private static void LoadCustomMappings(IEnumerable<Type> types, IMapperConfiguration mapperConfiguration)
        {
            var maps = types
                .Where(t => !t.IsInterface && !t.IsAbstract && t.GetInterfaces()
                    .Any(i => typeof(IHaveCustomMappings).IsAssignableFrom(i))
                )
                .Select(t => (IHaveCustomMappings)Activator.CreateInstance(t))
                .ToList();

            maps.ForEach(m => m.CreateMappings(mapperConfiguration));
        }
    }
}

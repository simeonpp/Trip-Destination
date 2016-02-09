namespace TripDestination.Web.MVC
{
    using System;
    using AutoMapper;
    using Models;
    using ViewModels;
    using System.Reflection;
    using System.Linq;
    using System.Collections.Generic;
    using Common.Infrastructure.Mapping;
    public static class AutoMapperConfig
    {
        private static IMapper mapper;

        private static MapperConfiguration configuration;

        private static object syncRoot = new Object();

        public static IMapper GetMapper
        {
            get
            {
                if (mapper == null)
                {
                    lock (syncRoot)
                    {
                        if (mapper == null)
                            Register();
                    }
                }

                return mapper;
            }
        }


        public static void Register()
        {
            var types = Assembly.GetExecutingAssembly().GetExportedTypes();

            LoadStandardMappings(types);
            LoadCustomMappings(types);
            mapper = configuration.CreateMapper();
        }

        private static void LoadStandardMappings(IEnumerable<Type> types)
        {
            var maps = types
                .Where(t => !t.IsInterface && !t.IsAbstract && t.GetInterfaces()
                    .Any(i => i.IsGenericType && (
                        i.GetGenericTypeDefinition() == typeof(IMapFrom<>) || i.GetGenericTypeDefinition() == typeof(IMapTo<>)
                    ))
                )
                .Select(t => new
                {
                    Source = t.GetInterfaces()
                                .Where(i => i.GetGenericTypeDefinition() == typeof(IMapFrom<>) || i.GetGenericTypeDefinition() == typeof(IMapTo<>))
                                .FirstOrDefault()
                                .GetGenericArguments()[0],
                    Destination = t
                })
                .ToList();

            configuration = new MapperConfiguration(cfg =>
                maps.ForEach(
                    m => cfg.CreateMap(m.Source, m.Destination))
            );
        }

        private static void LoadCustomMappings(IEnumerable<Type> types)
        {
            var maps = types
                    .Where(t => !t.IsAbstract && !t.IsInterface && t.GetInterfaces()
                        .Any(i => typeof(IHaveCustomMappings).IsAssignableFrom(t))
                    )
                    .Select(t => (IHaveCustomMappings)Activator.CreateInstance(t))
                    .ToList();

            foreach (var map in maps)
            {
                map.CreateMappings(configuration);
            }
        }

    }
}
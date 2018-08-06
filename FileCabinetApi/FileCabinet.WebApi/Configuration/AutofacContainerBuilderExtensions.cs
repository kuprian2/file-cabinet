using Autofac;
using AutoMapper;
using FileCabinet.Bll.Configuration;
using System;
using System.Linq;

namespace FileCabinet.WebApi.Configuration
{
    public static class AutofacContainerBuilderExtensions
    {
        public static ContainerBuilder ResolveDependencies(this ContainerBuilder builder)
        {
            builder.RegisterModule<ServiceModule>();
            return builder;
        }

        public static ContainerBuilder UseAutoMapper(this ContainerBuilder builder)
        {
            if (builder == null) throw new NullReferenceException();

            var autoMapperProfileTypes =
                AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(a =>
                        a.GetTypes().Where(p =>
                            typeof(Profile).IsAssignableFrom(p) &&
                            p.IsPublic &&
                            !p.IsAbstract));

            var autoMapperProfiles =
                autoMapperProfileTypes
                    .Select(p => (Profile)Activator.CreateInstance(p));

            builder
                .Register(ctx => new MapperConfiguration(cfg =>
                {
                    foreach (var profile in autoMapperProfiles)
                    {
                        cfg.AddProfile(profile);
                    }
                }));

            builder
                .Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper())
                .As<IMapper>();

            return builder;
        }
    }
}
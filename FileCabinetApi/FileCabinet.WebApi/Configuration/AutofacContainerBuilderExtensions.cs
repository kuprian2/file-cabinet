using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using FileCabinet.Bll.Configuration;
using FileCabinet.WebApi.IdentityModels;
using FileCabinet.WebApi.ModelBinders;
using FileCabinet.WebApi.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileCabinet.WebApi.Configuration
{
    public static class AutofacContainerBuilderExtensions
    {
        public static ContainerBuilder ResolveDependencies(this ContainerBuilder builder)
        {
            builder.RegisterModule<ServiceModule>();

            builder
                .RegisterType<ApplicationDbContext>()
                .AsSelf()
                .InstancePerRequest();

            builder
                .RegisterType<ApplicationUserStore<ApplicationUser>>()
                .AsImplementedInterfaces()
                .InstancePerRequest();

            builder
                .Register(c =>
                    new IdentityFactoryOptions<ApplicationUserManager>
                    {
                        DataProtectionProvider =
                            new DpapiDataProtectionProvider("FileCabinet.WebApi")
                    });

            builder
                .RegisterType<ApplicationUserManager>()
                .AsSelf()
                .InstancePerRequest();

            builder
                .RegisterType<CommaDelimitedArrayModelBinder>()
                .AsModelBinderForTypes(typeof(IEnumerable<TagInfoModel>));

            return builder;
        }

        public static ContainerBuilder UseAutoMapper(this ContainerBuilder builder)
        {
            if (builder == null) throw new NullReferenceException();

            var autoMapperProfileTypes =
                AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(a =>
                        a.GetTypes()
                            .Where(p =>
                                typeof(Profile).IsAssignableFrom(p) &&
                                p.IsPublic &&
                                !p.IsAbstract));

            var autoMapperProfiles =
                autoMapperProfileTypes
                    .Select(p => (Profile)Activator.CreateInstance(p));

            builder
                .Register(ctx =>
                    new MapperConfiguration(cfg =>
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
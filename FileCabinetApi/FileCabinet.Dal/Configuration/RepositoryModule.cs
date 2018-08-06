using Autofac;
using FileCabinet.Dal.Contracts.Repositories;
using FileCabinet.Dal.Contracts.UoW;
using FileCabinet.Dal.EF;
using FileCabinet.Dal.Repositories;
using FileCabinet.Dal.UoW;
using System.Configuration;
using System.Data.Entity;

namespace FileCabinet.Dal.Configuration
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["fileCabinet"].ConnectionString;

            builder//.RegisterType<DbContext>().AsSelf()
                .RegisterType<FileCabinetDbContext>()
                .As<DbContext>()
                .WithParameter("connectionString", connectionString)
                .InstancePerLifetimeScope();

            builder.
                RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<,>))
                .InstancePerLifetimeScope();

            builder
                .RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerRequest();

            base.Load(builder);
        }
    }
}

using Data.Configuration.Context;
using Data.Dto;
using Data.Messaging.Publisher;
using Data.Repository.Implementation;
using Data.Repository.Implementation.UnitOfWork;
using Data.Util.AutoMapper;
using Data.Util.Validation;
using Domain.Repository.Interface;
using Domain.UseCase;
using FluentValidation;

namespace API.Extensions
{
    public static class DependenciesExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);

            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddScoped<IUnitOfWork<AppDbContext>, UnitOfWork<AppDbContext>>();
            services.AddScoped<IUnitOfWork<LogDbContext>, UnitOfWork<LogDbContext>>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductUseCase, ProductUseCase>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserUseCase, UserUseCase>();

            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<ILogUseCase, LogUseCase>();

            services.AddScoped<IEventPublisher, EventPublisher>();

            services.AddScoped<IValidator<UserDto>, UserDtoValidator>();

            return services;
        }
    }
}

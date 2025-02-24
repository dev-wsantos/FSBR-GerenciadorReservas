using GerenciadorReservas.Application.Interfaces;
using GerenciadorReservas.Application.Mappings;
using GerenciadorReservas.Application.Services;
using GerenciadorReservas.Domain.Factories;
using GerenciadorReservas.Domain.Interfaces;
using GerenciadorReservas.Infra.Data.Data.Context;
using GerenciadorReservas.Infra.Data.Data.Repositories;
using GerenciadorReservas.Infra.Data.Data.UnitOfWork;
using GerenciadorReservas.Infra.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GerenciadorReservas.Infra.IoC
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                a => a.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ISalaRepository, SalaRepository>();
            services.AddScoped<IReservaRepository, ReservaRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ISalaService, SalaService>();
            services.AddScoped<IReservaService, ReservaService>();
            services.AddScoped<IReservaFactory, ReservaFactory>();

            services.AddSingleton<IEmailService, EmailService>();

            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            return services;
        }
    }
}

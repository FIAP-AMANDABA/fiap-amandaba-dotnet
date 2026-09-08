using Amandaba.Domain.Entities;
using Amandaba.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Amandaba.Tests.Integration
{
    public class AmandabaApiFactory
        : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(
            IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    service =>
                        service.ServiceType ==
                        typeof(DbContextOptions<ApplicationContext>)
                );

                if (descriptor is not null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<ApplicationContext>(options =>
                {
                    options.UseInMemoryDatabase(
                        "AmandabaIntegrationTests"
                    );
                });

                var serviceProvider =
                    services.BuildServiceProvider();

                using var scope =
                    serviceProvider.CreateScope();

                var context = scope.ServiceProvider
                    .GetRequiredService<ApplicationContext>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Seed(context);
            });
        }

        private static void Seed(ApplicationContext context)
        {
            var especie = new EspecieEntity
            {
                IdEspecie = 1,
                Nome = "CACHORRO"
            };

            var pet = new PetEntity
            {
                IdPet = 1,
                IdTutor = 1,
                IdEspecie = 1,
                Nome = "Luna",
                Raca = "Golden Retriever",
                Sexo = "FEMEA",
                DataNascimento = new DateTime(2020, 4, 10),
                Cor = "Dourado",
                Castrado = true,
                DataCadastro = DateTime.Now,
                Status = "ATIVO",
                Especie = especie
            };

            context.Especies.Add(especie);
            context.Pets.Add(pet);

            context.SaveChanges();
        }
    }
}
using Application.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Presentation.Menus;

namespace Presentation
{
    public class AppRunner
    {
        public async Task RunAsync()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddUserSecrets<ZooDbContextFactory>()
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new Exception("Connection string was not found.");

            DbContextOptions<ZooDbContext> options = new DbContextOptionsBuilder<ZooDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            using ZooDbContext context = new ZooDbContext(options);

            AnimalRepository animalRepository = new AnimalRepository(context);
            HabitatRepository habitatRepository = new HabitatRepository(context);
            VisitorRepository visitorRepository = new VisitorRepository(context);
            VisitRepository visitRepository = new VisitRepository(context);

            AnimalService animalService = new AnimalService(animalRepository);
            HabitatService habitatService = new HabitatService(habitatRepository);
            VisitorService visitorService = new VisitorService(visitorRepository);
            VisitService visitService = new VisitService(visitRepository);

            MainMenu mainMenu = new MainMenu(
                animalService,
                habitatService,
                visitorService,
                visitService
            );

            await mainMenu.StartMainMenu();
        }
    }
}
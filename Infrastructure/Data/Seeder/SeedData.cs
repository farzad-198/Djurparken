using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public static class SeedData
    {
        public static async Task SeedAsync(ZooDbContext context)
        {
            bool hasData = await context.Habitats.AnyAsync()
                || await context.Animals.AnyAsync()
                || await context.Visitors.AnyAsync()
                || await context.Visits.AnyAsync();

            if (hasData)
            {
                return;
            }

            Habitat savanna = new Habitat
            {
                Id = Guid.NewGuid(),
                Name = "Savanna",
                Climate = "Warm and dry",
                Vegetation = "Grassland"
            };

            Habitat rainforest = new Habitat
            {
                Id = Guid.NewGuid(),
                Name = "Rainforest",
                Climate = "Warm and humid",
                Vegetation = "Dense tropical plants"
            };

            Habitat arcticZone = new Habitat
            {
                Id = Guid.NewGuid(),
                Name = "Arctic Zone",
                Climate = "Cold",
                Vegetation = "Moss and low vegetation"
            };

            await context.Habitats.AddRangeAsync(savanna, rainforest, arcticZone);
            await context.SaveChangesAsync();

            Animal lion = new Animal
            {
                Id = Guid.NewGuid(),
                Name = "Leo",
                Species = "Lion",
                BirthDate = new DateTime(2018, 5, 12),
                Gender = "Male",
                Status = "Healthy",
                HabitatId = savanna.Id
            };

            Animal elephant = new Animal
            {
                Id = Guid.NewGuid(),
                Name = "Ella",
                Species = "Elephant",
                BirthDate = new DateTime(2015, 9, 3),
                Gender = "Female",
                Status = "Healthy",
                HabitatId = savanna.Id
            };

            Animal monkey = new Animal
            {
                Id = Guid.NewGuid(),
                Name = "Milo",
                Species = "Monkey",
                BirthDate = new DateTime(2020, 3, 8),
                Gender = "Male",
                Status = "Under observation",
                HabitatId = rainforest.Id
            };

            Animal tiger = new Animal
            {
                Id = Guid.NewGuid(),
                Name = "Tara",
                Species = "Tiger",
                BirthDate = new DateTime(2017, 11, 21),
                Gender = "Female",
                Status = "Healthy",
                HabitatId = rainforest.Id
            };

            Animal penguin = new Animal
            {
                Id = Guid.NewGuid(),
                Name = "Pablo",
                Species = "Penguin",
                BirthDate = new DateTime(2021, 1, 15),
                Gender = "Male",
                Status = "Healthy",
                HabitatId = arcticZone.Id
            };

            await context.Animals.AddRangeAsync(lion, elephant, monkey, tiger, penguin);
            await context.SaveChangesAsync();

            Visitor john = new Visitor
            {
                Id = Guid.NewGuid(),
                FullName = "John Smith",
                PhoneNumber = "0701234567",
                Age = 34
            };

            Visitor emma = new Visitor
            {
                Id = Guid.NewGuid(),
                FullName = "Emma Johnson",
                PhoneNumber = "0709876543",
                Age = 27
            };

            Visitor oliver = new Visitor
            {
                Id = Guid.NewGuid(),
                FullName = "Oliver Brown",
                PhoneNumber = "0705551122",
                Age = 41
            };

            Visitor sophia = new Visitor
            {
                Id = Guid.NewGuid(),
                FullName = "Sophia Davis",
                PhoneNumber = "0704448899",
                Age = 22
            };

            await context.Visitors.AddRangeAsync(john, emma, oliver, sophia);
            await context.SaveChangesAsync();

            Visit visit1 = new Visit
            {
                Id = Guid.NewGuid(),
                VisitorId = john.Id,
                VisitDate = new DateTime(2026, 5, 1),
                HasPaidTicket = true
            };

            Visit visit2 = new Visit
            {
                Id = Guid.NewGuid(),
                VisitorId = emma.Id,
                VisitDate = new DateTime(2026, 5, 3),
                HasPaidTicket = true
            };

            Visit visit3 = new Visit
            {
                Id = Guid.NewGuid(),
                VisitorId = oliver.Id,
                VisitDate = new DateTime(2026, 5, 10),
                HasPaidTicket = false
            };

            Visit visit4 = new Visit
            {
                Id = Guid.NewGuid(),
                VisitorId = sophia.Id,
                VisitDate = new DateTime(2026, 5, 15),
                HasPaidTicket = true
            };

            await context.Visits.AddRangeAsync(visit1, visit2, visit3, visit4);
            await context.SaveChangesAsync();
        }
    }
}
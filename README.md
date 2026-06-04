# Djurparken

Detta är ett konsolbaserat projekt för att hantera en djurpark. Projektet är byggt med C#, Entity Framework Core och SQL Server.

Projektet följer en enkel Onion Architecture med separata lager för Domain, Application, Infrastructure och Presentation.

## Funktioner

Programmet innehåller CRUD-funktionalitet för:

- Djur
- Habitat
- Besökare
- Besök

Det finns också en statistikmeny där man kan se information om antal djur, habitat, besökare och besök.
Tekniker

Projektet använder:

C#
.NET
Entity Framework Core
SQL Server
LINQ
Async/await
Repository Pattern
Service Layer
User Secrets
Code First Migrations
Databas

Projektet använder Entity Framework Core Code First.

Connection string sparas med User Secrets och används av både AppRunner och ZooDbContextFactory.

Seed Data

Projektet innehåller seed data som lägger till exempeldata första gången programmet körs.

Seed data innehåller exempel på:

Habitat
Djur
Besökare
Besök

Seed data körs i AppRunner:
await SeedData.SeedAsync(context);

Menysystem

Programmet har ett konsolbaserat menysystem.

Huvudmenyn innehåller:
1. Manage animals
2. Manage habitats
3. Manage visitors
4. Manage visits
5. Show statistics
0. Exit

Varje meny har funktioner för att lägga till, visa, uppdatera, ta bort och söka information.

Hantering av ID

Entity-klasserna använder Guid som ID i databasen.

För att göra programmet mer användarvänligt visas inte Guid för användaren i menyerna. I stället visas listor med enkla nummer.
1. Leo - Lion - Healthy
2. Ella - Elephant - Healthy
3. Milo - Monkey - Under observation

Användaren väljer ett nummer, och programmet använder sedan rätt Guid internt.

Exempel på funktionalitet

När man lägger till ett djur behöver man inte skriva in ett Habitat ID manuellt. Programmet visar i stället en lista med habitat:
1. Savanna
2. Rainforest
3. Arctic Zone
Användaren väljer ett nummer och programmet kopplar djuret till rätt habitat.
Syfte

Syftet med projektet är att visa grundläggande kunskaper i:

Objektorienterad programmering
Databashantering med Entity Framework Core
CRUD-operationer
Konsolbaserat användargränssnitt
Enkel lagerindelad arkitektur
Async-programmering

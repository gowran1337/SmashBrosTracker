using SmashBrosTrackerGoran.Data;
using SmashBrosTrackerGoran.Models;

public static class SampleData
{
    public static void Initialize(SmashDbContext context)
    {
        context.Database.EnsureCreated(); // Ensure the database is created

        if (!context.Characters.Any()) // Check if the Characters table is empty
        {
            var characters = new List<Character>
            {
                new Character { Name = "Fox" },
                new Character { Name = "Falco" },
                new Character { Name = "Marth" },
                new Character { Name = "Sheik" },
                new Character { Name = "Captain Falcon" },
                new Character { Name = "Peach"  },
                new Character { Name = "Jigglypuff",},
                new Character { Name = "Luigi",}
            };

            context.Characters.AddRange(characters); // Add the characters to the table
            context.SaveChanges(); // Save the changes
        }
    }
}
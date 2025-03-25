using Microsoft.EntityFrameworkCore;
using SmashBrosTrackerGoran.Models;

namespace SmashBrosTrackerGoran.Data
{
    public class SmashDbContext : DbContext
    {
        public SmashDbContext(DbContextOptions<SmashDbContext> options)
            : base(options)
        {
        }
        //tables
        public DbSet<Player> Players { get; set; }
        public DbSet<KebabDebt> KebabDebts { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Character> Characters { get; set; }

        //junction tables
        public DbSet<SessionPlayer> SessionPlayers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. One-to-Many: Character to Player
            modelBuilder.Entity<Player>()
                .HasOne(p => p.Character) // A player has one character
                .WithMany(c => c.Players) // A character can have many players
                .HasForeignKey(p => p.CharacterId); // Foreign key in Player table

            // 2. Many-to-Many: Session to Player via SessionPlayer
            modelBuilder.Entity<SessionPlayer>()
                .HasKey(sp => new { sp.SessionId, sp.PlayerId }); // Composite primary key

            modelBuilder.Entity<SessionPlayer>()
                .HasOne(sp => sp.Session) // A SessionPlayer belongs to one Session
                .WithMany(s => s.SessionPlayers) // A Session can have many SessionPlayers
                .HasForeignKey(sp => sp.SessionId); // Foreign key to Session

            modelBuilder.Entity<SessionPlayer>()
                .HasOne(sp => sp.Player) // A SessionPlayer belongs to one Player
                .WithMany(p => p.SessionPlayers) // A Player can have many SessionPlayers
                .HasForeignKey(sp => sp.PlayerId); // Foreign key to Player

            // 3. One-to-Many: Player to KebabDebt (DebtsOwed)
            modelBuilder.Entity<KebabDebt>()
                .HasOne(kd => kd.OwedBy) // A KebabDebt is owed by one Player
                .WithMany(p => p.DebtsOwed) // A Player can owe many KebabDebts
                .HasForeignKey(kd => kd.OwedById) // Foreign key to Player (OwedBy)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            // 4. One-to-Many: Player to KebabDebt (DebtsReceived)
            modelBuilder.Entity<KebabDebt>()
                .HasOne(kd => kd.OwedTo) // A KebabDebt is owed to one Player
                .WithMany(p => p.DebtsReceived) // A Player can receive many KebabDebts
                .HasForeignKey(kd => kd.OwedToId) // Foreign key to Player (OwedTo)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            // 5. One-to-Many: Session to Player (Winner)
            modelBuilder.Entity<Session>()
                .HasOne(s => s.Winner) // A Session has one Winner (Player)
                .WithMany() // A Player can win many Sessions
                .HasForeignKey(s => s.WinnerId) // Foreign key to Player (Winner)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete
        }

    }
}

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

        // Tables
        public DbSet<Player> Players { get; set; }
        public DbSet<KebabDebt> KebabDebts { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<SessionPlayer> SessionPlayers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure SessionPlayer as the join table with additional properties
            modelBuilder.Entity<SessionPlayer>(entity =>
            {
                // Composite primary key
                entity.HasKey(sp => new { sp.SessionId, sp.PlayerId });

                // Relationship to Session
                entity.HasOne(sp => sp.Session)
                      .WithMany(s => s.SessionPlayers)
                      .HasForeignKey(sp => sp.SessionId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Relationship to Player
                entity.HasOne(sp => sp.Player)
                      .WithMany(p => p.SessionPlayers)
                      .HasForeignKey(sp => sp.PlayerId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Relationship to Character
                entity.HasOne(sp => sp.Character)
                      .WithMany()
                      .HasForeignKey(sp => sp.CharacterId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure Session's Winner relationship
            modelBuilder.Entity<Session>(entity =>
            {
                entity.HasOne(s => s.Winner)
                      .WithMany()
                      .HasForeignKey(s => s.WinnerId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure KebabDebt relationships
            modelBuilder.Entity<KebabDebt>(entity =>
            {
                // OwedBy relationship
                entity.HasOne(kd => kd.OwedBy)
                      .WithMany(p => p.DebtsOwed)
                      .HasForeignKey(kd => kd.OwedById)
                      .OnDelete(DeleteBehavior.Restrict);

                // OwedTo relationship
                entity.HasOne(kd => kd.OwedTo)
                      .WithMany(p => p.DebtsReceived)
                      .HasForeignKey(kd => kd.OwedToId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure Player-Character relationship (if needed)
            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasOne(p => p.Character)
                      .WithMany(c => c.Players)
                      .HasForeignKey(p => p.CharacterId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
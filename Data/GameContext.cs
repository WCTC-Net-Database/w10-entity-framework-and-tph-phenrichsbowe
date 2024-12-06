using Microsoft.EntityFrameworkCore;
using W9_assignment_template.Models;

namespace W9_assignment_template.Data;

public class GameContext : DbContext
{
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<Ability> Abilities { get; set; }

    public GameContext(DbContextOptions<GameContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Character>()
            .HasDiscriminator<string>("Discriminator")
            .HasValue<Player>("Player")
            .HasValue<Goblin>("Goblin");

        modelBuilder.Entity<Ability>()
            .HasDiscriminator<string>("AbilityType")
            .HasValue<GoblinAbility>("GoblinAbility")
            .HasValue<PlayerAbility>("PlayerAbility");

        modelBuilder.Entity<Character>()
                .HasMany(c => c.Abilities)
                .WithMany(a => a.Characters)
                .UsingEntity(j => j.ToTable("CharacterAbilities"));

        base.OnModelCreating(modelBuilder);
    }
}
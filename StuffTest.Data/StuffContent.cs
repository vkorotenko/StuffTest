using Microsoft.EntityFrameworkCore;
using StuffTest.Model;
using System.Linq;

namespace StuffTest.Data;

public class StuffContent : DbContext
{
    public DbSet<User> Users { get; set; }



    public StuffContent(DbContextOptions<StuffContent> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
        ConfigureModelBuilderForPosition(modelBuilder);
        ConfigureModelBuilderForUser(modelBuilder);

    }

    private static void ConfigureModelBuilderForPosition(ModelBuilder modelBuilder)
    {
        var bulder = modelBuilder.Entity<Position>();

        bulder.ToTable("position");
        bulder.HasKey(x => x.Id);

        bulder.HasIndex(x => x.Id)
            .IsUnique();
        // заполняем таблицу должностей
        foreach (var rate in SeedData.Positions)
        {
            modelBuilder.Entity<Position>().HasData(rate);
        }

    }

    static void ConfigureModelBuilderForUser(ModelBuilder modelBuilder)
    {
        var bulder = modelBuilder.Entity<User>();

        bulder.ToTable("users");
        bulder.HasKey(x => x.Id);

        bulder.HasIndex(x => x.Id)
            .IsUnique();




        bulder.Property(user => user.FirstName)
            .IsRequired()
            .HasMaxLength(60);
        bulder.Property(user => user.LastName)
            .HasMaxLength(60);
        bulder.Property(user => user.MiddleName)
            .HasMaxLength(60);
        modelBuilder.Entity<User>()
            .HasKey(u => new { u.FirstName, u.LastName, u.MiddleName })
            .HasName("IDX_FIO_KEY");

        // заполняем таблицу пользователей
        foreach (var rate in SeedData.Users)
        {
            modelBuilder.Entity<User>().HasData(rate);
        }
    }


}
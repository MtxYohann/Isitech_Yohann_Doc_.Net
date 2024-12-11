using mvc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace mvc.Data;

// Cette classe est une classe de contexte de base de donne
public class ApplicationDbContext : IdentityDbContext<Teacher>
{
    // Nous allons creer un dbset pour chaque table
    //Dbset est une classe qui représente une table
    //ELle permet de faire le mapping entre la table et la classe C#

    public DbSet<Student> Students { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<IdentityRole>(e => e.Property(m => m.Id).HasMaxLength(100));
        builder.Entity<Teacher>(e => e.Property(m => m.Id).HasMaxLength(100));
        builder.Entity<IdentityUserLogin<string>>(ul =>
        {
            ul.Property(u => u.UserId).HasMaxLength(100);
            ul.Property(u => u.LoginProvider).HasMaxLength(100);
            ul.Property(u => u.ProviderKey).HasMaxLength(100);
        });
        builder.Entity<IdentityUserToken<string>>(ul =>
        {
            ul.Property(u => u.UserId).HasMaxLength(50);
            ul.Property(u => u.LoginProvider).HasMaxLength(100);
            ul.Property(u => u.Name).HasMaxLength(100);
            //ul.Property(u => u.Value).HasMaxLength(100);
        });

        // builder.Entity<IdentityUserRole<string>>(entity =>
        //     {
        //         entity.HasNoKey();
        //         entity.HasData(UserRoles);
        //     });
    }

}
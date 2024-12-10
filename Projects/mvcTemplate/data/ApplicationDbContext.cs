using mvc.Models;
using Microsft.EntityFrameworkCore;

namespace mvc.Data;

// Cette classe est une classe de contexte de base de donne
public class ApplicationDbContext
{
    // Nous allons creer un dbset pour chaque table
    //Dbset est une classe qui représente une table
    //ELle permet de faire le mapping entre la table et la classe C#

    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)

}
using mvc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace mvc.Data;

// Cette classe est une classe de contexte de base de donne
public class ApplicationDbContext : IdentityDbContext<User>
{
    // Nous allons creer un dbset pour chaque table
    //Dbset est une classe qui représente une table
    //ELle permet de faire le mapping entre la table et la classe C#

    public DbSet<Event> Events { get; set; }


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

}
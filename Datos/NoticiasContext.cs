using Microsoft.EntityFrameworkCore;
using Noticias_Proyecto_1_Final.Modelos;
namespace Noticias_Proyecto_1_Final.Datos
{
    //modelos que se crearan como tablas en la bd
    public class NoticiasContext :DbContext
    {
        public NoticiasContext(DbContextOptions<NoticiasContext> options ):base(options)
        {
            
        }
        public DbSet<Noticias> noticias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Noticias>().HasData(
                new Noticias()

                {
                    IdNoticia = 1,
                    Titulo = " Muere joven de 18",
                    Pais = "Republica Dominicana",
                    Fecha = DateTime.Now,
                    Categoria = "Farandula",
                    Fuente = "sabra Dios",
                    Contenido = "Se murio joven",
                    Autor = "Anthony Sanchez",
                    Enlace = "Idk"
                });
        }
    }
}

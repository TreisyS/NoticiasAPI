using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Noticias_Proyecto_1_Final.Modelos
{
    public class Noticias
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdNoticia { get; set; }


        [Required]
        [MaxLength(40)]
        public string Titulo { get; set; }

        public DateTime Fecha { get; set; }
        [Required]
        public string Pais { get; set; } 
        [Required]
        public string Categoria { get; set; }
        public string Fuente { get; set; }
        [Required] 
        public string Contenido { get; set; }

        public string Enlace { get; set; }

        public string Autor { get; set; }

    }
}

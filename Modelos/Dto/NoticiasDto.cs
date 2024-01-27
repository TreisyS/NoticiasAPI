using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Noticias_Proyecto_1_Final.Modelos.Dto
{

    //Los datos que expondre
    public class NoticiasDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdNoticia { get; set; }
        [Required]
        [MaxLength(90)]
        public string Titulo { get; set; }
        [Required]
        public string Pais { get; set; }
        [Required]
        public string Categoria { get; set; }

        public DateTime Fecha { get; set; }

        public string Fuente { get; set; }
        [Required]
        public string Contenido { get; set; }

        public string Enlace { get; set; }

        public string Autor { get; set; }


    }
}

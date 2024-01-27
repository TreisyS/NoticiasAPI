using Noticias_Proyecto_1_Final.Modelos.Dto;

namespace Noticias_Proyecto_1_Final.Datos
{
    //datos proveedor
    public static class NoticiasStore
    {
        public static List<NoticiasDto> noticiasList = new List<NoticiasDto>
        {
                
                //Datos ficticios pero despues vendran de la bd
                new NoticiasDto{IdNoticia=1, Titulo = "Virus nuevo en China", Contenido = " INFORMACION SOBRE LA NOTICIA", Enlace = "https://www.france24.com/es/programas/así-es-asia/20231201-china-aplaca-temores-causados-por-su-reciente-aumento-de-enfermedades-respiratorias", Fuente = "De los tesoros"},

                 new NoticiasDto{IdNoticia=2,  Titulo = "No hay agua en africa", Contenido = " INFORMACION SOBRE LA NOTICIA", Enlace = "https://www.france24.com/es/programas/así-es-asia/20231201-china-aplaca-temores-causados-por-su-reciente-aumento-de-enfermedades-respiratorias" , Fuente = "De los tesoros"},


        };

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Noticias_Proyecto_1_Final.Datos;
using Noticias_Proyecto_1_Final.Modelos;
using Noticias_Proyecto_1_Final.Modelos.Dto;

namespace Noticias_Proyecto_1_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoticiasController : ControllerBase
    {
        private readonly ILogger<NoticiasController> _logger;
        private readonly NoticiasContext _db;

        //servicio loggger
        public NoticiasController(ILogger<NoticiasController> logger, NoticiasContext db)
        {
            _logger = logger;
            _db = db;
        }


        //endpoint
        //metodo get
        //retornamos una lista


        

        [HttpGet("{idNoticias:int}", Name = "GetNoticia")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<NoticiasDto> GetNoticia(int idNoticias)
        {
            _logger.LogError("Error al traer noticias con el id: " + idNoticias);
            if (idNoticias == 0)
            {
                return BadRequest();
            }

            //  var noticia = NoticiasStore.noticiasList.FirstOrDefault(v => v.IdNoticia == idNoticias);
           
            var noticia = _db.noticias.FirstOrDefault(v => v.IdNoticia == idNoticias);
           
            if (noticia == null)
            {
                return NotFound();
            }

            return Ok(noticia);
        }



        //Crear Recurso
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<NoticiasDto> CreateNoticias([FromBody] NoticiasCreateDto noticiasdto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //evitar que no se entren dublicados

            if (_db.noticias.FirstOrDefault(v => v.Titulo.ToLower() == noticiasdto.Titulo.ToLower()) != null)
            {

                ModelState.AddModelError("TituloExiste", "La noticia con ese titulo ya existe");
                return BadRequest(ModelState);


            }
            //validacion
            if (noticiasdto == null)
            {
                return BadRequest(noticiasdto);
            }
        
            //agregar nuevo registro a nuestra lista

            Noticias modelo = new()
            {
                
                Titulo = noticiasdto.Titulo,
                Pais = noticiasdto.Pais,
                Categoria = noticiasdto.Categoria,
                Fecha = noticiasdto.Fecha,
                Fuente = noticiasdto.Fuente,
                Contenido = noticiasdto.Contenido,
                Enlace = noticiasdto.Enlace,
                Autor = noticiasdto.Autor
                

            };
            //insert
            _db.noticias.Add(modelo);
            //reflejar los datos en la bd
            _db.SaveChanges();


            return CreatedAtRoute("GetNoticia", new { IdNoticias = modelo.IdNoticia }, modelo);


        }


        //delete
        [HttpDelete("{idNoticias:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteNoticia(int idNoticias)
        {
            if (idNoticias <= 0)
            {
                return BadRequest();
            }

            var noticia = _db.noticias.FirstOrDefault(v => v.IdNoticia == idNoticias);
            if (noticia == null)
            {
                return NotFound();
            }

            _db.noticias.Remove(noticia);
            _db.SaveChanges();

            return NoContent();
        }



        //ACTUALIZAR
        [HttpPut("{idNoticias:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateNoticias(int idNoticias, [FromBody] NoticiasUpdateDto noticiasdto)
        {
            if (noticiasdto == null || idNoticias != noticiasdto.IdNoticia)
            {
                return BadRequest();
            }

            // Buscar la entidad existente en la base de datos
            var noticia = _db.noticias.FirstOrDefault(v => v.IdNoticia == idNoticias);

            if (noticia == null)
            {
                return NotFound();
            }

            // Actualizar las propiedades de la entidad existente
            noticia.Titulo = noticiasdto.Titulo;
            noticia.Pais = noticiasdto.Pais;
            noticia.Categoria = noticiasdto.Categoria;
            
            noticia.Fuente = noticiasdto.Fuente;
            noticia.Contenido = noticiasdto.Contenido;
            noticia.Enlace = noticiasdto.Enlace;
            noticia.Autor = noticiasdto.Autor;

            // Guardar cambios en la base de datos
            _db.SaveChanges();

            return NoContent();
        }

        //Actualiza solo una propiedad

        [HttpPatch("{idNoticias:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartNoticias(int idNoticias, JsonPatchDocument<NoticiasUpdateDto> patchDto)
        {
            if (patchDto == null || idNoticias == 0)
            {
                return BadRequest();
            }

            var noticia = _db.noticias.FirstOrDefault(v => v.IdNoticia == idNoticias);

            if (noticia == null)
            {
                return NotFound();
            }

            // Mapear la entidad Noticias a un DTO
            var noticiasdto = new NoticiasUpdateDto
            {
                IdNoticia = noticia.IdNoticia,
                Titulo = noticia.Titulo,
                Pais = noticia.Pais,
                Categoria = noticia.Categoria,
                Fecha = noticia.Fecha,
                Fuente = noticia.Fuente,
                Contenido = noticia.Contenido,
                Enlace = noticia.Enlace,
                Autor = noticia.Autor
            };

            // Aplicar los cambios del patch al DTO
            patchDto.ApplyTo(noticiasdto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapear el DTO actualizado de nuevo a la entidad Noticias
            noticia.Titulo = noticiasdto.Titulo;
            noticia.Pais = noticiasdto.Pais;
            noticia.Categoria = noticiasdto.Categoria;
            noticia.Fecha = noticiasdto.Fecha;
            noticia.Fuente = noticiasdto.Fuente;
            noticia.Contenido = noticiasdto.Contenido;
            noticia.Enlace = noticiasdto.Enlace;
            noticia.Autor = noticiasdto.Autor;

            // Guardar cambios en la base de datos
            _db.SaveChanges();

            return NoContent();
        }

        //filtro
        //endpoint para que nos devuelva una sola noticia.

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<NoticiasDto>> GetNoticias(int page = 1, int pageSize = 10)
        {
            var noticias = _db.noticias
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(n => new NoticiasDto
                {
                    IdNoticia = n.IdNoticia,
                    Titulo = n.Titulo,
                    Pais = n.Pais,
                    Categoria = n.Categoria,
                    Fecha = n.Fecha,
                    Fuente = n.Fuente,
                    Contenido = n.Contenido,
                    Enlace = n.Enlace,
                    Autor = n.Autor
                })
                .ToList();

            if (noticias.Count == 0)
            {
                return NotFound();
            }

            return Ok(noticias);
        }

        [HttpGet("ByCountry/{country}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<NoticiasDto>> GetNoticiasByCountry(string country, int page = 1, int pageSize = 10)
        {
            var noticias = _db.noticias
                .Where(n => n.Pais == country)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(n => new NoticiasDto
                {
                    IdNoticia = n.IdNoticia,
                    Titulo = n.Titulo,
                    Pais = n.Pais,
                    Categoria = n.Categoria,
                    Fecha = n.Fecha,
                    Fuente = n.Fuente,
                    Contenido = n.Contenido,
                    Enlace = n.Enlace,
                    Autor = n.Autor
                })
                .ToList();

            if (noticias.Count == 0)
            {
                return NotFound();
            }

            return Ok(noticias);
        }
        [HttpGet("ByCategory/{category}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<NoticiasDto>> GetNoticiasByCategory(string category, int page = 1, int pageSize = 10)
        {
            var noticias = _db.noticias
                .Where(n => n.Categoria == category)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(n => new NoticiasDto
                {
                    IdNoticia = n.IdNoticia,
                    Titulo = n.Titulo,
                    Pais = n.Pais,
                    Categoria = n.Categoria,
                    Fecha = n.Fecha,
                    Fuente = n.Fuente,
                    Contenido = n.Contenido,
                    Enlace = n.Enlace,
                    Autor = n.Autor
                })
                .ToList();

            if (noticias.Count == 0)
            {
                return NotFound();
            }

            return Ok(noticias);
        }

        [HttpGet("BySearch/{term}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<NoticiasDto>> GetNoticiasBySearch(string term, int page = 1, int pageSize = 10)
        {
            var noticias = _db.noticias
                .Where(n => n.Titulo.Contains(term) || n.Contenido.Contains(term))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(n => new NoticiasDto
                {
                    IdNoticia = n.IdNoticia,
                    Titulo = n.Titulo,
                    Pais = n.Pais,
                    Categoria = n.Categoria,
                    Fecha = n.Fecha,
                    Fuente = n.Fuente,
                    Contenido = n.Contenido,
                    Enlace = n.Enlace,
                    Autor = n.Autor
                })
                .ToList();

            if (noticias.Count == 0)
            {
                return NotFound();
            }

            return Ok(noticias);
        }





    }

}
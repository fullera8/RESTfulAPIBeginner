using AutoMapper;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Helpers;
using CourseLibrary.API.Models;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/authorcollections")]
    public class AuthorCollectionsController : ControllerBase
    {
        private readonly ICourseLibraryRepository courseLibraryRepository;
        private readonly IMapper mapper;

        public AuthorCollectionsController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            this.courseLibraryRepository = courseLibraryRepository ??
                throw new ArgumentNullException(nameof(courseLibraryRepository));
            this.mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        //Create author collection for multiple author return
        /// <summary>
        /// Convert a comma delimited string of authors into a typed collection and vaLidate all authors provided exist
        /// </summary>
        /// <param name="ids">Comma delimited list of authors</param>
        /// <returns>Typed collection of Authors</returns>
        [HttpGet("{ids}", Name = "GetAuthorCollection")]
        public IActionResult GetAuthorCollection( 
            [FromRoute]
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids) 
        {
            //validate not null
            if (ids == null)
                return BadRequest();

            //Get corresponding authors
            var authorsEntities = this.courseLibraryRepository.GetAuthors(ids);

            //validate all authors exist
            if (ids.Count() != authorsEntities.Count())
                return NotFound();

            //return
            var authorResult = this.mapper.Map<IEnumerable<AuthorDto>>(authorsEntities);
            return Ok(authorResult);
        }

        /// <summary>
        /// Creates multiple authors based on collection passed in from the body
        /// </summary>
        /// <param name="authorCollection">Author collection provided by the user</param>
        /// <returns>List of new authors</returns>
        [HttpPost]
        public ActionResult<IEnumerable<AuthorDto>> CreateAuthorCollection(
            [FromBody] IEnumerable<AuthorsForCreationDto> authorCollection) 
        {
            //map
            var authorEntities = this.mapper.Map<IEnumerable<Author>>(authorCollection);

            //iterate
            foreach (var author in authorEntities)
            {
                this.courseLibraryRepository.AddAuthor(author);
            }

            //save new authors
            this.courseLibraryRepository.Save();

            //Get list of author ids
            var authorReturnCollection = this.mapper.Map<IEnumerable<AuthorDto>>(authorEntities);
            var idsAsString = string.Join(",", authorReturnCollection.Select(a => a.Id));

            //return
            return CreatedAtRoute(
                "GetAuthorCollection",
                new { ids = idsAsString },
                authorReturnCollection);
        }
    }
}

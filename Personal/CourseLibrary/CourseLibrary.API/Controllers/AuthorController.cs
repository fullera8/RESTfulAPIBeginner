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
    [Route("api/authors")]//specifies the template for this whole page
    public class AuthorController : ControllerBase
    {
        private readonly ICourseLibraryRepository courseLibraryRepository;

        public AuthorController(ICourseLibraryRepository courseLibraryRepository)
        {
            this.courseLibraryRepository = courseLibraryRepository ??
                throw new ArgumentNullException(nameof(courseLibraryRepository));
        }

        /// <summary>
        /// Get all authors
        /// </summary>
        /// <returns>Complete author list in Accept header format</returns>
        [HttpGet()]
        public IActionResult GetAutors()
        {
            var authorsFromRepo = this.courseLibraryRepository.GetAuthors();
            return Ok(authorsFromRepo);
        }

        /// <summary>
        /// Get single auto
        /// </summary>
        /// <param name="authorId">Author Guid ID</param>
        /// <returns>Author in Accept header format</returns>
        [HttpGet("{authorId:guid}")]//{ is the dynamic tag, name in the tag is var name, : is var strong typing
        public IActionResult GetAuthor(Guid authorId)//call var name in the tag, must be exact match
        {
            //Check for author, bad because API can get interrupted and cause bad return
            //if (!this.courseLibraryRepository.AuthorExists(authorId))
            //    return NotFound();

            //Get author
            var authorFromRepo = this.courseLibraryRepository.GetAuthor(authorId);

            //author exists validation
            if (authorFromRepo == null)
                return NotFound();

            //return author
            return Ok(authorFromRepo);

        }
    }
}

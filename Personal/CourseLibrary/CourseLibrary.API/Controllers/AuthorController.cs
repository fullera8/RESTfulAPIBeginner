using AutoMapper;
using CourseLibrary.API.Helpers;
using CourseLibrary.API.Models;
using CourseLibrary.API.ResourceParameters;
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
        private readonly IMapper mapper;

        public AuthorController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            this.courseLibraryRepository = courseLibraryRepository ??
                throw new ArgumentNullException(nameof(courseLibraryRepository));
            this.mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get all authors for a category
        /// </summary>
        /// <param name="mainCategory">Category filter</param>
        /// <param name="searchQuery">Generic search criteria</param>
        /// <returns>Complete author list in Accept header format</returns>
        [HttpGet()]
        [HttpHead]
        public ActionResult<IEnumerable<AuthorDto>> GetAutors(
            [FromQuery] AuthorsResourceParameters authorsResourceParameters //author params
        )
        {
            //Testing error return
            //throw new Exception("Test Exception");

            var authorsFromRepo = this.courseLibraryRepository.GetAuthors(authorsResourceParameters);


            //No need to do it this way, mapped in the AuthorProfiles and automapper handle it
            //var authors = new List<AuthorDto>();
            //foreach (var author in authorsFromRepo)
            //{
            //    authors.Add(new AuthorDto() { 
            //        Id = author.Id,
            //        Name = $"{author.FirstName} {author.LastName}",
            //        MainCategory = author.MainCategory,
            //        Age = author.DateOfBirth.GetCurrentAge()
            //    });
            //}

            return Ok(this.mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo));
        }

        /// <summary>
        /// Get single author. Name value used for reference to this call in other API calls
        /// </summary>
        /// <param name="authorId">Author Guid ID</param>
        /// <returns>Author in Accept header format</returns>
        [HttpGet("{authorId:guid}", Name = "GetAuthor")]//{ is the dynamic tag, name in the tag is var name, : is var strong typing
        public ActionResult<AuthorDto> GetAuthor(Guid authorId)//call var name in the tag, must be exact match
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
            return Ok(this.mapper.Map<AuthorDto>(authorFromRepo));

        }

        [HttpPost]
        public ActionResult<AuthorDto> CreateAuthor([FromBody] AuthorsForCreationDto author)
        {
            //map author
            var authorEntity = this.mapper.Map<Entities.Author>(author);

            //create author, DB is black box logic
            this.courseLibraryRepository.AddAuthor(authorEntity);
            this.courseLibraryRepository.Save();

            //return new author detail
            var authorReturn = this.mapper.Map<AuthorDto>(authorEntity);
            return CreatedAtRoute(
                "GetAuthor",
                new { authorId = authorReturn.Id },
                authorReturn
                );
        }

        /// <summary>
        /// Allows user to view what response headers are allowed in the allow header return
        /// </summary>
        /// <returns>List of allowed response headers</returns>
        [HttpOptions]
        public IActionResult GetAuthorsOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST");
            return Ok();
        }
    }
}

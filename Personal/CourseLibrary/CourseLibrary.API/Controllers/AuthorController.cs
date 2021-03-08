﻿using AutoMapper;
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
        /// Get all authors
        /// </summary>
        /// <returns>Complete author list in Accept header format</returns>
        [HttpGet()]
        [HttpHead]
        public ActionResult<IEnumerable<AuthorDto>> GetAutors()
        {
            //Testing error return
            //throw new Exception("Test Exception");
            
            var authorsFromRepo = this.courseLibraryRepository.GetAuthors();


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
        /// Get single auto
        /// </summary>
        /// <param name="authorId">Author Guid ID</param>
        /// <returns>Author in Accept header format</returns>
        [HttpGet("{authorId:guid}")]//{ is the dynamic tag, name in the tag is var name, : is var strong typing
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
            return Ok(this.mapper.Map<IEnumerable<AuthorDto>>(authorFromRepo));

        }
    }
}

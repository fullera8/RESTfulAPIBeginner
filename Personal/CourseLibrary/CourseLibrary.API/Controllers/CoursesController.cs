using AutoMapper;
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
    [Route("api/authors/{authorID:guid}/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseLibraryRepository courseLibraryRepository;
        private readonly IMapper mapper;

        public CoursesController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            this.courseLibraryRepository = courseLibraryRepository ??
                throw new ArgumentNullException(nameof(courseLibraryRepository)); ;
            this.mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper)); ;
        }

        /// <summary>
        /// Get all courses for a given author.
        /// </summary>
        /// <param name="authorId">Author guid</param>
        /// <returns>Courses list with details</returns>
        [HttpGet]
        public ActionResult<IEnumerable<CourseDto>> GetCoursesForAuthor(Guid authorId) { //authorId defined at class level
            if (!this.courseLibraryRepository.AuthorExists(authorId))
                return NotFound();

            var coursesForAuthorFromRepo = this.courseLibraryRepository.GetCourses(authorId);
            return Ok(this.mapper.Map<IEnumerable<CourseDto>>(coursesForAuthorFromRepo));
        }

        /// <summary>
        /// Get course for a specific author
        /// </summary>
        /// <param name="authorId">Author ID Guid</param>
        /// <param name="courseId">Course ID Guid</param>
        /// <returns>Course details</returns>
        [HttpGet("{courseId:guid}")]
        public ActionResult<CourseDto> GetCourseForAuthor(Guid authorId, Guid courseId)
        {
            //Course validation
            //check for author
            if (!this.courseLibraryRepository.AuthorExists(authorId))
                return NotFound();
            //check for course (no library method)
            var courseForAuthorFromRepo = this.courseLibraryRepository.GetCourse(authorId, courseId);
            if (courseForAuthorFromRepo == null)
                return NotFound();

            //Return course details
            return Ok(this.mapper.Map<CourseDto>(courseForAuthorFromRepo));
        }
    }
}

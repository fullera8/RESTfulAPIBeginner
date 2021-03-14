using AutoMapper;
using CourseLibrary.API.Models;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.JsonPatch;
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
        [HttpGet("{courseId:guid}", Name = "GetCourseForAuthor")]
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

        
        [HttpPost]
        public ActionResult<CourseDto> CreateCourseForAuthor( 
            Guid authorId, 
            CoursesForCreationDto course)
        {
            //author validation required
            if (!this.courseLibraryRepository.AuthorExists(authorId))
                return NotFound();

            //map to entities
            var courseEntity = this.mapper.Map<Entities.Course>(course);
            this.courseLibraryRepository.AddCourse(authorId, courseEntity);

            //Add Course
            this.courseLibraryRepository.AddCourse(authorId, courseEntity);
            this.courseLibraryRepository.Save();

            //Return saved output
            var courseReturn = this.mapper.Map<CourseDto>(courseEntity);
            return CreatedAtRoute(
                "GetCourseForAuthor",
                new { authorId = courseReturn.AuthorId, courseId = courseReturn.Id },
                courseReturn
                );
        }

        /// <summary>
        /// Updates an author's course
        /// </summary>
        /// <param name="authorId">Author GUID</param>
        /// <param name="courseId">Course GUID</param>
        /// <returns>Returns no content.</returns>
        [HttpPut("{courseId}")]
        public IActionResult UpdateCourseForAuthor(Guid authorId, Guid courseId, CourseForUpdateDto courseForUpdateDto)
        {
            //user input validation
            if (!this.courseLibraryRepository.AuthorExists(authorId))
                return NotFound();
            var courseForAuthorEntity = this.courseLibraryRepository.GetCourse(authorId, courseId);

            //If course does not exist, create it
            if (courseForAuthorEntity == null)
            {
               //map to entity and set the ID
                var courseAdd = this.mapper.Map<Entities.Course>(courseForUpdateDto);
                courseAdd.Id = courseId;

                //Add course
                this.courseLibraryRepository.AddCourse(authorId, courseAdd);
                this.courseLibraryRepository.Save();

                //Return saved output
                var courseReturn = this.mapper.Map<CourseDto>(courseAdd);
                return CreatedAtRoute(
                    "GetCourseForAuthor",
                    new { authorId, courseId = courseReturn.Id }, //notice because of the same authorId name and order, no mapping required
                    courseReturn
                );
            }

            //map to the entity, this is another way to update the existing var without creating a new var
            this.mapper.Map(courseForUpdateDto, courseForAuthorEntity);

            //apply the update
            this.courseLibraryRepository.UpdateCourse(courseForAuthorEntity);
            this.courseLibraryRepository.Save();

            //return no content, standard for an httpput
            return NoContent();
        }

        [HttpPatch("{courseId}")]
        public ActionResult PartiallyUpdateCourseForAuthor(
            Guid authorId, 
            Guid courseId, 
            JsonPatchDocument<CourseForUpdateDto> patchDocument)
        {
            //Validate course for author exists
            if (!this.courseLibraryRepository.AuthorExists(authorId))
                return NotFound();
            var courseForAuthorEntity = this.courseLibraryRepository.GetCourse(authorId, courseId);
            if(courseForAuthorEntity == null)
                return NotFound();

            // Map the requested resource to an update entity. 
            // Requires a map from entity to dto for the patch applyto method
            var courseToPatch = this.mapper.Map<CourseForUpdateDto>(courseForAuthorEntity);
            // TO DO: Add patch validation
            patchDocument.ApplyTo(courseToPatch);

            // Map the update back to entity
            this.mapper.Map(courseToPatch, courseForAuthorEntity);

            // Update and save the course
            this.courseLibraryRepository.UpdateCourse(courseForAuthorEntity);
            this.courseLibraryRepository.Save();

            //Return no content because update
            return NoContent();
        }
    }
}

using CourseLibrary.API.Entities;
using CourseLibrary.API.ResourceParameters;
using System;
using System.Collections.Generic;

namespace CourseLibrary.API.Services
{
    public interface ICourseLibraryRepository
    {    
        IEnumerable<Course> GetCourses(Guid authorId);
        Course GetCourse(Guid authorId, Guid courseId);
        void AddCourse(Guid authorId, Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(Course course);
        /// <summary>
        /// Get all authors in the database
        /// </summary>
        /// <returns>A list of all authors</returns>
        IEnumerable<Author> GetAuthors();
        /// <summary>
        /// Get a list of authors for a given category filter and/or search based on query criteria
        /// </summary>
        /// <param name="authorsResourceParameters">Variable list of author parameters</param>
        /// <returns>A list of authors in a given category</returns>
        IEnumerable<Author> GetAuthors(AuthorsResourceParameters authorsResourceParameters);
        /// <summary>
        /// Find all authors given a list of potential author GUIDS
        /// </summary>
        /// <param name="authorIds">List of author GUIDS</param>
        /// <returns>Sorted list of authors by first and last name</returns>
        IEnumerable<Author> GetAuthors(IEnumerable<Guid> authorIds);
        Author GetAuthor(Guid authorId);
        void AddAuthor(Author author);
        void DeleteAuthor(Author author);
        void UpdateAuthor(Author author);
        bool AuthorExists(Guid authorId);
        bool Save();
    }
}

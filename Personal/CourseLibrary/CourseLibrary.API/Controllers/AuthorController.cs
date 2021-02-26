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
    public class AuthorController : ControllerBase
    {
        //private readonly ICourseLibraryRepository courseLibraryRepository;

        //public AuthorController(ICourseLibraryRepository courseLibraryRepository)
        //{
        //    this.courseLibraryRepository = courseLibraryRepository ??
        //        throw new ArgumentNullException(nameof(courseLibraryRepository));
        //}
        //public IActionResult GetAutors()
        //{
        //    var authorsFromRepo = this.courseLibraryRepository.GetAuthors();
        //    return new JsonResult(authorsFromRepo);
        //}
    }
}

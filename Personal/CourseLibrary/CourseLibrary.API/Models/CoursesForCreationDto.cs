using CourseLibrary.API.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseLibrary.API.Models
{
    public class CoursesForCreationDto : CourseForManipulationDto
    {        
        //Happens in seperate class
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (Title == Description)
        //    {
        //        yield return new ValidationResult(
        //            "Description must be different from title.",
        //            new[] { "CoursesForCreationDto" });
        //    }
        //}
    }
}

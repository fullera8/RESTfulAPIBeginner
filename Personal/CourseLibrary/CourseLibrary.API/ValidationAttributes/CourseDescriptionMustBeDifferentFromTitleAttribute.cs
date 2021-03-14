using CourseLibrary.API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseLibrary.API.ValidationAttributes
{
    //Custom validation rules that can be applied with the same tags as regular validation so they are 
    //checked simultaniously and appear with default validation exceptions if multiple validations fail.
    public class CourseDescriptionMustBeDifferentFromTitleAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var course = (CourseForManipulationDto)validationContext.ObjectInstance;

            if (course.Title == course.Description)
            {
                return new ValidationResult(
                    ErrorMessage,
                    new[] { nameof(CourseForManipulationDto) });
            }
            
            return ValidationResult.Success;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseLibrary.API.Models
{
    public class CoursesForCreationDto : IValidatableObject
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(1500)]
        public string Description { get; set; }

        /// <summary>
        /// Confirm business rule title does not match course
        /// </summary>
        /// <param name="validationContext">Provides access to course info we are working on</param>
        /// <returns>Business rule validation</returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Title == Description)
            {
                yield return new ValidationResult(
                    "Description must be different from title.",
                    new[] { "CoursesForCreationDto" });
            }
        }
    }
}

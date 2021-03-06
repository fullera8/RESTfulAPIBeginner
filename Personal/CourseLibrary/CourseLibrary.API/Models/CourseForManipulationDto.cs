﻿using CourseLibrary.API.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseLibrary.API.Models
{
    [CourseDescriptionMustBeDifferentFromTitleAttribute(ErrorMessage = "Course title cannot match description.")]
    public abstract class CourseForManipulationDto
    {
        [Required(ErrorMessage = "Course title is required.")]
        [MaxLength(100, ErrorMessage = "Course title cannot be greater than 100 chars.")]
        public string Title { get; set; }

        [MaxLength(1500, ErrorMessage = "Course description cannot be greater than 1,500 chars.")]
        public virtual string Description { get; set; }
    }
}

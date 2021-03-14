﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseLibrary.API.Models
{
    public class CourseForUpdateDto : CourseForManipulationDto
    {
        [Required(ErrorMessage = "Course description is required.")]
        public override string Description { get => base.Description; set => base.Description = value; }
    }
}

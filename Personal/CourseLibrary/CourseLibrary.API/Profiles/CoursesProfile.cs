using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseLibrary.API.Profiles
{
    public class CoursesProfile : Profile
    {
        public CoursesProfile()
        {
            //No special mapping required, this is enough of a base statement
            CreateMap<Entities.Course, Models.CourseDto>();
        }
    }
}

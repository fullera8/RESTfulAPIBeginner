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

            //Create map for post(create) method
            CreateMap<Models.CoursesForCreationDto, Entities.Course>();

            //create map for the put(update) method
            CreateMap<Models.CourseForUpdateDto, Entities.Course>();

            //create map for the patch(update) method
            CreateMap<Entities.Course, Models.CourseForUpdateDto>();
        }
    }
}

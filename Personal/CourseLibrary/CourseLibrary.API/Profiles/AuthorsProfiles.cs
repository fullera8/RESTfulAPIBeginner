using AutoMapper;
using CourseLibrary.API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseLibrary.API.Controllers.Profiles
{
    public class AuthorsProfiles : Profile
    {
        //Acts as a mapping from the Author entity to the dto model
        public AuthorsProfiles()
        {
            /*  Will map same names or null out names not available in both.
                To map the entities to the model that are not 1 to 1 or have different names
                put the model property and where to map from. Additional logic is added in MapFrom if
                properties need to be flattened.
            */
            CreateMap<Entities.Author, Models.AuthorDto>()
                .ForMember(
                    dest => dest.Name, //Model destination
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}") //how to map from source (flatten)
                )
                .ForMember(
                    dest => dest.Age,
                    opt => opt.MapFrom(src => src.DateOfBirth.GetCurrentAge())
                );

        }
    }
}

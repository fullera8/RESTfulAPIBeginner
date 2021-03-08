using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseLibrary.API.ResourceParameters
{
    public class AuthorsResourceParameters
    {
        /// <summary>
        /// Category filter
        /// </summary>
        public string MainCategory { get; set; }
        /// <summary>
        /// Generic search criteria
        /// </summary>
        public string SearchQuery { get; set; }
    }
}

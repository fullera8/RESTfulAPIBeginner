using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseLibrary.API.Helpers
{
    //extend other classes with the static class (used in )
    public static class DateTimeOffsetExtensions
    {
        /// <summary>
        /// Get current age based on birth date (year only)
        /// </summary>
        /// <param name="dateTimeOffset">Birth date</param>
        /// <returns>Age in years</returns>
        public static int GetCurrentAge(this DateTimeOffset dateTimeOffset)
        {
            var currentDate = DateTime.UtcNow;
            int age = currentDate.Year - dateTimeOffset.Year;

            if (currentDate < dateTimeOffset.AddYears(age))
                age--;

            return age;
        }
    }
}

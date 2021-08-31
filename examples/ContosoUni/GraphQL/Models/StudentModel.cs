using System;
using System.Collections.Generic;
using HotChocolate.Data;

namespace ContosoUniversity.Models
{
    public class StudentModel
    {
        [UseFiltering]
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        [UseProjection]
        public virtual ICollection<EnrollmentModel> Enrollments { get; set; }
    }
}
namespace ContosoUniversity.Models
{
    public class EnrollmentModel
    {
        public int EnrollmentIdentifier { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }

        //public Grade? Grade { get; set; }
        // public virtual Course Course { get; set; }
        public virtual StudentModel Student { get; set; }
    }
}
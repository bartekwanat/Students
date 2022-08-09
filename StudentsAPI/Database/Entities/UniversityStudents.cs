namespace StudentsAPI.Database.Entities
{
    public class UniversityStudents
    {
        public Guid StudentId { get; set; }
        public Student Student { get; set; }

        public Guid UniversityId { get; set; }
        public University University { get; set; }
    }
}

using System.Runtime.Serialization;

namespace StudentsAPI.Database.Entities
{
    public class Student
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }

        [IgnoreDataMember]
        public Guid UniversityId { get; set; }
        public virtual University University { get; set; }
    }
}

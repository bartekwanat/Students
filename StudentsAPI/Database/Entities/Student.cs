using System.Runtime.Serialization;

namespace StudentsAPI.Database.Entities
{
    public class Student : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }

        [IgnoreDataMember]
        public Guid UniversityId { get; set; }
        public virtual University University { get; set; }
    }
}

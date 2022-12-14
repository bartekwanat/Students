using System.Runtime.Serialization;

namespace StudentsAPI.Database.Entities
{
    public class University : BaseEntity
    {
        public string UniversityName { get; set; }

        [IgnoreDataMember]
        public IEnumerable<Student> Students { get; set; }
    }
}

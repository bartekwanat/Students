using System.Runtime.Serialization;

namespace StudentsAPI.Database.Entities
{
    public class University
    {
        public Guid Id { get; set; }
        public string UniversityName { get; set; }

        [IgnoreDataMember]
        public List<Student> Students { get; set; }
    }
}

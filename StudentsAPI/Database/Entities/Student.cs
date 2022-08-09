﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace StudentsAPI.Database.Entities
{
    public class Student : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        [IgnoreDataMember]
        public List<UniversityStudents> UniversityStudents { get; set; }
        public ICollection<University> Universities { get; set; }

    }
}

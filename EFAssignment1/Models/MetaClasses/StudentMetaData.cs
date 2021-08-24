using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EFAssignment1.Models
{
    public class StudentMetaData
    {
        [StringLength(50)]
        public string StudentName { get; set; }
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> EnrollmentDate { get; set; }
    }
    [MetadataType(typeof(StudentMetaData))]
    public partial class Student
    {

    }
}
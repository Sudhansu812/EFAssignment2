using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EFAssignment1.Models
{
    public class CourseMetaData
    {
        [StringLength(50)]
        public string CourseTitle { get; set; }
        [Range(1,8)]
        public int CourseCredits { get; set; }
    }
    [MetadataType(typeof(CourseMetaData))]
    public partial class Course
    {

    }
}
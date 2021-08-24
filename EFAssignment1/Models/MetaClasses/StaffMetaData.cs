using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EFAssignment1.Models
{
    public class StaffMetaData
    {
        [StringLength(50)]
        public string StaffName { get; set; }
    }
    [MetadataType(typeof(StaffMetaData))]
    public partial class Staff
    {

    }
}
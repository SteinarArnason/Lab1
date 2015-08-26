using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vefthjonustur.Models
{
    /// <summary>
    ///  ID         = 1,
    ///   Name       = "Web services",
    ///   TemplateID = "T-514-VEFT",
    ///   StartDate  = DateTime.Now,
    ///  EndDate    = DateTime.Now.AddMonths(3)
    /// </summary>     
    public class Course
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public String TemplateID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Student> Students { get; set; }
    }
}

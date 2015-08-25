using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using System.Web.Http.Description;
using Vefthjonustur.Models;

namespace Lab1.Controllers
{
    [RoutePrefix("api/courses")]
    public class CoursesController : ApiController
    {
        private static List<Course> _courses;
        private static List<Student> _students;

        #region Constructor
        public CoursesController()
        {
            if (_courses == null)
            {
                _courses = new List<Course>
                {
                    new Course
                    {
                        ID         = 1,
                        Name       = "Web services",
                        TemplateID = "T-514-VEFT",
                        StartDate  = DateTime.Now,
                        EndDate    = DateTime.Now.AddMonths(3)
                    },
                    new Course
                    {
                        ID         = 2,
                        Name       = "Computer Networking",
                        TemplateID = "T-409-TSAM",
                        StartDate  = DateTime.Now,
                        EndDate    = DateTime.Now.AddMonths(3)
                    }
                };
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        
        [HttpGet]
        [Route("")]
        public List<Course> getCourses()
        {
            return _courses;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param>id = ID of course, name = Name of the course, templateID = TemplateID of the course</param>
        /// <returns>BadRequest if the input data is incorrect, else it returns you to the newly created Course</returns>
        [HttpPost]
        [ResponseType(typeof(Course))]
        public IHttpActionResult AddCourse(int id, String name, string templateID)
        {
            var course = new Course { ID = id, Name = name, TemplateID = templateID, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(3) };
            if (course == null)
            {
                return BadRequest();
            }
            var location = Url.Link("GetCourse", new { id = course.ID });
            return Created(location, course);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using System.Web.Http.Description;
using Vefthjonustur.Models;
using System.Diagnostics;

namespace Lab1.Controllers
{
 
	[RoutePrefix("api/courses")]
	public class CoursesController : ApiController
	{
		private static List<Course> _courses;
		private static List<Student> _students;
		private static int ID_counter;

		#region Constructor
		public CoursesController()
		{
			if (_courses == null)
			{
				ID_counter = 3;
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

		#region Get Courses
		/// <summary>
		/// Gets all the courses listed
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("")]
		public IHttpActionResult getCourses()
		{
			if(_courses == null)
			{
				return NotFound();
			}
			return Ok(_courses);
		}
		#endregion

		#region Get Course by ID
		/// <summary>
		///
		///  
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("getCourse/{id:int}", Name ="byId")]
		public IHttpActionResult getCourseById(int id)
		{
			Course ret = _courses.Find(i => i.ID == id);
			if(ret == null)
			{
				return NotFound();
			}
			else
			{
				return Ok(ret);
			}
		}
		#endregion

		#region Add Course
		/// <summary>
		/// Example : 
		///  {
		///        "Name":"MyCourse",
		///        "TemplateID":"T-testingCourse",
		///        "StartDate":"2015-08-17T13:10:20",
		///        "EndDate":"2015-12-15T12:13:14"
		///  }
		/// </summary>
		/// <param name = c></param>
		/// <returns>BadRequest if the input data is incorrect, else it returns you to the newly created Course</returns>
		[HttpPost]
		[Route("add")]
		public IHttpActionResult AddCourse(Course c)
		{
			c.ID = ID_counter;
			Debug.WriteLine(ID_counter);
			if(c.Name.Length < 1|| c.TemplateID.Length < 1)
			{
				Debug.WriteLine("Going to send bad request");
				return BadRequest();
			}
			Debug.WriteLine("Everything okay");
			ID_counter++;
			_courses.Add(c);
			string location = Url.Link("byId", new { id = c.ID });
			return Created(location,c);
		}
		#endregion
		
		#region Update Course
		/// <summary>
		/// Example data 
		/// {
		///"Name":"UpdateCourse",
		///"TemplateID":"T-xxx-UpdateCourse",
		///"StartDate":"2000-08-17T13:10:20",
		///"EndDate":"2000-12-15T12:13:14"
		///}
		/// </summary>
		/// <param name="id">Id of course that you want to update</param>
		/// <param name="c"> Course Json object</param>
		/// <returns>Updated course</returns>
		[HttpPut]
		[Route("update/{id:int}")]
		public IHttpActionResult updateCourse(int id, Course c)
		{
			Course ret = _courses.Find(i => i.ID == id);
			if(ret == null)
			{
				return NotFound();
			}
			else
			{
				ret.Name = c.Name;
				ret.TemplateID = c.TemplateID;
				ret.StartDate = c.StartDate;
				ret.EndDate = c.EndDate;
				ret.Students = c.Students;
				return Ok(ret);
			}
		}
		#endregion

		#region Delete Course
		/// <summary>
		/// Deletes the given Course ID
		/// </summary>
		/// <param name="id">ID of the course that we want to delete</param>
		/// <returns></returns>
		[HttpDelete]
		[Route("delete/{id:int}")]
		public IHttpActionResult deleteCourse(int id)
		{
			Course ret = _courses.Find(i => i.ID == id);
			if (ret == null)
			{
				return NotFound();
			}
			else
			{
				_courses.Remove(ret);
				//Eigum að skila 204
				return Ok();
			}
		}
		#endregion

		#region add student
		/// <summary>
		/// Adds a student to a Course
		/// </summary>
		/// <param name="cId">ID of the course we're going to add a student to</param>
		/// <param name="s">The student we're adding to the course</param>
		/// <returns>the student just added</returns>
		[HttpPost]
		[Route("addStudent/{cId:int}")]
		public IHttpActionResult addStudent(int cId, Student s)
		{
			Course ret = _courses.Find(i => i.ID == cId);
			if (ret == null)
			{
				return NotFound();
			}
			else
			{
				Student student = ret.Students.Find(i => i.SSN == s.SSN);
				if(student != null)
				{
					return BadRequest();
				}
				else
				{ 
					ret.Students.Add(s);
					return Ok(s);
				}
			}
		}
		#endregion

		#region Get Students In Course
		/// <summary>
		/// Gets all the student in the selected course
		/// </summary>
		/// <param name="ID">The course we want to get students from</param>
		/// <returns>A list of the students</returns>
		[HttpGet]
		[Route("getStudents/{ID:int}")]
		public IHttpActionResult getStudentsInCourse(int ID)
		{
			Course ret = _courses.Find(i => i.ID == ID);
			if (ret == null)
			{
				return NotFound();
			}
			else
			{
				return Ok(ret.Students);
			}
		}
		#endregion

	}
}
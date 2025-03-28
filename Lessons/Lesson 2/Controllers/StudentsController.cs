using Lesson_2.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lesson_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        // GET: api/<StudentsController>
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            Student student = new Student();
            return student.GetAll();
        }

        [HttpGet("Search")] // this was done using query string search?department=IT&minAge=20
        public IEnumerable<Student> GetWithFilter(string department, double minAge)
        {
            Student student = new Student();
            return student.GetByDepartmentAndAge(department, minAge);
        }

        // this is using Routing search2/department/IT/minAge/20
        [HttpGet("Search2/department/{department}/minAge/{minAge}")]
        public IEnumerable<Student> GetWithFilterRouting(string department, double minAge)
        {
            Student student = new Student();
            return student.GetByDepartmentAndAge(department, minAge);
        }

        // GET api/<StudentsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<StudentsController>
        [HttpPost]
        public int Post([FromBody] Student student)
        {
            return student.AddMe();
        }

        // PUT api/<StudentsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StudentsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

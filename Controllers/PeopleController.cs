using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        [HttpGet("all")]
        public List<People> GetPeople() => Repository.People;

        //[HttpGet("{id}/{some}")]
        [HttpGet("{id}")]
        public ActionResult<People> Get(int id)
        {
            var people = Repository.People.FirstOrDefault(p => p.Id == id);
            if(people == null)
            {
                return NotFound();
            }

            return Ok(people);
        }

        [HttpGet("search/{search}")]
        public List<People> Get(string search) =>
            Repository.People.Where(p => p.Name.ToUpper().Contains(search.ToUpper())).ToList();

        [HttpPost]
        public IActionResult Add(People people)
        {
            if (string.IsNullOrEmpty(people.Name))
            {
                return BadRequest();
            }

            Repository.People.Add(people);

            return NoContent();
        }
    }

    public class Repository
    {
        public static List<People> People = new List<People>
        {
            new People() {
                Id = 1,
                Name="Allan",
                Birthdate = new DateTime(1995,7,24)
            },
            new People() {
                Id = 2,
                Name="Allan Old",
                Birthdate = new DateTime(1990,7,24)
            },
            new People() {
                Id = 3,
                Name="Allan New",
                Birthdate = new DateTime(2000,7,24)
            }
        };
    }

    public class People
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime Birthdate { get; set; }

    }
}

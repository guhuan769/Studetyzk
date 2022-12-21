using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Model;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public Person GetPerson()
        {
            return new Person(1, "顾欢", 99);
        }
        /// <summary>
        /// https://localhost:7000/api/Test/Multi/5/6 这样传递的好处 更符合REST
        /// </summary>
        /// <param name="i1">5</param>
        /// <param name="i2">6</param>
        /// <returns></returns>
        [HttpGet("{i1}/{i2}")]
        public int Multi(int i1, int i2)
        {
            return i1 + i2;
        }

        [HttpGet("students/school/{schoolName}/class/{classNo}")]
        public Person GetStudent(string schoolName, [FromRoute(Name = "classNo")]int classNum)
        {
            return new Person(1, $"{schoolName}", 99);
        }
        [HttpPost]
        public Person SaveStudent(Person person)
        {
            return new Person(1, $"{person.Name}", 99);
        }

        [HttpGet]
        public IActionResult GetCJ2(int id)
        {
            if (id == 1)
                return Ok(88);
            else
                return NotFound("id错误");
        }

        [HttpGet]
        public async Task<List<Person>> GetPersonList(long Id)
        {
            List<Person> list = new List<Person>();
            if (Id == 1)
            {
                list.Add(new Person(1, "顾欢", 99));
                list.Add(new Person(1, "顾欢", 99));
            }
            return list;
        }

        [HttpPost]
        public string[] SaveNote(SaveNoteRequest saveNoteRequest)
        {
            System.IO.File.WriteAllText($"{saveNoteRequest.Title}.txt", saveNoteRequest.Content);
            return new string[] { "Ok", saveNoteRequest.Title };
        }
    }
}

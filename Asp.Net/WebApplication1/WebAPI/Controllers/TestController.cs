using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Model;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public Person GetPerson()
        {
            return new Person("顾欢", 99);
        }

        [HttpPost]
        public string[] SaveNote(SaveNoteRequest saveNoteRequest)
        {
            System.IO.File.WriteAllText($"{saveNoteRequest.Title}.txt", saveNoteRequest.Content);
            return new string[] { "Ok", saveNoteRequest.Title };
        }
    }
}

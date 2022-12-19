using Microsoft.AspNetCore.Mvc;
using MVCTest.Models;

namespace MVCTest.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Demo1()
        {
            Person person = new Person("gh",true,DateTime.Now); 
            return View(person);
        }
    }
}

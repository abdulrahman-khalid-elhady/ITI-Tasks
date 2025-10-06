using Microsoft.AspNetCore.Mvc;

namespace MVC_ITI_Project.Controllers
{
    public class CourseStudentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

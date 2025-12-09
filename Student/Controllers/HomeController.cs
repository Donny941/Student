using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StudentDashboard.Models;
using StudentDashboard.Services;

namespace StudentDashboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StudentService _studentService;

        public HomeController(ILogger<HomeController> logger,
            StudentService studentService)
        {
            _logger = logger;
            _studentService = studentService;

        }

        public IActionResult Index()
        {


            return View();
        }

        public IActionResult Create()
        {
            return View("_Create");
        }
        public async Task<IActionResult> AddingStudent(StudentDashboard.Models.Entities.Student student)
        {
            try
            {
                await _studentService.AddStudent(student);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View("index");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

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

        [HttpGet]
        public async Task<JsonResult> GetStudents()
        {
            try
            {
                var students = await _studentService.GetStudentAsync();
                return new JsonResult(new { data = students });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero degli studenti");
                return new JsonResult(new { data = new List<StudentDashboard.Models.Entities.Student>() });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddingStudent([FromBody] StudentDashboard.Models.Entities.Student student)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, message = "Dati non validi" });
                }

                var result = await _studentService.AddStudent(student);

                if (result)
                {
                    return Json(new
                    {
                        success = true,
                        message = "Studente aggiunto con successo!",
                        studentId = student.Id
                    });
                }
                else
                {
                    return Json(new { success = false, message = "Errore durante il salvataggio" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante l'aggiunta dello studente");
                return Json(new { success = false, message = "Si è verificato un errore: " + ex.Message });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
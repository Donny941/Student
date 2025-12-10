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
        public async Task<IActionResult> GetStudentModals(Guid studentId)
        {
            try
            {

                if (studentId == Guid.Empty)
                {
                    return BadRequest("ID studente non fornito.");
                }


                var student = await _studentService.GetStudentById(studentId);

                if (student == null)
                {
                    return NotFound($"Studente con ID {studentId} non trovato.");
                }


                return PartialView("_StudentModals", student);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Errore durante il tentativo di caricamento della modale per l'ID: {StudentId}", studentId);
                return StatusCode(500, "Errore interno durante il caricamento del modulo.");
            }
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
        public async Task<IActionResult> AddingStudent(StudentDashboard.Models.Entities.Student student)
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

        [HttpPost]
        public async Task<IActionResult> UpdateStudent(StudentDashboard.Models.Entities.Student student)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Dati non validi" });
            }

            try
            {
                var existingUser = await _studentService.GetStudentById(student.Id);

                if (existingUser is null)
                {
                    return Json(new { success = false, message = $"Studente con ID {student.Id} non trovato." });
                }

                var isUpdated = await _studentService.UpdateStudentAsync(existingUser);

                if (isUpdated)
                {
                    return Json(new { success = true, message = "Studente aggiornato con successo." });
                }
                else
                {
                    return Json(new { success = false, message = "Nessuna modifica effettuata o errore di salvataggio del database." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Errore durante l'aggiornamento: {ex.Message}" });
            }
        }

        [HttpPost]

        public async Task<IActionResult> DeleteStudent(Guid Id)
        {
            if (Id == null)
            {
                return Json(new { success = false, message = "ID studente non valido." });
            }

            try
            {
                var isDeleted = await _studentService.DeleteStudentAsync(Id);

                if (isDeleted)
                {
                    return Json(new { success = true, message = "Studente eliminato con successo." });
                }
                else
                {
                    return Json(new { success = false, message = "Studente non trovato o errore durante l'eliminazione." });
                }
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = $"Errore durante l'eliminazione: {ex.Message}" });
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeSheets.API.Data;
using TimeSheets.API.Models;

namespace TimeSheets.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class StudentsController : Controller
    {
        private readonly TimeSheetsDbContext _timeSheetsDbContext;
        public StudentsController(TimeSheetsDbContext timeSheetsDbContext)
        {
            _timeSheetsDbContext = timeSheetsDbContext;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _timeSheetsDbContext.Students.ToListAsync();
            return Ok(students);
        }

        [HttpPost]

        public async Task<IActionResult> AddStudent([FromBody] Student studentReq)
        {
            studentReq.Id = Guid.NewGuid();
            await _timeSheetsDbContext.Students.AddAsync(studentReq);
            await _timeSheetsDbContext.SaveChangesAsync();

            return Ok(studentReq);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetStudent([FromRoute] Guid id)
        {
            var student = await _timeSheetsDbContext.Students.FirstOrDefaultAsync(x => x.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateStudent([FromRoute] Guid id, Student updateStudentReq)
        {
           var student = await _timeSheetsDbContext.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            student.studentNumber = updateStudentReq.studentNumber;
            student.firstName = updateStudentReq.firstName;
            student.middleInitial = updateStudentReq.middleInitial;
            student.lastName = updateStudentReq.lastName;
            student.programId = updateStudentReq.programId;
            student.studentEmail = updateStudentReq.studentEmail;
            student.mDate = updateStudentReq.mDate;
            student.cDate = updateStudentReq.cDate;
            student.mUser = updateStudentReq.mUser;
            student.cUser = updateStudentReq.cUser;
            student.status = updateStudentReq.status;


            await _timeSheetsDbContext.SaveChangesAsync();
            return Ok(student);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] Guid id)
        {
            var student = await _timeSheetsDbContext.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            _timeSheetsDbContext.Students.Remove(student);
            await _timeSheetsDbContext.SaveChangesAsync();
            return Ok(student);
        }
    }
}

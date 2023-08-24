using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using TimeSheets.API.Data;
using TimeSheets.API.Models;

namespace TimeSheets.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class TimesheetsController : Controller
    {
        private readonly TimeSheetsDbContext _timeSheetsDbContext;
        public TimesheetsController(TimeSheetsDbContext timeSheetsDbContext)
        {
            _timeSheetsDbContext = timeSheetsDbContext;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllTimesheets()
        {
            var timesheets = await _timeSheetsDbContext.Timesheets.ToListAsync();

            foreach (Timesheet t in timesheets)
            {
                t.workList = await _timeSheetsDbContext.Works.Where(x => x.TimesheetId == t.Id).ToListAsync();
                t.qAList = await _timeSheetsDbContext.QAs.Where(x => x.TimesheetId == t.Id).ToListAsync();
            }

            return Ok(timesheets);
        }

        [HttpPost]

        public async Task<IActionResult> AddTimesheet([FromBody] Timesheet timesheetReq)
        {
            timesheetReq.Id = Guid.NewGuid();
            await _timeSheetsDbContext.Timesheets.AddAsync(timesheetReq);
            await _timeSheetsDbContext.SaveChangesAsync();

            return Ok(timesheetReq);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetTimesheet([FromRoute] Guid id)
        {
            var timesheet = await _timeSheetsDbContext.Timesheets.FirstOrDefaultAsync(x => x.Id == id);

            if (timesheet == null)
            {
                return NotFound();
            }

            timesheet.workList = await _timeSheetsDbContext.Works.Where(x => x.TimesheetId == timesheet.Id).ToListAsync();
            timesheet.qAList = await _timeSheetsDbContext.QAs.Where(x => x.TimesheetId == timesheet.Id).ToListAsync();


            return Ok(timesheet);
        }

        [HttpGet]
        [Route("{studentNumber}")]
        public async Task<IActionResult> GetTimesheetsBySID([FromRoute] string studentNumber)
        {
            var timesheets = await _timeSheetsDbContext.Timesheets.Where(x => x.studentNumber == studentNumber).ToListAsync();

            if (timesheets == null)
            {
                return NotFound();
            }

            foreach (Timesheet t in timesheets)
            {
                t.workList = await _timeSheetsDbContext.Works.Where(x => x.TimesheetId == t.Id).ToListAsync();
                t.qAList = await _timeSheetsDbContext.QAs.Where(x => x.TimesheetId == t.Id).ToListAsync();
            }
            return Ok(timesheets);
        }


        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateTimesheet([FromRoute] Guid id, Timesheet updateTimesheetReq)
        {
           var timesheet = await _timeSheetsDbContext.Timesheets.FindAsync(id);

            if (timesheet == null)
            {
                return NotFound();
            }

            timesheet.sheetName = updateTimesheetReq.sheetName;
            timesheet.firstName = updateTimesheetReq.firstName;
            timesheet.lastName = updateTimesheetReq.lastName;
            timesheet.companyName = updateTimesheetReq.companyName;
            timesheet.mentorName = updateTimesheetReq.mentorName;
            timesheet.periodId = updateTimesheetReq.periodId;
            timesheet.workList = updateTimesheetReq.workList;
            timesheet.qAList = updateTimesheetReq.qAList;
            timesheet.comment = updateTimesheetReq.comment;
            timesheet.mDate = updateTimesheetReq.mDate;
            timesheet.cDate = updateTimesheetReq.cDate;
            timesheet.mUser = updateTimesheetReq.mUser;
            timesheet.cUser = updateTimesheetReq.cUser;
            timesheet.status = updateTimesheetReq.status;


            await _timeSheetsDbContext.SaveChangesAsync();
            return Ok(timesheet);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteTimesheet([FromRoute] Guid id)
        {
            var timesheet = await _timeSheetsDbContext.Timesheets.FindAsync(id);

            if (timesheet == null)
            {
                return NotFound();
            }

            _timeSheetsDbContext.Timesheets.Remove(timesheet);
            await _timeSheetsDbContext.SaveChangesAsync();
            return Ok(timesheet);
        }
    }
}

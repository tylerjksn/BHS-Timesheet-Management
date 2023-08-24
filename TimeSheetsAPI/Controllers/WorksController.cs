using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeSheets.API.Data;
using TimeSheets.API.Models;

namespace TimeSheets.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class WorksController : Controller
    {
        private readonly TimeSheetsDbContext _timeSheetsDbContext;
        public WorksController(TimeSheetsDbContext timeSheetsDbContext)
        {
            _timeSheetsDbContext = timeSheetsDbContext;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllWorks()
        {
            var works = await _timeSheetsDbContext.Works.ToListAsync();
            return Ok(works);
        }

        [HttpPost]

        public async Task<IActionResult> AddWork([FromBody] Work workReq)
        {
            workReq.Id = Guid.NewGuid();
            await _timeSheetsDbContext.Works.AddAsync(workReq);
            await _timeSheetsDbContext.SaveChangesAsync();

            return Ok(workReq);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetWork([FromRoute] Guid id)
        {
            var work = await _timeSheetsDbContext.Works.FirstOrDefaultAsync(x => x.Id == id);

            if (work == null)
            {
                return NotFound();
            }

            return Ok(work);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateWork([FromRoute] Guid id, Work updateWorkReq)
        {
           var work = await _timeSheetsDbContext.Works.FindAsync(id);

            if (work == null)
            {
                return NotFound();
            }

            work.date = updateWorkReq.date;
            work.day = updateWorkReq.day;
            work.timeIn = updateWorkReq.timeIn;
            work.timeOut = updateWorkReq.timeOut;
            work.hours = updateWorkReq.hours;
            work.mDate = updateWorkReq.mDate;
            work.cDate = updateWorkReq.cDate;
            work.mUser = updateWorkReq.mUser;
            work.cUser = updateWorkReq.cUser;
            work.status = updateWorkReq.status;


            await _timeSheetsDbContext.SaveChangesAsync();
            return Ok(work);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteWork([FromRoute] Guid id)
        {
            var work = await _timeSheetsDbContext.Works.FindAsync(id);

            if (work == null)
            {
                return NotFound();
            }

            _timeSheetsDbContext.Works.Remove(work);
            await _timeSheetsDbContext.SaveChangesAsync();
            return Ok(work);
        }
    }
}

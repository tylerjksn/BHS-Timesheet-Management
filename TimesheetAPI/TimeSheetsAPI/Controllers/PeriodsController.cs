using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeSheets.API.Data;
using TimeSheets.API.Models;

namespace TimeSheets.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PeriodsController : Controller
    {
        private readonly TimeSheetsDbContext _timeSheetsDbContext;
        public PeriodsController(TimeSheetsDbContext timeSheetsDbContext)
        {
            _timeSheetsDbContext = timeSheetsDbContext;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllPeriods()
        {
            var periods = await _timeSheetsDbContext.Periods.ToListAsync();
            return Ok(periods);
        }

        [HttpPost]

        public async Task<IActionResult> AddPeriod([FromBody] Period periodReq)
        {
            periodReq.Id = Guid.NewGuid();
            await _timeSheetsDbContext.Periods.AddAsync(periodReq);
            await _timeSheetsDbContext.SaveChangesAsync();

            return Ok(periodReq);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetPeriod([FromRoute] Guid id)
        {
            var period = await _timeSheetsDbContext.Periods.FirstOrDefaultAsync(x => x.Id == id);

            if (period == null)
            {
                return NotFound();
            }

            return Ok(period);
        }

        [HttpGet]
        [Route("{periodId}")]
        public async Task<IActionResult> GetPeriodByPeriodId([FromRoute] string periodId) //202#Q#W#
        {
            var period = await _timeSheetsDbContext.Periods.FirstOrDefaultAsync(x => x.periodId == periodId);

            if (period == null)
            {
                return NotFound();
            }

            return Ok(period);
        }







        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdatePeriod([FromRoute] Guid id, Period updatePeriodReq)
        {
           var period = await _timeSheetsDbContext.Periods.FindAsync(id);

            if (period == null)
            {
                return NotFound();
            }

            period.periodId = updatePeriodReq.periodId;
            period.year = updatePeriodReq.year;
            period.quarter = updatePeriodReq.quarter;
            period.week = updatePeriodReq.week;
            period.mDate = updatePeriodReq.mDate;
            period.cDate = updatePeriodReq.cDate;
            period.mUser = updatePeriodReq.mUser;
            period.cUser = updatePeriodReq.cUser;
            period.status = updatePeriodReq.status;


            await _timeSheetsDbContext.SaveChangesAsync();
            return Ok(period);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeletePeriod([FromRoute] Guid id)
        {
            var period = await _timeSheetsDbContext.Periods.FindAsync(id);

            if (period == null)
            {
                return NotFound();
            }

            _timeSheetsDbContext.Periods.Remove(period);
            await _timeSheetsDbContext.SaveChangesAsync();
            return Ok(period);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeSheets.API.Data;
using TimeSheets.API.Models;

namespace TimeSheets.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class QAsController : Controller
    {
        private readonly TimeSheetsDbContext _timeSheetsDbContext;
        public QAsController(TimeSheetsDbContext timeSheetsDbContext)
        {
            _timeSheetsDbContext = timeSheetsDbContext;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllQAs()
        {
            var qAs = await _timeSheetsDbContext.QAs.ToListAsync();
            return Ok(qAs);
        }

        [HttpPost]

        public async Task<IActionResult> AddQA([FromBody] QA qAReq)
        {
            qAReq.Id = Guid.NewGuid();
            await _timeSheetsDbContext.QAs.AddAsync(qAReq);
            await _timeSheetsDbContext.SaveChangesAsync();

            return Ok(qAReq);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetQA([FromRoute] Guid id)
        {
            var qA = await _timeSheetsDbContext.QAs.FirstOrDefaultAsync(x => x.Id == id);

            if (qA == null)
            {
                return NotFound();
            }

            return Ok(qA);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateQA([FromRoute] Guid id, QA updateQAReq)
        {
           var qA = await _timeSheetsDbContext.QAs.FindAsync(id);

            if (qA == null)
            {
                return NotFound();
            }

            qA.questionId = updateQAReq.questionId;
            qA.ans = updateQAReq.ans;
            qA.mDate = updateQAReq.mDate;
            qA.cDate = updateQAReq.cDate;
            qA.mUser = updateQAReq.mUser;
            qA.cUser = updateQAReq.cUser;
            qA.status = updateQAReq.status;


            await _timeSheetsDbContext.SaveChangesAsync();
            return Ok(qA);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteQA([FromRoute] Guid id)
        {
            var qA = await _timeSheetsDbContext.QAs.FindAsync(id);

            if (qA == null)
            {
                return NotFound();
            }

            _timeSheetsDbContext.QAs.Remove(qA);
            await _timeSheetsDbContext.SaveChangesAsync();
            return Ok(qA);
        }
    }
}

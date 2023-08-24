using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeSheets.API.Data;
using TimeSheets.API.Models;

namespace TimeSheets.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class SheetTemplatesController : Controller
    {
        private readonly TimeSheetsDbContext _timeSheetsDbContext;
        public SheetTemplatesController(TimeSheetsDbContext timeSheetsDbContext)
        {
            _timeSheetsDbContext = timeSheetsDbContext;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllSheetTemplates()
        {
            var sheetTemplates = await _timeSheetsDbContext.SheetTemplates.ToListAsync();

            foreach (SheetTemplate st in sheetTemplates)
            {
                st.Questions = await _timeSheetsDbContext.Template_Questions.Where(x => x.SheetTemplateId == st.Id).ToListAsync();
                
            }

            return Ok(sheetTemplates);
        }

        [HttpPost]

        public async Task<IActionResult> AddSheetTemplate([FromBody] SheetTemplate sheetTemplateReq)
        {
            sheetTemplateReq.Id = Guid.NewGuid();
            await _timeSheetsDbContext.SheetTemplates.AddAsync(sheetTemplateReq);
            await _timeSheetsDbContext.SaveChangesAsync();

            return Ok(sheetTemplateReq);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetSheetTemplate([FromRoute] Guid id)
        {
            var sheetTemplate = await _timeSheetsDbContext.SheetTemplates.FirstOrDefaultAsync(x => x.Id == id);

            if (sheetTemplate == null)
            {
                return NotFound();
            }

            sheetTemplate.Questions = await _timeSheetsDbContext.Template_Questions.Where(x => x.SheetTemplateId == sheetTemplate.Id).ToListAsync();

            return Ok(sheetTemplate);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateSheetTemplate([FromRoute] Guid id, SheetTemplate updateSheetTemplateReq)
        {
           var sheetTemplate = await _timeSheetsDbContext.SheetTemplates.FindAsync(id);

            if (sheetTemplate == null)
            {
                return NotFound();
            }
            sheetTemplate.sheetName = updateSheetTemplateReq.sheetName;
            sheetTemplate.mDate = updateSheetTemplateReq.mDate;
            sheetTemplate.cDate = updateSheetTemplateReq.cDate;
            sheetTemplate.mUser = updateSheetTemplateReq.mUser;
            sheetTemplate.cUser = updateSheetTemplateReq.cUser;
            sheetTemplate.status = updateSheetTemplateReq.status;

            await _timeSheetsDbContext.SaveChangesAsync();
            return Ok(sheetTemplate);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteSheetTemplate([FromRoute] Guid id)
        {
            var sheetTemplate = await _timeSheetsDbContext.SheetTemplates.FindAsync(id);

            if (sheetTemplate == null)
            {
                return NotFound();
            }

            _timeSheetsDbContext.SheetTemplates.Remove(sheetTemplate);
            await _timeSheetsDbContext.SaveChangesAsync();
            return Ok(sheetTemplate);
        }
    }
}

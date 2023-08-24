using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TimeSheets.API.Data;
using TimeSheets.API.Models;

namespace TimeSheets.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class QuestionsController : Controller
    {
        private readonly TimeSheetsDbContext _timeSheetsDbContext;
        public QuestionsController(TimeSheetsDbContext timeSheetsDbContext)
        {
            _timeSheetsDbContext = timeSheetsDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQuestions()
        {
            var questions = await _timeSheetsDbContext.Questions.ToListAsync();
            return Ok(questions);
        }

        [HttpPost]
        public async Task<IActionResult> AddQuestion([FromBody] Question questionReq)
        {
            questionReq.Id = Guid.NewGuid();
            await _timeSheetsDbContext.Questions.AddAsync(questionReq);
            await _timeSheetsDbContext.SaveChangesAsync();

            return Ok(questionReq);
        }

    /*   [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetQuestion([FromRoute] Guid id)
        {
            var question = await _timeSheetsDbContext.Questions.FirstOrDefaultAsync(x => x.Id == id);

            if (question == null)
            {
                return NotFound();
            }

            return Ok(question);
        } */

        [HttpGet]
        [Route("{sheetTemplateId:Guid}")]
        public async Task<IActionResult> GetQuestionsByTemplateId([FromRoute] Guid sheetTemplateId)
        {
            var questionList = await (from t in _timeSheetsDbContext.Template_Questions
                               join q in _timeSheetsDbContext.Questions on t.QuestionId equals q.Id
                               where t.SheetTemplateId == sheetTemplateId
                               select q).ToListAsync();

            if (questionList == null)
            {
                return NotFound();
            }

            return Ok(questionList);
      
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateQuestion([FromRoute] Guid id, Question updateQuestionReq)
        {
           var question = await _timeSheetsDbContext.Questions.FindAsync(id);

            if (question == null)
            {
                return NotFound();
            }

            question.questionName = updateQuestionReq.questionName;
            question.qFullText = updateQuestionReq.qFullText;
            question.mDate = updateQuestionReq.mDate;
            question.cDate = updateQuestionReq.cDate;
            question.mUser = updateQuestionReq.mUser;
            question.cUser = updateQuestionReq.cUser;
            question.status = updateQuestionReq.status;


            await _timeSheetsDbContext.SaveChangesAsync();
            return Ok(question);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteQuestion([FromRoute] Guid id)
        {
            var question = await _timeSheetsDbContext.Questions.FindAsync(id);

            if (question == null)
            {
                return NotFound();
            }

            _timeSheetsDbContext.Questions.Remove(question);
            await _timeSheetsDbContext.SaveChangesAsync();
            return Ok(question);
        }
    }
}

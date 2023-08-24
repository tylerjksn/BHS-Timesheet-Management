namespace TimeSheets.API.Models
{
    public class Template_Question
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public Guid SheetTemplateId { get; set; }
        
    }
}

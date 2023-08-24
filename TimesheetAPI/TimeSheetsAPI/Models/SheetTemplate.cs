using System.Data;

namespace TimeSheets.API.Models
{
    public class SheetTemplate
    {
        public Guid Id { get; set; }
        public string sheetName { get; set; }
        public int countOfDays { get; set; }
        public DateTime mDate { get; set; }
        public DateTime cDate { get; set; }
        public string mUser { get; set; }
        public string cUser { get; set; }
        public string status { get; set; }

        //Navigation
        public List<Template_Question> Questions { get; set; }
    }
}

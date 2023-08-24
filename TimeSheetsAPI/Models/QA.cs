using System.Data;

namespace TimeSheets.API.Models
{
    public class QA
    {
        public Guid Id { get; set; }
        public string questionId { get; set; }
        public string ans { get; set; }
        public Guid TimesheetId { get; set; }
        public DateTime mDate { get; set; }
        public DateTime cDate { get; set; }
        public string mUser { get; set; }
        public string cUser { get; set; }
        public string status { get; set; }


    }
}

using System.Data;

namespace TimeSheets.API.Models
{
    public class Timesheet
    {
        public Guid Id { get; set; }
        public int sheetName { get; set; }
        public string periodId { get; set; }
        public string studentNumber { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string companyName { get; set; }
        public string mentorName { get; set; }
        public ICollection<Work> workList { get; set; }
        public ICollection<QA> qAList { get; set; }
        public string comment { get; set; }
        public DateTime mDate { get; set; }
        public DateTime cDate { get; set; }
        public string mUser { get; set; }
        public string cUser { get; set; }
        public string status { get; set; }

    }
}

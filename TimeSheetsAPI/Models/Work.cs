namespace TimeSheets.API.Models
{
    public class Work
    {
        public Guid Id { get; set; }
        public string date { get; set; }
        public string day { get; set; }
        public string timeIn { get; set; }
        public string timeOut { get; set; }
        public string hours { get; set; }
        public Guid TimesheetId { get; set; }
        public DateTime mDate { get; set; }
        public DateTime cDate { get; set; }
        public string mUser { get; set; }
        public string cUser { get; set; }
        public string status { get; set; }


    }
}

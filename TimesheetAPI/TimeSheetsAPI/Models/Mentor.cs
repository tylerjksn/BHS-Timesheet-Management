namespace TimeSheets.API.Models
{
    public class Mentor
    {
        public Guid Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string employmentLevel { get; set; }
        public DateTime mDate { get; set; }
        public DateTime cDate { get; set; }

        public string mUser { get; set; }
        public string cUser { get; set; }
        public string status { get; set; }
    }
}

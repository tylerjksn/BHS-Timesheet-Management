namespace TimeSheets.API.Models
{
    public class Company
    {
        public Guid Id { get; set; }
        public string companyName { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public DateTime mDate { get; set; }
        public DateTime cDate { get; set; }
        public string mUser { get; set; }
        public string cUser { get; set; }
        public string status { get; set; }
    }
}

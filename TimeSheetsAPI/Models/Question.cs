using System.Data;

namespace TimeSheets.API.Models
{
    public class Question
    {
        public Guid Id { get; set; }
        public string questionName { get; set; }
        public string qFullText { get; set; }
        public DateTime mDate { get; set; }
        public DateTime cDate { get; set; }
        public string mUser { get; set; }
        public string cUser { get; set; }
        public string status { get; set; }

        
    }

}

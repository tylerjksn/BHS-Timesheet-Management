using System.Data;

namespace TimeSheets.API.Models
{
    public class Student
    {
        public Guid Id { get; set; }
        public string programId { get; set; }
        public string studentNumber { get; set; }
        public string firstName { get; set; }
        public char? middleInitial { get; set; }
        public string lastName { get; set; }
        public string studentEmail { get; set; }
        public DateTime mDate { get; set; }
        public DateTime cDate { get; set; }
        public string mUser { get; set; }
        public string cUser { get; set; }
        public string status { get; set; }


    }
}

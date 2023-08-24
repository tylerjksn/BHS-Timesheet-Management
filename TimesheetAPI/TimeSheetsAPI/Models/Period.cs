using System.Data;

namespace TimeSheets.API.Models
{
    public class Period
    {
        public Guid Id { get; set; }
        public string periodId { get; set; } //Y202#Q#W#
        public int year { get; set; }
        public int quarter { get; set; }
        public int week { get; set; }
        public DateTime mDate { get; set; }
        public DateTime cDate { get; set; }
        public string mUser { get; set; }
        public string cUser { get; set; }
        public string status { get; set; }


    }
}

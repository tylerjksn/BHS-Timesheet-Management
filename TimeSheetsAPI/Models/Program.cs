﻿using System.Data;

namespace TimeSheets.API.Models
{
    public class Program
    {
        public Guid Id { get; set; }
        public string programId { get; set; }
        public DateTime mDate { get; set; }
        public DateTime cDate { get; set; }
        public string mUser { get; set; }
        public string cUser { get; set; }
        public string status { get; set; }


    }
}

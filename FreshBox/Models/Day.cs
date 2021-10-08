using SQLite;
using System;
using System.Collections.Generic;

namespace FreshBox.Models
{
    public class Day
    {
        [PrimaryKey]
        public DateTime Date { get; set; }
    }
}

using SQLite;
using System.Collections.Generic;

namespace FreshBox.Models
{
    public class FreshBoxes
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
}

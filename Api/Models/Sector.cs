using System.Collections.Generic;

namespace Api.Models
{
    public class Sector
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brief { get; set; }
        public List<Company> Companies { get; set; }
    }
}

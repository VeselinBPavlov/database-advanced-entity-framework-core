using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedQuerying
{
    public class Town
    {
        public int TownId { get; set; }

        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}

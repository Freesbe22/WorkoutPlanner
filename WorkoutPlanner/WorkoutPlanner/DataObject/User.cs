using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutPlanner.DataObject
{
    public class User
    {
        public string Id { get; set; } // Firebase UID
        public string Name { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public List<UserProgress> Progress { get; set; } = new();
    }

}

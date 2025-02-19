using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutPlanner.DataObject
{
    public class UserProgress
    {
        public string Id { get; set; }
        public User User { get; set; } // Référence à l'utilisateur
        public Workout Workout { get; set; } // Référence à l'entraînement
        public DateTime Date { get; set; }
        public List<ProgressSet> Sets { get; set; } = new();
    }
}

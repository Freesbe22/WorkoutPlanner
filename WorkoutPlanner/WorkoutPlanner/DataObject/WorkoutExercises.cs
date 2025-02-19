using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutPlanner.DataObject
{
    public class WorkoutExercise
    {
        public string Id { get; set; }
        public Exercise Exercise { get; set; } // Référence à un Exercise
        public int Sets { get; set; }
        public int Reps { get; set; } // Pour musculation
        public int Duration { get; set; } // Pour le cardio (en secondes)
        public int RestTime { get; set; } // Temps de repos entre les séries
    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutPlanner.DataObject
{
    public class ProgressSet
    {
        public UserProgress UserProgress { get; set; } // Référence à l'exercice
        public Exercise Exercise { get; set; } // Référence à l'exercice
        public int SetNumber { get; set; }
        public int Reps { get; set; }
        public double Weight { get; set; } // Poids utilisé
        public int Duration { get; set; } // Temps pour le cardio
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutPlanner.DataObject.Enum;

namespace WorkoutPlanner.DataObject
{
    public class Workout
    {
        public string Id { get; set; }          // Identifiant unique de l'entraînement
        public string Name { get; set; }        // Nom de l'entraînement (ex: "Full Body", "Rest Day")
        public string Description { get; set; } // Description de l'entraînement
        public int DayNumber { get; set; }      // Jour dans la phase (ex: 1, 2, 3...)
        public WorkoutType Type { get; set; }   // Normal ou RestDay
        public List<WorkoutSectionDetails> Sections { get; set; } // Liste des IDs des phases du programme
    }
}

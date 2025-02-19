using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutPlanner.DataObject.Enum;

namespace WorkoutPlanner.DataObject
{
    public class Phase
    {
        public string Id { get; set; }             // Identifiant unique de la phase
        public string Name { get; set; }           // Nom de la phase (ex: "Semaine 1-4")
        public string ProgramId { get; set; }      // Référence au programme
        public ScheduleType ScheduleType { get; set; } // Type de planification
        public List<Workout> Workouts { get; set; }  // Liste des IDs des entraînements de cette phase
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutPlanner.DataObject
{
    public class WorkoutSectionDetails
    {
        public string Id { get; set; }
        public string WorkoutId { get; set; }
        public WorkoutSection Section { get; set; }
        public int Order { get; set; } // Permet d’organiser les sections
        public List<WorkoutExercise> Exercises { get; set; } = new();
    }

}

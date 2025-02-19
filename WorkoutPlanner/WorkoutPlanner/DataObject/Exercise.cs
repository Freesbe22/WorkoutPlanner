using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutPlanner.DataObject.Enum;

namespace WorkoutPlanner.DataObject
{
    public class Exercise
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Keywords { get; set; } = new(); // Pour faciliter la recherche
        public string Instructions { get; set; }
        public string VideoUrl { get; set; }
        public ExerciseCategory Category { get; set; }
        public List<MuscleGroup> MuscleGroups { set; get; }
        public List<Equipment> Equipements { set; get; }
    }
}

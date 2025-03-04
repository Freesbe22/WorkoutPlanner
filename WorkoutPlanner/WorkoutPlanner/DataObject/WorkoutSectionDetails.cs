using Google.Cloud.Firestore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutPlanner.DataObject
{
    [FirestoreData]
    public class WorkoutSectionDetails
    {
        [FirestoreProperty]
        public string Id { get; set; }
        [FirestoreProperty]
        public string WorkoutId { get; set; }
        [FirestoreProperty]
        public WorkoutSection Section { get; set; }
        [FirestoreProperty]
        public int Order { get; set; } // Permet d’organiser les sections
        [FirestoreProperty]
        public List<WorkoutExercise> Exercises { get; set; } = new();
    }

}

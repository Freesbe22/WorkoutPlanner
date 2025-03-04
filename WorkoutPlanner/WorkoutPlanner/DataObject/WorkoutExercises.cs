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
    public class WorkoutExercise
    {
        [FirestoreDocumentId]
        public string Id { get; set; }
        [FirestoreProperty]
        public Exercise Exercise { get; set; } // Référence à un Exercise
        [FirestoreProperty]
        public int Sets { get; set; }
        [FirestoreProperty]
        public int Reps { get; set; } // Pour musculation
        [FirestoreProperty]
        public int Duration { get; set; } // Pour le cardio (en secondes)
        [FirestoreProperty]
        public int RestTime { get; set; } // Temps de repos entre les séries
    }


}

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
    public class ProgressSet
    {
        [FirestoreDocumentId]
        public string Id { get; set; } // Firebase UID
        [FirestoreProperty]
        public UserProgress UserProgress { get; set; } // Référence à l'exercice
        [FirestoreProperty]
        public Exercise Exercise { get; set; } // Référence à l'exercice
        [FirestoreProperty]
        public int SetNumber { get; set; }
        [FirestoreProperty]
        public int Reps { get; set; }
        [FirestoreProperty]
        public double Weight { get; set; } // Poids utilisé
        [FirestoreProperty]
        public int Duration { get; set; } // Temps pour le cardio
    }
}

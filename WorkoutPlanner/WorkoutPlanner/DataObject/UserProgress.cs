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
    public class UserProgress
    {
        [FirestoreDocumentId]
        public string Id { get; set; }
        [FirestoreProperty]
        public User User { get; set; } // Référence à l'utilisateur
        [FirestoreProperty]
        public Workout Workout { get; set; } // Référence à l'entraînement
        [FirestoreProperty]
        public DateTime Date { get; set; }
        [FirestoreProperty]
        public List<ProgressSet> Sets { get; set; } = new();
    }
}

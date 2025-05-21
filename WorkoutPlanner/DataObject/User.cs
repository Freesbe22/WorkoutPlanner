using Google.Cloud.Firestore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutPlanner.DataObject
{
    [FirestoreData]
    public class User
    {
        [FirestoreDocumentId]
        public string Id { get; set; } // Firebase UID
        [FirestoreDocumentId]
        public string AuthId { get; set; } // Firebase Auth UID
        [FirestoreProperty]
        [Required]
        public string Name { get; set; }
        [FirestoreProperty]
        public int Age { get; set; }
        [FirestoreProperty]
        public double Weight { get; set; }
        [FirestoreProperty]
        public double Height { get; set; }
        [FirestoreProperty]
        public List<UserProgress> Progress { get; set; } = new();
        [FirestoreProperty]
        public string CurrentWorkoutPlanId { get; set; } // Son programme actif
        [FirestoreProperty]
        public List<string> WorkoutPlanIds { get; set; } = new(); // Tous ses programmes
    }

}

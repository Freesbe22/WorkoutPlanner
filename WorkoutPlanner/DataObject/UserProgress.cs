using Google.Cloud.Firestore;

namespace WorkoutPlanner.DataObject
{
    [FirestoreData]
    public class UserProgress
    {
        [FirestoreDocumentId]
        public string Id { get; set; }
        [FirestoreProperty]
        public string UserId { get; set; } // Référence à l'utilisateur
        [FirestoreProperty]
        public string WorkoutId { get; set; } // Référence à l'entraînement
        [FirestoreProperty]
        public string WorkoutPlanId { get; set; } // Référence au programme
        [FirestoreProperty]
        public DateTime Date { get; set; }
        [FirestoreProperty]
        public List<ProgressSet> Sets { get; set; } = new();
    }
}

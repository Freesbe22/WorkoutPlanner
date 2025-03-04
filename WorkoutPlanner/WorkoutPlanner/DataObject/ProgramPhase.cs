using Google.Cloud.Firestore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using WorkoutPlanner.DataObject.Enum;

namespace WorkoutPlanner.DataObject
{
    [FirestoreData]
    public class ProgramPhase
    {
        [FirestoreDocumentId]
        public string Id { get; set; }             // Identifiant unique de la phase

        [FirestoreProperty]
        [Required]
        public string Name { get; set; }           // Nom de la phase (ex: "Semaine 1-4")

        [FirestoreProperty]
        [Required]
        public int Duration { get; set; }      // Durée de la phase en semaine

        [FirestoreProperty]
        [Required]
        public string ProgramId { get; set; }      // Référence au programme

        [FirestoreProperty]
        [Required]
        public ScheduleType ScheduleType { get; set; } // Type de planification

        [FirestoreProperty]
        public List<Workout> Workouts { get; set; }  // Liste des IDs des entraînements de cette phase
    }
}

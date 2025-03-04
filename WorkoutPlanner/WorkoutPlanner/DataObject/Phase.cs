using Google.Cloud.Firestore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutPlanner.DataObject.Enum;

namespace WorkoutPlanner.DataObject
{
    [FirestoreData]
    public class Phase
    {
        [FirestoreDocumentId]
        public string Id { get; set; }             // Identifiant unique de la phase
        [FirestoreProperty]
        public string Name { get; set; }           // Nom de la phase (ex: "Semaine 1-4")
        [FirestoreProperty]
        public string ProgramId { get; set; }      // Référence au programme
        [FirestoreProperty]
        public ScheduleType ScheduleType { get; set; } // Type de planification
        [FirestoreProperty]
        public List<Workout> Workouts { get; set; }  // Liste des IDs des entraînements de cette phase
    }
}

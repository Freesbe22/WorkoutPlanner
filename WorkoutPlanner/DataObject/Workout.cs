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
    public class Workout
    {
        [FirestoreDocumentId]
        public string Id { get; set; }          // Identifiant unique de l'entraînement
        [FirestoreProperty]
        public string Name { get; set; }        // Nom de l'entraînement (ex: "Full Body", "Rest Day")
        [FirestoreProperty]
        public string Description { get; set; } // Description de l'entraînement
        [FirestoreProperty]
        public int DayNumber { get; set; }      // Jour dans la phase (ex: 1, 2, 3...)
        [FirestoreProperty]
        public List<WorkoutSectionDetails> Sections { get; set; } // Liste des IDs des phases du programme
    }
}

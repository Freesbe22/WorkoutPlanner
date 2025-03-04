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
    public class Exercise
    {
        [FirestoreDocumentId]
        public string Id { get; set; }
        [FirestoreProperty]
        public string Name { get; set; }
        [FirestoreProperty]
        public List<string> Keywords { get; set; } = new(); // Pour faciliter la recherche
        [FirestoreProperty]
        public string Instructions { get; set; }
        [FirestoreProperty]
        public string VideoUrl { get; set; }
        [FirestoreProperty]
        public ExerciseCategory Category { get; set; }
        [FirestoreProperty]
        public List<MuscleGroup> MuscleGroups { set; get; }
        [FirestoreProperty]
        public List<Equipment> Equipements { set; get; }
    }
}

using Google.Cloud.Firestore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutPlanner.DataObject.Enum;
using WorkoutPlanner.Tools.Services;

namespace WorkoutPlanner.DataObject
{
    [FirestoreData]
    public class WorkoutPlan
    {
        [FirestoreDocumentId]
        public string Id { get; set; }             // Identifiant unique du programme
        [FirestoreProperty]
        public string Name { get; set; }           // Nom du programme (ex: "Prise de masse 12 semaines")
        [FirestoreProperty]
        public string Description { get; set; }    // Description du programme
        [FirestoreProperty]
        public WorkoutPlanGoal Goal { get; set; }      // Objectif du programme (Perte de poids, Prise de masse...)
        [FirestoreProperty]
        public List<Phase> Phases { get; set; } = new();// Liste des IDs des phases du programme
        [FirestoreProperty]
        public string UserId { get; set; } // Lier le plan à un utilisateur (null si public)
        [FirestoreProperty]
        public bool IsPublic { get; set; } = false; // Par défaut, privé
    }
}

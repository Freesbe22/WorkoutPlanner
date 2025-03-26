using Google.Cloud.Firestore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using WorkoutPlanner.DataObject.Enum;

namespace WorkoutPlanner.DataObject
{
    [FirestoreData]
    public class WorkoutPlan
    {
        [FirestoreDocumentId]
        public string Id { get; set; }             // Identifiant unique du programme
        [FirestoreProperty]
        [Required]
        public string Name { get; set; }           // Nom du programme (ex: "Prise de masse 12 semaines")
        [FirestoreProperty]
        public string Description { get; set; }    // Description du programme
        [FirestoreProperty]
        [Required]
        public WorkoutPlanGoal Goal { get; set; }      // Objectif du programme (Perte de poids, Prise de masse...)
        [FirestoreProperty]
        public List<ProgramPhase> Phases { get; set; } = new();// Liste des IDs des phases du programme
        [FirestoreProperty]
        public string UserId { get; set; } // Lier le plan à un utilisateur (null si public)
        [FirestoreProperty]
        public bool IsPublic { get; set; } = false; // Par défaut, privé
    }
}

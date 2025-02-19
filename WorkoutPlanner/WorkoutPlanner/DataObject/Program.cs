using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutPlanner.DataObject.Enum;

namespace WorkoutPlanner.DataObject
{
    public class Program
    {
        public string Id { get; set; }             // Identifiant unique du programme
        public string Name { get; set; }           // Nom du programme (ex: "Prise de masse 12 semaines")
        public string Description { get; set; }    // Description du programme
        public ProgramGoal Goal { get; set; }      // Objectif du programme (Perte de poids, Prise de masse...)
        public List<Phase> Phases { get; set; } // Liste des IDs des phases du programme
    }
}

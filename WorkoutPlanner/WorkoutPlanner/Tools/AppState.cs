using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WorkoutPlanner.DataObject;

namespace WorkoutPlanner.Tools
{
    public class AppState
    {
        public User User { get; set; }
        public bool EstConnecté { get; set; }
        public DateTime DernierChargement { get; set; }

        // Exemple de data chargée une seule fois
        public List<string> ExercicesDisponibles { get; private set; } = new();

    }
}

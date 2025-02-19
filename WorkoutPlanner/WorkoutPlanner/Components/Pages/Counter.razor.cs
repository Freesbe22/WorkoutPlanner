using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutPlanner.DataObject;

namespace WorkoutPlanner.Components.Pages
{
    partial class Counter : ComponentBase
    {
        private int currentCount = 0;

        private void IncrementCount()
        {
            currentCount++;

            FirebaseClient firebaseClient = new FirebaseClient("https://thedreamlife-workoutplanner-default-rtdb.europe-west1.firebasedatabase.app/");
            firebaseClient.Child("Exercises").PostAsync(new Exercise
            {
                Name = "Push-up"+ currentCount,
            });
        }
    }
}

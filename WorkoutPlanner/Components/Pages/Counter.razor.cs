using Microsoft.AspNetCore.Components;
using WorkoutPlanner.DataObject;

namespace WorkoutPlanner.Components.Pages
{
    partial class Counter : ComponentBase
    {
        private int currentCount = 0;

        private void IncrementCount()
        {
            currentCount++;

            //FirebaseClient firebaseClient = new FirebaseClient("https://thedreamlife-workoutplanner-default-rtdb.europe-west1.firebasedatabase.app/");
            //firebaseClient.Child("Exercises").PostAsync(new Exercise
            //{
            //    Name = "Push-up"+ currentCount,
            //});
        }
    }
}

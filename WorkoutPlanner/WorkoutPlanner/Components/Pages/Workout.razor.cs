using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutPlanner.DataObject;

namespace WorkoutPlanner.Components.Pages
{
    partial class Workout : ComponentBase
    {
        [Inject]
        public FirebaseClient FirebaseClient { get; set; }
        public ObservableCollection<WorkoutPlanner.DataObject.Program> Programs { get; set; } = new ObservableCollection<WorkoutPlanner.DataObject.Program>();
        public WorkoutPlanner.DataObject.Program Program { get; set; }

        protected override void OnInitialized()
        {
            Programs.CollectionChanged += Exercises_CollectionChanged;

            var collection = FirebaseClient
    .Child("Program")
    .AsObservable<WorkoutPlanner.DataObject.Program>()
    .Subscribe((item) =>
    {
        if (item.Object != null)
        {
            Programs.Add(item.Object);
        }
    });

            if(Programs.Count < 1)
            {
                FirebaseClient.Child("Program").PostAsync(new WorkoutPlanner.DataObject.Program
                {
                    Name = "Your first program",
                });
            }
            base.OnInitialized();
        }

        private void Exercises_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Program = Programs.First();
            InvokeAsync( () => { StateHasChanged(); });
        }
    }
}

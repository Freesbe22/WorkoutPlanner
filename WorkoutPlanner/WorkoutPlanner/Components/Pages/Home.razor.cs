using Firebase.Database;
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
    partial class Home : ComponentBase
    {
        [Inject]
        [Required]
        public required FirebaseClient FirebaseClient { get; set; }
        public ObservableCollection<Exercise> Exercises { get; set; } = new ObservableCollection<Exercise>();

        protected override async Task OnInitializedAsync()
        {
            var collection = FirebaseClient
    .Child("Todo")
    .AsObservable<Exercise>()
    .Subscribe((item) =>
    {
        if (item.Object != null)
        {
            Exercises.Add(item.Object);
        }
    });
            await Task.Delay(50);
            StateHasChanged();
            Exercises.CollectionChanged += Exercises_CollectionChanged;
            await base.OnInitializedAsync();
        }

        private void Exercises_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            InvokeAsync( () => { StateHasChanged(); });
        }
    }
}

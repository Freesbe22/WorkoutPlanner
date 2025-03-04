using Microsoft.AspNetCore.Components;
using WorkoutPlanner.DataObject;
using WorkoutPlanner.Tools;
using WorkoutPlanner.Tools.Services;

namespace WorkoutPlanner.Components.Pages
{
    partial class Workout : ComponentBase
    {
        [Inject]
        public FirestoreService FirestoreService { get; set; }
        public List<WorkoutPlan> Programs { get; set; } = new List<WorkoutPlan>();
        public WorkoutPlan Program { get; set; }
        private bool Initialised { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            await LoadPrograms();
            Initialised = true;
            InvokeAsync(() => { StateHasChanged(); });

            await base.OnInitializedAsync();
        }

        private async Task LoadPrograms()
        {
            var firebaseObjectsPublic = await FirestoreService.db.Collection(typeof(WorkoutPlan).Name).WhereEqualTo("IsPublic", true).GetSnapshotAsync();
            firebaseObjectsPublic.ToList().ForEach(firebaseObject => Programs.Add(firebaseObject.ConvertTo<WorkoutPlan>()));
            var firebaseObjectsUser = await FirestoreService.db.Collection(typeof(WorkoutPlan).Name).WhereEqualTo("UserId", "-OK70UxZQBMP6H9kHQuigoi").GetSnapshotAsync();
            firebaseObjectsUser.ToList().ForEach(firebaseObject => Programs.Add(firebaseObject.ConvertTo<WorkoutPlan>()));

            Program = Programs.FirstOrDefault();

            //var firebaseObjectsUser = await FirebaseClient.Child(typeof(WorkoutPlan).Name).OrderBy("UserId").EqualTo("-OK70UxZQBMP6H9kHQuigoi").OnceAsync<WorkoutPlan>();

            //Programs = DBHelper.FirebaseObjectsToList(firebaseObjectsPublic);
            //Programs.AddRange(DBHelper.FirebaseObjectsToList(firebaseObjectsUser));
        }

        protected override void OnAfterRender(bool firstRender)
        {

            if (Programs.Count < 1 && Initialised)
            {
                //DBHelper.Post(FirebaseClient, new WorkoutPlan
                //{
                //    Name = "Your first program",
                //});
            }
            base.OnAfterRender(firstRender);
        }
    }
}

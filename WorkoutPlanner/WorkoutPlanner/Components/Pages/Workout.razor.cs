using Microsoft.AspNetCore.Components;
using WorkoutPlanner.DataObject;
using WorkoutPlanner.Tools;
using WorkoutPlanner.Tools.Services;

namespace WorkoutPlanner.Components.Pages
{
    partial class Workout : ComponentBase
    {
        #region Variables
        [Inject]
        public FirestoreService FirestoreService { get; set; }
        private List<WorkoutPlan> Programs { get; set; } = new List<WorkoutPlan>();
        private WorkoutPlan? Program { get; set; }
        private bool Initialised { get; set; } = false;
        #endregion

        #region Loading
        protected override async Task OnInitializedAsync()
        {
            await LoadPrograms();
            Initialised = true;
            await InvokeAsync(() => { StateHasChanged(); });

            await base.OnInitializedAsync();
        }

        private async Task LoadPrograms()
        {
            ///WIP Tmp
            var firebaseObjectsPublic = await FirestoreService.db.Collection(typeof(WorkoutPlan).Name).WhereEqualTo("IsPublic", true).GetSnapshotAsync();
            var firebaseObjectsUser = await FirestoreService.db.Collection(typeof(WorkoutPlan).Name).WhereEqualTo("UserId", "-OK70UxZQBMP6H9kHQuigoi").GetSnapshotAsync();
            firebaseObjectsPublic.ToList().ForEach(firebaseObject => Programs.Add(firebaseObject.ConvertTo<WorkoutPlan>()));
            firebaseObjectsUser.ToList().ForEach(firebaseObject => Programs.Add(firebaseObject.ConvertTo<WorkoutPlan>()));

            Program = Programs.FirstOrDefault();
        }
        #endregion
    }
}

using Firebase.Auth;
using Microsoft.AspNetCore.Components;

using WorkoutPlanner.Components.Pages.Execution;
using WorkoutPlanner.DataObject;
using WorkoutPlanner.Tools.Services;

namespace WorkoutPlanner.Components.Pages.Workout
{
    partial class WorkoutPlanDetailView : ComponentBase
    {
        #region Variables
        [Inject]
        public FirestoreService FirestoreService { get; set; }
        [Inject]
        public FirebaseAuthClient AuthClient { get; set; }
        private List<WorkoutPlan> Programs { get; set; } = new List<WorkoutPlan>();
        private WorkoutPlan? Program { get; set; }
        private ProgramPhase? Phase { get; set; }
        private bool Initialised { get; set; } = false;

        public ExecutionModal ExecutionModal { get; set; } = new ExecutionModal();
        private bool IsModalVisible { get; set; } = false;

        #endregion

        #region Loading
        protected override async Task OnInitializedAsync()
        {
            await LoadPrograms();
            Initialised = true;
            await InvokeAsync(() => { StateHasChanged(); });

            await base.OnInitializedAsync();
        }

        protected async Task OnProgramChanged()
        {
            if(Program is null)
            {
                await LoadPrograms();
            }
            else 
            {
                await FirestoreService.UpdateObjectReference(FirestoreService.db.Collection(typeof(WorkoutPlan).Name).Document(Program.Id), Program);
            }
            await InvokeAsync(() => { StateHasChanged(); });
        }

        private async Task LoadPrograms()
        {
            ///WIP Tmp
            var firebaseObjectsPublic = await FirestoreService.db.Collection(typeof(WorkoutPlan).Name).WhereEqualTo(nameof(WorkoutPlan.IsPublic), true).GetSnapshotAsync();
            var firebaseObjectsUser = await FirestoreService.db.Collection(typeof(WorkoutPlan).Name).WhereEqualTo(nameof(WorkoutPlan.UserId), AuthClient.User.Uid).GetSnapshotAsync();
            firebaseObjectsPublic.ToList().ForEach(firebaseObject => Programs.Add(firebaseObject.ConvertTo<WorkoutPlan>()));
            firebaseObjectsUser.ToList().ForEach(firebaseObject => Programs.Add(firebaseObject.ConvertTo<WorkoutPlan>()));

            Program = Programs.FirstOrDefault();
        }
        #endregion

        private void Toggle ()
        {
            IsModalVisible = !IsModalVisible;
            ExecutionModal.Toggle();
        }
    }
}

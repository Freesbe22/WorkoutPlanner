using BootstrapBlazor.Components;
using Firebase.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Diagnostics.CodeAnalysis;
using WorkoutPlanner.DataObject;
using WorkoutPlanner.DataObject.Enum;
using WorkoutPlanner.Tools.Services;

namespace WorkoutPlanner.Components.Pages.Workout
{
    partial class WorkoutPlanEditView : ComponentBase
    {
        #region Variables
        [Inject]
        public FirestoreService FirestoreService { get; set; }
        [Inject]
        public FirebaseAuthClient AuthClient { get; set; }
        [Parameter]
        public WorkoutPlan? Program { get; set; }
        [Parameter]
        public EventCallback OnProgramChange { get; set; }

        private bool Initialised { get; set; } = false;
        private bool IsEdit { get; set; } = false;
        private Modal ModalPlan { get; set; }= new Modal();
        private IEnumerable<SelectedItem> Goals { get; set; } = new List<SelectedItem>()
            {
                new SelectedItem(WorkoutPlanGoal.Maintenance.ToString(), "Maintenance"),
                new SelectedItem(WorkoutPlanGoal.WeightLoss.ToString(), "Weight loss"),
                new SelectedItem(WorkoutPlanGoal.MuscleGain.ToString(), "Muscle gain")
            };
        #endregion

        #region Loading
        protected override async Task OnInitializedAsync()
        {
            IsEdit = Program is not null;
            if (!IsEdit)
            {
                Program = new WorkoutPlan()
                {
                    IsPublic = false,
                    UserId = AuthClient.User.Uid
                };
            }

            Initialised = true;
            await InvokeAsync(() => { StateHasChanged(); });

            await base.OnInitializedAsync();
        }
        #endregion

        #region Events
        private void ToggleModal()
        {
            ModalPlan.Toggle();
        }

        private Task OnGoalTypeChanged(SelectedItem item)
        {
            Program.Goal = (WorkoutPlanGoal)Enum.Parse(typeof(WorkoutPlanGoal), item.Value);
            return Task.CompletedTask;
        }

        private async Task OnValidSubmit(EditContext context)
        {
            if (IsEdit)
            {
                await FirestoreService.UpdateObjectReference(FirestoreService.db.Collection(typeof(WorkoutPlan).Name).Document(Program.Id), Program);
            }
            else
            {
                await FirestoreService.db.Collection(typeof(WorkoutPlan).Name).AddAsync(Program);
            }
            await ModalPlan.Close();
            await OnProgramChange.InvokeAsync();
        }
        private async Task OnCancel()
        {
            await ModalPlan.Close();
            await Task.Delay(1);
        }
        #endregion
    }
}

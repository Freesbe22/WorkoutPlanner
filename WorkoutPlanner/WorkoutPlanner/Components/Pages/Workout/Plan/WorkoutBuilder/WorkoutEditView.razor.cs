using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using WorkoutPlanner.Components.Shared.EditModal;
using WorkoutPlanner.DataObject;
using WorkoutPlanner.DataObject.Enum;
using WorkoutPlanner.Tools.Services;

namespace WorkoutPlanner.Components.Pages.Workout.Plan.WorkoutBuilder
{
    partial class WorkoutEditView : ComponentBase
    {
        #region Variables
        [Inject]
        public FirestoreService FirestoreService { get; set; }
        [Parameter]
        [NotNull]
        public ProgramPhase Phase { get; set; }
        [Parameter]
        public DataObject.Workout? Workout { get; set; }
        [Parameter]
        public EventCallback OnWorkoutChange { get; set; }

        private bool Initialised { get; set; } = false;
        private bool IsEdit { get; set; } = true;
        private EditModal ModalWorkout { get; set; }= new EditModal();
        #endregion

        #region Loading
        protected override async Task OnInitializedAsync()
        {

            IsEdit = Workout is not null;
            if (!IsEdit)
            {
                if (Phase.Workouts is null)
                {
                    Phase.Workouts = new List<DataObject.Workout>();
                }

                //Default workout
                Workout = new DataObject.Workout()
                {
                    Sections = new List<WorkoutSectionDetails>()
                    {
                        new WorkoutSectionDetails()
                        {
                            Order =0,
                            Section = WorkoutSection.Warmup
                        },
                        new WorkoutSectionDetails()
                        {
                            Order =1,
                            Section = WorkoutSection.StrengthTraining
                        }
                    }
                };
            }

            Initialised = true;
            await InvokeAsync(() => { StateHasChanged(); });

            await base.OnInitializedAsync();
        }
        #endregion

        #region Events
        private async Task ToggleModal()
        {
            ModalWorkout.Toggle();
            await InvokeAsync(() => { StateHasChanged(); });
        }

        private Task OnWorkoutTypeChanged(CheckboxState state, bool value)
        {
            return Task.CompletedTask;
        }

        private async Task OnValidSubmit(EditContext context)
        {
            if (!IsEdit)
            {
                Phase.Workouts.Add(Workout);
            }
            ModalWorkout.Toggle();
            await OnWorkoutChange.InvokeAsync();
        }

        private async Task OnCancel()
        {
            ModalWorkout.Toggle();
            await InvokeAsync(() => { StateHasChanged(); });
        }
        #endregion
    }
}

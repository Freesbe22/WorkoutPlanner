using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

using WorkoutPlanner.Components.Pages.Execution;
using WorkoutPlanner.DataObject;
using WorkoutPlanner.Tools;
using WorkoutPlanner.Tools.Services;

namespace WorkoutPlanner.Components.Pages.Workout.Plan.WorkoutBuilder
{
    partial class WorkoutListView : ComponentBase
    {
        #region Variables
        [Inject]
        public FirestoreService FirestoreService { get; set; }
        [Parameter]
        [NotNull]
        public ProgramPhase Phase { get; set; }
        [Parameter]
        public EventCallback OnWorkoutChange { get; set; }
        [Parameter]
        public EventCallback<DataObject.Workout> OnWorkoutSelected { get; set; }
        private bool Initialised { get; set; } = false;

        #endregion

        #region Loading
        protected override async Task OnInitializedAsync()
        {
            Initialised = true;

            await base.OnInitializedAsync();
        }

        protected async Task OnWorkoutChanged()
        {
            await OnWorkoutChange.InvokeAsync();
            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async Task OnWorkoutClick (DataObject.Workout workout)
        {
            await OnWorkoutSelected.InvokeAsync(workout);
            await InvokeAsync(() => { StateHasChanged(); });
        }

        #endregion
    }
}

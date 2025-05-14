using Microsoft.AspNetCore.Components;
using WorkoutPlanner.DataObject;
using WorkoutPlanner.Tools;
using WorkoutPlanner.Tools.Services;

namespace WorkoutPlanner.Components.Pages.Workout.Plan.WorkoutBuilder
{
    partial class WorkoutOverviewView : ComponentBase
    {
        #region Variables
        [Inject]
        public FirestoreService FirestoreService { get; set; }
        [Parameter]
        public DataObject.Workout? Workout { get; set; }
        [Parameter]
        public EventCallback OnWorkoutChange { get; set; }
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
        #endregion
    }
}

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Diagnostics.CodeAnalysis;
using WorkoutPlanner.DataObject;
using WorkoutPlanner.Tools;
using WorkoutPlanner.Tools.Services;

namespace WorkoutPlanner.Components.Pages.Plan
{
    partial class PhaseListView : ComponentBase
    {
        #region Variables
        [Inject]
        public FirestoreService FirestoreService { get; set; }
        [Parameter]
        [NotNull]
        public WorkoutPlan Program { get; set; }
        private ProgramPhase? Phase { get; set; }
        private bool Initialised { get; set; } = false;
        private bool IsBackdropOpen { get; set; } = false;
        #endregion

        #region Loading
        protected override async Task OnInitializedAsync()
        {
            await Load();
            Initialised = true;
            await InvokeAsync(() => { StateHasChanged(); });

            await base.OnInitializedAsync();
        }

        private async Task Load()
        {
            Phase = Program.Phases.FirstOrDefault();
        }
        #endregion

        #region Events
        private void SelectPhase(ProgramPhase phase)
        {
            Phase = phase;
        }
        private void EditPhase(ProgramPhase phase)
        {
        }
        private void NewPhase()
        {
        }
        #endregion
    }
}

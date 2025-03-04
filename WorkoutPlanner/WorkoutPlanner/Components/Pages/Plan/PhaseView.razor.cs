using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using WorkoutPlanner.DataObject;
using WorkoutPlanner.DataObject.Enum;
using WorkoutPlanner.Tools.Services;
using static Google.Protobuf.Reflection.FeatureSet.Types;

namespace WorkoutPlanner.Components.Pages.Plan
{
    partial class PhaseView : ComponentBase
    {
        #region Variables
        [Inject]
        public FirestoreService FirestoreService { get; set; }
        [Parameter]
        [NotNull]
        public WorkoutPlan? Program { get; set; }
        private bool Initialised { get; set; } = false;
        private Modal Modal { get; set; }= new Modal();
        private ProgramPhase Phase { get; set; } = new ProgramPhase();
        private IEnumerable<SelectedItem> ScheduleTypes { get; set; }
        #endregion

        #region Loading
        protected override async Task OnInitializedAsync()
        {
            Phase.ProgramId = Program.Id;
            ScheduleTypes = new List<SelectedItem>() 
            { 
                new SelectedItem(ScheduleType.DayNumbers.ToString(), "Numerical (Day 1)"),
                new SelectedItem(ScheduleType.WeekDays.ToString(), "Day of Week (Mon.)")
            };
            Initialised = true;
            await InvokeAsync(() => { StateHasChanged(); });

            await base.OnInitializedAsync();
        }
        #endregion

        #region Events
        private void ToggleModal()
        {
            Modal.Toggle();
        }

        private Task OnScheduleTypeChanged(SelectedItem item)
        {
            Phase.ScheduleType = (ScheduleType)Enum.Parse(typeof(ScheduleType), item.Value);
            return Task.CompletedTask;
        }
        private async Task OnValidSubmit(EditContext context)
        {
            Program.Phases.Add(Phase);
            await FirestoreService.UpdateObjectReference(FirestoreService.db.Collection(typeof(WorkoutPlan).Name).Document(Program.Id),Program);
            Modal.Close();
        }
        private async Task OnCancel()
        {
            Modal.Close();
            await Task.Delay(1);
        }
        #endregion
    }
}

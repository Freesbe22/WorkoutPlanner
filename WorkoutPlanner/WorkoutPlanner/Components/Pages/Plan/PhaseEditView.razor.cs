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
    partial class PhaseEditView : ComponentBase
    {
        #region Variables
        [Inject]
        public FirestoreService FirestoreService { get; set; }
        [Parameter]
        [NotNull]
        public WorkoutPlan? Program { get; set; }
        [Parameter]
        public ProgramPhase? Phase { get; set; }
        [Parameter]
        public EventCallback OnPhaseChange { get; set; }

        private bool Initialised { get; set; } = false;
        private bool IsEdit { get; set; } = true;
        private Modal ModalPhase { get; set; }= new Modal();
        private IEnumerable<SelectedItem> ScheduleTypes { get; set; }
        #endregion

        #region Loading
        protected override async Task OnInitializedAsync()
        {
            ScheduleTypes = new List<SelectedItem>() 
            { 
                new SelectedItem(ScheduleType.WeekDays.ToString(), "Day of Week (Mon.)"),
                new SelectedItem(ScheduleType.DayNumbers.ToString(), "Numerical (Day 1)")
            };


            IsEdit = Phase is not null;
            if (!IsEdit)
            {
                Phase = new ProgramPhase() { ProgramId = Program.Id };
            }

            Initialised = true;
            await InvokeAsync(() => { StateHasChanged(); });

            await base.OnInitializedAsync();
        }
        #endregion

        #region Events
        private void ToggleModal()
        {
            ModalPhase.Toggle();
        }

        private Task OnScheduleTypeChanged(SelectedItem item)
        {
            Phase.ScheduleType = (ScheduleType)Enum.Parse(typeof(ScheduleType), item.Value);
            return Task.CompletedTask;
        }
        private async Task OnValidSubmit(EditContext context)
        {
            if (!IsEdit)
            {
                Program.Phases.Add(Phase);
            }
            await FirestoreService.UpdateObjectReference(FirestoreService.db.Collection(typeof(WorkoutPlan).Name).Document(Program.Id),Program);
            ModalPhase.Close();
            OnPhaseChange.InvokeAsync();
        }
        private async Task OnCancel()
        {
            ModalPhase.Close();
            await Task.Delay(1);
        }
        #endregion
    }
}

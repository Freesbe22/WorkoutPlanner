using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;
using WorkoutPlanner.DataObject;
using WorkoutPlanner.DataObject.Enum;
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

            await base.OnInitializedAsync();
        }

        private async Task Load()
        {
            await SelectPhase("041aHhb71TqSs0aTA2de");
        }

        public async Task<IEnumerable<UserProgress>> GetLastWorkouts(string userId)
        {
            //int maxWorkoutProgram = 0;
            //foreach (var phase in Program.Phases)
            //{
            //    try
            //    {
            //        maxWorkoutProgram += phase.Workouts.Where(
            //            workout => workout.Type == WorkoutType.Normal)
            //            .ToList().Count * phase.Cycle;
            //    }
            //    catch (Exception)
            //    {
            //        //Nothing
            //    }
            //}

            //var progressRef = FirestoreService.db.Collection(typeof(UserProgress).Name)
            //    .WhereEqualTo("UserId", userId)
            //    .OrderBy("Date")
            //    .Limit(maxWorkoutProgram);

            //var snapshot = await progressRef.GetSnapshotAsync();
            //return snapshot.ToList().Select(firebaseObject => firebaseObject.ConvertTo<UserProgress>());

            return new List<UserProgress>().AsEnumerable();
        }

        public async Task SelectPhase(string userId)
        {
            try
            {
                var workouts = await GetLastWorkouts(userId);
                var workoutsOfProgram = workouts.Where(workout => workout.WorkoutPlanId == Program.Id).ToList();

                var phasesWithWorkout = Program.Phases.Where(phase => phase.Workouts != null).ToList();
                var lastPhase = new ProgramPhase();
                var maxWorkoutsOfPhase = 0;
                var workoutFinishedInPhase = 0;
                try
                {
                    lastPhase = phasesWithWorkout.Where(phase => phase.Workouts.Where(workout => workout.Id.Equals(workoutsOfProgram.First().WorkoutId)).ToList().Count > 0).First();
                    maxWorkoutsOfPhase = lastPhase.Workouts.Where(workout => workout.Type == WorkoutType.Normal).ToList().Count * lastPhase.Cycle;

                    lastPhase.Workouts.ForEach(workout => workoutFinishedInPhase += workoutsOfProgram.Where(workoutProgram => workoutProgram.WorkoutId.Equals(workout.Id)).ToList().Count);
                }
                catch (Exception)
                {
                    lastPhase = Program.Phases.OrderBy(phase => phase.Order).First();
                }

                if (maxWorkoutsOfPhase <= workoutFinishedInPhase)
                {
                    var nextPhase = Program.Phases.Where(phase => phase.Order == lastPhase.Order + 1).FirstOrDefault();

                    if (nextPhase != null)
                    {
                        Phase = nextPhase;
                    }
                    else
                    {
                        Phase = Program.Phases.OrderBy(phase => phase.Order).FirstOrDefault();
                    }
                }
                else
                {
                    Phase = lastPhase;
                }
            }
            catch (Exception)
            {
                Phase = null;
            }
        }
        #endregion

        #region Events
        private void SelectPhase(ProgramPhase phase)
        {
            Phase = phase;
        }
        #endregion
    }
}

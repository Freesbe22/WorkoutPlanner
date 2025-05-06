using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;
using WorkoutPlanner.DataObject;
using WorkoutPlanner.DataObject.Enum;
using WorkoutPlanner.Components.Shared.SwipeArea;
using WorkoutPlanner.Tools.Services;
using SwipeDirection = WorkoutPlanner.Components.Shared.SwipeArea.SwipeDirection;

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
        [Parameter]
        public ProgramPhase? Phase { get; set; }
        [Parameter]
        public EventCallback OnPhaseChange { get; set; }
        public Workout? Workout { get; set; }
        private bool Initialised { get; set; } = false;
        private bool IsWorkoutOverview { get; set; } = false;
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
            await SelectPhase(Program.UserId);
            //Workout = Phase.Workouts.FirstOrDefault(workout => workout.IsRest == false);
            Workout = Phase.Workouts.FirstOrDefault();
        }

        protected async Task OnWorkoutSelected(Workout workout)
        {
            Workout = workout;
            IsWorkoutOverview = true;
            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async Task OnPhaseChanged()
        {
            await OnPhaseChange.InvokeAsync();
            await SelectPhase(Program.UserId);
            await InvokeAsync(() => { StateHasChanged(); });
        }

        public async Task<IEnumerable<UserProgress>> GetLastWorkouts(string userId)
        {
            int maxWorkoutProgram = 0;
            foreach (var phase in Program.Phases)
            {
                try
                {
                    maxWorkoutProgram += phase.Workouts.Count * phase.Cycle;
                }
                catch (Exception)
                {
                    //Nothing
                }
            }

            try
            {
                var progressRef = FirestoreService.db.Collection(typeof(UserProgress).Name)
                        .WhereEqualTo("UserId", userId)
                        .OrderBy("Date")
                        .Limit(maxWorkoutProgram);

                var snapshot = await progressRef.GetSnapshotAsync();
                return snapshot.ToList().Select(firebaseObject => firebaseObject.ConvertTo<UserProgress>());
            }
            catch (Exception)
            {
                return new List<UserProgress>();
            }
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
                    maxWorkoutsOfPhase = lastPhase.Workouts.Count * lastPhase.Cycle;

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

        public void WorkoutOverviewTogle(bool isWorkoutOverview)
        {
                IsWorkoutOverview = isWorkoutOverview;
                StateHasChanged();
        }

        public void OnSwipeEnd(SwipeEventArgs e)
        {
            if (e.SwipeDirection == SwipeDirection.LeftToRight && IsWorkoutOverview)
            {
                WorkoutOverviewTogle(false);
            }
            else if (e.SwipeDirection == SwipeDirection.RightToLeft && !IsWorkoutOverview)
            {
                WorkoutOverviewTogle(true);
            }
        }

        public void OnNavClicked(bool isWorkoutOverview)
        {
            WorkoutOverviewTogle(isWorkoutOverview);
        }

        #endregion
    }
}

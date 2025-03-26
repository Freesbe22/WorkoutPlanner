using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WorkoutPlanner.DataObject.Enum;

namespace WorkoutPlanner.Tools.Services
{
    public class FirestoreService
    {
        public FirestoreDb db;

        public FirestoreService()
        {
            SetupFirestore();
        }

        private async Task SetupFirestore()
        {
            if (db == null)
            {
                db = new FirestoreDbBuilder
                {
                    ProjectId = "thedreamlife-workoutplanner",

                    ConverterRegistry = new ConverterRegistry
                    {
                        new EnumJsonConverter<WorkoutPlanGoal>(),
                        new EnumJsonConverter<ScheduleType>(),
                    },
                    JsonCredentials = await Secret.FirestoreCredentialsAsync()
                }.Build();
            }
        }

        public async Task UpdateObjectReference(DocumentReference doc, object obj)
        {
            var updates = new Dictionary<string, object>();
            foreach(var prop in obj.GetType().GetProperties())
            {
                updates.Add(prop.Name, prop.GetValue(obj));
            }
            await doc.UpdateAsync(updates);
        }
    }

    public class EnumJsonConverter<T> : IFirestoreConverter<T> where T : Enum
    {
        public object ToFirestore(T value) => value.ToString();

        public T FromFirestore(object value)
        {
            return (T)Enum.Parse(typeof(T), (string)value);
            throw new ArgumentException("Invalid value");
        }
    }
    public class WorkoutPlanGoalConverter : IFirestoreConverter<WorkoutPlanGoal>
    {
        public object ToFirestore(WorkoutPlanGoal value) => value.ToString();

        public WorkoutPlanGoal FromFirestore(object value)
        {
            return (WorkoutPlanGoal)Enum.Parse(typeof(WorkoutPlanGoal), (string)value);
            throw new ArgumentException("Invalid value");
        }
    }
    public class ScheduleTypeConverter : IFirestoreConverter<ScheduleType>
    {
        public object ToFirestore(ScheduleType value) => value.ToString();

        public ScheduleType FromFirestore(object value)
        {
            return (ScheduleType)Enum.Parse(typeof(ScheduleType), (string)value);
            throw new ArgumentException("Invalid value");
        }
    }
}

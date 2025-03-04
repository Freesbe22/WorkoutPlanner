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
                var stream = await FileSystem.OpenAppPackageFileAsync("thedreamlife-workoutplanner-firebase-adminsdk-fbsvc-2b161b85ab.json");
                var reader = new StreamReader(stream);
                var contents = reader.ReadToEnd();

                db = new FirestoreDbBuilder
                {
                    ProjectId = "thedreamlife-workoutplanner",

                    ConverterRegistry = new ConverterRegistry
                    {
                        new EnumJsonConverter<WorkoutPlanGoal>(),
                    },
                    JsonCredentials = contents
                }.Build();
            }
        }
        //public async Task InsertSampleModel(SampleModel sample)
        //{
        //    await SetupFirestore();
        //    await db.Collection("SampleModels").AddAsync(sample);
        //}
        //public async Task<List<SampleModel>> GetSampleModels()
        //{
        //    await SetupFirestore();
        //    var data = await db
        //                    .Collection("SampleModels")
        //                    .GetSnapshotAsync();
        //    var sampleModels = data.Documents
        //        .Select(doc =>
        //        {
        //            var sampleModel = doc.ConvertTo<SampleModel>();
        //            sampleModel.Id = doc.Id; // FirebaseId hinzufügen
        //            return sampleModel;
        //        })
        //        .ToList();
        //    return sampleModels;
        //}
    }
    //public class EnumJsonConverter<T> : JsonConverter<T> where T : Enum
    //{
    //    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    //        => (T)Enum.Parse(typeof(T), reader.GetString());

    //    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    //        => writer.WriteStringValue(value.ToString());
    //}
    public class EnumJsonConverter<T> : IFirestoreConverter<T> where T : Enum
    {
        public object ToFirestore(T value) => (T)Enum.Parse(typeof(T), value.ToString());

        public T FromFirestore(object value)
        {
            return (T)Enum.Parse(typeof(T), (string)value);
            throw new ArgumentException("Invalid value");
        }
    }
}

namespace WorkoutPlanner.Tools
{
    class DBHelper
    {
        private static bool IsNameSpaceValid<T>(T data)
        {
            return IsNameSpaceValid(data.GetType());
        }
        private static bool IsNameSpaceValid(Type type)
        {
            return type.Namespace.Equals("WorkoutPlanner.DataObject");
        }

        //public static async void Post<T>(FirebaseClient firebaseClient, T data)
        //{
        //    if (IsNameSpaceValid(data))
        //    {
        //        var idProperty = typeof(T).GetProperty("Id");
        //        if (idProperty != null)
        //        {
        //            idProperty.SetValue(data, (await firebaseClient.Child(data.GetType().Name).PostAsync(data)).Key); // Assigner l'ID de Firebase lors de la création
        //        }
        //    }
        //    else throw new Exception("Unhandled Data Object");
        //}

        //public static async Task<List<T>> GetDataListAsync<T>(FirebaseClient firebaseClient, T data) 
        //{
        //    if (IsNameSpaceValid(typeof(T)))
        //    {
        //        var firebaseObjects = await firebaseClient.Child(typeof(T).Name).OnceAsync<T>();

        //        return firebaseObjects.Select(item =>
        //        {
        //            var obj = item.Object;
        //            var idProperty = typeof(T).GetProperty("Id");
        //            if (idProperty != null)
        //            {
        //                idProperty.SetValue(obj, item.Key); // Assigner l'ID depuis Firebase
        //            }
        //            return obj;
        //        }).ToList();
        //    }
        //    else throw new Exception("Unhandled Data Object");
        //}
        
        //public static List<T> FirebaseObjectsToList<T>(IReadOnlyCollection<FirebaseObject<T>> firebaseObjects)
        //{
        //    return firebaseObjects.Select(item =>
        //    {
        //        var obj = item.Object;
        //        var idProperty = typeof(T).GetProperty("Id");
        //        if (idProperty != null)
        //        {
        //            idProperty.SetValue(obj, item.Key); // Assigner l'ID depuis Firebase
        //        }
        //        return obj;
        //    }).ToList();
        //}

        //public static async Task FillObservablesAsync<T>(FirebaseClient firebaseClient, ObservableCollection<T> datas)
        //{
        //    if (IsNameSpaceValid(typeof(T)))
        //    {
        //        //        var tmp = firebaseClient
        //        //.Child(typeof(T).Name).OnceAsListAsync<T>();
        //        //        tmp.Wait();
        //        //        datas = (ObservableCollection<T>)(tmp.Result).ToObservable(); 
        //        var tmp = firebaseClient
        //.Child(typeof(T).Name)
        //        .AsObservable<T>()
        //        .Subscribe((item) =>
        //        {
        //            if (item.Object != null)
        //            {
        //                datas.Add(item.Object);
        //            }
        //        });
        //        tmp.
        //    }
        //    else throw new Exception("Unhandled Data Object");
        //}
    }
}

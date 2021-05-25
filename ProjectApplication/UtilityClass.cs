using ProjectApplication.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApplication
{
    static class UtilityClass<T>
    {
        public static void Delete(ObservableCollection<T> observableCollection, ProjectApplicationContext Ctx, T item)
        {
            
            observableCollection.Remove(item);
            Ctx.SaveChanges();
        }
    }
}

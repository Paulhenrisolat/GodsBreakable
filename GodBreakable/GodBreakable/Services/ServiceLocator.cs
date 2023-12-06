using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodBreakable
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> listServices = new Dictionary<Type, object>();

        public static void RegisterService<T>(T service)
        {
            listServices[typeof(T)] = service;
        }

        public static T GetService<T>()
        {
            return (T)listServices[typeof(T)];
        }

        public static Int32 Count
        {
            get { return listServices.Count; }
        }
    }
}

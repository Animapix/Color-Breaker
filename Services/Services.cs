using System;
using System.Collections.Generic;

namespace Animapix
{
    public static class Services
    {
        private static readonly Dictionary<Type, object> _listeServices = new Dictionary<Type, object>();

        public static void RegisterService<T>(T service)
        {
            _listeServices[typeof(T)] = service;
        }

        public static T Get<T>()
        {
            return (T)_listeServices[typeof(T)];
        }
    }
}

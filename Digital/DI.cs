using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace Digital
{

    public class DI
    {
        public void Set<Interface, Q>(Func<Interface> func) where Q:Interface
        {
            var type = typeof(Interface);
            if (dict.ContainsKey(type)) dict.Remove(type, out var xxx);
            dict.TryAdd(type, func as Func<object>);
        }
        public void Set<Interface, Q>() where Q : Interface, new()
        {
            Func<Interface> action;
            lock (obj)
                action = () => new Q();
            var type = typeof(Interface);
            if (dict.ContainsKey(type)) dict.Remove(type, out var xxx);
                dict.TryAdd(type, action as Func<object>);
        }
        public Interface Get<Interface>()
        {
            if (!dict.TryGetValue(typeof(Interface), out var value))
                throw new Exception("Type not found");
            lock (obj)
                return (value as Func<Interface>)();
        }
        ConcurrentDictionary<Type, Func<object>> dict;
        object obj;
        public DI()
        {
            this.obj = new object();
            dict = new ConcurrentDictionary<Type, Func<object>>();
        }
    }
}

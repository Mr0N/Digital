using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;

namespace Digital
{

    public class DI:IDisposable
    {
        private void CheckRemove(Type type)
        {
            if (dict.ContainsKey(type)) dict.Remove(type, out var xxx);
            if (objDict.ContainsKey(type)) dict.Remove(type, out var uuu);
        }
        public Config Set<Interface, Q>(Func<Interface> func) where Q : Interface
        {
            Action la = new Action(() =>
            {
                var type = typeof(Interface);
                CheckRemove(type);
                dict.TryAdd(type, func as Func<object>);
                
            });
           Action laSet = new Action(() =>
            {
                var type = typeof(Interface);
                CheckRemove(type);
                objDict.TryAdd(type, func());
                
            });
            return new Config(this, la, laSet);
        }
        public Config Set<Interface, Q>() where Q : Interface, new()
        {
            Action la = new Action(() =>
            {
                Func<Interface> action;
                lock (obj)
                    action = () => new Q();
                var type = typeof(Interface);
                CheckRemove(type);
                dict.TryAdd(type, action as Func<object>); ;

            });
            Action laSet = new Action(() =>
            {
                var type = typeof(Interface);
                CheckRemove(type);
                objDict.TryAdd(type, new Q());
            });
            return new Config(this, la, laSet);
        }
        public Interface Get<Interface>()
        {
            bool val = dict.TryGetValue(typeof(Interface), out var value);
            bool objCheck = this.objDict.TryGetValue(typeof(Interface), out var ObjValue);
            if (!(val || objCheck))
                throw new Exception("Type not found");
            if (value != null)
            {
                lock (obj)
                    return (value as Func<Interface>)();
            }
            else if (ObjValue != null)
                return (Interface)ObjValue;
            else
                throw new Exception("Type not found");
        }

        public void Dispose()
        {
            if (objDict == null) return;
            foreach (var item in objDict)
            {
                var val = item.Value.GetType()
                     .GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public)
                    .Where(a => a.Name == "Dispose");
                foreach (var xxx in val)
                {
                    xxx.Invoke(item.Value,null);
                }  
            }
        }

        ConcurrentDictionary<Type, Func<object>> dict;
        internal ConcurrentDictionary<Type, object> objDict;
        object obj;
        public DI()
        {
            this.obj = new object();
            dict = new ConcurrentDictionary<Type, Func<object>>();
            this.objDict = new ConcurrentDictionary<Type, object>();
        }
    }
}

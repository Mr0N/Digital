using Digital;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Test
{
    interface IXXX
    {
        public string Message();
    }
    class XXX : IXXX
    {
        public string Message()
        {
            return fields;
        }
        string fields;
        public XXX(string one)
        {
            this.fields = one;
        }
    }

    interface IYYY
    {
        public string Message();
    }
    class YYY : IYYY,IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine("DISPOSE!!!!");
        }

        public string Message()
        {
            return "12345678";
        }
    }
   
    class Program
    {
        static void Main()
        {
            Start();
            Console.ReadKey();
        }
        static void Start()
        {
           using var di = new DI()
                  .Set<IYYY, YYY>()
                  .NotCreateObject()
                  .Build();
             
            Console.WriteLine("Start");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(di.Get<IYYY>().GetHashCode());
            }
            
        }
    }
}

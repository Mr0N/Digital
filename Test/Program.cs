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
            var di = new DI()
                    .Set<IXXX, XXX>(() => new XXX("dsicdsijcsdiocdjsicweo;vweo"))
                    .NotCreateObject()
                    .Build();

            Work(di);
            Console.ReadKey();
        }
        static void Work(DI dI)
        {
            Console.WriteLine(dI.Get<IXXX>().Message());
        }
    }
}

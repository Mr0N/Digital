using Digital;
using System;

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
    class YYY : IYYY
    {
        public string Message()
        {
            return "12345678";
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var di = new DI().Set<IXXX, XXX>(() => new XXX("HI!"))
                  .NotCreateObject()
                  .Build();
            di.Set<IYYY, YYY>().CreateObject().Build();
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(di.Get<IYYY>().GetHashCode());
            }
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(di.Get<IXXX>().GetHashCode());
            }
            Console.ReadKey();
        }
    }
}

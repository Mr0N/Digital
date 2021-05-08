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
            DI di = new DI();
            di.Set<IXXX, XXX>(()=>new XXX("HI!"));
            Console.WriteLine(di.Get<IXXX>().Message());
            di.Set<IYYY, YYY>();
            Console.WriteLine(di.Get<IYYY>().Message());
            Console.ReadKey();
            
        }
    }
}

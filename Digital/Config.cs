using Digital.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital
{
    public class Config: ConfigAbstract
    {
        private Config()
        {

        }
        public override DI Build()
        {
            if (this.CreateNewObject)
            {
                la();
            }
            else
            {
                laObj();
            }
            return dI;
        }
        public Config CreateObject()
        {
            this.CreateNewObject = true;
            return this;
        }
        public Config NotCreateObject()
        {
            this.CreateNewObject = false;
            return this;
        }

        private bool CreateNewObject { set; get; }

        DI dI;
        Action la;
        Action laObj;
        internal Config(DI dI, Action la,Action laObj)
        {
            this.dI = dI;
            this.la = la;
            this.laObj = laObj;
        }
    }
}

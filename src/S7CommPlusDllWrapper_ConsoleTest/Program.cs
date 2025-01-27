using System;
using System.Data;
using S7CommPlusDllWrapper;

namespace S7CommPlusDllWrapper_ConsoleTest
{

    

    public class Program
    {
        private DllWrapper wrapper;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            DllWrapper wrapper = new DllWrapper();

            object ob =  wrapper.CallFunction("createConnectionObject");
            Console.WriteLine(ob);

            object ob1 =  wrapper.CallFunction("initiateConnection");
            Console.WriteLine(ob1.ToString());

            object ob2 =   wrapper.CallFunction("getDataBlockInfoList");
            Console.WriteLine(ob2.ToString());
            
        }
    }
}

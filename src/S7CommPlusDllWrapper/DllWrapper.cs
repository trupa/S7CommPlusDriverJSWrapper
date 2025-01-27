using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S7CommPlusDriver;
using S7CommPlusDriver.ClientApi;
using System.Reflection;
using System.Dynamic;

namespace S7CommPlusDllWrapper
{
    public class DllWrapper
    {

        //private static string dllPath = @"..\S7CommPlusDriver\bin\x64\Debug\S7CommPlusDriver.dll";
        //private static Assembly S7CommPlusAssembly = Assembly.LoadFrom(dllPath);

        //private static Type S7CommPlusConnection_Type = S7CommPlusAssembly.GetType("S7CommPlusDriver.S7CommPlusConnection");
        //private static Type S7CommClientAPI_Type = S7CommPlusAssembly.GetType("S7CommPlusDriver.ClientAPI");
        //private static Type DatablockInfo_Type = S7CommPlusAssembly.GetType("S7CommPlusDriver.S7CommPlusConnection+DatablockInfo");
        //private static Type PlcTag_Type = S7CommPlusAssembly.GetType("S7CommPlusDriver.ClientAPI+PLCTag");
        //private static Type DatablockInfoList_Type = typeof(List<>).MakeGenericType(DatablockInfo_Type);

        //private static MethodInfo S7CommPlusConnection_Method_Connect = S7CommPlusConnection_Type.GetMethod("Connect");
        //private static MethodInfo S7CommPlusConnection_Method_GetListOfDatablocks = S7CommPlusConnection_Type.GetMethod("GetListOfDatablocks");
        //private static MethodInfo S7CommPlusConnection_Method_GetTagBySymbol = S7CommPlusConnection_Type.GetMethod("getPlcTagBySymbol");



        //private object conn = null;
        private S7CommPlusConnection conn = null;
        //private object dbInfoList = null;
        private List<S7CommPlusConnection.DatablockInfo> dbInfoList = null;
        //private object tag = null;
        private PlcTag tag = null;



        public async Task<object> CallFunction(dynamic input)
        {
            string command = (string)input.command;

            if (command == "createConnectionObject")
            {
                //System.Console.WriteLine("Creating S7CommPlusConnection object...");

                //conn = Activator.CreateInstance(S7CommPlusConnection_Type);
                //return conn;

                //conn = await Task.Run(() => new S7CommPlusConnection());
                conn = new S7CommPlusConnection();

                return conn;
            }
            else if (command == "initiateConnection")
            {
                System.Console.WriteLine("Initiating Connection...");

                string ipAddress = (string)input.IPaddress;
                string password = (string)input.password;
                int timeout = (int)input.timeout;
                //int connectionResult = (int)S7CommPlusConnection_Method_Connect.Invoke(conn, new object[] { ipAddress, password, timeout });

                //int connectionResult = await Task.Run(() => conn.Connect(ipAddress, password, timeout));

                int connectionResult = conn.Connect(ipAddress, password, timeout);

                if (connectionResult != 0)
                {
                    return "Error";
                }
                return "successful";


            }
            else if (command == "getDataBlockInfoList")
            {
                System.Console.WriteLine("Getting DataBlockInfo List...");

                //dbInfoList = Activator.CreateInstance(DatablockInfoList_Type);
                //object[] parameters1 = new object[] { dbInfoList };


                //int dataBlockListAccessResult = (int)S7CommPlusConnection_Method_GetListOfDatablocks.Invoke(conn, parameters1);
                //int dataBlockListAccessResult = await Task.Run(() => conn.GetListOfDatablocks(out dbInfoList));

                int dataBlockListAccessResult = conn.GetListOfDatablocks(out dbInfoList);

                if (dataBlockListAccessResult != 0)
                {
                    return "Error";
                }                

                return dbInfoList;
            }
            else if (command == "readVariable")
            {

                System.Console.WriteLine("loading tag ...");

                //PlcTag  tag2 = (PlcTag)Activator.CreateInstance(PlcTag_Type);


                //tag = S7CommPlusConnection_Method_GetTagBySymbol.Invoke(conn, new object[] { input.tagSymbol });

                
                PlcTag tag = conn.getPlcTagBySymbol(input.tagSymbol);
                
                if (tag == null) return null;             

                PlcTags tags = new PlcTags();
                tags.AddTag(tag);
                if (tags.ReadTags(conn) != 0) return null;


                

                return tag;
            }

            return null;
        }
    }
}
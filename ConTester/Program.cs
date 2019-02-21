using InkDraw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConTester
{
    class Program
    {
        static void Main(string[] args)
        {
            string ipAddr = "192.168.232.113";//"10.46.232.93";
            int portNum = 6020;

            try
            {
                InkDrawCmd idc = new InkDrawCmd(ipAddr, portNum);
                idc.Grade = "TR564";
                idc.Color = "INP";
                idc.LotNo = "E18Z05J-01";
                idc.SetPrintOrder();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            string a = "TEXT:Obj Inkdraw ver 1.13.148#TEXT: (c)2000 - 2011 HSA Systems#TEXT: http://www.hsasystems.com#RESULT: Transmission OK (0)#";
            string b = "TEXT:Obj Inkdraw ver 1.13.148#TEXT:(c) 2000-2011 HSA Systems#TEXT:http://www.hsasystems.com#RESULT:COMMAND: F: File not found (103)#";

            //InkDrawErrorCode idec = new InkDrawErrorCode();
            //Console.WriteLine(idec.ErrorMessage("103"));

            //Regex reg = new Regex(@"\((.+)\)");
            //MatchCollection resultColl = reg.Matches(a);


            //string r = a.Split('#').Last();

            //string r = b.Replace("(c)", "");
            //Console.WriteLine(r);

            //Console.WriteLine();

            //string x = a.Replace("(c)", "");
            //int ck = x.IndexOf('(');
            //string errorCode = x.Replace(x.Substring(0, ck), "").Replace("(","").Replace(")#","");

            //Console.WriteLine(errorCode);
        }





    }
}

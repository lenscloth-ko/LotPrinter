using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using InkDraw;
//using DynamicSDK;

namespace ConTester
{
    class Program
    {
        static void Main(string[] args)
        {
            string ipAddr = "10.46.232.93";
            int portNum = 6020;

            try
            {
                //mesOrderTable.Rows.Add(new object[] { "1", "TR654", "INP", "E18Z05J-01", "1", "40", "N", "" });
                //mesOrderTable.Rows.Add(new object[] { "1", "DP270E", "NP", "418X0160", "1", "50", "N", "" });
                //mesOrderTable.Rows.Add(new object[] { "1", "L1912", "WS8Q766", "Z18825K-01", "1", "40", "N", "" });
                //mesOrderTable.Rows.Add(new object[] { "1", "XR404", "G9211", "Z18910D-09", "1", "40", "N", "" });

                InkDrawCmd idc = new InkDrawCmd(ipAddr, portNum);
                idc.Grade = "DP270E";
                idc.Color = "NP";
                idc.LotNo = "418X0160";
                idc.SerialStartNum = 1;
                idc.SerialEndNum = 12;
                idc.BagStartNum = 71;
                idc.BagEndNum = 90;
                Console.WriteLine("X : " + idc.SetPrintOrder());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        } // end Main(string[] args)

    }
}

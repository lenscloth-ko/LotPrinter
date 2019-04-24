using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using InkDraw;
using System.Data;
//using DynamicSDK;

namespace ConTester
{
    class Program
    {
        static void Main(string[] args)
        {
            #region InkDraw 정보전달
            //string ipAddr = "10.46.232.93"; //"192.168.0.2";
            //int portNum = 6020;//2000;

            //try
            //{
            //    InkDrawCmd idc = new InkDrawCmd(ipAddr, portNum);
            //    idc.Grade = "1ABCDEFGHIJKLMN";
            //    idc.Color = "2ABCDEFGHIJKLMN";
            //    idc.LotNo = "3ABCDE-";
            //    idc.SerialStartNum = 5;
            //    idc.SerialEndNum = 12;
            //    idc.BagStartNum = 1;
            //    idc.BagEndNum = 10;
            //    Console.WriteLine("X : " + idc.SetPrintOrder());
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            #endregion

            #region InkDraw 로그저장테스트
            //string DBPath = @"C:\Inkdraw\Logfile\printlog_test.mdb";
            //string sqlString = @"select ID, Timestamp, CB, File, Datafile, Record from printlog";

            //InkDrawPrintLog plog = new InkDrawPrintLog();
            //DataSet ds = plog.GetPrintLog(sqlString, DBPath);

            ////Console.WriteLine(ds.Tables[0].Rows.Count);
            //Console.WriteLine("InkDraw print log DataTable");
            //Console.WriteLine("|ID|\t|Timestamp|\t|CB|\t|File|\t|Datafile|\t|Record|");

            //foreach (DataRow row in ds.Tables[0].Rows)
            //{
            //    Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}"
            //        , row["ID"] // ID
            //        , row["Timestamp"] // Timestamp
            //        , row["CB"] // CB
            //        , row["File"].ToString().Substring(0, 10) // File
            //        , row["Datafile"] // Datafile
            //        , row["Record"] // Record
            //    );
            //} // end foreach
            #endregion

            #region CoLOS 정보전달

            #endregion


        } // end Main(string[] args)

    } // end class Program

} // end namespace

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InkDraw
{
    public class InkDrawPrintLog
    {
        public DataSet GetPrintLog(string sql, string DB_path)
        {

            string connStr = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DB_path + ";Jet OLEDB:Database Password=";

            OleDbConnection conn = new System.Data.OleDb.OleDbConnection(connStr);
            DataSet ds = new DataSet();
            OleDbDataAdapter adp = new OleDbDataAdapter(sql, conn);
            adp.Fill(ds);
            return ds;

        } // end GetPrintLog(string sql, string DB_path)

    }
}

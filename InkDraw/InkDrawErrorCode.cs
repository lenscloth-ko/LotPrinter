using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InkDraw
{
    /// <summary>
    /// 리턴받은 에러코드로 메세지 확인
    /// InkDrawErrorCode idec = new InkDrawErrorCode();
    /// Console.WriteLine(idec.ErrorMessage("103"));
    /// </summary>
    public class InkDrawErrorCode
    {
        /// <summary>
        /// DataTable
        /// </summary>
        private DataTable errorCodesTable = new DataTable("errorCodesTable");

        public InkDrawErrorCode()
        {
            initTable();
        } // end InkDrawErrorCode()

        /// <summary>
        /// 에러 코드의 내용 확인
        /// </summary>
        /// <param name="errorNum">에러번호 문자열</param>
        /// <returns>에러 메세지 내용</returns>
        public string ErrorMessage(string errorNum)
        {
            DataRow[] rows;
            rows = this.errorCodesTable.Select(
                string.Format("Code = '{0}'", errorNum)
                );

            return rows[0]["Message"].ToString();
        } // end ErrorMessage(string errorNum)

        /// <summary>
        /// memory init
        /// </summary>
        private void initTable()
        {
            //대소문자 구분여부 = true
            this.errorCodesTable.CaseSensitive = true;

            //항목
            this.errorCodesTable.Columns.Add("Code");
            this.errorCodesTable.Columns.Add("Message");

            //Row
            this.errorCodesTable.Rows.Add(new object[] { "0", "OK" });
            this.errorCodesTable.Rows.Add(new object[] { "1", "Wrong password" });
            this.errorCodesTable.Rows.Add(new object[] { "2", "Unknown command" });
            this.errorCodesTable.Rows.Add(new object[] { "10", "Password OK" });
            this.errorCodesTable.Rows.Add(new object[] { "020", "Purge done" });
            this.errorCodesTable.Rows.Add(new object[] { "021", "Purge started" });
            this.errorCodesTable.Rows.Add(new object[] { "030", "Calibration performed" });
            this.errorCodesTable.Rows.Add(new object[] { "100", "COMMAND: Unknown command" });
            this.errorCodesTable.Rows.Add(new object[] { "101", "COMMAND: S: Printer is not running" });
            this.errorCodesTable.Rows.Add(new object[] { "102", "COMMAND: R: Printer is already running" });
            this.errorCodesTable.Rows.Add(new object[] { "103", "COMMAND: F: File not found" });
            this.errorCodesTable.Rows.Add(new object[] { "104", "COMMAND: G / A: Database not active" });
            this.errorCodesTable.Rows.Add(new object[] { "105", "COMMAND: Illegal data, a number was expected" });
            this.errorCodesTable.Rows.Add(new object[] { "106", "COMMAND: S: Cannot stop printer" });
            this.errorCodesTable.Rows.Add(new object[] { "107", "COMMAND: R: Printer is not running" });
            this.errorCodesTable.Rows.Add(new object[] { "108", "COMMAND: F: Too many open windows" });
            this.errorCodesTable.Rows.Add(new object[] { "109", "COMMAND: P: Not in user-managed buffer" });
            this.errorCodesTable.Rows.Add(new object[] { "110", "COMMAND: DATABASE: Record not found" });
            this.errorCodesTable.Rows.Add(new object[] { "120", "COMMAND: calibration works only on HP heads" });
            this.errorCodesTable.Rows.Add(new object[] { "121", "COMMAND: calibration failed" });
            this.errorCodesTable.Rows.Add(new object[] { "130", "COMMAND: C: Cannot close main window" });
            this.errorCodesTable.Rows.Add(new object[] { "131", "COMMAND: C: Cannot print window" });
            this.errorCodesTable.Rows.Add(new object[] { "200", "REQUEST: Unknown command" });
            this.errorCodesTable.Rows.Add(new object[] { "210", "REQUEST: CONNECT: Message not found" });
            this.errorCodesTable.Rows.Add(new object[] { "211", "REQUEST: CONNECT: Message in use" });
            this.errorCodesTable.Rows.Add(new object[] { "220", "REQUEST: OBJECT DATA: Object not found" });
            this.errorCodesTable.Rows.Add(new object[] { "230", "REQUEST: VELOCITY: Not in print mode" });
            this.errorCodesTable.Rows.Add(new object[] { "300", "OBJECT: Object not found" });
            this.errorCodesTable.Rows.Add(new object[] { "301", "OBJECT: Unknown command" });
            this.errorCodesTable.Rows.Add(new object[] { "302", "OBJECT: Invalid object type (server error!)" });
            this.errorCodesTable.Rows.Add(new object[] { "310", "OBJECT: POSITION: Invalid X value" });
            this.errorCodesTable.Rows.Add(new object[] { "311", "OBJECT: POSITION: Invalid Y value" });
            this.errorCodesTable.Rows.Add(new object[] { "312", "OBJECT: POSITION: X value missing" });
            this.errorCodesTable.Rows.Add(new object[] { "313", "OBJECT: POSITION: Y value missing" });
            this.errorCodesTable.Rows.Add(new object[] { "314", "OBJECT: SIZE: Invalid X value" });
            this.errorCodesTable.Rows.Add(new object[] { "315", "OBJECT: SIZE: Invalid Y value" });
            this.errorCodesTable.Rows.Add(new object[] { "316", "OBJECT: SIZE: X value is missing" });
            this.errorCodesTable.Rows.Add(new object[] { "317", "OBJECT: SIZE: Y value missing" });
            this.errorCodesTable.Rows.Add(new object[] { "318", "OBJECT: FONT: Illegal data, a number was expected" });
            this.errorCodesTable.Rows.Add(new object[] { "319", "OBJECT: COLOR: Illegal data" });
            this.errorCodesTable.Rows.Add(new object[] { "320", "OBJECT: COUNTER: Illegal data, a number was expected" });
            this.errorCodesTable.Rows.Add(new object[] { "321", "OBJECT: FONT: Invalid font type" });
            this.errorCodesTable.Rows.Add(new object[] { "330", "OBJECT: DATE: Illegal data, a number was expected" });
            this.errorCodesTable.Rows.Add(new object[] { "331", "OBJECT: DATE: Illegal data, format dd / mm / yy was expected" });
            this.errorCodesTable.Rows.Add(new object[] { "332", "OBJECT: DATE: Function failed" });
            this.errorCodesTable.Rows.Add(new object[] { "335", "OBJECT: TEXT: invalid voicemail, a number was expected" });
            this.errorCodesTable.Rows.Add(new object[] { "340", "OBJECT: LOGO: File not found" });
            this.errorCodesTable.Rows.Add(new object[] { "341", "OBJECT: LOGO: Cannot change logo" });
            this.errorCodesTable.Rows.Add(new object[] { "342", "OBJECT: LOGO: Illegal data, a number was expected" });
            this.errorCodesTable.Rows.Add(new object[] { "352", "OBJECT: BARCODE: Function failed" });
            this.errorCodesTable.Rows.Add(new object[] { "353", "OBJECT: BARCODE: Invalid barcode type" });
            this.errorCodesTable.Rows.Add(new object[] { "354", "OBJECT: BARCODE: Invalid checksum value, 0-2 is allowed" });
            this.errorCodesTable.Rows.Add(new object[] { "355", "OBJECT: BARCODE: Invalid ink spacing, a number was expected" });
            this.errorCodesTable.Rows.Add(new object[] { "356", "OBJECT: BARCODE: Only applicable to data matrix" });
            this.errorCodesTable.Rows.Add(new object[] { "357", "OBJECT: BARCODE: Invalid format, see documentation" });
            this.errorCodesTable.Rows.Add(new object[] { "360", "OBJECT: LINE: Illegal data, a number was expected" });
            this.errorCodesTable.Rows.Add(new object[] { "370", "OBJECT: RECTANGLE: Illegal data, a number was expected" });
            this.errorCodesTable.Rows.Add(new object[] { "380", "OBJECT: ELLIPSE: Illegal data, a number was expected" });
            this.errorCodesTable.Rows.Add(new object[] { "390", "OBJECT: FIELD: Illegal data, a number was expected" });
            this.errorCodesTable.Rows.Add(new object[] { "391", "OBJECT: FIELD: Invalid command" });
            this.errorCodesTable.Rows.Add(new object[] { "392", "OBJECT: FIELD: Too many lines" });
            this.errorCodesTable.Rows.Add(new object[] { "393", "OBJECT: FIELD: Unknown alignment" });
            this.errorCodesTable.Rows.Add(new object[] { "400", "OBJECT: ROTATION: Only rotations of 0, 90, 180, and 270 degrees allowed" });
            this.errorCodesTable.Rows.Add(new object[] { "500", "OBJECT: SNAP: Valid values ​​are 1-9" });
            this.errorCodesTable.Rows.Add(new object[] { "600", "OBJECT: SCHEDULE: Illegal data, numbers; day, time, text was expected" });
            this.errorCodesTable.Rows.Add(new object[] { "1000", "PARAMETER: Unknown command" });
            this.errorCodesTable.Rows.Add(new object[] { "1010", "PARAMETER: START: Illegal data, a number was expected" });
            this.errorCodesTable.Rows.Add(new object[] { "1020", "PARAMETER: PAGE: Unknown page" });
            this.errorCodesTable.Rows.Add(new object[] { "1030", "PARAMETER: SIGNAL: Unknown signal" });
            this.errorCodesTable.Rows.Add(new object[] { "1040", "PARAMETER: ENDLESS: Illegal data, + or - were expected" });
            this.errorCodesTable.Rows.Add(new object[] { "1050", "PARAMETER: MODE: Illegal data, P, M or V was expected" });
            this.errorCodesTable.Rows.Add(new object[] { "1060", "PARAMETER: VELOCITY / ENCODER: Illegal data, a number was expected" });
            this.errorCodesTable.Rows.Add(new object[] { "1070", "PARAMETER: MODULES: Illegal data, a number was expected" });
            this.errorCodesTable.Rows.Add(new object[] { "1080", "PARAMETER: REPEAT: expected format is number; distance" });
            this.errorCodesTable.Rows.Add(new object[] { "1090", "PARAMETER: SPIT: expected format is frequency; size; +/- (tickle)" });
            this.errorCodesTable.Rows.Add(new object[] { "1100", "PARAMETER: PURGE: expected format is main_bits; length" });
            this.errorCodesTable.Rows.Add(new object[] { "1101", "PARAMETER: START PURGE: expected format is main_bits" });
            this.errorCodesTable.Rows.Add(new object[] { "1110", "PARAMETER: HP: expected format is type [; value]" });
            this.errorCodesTable.Rows.Add(new object[] { "1120", "PARAMETER: SIZE: illegal data, a number was expected" });
            this.errorCodesTable.Rows.Add(new object[] { "1130", "PARAMETER: STITCH: expected format is head; value" });
            this.errorCodesTable.Rows.Add(new object[] { "1140", "PARAMETER: STITCH: invalid head, 1-15 allowed" });
            this.errorCodesTable.Rows.Add(new object[] { "1200", "ADJUST: Invalid print head" });
            this.errorCodesTable.Rows.Add(new object[] { "1201", "ADJUST: Unknown command" });
            this.errorCodesTable.Rows.Add(new object[] { "1202", "ADJUST: Invalid head offset, a number was expected" });
            this.errorCodesTable.Rows.Add(new object[] { "1203", "ADJUST: Invalid engine offset, a number was expected" });
            this.errorCodesTable.Rows.Add(new object[] { "1300", "DATABASE: Requires an assigned database" });
            this.errorCodesTable.Rows.Add(new object[] { "1301", "DATABASE: File not found" });
        } // end initTable()


    } // end Class
} // end namespace

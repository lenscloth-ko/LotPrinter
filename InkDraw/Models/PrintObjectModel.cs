using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InkDraw.Models
{
    /// <summary>
    /// 출력요소 DTO Class
    /// </summary>
    public class PrintObjectModel
    {
        public string Grade { get; set; }
        public string Color { get; set; }
        public string LotNo { get; set; }
        public string WetNt { get; set; }
        public string StartSerialNo { get; set; }
        public string EndSerialNo { get; set; }
        public string Inspector { get; set; }
        public string BarcodeText { get; set; }
        public string QRCode01Text { get; set; }
        public string QRCode02Text { get; set; }

    }
}

//using NLog;
//using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using NLog;
//using log4net;  // log4net 네임스페이스
//using log4net.Config;
//using log4net.Layout;
//using log4net.Repository.Hierarchy;
//using log4net.Appender;

namespace InkDraw
{
    /// <summary>
    /// 해당 클래스 instance 후
    /// 필수 파라마티 설정하고 호출하는 부분까지 모두를
    /// try catch 블럭 안에서 호출하셔야 올바른 exception처리가 가능해집니다.
    /// </summary>
    public class InkDrawCmd
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        #region 설정
        /// <summary>
        /// 프린터에 달려있는 InkDraw S/W가 설치된 PC의 IP정보
        /// InkDraw > File -> Preferences-> Network 에서 설정
        /// </summary>
        private string _inkDrawIP;

        /// <summary>
        /// 프린터에 달려있는 InkDraw S/W가 설치된 PC의 Port정보
        /// InkDraw > File -> Preferences-> Network 에서 설정
        /// </summary>
        private int _inkDrawPort;

        /// <summary>
        /// Tcp Client
        /// </summary>
        private TcpClient tc;

        /// <summary>
        /// 현재 연결성공 여부
        /// </summary>
        private bool isConnected = false;

        //Logger logger = LogManager.GetCurrentClassLogger();
        
        #endregion

        #region 요소정보
        /// <summary>
        /// GRADE 값 
        /// </summary>
        private string _grade;
        public string Grade
        {
            get
            {
                return _grade;
            }
            set
            {
                _grade = value;
            }
        } // end Grade

        /// <summary>
        /// COLOR 값
        /// </summary>
        private string _color;
        public string Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        } // end Color

        /// <summary>
        /// Lot No 값
        /// </summary>
        private string _lotNo;
        public string LotNo
        {
            get
            {
                return _lotNo;
            }
            set
            {
                _lotNo = value;
            }
        } // end Lot No

        /// <summary>
        /// WetNt 값
        /// </summary>
        private string _wetNt;
        public string WetNt
        {
            get
            {
                return _wetNt;
            }
            set
            {
                _wetNt = value;
            }
        } // end WetNt

        /// <summary>
        /// 일력번호 시작값
        /// </summary>
        private int _serialStartNum;
        public int SerialStartNum
        {
            get
            {
                return _serialStartNum;
            }
            set
            {
                _serialStartNum = value;
            }
        } // end SerialStartNum

        /// <summary>
        /// 일력번호 종료값
        /// </summary>
        private int _serialEndNum;
        public int SerialEndNum
        {
            get
            {
                return _serialEndNum;
            }
            set
            {
                _serialEndNum = value;
            }
        } // end SerialEndNum

        /// <summary>
        ///파레트 번호 시작값
        /// </summary>
        private int _bagStartNum;
        public int BagStartNum
        {
            get
            {
                return _bagStartNum;
            }
            set
            {
                _bagStartNum = value;
            }
        } // end PalletStartNum

        /// <summary>
        ///파레트 번호 종료값
        /// </summary>
        private int _bagEndNum;
        public int BagEndNum
        {
            get
            {
                return _bagEndNum;
            }
            set
            {
                _bagEndNum = value;
            }
        } // end PalletEndNum

        /// <summary>
        /// 작업자
        /// </summary>
        private string _inspector;
        public string Inspector
        {
            get
            {
                return _inspector;
            }
            set
            {
                _inspector = value;
            }
        } // end SerialEndNum
        #endregion

        /// <summary>
        /// InkDraw S/W가 설치된 PC의 IP, Port로 마킹시스템과 연결
        /// </summary>
        /// <param name="ip">프린터에 달려있는 InkDraw S/W가 설치된 PC의 IP정보, InkDraw > File -> Preferences-> Network 에서 설정</param>
        /// <param name="port">프린터에 달려있는 InkDraw S/W가 설치된 PC의 Port정보, InkDraw > File -> Preferences-> Network 에서 설정</param>
        public InkDrawCmd(string ip, int port)
        {
            

            _inkDrawIP = ip;
            _inkDrawPort = port;
            ConnectInkdraw();
        } // end InkDrawCmd(string ip, int port)

        /// <summary>
        /// InkDraw에(설정된 연결정보) 연결
        /// </summary>
        private void ConnectInkdraw()
        {
            try
            {
                tc = new TcpClient(_inkDrawIP, _inkDrawPort);
                isConnected = true;
                logger.Info(string.Format("Connected InkDraw IP : {0}, Port {1}",_inkDrawIP, _inkDrawPort));
            }
            catch (Exception ex)
            {
                isConnected = false;
                logger.Error(ex.Message);
                throw new ArgumentException(ex.Message, "ip, port");
            }
        } // end ConnectInkdraw()

        /// <summary>
        /// 인쇄(마킹) 지시
        /// </summary>
        /// <returns></returns>
        public string SetPrintOrder()
        {
            string returnCode = "0";

            if (isConnected)
            {
                NetworkStream stream = tc.GetStream();

                // 1. 파일을 Open 한다
                //CallCommandStream("COMMAND:load file;ABS01.ink#", stream);

                // 2. 파일 Open 확인 한다
                //CallCommandStream("REQUEST: connect;ABS01.ink#", stream);
                returnCode += CallCommandStream("REQUEST:status#", stream);

                // 3. GRADE, COLOR, LOT NO, Counter(일련번호) 입력 확인
                StringBuilder sb = new StringBuilder();

                if (String.IsNullOrEmpty(_grade))
                {
                    //logger.Error("필수 입력 정보가 누락되었습니다. : Grade");
                    throw new ArgumentException("필수 입력 정보가 누락되었습니다.", "Grade");
                }
                else
                {
                    sb.AppendFormat("OBJECT:TGRADE;TEX;{0}#", _grade);
                }
                if (String.IsNullOrEmpty(_color))
                {
                    logger.Error("필수 입력 정보가 누락되었습니다. : Color");
                    throw new ArgumentException("필수 입력 정보가 누락되었습니다.", "Color");
                }
                else
                {
                    sb.AppendFormat("OBJECT:TCOLOR;TEX;{0}#", _color);
                }
                if (String.IsNullOrEmpty(_lotNo))
                {
                    logger.Error("필수 입력 정보가 누락되었습니다. : Lot No");
                    throw new ArgumentException("필수 입력 정보가 누락되었습니다.", "Lot No");
                }
                else
                {
                    sb.AppendFormat("OBJECT:TLOTNO;TEX;{0}#", _lotNo);
                }

                if ( (_serialEndNum - _serialStartNum) < 0 )
                {
                    logger.Error("시작번호가 종료번호보다 같거나 작아야 합니다. : Serial No");
                    throw new ArgumentException("시작번호가 종료번호보다 같거나 작아야 합니다.", "Serial No");
                }

                if ((_bagEndNum - _bagStartNum) < 0)
                {
                    logger.Error("시작번호가 종료번호보다 같거나 작아야 합니다. : Bag No");
                    throw new ArgumentException("시작번호가 종료번호보다 같거나 작아야 합니다.", "Bag No");
                }

                // 4. InkDraw Object에 정보 전송
                returnCode += CallCommandStream(sb.ToString(), stream);

                // 5. InkDraw Counter Object 설정
                sb.Clear();

                sb.Append("OBJECT:CSerial;MIN;1#");

                if (!string.IsNullOrEmpty(_serialStartNum.ToString()))
                {
                    sb.AppendFormat("OBJECT:CSerial;CUR;{0}#", _serialStartNum);
                }
                else
                {
                    sb.Append("OBJECT:CSerial;CUR;1#");
                }

                //MAX는 마지막 숫자가 아니라 1부터 출력되는 숫자로 설정함
                //40~60 이렇게 20개 출력시에는
                //MIN : 1 ( 고정 )
                //MAX : 60-40 = 20
                //CUR : 40
                //이렇게 설정요망

                int finalNumber = _serialEndNum - _serialStartNum;

                sb.AppendFormat("OBJECT:CSerial;MAX;{0}#", finalNumber);
                sb.AppendFormat("OBJECT:CSerial;DIG;2#");
                sb.AppendFormat("OBJECT:CSerial;DIR;+#");

                returnCode += CallCommandStream(sb.ToString(), stream);

                // 6. Bag Number Counter Object 설정
                sb.Clear();

                //Bag Number
                sb.Append("OBJECT:CBagNo;MIN;1#");

                if (!string.IsNullOrEmpty(_bagStartNum.ToString()))
                {
                    sb.AppendFormat("OBJECT:CBagNo;CUR;{0}#", _bagStartNum);
                }
                else
                {
                    sb.Append("OBJECT:CBagNo;CUR;1#");
                }

                //MAX는 마지막 숫자가 아니라 1부터 출력되는 숫자로 설정함
                //40~60 이렇게 20개 출력시에는
                //MIN : 1 ( 고정 )
                //MAX : 60-40 = 20
                //CUR : 40
                //이렇게 설정요망

                int finalBagNumber = _bagEndNum - _bagStartNum;
                finalBagNumber = finalBagNumber + _bagStartNum;

                sb.AppendFormat("OBJECT:CBagNo;MAX;{0}#", finalBagNumber);
                sb.AppendFormat("OBJECT:CBagNo;DIG;2#");
                sb.AppendFormat("OBJECT:CBagNo;DIR;+#");

                returnCode += CallCommandStream(sb.ToString(), stream);

                // 5. 출력지시 및 피드백 확인
                stream.Close();
                tc.Close();
                isConnected = false;
            }

            //logger.Info(string.Format("return : {0}", returnCode));
            return returnCode;
        } // end SetPrintOrder()

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="ns"></param>
        /// <returns></returns>
        private string CallCommandStream(string cmd, NetworkStream ns)
        {
            byte[] buff = Encoding.ASCII.GetBytes(cmd);

            // (1) 스트림에 바이트 데이타 전송
            ns.Write(buff, 0, buff.Length);

            // (2) 스트림으로부터 바이트 데이타 읽기
            byte[] outbuf = new byte[1024];
            int nbytes = ns.Read(outbuf, 0, outbuf.Length);
            string output = Encoding.ASCII.GetString(outbuf, 0, nbytes);

            string errorCode = string.Empty;
            string x = output.Replace("(c)", "");

            if (x.IndexOf(")#") > 0)
            {
                int ck = x.IndexOf('(');
                errorCode = x.Replace(x.Substring(0, ck), "").Replace("(", "").Replace(")#", "");
                //logger.Info(string.Format("request Command : {0} => return errorCode : {1}", cmd, errorCode));
            }
            else
            {
                errorCode = "T";
                //logger.Info(string.Format("[x]request Command : {0} => return errorCode : {1}", cmd, errorCode));
            }

            //logger.Info(string.Format("Call Function CallCommandStream => return : " + errorCode));
            return errorCode;
        } // end CallCommandStream(string cmd)


    } // end class
} // end namespace

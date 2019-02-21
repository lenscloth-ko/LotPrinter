using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace InkDraw
{
    /// <summary>
    /// 해당 클래스 instance 후
    /// 필수 파라마티 설정하고 호출하는 부분까지 모두를
    /// try catch 블럭 안에서 호출하셔야 올바른 exception처리가 가능해집니다.
    /// </summary>
    public class InkDrawCmd
    {
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
        #endregion

        #region 요소정보
        /// <summary>
        /// GRADE 값 
        /// </summary>
        private string _grade;
        /// <summary>
        /// GRADE 값
        /// </summary>
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
        /// <summary>
        /// COLOR 값
        /// </summary>
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
        /// <summary>
        /// Lot No 값
        /// </summary>
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
        } // end Color

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
            }
            catch (Exception ex)
            {
                isConnected = false;
                throw new ArgumentException(ex.Message, "ip, port");
            }
        } // end ConnectInkdraw()

        /// <summary>
        /// 인쇄(마킹) 지시
        /// </summary>
        /// <returns></returns>
        public string SetPrintOrder()
        {
            if (isConnected)
            {
                NetworkStream stream = tc.GetStream();

                // 1. 파일을 Open 한다
                //CallCommandStream("COMMAND:load file;ABS03.ink#");

                // 2. 파일 Open 확인 한다
                //CallCommandStream("REQUEST: connect;ABS01.ink#", stream);
                CallCommandStream("REQUEST:status#", stream);

                // 3. GRADE, COLOR, LOT NO, Counter(일련번호) 입력 확인
                StringBuilder sb = new StringBuilder();

                if (String.IsNullOrEmpty(_grade))
                {
                    throw new ArgumentException("필수 입력 정보가 누락되었습니다.", "Grade");
                }
                else
                {
                    sb.AppendFormat("OBJECT:TGRADE;TEX;{0}#", _grade);
                }
                if (String.IsNullOrEmpty(_color))
                {
                    throw new ArgumentException("필수 입력 정보가 누락되었습니다.", "Color");
                }
                else
                {
                    sb.AppendFormat("OBJECT:TCOLOR;TEX;{0}#", _color);
                }
                if (String.IsNullOrEmpty(_lotNo))
                {
                    throw new ArgumentException("필수 입력 정보가 누락되었습니다.", "Lot No");
                }
                else
                {
                    sb.AppendFormat("OBJECT:TLOTNO;TEX;{0}#", _lotNo);
                }

                // 4. InkDraw Object에 정보 전송
                CallCommandStream(sb.ToString(), stream);

                // 5. InkDraw Counter Object 설정
                sb.Clear();
                sb.AppendFormat("OBJECT:CSerial;MIN;1#");
                sb.AppendFormat("OBJECT:CSerial;CUR;1#");
                sb.AppendFormat("OBJECT:CSerial;MAX;5#");
                sb.AppendFormat("OBJECT:CSerial;DIG;2#");
                sb.AppendFormat("OBJECT:CSerial;DIR;+#");
                CallCommandStream(sb.ToString(), stream);

                // 5. 출력지시 및 피드백 확인

                stream.Close();
                tc.Close();
                isConnected = false;
            }

            return string.Empty;
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
            }
            else
            {
                errorCode = output;
            }

            Console.WriteLine(errorCode);

            return errorCode;
        } // end CallCommandStream(string cmd)


    } // end class
} // end namespace

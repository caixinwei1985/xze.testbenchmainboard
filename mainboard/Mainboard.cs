using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO.Ports;
using System.Messaging;
using System.Runtime.CompilerServices;
using System.Configuration;
using XZE.STM32ISP;
using System.IO;

namespace Xze.TestBench
{
    public class Mainboard : IDisposable
    {
        public enum WritableRegs
        {
            /// <summary>
            /// Fan 1 control register
            /// </summary>
            Fan1 = 0x0100,
            Fan2,
            /// <summary>
            /// 
            /// </summary>
            SW5V,
            SW24V,
            Lamp,
            LED,
            Laser,
            Buzer,
            XDir,
            YDir,
            ZDir,
            MotoEn,
            XPul,
            YPul,
            ZPul,
            HpOut1,
            HpOut2,
            RegEnd
        }

        public enum ReadableRegs
        {
            Protect0 = 0x0200,
            BtnStart,
            Js1Btn,
            Js1D0,
            Js1D1,
            Js1D2,
            Js1D3,
            Js2Btn,
            Js2D0,
            Js2D1,
            Js2D2,
            Js2D3,
            XSW0,
            XSW1,
            XSW2,
            YSW0,
            YSW1,
            YSW2,
            ZSW0,
            ZSW1,
            ZSW2,
            NTC1,
            NTC2,
            NTC3,
            MotoCurr,
            Protect1,
            Protect2,
            RegsEnd
        }

        //#define CMD_MOTO_MOVE   0x0300  /* DATA 8 Bytes: byte0 axis,byte1 acc, byte2:byte3 speed,byte4:byte7 step */
        const UInt16 CMD_MOTO_MOVE = 0x0300;
        //#define CMD_MOTO_RST    0x0301  
        const UInt16 CMD_MOTO_RST = 0x0301;
        //#define NTF_MOTO_COMP   0x0400
        //#define NTF_MOTO_OT     0x0401
        public enum Notify
        {
            MotoComplete = 0x0400,
            MotoTimeout,
            ProtectionTrigger,
            MotoReset,
            MotoFarTrigger,
            MotoNearTrigger,
            StartBtnPress,
            JS1BtnPress,
            JS1D0Touch,
            JS1D1Touch,
            JS1D2Touch,
            JS1D3Touch,
            JS2BtnPress,
            JS2D0Touch,
            JS2D1Touch,
            JS2D2Touch,
            JS2D3Touch,
            RXCollid,
            MOTO_EMER,

            /// <summary>
            /// X轴电机运行完成
            /// </summary>
            MOTO_COMP_X,
            /// <summary>
            /// X轴电机运行超时
            /// </summary>
            MOTO_OT_X,
            /// <summary>
            /// X轴电机复位
            /// </summary>
            MOTO_RESET_X,
            /// <summary>
            /// X轴电机运行到远点
            /// </summary>
            MOTO_FAR_X,
            MOTO_NEAR_X,
            MOTO_EMER_X,

            MOTO_COMP_Y,
            MOTO_OT_Y,
            MOTO_RESET_Y,
            MOTO_FAR_Y,
            MOTO_NEAR_Y,
            MOTO_EMER_Y,

            MOTO_COMP_Z,
            MOTO_OT_Z,
            MOTO_RESET_Z,
            MOTO_FAR_Z,
            MOTO_NEAR_Z,
            MOTO_EMER_Z,
            NotifyEnd
        }

        public enum Axis { X = 0, Y, Z }
        public class NotifyEventArgs : EventArgs
        {
            public Notify Notify { set; get; }
            public NotifyEventArgs(Notify notify)
            {
                Notify = notify;
            }
        }
        /// <summary>
        /// 字符串信息被收到事件
        /// </summary>
        public event EventHandler<string> StringInfoReceived;
        /// <summary>
        /// 上位机收到下位机的通知触发事件
        /// </summary>
        public event EventHandler<NotifyEventArgs> NotifyReceived;

        private SerialPort _sp;
        private SerialPort _ispcom;
        private object _splock = new object();
        /// <summary>
        /// 串口资源互斥开关
        /// </summary>
        private Mutex _spmutex;
        /// <summary>
        /// 寄存器读写响应消息接收事件触发
        /// </summary>
        private AutoResetEvent _recvevent;
        /// <summary>
        /// 串口数据接收事件触发
        /// </summary>
        private AutoResetEvent _datarecvevent;
        /// <summary>
        /// 线程间传递寄存器数据的缓冲区
        /// </summary>
        private UInt16 _regdata;
        bool _keepthread;
        public Mainboard(SerialPort sp)
        {
            _sp = sp;
            _sp.BaudRate = 115200;
            _sp.DataBits = 8;
            _sp.Parity = Parity.None;
            _sp.StopBits = StopBits.One;
            _sp.DataReceived += _sp_DataReceived;
            _sp.ReadTimeout = 1000;
            _sp.Open();
            _spmutex = new Mutex(false);
            _recvevent = new AutoResetEvent(true);
            _recvevent.Reset();
            _datarecvevent = new AutoResetEvent(true);
            _datarecvevent.Reset();
            _keepthread = true;
            Thread _t = new Thread(new ThreadStart(recv_thread));
            _t.Start();
        }

        private void _sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            _datarecvevent.Set();
        }
        /// <summary>
        /// 电机运动配置，定义方向：远点为前进，近点为后退
        /// </summary>
        /// <param name="axis">指定运动的轴向</param>
        /// <param name="acc">加速度</param>
        /// <param name="speed">目标速度</param>
        /// <param name="steps">行进的总步数，正数代表前进，负数代表后退</param>
        /// <param name="timeout">超时时长</param>
        /// <returns></returns>
        public async Task<bool> MotoMove(Axis axis, byte acc, UInt16 speed, Int32 steps, UInt16 timeout)
        {
            bool rs = true;
            byte[] buf = new byte[17];
            int i = 0;
            buf[i++] = 0x5a;
            buf[i++] = 0xa5;
            buf[i++] = (byte)(buf.Length >> 8);
            buf[i++] = (byte)(buf.Length & 0xff);
            buf[i++] = (byte)(CMD_MOTO_MOVE >> 8);
            buf[i++] = (byte)(CMD_MOTO_MOVE & 0xff);
            buf[i++] = (byte)axis;
            buf[i++] = acc;
            buf[i++] = (byte)(speed >> 8);
            buf[i++] = (byte)(speed & 0xff);
            buf[i++] = (byte)(steps >> 24);
            buf[i++] = (byte)((steps >> 16) & 0xff);
            buf[i++] = (byte)((steps >> 8) & 0xff);
            buf[i++] = (byte)((steps & 0xff));
            buf[i++] = (byte)((timeout >> 8) & 0xff);
            buf[i++] = (byte)((timeout & 0xff));
            buf[i] = 0;
            for (i = 0; i < buf.Length - 1; i++)
                buf[buf.Length - 1] ^= buf[i];
            await Task.Run(() =>
            {
                //串口资源互斥锁
                if (!_spmutex.WaitOne())
                    rs = false;
                else
                {
                    _sp.Write(buf, 0, buf.Length);
                    if (0xffff == recv_proc(CMD_MOTO_MOVE))
                        rs = false;
                    _spmutex.ReleaseMutex();
                }

            }).ConfigureAwait(false);
            return rs;
        }
        /// <summary>
        /// 复位指定轴向电机位置
        /// </summary>
        /// <param name="axis">需要复位的轴向</param>
        /// <param name="timeout">超时，10ms单位</param>
        /// <returns></returns>
        public async Task<bool> MotoReset(Axis axis, UInt16 timeout)
        {
            bool rs = true;
            byte[] buf = new byte[10];
            int i = 0;
            buf[i++] = 0x5a;
            buf[i++] = 0xa5;
            buf[i++] = (byte)(buf.Length >> 8);
            buf[i++] = (byte)(buf.Length & 0xff);
            buf[i++] = (Byte)(CMD_MOTO_RST >> 8);
            buf[i++] = (byte)(CMD_MOTO_RST & 0xff);
            buf[i++] = (byte)axis;
            buf[i++] = (byte)(timeout >> 8);
            buf[i++] = (byte)(timeout & 0xff);
            buf[i] = 0;
            for (i = 0; i < buf.Length - 1; i++)
                buf[buf.Length - 1] ^= buf[i];
            await Task.Run(() =>
            {
                //串口资源互斥锁
                if (!_spmutex.WaitOne())
                    rs = false;
                else
                {
                    _sp.Write(buf, 0, buf.Length);
                    if (0xffff == recv_proc(CMD_MOTO_RST))
                        rs = false;
                    _spmutex.ReleaseMutex();
                }

            }).ConfigureAwait(false);
            return rs;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj">开启当前接收过程的命令码</param>
        /// <returns></returns>
        private UInt16 recv_proc(object obj)
        {
            UInt16 cmd = Convert.ToUInt16(obj);
            if (_recvevent.WaitOne(100))
            {
                //如果是读命令的响应，返回数据,写命令则返回非0xffff的值
                if ((cmd & 0xff00) == 0x0200)
                    return _regdata;
                else
                    return 0;
            }
            return 0xffff;
        }
        private byte[] generate_write_command(UInt16 cmd, UInt16 val)
        {
            byte[] ar = null;
            byte chk = 0;
            ar = new byte[9];
            ar[0] = 0x5a;
            ar[1] = 0xa5;
            ar[2] = 0x00;
            ar[3] = 0x09;
            ar[4] = (byte)(cmd >> 8);
            ar[5] = (byte)(cmd & 0xff);
            ar[6] = (byte)(val >> 8);
            ar[7] = (byte)(val & 0xff);
            for (int i = 0; i < 8; i++)
                chk ^= ar[i];
            ar[8] = chk;
            return ar;
        }
        private byte[] generate_read_command(UInt16 cmd)
        {
            byte[] ar = null;
            byte chk = 0;
            ar = new byte[7];
            ar[0] = 0x5a;
            ar[1] = 0xa5;
            ar[2] = 0x00;
            ar[3] = 0x07;
            ar[4] = (byte)(cmd >> 8);
            ar[5] = (byte)(cmd & 0xff);

            for (int i = 0; i < 6; i++)
                chk ^= ar[i];
            ar[6] = chk;
            return ar;
        }
        /// <summary>
        /// 读输入寄存器状态
        /// </summary>
        /// <param name="reg">寄存器名称</param>
        /// <returns>返回输入状态 '0'或‘非0’,0xffff表示访问错误</returns>
        private async Task<UInt16> ReadReg(ReadableRegs reg)
        {
            UInt16 val = 0;
            byte[] ar = generate_read_command((UInt16)reg);
            await Task.Run(() =>
            {
                bool wait;
                //串口资源互斥锁
                //Console.WriteLine($"Before wait, thread id is {Thread.CurrentThread.ManagedThreadId}");
                wait = _spmutex.WaitOne(50);
                if (!wait)
                {
                    val = 0xffff;
                }
                else
                {
                    //Console.WriteLine($"Fetch wait, thread id is {Thread.CurrentThread.ManagedThreadId}");
                    _sp.Write(ar, 0, ar.Length);
                    val = recv_proc((UInt16)reg);
                    _spmutex.ReleaseMutex();
                }
                //Console.WriteLine($"after wait, thread id is {Thread.CurrentThread.ManagedThreadId}");

            }).ConfigureAwait(false);
            return val;
        }
        private bool waitforbytestoread(int count, int to)
        {
            DateTime start = DateTime.Now;
            bool timeout = false;
            while (_sp.BytesToRead < count)
            {
                if ((DateTime.Now - start).TotalMilliseconds > to)
                {
                    timeout = true;
                    break;
                }
            }
            if (timeout)
                return false;
            return true;
        }
        /// <summary>
        /// 写输出寄存器
        /// </summary>
        /// <param name="wr">输出寄存器名称</param>
        /// <param name="val">输出状态，‘0’或‘非0’</param>
        /// <returns>返回下位机的响应状态，true成功，false失败</returns>
        private async Task<bool> WriteReg(WritableRegs wr, UInt16 val)
        {
            bool rs = true; ;
            if (val != 0)
                val = 1;
            byte[] ar = generate_write_command((UInt16)wr, val);
            await Task.Run(() =>
           {
               if (!_spmutex.WaitOne(10))
               {
                   rs = false;
               }
               else
               {
                   _sp.Write(ar, 0, ar.Length);
                   if (0xffff != recv_proc((UInt16)wr))
                       rs = true;
                   else
                       rs = false;
                   _spmutex.ReleaseMutex();
               }

           }).ConfigureAwait(false);
            if (rs)
                Console.WriteLine($"Register {wr} write {val} succ");
            else
                Console.WriteLine($"Register {wr} write {val} fail");
            return rs;
        }

        public async Task<bool> ResetToISP()
        {
            UInt16 val = 0;
            byte[] ar = generate_read_command(0xff00);
            await Task.Run(() =>
            {
                //串口资源互斥锁
                if (!_spmutex.WaitOne(500))
                {
                    val = 0xffff;
                }
                else
                {
                    _sp.Write(ar, 0, ar.Length);
                    val = recv_proc(0xff00);
                    _spmutex.ReleaseMutex();
                }
            }).ConfigureAwait(false);
            if (val == 0xffff)
                return false;
            return true;
        }
        private void recv_thread()
        {
            // decode
            int b;
            int chk;
            byte[] buf = new byte[128];
            while (_keepthread)
            {
                _datarecvevent.WaitOne();
                if (!_keepthread)
                    break;
                while (_sp.BytesToRead > 0)
                {
                    chk = 0;
                    buf = new byte[2];
                    b = _sp.ReadByte();
                    if (0xaa != b && 0xcc != b)
                        continue;
                    chk ^= b;

                    /* Hex格式帧 */
                    if (0xaa == b)
                    {
                        b = _sp.ReadByte();
                        if (0x55 != b)
                            continue;
                        chk ^= b;
                        if (!waitforbytestoread(2, 1000))
                            continue;
                        _sp.Read(buf, 0, 2);
                        chk ^= buf[0];
                        chk ^= buf[1];
                        int len = buf[0] * 256 + buf[1] - 4;
                        buf = new byte[len];

                        if (!waitforbytestoread(len, 1000))
                            continue;
                        _sp.Read(buf, 0, len);
                        for (int i = 0; i < len - 1; i++)
                            chk ^= buf[i];
                        if (chk != buf[len - 1])
                            continue;
                        if (buf[0] == 0x01 || buf[0] == 0x02 || buf[0] == 0x03)
                        {
                            //写操作的应答，以及读操作的返回
                            _recvevent.Set();
                            //读操作应答
                            if (buf[0] == 0x02)
                                _regdata = (UInt16)(buf[2] * 256 + buf[3]);

                        }
                        if (buf[0] == 0x04)
                        {
                            Notify notify = (Notify)(buf[0] * 256 + buf[1]);
                            NotifyReceived?.Invoke(this, new NotifyEventArgs(notify));
                        }
                    }
                    /* ASCII码字符串帧 */
                    if (0xcc == b)
                    {
                        b = _sp.ReadByte();
                        if (0x33 != b)
                            continue;
                        chk ^= b;
                        if (!waitforbytestoread(2, 1000))
                            continue;
                        _sp.Read(buf, 0, 2);
                        int len = buf[0] * 256 + buf[1] - 4;
                        chk ^= buf[0];
                        chk ^= buf[1];
                        buf = new byte[len];
                        if (!waitforbytestoread(len, 1000))
                            continue;
                        _sp.Read(buf, 0, len);
                        for (int i = 0; i < buf.Length - 1; i++)
                            chk ^= buf[i];
                        if (chk != buf[buf.Length - 1])
                            continue;
                        string s = System.Text.Encoding.ASCII.GetString(buf, 0, buf.Length - 1);
                        StringInfoReceived?.Invoke(this, s);
                    }
                }
            }
            Console.WriteLine("receive thread exit");
        }
        public async Task<bool> Fan1On()
        {
            return await WriteReg(WritableRegs.Fan1, 0).ConfigureAwait(false);
        }
        public async Task<bool> Fan1Off()
        {
            return await WriteReg(WritableRegs.Fan1, 1).ConfigureAwait(false);
        }
        public async Task<bool> Fan2On()
        {
            return await WriteReg(WritableRegs.Fan2, 0).ConfigureAwait(false);
        }
        public async Task<bool> Fan2Off()
        {
            return await WriteReg(WritableRegs.Fan2, 1).ConfigureAwait(false); ;
        }

        public async Task<bool> VoutOn()
        {
            return await WriteReg(WritableRegs.SW24V, 0).ConfigureAwait(false);
        }
        public async Task<bool> VoutOff()
        {
            return await WriteReg(WritableRegs.SW24V, 1).ConfigureAwait(false);
        }
        public async Task<bool> VccOn()
        {
            return await WriteReg(WritableRegs.SW5V, 0).ConfigureAwait(false);
        }
        public async Task<bool> VccOff()
        {
            return await WriteReg(WritableRegs.SW5V, 1).ConfigureAwait(false);
        }
        public async Task<bool> BuzzerOn()
        {
            return await WriteReg(WritableRegs.Buzer, 0).ConfigureAwait(false);
        }
        public async Task<bool> BuzzerOff()
        {
            return await WriteReg(WritableRegs.Buzer, 1).ConfigureAwait(false);
        }
        public async Task<bool> LaserOn()
        {
            return await WriteReg(WritableRegs.Laser, 0).ConfigureAwait(false);
        }
        public async Task<bool> LaserOff()
        {
            return await WriteReg(WritableRegs.Laser, 1).ConfigureAwait(false);
        }
        public async Task<bool> LampOn()
        {
            return await WriteReg(WritableRegs.Lamp, 0).ConfigureAwait(false);
        }
        public async Task<bool> LampOff()
        {
            return await WriteReg(WritableRegs.Lamp, 1).ConfigureAwait(false);
        }
        public async Task<bool> LedOn()
        {
            return await WriteReg(WritableRegs.LED, 0).ConfigureAwait(false);
        }
        public async Task<bool> LedOff()
        {
            return await WriteReg(WritableRegs.LED, 1).ConfigureAwait(false);
        }
        public async Task<bool> HighPowerPort1On()
        {
            return await WriteReg(WritableRegs.HpOut1, 0).ConfigureAwait(false);
        }
        public async Task<bool> HighPowerPort1Off()
        {
            return await WriteReg(WritableRegs.HpOut1, 1).ConfigureAwait(false);
        }

        public async Task<bool> HighPowerPort2On()
        {
            return await WriteReg(WritableRegs.HpOut2, 0).ConfigureAwait(false);
        }

        public async Task<bool> HighPowerPort2Off()
        {
            return await WriteReg(WritableRegs.HpOut2, 1).ConfigureAwait(false);
        }
        /// <summary>
        /// 获取X轴零点的状态
        /// </summary>
        /// <remarks>建议使用<seealso cref="x = await fun() "/>形式作异步调用</remarks>
        /// <returns>0:开关无遮挡;1:开关有遮挡;0xffff:读取失败</returns>
        public async Task<UInt16> GetXZeroVal()
        {
            return await ReadReg(ReadableRegs.XSW0).ConfigureAwait(false);
        }
        /// <summary>
        /// 获取X轴近点的状态
        /// </summary>
        /// <remarks>建议使用<seealso cref="x = await fun() "/>形式作异步调用</remarks>
        /// <returns>0:开关无遮挡;1:开关有遮挡;0xffff:读取失败</returns>
        public async Task<UInt16> GetXNearVal()
        {
            return await ReadReg(ReadableRegs.XSW1).ConfigureAwait(false);
        }
        /// <summary>
        /// 获取X轴远点的状态
        /// </summary>
        /// <remarks>建议使用<seealso cref="x = await fun() "/>形式作异步调用</remarks>
        /// <returns>0:开关无遮挡;1:开关有遮挡;0xffff:读取失败</returns>
        public async Task<UInt16> GetXFarVal()
        {
            return await ReadReg(ReadableRegs.XSW2).ConfigureAwait(false);
        }
        /// <summary>
        /// 获取Y轴零点的状态
        /// </summary>
        /// <remarks>建议使用<seealso cref="x = await fun() "/>形式作异步调用</remarks>
        /// <returns>0:开关无遮挡;1:开关有遮挡;0xffff:读取失败</returns>
        public async Task<UInt16> GetYZeroVal()
        {
            return await ReadReg(ReadableRegs.YSW0).ConfigureAwait(false);
        }
        /// <summary>
        /// 获取Y轴近点的状态
        /// </summary>
        /// <remarks>建议使用<seealso cref="x = await fun() "/>形式作异步调用</remarks>
        /// <returns>0:开关无遮挡;1:开关有遮挡;0xffff:读取失败</returns>
        public async Task<UInt16> GetYNearVal()
        {
            return await ReadReg(ReadableRegs.YSW1).ConfigureAwait(false);
        }
        /// <summary>
        /// 获取Y轴远点的状态
        /// </summary>
        /// <remarks>建议使用<seealso cref="x = await fun() "/>形式作异步调用</remarks>
        /// <returns>0:开关无遮挡;1:开关有遮挡;0xffff:读取失败</returns>
        public async Task<UInt16> GetYFarVal()
        {
            return await ReadReg(ReadableRegs.YSW2).ConfigureAwait(false);
        }
        /// <summary>
        /// 获取Z轴零点的状态
        /// </summary>
        /// <remarks>建议使用<seealso cref="x = await fun() "/>形式作异步调用</remarks>
        /// <returns>0:开关无遮挡;1:开关有遮挡;0xffff:读取失败</returns>
        public async Task<UInt16> GetZZeroVal()
        {
            return await ReadReg(ReadableRegs.ZSW0).ConfigureAwait(false);
        }
        /// <summary>
        /// 获取Z轴近点的状态
        /// </summary>
        /// <remarks>建议使用<seealso cref="x = await fun() "/>形式作异步调用</remarks>
        /// <returns>0:开关无遮挡;1:开关有遮挡;0xffff:读取失败</returns>
        public async Task<UInt16> GetZNearVal()
        {
            return await ReadReg(ReadableRegs.ZSW1).ConfigureAwait(false);
        }
        /// <summary>
        /// 获取Z轴远点的状态
        /// </summary>
        /// <remarks>建议使用<seealso cref="x = await fun() "/>形式作异步调用</remarks>
        /// <returns>0:开关无遮挡;1:开关有遮挡;0xffff:读取失败</returns>
        public async Task<UInt16> GetZFarVal()
        {
            return await ReadReg(ReadableRegs.ZSW2).ConfigureAwait(false);
        }
        /// <summary>
        /// Mainboard类的静态工具方法,查找连接到的设备，
        /// </summary>
        /// <returns>返回当前设备的句柄，没找到设备则返回<see cref="null"/></returns>
        public static Mainboard FindDevice()
        {
            Mainboard mb = null;
            string[] names = SerialPort.GetPortNames();
            foreach (string s in names)
            {
                try
                {
                    mb = new Mainboard(new SerialPort(s));
                    Console.WriteLine(DateTime.Now.ToLongTimeString());
                    if (mb.LedOff().Result)
                    {
                        Console.WriteLine($"Device is finded at port {mb._sp.PortName}");
                        break;
                    }
                    else
                    {
                        mb._sp.Close();
                        mb.Dispose();
                        mb = null;
                    }
                    Console.WriteLine(DateTime.Now.ToLongTimeString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    mb = null;
                }
                finally
                {

                }
            }
            return mb;
        }

        public bool UpdateFirmware(FileStream fw)
        {
            bool rs = true;
            string name = _sp.PortName;
            STMisp stmisp = new STMisp();
            stmisp.Write = isp_write;
            stmisp.Read = isp_read;
            ResetToISP().Wait();

            // 退出接收线程
            _keepthread = false;
            _datarecvevent.Set();
            _sp.Close();
            Thread.Sleep(100);
            _ispcom = new SerialPort(name ,115200, Parity.Even, 8, StopBits.One);
            _ispcom.ReadTimeout = 200;
            _ispcom.Open();
            ISPACK ack;
            ack = stmisp.InitBL();
            if (ack != ISPACK.ISP_ACK)
                rs = false;
#if true
            ack = stmisp.Erase();
            if (ack == ISPACK.ISP_TO)
                rs = false;
            if (ack == ISPACK.ISP_NACK)
            {
                ack = stmisp.Read_unprotect();
                if (ack != ISPACK.ISP_ACK)
                    rs = false;
                Thread.Sleep(50);
                if (stmisp.InitBL() == ISPACK.ISP_TO)
                    rs = false;
            }
            if (rs)
            {
                long len_rem = fw.Length;
                uint start_addr = 0x08000000;
                byte[] wm = new byte[256];

                while (len_rem >= 256)
                {
                    fw.Read(wm, 0, 256);
                    if (WriteMemoryResult.OK != stmisp.WriteMemory(start_addr, 256, wm))
                    {
                        rs = false;
                        break;
                    }
                    len_rem -= 256;
                    start_addr += 256;
                    Console.WriteLine("remain bytes:{0}", len_rem);
                    Thread.Sleep(50);
                }
                if (len_rem > 0 && rs == true)
                {
                    fw.Read(wm, 0, (int)len_rem);
                    if (WriteMemoryResult.OK != stmisp.WriteMemory(start_addr, 256, wm))
                    {
                        rs = false;
                    }
                    Thread.Sleep(30);
                }
                fw.Close();
            }
#endif
            if (rs)
            {
                if (stmisp.Go() != ISPACK.ISP_ACK)
                {
                    Console.WriteLine("go fail");
                }
                else
                    Console.WriteLine("go succ");
            }
            _ispcom.Close();
            _sp.Open();
            _sp.DiscardInBuffer();
            _keepthread = true;
            Thread _t = new Thread(new ThreadStart(recv_thread));
            _t.Start();
            return rs;
        }



        private bool isp_write(byte[] buff, int count)
        {
            _ispcom.DiscardOutBuffer();
            _ispcom.Write(buff, 0, count);
            return true;
        }

        private bool isp_read(byte[] buff, int count)
        {
            int cnt;
            try
            {
                cnt = _ispcom.Read(buff, 0, count);
            }
            catch
            {
                cnt = 0;
            }
            if (cnt > 0)
            {
                return true;
            }
            else
            {
                Console.WriteLine("串口连接故障");
            }
            return false;
        }
#region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~Mainboard() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            _keepthread = false;
            _datarecvevent.Set();
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
#endregion
    }
}


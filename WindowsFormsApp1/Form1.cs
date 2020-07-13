using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xze.TestBench;
namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Mainboard _mainboard;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            System.IO.Ports.SerialPort sp = new System.IO.Ports.SerialPort("COM14");
            _mainboard = new Mainboard(sp);
            _mainboard.StringInfoReceived += _mainboard_StringInfoReceived;
            _mainboard.NotifyReceived += _mainboard_NotifyReceived;

        }

        private void _mainboard_NotifyReceived(object sender, Mainboard.NotifyEventArgs e)
        {
            switch (e.Notify)
            {
                case Mainboard.Notify.MotoComplete:
                    Console.Write("moto move completely");
                    break;
                case Mainboard.Notify.MotoTimeout:
                    Console.Write("moto move timeout");
                    break;
                case Mainboard.Notify.ProtectionTrigger:
                    break;
                case Mainboard.Notify.MotoReset:
                    break;
                case Mainboard.Notify.MotoFarTrigger:
                    break;
                case Mainboard.Notify.MotoNearTrigger:
                    break;
                case Mainboard.Notify.StartBtnPress:
                    Console.WriteLine("Start button press");
                    break;
                case Mainboard.Notify.JS1BtnPress:
                    break;
                case Mainboard.Notify.JS1D0Touch:
                    break;
                case Mainboard.Notify.JS1D1Touch:
                    break;
                case Mainboard.Notify.JS1D2Touch:
                    break;
                case Mainboard.Notify.JS1D3Touch:
                    break;
                case Mainboard.Notify.JS2BtnPress:
                    break;
                case Mainboard.Notify.JS2D0Touch:
                    break;
                case Mainboard.Notify.JS2D1Touch:
                    break;
                case Mainboard.Notify.JS2D2Touch:
                    break;
                case Mainboard.Notify.JS2D3Touch:
                    break;
                case Mainboard.Notify.RXCollid:
                    Console.WriteLine("Rx Collid");
                    break;
                case Mainboard.Notify.NotifyEnd:
                    break;
                default:
                    break;
            }
        }

        private void _mainboard_StringInfoReceived(object sender, string e)
        {
            Console.Write(e);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private  void btSend_Click(object sender, EventArgs e)
        {
           

            //if ( await _mainboard.WrtieReg(Mainboard.WritableRegs.XDir, val))
            //    Console.WriteLine("xdir wroten succ");
            //else
            //    Console.WriteLine("xdir wrtoen fail");
            //if (_mainboard.WrtieReg(Mainboard.WritableRegs.MotoEn, val).Result)
            //    Console.WriteLine("Motoen wroten");
            //else
            //    Console.WriteLine("Motoen wroten fail");
            CallingAsync();
            Console.WriteLine("Calling complete");
        }
        private async void CallingAsync()
        {
            UInt16 val;

            if (chkSet.Checked)
                val = 1;
            else
                val = 0;
            Console.WriteLine("begin calling");
            Task<bool> t1 = _mainboard.WriteReg(Mainboard.WritableRegs.XDir, val);
            Task<bool> t2 = _mainboard.WriteReg(Mainboard.WritableRegs.MotoEn, val);

            Console.WriteLine("end calling");
            bool rs1 = await t1;
            bool rs2 = await t2;
            Console.WriteLine($"t1 result = {rs1}");
            Console.WriteLine($"t2 result = {rs2}");
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            if (await _mainboard.MotoMove(Mainboard.Axis.Y, 200, 60000, 600000, 6000))
                Console.WriteLine("Moto move succ");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _mainboard.Dispose();
        }

        private async void methodasync()
        {
            if (true == await _mainboard.WriteReg(Mainboard.WritableRegs.SW5V, 1))
                Console.WriteLine("5v ctrl");
        }
        private void button2_Click(object sender, EventArgs e)
        {
            methodasync();
            Console.WriteLine("calling  finish");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _mainboard.ResetToISP();
        }
    }
}

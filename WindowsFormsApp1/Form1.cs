using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        Timer _timer;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //System.IO.Ports.SerialPort sp = new System.IO.Ports.SerialPort("COM8");
            //_mainboard = new Mainboard(sp);
            comboBox1.DataSource = Enum.GetValues(typeof(Mainboard.Axis));
            comboBox1.SelectedIndex = 0;
            _mainboard = Mainboard.FindDevice();
            if (_mainboard != null)
            {
                _mainboard.StringInfoReceived += _mainboard_StringInfoReceived;
                _mainboard.NotifyReceived += _mainboard_NotifyReceived;
                _timer = new Timer();
                _timer.Interval = 200;
                _timer.Tick += _timer_Tick;
                _timer.Start();
            }
        }

        private async void timer_tick_calling()
        {
            UInt16 rs = await _mainboard.GetXZeroVal();
            if (rs == 0)
                chkXSW0.Checked = true;
            else if (rs == 1)
                chkXSW0.Checked = false;
            rs = await _mainboard.GetXNearVal();
            if (rs == 0)
                chkXSW1.Checked = true;
            else
                chkXSW1.Checked = false;
            rs = await _mainboard.GetXFarVal();
            if (rs == 0)
                chkXSW2.Checked = true;
            else
                chkXSW2.Checked = false;

            rs = await _mainboard.GetYZeroVal();
            if (rs == 0)
                chkYSW0.Checked = true;
            else
                chkYSW0.Checked = false;

            rs = await _mainboard.GetYNearVal();
            if (rs == 0) 
                chkYSW1.Checked = true;
            else 
                chkYSW1.Checked = false;

            rs = await _mainboard.GetYFarVal();
            if (rs == 0)
                chkYSW2.Checked = true;
            else
                chkYSW2.Checked = false;
        }
        private void _timer_Tick(object sender, EventArgs e)
        {
            timer_tick_calling();
        }

        private void _mainboard_NotifyReceived(object sender, Mainboard.NotifyEventArgs e)
        {
            Console.WriteLine(e.Notify.ToString());

        }

        private void _mainboard_StringInfoReceived(object sender, string e)
        {
            Console.Write(e);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Mainboard.Axis axis = (Mainboard.Axis)comboBox1.SelectedItem;
            int steps =(int) nudSteps.Value;
            int speed = (int)nudSpeed.Value;
            int acc = (int)nudAcc.Value;
            if (chkNeg.Checked)
                steps = 0 - steps;
            if (await _mainboard.MotoMove(axis,(byte)acc,(ushort)speed,steps, 6000))
                Console.WriteLine("Moto move succ");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            _mainboard?.Dispose();
        }

        UInt16 valsw = 0;
        private void button2_Click(object sender, EventArgs e)
        {

            if (valsw == 0)
                _mainboard.Fan2On();
            else
                _mainboard.Fan2Off();
            valsw = (UInt16)(valsw ^ 1);
        }
        private double acceleratingtime(int acc, int alignment, int startspeed, int targetspeed)
        {
            double time = 0.0;
            double currspeed = startspeed;
            int steps = (targetspeed - startspeed) / acc;
            Console.WriteLine($"accelerating steps = {steps} ");
            double t = 0;
            while (steps > 0)
            {
                if (steps >= alignment)
                {
                    t = 1 / currspeed * alignment;
                    currspeed += acc * alignment;
                    steps -= alignment;
                }
                else if (steps > 0)
                {
                    t = 1 / currspeed;
                    currspeed += acc;
                    steps--;
                }
                time += t;
                Console.WriteLine($"Spend time:{t},Remaind steps:{steps}");
            }
            return time;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (valsw > 0)
                _mainboard.HighPowerPort2Off();
            else
                _mainboard.HighPowerPort2On();
            valsw = (UInt16)(valsw ^ 1);
        }

        private void btFan1_Click(object sender, EventArgs e)
        {
            if (valsw > 0)
                _mainboard.Fan1Off();
            else
                _mainboard.Fan1On();
            valsw = (UInt16)(valsw ^ 1);
        }

        private void btVout_Click(object sender, EventArgs e)
        {
            if (valsw > 0)
            { _mainboard.VoutOff(); }
            else
            {
                _mainboard.VoutOn();
            }
            valsw = (UInt16)(valsw ^ 1);
        }

        private void btVcc_Click(object sender, EventArgs e)
        {
            if (valsw > 0)
                _mainboard.VccOff();
            else
                _mainboard.VccOn();
            valsw = (UInt16)(valsw ^ 1);
        }

        private void btLed_Click(object sender, EventArgs e)
        {
            if (valsw > 0)
                _mainboard.LedOff();
            else
                _mainboard.LedOn();
            valsw = (UInt16)(valsw ^ 1);
        }

        private void btBuzzer_Click(object sender, EventArgs e)
        {
            if (valsw > 0)
                _mainboard.BuzzerOff();
            else
                _mainboard.BuzzerOn();
            valsw = (UInt16)(valsw ^ 1);
        }

        private void btLamp_Click(object sender, EventArgs e)
        {
            if (valsw > 0)
                _mainboard.LampOff();
            else
                _mainboard.LampOn();
            valsw = (UInt16)(valsw ^ 1);
        }

        private void btLaser_Click(object sender, EventArgs e)
        {
            if (valsw > 0)
                _mainboard.LaserOff();
            else
                _mainboard.LaserOn();
            valsw = (UInt16)(valsw ^ 1);
        }

        private void btHPO1_Click(object sender, EventArgs e)
        {
            if (valsw > 0)
                _mainboard.HighPowerPort1Off();
            else
                _mainboard.HighPowerPort1On();
            valsw = (UInt16)(valsw ^ 1);
        }

        private async void button2_Click_1(object sender, EventArgs e)
        {
            if (await _mainboard.MotoReset((Mainboard.Axis) comboBox1.SelectedItem, 20000))
                Console.WriteLine("Reset send succ");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Console.WriteLine($"total time = {acceleratingtime(8, 16, 1000, 60000)}");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            timer_tick_calling();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btISP_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter ="BIN File| *.bin";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(ofd.FileName, FileMode.Open);
                if (_mainboard.UpdateFirmware(fs))
                    MessageBox.Show("Update succeed");
                else
                    MessageBox.Show("Update failed");
            }

        }
    }
}

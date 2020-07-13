using System;
using System.IO;
using System.IO.Enumeration;
using System.IO.Pipes;
namespace xze.testbenchmainboard
{
    
    public class MainBoard
    {
        public enum WritableRegs
        {
            Fan1 = 0x0100,
            Fan2,
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
            ZPul
        }
        
        //#define REG_FAN1_W      0x0100
        const UInt16 REG_FAN1_W = 0X0100;
        //#define REG_FAN2_W      0x0101
        const UInt16 REG_FAN2_W = 0x0101;
        //#define REG_SW5V_W      0x0102
        const UInt16 REG_SW5V_W = 0x0102;
        //#define REG_SW24V_W     0X0103
        const UInt16 REG_SW24V_W = 0X0103;
        //#define REG_LAMP_W      0x0104
        const UInt16 REG_LAMP_W = 0x0104;
        //#define REG_LED_W       0x0105
        const UInt16 REG_LED_W = 0x0105;
        //#define REG_LASER_W     0x0106
        const UInt16 REG_LASER_W = 0x0106;
        //#define REG_BUZZER_W    0x0107
        const UInt16 REG_BUZZER_W = 0x0107;
        //#define REG_XDIR_W      0x0108
        const UInt16 REG_XDIR_W = 0x0108;
        //#define REG_YDIR_W      0x0109
        const UInt16 REG_YDIR_W = 0x0109;
        //#define REG_ZDIR_W      0x010A
        const UInt16 REG_ZDIR_W = 0x010A;
        //#define REG_DRVEN_W     0x010B
        const UInt16 REG_DRVEN_W = 0x010B;
        //#define REG_XPUL_W      0x010C
        const UInt16 REG_XPUL_W = 0x010C;
        //#define REG_YPUL_W      0x010D
        const UInt16 REG_YPUL_W = 0x010D;
        //#define REG_ZPUL_W      0x010E
        const UInt16 REG_ZPUL_W = 0x010E;
       public enum ReadableRegs
        {
            Protect = 0x0200,
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
            ZSW0,
            ZSW1,
            ZSW2,
            NTC1,
            NTC2,
            NTC3,
            MotoCurr,
           RegsNumber    
        }
        //#define REG_PROT_R      0x0200
        const UInt16 REG_PROT_R = 0x0200;
        //#define REG_START_R     0x0201
        const UInt16 REG_START_R = 0x0201;
        //#define REG_JS1BT_R     0x0202
        const UInt16 REG_JS1BT_R = 0x0202;
        //#define REG_JS1D0_R     0x0203
        const UInt16 REG_JS1D0_R = 0x0203;
        //#define REG_JS1D1_R     0x0204
        const UInt16 REG_JS1D1_R = 0x0204;
        //#define REG_JS1D2_R     0x0205
        const UInt16 REG_JS1D2_R = 0x0205;
        //#define REG_JS1D3_R     0x0206
        const UInt16 REG_JS1D3_R = 0x0206;
        //#define REG_JS2BT_R     0x0207
        const UInt16 REG_JS2BT_R = 0x0207;
        //#define REG_JS2D0_R     0x0208
        const UInt16 REG_JS2D0_R = 0x0208;
        //#define REG_JS2D1_R     0x0209
        const UInt16 REG_JS2D1_R = 0x0209;
        //#define REG_JS2D2_R     0x020A
        const UInt16 REG_JS2D2_R = 0x020A;
        //#define REG_JS2D3_R     0x020B
        const UInt16 REG_JS2D3_R = 0x020B;
        //#define REG_XSW0_R      0x020C
        const UInt16 REG_XSW0_R = 0x020C;
        //#define REG_XSW1_R      0x020D
        const UInt16 REG_XSW1_R = 0x020D;
        //#define REG_XSW2_R      0x020E
        const UInt16 REG_XSW2_R = 0x020E;
        //#define REG_YSW0_R      0x020F
        const UInt16 REG_YSW0_R = 0x020F;
        //#define REG_YSW1_R      0x0210
        const UInt16 REG_YSW1_R = 0x0210;
        //#define REG_YSW2_R      0x0211
        const UInt16 REG_YSW2_R = 0x0211;
        //#define REG_ZSW0_R      0x0212
        const UInt16 REG_ZSW0_R = 0x0212;
        //#define REG_ZSW1_R      0x0213
        const UInt16 REG_ZSW1_R = 0x0213;
        //#define REG_ZSW2_R      0x0214
        const UInt16 REG_ZSW2_R = 0x0214;
        //#define REG_NTC1_R      0x0215
        const UInt16 REG_NTC1_R = 0x0215;
        //#define REG_NTC2_R      0x0216
        const UInt16 REG_NTC2_R = 0x0216;
        //#define REG_NTC3_R      0x0217
        const UInt16 REG_NTC3_R = 0x0217;
        //#define REG_MOTCUR_R    0x0218
        const UInt16 REG_MOTCUR_R = 0x0218;

        //#define CMD_MOTO_MOVE   0x0300  /* DATA 8 Bytes: byte0 axis,byte1 acc, byte2:byte3 speed,byte4:byte7 step */
        const UInt16 CMD_MOTO_MOVE = 0x0300;
        //#define CMD_MOTO_RST    0x0301  
        const UInt16 CMD_MOTO_RST = 0x0301;
        //#define NTF_MOTO_COMP   0x0400
        //#define NTF_MOTO_OT     0x0401


        private byte[] create 
        public UInt16 ReadReg(ReadableRegs rr)
        {
            UInt16 val = 0;

            return val;
        }
    }
}

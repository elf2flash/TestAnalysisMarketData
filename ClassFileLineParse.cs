using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Test_ReadFile
{
    class ClassFileLineParse
    {
        // Class members.
        //
        // Property.
        public int Number { get; set; }

        //<TICKER>;<PER>;<DATE>;<TIME>;<OPEN>;<HIGH>;<LOW>;<CLOSE>;<VOL>
        //GAZP;1;20200406;100100;192.2500000;193.4400000;192.1200000;193.1500000;701990
        public string str_Ticker { get; set; }
        public int i_Per { get; set; }
        public string str_Date { get; set; }
        public int i_DateY { get; set; }
        public int i_DateM { get; set; }
        public int i_DateD { get; set; }
        public string str_Time { get; set; }
        public int i_TimeH { get; set; }
        public int i_TimeM { get; set; }
        public int i_TimeS { get; set; }
        public float fl_Open { get; set; }
        public float fl_High { get; set; }
        public float fl_Low { get; set; }
        public float fl_Close { get; set; }
        public int i_Vol { get; set; }


        // Method.
        public int Multiply(int num)
        {
            return num * Number;
        }
        public int parse(string[] in_mstr_FileLineWords)
        {
            str_Ticker = in_mstr_FileLineWords[0];
            int itmp = 0;
            if (Int32.TryParse(in_mstr_FileLineWords[1], out itmp))
                i_Per = itmp;
            else
                i_Per = int.MaxValue;
            //--------------
            str_Date = in_mstr_FileLineWords[2];
            if (str_Date.Length == 8)
            {
                if (Int32.TryParse(str_Date.Substring(0, 4), out itmp))
                    i_DateY = itmp;
                else
                    i_DateY = int.MaxValue;
                if (Int32.TryParse(str_Date.Substring(4, 2), out itmp))
                    i_DateM = itmp;
                else
                    i_DateM = int.MaxValue;
                if (Int32.TryParse(str_Date.Substring(6, 2), out itmp))
                    i_DateD = itmp;
                else
                    i_DateD = int.MaxValue;
            }
            //--------------
            str_Time = in_mstr_FileLineWords[3];
            if (str_Time.Length == 6)
            {
                if (Int32.TryParse(str_Time.Substring(0, 2), out itmp))
                    i_TimeH = itmp;
                else
                    i_TimeH = int.MaxValue;
                if (Int32.TryParse(str_Time.Substring(2, 2), out itmp))
                    i_TimeM = itmp;
                else
                    i_TimeM = int.MaxValue;
                if (Int32.TryParse(str_Time.Substring(4, 2), out itmp))
                    i_TimeS = itmp;
                else
                    i_TimeS = int.MaxValue;
            }
            //--------------
            float ftmp = 0.0f;
            if (float.TryParse(in_mstr_FileLineWords[4], NumberStyles.Any, CultureInfo.InvariantCulture.NumberFormat, out ftmp))
                fl_Open = ftmp;
            else
                fl_Open = int.MaxValue;
            if (float.TryParse(in_mstr_FileLineWords[5], NumberStyles.Any, CultureInfo.InvariantCulture.NumberFormat, out ftmp))
                fl_High = ftmp;
            else
                fl_High = int.MaxValue;
            if (float.TryParse(in_mstr_FileLineWords[6], NumberStyles.Any, CultureInfo.InvariantCulture.NumberFormat, out ftmp))
                fl_Low = ftmp;
            else
                fl_Low = int.MaxValue;
            if (float.TryParse(in_mstr_FileLineWords[7], NumberStyles.Any, CultureInfo.InvariantCulture.NumberFormat, out ftmp))
                fl_Close = ftmp;
            else
                fl_Close = int.MaxValue;
            if (Int32.TryParse(in_mstr_FileLineWords[8], out itmp))
                i_Vol = itmp;
            else
                i_Vol = int.MaxValue;
            return 0;
        }

        public int disp()
        {
            string str_tmp = "";
            str_tmp += str_Ticker;
            str_tmp += " " + i_Per;
            if (str_Date.Length == 8)
            {
                str_tmp += " " + str_Date.Substring(0, 4);
                str_tmp += "." + str_Date.Substring(4, 2);
                str_tmp += "." + str_Date.Substring(6, 2);
            }
            else
            {
                str_tmp += str_Date;
            }
            //str_tmp += " " + i_DateY;
            //str_tmp += " " + i_DateM;
            //str_tmp += " " + i_DateD;
            if (str_Time.Length == 6)
            {
                str_tmp += " " + str_Time.Substring(0, 2);
                str_tmp += ":" + str_Time.Substring(2, 2);
                str_tmp += ":" + str_Time.Substring(4, 2);
            }
            else
            {
                str_tmp += " " + str_Time;
            }
            str_tmp += " " + fl_Open;
            str_tmp += " " + fl_High;
            str_tmp += " " + fl_Low;
            str_tmp += " " + fl_Close;
            str_tmp += " " + i_Vol;
            Console.WriteLine(str_tmp);
            return 0;
        }

        // Instance Constructor.
        public ClassFileLineParse()
        {
            Number      = 0;
            str_Ticker  = "";
            i_Per       = 0;
            str_Date    = "";
            i_DateY     = 0;
            i_DateM     = 0;
            i_DateD     = 0;
            str_Time    = "";
            fl_Open     = 0.0f;
            fl_High     = 0.0f;
            fl_Low      = 0.0f;
            fl_Close    = 0.0f;
            i_Vol       = 0;
        }
    }
}

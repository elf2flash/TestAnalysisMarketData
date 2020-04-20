using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_ReadFile
{
    class ClassStatistic
    {
        // Class members.
        //
        // Property.
        public bool b_InitFlag { get; set; }
        public bool b_DispDayStat { get; set; } // печатать ежедненвную статистику
        public int i_LockMinutes { get; set; }  // отступаем i_LockMinutes минут с начала торгов и i_LockMinutes минут до окончания торгов
        public string str_Ticker { get; set; }
        public int i_PrevDateY { get; set; }
        public int i_PrevDateM { get; set; }
        public int i_PrevDateD { get; set; }
        public int i_PrevTimeH { get; set; }
        public int i_PrevTimeM { get; set; }
        public int i_PrevTimeS { get; set; }
        public float fl_PriceMax { get; set; }
        public float fl_PriceMin { get; set; }
        public float fl_DayPriceMax { get; set; }
        public float fl_DayPriceMin { get; set; }
        public float fl_TopCh { get; set; }
        public float fl_BottomCh { get; set; }
        public float fl_DayTopCh { get; set; }
        public float fl_DayBottomCh { get; set; }
        public string str_FirstDate { get; set; }
        public string str_FirstTime { get; set; }
        public string str_PrevDate { get; set; }
        public string str_PrevTime { get; set; }
        public float fl_PrevMHigh { get; set; }
        public float fl_PrevMLow { get; set; }
        
        
        // Instance Constructor.
        public ClassStatistic()
        {
            b_InitFlag      = false;
            b_DispDayStat   = false;
            i_LockMinutes   = 0;
            str_Ticker = "Ticker";
            //
            i_PrevDateY     = 0;
            i_PrevDateM     = 0;
            i_PrevDateD     = 0;
            i_PrevTimeH     = 0;
            i_PrevTimeM     = 0;
            i_PrevTimeS     = 0;
            //
            fl_PriceMax     = 0.0f;
            fl_PriceMin     = 0.0f;
            fl_DayPriceMax  = 0.0f;
            fl_DayPriceMin  = 0.0f;
            //
            fl_TopCh        = 0.0f;
            fl_BottomCh     = 0.0f;
            fl_DayTopCh     = 0.0f;
            fl_DayBottomCh  = 0.0f;
            //
            str_FirstDate   = "";
            str_FirstTime   = "";
            str_PrevDate    = "";
            str_PrevTime    = "";
            //
            fl_PrevMHigh    = 0.0f;
            fl_PrevMLow     = 0.0f;
        }

        // Method.
        public int calcDay(int in_i_DateY, int in_i_DateM, int in_i_DateD, int in_i_TimeH, int in_i_TimeM, int in_i_TimeS, float in_fl_MHigh, float in_fl_MLow)
        {
            string str_tmp0, str_tmp1;
            if (!b_InitFlag)
            {
                b_InitFlag = true;
                fl_PriceMax = in_fl_MHigh;
                fl_PriceMin = in_fl_MLow;
                if (in_i_DateM < 10)
                    str_tmp0 = "0" + in_i_DateM.ToString();
                else
                    str_tmp0 = in_i_DateM.ToString();
                if (in_i_DateD < 10)
                    str_tmp1 = "0" + in_i_DateD.ToString();
                else
                    str_tmp1 = in_i_DateD.ToString();
                str_FirstDate = str_tmp1 + "." + str_tmp0 + "." + in_i_DateY.ToString();
                if (in_i_TimeH < 10)
                    str_tmp0 = "0" + in_i_TimeH.ToString();
                else
                    str_tmp0 = in_i_TimeH.ToString();
                if (in_i_TimeM < 10)
                    str_tmp1 = "0" + in_i_TimeM.ToString();
                else
                    str_tmp1 = in_i_TimeM.ToString();
                str_FirstTime = str_tmp0 + ":" + str_tmp1 + ":" + "00";
            }
            float fl_delta_tmp = 0.0f;
            // в начале нового дня обновляем предыдущие значения минуток
            if (i_PrevDateD != in_i_DateD || (i_PrevDateD == in_i_DateD && i_PrevDateM != in_i_DateM) || (i_PrevDateD == in_i_DateD && i_PrevDateM == in_i_DateM && i_PrevDateY != in_i_DateY))
            {
                fl_DayTopCh = 0.0f;
                fl_DayBottomCh = 0.0f;
                fl_DayPriceMax = in_fl_MHigh;
                fl_DayPriceMin = in_fl_MLow;
            }
            else if (i_LockMinutes > 0 && ((in_i_TimeH == 10 && in_i_TimeM < i_LockMinutes) || (in_i_TimeH == 18 && in_i_TimeM > 50 - i_LockMinutes)))
            {
                // отступаем i_LockMinutes минут с начала торгов и i_LockMinutes минут до окончания торгов
            }
            // с приходом новой минутки рассчитываем изменения за день
            else
            {
                if (i_PrevDateY == in_i_DateY && i_PrevDateM == in_i_DateM && i_PrevDateD == in_i_DateD)
                {
                    if ((i_PrevTimeM == 59 && i_PrevTimeH == in_i_TimeH + 1 && in_i_TimeM == 0) || (i_PrevTimeH == in_i_TimeH && i_PrevTimeM + 1 == in_i_TimeM))
                    {
                        if (in_fl_MHigh > fl_PrevMHigh)
                        {
                            fl_delta_tmp = 1 - (fl_PrevMHigh / in_fl_MHigh);
                            if (fl_DayTopCh < fl_delta_tmp)
                                fl_DayTopCh = fl_delta_tmp;
                        }
                        else
                        {
                            fl_delta_tmp = 1 - (in_fl_MHigh / fl_PrevMHigh);
                            if (fl_DayBottomCh < fl_delta_tmp)
                                fl_DayBottomCh = fl_delta_tmp;
                        }
                    }
                }
            }
            i_PrevDateY = in_i_DateY;
            i_PrevDateM = in_i_DateM;
            i_PrevDateD = in_i_DateD;
            i_PrevTimeH = in_i_TimeH;
            i_PrevTimeM = in_i_TimeM;
            i_PrevTimeS = in_i_TimeS;
            //
            fl_PrevMHigh = in_fl_MHigh;
            fl_PrevMLow = in_fl_MLow;
            if (fl_PriceMax < in_fl_MHigh)
                fl_PriceMax = in_fl_MHigh;
            if (fl_PriceMin > in_fl_MLow)
                fl_PriceMin = in_fl_MLow;
            if (fl_DayPriceMax < in_fl_MHigh)
                fl_DayPriceMax = in_fl_MHigh;
            if (fl_DayPriceMin > in_fl_MLow)
                fl_DayPriceMin = in_fl_MLow;
            if (fl_TopCh < fl_DayTopCh)
                fl_TopCh = fl_DayTopCh;
            if (fl_BottomCh < fl_DayBottomCh)
                fl_BottomCh = fl_DayBottomCh;
            /////////////////
            if (in_i_DateM < 10)
                str_tmp0 = "0" + in_i_DateM.ToString();
            else
                str_tmp0 = in_i_DateM.ToString();
            if (in_i_DateD < 10)
                str_tmp1 = "0" + in_i_DateD.ToString();
            else
                str_tmp1 = in_i_DateD.ToString();
            str_PrevDate = str_tmp1 + "." + str_tmp0 + "." + in_i_DateY.ToString();
            if (in_i_TimeH < 10)
                str_tmp0 = "0" + in_i_TimeH.ToString();
            else
                str_tmp0 = in_i_TimeH.ToString();
            if (in_i_TimeM < 10)
                str_tmp1 = "0" + in_i_TimeM.ToString();
            else
                str_tmp1 = in_i_TimeM.ToString();
            str_PrevTime = str_tmp0 + ":" + str_tmp1 + ":" + "00";
            /////////////////
            if (b_DispDayStat && in_i_TimeH == 18 && in_i_TimeM == 50)
                DispDayStat();
            return 0;
        }
        public int DispDayStat()
        {
            string str_tmp = "";
            str_tmp += i_PrevDateY + "/" + i_PrevDateM + "/" + i_PrevDateD;
            str_tmp += "  DayPrice: Max / Min    = " + fl_DayPriceMax + "";
            str_tmp += " / " + fl_DayPriceMin + "\n";
            str_tmp += " DayChange Percent: Top / Bottom    = " + fl_DayTopCh*100.0f + "%";
            str_tmp += " / " + fl_DayBottomCh*100.0f + "%\n";
            Console.WriteLine(str_tmp);
            return 0;
        }
        public int DispFinishStat()
        {
            //string str_tmp = "";
            //str_tmp += " Price: Max / Min    = " + fl_PriceMax + "";
            //str_tmp += " / " + fl_PriceMin + "\n";
            //str_tmp += " Change Percent: Top / Bottom = " + fl_TopCh * 100.0f + "%";
            //str_tmp += " / " + fl_BottomCh * 100.0f + "%\n";
            //Console.WriteLine(str_tmp);
            Console.WriteLine(" ===" + str_Ticker + "===  (" + str_FirstDate + " - " + str_PrevDate + ")");
            Console.WriteLine("{0,10}    |{1,10}", fl_PriceMax, fl_PriceMin);
            Console.WriteLine("{0,10}%   |{1,10}%", fl_TopCh * 100.0f, fl_BottomCh * 100.0f);
            return 0;
        }
        
    }
}

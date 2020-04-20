using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Test_ReadFile
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string[] m_str_Files;
            try
            {
                m_str_Files = Directory.GetFiles(@"MarketData1");
            }
            catch (UnauthorizedAccessException uex)
            {
                Console.WriteLine("catch (UnauthorizedAccessException uex)");
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine("catch (Exception ex)");
                return;
            }

            //Console.WriteLine(m_str_Files[0]);

            string path = @"SBER_200101_200413.txt";
            //string path = @"GAZP_200406_200406.txt";
            //string path = @"GAZP_200406_200406_small.txt";
            foreach (var str_FileName in m_str_Files)
            {
                ClassFileLineParse cFileLineParse = new ClassFileLineParse();
                ClassStatistic cStatistic = new ClassStatistic();
                cStatistic.i_LockMinutes = 15;      // отступаем i_LockMinutes минут с начала торгов и i_LockMinutes минут до окончания торгов
                cStatistic.b_DispDayStat = false;   // 
                try
                {
                    using (StreamReader sr = new StreamReader(str_FileName, System.Text.Encoding.Default))
                    {
                        string str_FileLine;
                        string[] mstr_FileLineWords;
                        string str_tmp = "";
                        int k = 0;
                        while ((str_FileLine = sr.ReadLine()) != null)
                        {
                            mstr_FileLineWords = str_FileLine.Split(';');
                            cFileLineParse.parse(mstr_FileLineWords);
                            if (k == 1)
                                cStatistic.str_Ticker = cFileLineParse.str_Ticker;
                            if (k>0)
                                cStatistic.calcDay(cFileLineParse.i_DateY, cFileLineParse.i_DateM, cFileLineParse.i_DateD,
                                                    cFileLineParse.i_TimeH, cFileLineParse.i_TimeM, cFileLineParse.i_TimeS,
                                                    cFileLineParse.fl_High, cFileLineParse.fl_Low);
                            k++;
                            //foreach (string istr in mstr_FileLineWords)
                            //    Console.WriteLine(istr);
                            //cFileLineParse.disp();
                        }
                        //Console.WriteLine(str_tmp);

                        Console.WriteLine("------------------------\n");
                        cStatistic.DispFinishStat();
                        //Console.WriteLine("\nEnd!\n");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}


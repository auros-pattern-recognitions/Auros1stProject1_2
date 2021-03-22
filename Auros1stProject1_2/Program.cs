using System;
using static System.Console;
using System.IO;
using System.Collections;

namespace Auros1stProject1_2
{
    class Program
    {
        //si정보를 변환 함수(ev(광자 에너지), e1(유전상수실수부), e2(유전상수허수부))
        static void si_ChangeToNm(string[] a, string[] b, string[] c, int length)
        {
            // 문자열을 숫자로 변경
            double[] ev = new double[length - 1];
            double[] e1 = new double[length - 1];
            double[] e2 = new double[length - 1];
            for (int i = 0; i < length - 1; i++)
            {
                ev[i] = double.Parse(a[i + 1]);
                e1[i] = double.Parse(b[i + 1]);
                e2[i] = double.Parse(c[i + 1]);
            }

            // nm, n, k로 데이터 변환
            double[] Nm = new double[length - 1];
            double[] N = new double[length - 1];
            double[] K = new double[length - 1];
            for (int i = 0; i < length - 1; i++)
            {
                double sum = Math.Pow(e1[i], 2) + Math.Pow(e2[i], 2);
                double Nroot = 0.5 * (Math.Sqrt(sum) + e1[i]);
                double Kroot = 0.5 * (Math.Sqrt(sum) - e1[i]);
                N[i] = Math.Sqrt(Nroot);
                K[i] = Math.Sqrt(Kroot);
                // 1nm는 10의 -9승
                // h(플랑크 상수) = 4.126*10^(-15) eV*s // c(빛의 속도) = 3 * 10^(8) m/s
                Nm[i] = (((4.136 * Math.Pow(10, -15)) * (3 * Math.Pow(10, 8))) / (ev[i])) * Math.Pow(10, 9);
            }

            // 데이터 확인
            for (int i = 0; i < length - 1; i++)
            {
                WriteLine($"{Nm[i]}     {N[i]:N3}     {K[i]:N3}");
            }

            // 텍스트에 저장
            StreamWriter writer;
            writer = File.CreateText("Si_nm.txt");
            writer.Write("nm n k\n");
            for (int i = 0; i < length - 1; i++)
            {
                writer.Write(Nm[i]);
                writer.Write("\t");
                writer.Write(N[i]);
                writer.Write("\t");
                writer.Write(K[i]);
                writer.Write("\n");
            }
            writer.Close();
        }

        //sio2정보를 변환 함수(ev(광자 에너지), e1(유전상수실수부), e2(유전상수허수부))
        static void sio2_ChangeToNm(string[] a, string[] b, string[] c, int length)
        {
            // 문자열을 숫자로 변경
            double[] Angstrom = new double[length - 1];
            double[] N = new double[length - 1];
            double[] K = new double[length - 1];
            for (int i = 0; i < length - 1; i++)
            {
                Angstrom[i] = double.Parse(a[i + 1]);
                N[i] = double.Parse(b[i + 1]);
                K[i] = double.Parse(c[i + 1]);
            }

            // 단위 변경
            double[] Nm = new double[length - 1];
            for (int i = 0; i < length - 1; i++)
            {
                // 옹스트롱(10의 -10승)을 나노미터(10의 -9승) 단위로 변경
                Nm[i] = Angstrom[i] * 0.1;
            }

            // 데이터 확인
            for (int i = 0; i < length - 1; i++)
            {
                WriteLine($"{Nm[i]:N3}     {N[i]:N3}     {K[i]:N3}");
            }

            // 텍스트에 저장
            StreamWriter writer;
            writer = File.CreateText("SiO2_nm.txt");
            writer.Write("nm n k\n");
            for (int i = 0; i < length - 1; i++)
            {
                writer.Write(Nm[i]);
                writer.Write("\t");
                writer.Write(N[i]);
                writer.Write("\t");
                writer.Write(K[i]);
                writer.Write("\n");
            }
            writer.Close();
        }

        static void Main(string[] args)
        {
            //
            // 단위 변환.
            //
            // 2021.03.22 정지훈
            //
            #region si단위 변환
            // 파일 읽기
            string path1 = @"C:/Users\jungj/OneDrive/바탕 화면\프로젝트 자료/BIT 1차과제 자료_오로스테크놀로지/Si.txt";
            string si_textvalue = File.ReadAllText(path1);

            // 텍스트의 열 갯수 저장
            int si_linecount = File.ReadAllLines(path1).Length;

            // 광자에너지, 유전상수 실수부, 유전상수 허수부
            string[] si_ev = new string[si_linecount];
            string[] si_e1 = new string[si_linecount];
            string[] si_e2 = new string[si_linecount];

            // 데이터 분할
            char[] delimiterChars = { ' ', '\t', '\n' };
            string[] si_words = si_textvalue.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

            // 데이터를 배열에 저장
            int i = -1, a = 0, b = 0, c = 0;
            int array;
            foreach (string word in si_words)
            {
                i++;
                array = i % 3;

                switch (array)
                {
                    case 0:
                        {
                            si_ev[a] = word;
                            a++;
                        }
                        break;
                    case 1:
                        {
                            si_e1[b] = word;
                            b++;
                        }
                        break;
                    case 2:
                        {
                            si_e2[c] = word;
                            c++;
                        }
                        break;
                    default:
                        break;
                }
            }

            // 데이터 단위 변환
            si_ChangeToNm(si_ev, si_e1, si_e2, si_linecount);
            WriteLine("==================================================");

            // 데이터 확인
            /*for (int j = 0; j < si_linecount; j++)
            {
                WriteLine($"{si_ev[j]} {si_e1[j]} {si_e2[j]}");
            }*/
            WriteLine("==================================================");
            #endregion

            #region sio2단위 변환
            // 데이터 읽기
            string path2 = @"C:/Users\jungj/OneDrive/바탕 화면\프로젝트 자료/BIT 1차과제 자료_오로스테크놀로지/SIO2.txt";
            string sio2_textvalue = File.ReadAllText(path2);

            // 텍스트 열의 갯수 저장
            int sio2_linecount = File.ReadAllLines(path2).Length;

            //
            string[] sio2_Angstrom = new string[sio2_linecount];
            string[] sio2_N = new string[sio2_linecount];
            string[] sio2_K = new string[sio2_linecount];

            // 데이터 분할
            string[] sio2_words = sio2_textvalue.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

            // 데이터를 배열에 저장
            i = -1; a = 0; b = 0; c = 0;
            foreach (string word in sio2_words)
            {
                i++;
                array = i % 3;

                switch (array)
                {
                    case 0:
                        {
                            sio2_Angstrom[a] = word;
                            a++;
                        }
                        break;
                    case 1:
                        {
                            sio2_N[b] = word;
                            b++;
                        }
                        break;
                    case 2:
                        {
                            sio2_K[c] = word;
                            c++;
                        }
                        break;
                    default:
                        break;
                }
            }

            // 데이터 단위 변환
            sio2_ChangeToNm(sio2_Angstrom, sio2_N, sio2_K, sio2_linecount);

            // 데이터 확인
            /*for (int j = 0; j < sio2_linecount; j++)
            {
                WriteLine($"{sio2_Angstrom[j]} {sio2_N[j]} {sio2_K[j]}");
            }*/
            #endregion
        }
    }
}

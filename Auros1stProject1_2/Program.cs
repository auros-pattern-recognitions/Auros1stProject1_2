using System;
using static System.Console;
using System.IO;
using System.Collections;

namespace Auros1stProject1_2
{
    class Program
    {
        //si정보를 변환 함수(ev(광자 에너지), e1(유전상수실수부), e2(유전상수허수부))
        static void si_ChangeToNm(double[] ev, double[] e1, double[] e2, int length)
        {
            // nm, n, k로 데이터 변환
            double[] Nm = new double[length];
            double[] N = new double[length];
            double[] K = new double[length];
            for (int i = 0; i < length; i++)
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
            for (int i = 0; i < length; i++)
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
        static void sio2_ChangeToNm(double[] Angstrom, double[] N, double[] K, int length)
        {
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
            // 단위 변환(수정1).
            //
            // 2021.03.22 정지훈
            //
            #region si단위 변환
            // 파일 읽기
            string path1 = @"C:/Users\jungj/OneDrive/바탕 화면\프로젝트 자료/BIT 1차과제 자료_오로스테크놀로지/Si.txt";

            string[] MeasurementSpectrumData;   // 측정 스펙트럼 데이터 저장할 배열. (한 줄씩 저장)
            string[] SingleLineData;            // 한 줄의 스펙트럼 데이터를 임시로 저장할 배열.
            MeasurementSpectrumData = File.ReadAllLines(path1);

            // 텍스트의 열 갯수 저장
            int LoopNum = MeasurementSpectrumData.Length;

            // 광자에너지, 유전상수 실수부, 유전상수 허수부
            double[] si_ev = new double[LoopNum-1];
            double[] si_e1 = new double[LoopNum-1];
            double[] si_e2 = new double[LoopNum-1];

            // 데이터 분할
            char[] delimiterChars = { ' ', '\t', '\n' };

            // 데이터를 배열에 저장
            int startindex = 1;
            for (int i = startindex; i < LoopNum; i++)
            {
                SingleLineData = MeasurementSpectrumData[i].Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
                si_ev[i-1] = double.Parse(SingleLineData[0]);
                si_e1[i-1] = double.Parse(SingleLineData[1]);
                si_e2[i-1] = double.Parse(SingleLineData[2]);
            }

            // 데이터 단위 변환
            si_ChangeToNm(si_ev, si_e1, si_e2, LoopNum-1);
            WriteLine("==================================================");

            // 데이터 확인
            /*for (int j = 0; j < LoopNum-1; j++)
            {
                WriteLine($"{si_ev[j]} {si_e1[j]} {si_e2[j]}");
            }*/
            WriteLine("==================================================");
            #endregion

            #region sio2단위 변환
            // 데이터 읽기
            string path2 = @"C:/Users\jungj/OneDrive/바탕 화면\프로젝트 자료/BIT 1차과제 자료_오로스테크놀로지/SIO2.txt";
            string[] sio2_MeasurementSpectrumData;   // 측정 스펙트럼 데이터 저장할 배열. (한 줄씩 저장)
            string[] sio2_SingleLineData;            // 한 줄의 스펙트럼 데이터를 임시로 저장할 배열.
            sio2_MeasurementSpectrumData = File.ReadAllLines(path2);

            // 텍스트 열의 갯수 저장
            int sio2_LoopNum = sio2_MeasurementSpectrumData.Length;

            //
            double[] sio2_Angstrom = new double[sio2_LoopNum-1];
            double[] sio2_N = new double[sio2_LoopNum-1];
            double[] sio2_K = new double[sio2_LoopNum-1];

            // 데이터를 배열에 저장
            for (int i = startindex; i < sio2_LoopNum; i++)
            {
                sio2_SingleLineData = sio2_MeasurementSpectrumData[i].Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
                sio2_Angstrom[i - 1] = double.Parse(sio2_SingleLineData[0]);
                sio2_N[i - 1] = double.Parse(sio2_SingleLineData[1]);
                sio2_K[i - 1] = double.Parse(sio2_SingleLineData[2]);
            }

            // 데이터 단위 변환
            sio2_ChangeToNm(sio2_Angstrom, sio2_N, sio2_K, sio2_LoopNum);

            // 데이터 확인
            /*for (int j = 0; j < sio2_LoopNum-1; j++)
            {
                WriteLine($"{sio2_Angstrom[j]} {sio2_N[j]} {sio2_K[j]}");
            }*/
            #endregion
        }
    }
}

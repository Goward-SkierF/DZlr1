using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace ConsoleApp5
{
    public class Program
    {
        static void Main(string[] args)
        {
            
        }
        

    }
}
namespace MathUtilities
{
    public class InfinitesimalAnalyzer
    {
        private static readonly double[] PowersOfTen = new double[]
        {
            1, 1E-01, 1E-02, 1E-03, 1E-04, 1E-05, 1E-06, 1E-07,
            1E-08, 1E-09, 1E-10, 1E-11, 1E-12, 1E-13, 1E-14, 1E-15
        };

        public Func<double, double> Function
        {
            get;
            set;
        }
        public InfinitesimalAnalyzer(Func<double, double> function)
        {
            Function = function;
        }
        public bool IsInfinitesimal() => 
            Function(0) == 0;
        public TableItem[] GetTableRepresentation(uint start = 0, uint amount =16)
        {
            //if (amount > 16)
            //{
            //    throw new ArgumentException("количетсво ячеек < или = 16");
            //}

            //if (start + amount > 16)
            //{
            //    throw new ArgumentException("нельзя выходить за 16");
            //}


            TableItem[] answer = new TableItem[amount - start];
            for (int i = (int)start; i < amount; i++)
            {
                answer[i - start] = new TableItem { 
                    Input = PowersOfTen[i],
                    Output = Function(PowersOfTen[i]) };
            }

            return answer;
        }
        public Asymptote GetAsymptote() =>
            new Asymptote(Function);

        public double GetCoefficient() =>
            Pow(10, GetAsymptote().K);
    }
    public class TableItem
    {
        public double Input 
        { get; set; }
        public double Output 
        { get; set; }
        public double LgInput 
        { get => Log10(Input); }
        public double LgOutput 
        { get => Log10(Abs(Output)); }
    }
    public class Asymptote
    {
        public Asymptote(Func<double, double> function)
        {
            double left = Pow(10, -4);
            double right = Pow(10, -4.001);

            Alpha = LogOfAbs(function(right) / function(left)) / Log10(right /left);
        }

        private double LogOfAbs(double a) => 
            Log10(Abs(a));

        public double Alpha 
        { get; set; }

        public double K 
        { get; set; }
    }

}


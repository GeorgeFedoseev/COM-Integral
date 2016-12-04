using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpsonMethod {

    public class SimpsonIntCalculator : Component {

        Int64 N; // number of iterations

        double a, b; // limits
        Func<double, double> f; // function
        Dictionary<double, double> f_values; // funciton values

        public SimpsonIntCalculator(double _a, double _b, double maxDerivative, double eps, Func<double, double> _f, Dictionary<double, double> _f_values) {
            a = _a; b = _b; f = _f; f_values = _f_values;
            N = (Int64)Math.Sqrt((maxDerivative * Math.Pow(b - a, 5) / 2880 / eps));
            //Console.WriteLine("maxDerivative = " + maxDerivative);
            //Console.WriteLine("N = "+N);
        }

        public SimpsonIntCalculator(double _a, double _b, int _N, Func<double, double> _f, Dictionary<double, double> _f_values) {
            a = _a; b = _b; N = _N; f = _f; f_values = _f_values;
        }

        public double Calculate() {
            double step = (b - a) / N;
            double sum = 0;
            for (int i = 0; i < N; i++) {
                double a_local = a + i * step, b_local = a_local + step;

                double middle = (a_local + b_local) / 2;

                try {                
                    if (!f_values.ContainsKey(a_local)) {
                        f_values[a_local] = f(a_local);
                    }

                    if (!f_values.ContainsKey(b_local)) {
                        f_values[b_local] = f(b_local);
                    }

                    if (!f_values.ContainsKey(middle)) {
                        f_values[middle] = f(middle);
                    }
                } catch (SystemException e) {
                    f_values.Clear();
                    i--;
                    continue;
                }

                sum += (b_local - a_local)/6 * (f_values[a_local] + 4*f_values[middle] + f_values[b_local]) ;
            }
            return sum;
        }

        public Dictionary<double, double> GetComputedValues() {            
            return f_values;
        }
    }
}

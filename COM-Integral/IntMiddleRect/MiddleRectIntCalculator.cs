using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleRectsMethod {

    public class MiddleRectIntCalculator : Component {

        Int64 N; // number of iterations

        double a, b; // limits
        Func<double, double> f; // function
        Dictionary<double, double> f_values; // funciton values

        public MiddleRectIntCalculator(double _a, double _b, double maxDerivative, double eps, Func<double, double> _f, Dictionary<double, double> _f_values) {
            a = _a; b = _b; f = _f; f_values = _f_values;
            N = (Int64)Math.Sqrt((maxDerivative * Math.Pow(b - a, 3) / 24 / eps));
            Console.WriteLine("maxDerivative = " + maxDerivative);
            Console.WriteLine("N = "+N);
        }

        public MiddleRectIntCalculator(double _a, double _b, int _N, Func<double, double> _f, Dictionary<double, double> _f_values) {
            a = _a; b = _b; N = _N; f = _f; f_values = _f_values;
        }

        public double Calculate() {
            double step = (b - a) / N;
            double sum = 0;
            for (int i = 0; i < N; i++) {
                double a_local = a + i * step, b_local = a_local + step;

                var point = (a_local + b_local) / 2;

                try {
                    if (!f_values.ContainsKey(point)) {
                        f_values[point] = f(point);
                    }
                } catch (SystemException e) {
                    f_values.Clear();
                    i--;
                    continue;
                }
                

                sum += (b_local - a_local) * f_values[point];
            }
            return sum;
        }

        public Dictionary<double, double> GetComputedValues() {
            return f_values;
        }

    }
}

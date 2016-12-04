using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxDerivative
{
    public class MaxDerivativeCalculator : Component {
        Func<double, double> f;
        double a, b;

        public MaxDerivativeCalculator(Func<double, double> _f, double _a, double _b) {
            f = _f;
            a = _a;
            b = _b;
        }

        double Derivative(double x) {
            double h = 1e-6;
            return (f(x-h) -  2*f(x) + f(x+h))/ h / h;
        }

        public double Calculate() {
            double max = double.MinValue;
            int N = 1000;
            double step = (b - a) / N;

            for (int i = 0; i < N; i++) {
                var val = Math.Abs(Derivative(a + step * i));
              //  Console.WriteLine("derivative = "+val);
                if (val > max) {
                    max = val;
                }
            }

            return max;
        }
    }
}

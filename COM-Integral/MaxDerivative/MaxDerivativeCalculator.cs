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

        double SecondDerivative(double x, Func<double, double> _f) {
            double h = 1e-6;
            return (_f(x-h) -  2* _f(x) + _f(x+h))/ h / h;
        }

        public double Calculate2nd() {
            double max = double.MinValue;
            int N = 1000;
            double step = (b - a) / N;

            for (int i = 0; i < N; i++) {
                var val = Math.Abs(SecondDerivative(a + step * i, f));
              //  Console.WriteLine("derivative = "+val);
                if (val > max) {
                    max = val;
                }
            }

            return max;
        }

        public double Calculate4th() {
            double max = double.MinValue;
            int N = 1000;
            double step = (b - a) / N;

            for (int i = 0; i < N; i++) {
                var val = Math.Abs( SecondDerivative(a + step * i, (x) => { return SecondDerivative(x, f);  }) );
                //  Console.WriteLine("derivative = "+val);
                if (val > max) {
                    max = val;
                }
            }

            return max;
        }


    }
}

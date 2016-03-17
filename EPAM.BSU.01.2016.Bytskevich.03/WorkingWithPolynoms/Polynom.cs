using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WorkingWithPolynoms
{
    public class Polynom
    {
        public  double[] Coefficients { get; }

        public Polynom(params double[] coefficients)
        {
            if (coefficients.Length > 0)
            {
                this.Coefficients = new double[coefficients.Length];
                coefficients.CopyTo(this.Coefficients, 0);
            }
            else 
                throw new ArgumentException();
        }

        public double CalculateValue(double argument)
        {
            double result = 0;
            for (int i = 0; i < Coefficients.Length; i++)
                result += Coefficients[i]*Math.Pow(argument, i);
            return result;
        }

        public static Polynom operator +(Polynom lhs, Polynom rhs)
        {
            double[] longerArray = null, shorterArray = null;
            if (lhs.Coefficients.Length > rhs.Coefficients.Length)
            {
                longerArray = lhs.Coefficients;
                shorterArray = rhs.Coefficients;
            }
            else
            {
                longerArray = rhs.Coefficients;
                shorterArray = lhs.Coefficients;
            }
            double[] actualCoefficients = new double[longerArray.Length];
            longerArray.CopyTo(actualCoefficients, 0);
            for (int i = 0; i < shorterArray.Length; i++)
                checked
                {
                    actualCoefficients[i] = longerArray[i] + shorterArray[i];
                }
            return new Polynom(actualCoefficients);
        }

        public static Polynom operator -(Polynom lhs, Polynom rhs)
        {
            double[] longerArray = null, shorterArray = null;
            if (lhs.Coefficients.Length > rhs.Coefficients.Length)
            {
                longerArray = lhs.Coefficients;
                shorterArray = rhs.Coefficients;
            }
            else
            {
                longerArray = rhs.Coefficients;
                shorterArray = lhs.Coefficients;
            }
            double[] actualCoefficients = new double[longerArray.Length];
            longerArray.CopyTo(actualCoefficients, 0);
            for (int i = 0; i < shorterArray.Length; i++)
                actualCoefficients[i] = longerArray[i] - shorterArray[i];
            if(longerArray != lhs.Coefficients)
                for (int i = 0; i < actualCoefficients.Length; i++)
                    checked
                    {
                        actualCoefficients[i] = -actualCoefficients[i];
                    }
            return new Polynom(actualCoefficients);
        }

        public static Polynom operator *(Polynom lhs, Polynom rhs)
        {
            double[] actualCoefficients = new double[lhs.Coefficients.Length + rhs.Coefficients.Length - 1];
            for (int i = 0; i < lhs.Coefficients.Length; i++)
            {
                for (int j = 0; j < rhs.Coefficients.Length; j++)
                    actualCoefficients[i + j] += lhs.Coefficients[i]*rhs.Coefficients[j]; object o = new object(); 
            }           
            return new Polynom(actualCoefficients);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj.GetType() != this.GetType())
                return false;
            else if (this.Coefficients.Length != (obj as Polynom).Coefficients.Length)
                return false;
            else
                return this.Coefficients.SequenceEqual((obj as Polynom).Coefficients);
        }

        public override string ToString()
        {
            string result = "";
            if (this.Coefficients[0] != 0)
                result += this.Coefficients[0].ToString() + " ";
            if (this.Coefficients[1] != 0)
            {
                if (this.Coefficients[1] > 0 && result != "")
                    result += "+";
                result += this.Coefficients[1].ToString() + "X ";
            }
            for (int i = 2; i < this.Coefficients.Length; i++)
            {
                if (this.Coefficients[i] != 0)
                {
                    if (this.Coefficients[i] > 0 && result != "")
                        result += "+";
                    result += this.Coefficients[i].ToString() + "(X^" + i.ToString() + ")";
                    if (i != this.Coefficients.Length - 1)
                        result += " ";
                }
            }
            return result;
        }

        public override int GetHashCode()
        {
            return (int)(this.Coefficients.Min()*100 - this.Coefficients.Sum()*10 + this.Coefficients.Max() - this.Coefficients.Length * 1000);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WorkingWithPolynoms;

namespace WorkingWithPolynomsNUnitTests
{
    [TestFixture]
    public class PolynomClassTests
    {
        #region Test method:  public double CalculateValue(double argument)
        public IEnumerable<TestCaseData> TestCasesForCalculateValue
        {
            get
            {
                yield return new TestCaseData(new Polynom(1, 1, 2, 3), 10).Returns(3211);
                yield return new TestCaseData(new Polynom(200, 1, -2, 3), 10).Returns(3010);
                yield return new TestCaseData(new Polynom(-1, -2, -3, -4), 1).Returns(-10);
                yield return new TestCaseData(new Polynom(0, 0, 0, 0, 0, 0), 10).Returns(0);
                yield return new TestCaseData(new Polynom(0, long.MaxValue, long.MaxValue), 1).Returns(2*Convert.ToDecimal(long.MaxValue));
                yield return new TestCaseData(new Polynom(0, double.MaxValue, double.MaxValue, double.MaxValue), 10).Returns(double.PositiveInfinity);
                yield return new TestCaseData(new Polynom(0, double.MinValue, double.MinValue, double.MinValue), 10).Returns(double.NegativeInfinity);
                yield return new TestCaseData(new Polynom(0.1, 0, 0, 0.3), 10).Returns(300.1);
            }
        }

        [Test, TestCaseSource(nameof(TestCasesForCalculateValue))]
        public double CalculateValue_ForArgument(Polynom polynom, double argument)
        {
            return polynom.CalculateValue(argument);
        }
        #endregion

        #region Test method: public static Polynom operator +(Polynom lhs, Polynom rhs)
        public IEnumerable<TestCaseData> TestCasesForAddition
        {
            get
            {
                yield return new TestCaseData(new Polynom(0, 1, 2, 3), new Polynom(0, 0, 0, 0, 4, 5)).Returns("1X +2(X^2) +3(X^3) +4(X^4) +5(X^5)");
                yield return new TestCaseData(new Polynom(0, 1, 2, 3), new Polynom(0, -1, -2, -3)).Returns("");
                yield return new TestCaseData(new Polynom(0, 0), new Polynom(0, -1, -2, -3)).Returns("-1X -2(X^2) -3(X^3)");
                yield return new TestCaseData(new Polynom(1, 2, 3, 4, 5, 6, 7), new Polynom(-10, 0, -10, 0, -10)).Returns("-9 +2X -7(X^2) +4(X^3) -5(X^4) +6(X^5) +7(X^6)");
            }
        }

        [Test, TestCaseSource(nameof(TestCasesForAddition))]
        public string SumTwoPolynoms_ToString(Polynom first, Polynom second)
        {
            return (first + second).ToString();
        }
        #endregion

        #region Test method: public static Polynom operator -(Polynom lhs, Polynom rhs)
        public IEnumerable<TestCaseData> TestCasesForSubtraction
        {
            get
            {
                yield return new TestCaseData(new Polynom(0, 1, 2, 3), new Polynom(0, 0, 0, 0, 4, 5)).Returns("1X +2(X^2) +3(X^3) -4(X^4) -5(X^5)");
                yield return new TestCaseData(new Polynom(0.1, 0, 0, 0, 0.04, -0.05), new Polynom(0.05, 0, 0, 0, 4, 5)).Returns("0,05 -3,96(X^4) -5,05(X^5)");
                yield return new TestCaseData(new Polynom(0, 0), new Polynom(0, 0, 0, 0, 0)).Returns("");
            }
        }

        [Test, TestCaseSource(nameof(TestCasesForSubtraction))]
        public string SubtractTwoPolynoms_ToString(Polynom first, Polynom second)
        {
            return (first - second).ToString();
        }
        #endregion

        #region Test method: public static Polynom operator *(Polynom lhs, Polynom rhs)
        [Test, TestCaseSource(nameof(TestCasesForMultiplication))]
        public string MultiplyTwoPolynoms_ToString(Polynom first, Polynom second)
        {
            return (first * second).ToString();
        }

        public IEnumerable<TestCaseData> TestCasesForMultiplication
        {
            get
            {
                yield return new TestCaseData(new Polynom(0, 1, 2, 3), new Polynom(0, 0, 0, 0, 4, 5)).Returns("4(X^5) +13(X^6) +22(X^7) +15(X^8)");
                yield return new TestCaseData(new Polynom(1, 1, 0, 2), new Polynom(1, 1, 3)).Returns("1 +2X +4(X^2) +5(X^3) +2(X^4) +6(X^5)");
                yield return new TestCaseData(new Polynom(0, 0), new Polynom(0, 0, 0, 0, 0)).Returns("");
            }
        }
        #endregion

        #region Test method:  public override bool Equals(object obj)
        [Test, TestCaseSource(nameof(TestCasesForEquals))]
        public string ComparingTwoPolynoms_ToString(Polynom first, Polynom second)
        {
            return first.Equals(second).ToString();
        }

        public IEnumerable<TestCaseData> TestCasesForEquals
        {
            get
            {
                yield return new TestCaseData(new Polynom(0, 1, 2, 3), new Polynom(0, 0, 0, 0, 4, 5)).Returns("False");
                yield return new TestCaseData(new Polynom(0, 1, 2, 3), new Polynom(0, 1, 2, 3)).Returns("True");
                yield return new TestCaseData(new Polynom(0, 0), new Polynom(0, 0, 0, 0, 0)).Returns("False");
                yield return new TestCaseData(new Polynom(0, 0, 0), new Polynom(0, 0, 0)).Returns("True");
                yield return new TestCaseData(new Polynom(1, 2, 3), null).Returns("False");
            }
        }
        #endregion

        #region Test method:  public override int GetHashCode()
        [Test, TestCaseSource(nameof(TestCasesForGetHashCode))]
        public string ComparingTwoHashesOfPolynoms_ToString(Polynom first, Polynom second)
        {
            return (first.GetHashCode() == second.GetHashCode()).ToString();
        }

        public IEnumerable<TestCaseData> TestCasesForGetHashCode
        {
            get
            {
                yield return new TestCaseData(new Polynom(0, 1, 2, 3), new Polynom(0, 0, 0, 0, 4, 5)).Returns("False");
                yield return new TestCaseData(new Polynom(0, 1, 2, 3), new Polynom(0, 1, 2, 3)).Returns("True");
                yield return new TestCaseData(new Polynom(0, 0), new Polynom(0, 0, 0, 0, 0)).Returns("False");
                yield return new TestCaseData(new Polynom(0, 0, 0), new Polynom(0, 0, 0)).Returns("True");
            }
        }
        #endregion

    }
}

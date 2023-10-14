using System;

namespace assign1
{
    public class Term : IComparable
    {
        private double coefficient;
        private int exponent;

        // Implement public read and write properties for each data member
        // The set property of exponent should raise an ArgumentOutOfRangeException
        // if value is less than 0 or greater than 20
        // Constructor: Creates a term with the given coefficient and exponent
        public Term(double coefficient, int exponent)
        {
            // Check if the exponent is within the range [0, 20]
            if (exponent < 0 || exponent > 20)
            {
                throw new ArgumentOutOfRangeException("Exponent must be between 0 and 20.");
            }

            this.coefficient = coefficient;
            this.exponent = exponent;
        }

        // Public property for coefficient
        public double Coefficient
        {
            get { return coefficient; }
            set { coefficient = value; }
        }

        // Public property for exponent
        public int Exponent
        {
            get { return exponent; }
            set
            {
                if (value < 0 || value > 20)
                {
                    throw new ArgumentOutOfRangeException("Exponent must be between 0 and 20.");
                }
                exponent = value;
            }
        }

        // Evaluates the current term at x and returns the result
        public double Evaluate(double x)
        {
            return coefficient * Math.Pow(x, exponent);
        }

        // Returns -1, 0, or 1 if the exponent of the current term
        // is less than, equal to, or greater than the exponent of obj
        // Raises an ArgumentException if obj is either null or not a term
        public int CompareTo(Object obj)
        {
            if (obj is Term otherTerm)
            {
                return exponent.CompareTo(otherTerm.exponent);
            }
            throw new ArgumentException("Object is not a Term.");
        }

        // Returns a string representation of the current term
        public override string ToString()
        {
            if (exponent == 0)
            {
                return coefficient.ToString();
            }
            else if (exponent == 1)
            {
                return $"{coefficient}x";
            }
            else
            {
                return $"{coefficient}x^{exponent}";
            }
        }
    }
}

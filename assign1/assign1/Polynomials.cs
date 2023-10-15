using System;

namespace assign1
{
    public class Polynomials
    {
        // A collection of polynomials
        private List<Polynomial> polynomialsList;

        // Creates an empty list polynomialsList of polynomials
        public Polynomials()
        {
            polynomialsList = new List<Polynomial>();
        }

        // Retrieves the polynomial stored at position i in polynomialsList
        public Polynomial Retrieve(int i)
        {
            if (i >= 0 && i < polynomialsList.Count)
            {
                return polynomialsList[i];
            }
            else
            {
                throw new IndexOutOfRangeException("Index is out of range.");
            }
        }

        // Inserts polynomial p into polynomialsList
        public void Insert(Polynomial p)
        {
            polynomialsList.Add(p);
        }

        // Deletes the polynomial at index i
        public void Delete(int i)
        {
            if (i >= 0 && i < polynomialsList.Count)
            {
                polynomialsList.RemoveAt(i);
            }
            else
            {
                throw new IndexOutOfRangeException("Index is out of range.");
            }
        }

        // Returns the number of polynomials in polynomialsList
        public int Size()
        {
            return polynomialsList.Count;
        }

        // Prints out the list of polynomials
        public void Print()
        {
            Console.WriteLine("\nList of Polynomials:");
            for (int i = 0; i < polynomialsList.Count; i++)
            {
                Console.Write($"Polynomial {i+1}: "); //Add 1 so that the 0th polynomial displays as the 1st, etc.
                this.Retrieve(i).Print();
            }

        }
    }
}
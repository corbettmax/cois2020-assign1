using System;
using System.Runtime.InteropServices;

namespace assign1
{
    public class Polynomial : ICloneable
    {
        // A reference to the first node of a singly linked list
        private Node<Term> front { get; set; }
        // Creates the zero polynomial, i.e. 0
        public Polynomial()
        {
            front = new Node<Term>(null, null);
        }
        // Inserts term t into the current polynomial in its proper order
        // If a term with the same exponent already exists then the two terms are added together
        // If the two terms cancel out then no new term is created
        public void AddTerm(Term t)
        {
            //if term being added is 0
            if (t.Coefficient == 0) return;

            //reference to current index
            Node<Term> current = front;

            //loop through the singly linked list
            while (true)
            {
                //if at the end of the list or if exponent of current postition is now less than that of the Term being added
                if (current.Next == null || current.Next.Item.CompareTo(t) < 0)
                {
                    //insert term into a new node between current index and next
                    Node<Term> temp = current.Next;
                    current.Next = new Node<Term>(t, temp);
                    return;
                }

                // If a term with the same exponent already exists then the two terms are added together
                else if (current.Next.Item.Exponent == t.Exponent)
                {

                    // If the two terms cancel out then no new term is created
                    if (current.Next.Item.Coefficient + t.Coefficient == 0)
                    {
                        try
                        {
                            //remove the Term that gets cancelled out by moving the node's reference to next.next
                            current.Next = current.Next.Next;
                        }
                        catch
                        {
                            //catch exception from next.next being null, refer to null instead
                            //NOTE: try/catch statement may not be necessary
                            current.Next = null;
                        }

                    }
                    //if term with same degree already exists and addition of coefficients doesn't cancel out the term
                    else
                    {
                        //reset next's item to be a new term with the updated coefficient but same exponent
                        current.Next.Item = new Term(current.Next.Item.Coefficient + t.Coefficient, t.Exponent);
                    }

                    return;
                }

                //update index
                current = current.Next;

            }
        }

        // Adds polynomials p and q to yield a new polynomial
        public static Polynomial operator +(Polynomial p, Polynomial q)
        {
            //references to the respective fronts of the polynomials to add
            Node<Term> pcurrent = p.front;
            Node<Term> qcurrent = q.front;

            //new polynomial to return
            Polynomial result = new();

            //loop through polynomial p and add each term to result
            while (pcurrent.Next != null)
            {
                result.AddTerm(pcurrent.Next.Item);
                pcurrent = pcurrent.Next;
            }

            //loop through polynomial q and add each term to result
            while (qcurrent.Next != null)
            {
                result.AddTerm(qcurrent.Next.Item);
                qcurrent = qcurrent.Next;
            }

            //return the resulting
            return result;
        }



        // Multiplies polynomials p and q to yield a new polynomial
        public static Polynomial operator *(Polynomial p, Polynomial q)
        {
            //references to the respective fronts of the polynomials to add
            Node<Term> pcurrent = p.front;
            Node<Term> qcurrent = q.front;
            
            //new polynomial to store result
            Polynomial result = new();


            //if either polynomial is == 0
            if(pcurrent.Next == null || qcurrent.Next == null)
            {
                 return result;
            }


            //Loop through each term in polynomial p
            while (pcurrent.Next != null)
            {
                //Loop through each term polynomial q
                while (qcurrent.Next != null)
                {
                    result.AddTerm(new Term(pcurrent.Next.Item.Coefficient *
                        qcurrent.Next.Item.Coefficient, pcurrent.Next.Item.Exponent
                        + qcurrent.Next.Item.Exponent));
                    qcurrent = qcurrent.Next; //Move to next term in q
                }
                qcurrent = q.front; //Set current term in q back to the first
                pcurrent = pcurrent.Next; //Move to next term in p
            }
            return result;
        }

        // Evaluates the current polynomial at x and returns the result
        public double Evaluate(double x)
        {
            
            //if the polynomial is empty that means it is equal to 0
            if(front.Next == null)
            {
                return 0;
            }
            
            
            Node<Term> current = front.Next;
            double total = 0;
            //loop through each term in the polynomial and add each evalution to the total
            while (current != null)
            {
                //evaluate the term at x and return the total
                total += Math.Pow(x, current.Item.Exponent) * current.Item.Coefficient;
                //increment the index
                current = current.Next;
            };

            //return the total of the evalution
            return total;

        }


        // Creates and returns a clone of the current polynomial except that the exponents
        // of the current polynomial are assigned to the coefficients of the clone in reverse order
        // For example, 4x^3 – 3x + 9 is cloned as 9x^3 – 3x + 4
        public Object Clone()
        {
            Queue<int> E = new Queue<int>(); // Stack of exponents
            Stack<double> C = new Stack<double>(); // Stack of coefficients
            Node<Term> current = front;
            while (current.Next != null)
            {
                E.Enqueue(current.Next.Item.Exponent);
                C.Push(current.Next.Item.Coefficient);
                current = current.Next;
            }

            Polynomial cloned = new();

            while (E.Count > 0)
            {
                Term termy = new Term(C.Pop(), E.Dequeue());
                cloned.AddTerm(termy);
            }

            return cloned;

        }

        // Prints the current polynomial
        public void Print()
        {
            //if the polynomial is empty that means it's equal to 0
            if(front.Next == null)
            {
                Console.WriteLine("0");
                return;
            }
            
            Node<Term> current = front;
            string printed;

            //Represent the first term without "+" but with "-" if negative
            if (current.Next.Item.Coefficient < 0)
            {
                printed = "-" + current.Next.Item.ToString(); 
            }
            else
                printed = current.Next.Item.ToString();

            current = current.Next;

            while (current.Next != null)
            {
                //If negative coefficient, use minus sign instead of plus
                if (current.Next.Item.Coefficient < 0)
                {
                    printed += " - " + current.Next.Item.ToString();
                }
                else
                {
                    printed += " + " + current.Next.Item.ToString();
                }

                current = current.Next;

            };

            //Display the finalized polynomial
            Console.WriteLine(printed);
        }

    }
}
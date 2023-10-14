using System;


public class Polynomial : ICloneable
{
    // A reference to the first node of a singly linked list
    private Node<Term> front;
    // Creates the zero polynomial, i.e. 0
    public Polynomial()
    {
        Term T = new Term(0.00, 0);
        Node<Term> head = new Node(T, null);
        this.front = new Node<Term>(null,head);
    }
    // Inserts term t into the current polynomial in its proper order
    // If a term with the same exponent already exists then the two terms are added together
    // If the two terms cancel out then no new term is created
    public void AddTerm(Term t)
    {
        //if term being added is 0
        if (t.getCoefficient() == 0) return;

        //reference to current index
        Node<Term> current = this.front;
        
        //loop through the singly linked list
        while (true)
        {
            //if at the end of the list or if exponent of current postition is now less than that of the Term being added
            if(current.getNext()==null | current.getNext().getItem().getExponent() < t.getExponent())
            {
                //insert term into a new node between current index and next
                current.setNext(new Node<Term>(t, current.getNext()));
                return;
            }
            
            // If a term with the same exponent already exists then the two terms are added together
            else if(current.getNext().getItem().getExponent() == t.getExponent())
            {

                // If the two terms cancel out then no new term is created
                if (current.getNext().getItem().getCoefficient() + t.getCoefficient() == 0)
                {
                    try
                    {
                        //remove the Term that gets cancellled out by moving the node's reference to next.next
                        current.setNext(current.getNext().getNext());
                    }
                    catch
                    {
                        //catch exception from next.next being null, refer to null instead
                        //NOTE: try/catch statement may not be necessary
                        current.setNext(null);
                    }

                }
                //if term with same degree already exists and addition of coefficients doesn't cancel out the term
                else
                {
                    //reset next's item to be a new term with the updated coefficient but same exponent
                    current.getNext().setItem(new Term(current.getNext().getItem().getCoefficient() + t.getCoefficient(), t.getExponent()));
                }

                return;
            }

            //update index
            current = current.getNext();
            
        }
    }

    // Adds polynomials p and q to yield a new polynomial
    public static Polynomial operator +(Polynomial p, Polynomial q)
    {
        //references to the respective fronts of the polynomials to add
        Node<Term> pcurrent = p.getFront();
        Node<Term> qcurrent = q.getFront();
        
        //new polynomial to return
        Polynomial result = new Polynomial();

        //loop through polynomial p and add each term to result
        while (pcurrent.getNext() != null)
        {
            result.AddTerm(pcurrent.getNext().getItem())
            pcurrent = pcurrent.getNext();
        }
        
        //loop through polynomial q and add each term to result
        while (qcurrent.getNext() != null)
        {
            result.AddTerm(qcurrent.getNext().getItem())
            qcurrent = qcurrent.getNext();
        }

        //return the resulting
        return result;

    }



    // Multiplies polynomials p and q to yield a new polynomial
    public static Polynomial operator *(Polynomial p, Polynomial q)
    { 
        //references to the respective fronts of the polynomials to add
        Node<Term> pcurrent = p.getFront();
        Node<Term> qcurrent = q.getFront();

        //new polynomial to store result
        Polynomial result = new Polynomial();

        //loop through polynomial p and add each term to result
        while (pcurrent.getNext() != null)
        {
            while (qcurrent.getNext() != null)
            {
                result.AddTerm(new Term(pcurrent.getNext().getItem().getCoefficient() *
                    qcurrent.getNext().getItem().getCoefficient(), pcurrent.getNext().getItem().getExponent() 
                    + qcurrent.getNext().getItem().getCoefficient()));
                qcurrent = qcurrent.getNext();
            }
            pcurrent = pcurrent.getNext();
        }
        return result;
    }

    // Evaluates the current polynomial at x and returns the result
    public double Evaluate(double x)
    {
        Node<Term> current = this.getFront().getNext();
        double total = 0;
        while (current != null)
        {
            total += Math.Pow(x, current.getItem().getExponent())*current.getItem().getCoefficient();
            current = current.getNext();
        };

        return total;

    }
    // Creates and returns a clone of the current polynomial except that the exponents
    // of the current polynomial are assigned to the coefficients of the clone in reverse order
    // For example, 4x^3 – 3x + 9 is cloned as 9x^3 – 3x + 4
    public Object Clone()
    {
        Queue<int> E = new Queue<int>(); // Stack of exponents
        Stack<int> C = new Stack<int>(); // Stack of coefficients
        Node<Term> current = this.getFront();
        while (current.getNext() != null)
        {
            E.Enqueue(current.getNext().getItem().getExponent());
            C.Push(current.getNext().getItem().getCoefficient());
            current = current.getNext();
        }

        Polynomial cloned = new Polynomial();

        while(E.Count > 0)
        {
            Term termy = new Term(C.Pop(), E.Dequeue());
            cloned.AddTerm(termy);
        }

        return cloned;

    }

    // Prints the current polynomial
    public void Print()
    {
        Node<Term> current = this.getFront();
        string printed = current.getNext().getItem().ToString(); // represent the first term without "+" but with "-" if negative

        while (current.getNext() != null)
        {
            if(current.getNext().getItem().getCoefficient() < 0)
            {
                printed += current.getNext().getItem().ToString();
            }

            else
            {
                printed += "+" + current.getNext().getItem().ToString();
            }
            
        }

        Console.WriteLine(printed);

    }

}
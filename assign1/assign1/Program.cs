using System;
class Program
{
    static void Main(string[] args)
    {
        Polynomials S = new Polynomials(); // Collection of polynomials

        char input = '0'; //Used for selecting options

        do
        {
            //Print list of polynomials
            Console.WriteLine("\nCurrent Polynomials:");
            S.Print();

            //Select option
            Console.WriteLine("\nSelect Option");
            Console.WriteLine("1: Create Polynomial");
            Console.WriteLine("2: Add Polynomials");
            Console.WriteLine("3: Multiply Polynomials");
            Console.WriteLine("4: Delete Polynomial");
            Console.WriteLine("5: Evaluate Polynomial");
            Console.WriteLine("6: Clone Polynomial");
            Console.WriteLine("7: Quit");
            Console.Write("=> ");

            try
            {
                input = Convert.ToChar(Console.ReadLine());
            }
            catch (FormatException) { } //Skips to default switch case if input is not valid

            switch (input)
            {
                //Create polynomial
                case '1':
                    Polynomial tempPoly = new Polynomial(); //Create empty new polynomial
                    char? yesNo = null; //Used to control do/while loop

                    do
                    {
                        //Insert coefficient
                        double? coefficient = null;
                        do
                        {
                            try
                            {
                                //Take input
                                Console.Write("Insert coefficient => ");
                                coefficient = Convert.ToDouble(Console.ReadLine());
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Invalid input.");  //Returns error if input cannot be converted to double type
                            }
                        } while (coefficient == null); //Runs until valid value is inputted

                        //Insert exponent
                        int? exponent = null;
                        do
                        {
                            try
                            {
                                //Take input
                                Console.Write("Insert exponent => ");
                                exponent = Convert.ToInt32(Console.ReadLine());
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Invalid input.");  //Returns error if input cannot be converted to int type
                            }
                        } while (exponent == null); //Runs until valid value is inputted

                        Term tempTerm = new Term(coefficient, exponent);  //Create new term with inputted values
                        tempPoly.AddTerm(tempTerm);  //Add new term to current polynomial
                        Console.WriteLine("Current Polynomial:");
                        tempPoly.Print(); //Display current polynomial


                        //Ask user if they want to add another term
                        yesNo = null;
                        do
                        {
                            try
                            {
                                //Take input
                                Console.Write("Add another term? Y/N => ");
                                yesNo = Convert.ToChar(Console.ReadLine());
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Invalid input.");  //Returns error if input cannot be converted to char type
                            }
                        } while (yesNo == null); //Runs until valid value is inputted


                    } while (yesNo == 'Y'); //Loops until 'N' is chosen

                    S.Insert(tempPoly); //Insert finalized polynomial into list
                    break;

                //Add two polynomials and insert into S
                case '2':
                    Polynomial augend = S.Retrieve(CheckIndices(S));
                    Polynomial addend = S.Retrieve(CheckIndices(S));
                    S.Insert(augend + addend);
                    break;

                //Multiply two polynomials and insert into S
                case '3':
                    Polynomial multiplier = S.Retrieve(CheckIndices(S));
                    Polynomial multiplicand = S.Retrieve(CheckIndices(S));
                    S.Insert(multiplier * multiplicand);
                    break;

                //Delete polynomial
                case '4':
                    S.Delete(CheckIndices(S));
                    break;

                //Evaluate polynomial
                case '5':
                    Polynomial p = S.Retrieve(CheckIndices(S)); //Retrieve chosen polynomial and store as 'p'
                    double? value = null;
                    do
                    {
                        try
                        {
                            //Take input
                            Console.Write("Select a value for evaluation => ");
                            value = Convert.ToDouble(Console.ReadLine());
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input.");  //Returns error if input cannot be converted to double type
                        }

                    } while (value == null); //Runs until valid value is inputted
                    p.Evaluate(value); //Evaluate polynomial 'p' using inputted value
                    break;

                //Clone polynomial and insert clone into S
                case '6':
                    Polynomial p = S.Retrieve(CheckIndices(S)); //Retrieve chosen polynomial and store as 'p'
                    S.Insert(p.Clone()); //Clone 'p' and insert into collection
                    break;

                //Quit
                case '7':
                    break;

                default:
                    Console.WriteLine("Invalid input.");
                    break;
            }
        } while (input != '7'); //Break loop and end program when "Quit" is selected
    }

    //Checks if inputted value is in range of collection
    static int CheckIndices(Polynomials pclass)
    {
        int value;
        do
        {
            //Take input
            Console.Write("Select a polynomial number => ");
            value = Convert.ToInt32(Console.ReadLine());

            //Check if in range
            if (value > 1 && value <= pclass.Size())
                return value;
            else
                throw new IndexOutOfRangeException("Value out of range.");
        } while (value > 1 && value <= pclass.Size()); //Runs until valid value is inputted
    }
}

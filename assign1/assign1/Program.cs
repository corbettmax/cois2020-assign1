using System;
namespace assign1
{
    class Program
    {
        static void Main()
        {
            Polynomials S = new(); // Collection of polynomials

            char input; //Used for selecting options

            do
            {
                input = '0'; //Reset input value

                if (S.Size() > 0) //Checks that S is not empty
                {
                    //Print list of polynomials
                    S.Print();
                }

                //Select option
                Console.WriteLine("\nSelect Option");
                Console.WriteLine("1: Create New Polynomial");
                Console.WriteLine("2: Add Polynomials");
                Console.WriteLine("3: Multiply Polynomials");
                Console.WriteLine("4: Delete Polynomial");
                Console.WriteLine("5: Evaluate Polynomial");
                Console.WriteLine("6: Clone Polynomial");
                Console.WriteLine("7: Quit");
                Console.Write("=> ");

                try
                {
                    input = Convert.ToChar(Console.ReadLine()!);
                }
                catch (FormatException) { } //Skips to default switch case if input is not valid

                switch (input)
                {
                    //Create polynomial
                    case '1':
                        Polynomial tempPoly = new(); //Create empty new polynomial
                        char? yesNo; //Used to control do/while loop

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
                                // Check if the exponent is within the range [0, 20]
                                if (exponent < 0 || exponent > 20)
                                {
                                    Console.WriteLine("Exponent must be between 0 and 20.");
                                    exponent = null;
                                }
                            } while (exponent == null); //Runs until valid value is inputted

                            Term term = new((double)coefficient, (int)exponent);  //Create new term with inputted values
                            tempPoly.AddTerm(term);  //Add new term to current polynomial
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
                                    yesNo = Convert.ToChar(Console.ReadLine()!);
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Invalid input.");  //Returns error if input cannot be converted to char type
                                }
                            } while (yesNo == null); //Runs until valid value is inputted


                        } while (yesNo == 'Y' || yesNo == 'y'); //Loops until 'N' is chosen

                        S.Insert(tempPoly); //Insert finalized polynomial into list
                        break;

                    //Add two polynomials and insert into S
                    case '2':
                        if (S.Size() > 0) //Checks that S is not empty
                        {
                            Polynomial augend = S.Retrieve(CheckIndices(S));
                            Polynomial addend = S.Retrieve(CheckIndices(S));
                            S.Insert(augend + addend);
                        }
                        else
                            Console.WriteLine("Error: no polynomials in collection.");
                        break;

                    //Multiply two polynomials and insert into S
                    case '3':
                        if (S.Size() > 0) //Checks that S is not empty
                        { 
                            Polynomial multiplier = S.Retrieve(CheckIndices(S));
                            Polynomial multiplicand = S.Retrieve(CheckIndices(S));
                            S.Insert(multiplier * multiplicand);
                        }
                        else
                            Console.WriteLine("Error: no polynomials in collection.");
                        break;

                    //Delete polynomial
                    case '4':
                        if (S.Size() > 0) //Checks that S is not empty
                            S.Delete(CheckIndices(S));
                        else
                            Console.WriteLine("Error: no polynomials in collection.");
                        break;

                    //Evaluate polynomial
                    case '5':
                        if (S.Size() > 0) //Checks that S is not empty
                        {
                            Polynomial pEval = S.Retrieve(CheckIndices(S)); //Retrieve chosen polynomial and store as 'p'
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

                            Console.WriteLine($"Result: {pEval.Evaluate((double)value)}"); //Evaluate polynomial 'p' using inputted value and print result
                        }
                        else
                            Console.WriteLine("Error: no polynomials in collection.");
                        break;

                    //Clone polynomial and insert clone into S
                    case '6':
                        if (S.Size() > 0) //Checks that S is not empty
                        {
                            Polynomial pClone = S.Retrieve(CheckIndices(S)); //Retrieve chosen polynomial and store as 'p'
                            S.Insert((Polynomial)pClone.Clone()); //Clone 'p' and insert into collection
                        }
                        else
                            Console.WriteLine("Error: no polynomials in collection.");
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
            int? value = null;
            do
            {
                do
                {
                    try
                    {
                        //Take input
                        Console.Write("Select a polynomial number => ");
                        value = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input.");  //Returns error if input cannot be converted to int type
                    }
                } while (value == null); //Runs until valid value is inputted

                //Check if in range
                if (value > 0 && value <= pclass.Size())
                    return (int)value-1; //Input element, return index
                else
                    Console.WriteLine("Value out of range.");
            } while (true); //Runs until valid value is inputted
        }
    }
}
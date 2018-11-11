using System;
using orderTaker;

namespace console
{
    /// Console Program Shell around the Order Taker
    /// Gives instructions for users to use the OrderTaker
    class Program
    {
        static void Main(string[] args)
        {
            Order orderHandler = new Order();

            Console.WriteLine("Please enter your Diner order or 'x' to exit");
            string input = Console.ReadLine();

            // bad input should not exit program. exit commands should.
            while ( IsInputExit(input) == false )
            {   
                string output = orderHandler.MakeOrder(input);
                Console.WriteLine(output);

                // setup for next iteration
                Console.WriteLine("Please enter your Diner order");
                input = Console.ReadLine();
            }
        }

        static private bool IsInputExit(string input)
        {
            if( input == "x" 
                || input.StartsWith("exit") )
            {
                return true;
            }

            return false;
        }
        
    }
}

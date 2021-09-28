using System;
/*
Write a program to create a Generic Stack data structure
which can handle multiple data types. 

Use the class diagram given in the tutorial instruction document as reference.
*/


namespace Week8Program1
{
    public class MyGenericStack<T>
    {
        // Keep track of the number of elements added to the Stack in LIFO (Last In First Out) order
        private int top;

        // Store the element in the stack
        private T[] stackElements;

        // Constructor which initializes the stack given a stack size, 
        public MyGenericStack(int stackSize)
        {
            if (stackSize > 0)
            {
                stackElements = new T[stackSize];
            }
            else
            {
                throw new ArgumentException("The stack size must be greater than 0");
            }
        }

        // Method to add an element in the stack
        public void Push(T value)
        {
            // Check if the stack is full
            if (top == stackElements.Length - 1)
            {
                throw new StackOverflowException("The stack is full, you cannot push the value.");
            }
            
            // Increment top and add the value to the stack 
            top++;
            stackElements[top] = value;
            
        }

        // Method which returns the current value at the location indicated by top
        public T Pop()
        {
            // Check if the Stack is empty
            if (top < 0)
            {
                throw new ArgumentOutOfRangeException("The stack is empty, you cannot pop any value");
            }

            // Decrement top and return the element being deleted
            top--;
            return stackElements[top + 1];
        }

        // Write code for Accessor: returns the size of stack
        public int Length { get { return stackElements.Length; } }

        // Write code for Accessor: returns the actual number of elements in the stack
        public int NumberOfElements { get { return top; } }
    }

    class Program
    {
        // Initial static double and string arrays with potential values to be inserted into the stacks
        private static double[] doubleValues = new double[] { 1.2, 3.4, 5.6, 9.2, 4.5 };

        private static string[] stringValues = new string[] { "Paul", "George", "Alan", "Alex", "Martin"};

        // Create a stack of double and string value using the MyGenericStack class
        private static MyGenericStack<double> myDoubleStack;
        private static MyGenericStack<string> myStringStack;

        static void Main(string[] args)
        {
            // Initialize the stacks
            myDoubleStack = new MyGenericStack<double>(6);
            myStringStack = new MyGenericStack<string>(6);

            // Insert values into the respective stacks and check for exceptions
            try {
                foreach (double doubleVal in doubleValues)
                {
                    myDoubleStack.Push(doubleVal);
                }
                foreach (string stringVal in stringValues)
                {
                    myStringStack.Push(stringVal);
                }
            }
            catch(StackOverflowException e)
            {
                Console.WriteLine(e.ToString());
            }

            // Display the stack contents by poping the values from each stacks seperately
            try
            {
                int numberOfElement = myDoubleStack.NumberOfElements;
                Console.WriteLine("The Double Stack had the following values:");
                for (int loopVar = 0; loopVar < myDoubleStack.Length; loopVar++)
                {
                    Console.Write("{0} --> ", myDoubleStack.Pop());
                }

                numberOfElement = myStringStack.NumberOfElements;
                Console.WriteLine("\n\nThe String Stack had the following values:");
                for (int loopVar = 0; loopVar < numberOfElement; loopVar++)
                {
                    Console.Write("{0} --> ", myStringStack.Pop());
                }
            }
            catch(ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.ToString());
            }            
            
            Console.ReadKey();
        }
    }
}

/*
Expected Output:

The Double Stack had the following values:
4.5 --> 9.2 --> 5.6 --> 3.4 --> 1.2 --> 0 -->

The String Stack had the following values:
Martin --> Alex --> Alan --> George --> Paul -->

*/

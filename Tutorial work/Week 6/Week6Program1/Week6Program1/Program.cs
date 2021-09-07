using System;
using System.Linq;
/*
Write a program to Create Student a Result management System with the following specifications:
1. There are two classes, Person (Abstract base class), Student(derived class from person)
2. There is one Interface for Generating the Results
    */
namespace Week6LabProgram
{
    public abstract class Person
    {
        private string name, address;

        public Person(string name, string address)
        {
            this.name = name;
            this.address = address;
        }   
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
    }

    // Interface IResults for creating marksheet
    public interface IResults
    {
        // Get the marks for 5 subjects
        void GetMarks();
        // Calculate the average mark
        double CalculateResult();
        // Return the grade (Pass or Fail)
        string GetGrade();
        // Display the MarkSheet
        void DisplayResult();
    }

    public class Student : Person, IResults
    {
        private string standard, roll;
        private double[] marks = new double[5];

        public Student(string name, string address, string standard, string roll) : base(name, address) 
        {
            this.standard = standard;
            this.roll = roll;
        }
        public void GetMarks()
        {
            double subjectMark;
            // Accept the marks for 5 subjects and store it in an array
            for(int loopVar=0; loopVar<marks.Length; loopVar++)
            {
                Console.Write("Enter Mark for Subject-{0}: ", loopVar+1);
                subjectMark = Convert.ToDouble(Console.ReadLine());
                marks[loopVar] = subjectMark;
            }
            Console.WriteLine();
        }
        public double CalculateResult()
        {
            return marks.Sum() / 5.0;           
        }
        public string GetGrade()
        {
            double averageMark = CalculateResult();
            if (averageMark < 40)
                return "Fail";
            else
                return "Pass";
        }
        public void DisplayResult()
        {
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("\t\tMark Sheet");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("Name: {0}", this.Name);
            Console.WriteLine("Class: {0}", this.standard);
            Console.WriteLine("Roll: {0}", this.roll);
            Console.WriteLine("Address: {0}", this.Address);
            Console.WriteLine();
            Console.WriteLine("Marks Obtained: ");
            for (int loopVar = 0; loopVar < marks.Length; loopVar++)
            {
                Console.Write("Subject-{0}: {1} \n", loopVar + 1, marks[loopVar]);
            }
            Console.WriteLine();
            Console.WriteLine("Average Mark: {0}", this.CalculateResult());
            Console.WriteLine("Final Grade: {0}", this.GetGrade());
            Console.WriteLine("---------------------------------------------");
        }

    }
    class Week6Program1
    {
        static void Main(string[] args)
        {
            // Create a Student object
            Student s1 = new Student("George Woolsworth", "Ultimo, Sydney 2007, Australia", "V", "1004");
            // Get the student's marks
            s1.GetMarks();
            // Generate the Marks Sheet
            s1.DisplayResult();

            Console.ReadKey();
        }
    }
}

/* Test Case:
Enter Mark for Subject-1: 56
Enter Mark for Subject-2: 42
Enter Mark for Subject-3: 89
Enter Mark for Subject-4: 69
Enter Mark for Subject-5: 95

---------------------------------------------
                Mark Sheet
---------------------------------------------
Name: George Woolsworth
Class: V
Roll: 1004
Address: Ultimo, Sydney 2007, Australia

Marks Obtained:
Subject-1: 56
Subject-2: 42
Subject-3: 89
Subject-4: 69
Subject-5: 95

Average Mark: 70.2
Final Grade: Pass
---------------------------------------------    
    */

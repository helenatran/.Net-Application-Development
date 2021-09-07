using System;
/*
Write a program to create a Shape Class and derive two child class, Rectangle and Circle, based 
the specification given in the Tutorial instructions.
 The Base class implementation is provided.
 */
namespace Week6LabProgram
{
    // Base Class Shape
    public class Shape
    {
        public int NumberOfSides { get; set; }

        public Shape(int numberOfSides)
        {
            NumberOfSides = numberOfSides;
        }
        // Virtual method Area() to be overidden by Child class
        public virtual void Area()
        {
            Console.WriteLine("I am from Shape Class");
        }
        // Virtual method Display() to be overidden by Child class
        public virtual void Display()
        {
            Console.WriteLine("The Number of Sides are: {0}", NumberOfSides);
        }
    }

    // Create the Child class Circle derived from Shape
    public class Circle: Shape
    {
        // Data member/properties
        private double radius, area;
        private const double pi = 3.142;

        public Circle(int NumberOfSides, double radius): base(NumberOfSides)
        {
            this.radius = radius;
        }
        public override void Area()
        {
            area = pi * radius * radius; 
        }
        public override void Display()
        {
            Console.WriteLine("\nThe Number of sides of a Circle is : {0}", this.NumberOfSides);
            Console.WriteLine("The Radius of the Circle is: {0}", this.radius);
            Console.WriteLine("The Area of Circle is : {0}", this.area);
        }
    }
    // Create the child class Rectangle derived from Shape
    public class Rectangle : Shape
    {
        // Data member/properties
        private double length, width, area;
        
        public Rectangle(int NumberOfSides, double length, double width) : base(NumberOfSides)
        {
            this.length = length;
            this.width = width;
        }
        public override void Area()
        {
            area = width * length;
        }
        public override void Display()
        {
            Console.WriteLine("\nThe Number of sides of a Rectangle is : {0}", this.NumberOfSides);
            Console.WriteLine("The Length and Width of the Rectangle is: {0}, {1}", this.length, this.width);
            Console.WriteLine("The Area of Rectangle is : {0}", this.area);
        }
    }
    class Week6Program2
    {
        static void Main(string[] args)
        {
            // Create Circle and Rectangle Objects
            Circle C1 = new Circle(1, 4);
            Rectangle R1 = new Rectangle(4, 5, 4);

            // Calculate the areas
            C1.Area();
            R1.Area();

            // Display the dimensions and area of Circle and Rectangle
            C1.Display();
            R1.Display();

            Console.ReadKey();
        }
    }
}
/*
  Test Case:
    The Number of sides of a Circle is : 1
    The Radius of the Circle is: 4
    The Area of Circle is : 50.272

    The Number of sides of a Rectangle is : 4
    The Length and Width of the Rectangle is: 5, 4
    The Area of Rectangle is : 20
 */

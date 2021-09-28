using System;

namespace Week10Program1
{
    class Employee : IComparable<Employee>
    {
        private string firstName, lastName;
        private double hourlyRate, workHours;
        private int employeeId;

        // Employee Constructor
        public Employee() { }

        // Method to extract individual fields from employee details line
        public void LoadEmployee(string fileLine)
        {
            char[] delimiterChars = { ',', ' ' };
            string[] employeeInfo = fileLine.Split(delimiterChars, System.StringSplitOptions.RemoveEmptyEntries);

            // Assign values to respective properties
            firstName = employeeInfo[0];
            lastName = employeeInfo[1];
            hourlyRate = Convert.ToDouble(employeeInfo[2]);
            employeeId = Convert.ToInt32(employeeInfo[3]);
            workHours = Convert.ToDouble(employeeInfo[4]);
        }

        // Method to calculate the weekly salary of an Employee
        public double GetWeeklySalary()
        {
            return workHours * hourlyRate;
        }

        // Overide the ToString() method to display the employee details
        public override string ToString()
        {
            return firstName + " " + lastName + " ID#: " + employeeId + " Weekly Income: " + GetWeeklySalary();
        }

        // Method to compare 2 employees
        public int CompareTo(Employee other)
        {
            if (employeeId < other.employeeId) return -1;
            else if (employeeId == other.employeeId) return 1;
            else return 0;
        }
    }
}

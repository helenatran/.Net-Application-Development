using System;
using System.Collections.Generic;
using System.IO;

namespace Week10Program1
{
    class EmployeeList
    {
        private List<Employee> employees;

        // EmployeeList constructor
        public EmployeeList()
        {
            employees = new List<Employee>();
        }

        // Method to load all employees from the given file
        public void LoadEmployees(string filename)
        {
            StreamReader fileContent = new StreamReader(filename);

            // Read the StremReader till the last line
            while (!fileContent.EndOfStream)                                                                                                                                                                                                             
            {
                Employee employee = new Employee();

                // Read each line and from the StreamReader
                string line = fileContent.ReadLine();

                // Load the employee detail from file to respective fields
                employee.LoadEmployee(line);

                // Add the details to the list collection
                employees.Add(employee);
            }

            fileContent.Close();
        }

        // Method to display the details for all the Employee
        public void PrintEmployees()
        {
            foreach (Employee employee in employees)
            {
                Console.WriteLine(employee.ToString());
            }
        }
        //  Sort the employees by Employee ID
        public void SortEmployees()
        {
            employees.Sort();
        }
    }
}

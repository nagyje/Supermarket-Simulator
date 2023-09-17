/////////////////////////////////////////
// 
// Author: Joe Nagy, nagyje@etsu.edu
// Course CSCI 2210-001 - Data Structures
// Assignment: Project 4 - Supermarket Simulation
//
/////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationLogic
{
    /// <summary>
    /// This class is the main functional part of the supermarket simulation.
    /// It contains a queue of customers and stores info about how many checkouts it has completed.
    /// There are several methods to perform data manipulation for UI updates and simulation information.
    /// </summary>
    internal class Register
    {
        int ID;
        Queue<Customer> Line;
        double TotalSales;
        int CustomersServed;
        List<string> IDlist;

        /// <summary>
        /// Constructor method
        /// Only an ID is required to create a register,
        /// Line, TotalSales, CustomersServed, and IDList are manipulated through
        /// methods and simulation elements.
        /// </summary>
        /// <param name="iD">
        /// Integer that auto-increments after a Register is made
        /// </param>
        public Register(int iD)
        {
            ID = iD;
            Line = new Queue<Customer>();
            IDlist = new List<string>();
        }

        /// <summary>
        /// Adds a customer to the register's Line queue and 
        /// adds their ID to the List used for UI updates
        /// </summary>
        /// <param name="customer">
        /// Customer being added to the Register's Line
        /// </param>
        public void JoinLine(Customer customer)
        {
            Line.Enqueue(customer);
            IDlist.Add(customer.ID.ToString());
        }

        /// <summary>
        /// Dequeues the customer at the front of the queue,
        /// removes their ID from the UI updating List
        /// </summary>
        /// <returns>
        /// The Customer being dequeued
        /// </returns>
        public Customer CheckOut()
        {
            IDlist.RemoveAt(0);
            return Line.Dequeue();
        }

        /// <summary>
        /// Counts the elements in the Line queue
        /// </summary>
        /// <returns>
        /// Total Customers in the Line
        /// </returns>
        public int LineLength() { return Line.Count(); }

        /// <summary>
        /// Gets the List of customer IDs that are currently
        /// in the register line, used for UI updating
        /// </summary>
        /// <returns>
        /// List of Customer IDs
        /// </returns>
        public List<string> GetIDs()
        {
            return IDlist;
        }

        /// <summary>
        /// TotalSales setter/getter
        /// </summary>
        public void SetTotalSales(double sales)
        {
            TotalSales = sales;
        }
        public double GetTotalSales()
        {
            return TotalSales;
        }

        /// <summary>
        /// ID getter
        /// </summary>
        public int GetID()
        {
            return ID;
        }

        /// <summary>
        /// Looks at the front of the Register Line
        /// </summary>
        /// <returns>
        /// Customer at front of line
        /// </returns>
        public Customer Peek()
        {
            return Line.Peek();
        }
    }
}

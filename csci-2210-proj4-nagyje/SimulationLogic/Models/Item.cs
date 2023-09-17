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
    /// This class contains the data and management methods for the Item objects.
    /// They are added into the Customer Cart stacks during the simulation.
    /// </summary>
    internal class Item
    {
        string Name;
        double Price;

        /// <summary>
        /// Constructor method
        /// Requires both a name and a price to create an Item
        /// </summary>
        /// <param name="name">
        /// String - pulled from item naming arrays in the supermarket Run() method
        /// </param>
        /// <param name="price">
        /// Randomly generated double between 0.5 and 100
        /// </param>
        public Item(string name, double price)
        {
            Name = name;
            Price = price;
        }

        /// <summary>
        /// getter for name
        /// getter for price
        /// </summary>
        string GetName() { return Name; }
        public double GetPrice() { return Price; }
        
    }
}

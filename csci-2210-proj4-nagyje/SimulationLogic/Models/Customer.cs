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
    /// This class is used to manage and store the data for the Customer object that functions within the Supermarket simulation.
    /// The constructor for Customer does not include the Cart and CartTime variables, as they are manipulated through the
    /// supermarket.Run() method.
    /// </summary>
    internal class Customer
    {
        public int ID;
        Stack<Item> Cart;
        int CartTime;

        /// <summary>
        /// Constructor method 
        /// Only an ID is required to create a new customer because the other variables are
        /// handled within the simulation
        /// </summary>
        /// <param name="iD">
        /// Integer that is incremented each time a customer is created
        /// </param>
        public Customer(int iD)
        {
            ID = iD;
            Cart = new Stack<Item>();
        }

        /// <summary>
        /// Calculates the total price of the items within the Cart stack by popping them all and 
        /// adding each popped item's price to an external variable.
        /// </summary>
        /// <returns>
        /// cartValue - the total price of each item previously in the customer's cart
        /// </returns>
        public double GetCartValue()
        {
            double cartValue = 0;
            for (int i = 0; i < Cart.Count; i++)
            {
                double itemPrice = Cart.Pop().GetPrice();
                cartValue = itemPrice + cartValue;
            }
            return cartValue;
        }

        /// <summary>
        /// Adds a specified item to the Cart stack
        /// </summary>
        /// <param name="item">
        /// Name and price of an item you want to put in the customer's Cart
        /// </param>
        public void AddToCart(Item item)
        {
            Cart.Push(item);
        }

        /// <summary>
        /// Pops an item from the Cart stack
        /// </summary>
        /// <returns>
        /// The popped item off the top of the Cart stack
        /// </returns>
        public Item RemoveFromCart()
        {
            return Cart.Pop();
        }

        /// <summary>
        /// Counts the number of items in the Cart stack
        /// </summary>
        /// <returns>
        /// Integer value indicating number of items in the Cart
        /// </returns>
        public int CartSize()
        {
            return Cart.Count;
        }

        /// <summary>
        /// ID getter/setter
        /// </summary>
        int getID() { return ID; }
        int setID(int ID) { return ID; }

        /// <summary>
        /// Uses if statement to determine how long the Cart should take to fill before checking out.
        /// Three time brackets from 5-12,13-21,22-30 items
        /// </summary>
        /// <returns>
        /// Integer value indicating the time slot of the Cart
        /// </returns>
        public int GetCartTime()
        {
            int temp = this.Cart.Count;
            if (temp < 12)
            {
                CartTime = 1;
            } else if (temp < 21)
            {
                CartTime = 2;
            } else
            {
                CartTime = 3;
            }
            return CartTime; 
        }
    }
}

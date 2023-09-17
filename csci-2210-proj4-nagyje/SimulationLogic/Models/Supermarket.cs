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
    /// This class is used as the main simulation code for the Supermarket simulator.
    /// The Run() method contains the processes in which the supermarket creates and manages the 
    /// Registers, Customers, and Items. It also updates variables that connect to the GUI 
    /// for visual output. There are two data manipulation methods: GetRegisters() and GetAverage()
    /// </summary>
    public class Supermarket : ISupermarket
    {

        //Attributes		
        internal List<Register> Registers;
        public int LongestLine { get; set; }
        public int CustomersArrived { get; set; }
        public int CustomersServed { get; set; }
        public double TotalSales { get; set; }
        public double AverageCustomerTotal { get; set; }
        public double MinimumCustomerTotal { get; set; }
        public double MaximumCustomerTotal { get; set; }
        public int[] CurrentCheckouts { get; set; }

        /// <summary>
        /// Constructor method
        /// No inputs are needed to create a Supermarket
        /// </summary>
        public Supermarket()
        {
            Registers = new List<Register>();
        }
        
        /// <summary>
        /// Grabs the List of active Register objects during the simulation
        /// </summary>
        /// <returns>
        /// List of Registers
        /// </returns>
        internal List<Register> GetRegisters()
        {
            return Registers;
        }

        /// <summary>
        /// Gets the average sale price per customer from 
        /// TotalSales and CustomersServed
        /// </summary>
        /// <returns>
        /// double - TotalSales/CustomersServed
        /// </returns>
        internal double GetAverage()
        {
            return TotalSales / CustomersServed;
        }

        /// <summary>
        /// Main method of the entire simulation.
        /// Creates the necessary items, registers, and customers to run the simulation.
        /// As the customers arrive, their cart size determines when they will begin the checkout process.
        /// Customers are split up into 6 different time slots, which all arrive at different intervals 
        /// than the last. Early/late customers arrive in groups of 3, slightly less early/late groups arrive
        /// as 6, and the mid-day groups arrive as 10. This simulates a curve of business at the supermarket.
        /// As they begin to check out, the customers are put into the shortest register line of all the active 
        /// registers. After they are put in, the register logistics are managed: if there is a register with 0
        /// people in line, it is closed; if all registers have 3+ people in line, a new register is opened.
        /// The line with the most people in it is checked at the end of the checkout loop, and this whole loop 
        /// is repeated until all the customers have arrived. After that, the checkout process is done in a seperate
        /// loop without a customer addition phase until all customers have checked out.
        /// </summary>
        public void Run()
        {
            //Your code to run the simulation goes here
            Random random = new Random();

            // Item names
            string[] itemNames1 = {"Wooden", "Bronze", "Silver", "Gold", "Platinum", "Titanium", "Adamantium" };
            string[] itemNames2 = { "Desk", "Spoon", "Fork", "Plate", "Knife", "Chalice", "Chair" };
            List<Item> itemList = new List<Item>();

            foreach (string itemName in itemNames1)
            {
                foreach (string itemName2 in itemNames2)
                {
                    itemList.Add(new Item(itemName + " " + itemName2, random.NextDouble() * 99.5 + .5));
                }
            }

            // Supermarket opens 5-15 registers
            int numRegisters = (int)random.NextInt64(10) + 5;
            int registerID = 1;
            for (int f = 0; f < numRegisters; f++)
            {
                Registers.Add(new Register(registerID));
                registerID++;
            }

            // 50-500 customers arrive
            int custRandom = (int)random.NextInt64(450) + 50;

            Customer[][] customerArray = new Customer[6][];
            customerArray[0] = new Customer[((int)((int)custRandom * .1))];
            customerArray[1] = new Customer[((int)((int)custRandom * .2))];
            customerArray[2] = new Customer[((int)((int)custRandom * .3))];
            customerArray[3] = new Customer[((int)((int)custRandom * .3))];
            customerArray[4] = new Customer[((int)((int)custRandom * .2))];
            customerArray[5] = new Customer[((int)((int)custRandom * .1))];
            int numCustomers = 0;
            foreach (Customer[] customerArray2 in customerArray)
            {
                foreach (Customer customer2 in customerArray2)
                {
                    numCustomers++;
                }
            }
            // Create all customers
            int tempID = 1;
            int i;
            for (i = 0; i < customerArray.Length; i++)
            {
                for (int j = 0; j < customerArray[i].Length; j++)
                {
                    customerArray[i][j] = new Customer(tempID);
                    // Give customers items
                    int numItems = (int)random.NextInt64(25) + 5;
                    for (int k = 0; k < numItems; k++)
                    {
                        customerArray[i][j].AddToCart(itemList[(int)random.NextInt64(itemList.Count)]);
                    }
                    tempID++;
                }
            }

            // main shopping loop
            int timeKeeper1 = 0;
            int t = 0;
            i = 0;
            CustomersServed = 0;
            MinimumCustomerTotal = 9999999;
            MaximumCustomerTotal = 0;
            CurrentCheckouts = new int[15];
            while (i < customerArray.Length)        // while the jagged array is not finished
            {
                while (t < customerArray[i].Length)     // while the current time slot is not finished
                {
                    timeKeeper1++;
                    // check out existing shoppers
                    if (i == 0 || i == 5)
                    {
                        // 1st and 6th groups (low traffic simulated)
                        if (timeKeeper1 == 3)
                        {
                            // handle cart size UI
                            for (int z = 0; z < Registers.Count; z++)
                            {
                                if (Registers[z].LineLength() > 0)
                                {
                                    CurrentCheckouts[z] = Registers[z].Peek().CartSize();
                                }
                            }
                            Thread.Sleep(500);
                            for (int p = 0; p < Registers.Count; p++)
                            {
                                if (Registers[p].LineLength() > 0)
                                {
                                    Customer tempCustomer = Registers[p].CheckOut();
                                    double tempTotal = tempCustomer.GetCartValue();
                                    // manage UI update info
                                    Registers[p].SetTotalSales(tempTotal + Registers[p].GetTotalSales());
                                    TotalSales = TotalSales + tempTotal;
                                    if (MinimumCustomerTotal > tempTotal)
                                    {
                                        MinimumCustomerTotal = tempTotal;
                                    }
                                    if (MaximumCustomerTotal < tempTotal)
                                    {
                                        MaximumCustomerTotal = tempTotal;
                                    }
                                    AverageCustomerTotal = GetAverage();
                                    timeKeeper1++;
                                    CustomersServed++;
                                }
                            }
                            timeKeeper1 = 0;
                        }
                    } else if (i == 1 || i == 4)
                    {
                        // 2nd and 5th groups (medium traffic simulated)
                        if (timeKeeper1 == 6)
                        {
                            Thread.Sleep(500);
                            for (int p = 0; p < Registers.Count; p++)
                            {
                                if (Registers[p].LineLength() > 0)
                                {
                                    Customer tempCustomer = Registers[p].CheckOut();
                                    double tempTotal = tempCustomer.GetCartValue();
                                    Registers[p].SetTotalSales(tempTotal + Registers[p].GetTotalSales());
                                    TotalSales = TotalSales + tempTotal;
                                    timeKeeper1++;
                                    CustomersServed++;
                                    AverageCustomerTotal = GetAverage();
                                }
                            }
                            timeKeeper1 = 0;
                        }
                    } else if (i == 2 || i == 3)
                    {
                        // 3rd and 4th groups (high traffic simulated)
                        if (timeKeeper1 == 10)
                        {
                            Thread.Sleep(500);
                            for (int p = 0; p < Registers.Count; p++)
                            {
                                if (Registers[p].LineLength() > 0)
                                {
                                    Customer tempCustomer = Registers[p].CheckOut();
                                    double tempTotal = tempCustomer.GetCartValue();
                                    Registers[p].SetTotalSales(tempTotal + Registers[p].GetTotalSales());
                                    TotalSales = TotalSales + tempTotal;
                                    timeKeeper1++;
                                    CustomersServed++;
                                    AverageCustomerTotal = GetAverage();
                                }
                            }
                            timeKeeper1 = 0;
                        }
                    }
                    // put shoppers in least busy line

                    int leastBusy = Registers[0].LineLength();
                    int leastBusyIndex = 0;
                    for (int n = 0; n < Registers.Count; n++)
                    {
                        if (leastBusy > Registers[n].LineLength())
                        {
                            leastBusy = Registers[n].LineLength();
                            leastBusyIndex = n;
                        }
                    }
                    Registers[leastBusyIndex].JoinLine(customerArray[i][t]);

                    // remove registers if there are no lines
                    if (Registers[Registers.Count - 1].LineLength() == 0)
                    {
                        Registers.RemoveAt(Registers.Count - 1);
                        registerID--;
                    }
                    // add new register if the lines are long
                    if (leastBusy > 2 && Registers.Count < 15)
                    {
                        Registers.Add(new Register(registerID++));
                    }

                    // check longest line
                    int mostBusy = Registers[0].LineLength();
                    int mostBusyIndex = 0;
                    for (int n = 0; n < Registers.Count; n++)
                    {
                        if (mostBusy < Registers[n].LineLength())
                        {
                            mostBusy = Registers[n].LineLength();
                            mostBusyIndex = n;
                        }
                    }
                    LongestLine = mostBusy;
                    CustomersArrived++;
                    t++;
                    
                }
                t = 0;
                i++;
            }
            
            // Check out remaining customers after all have arrived
            while (Registers[0].LineLength() != 0)
            {
                Thread.Sleep(500);
                for (int p = 0; p < Registers.Count; p++)
                {
                    if (Registers[p].LineLength() > 0)
                    {
                        Customer tempCustomer = Registers[p].CheckOut();
                        double tempTotal = tempCustomer.GetCartValue();
                        Registers[p].SetTotalSales(tempTotal + Registers[p].GetTotalSales());
                        TotalSales = TotalSales + tempTotal;
                        if (MinimumCustomerTotal > tempTotal)
                        {
                            MinimumCustomerTotal = tempTotal;
                        }
                        if (MaximumCustomerTotal < tempTotal)
                        {
                            MaximumCustomerTotal = tempTotal;
                        }
                        AverageCustomerTotal = GetAverage();
                        CustomersServed++;
                    }
                }
            }
            
            // Give UI time to refresh last iteration
            for (i = 0; i < 2; i++)
            {
                Thread.Sleep(500);
            }

        }
    }
}

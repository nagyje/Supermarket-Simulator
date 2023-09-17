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
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SimulationLogic
{
    public class GuiController : IGuiController
    {
        /// <summary>
        /// Implements <see cref="IGuiController.UpdateUI(Supermarket, string[], List{string}[], ISupermarketStatistics)"/>
        /// </summary>
        public void UpdateUI(Supermarket supermarket, string[] queueLabels, List<string>[] queueOfCustomers, ISupermarketStatistics supermarketStatistics)
        {
            // Info at bottom of UI
            supermarketStatistics.AverageCustomerTotal = (decimal)supermarket.AverageCustomerTotal;
            supermarketStatistics.TotalSales = (decimal)supermarket.TotalSales;
            supermarketStatistics.MaximumCustomerTotal = (decimal)supermarket.MaximumCustomerTotal;
            supermarketStatistics.MinimumCustomerTotal = (decimal)supermarket.MinimumCustomerTotal;
            supermarketStatistics.LongestLine = supermarket.LongestLine;
            supermarketStatistics.CustomersArrived = supermarket.CustomersArrived;
            supermarketStatistics.CustomersDeparted = supermarket.CustomersServed;

            // ListBox updates
            List<string>[] registerQueueIDs = new List<string>[15];
            List<string> blank = new List<string>();
            blank.Add(" ");

            for (int i = 0; i < supermarket.Registers.Count; i++)
            {
                registerQueueIDs[i] = supermarket.Registers[i].GetIDs();
                queueOfCustomers[i] = registerQueueIDs[i];
            }
            // Erase ghost customers from unused registers
            for (int i = 0; i + supermarket.Registers.Count < 15; i++) 
            {
                queueOfCustomers[i + supermarket.Registers.Count] = blank;
            }
            
            // ListBox Labels
            string[] labels = new string[15];
            for (int i = 0; i < supermarket.Registers.Count; i++)
            {
                if (supermarket.Registers[i].LineLength() > 0) 
                { 
                    string tempHold = supermarket.Registers[i].GetID() + ": " + supermarket.Registers[i].Peek().CartSize();
                    if (i < 9)
                    {
                        tempHold = "0" + tempHold;
                    }
                    queueLabels[i] = tempHold;
                }
            }

        }
    }
}

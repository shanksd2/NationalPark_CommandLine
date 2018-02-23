using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading;
using Capstone.Models;
using Capstone.DAL;

namespace Capstone
{
    public class ParkReservationSystem_CLI
    {
        private string databaseconnectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
        private int inputValue = 0; 
        public int InputValue { get; set; }
        public void RunCLI()
        {
            PrintHeader();
            PrintMainMenu();
            int inputValue = 0;

            while (true)
            {
                string input = Console.ReadLine();
                // Assign user input to integer value to pass through proceeding methods

                int.TryParse(input, out this.inputValue);
                if (input.ToUpper() == "Q")
                {
                    return;
                }
                else
                {
                    if (CLI_Helper.ParkExists(input))
                    {
                        Console.WriteLine();
                        PrintParkDetails(inputValue);
                        ParkMenu(inputValue);
                        CampGroundView();
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input, Please Try Again.");
                        Thread.Sleep(2000);
                    }
                }
                PrintMainMenu();
            }
        }

        public void PrintHeader()
        {
            Console.WriteLine("National Park Database - Reservation System!");
        }

        public void PrintMainMenu()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Select a park for further details:");
            ParkSqlDAL menuPrint = new ParkSqlDAL(databaseconnectionString);
            List<Park> parksToPrint = menuPrint.ListAllParkNames();

            for (int i = 0; i < parksToPrint.Count; i++)
            {
                Console.WriteLine((i + 1).ToString() + ") " + parksToPrint[i].Name);
            }
            Console.WriteLine("Q) Quit");
        }

        public void PrintParkDetails(int input)
        {

            ParkSqlDAL parkDetails = new ParkSqlDAL(databaseconnectionString);
            List<Park> listOfParks = parkDetails.ListAllParkNames();

            Park parkToDetail = parkDetails.GetParkDetails(listOfParks[inputValue].Name.ToString());
            Console.WriteLine(parkToDetail.ToString());
        }

        public void ParkMenu(int input)
        {
            Console.WriteLine();
            Console.WriteLine("Select a Command");
            Console.WriteLine("1) View Campgrounds");
            Console.WriteLine("2) Search for Reservation");
            Console.WriteLine("3) Return to Previous Screen");
            Console.WriteLine();
            int menuInput = 0;

            bool wegood = true;
            while (wegood)
            {
                int.TryParse(Console.ReadLine(), out menuInput);
                switch (menuInput)
                {
                    case 1:
                        PrintParkCampGround(input);
                        wegood = false;
                        break;
                    case 2:
                        
                        wegood = false;
                        break;
                    case 3:
                        return;
                    default:
                        Console.WriteLine("Invalid Input. Please try another value.");
                        break;
                }
            }
        }

        public int GetParkID(int input)
        {
            ParkSqlDAL toFindID = new ParkSqlDAL(databaseconnectionString);
            List<Park> findID = toFindID.ListAllParkNames();
            int ID = findID[input].Park_id;
            return ID+1;
        }

        public void PrintParkCampGround(int input)
        {
            Console.Clear();
            CampGroundSqlDAL campGroundsinPark = new CampGroundSqlDAL(databaseconnectionString);
            List<Campground> printCampGrounds = campGroundsinPark.GetParkCampGround(GetParkID(input));

            Console.WriteLine("\tName".PadRight(25)+ "Open".PadRight(13) + "Close".PadRight(13) + "Daily Fee");
            int count = 0;
            foreach (Campground campground in printCampGrounds)
            {
                Console.WriteLine($"#{count + 1} \t{printCampGrounds[count].ToString()}");
                count++;
            }

        }

        public void CampGroundView()
        {
            Console.WriteLine();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) Search for Available Reservation");
            Console.WriteLine("2) Return to Previous Screen");
            string stringDecision = Console.ReadLine();
            int decision = 0;

            bool makingDecision = true;
            while (makingDecision)
            {
                int.TryParse(stringDecision, out decision);
                switch (decision)
                {
                    case 1:
                        ReservationMenu();
                        makingDecision = false;
                        break;
                    case 2:
                        return;
                    default:
                        Console.WriteLine("Invalid input. Enter another value:");
                        break;
                }
            }
        }

        public void ReservationMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Which campground (enter 0 to cancel)?");
            string campgroundString = Console.ReadLine();
            int campgroundInput = 0;
            int.TryParse(campgroundString, out campgroundInput);
            Console.WriteLine("What is the arrival date? _/_/__");
            string arrivalDateString = Console.ReadLine();
            Console.WriteLine("What is the departure date? _/_/__");
            string departureDateString = Console.ReadLine();

            SearchAvailableSites(campgroundInput, arrivalDateString, departureDateString);
            Console.ReadLine();
        }

        public void SearchAvailableSites(int camp_id, string arrival, string departure)
        {
            Console.Clear();
            SiteSqlDAL reservationAvailibility = new SiteSqlDAL(databaseconnectionString);
            List<Site> availableSites = reservationAvailibility.ReservationAvailable(camp_id, arrival, departure);
            CampGroundSqlDAL campGroundsinPark = new CampGroundSqlDAL(databaseconnectionString);
            List<Campground> printCampGrounds = campGroundsinPark.GetParkCampGround(GetParkID(inputValue));

            Console.WriteLine("Results Matching Your Search Criteria");
            Console.WriteLine("Site No.".PadRight(15) + "Max Occup.".PadRight(15) + "Accessible?".PadRight(15) + "Max RV Length".PadRight(15) + "Utility".PadRight(15) + "Cost".PadRight(15));

            foreach(Site s in availableSites)
            {
                Console.WriteLine(s.ToString()+ reservationAvailibility.PrintCost(s));

            }
        }
    }
}

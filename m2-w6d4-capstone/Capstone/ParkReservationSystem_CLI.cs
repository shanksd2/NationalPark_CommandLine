using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading;
using Capstone.Models;
using Capstone.DAL;
using System.Data.SqlClient;

namespace Capstone
{
    public class ParkReservationSystem_CLI
    {
        private string databaseconnectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
        public bool menuInComplete = true;
        public void RunCLI()
        {
            while (true)
            {
                Console.Clear();
                PrintHeader();
                PrintMainMenu();

                // Assign user input to integer value to pass through proceeding methods
                int parkSelection = CLI_Helper.GetInteger("==>");

                //Checks if a user input a string value of 'Q'. CLI_HELPER.GetInteger returns 0 in this case.
                if(parkSelection == 0)
                {
                    break;
                }
                else if (CLI_Helper.ParkExists(parkSelection))
                {
                    Console.WriteLine();
                    PrintParkDetails(parkSelection);
                    ParkMenu(parkSelection);
                }
                else
                {
                    Console.WriteLine("Invalid Input, Please Try Again.");
                    Thread.Sleep(2000);
                }

            }
        }

        public void PrintHeader()
        {
            Console.WriteLine("* * * * * * * * * * * * * * * * * * * * * * * * *");
            Console.WriteLine("*                                               *");
            Console.WriteLine("*  National Park Database - Reservation System  *");
            Console.WriteLine("*                                               *");
            Console.WriteLine("* * * * * * * * * * * * * * * * * * * * * * * * *");
        }

        public void PrintMainMenu()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Select a park for further details:  ");
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
            Console.Clear();
            ParkSqlDAL parkDetails = new ParkSqlDAL(databaseconnectionString);
            List<Park> listOfParks = parkDetails.ListAllParkNames();
            Park parkToDetail = new Park();
            try
            {
                parkToDetail = parkDetails.GetParkDetails(listOfParks[input - 1].Name.ToString());
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Invalid input.  Please try again.");
                return;
            }
            Console.WriteLine(parkToDetail.ToString());
            Console.WriteLine();
            Park.WrapText(parkToDetail);
        }

        public void ParkMenu(int parkSelection)
        {
            Console.WriteLine();
            Console.WriteLine("Select a Command");
            Console.WriteLine("1) View Campgrounds");
            Console.WriteLine("2) Search for Reservation");
            Console.WriteLine("3) Return to Previous Screen");
            Console.WriteLine();            

            bool wegood = true;
            while (wegood)
            {
                int menuInput = CLI_Helper.GetInteger("==>");
                switch (menuInput)
                {
                    case 1:
                        PrintParkCampGround(parkSelection);
                        wegood = false;
                        break;
                    case 2:
                        ParkWideReservations(parkSelection);
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

        public void PrintParkCampGround(int parkSelection)
        {
            Console.Clear();
            CampGroundSqlDAL campGroundsinPark = new CampGroundSqlDAL(databaseconnectionString);
            ParkSqlDAL park = new ParkSqlDAL(databaseconnectionString);
            List<Campground> printCampGrounds = new List<Campground>();
            try
            {
                printCampGrounds = campGroundsinPark.GetParkCampGround(park.GetParkID(parkSelection));
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Invalid input. Please try again.");
                return;
            }
            Console.WriteLine("\tName".PadRight(35) + "Open".PadRight(13) + "Close".PadRight(13) + "Daily Fee");
            int count = 0;
            foreach (Campground campground in printCampGrounds)
            {
                Console.WriteLine($"#{count + 1} \t{printCampGrounds[count].ToString()}");
                count++;
            }
            CampGroundView(parkSelection);
        }

        public void CampGroundView(int parkSelection)
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
                        ReservationMenu(parkSelection);
                        makingDecision = false;
                        break;
                    case 2:
                        return;
                    default:
                        Console.WriteLine("Invalid input. Enter another value:");
                        makingDecision = false;
                        break;
                }
            }
        }

        public void ReservationMenu(int parkSelection)
        {
            Console.WriteLine();
            int campgroundInput = 0;
            string arrivalDateString = string.Empty;
            string departureDateString = string.Empty;

            while (menuInComplete)
            {
                Console.Write("Which campground (enter 0 to cancel)?  ");
                string campgroundString = Console.ReadLine();
                if (campgroundString == "0")
                {
                    return;
                }
                int.TryParse(campgroundString, out campgroundInput);
                Console.Write("Please enter desired arrival date. _/_/__  ");
                arrivalDateString = Console.ReadLine();
                Console.Write("Please enter desired departure date. _/_/__  ");
                departureDateString = Console.ReadLine();
                SearchAvailableSitesInCampGround(parkSelection, campgroundInput, arrivalDateString, departureDateString);
            }
            Console.ReadLine();
        }

        public void ParkWideReservations(int parkSelection)
        {
            Console.WriteLine();
            Console.Write("Please enter desired arrival date. _/_/__   ");
            string arrivalDateString = Console.ReadLine();
            Console.Write("Please enter desired departure date. _/_/__ ");
            string departureDateString = Console.ReadLine();
            SearchAvailableSitesInPark(arrivalDateString, departureDateString, parkSelection);
        }

        public virtual void SearchAvailableSitesInCampGround(int parkSelection, int camp_id, string arrival, string departure)
        {
            Console.Clear();
            SiteSqlDAL reservationAvailibility = new SiteSqlDAL(databaseconnectionString);
            List<Site> availableSites = new List<Site>();
            try
            {
                availableSites = reservationAvailibility.ReservationAvailable(camp_id, arrival, departure);
            }
            catch (SqlException ex)
            {
                Console.WriteLine();
                Console.WriteLine("Invalid input.  Please try again.");
                Console.WriteLine();
                return;
            }

            Console.WriteLine();
            if (reservationAvailibility.AnyAvailable(availableSites))
            {
                Console.WriteLine("Results Matching Your Search Criteria");
                Console.WriteLine("Site No.".PadRight(15) + "Max Occup.".PadRight(15) + "Accessible?".PadRight(15) + "Max RV Length".PadRight(15) + "Utility".PadRight(15) + "Cost".PadRight(15));

                foreach (Site s in availableSites)
                {
                    Console.WriteLine(s.ToString() + reservationAvailibility.PrintCost(s, arrival, departure));
                }
                ReservationConfirmation(camp_id, arrival, departure, parkSelection);
            }
            else
            {
                Console.Write("No Available Sites. Would you like to enter alternate dates? (Y / N)  ");
                string answer = Console.ReadLine();
                if (answer.ToUpper() == "N")
                {
                    menuInComplete = false;
                    return;
                }
                else if (answer.ToUpper() == "Y")
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Sorry I don't understand.");
                    return;
                }
            }
        }

        public void SearchAvailableSitesInPark(string arrival, string departure, int parkSelection)
        {
            Console.Clear();
            SiteSqlDAL reservationAvailibility = new SiteSqlDAL(databaseconnectionString);
            List<Site> availableSites = new List<Site>();
            try
            {
                availableSites = reservationAvailibility.ReservationAvailable(arrival, departure, parkSelection);
            }
            catch (SqlException ex)
            {
                Console.WriteLine();
                Console.WriteLine("Invalid input.  Please try again.");
                Console.WriteLine();
                return;
            }
            CampGroundSqlDAL campGroundsinPark = new CampGroundSqlDAL(databaseconnectionString);
            ParkSqlDAL park = new ParkSqlDAL(databaseconnectionString);
            List<Campground> printCampGrounds = new List<Campground>();
            try
            {
                printCampGrounds = campGroundsinPark.GetParkCampGround(park.GetParkID(parkSelection));
            }
            catch (SqlException ex)
            {
                Console.WriteLine();
                Console.WriteLine("Invalid input.  Please try again.");
                Console.WriteLine();
                return;
            }
            Console.WriteLine();
            Console.WriteLine("Results Matching Your Search Criteria");
            Console.WriteLine("CampGround".PadRight(32) + "Site No.".PadRight(15) + "Max Occup.".PadRight(15) + "Accessible?".PadRight(15) + "Max RV Length".PadRight(15) + "Utility".PadRight(15) + "Cost".PadRight(15));

            int camp_id = 0;
            foreach (Site s in availableSites)
            {
                string campName = string.Empty;
                foreach (Campground camp in printCampGrounds)
                {
                    if (camp.Campground_id == s.Campground_id)
                    {
                        campName = camp.Name;
                        camp_id = camp.Campground_id;
                    }
                }
                Console.WriteLine((campName + " ID:" + (camp_id).ToString()).PadRight(32) + s.ToString() + reservationAvailibility.PrintCost(s, arrival, departure));
            }
            ReservationConfirmationForPark(arrival, departure, camp_id, parkSelection);
            Console.ReadLine();
        }

        public void ReservationConfirmation(int camp_id, string arrivalDate, string departureDate, int parkSelection)
        {
            SiteSqlDAL site = new SiteSqlDAL(databaseconnectionString);
            ReservationSqlDAL bookReservation = new ReservationSqlDAL(databaseconnectionString);
            Console.WriteLine();

            int siteInput = CLI_Helper.GetInteger("Which site should be reserved? (enter 0 to cancel)  ");
            if (siteInput == 0)
            {
                return;
            }
            Console.WriteLine();

            Console.Write("Under what name should the reservation be held?  ");
            string inputName = Console.ReadLine();

            bookReservation.MakeReservation(parkSelection, site.GetSiteID(siteInput, camp_id), inputName, arrivalDate, departureDate);
            string reservationID = bookReservation.GetReservationId(inputName);
            Thread.Sleep(2000);
            Console.WriteLine();
            Console.WriteLine($"The reservation has been booked and the confirmation ID is: {reservationID}");
            Console.ReadLine();
        }

        public void ReservationConfirmationForPark(string arrivalDate, string departureDate, int camp_id, int parkSelection)
        {
            SiteSqlDAL site = new SiteSqlDAL(databaseconnectionString);
            ReservationSqlDAL bookReservation = new ReservationSqlDAL(databaseconnectionString);
            Console.WriteLine();
            Console.Write("Which campground should be reserved? (enter 0 to cancel)  ");
            int campInput = CLI_Helper.GetInteger("==>");
            if (campInput == 0)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("Which camp site should be reserved? (enter 0 to cancel)   ");
            int siteInput = CLI_Helper.GetInteger("==>");
            if (siteInput == 0)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("Under what name should the reservation be held?  ");
            string inputName = Console.ReadLine();

            bookReservation.MakeReservation(parkSelection, site.GetSiteID(siteInput, camp_id), inputName, arrivalDate, departureDate);
            string reservationID = bookReservation.GetReservationId(inputName);
            Thread.Sleep(2000);
            Console.WriteLine();
            Console.WriteLine($"The reservation has been booked and the confirmation ID is: {reservationID}");
            Console.ReadLine();
        }
    }
}

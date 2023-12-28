using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RGR.ModelClasses;

namespace RGR
{
    public class ViewClass
    {
        public void MainMenu()
        {
            Console.WriteLine(" -------------------------- ");
            Console.WriteLine("|A platform for booking and|");
            Console.WriteLine("|managing venues for events|");
            Console.WriteLine(" -------------------------- ");
            Console.WriteLine("       All tables:");
            Console.WriteLine(" 1. Event");
            Console.WriteLine(" 2. Owner");
            Console.WriteLine(" 3. Location");
            Console.WriteLine(" 4. Event Name");
            Console.WriteLine(" 5. Event Loctaion");
            Console.WriteLine(" Escape. Close program");
            Console.WriteLine(" -------------------------- ");
        }
        public void TableMenu(string tablename)
        {
            Console.WriteLine();
            Console.WriteLine("| " + tablename + " |");
            Console.WriteLine(" 1. Print");
            Console.WriteLine(" 2. Add");
            Console.WriteLine(" 3. Remove");
            Console.WriteLine(" 4. Update");
            Console.WriteLine(" 5. Search");
            Console.WriteLine(" 6. Generate");
            Console.WriteLine("--------------- ");
        }
        public void ClearConsole()
        {
            Console.Clear();
        }
        public void Pause()
        {
            Console.WriteLine(" \nPress any key to continue");
            Console.ReadKey();
        }
        public void PrintMessage(string message)
        {
            Console.WriteLine($"{message}");
        }
        public void GetAllEvent(List<TEvent> list)
        {
            Console.WriteLine(" --------- ");
            Console.WriteLine("| Events: |");
            Console.WriteLine(" -------------------------------------------------------- ");
            Console.WriteLine("|{0,3}|{1,20}|{2,20}|{3,6}|", "Id", "Event name", "Event theme", "Event date");
            Console.WriteLine("|--------------------------------------------------------|");
            foreach (TEvent e in list) 
            {
                Console.WriteLine("|{0,3}|{1,20}|{2,20}|{3,6}|", e.Id, e.EventName, e.Theme, e.EventDate);
            }
            Console.WriteLine(" -------------------------------------------------------- ");
        }
        public void GetAllEventLocation(List<TEventLocation> list)
        {
            Console.WriteLine(" ----------------- ");
            Console.WriteLine("| Event Location: |");
            Console.WriteLine(" -------------------------------------------------------- ");
            Console.WriteLine("|{0,3}|{1,25}|{2,25}|", "Id", "Event name", "Location name");
            Console.WriteLine(" -------------------------------------------------------- ");
            foreach (TEventLocation e in list)
            {
                Console.WriteLine("|{0,3}|{1,25}|{2,25}|", e.Id, e.EventName, e.LocationName);
            }
            Console.WriteLine(" -------------------------------------------------------- ");
        }
        public void GetAllEventName(List<TEventName> list)
        {
            Console.WriteLine(" ------------- ");
            Console.WriteLine("| Event Name: |");
            Console.WriteLine(" ------------------------- ");
            Console.WriteLine("|{0,3}|{1,20}|", "Id", "Event name");
            Console.WriteLine(" ------------------------- ");
            foreach (TEventName e in list)
            {
                Console.WriteLine("|{0,3}|{1,20}|", e.Id, e.Name);
            }
            Console.WriteLine(" ------------------------- ");
        }
        public void GetAllLocation(List<TLocation> list)
        {
            Console.WriteLine(" ----------- ");
            Console.WriteLine("| Location: |");
            Console.WriteLine(" ----------- ");
            Console.WriteLine(" --------------------------------------------------------------------------------------------- ");
            Console.WriteLine("|{0,3}|{1,25}|{2,30}|{3,5}|{4,25}|", "Id", "Name", "Address", "Seats","Owner");
            Console.WriteLine(" --------------------------------------------------------------------------------------------- ");
            foreach (TLocation e in list)
            {
                Console.WriteLine("|{0,3}|{1,25}|{2,30}|{3,5}|{4,25}|", e.Id, e.Name, e.Address, e.NumberOfSeats, e.Owner);
            }
            Console.WriteLine(" --------------------------------------------------------------------------------------------- ");
        }
        public void GetAllOwner(List<TOwner> list)
        {
            Console.WriteLine(" -------- ");
            Console.WriteLine("| Owner: |");
            Console.WriteLine(" -------- ");
            Console.WriteLine(" ---------------------------------------------- ");
            Console.WriteLine("|{0,3}|{1,25}|{2,15}|", "Id", "Name", "Phone");
            Console.WriteLine(" ---------------------------------------------- ");
            foreach (TOwner e in list)
            {
                Console.WriteLine("|{0,3}|{1,25}|{2,15}|", e.Id, e.Name, e.Phone);
            }
            Console.WriteLine(" ---------------------------------------------- ");

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RGR.ModelClasses;

namespace RGR
{
    public class ControllerClass
    {
        private ModelClass model;
        private ViewClass view;
        public ControllerClass() 
        {
            model = new ModelClass();
            view = new ViewClass();
        }

        public void Start()
        {
            while (true)
            {
                view.ClearConsole();
                view.MainMenu();
                ConsoleKeyInfo key1 = Console.ReadKey();
                if (key1.Key == ConsoleKey.Escape)
                    break;
                else if (key1.Key == ConsoleKey.D1)
                {
                    view.TableMenu("Event");
                    ConsoleKeyInfo key2 = Console.ReadKey();
                    if (key2.Key == ConsoleKey.D1)
                    {
                        view.GetAllEvent(model.GetAllEvent());
                    }
                    else if (key2.Key == ConsoleKey.D2)
                    {
                        view.PrintMessage("\nInput data about new event:");
                        TEvent e = new TEvent();
                        view.PrintMessage(" Name: ");
                        e.EventName = Console.ReadLine();
                        view.PrintMessage(" Theme: ");
                        e.Theme = Console.ReadLine();
                        view.PrintMessage(" Date: ");
                        e.EventDate = DateOnly.FromDateTime(Convert.ToDateTime(Console.ReadLine()));

                        int result = model.AddEvent(e);
                        if (result == 0)
                        {
                            view.PrintMessage(" Error. You have entered unexisting event name.");
                        }
                        else
                        {
                            view.PrintMessage(" You have added a new row into table.");
                        }
                    }
                    if (key2.Key == ConsoleKey.D3)
                    {
                        view.GetAllEvent(model.GetAllEvent());
                        view.PrintMessage(" Choose the ID of Event which you want to remove.");
                        string str = Console.ReadLine();
                        int index = str == "" ? 0 : Convert.ToInt32(str);

                        int result = model.DeleteEvent(index);
                        if (result == 0)
                        {
                            view.PrintMessage(" You can not remove this row. It is connected with other row");
                        }
                        else if (result == -1)
                        {
                            view.PrintMessage(" You can not remove this row. Invalid ID");
                        }
                        else if (result == 1)
                        {
                            view.PrintMessage(" You have removed this row from table.");
                        }
                    }
                    if (key2.Key == ConsoleKey.D4)
                    {
                        view.GetAllEvent(model.GetAllEvent());
                        view.PrintMessage(" Choose the ID of Event which you want to update.");
                        int i = Convert.ToInt32(Console.ReadLine());

                        view.PrintMessage("\nInput new data about event:");
                        TEvent e = new TEvent();
                        view.PrintMessage(" Name: ");
                        e.EventName = Console.ReadLine();
                        view.PrintMessage(" Theme: ");
                        e.Theme = Console.ReadLine();
                        view.PrintMessage(" Date: ");
                        e.EventDate = DateOnly.FromDateTime(Convert.ToDateTime(Console.ReadLine()));

                        int result = model.UpdateEvent(i, e);
                        if (result == 0)
                        {
                            view.PrintMessage(" Error. You have entered unexisting ID.");
                        }
                        else if (result == -1)
                        {
                            view.PrintMessage(" Error. You have entered unexisting event name ");
                        }
                        else if (result == 1)
                        {
                            view.PrintMessage(" You have updated this row.");
                        }
                    }
                    if (key2.Key == ConsoleKey.D5)
                    {
                        view.PrintMessage(" Choose the key of search:");
                        view.PrintMessage(" 1. Event name");
                        view.PrintMessage(" 2. Theme");
                        view.PrintMessage(" 3. Event date");
                        ConsoleKeyInfo key3 = Console.ReadKey();
                        if (key3.Key == ConsoleKey.D1)
                        {
                            view.PrintMessage("  Enter event name:");
                            string name = Console.ReadLine();
                            view.GetAllEvent(model.SearchEventByName(name));
                        }
                        else if (key3.Key == ConsoleKey.D2)
                        {
                            view.PrintMessage("  Enter event name:");
                            string theme = Console.ReadLine();
                            view.GetAllEvent(model.SearchEventByTheme(theme));
                        }
                        else if (key3.Key == ConsoleKey.D3)
                        {
                            view.PrintMessage("  Enter event date:");
                            DateOnly date = DateOnly.FromDateTime(Convert.ToDateTime(Console.ReadLine()));
                            view.GetAllEvent(model.SearchEventByDate(date));
                        }
                    }
                    if (key2.Key == ConsoleKey.D6)
                    {
                        view.PrintMessage(" How many rows do you want to generate?:");
                        int n = Convert.ToInt32(Console.ReadLine());
                        model.GenerateEvents(n);
                        view.PrintMessage("  New rows were generated.");
                    }
                    view.Pause();
                }
                else if (key1.Key == ConsoleKey.D2)////////////////////////////////////////////////
                {
                    view.TableMenu("Owner");
                    ConsoleKeyInfo key2 = Console.ReadKey();
                    if (key2.Key == ConsoleKey.D1)
                    {
                        view.GetAllOwner(model.GetAllOwner());
                    }
                    else if (key2.Key == ConsoleKey.D2)
                    {
                        view.PrintMessage("\nInput data about new owner:");
                        TOwner o = new TOwner();
                        view.PrintMessage(" Name: ");
                        o.Name = Console.ReadLine();
                        view.PrintMessage(" Phone number: ");
                        o.Phone = Console.ReadLine();

                        int result = model.AddOwner(o);
                        if (result == 0)
                        {
                            view.PrintMessage(" Error.");
                        }
                        else
                        {
                            view.PrintMessage(" You have added a new row into table.");
                        }
                    }
                    if (key2.Key == ConsoleKey.D3)
                    {
                        view.GetAllOwner(model.GetAllOwner());
                        view.PrintMessage(" Choose the ID of Event which you want to remove.");
                        string str = Console.ReadLine();
                        int index = str == "" ? 0 : Convert.ToInt32(str);

                        int result = model.DeleteOwner(index);
                        if (result == 0)
                        {
                            view.PrintMessage(" You can not remove this row. It is connected with other row");
                        }
                        else if (result == -1)
                        {
                            view.PrintMessage(" You can not remove this row. Invalid ID");
                        }
                        else if (result == 1)
                        {
                            view.PrintMessage(" You have removed this row from table.");
                        }
                    }
                    if (key2.Key == ConsoleKey.D4)
                    {
                        view.GetAllOwner(model.GetAllOwner());
                        view.PrintMessage(" Choose the ID of owner which you want to update.");
                        int i = Convert.ToInt32(Console.ReadLine());

                        view.PrintMessage("\nInput new data about owner:");
                        TOwner o = new TOwner();
                        view.PrintMessage(" Name: ");
                        o.Name = Console.ReadLine();
                        view.PrintMessage(" Phone number: ");
                        o.Phone = Console.ReadLine();

                        int result = model.UpdateOwner(i, o);
                        if (result == 0)
                        {
                            view.PrintMessage(" Error. You have entered unexisting ID.");
                        }
                        else if (result == 1)
                        {
                            view.PrintMessage(" You have updated this row.");
                        }
                    }
                    if (key2.Key == ConsoleKey.D5)
                    {
                        view.PrintMessage(" Choose the key of search:");
                        view.PrintMessage(" 1. Owner name");
                        view.PrintMessage(" 2. Phone number");
                        ConsoleKeyInfo key3 = Console.ReadKey();
                        if (key3.Key == ConsoleKey.D1)
                        {
                            view.PrintMessage("  Enter owner name:");
                            string name = Console.ReadLine();
                            view.GetAllOwner(model.SearchOwnerByName(name));
                        }
                        else if (key3.Key == ConsoleKey.D2)
                        {
                            view.PrintMessage("  Enter phone number:");
                            string number = Console.ReadLine();
                            view.GetAllOwner(model.SearchOwnerByPhone(number));
                        }
                    }
                    if (key2.Key == ConsoleKey.D6)
                    {
                        view.PrintMessage(" How many rows do you want to generate?:");
                        int n = Convert.ToInt32(Console.ReadLine());
                        model.GenerateOwner(n);
                        view.PrintMessage("  New rows were generated.");
                    }
                    view.Pause();
                }
                else if (key1.Key == ConsoleKey.D3)
                {
                    view.TableMenu("Location");
                    ConsoleKeyInfo key2 = Console.ReadKey();
                    if (key2.Key == ConsoleKey.D1)
                    {
                        view.GetAllLocation(model.GetAllLocation());
                    }
                    else if (key2.Key == ConsoleKey.D2)
                    {
                        view.PrintMessage("\nInput data about new location:");
                        TLocation location = new TLocation();
                        view.PrintMessage(" Name: ");
                        location.Name = Console.ReadLine();
                        view.PrintMessage(" Address: ");
                        location.Address = Console.ReadLine();
                        view.PrintMessage(" Number of seats: ");
                        location.NumberOfSeats = Convert.ToInt32(Console.ReadLine());
                        view.PrintMessage(" Owner: ");
                        location.Owner = Console.ReadLine();

                        int result = model.AddLocation(location);
                        if (result == 0)
                        {
                            view.PrintMessage(" Error. You have entered unexisting owner's name");
                        }
                        else
                        {
                            view.PrintMessage(" You have added a new row into table.");
                        }
                    }
                    if (key2.Key == ConsoleKey.D3)
                    {
                        view.GetAllLocation(model.GetAllLocation());
                        view.PrintMessage(" Choose the ID of location which you want to remove.");
                        string str = Console.ReadLine();
                        int index = str == "" ? 0 : Convert.ToInt32(str);

                        int result = model.DeleteLocation(index);
                        if (result == 0)
                        {
                            view.PrintMessage(" You can not remove this row. It is connected with other row");
                        }
                        else if (result == -1)
                        {
                            view.PrintMessage(" You can not remove this row. Invalid ID.");
                        }
                        else if (result == 1)
                        {
                            view.PrintMessage(" You have removed this row from table.");
                        }
                    }
                    if (key2.Key == ConsoleKey.D4)
                    {
                        view.GetAllLocation(model.GetAllLocation());
                        view.PrintMessage(" Choose the ID of location which you want to update.");
                        int i = Convert.ToInt32(Console.ReadLine());

                        view.PrintMessage("\nInput new data about location:");
                        TLocation location = new TLocation();
                        view.PrintMessage(" Name: ");
                        location.Name = Console.ReadLine();
                        view.PrintMessage(" Address: ");
                        location.Address = Console.ReadLine();
                        view.PrintMessage(" Number of seats: ");
                        location.NumberOfSeats = Convert.ToInt32(Console.ReadLine());
                        view.PrintMessage(" Owner: ");
                        location.Owner = Console.ReadLine();

                        int result = model.UpdateLocation(i, location);
                        if (result == 0)
                        {
                            view.PrintMessage(" Error. You have entered unexisting owner.");
                        }
                        else if (result == -1)
                        {
                            view.PrintMessage(" Error. Invalid ID.");
                        }
                        else if (result == 1)
                        {
                            view.PrintMessage(" You have updated this row.");
                        }
                    }
                    if (key2.Key == ConsoleKey.D5)
                    {
                        view.PrintMessage(" Choose the key of search:");
                        view.PrintMessage(" 1. Location name");
                        view.PrintMessage(" 2. Location address");
                        view.PrintMessage(" 3. Number of seats");
                        view.PrintMessage(" 4. Owner name");
                        ConsoleKeyInfo key3 = Console.ReadKey();
                        if (key3.Key == ConsoleKey.D1)
                        {
                            view.PrintMessage("  Enter location name:");
                            string name = Console.ReadLine();
                            view.GetAllLocation(model.SearchLocationByName(name));
                        }
                        else if (key3.Key == ConsoleKey.D2)
                        {
                            view.PrintMessage("  Enter address:");
                            string address = Console.ReadLine();
                            view.GetAllLocation(model.SearchLocationByAddress(address));
                        }
                        else if (key3.Key == ConsoleKey.D3)
                        {
                            view.PrintMessage("  Enter a minimal number of seats:");
                            string str = Console.ReadLine();
                            int min = str == "" ? 0 : Convert.ToInt32(str);
                            view.PrintMessage("  Enter a minimal maximum of seats:");
                            str = Console.ReadLine();
                            int max = str == "" ? 0 : Convert.ToInt32(str);
                            view.GetAllLocation(model.SearchLocationByNumberOfSeats(min, max));
                        }
                        else if (key3.Key == ConsoleKey.D4)
                        {
                            view.PrintMessage("  Enter owner name:");
                            string name = Console.ReadLine();
                            view.GetAllLocation(model.SearchLocationByOwner(name));
                        }
                    }
                    else if (key2.Key == ConsoleKey.D6)
                    {
                        view.PrintMessage(" How many rows do you want to generate?:");
                        int n = Convert.ToInt32(Console.ReadLine());
                        model.GenerateLocation(n);
                        view.PrintMessage("  New rows were generated.");
                    }
                    view.Pause();

                }
                else if (key1.Key == ConsoleKey.D4) 
                {
                    view.TableMenu("Event Name");
                    ConsoleKeyInfo key2 = Console.ReadKey();
                    if (key2.Key == ConsoleKey.D1)
                    {
                        view.GetAllEventName(model.GetAllEventName());
                    }
                    else if (key2.Key == ConsoleKey.D2)
                    {
                        view.PrintMessage("\nInput new event name:");
                        TEventName name = new TEventName();
                        view.PrintMessage(" Name: ");
                        name.Name = Console.ReadLine();
                        
                        int result = model.AddEventName(name);
                        if (result == 0)
                        {
                            view.PrintMessage(" Error.");
                        }
                        else
                        {
                            view.PrintMessage(" You have added a new row into table.");
                        }
                    }
                    if (key2.Key == ConsoleKey.D3)
                    {
                        view.GetAllEventName(model.GetAllEventName());
                        view.PrintMessage(" Choose the ID of Event Name which you want to remove.");
                        string str = Console.ReadLine();
                        int index = str == "" ? 0 : Convert.ToInt32(str);

                        int result = model.DeleteEventName(index);
                        if (result == 0)
                        {
                            view.PrintMessage(" You can not remove this row. It is connected with other row");
                        }
                        else if (result == -1)
                        {
                            view.PrintMessage(" You can not remove this row. Invalid ID");
                        }
                        else if (result == 1)
                        {
                            view.PrintMessage(" You have removed this row from table.");
                        }
                    }
                    if (key2.Key == ConsoleKey.D4)
                    {
                        view.GetAllEventName(model.GetAllEventName());
                        view.PrintMessage(" Choose the ID of event name which you want to update.");
                        int i = Convert.ToInt32(Console.ReadLine());

                        view.PrintMessage("\nInput new data about owner:");
                        TEventName name = new TEventName();
                        view.PrintMessage(" Name: ");
                        name.Name = Console.ReadLine();

                        int result = model.UpdateEventName(i, name);
                        if (result == 0)
                        {
                            view.PrintMessage(" Error. You have entered unexisting ID.");
                        }
                        else if (result == 1)
                        {
                            view.PrintMessage(" You have updated this row.");
                        }
                    }
                    if (key2.Key == ConsoleKey.D5)
                    {
                        view.PrintMessage("  Enter event name:");
                        string name = Console.ReadLine();
                        view.GetAllEventName(model.SearchEventName(name));
                    }
                    if (key2.Key == ConsoleKey.D6)
                    {
                        view.PrintMessage(" How many rows do you want to generate?:");
                        int n = Convert.ToInt32(Console.ReadLine());
                        model.GenerateEventName(n);
                        view.PrintMessage("  New rows were generated.");
                    }
                    view.Pause();
                }
                else if (key1.Key == ConsoleKey.D5)
                {
                    view.TableMenu("Event Location");
                    ConsoleKeyInfo key2 = Console.ReadKey();
                    if (key2.Key == ConsoleKey.D1)
                    {
                        view.GetAllEventLocation(model.GetAllEventLocation());
                    }
                    else if (key2.Key == ConsoleKey.D2)
                    {
                        view.PrintMessage("\nChoose event:");
                        view.GetAllEvent(model.GetAllEvent());
                        string str = Console.ReadLine();
                        int eventId = str == "" ? 0 : Convert.ToInt32(str);

                        view.PrintMessage("\nChoose location:");
                        view.GetAllLocation(model.GetAllLocation());
                        str = Console.ReadLine();
                        int locationId = str == "" ? 0 : Convert.ToInt32(str);


                        int result = model.AddEventLocation(eventId, locationId);
                        if (result == 0)
                        {
                            view.PrintMessage(" Error. You have entered unexisting location ID.");
                        }
                        else if (result == -1)
                        {
                            view.PrintMessage(" Error. You have entered unexisting event ID.");
                        }
                        else
                        {
                            view.PrintMessage(" You have added a new row into table.");
                        }
                    }
                    if (key2.Key == ConsoleKey.D3)
                    {
                        view.GetAllEventLocation(model.GetAllEventLocation());
                        view.PrintMessage(" Choose the ID of the row which you want to remove.");
                        string str = Console.ReadLine();
                        int index = str == "" ? 0 : Convert.ToInt32(str);

                        int result = model.DeleteEventLocation(index);
                        if (result == -1)
                        {
                            view.PrintMessage(" Error. You have chosen unexisting ID");
                        }
                        else if (result == 1)
                        {
                            view.PrintMessage(" You have removed this row from table.");
                        }
                    }
                    if (key2.Key == ConsoleKey.D4)
                    {
                        view.GetAllEventName(model.GetAllEventName());
                        view.PrintMessage(" Choose the ID of event name which you want to update.");
                        int i = Convert.ToInt32(Console.ReadLine());

                        view.PrintMessage("\nChoose event:");
                        view.GetAllEvent(model.GetAllEvent());
                        string str = Console.ReadLine();
                        int eventId = str == "" ? 0 : Convert.ToInt32(str);

                        view.PrintMessage("\nChoose location:");
                        view.GetAllLocation(model.GetAllLocation());
                        str = Console.ReadLine();
                        int locationId = str == "" ? 0 : Convert.ToInt32(str);

                        int result = model.UpdateEventLocation(eventId, locationId, i);
                        if (result == 0)
                        {
                            view.PrintMessage(" Error. You have entered unexisting location ID.");
                        }
                        else if (result == -1)
                        {
                            view.PrintMessage(" Error. You have entered unexisting event ID.");
                        }
                        else if (result == -2)
                        {
                            view.PrintMessage(" Error. You have entered unexisting ID.");
                        }
                        else
                        {
                            view.PrintMessage(" You have updated this row.");
                        }
                    }
                    if (key2.Key == ConsoleKey.D5)
                    {
                        view.PrintMessage(" Choose the key of search:");
                        view.PrintMessage(" 1. Event name");
                        view.PrintMessage(" 2. Location name");
                        ConsoleKeyInfo key3 = Console.ReadKey();
                        if (key3.Key == ConsoleKey.D1)
                        {
                            view.PrintMessage("  Enter Event name:");
                            string name = Console.ReadLine();
                            view.GetAllEventLocation(model.SearchEventLocationByEvent(name));
                        }
                        else if (key3.Key == ConsoleKey.D2)
                        {
                            view.PrintMessage("  Enter Location name:");
                            string name = Console.ReadLine();
                            view.GetAllEventLocation(model.SearchEventLocationByLocation(name));
                        }

                    }
                    if (key2.Key == ConsoleKey.D6)
                    {
                        view.PrintMessage(" How many rows do you want to generate?:");
                        string str = Console.ReadLine();
                        int n = str == "" ? 0 : Convert.ToInt32(str);

                        model.GenerateEventLocation(n);
                        view.PrintMessage("  New rows were generated.");
                    }
                    view.Pause();
                }
            }
        }

    }
}

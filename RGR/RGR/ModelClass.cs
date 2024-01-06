using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RGR.ModelClasses;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System.Net;

namespace RGR
{
    public class ModelClass
    {
        private ContextClass context;
        public ModelClass() {
            context = new ContextClass();
        } 
        public List<TEvent> GetAllEvent()
        {
            var evs = context.Event.ToList();

            return evs;
        } // Отримання всіх івентів
        static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            Random random = new Random();

            char[] randomArray = new char[length];
            for (int i = 0; i < length; i++)
            {
                randomArray[i] = chars[random.Next(chars.Length)];
            }

            string randomString = new string(randomArray);
            return randomString;
        }
        public int AddEvent(TEvent event_)
        {
            var names = context.EventName.ToList();
            List<string> nm = new List<string>();
            List<int> ids = new List<int>();
            int eventNameId = -1;
            foreach (TEventName t in names) 
            {
                if (t.ID == event_.EventNameId)
                    eventNameId = t.ID;
            }
            if (eventNameId != -1) 
            {
                var evs = context.Event.ToList();
                foreach (TEvent ev in evs) 
                {
                    ids.Add(ev.ID);
                }
                event_.ID = ids.Max() + 1;

                context.Event.Add(event_);
                context.SaveChanges();
                return 1;
            }
            else  return 0;
        } // Додавання нового івенту 
        public int DeleteEvent(int index) 
        { 
            try
            {
                List<int> ids = new List<int>();
                var evs = context.Event.ToList();
                foreach (TEvent ev in evs)
                {
                    ids.Add(ev.ID);
                }
                if (!ids.Contains(index))
                {
                    return -1;
                }

                TEvent? even = context.Event.Find(index);
                context.Event.Remove(even);
                context.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        } // Видалення івенту 
        public int UpdateEvent(int id, TEvent event_)
        {
            List<int> ids_ = new List<int>();
            var evs = context.Event.ToList();
            foreach (TEvent ev in evs)
            {
                ids_.Add(ev.ID);
            }
            if (!ids_.Contains(id))
            {
                return 0;
            }
            var names = context.EventName.ToList();
            List<string> nm = new List<string>();
            List<int> ids = new List<int>();
            int eventNameId = -1;
            foreach (TEventName t in names)
            {
                if (t.ID == event_.EventNameId)
                    eventNameId = t.ID;
            }
            if (eventNameId != -1)
            {
                TEvent? updatedEvent = context.Event.Find(id);
                updatedEvent.Theme = event_.Theme;
                updatedEvent.EventNameId = event_.EventNameId;
                updatedEvent.EventDate = event_.EventDate;

                context.SaveChangesAsync();
                context = new ContextClass();
                return 1;
            }
            return -1;
        } // Оновлення івенту 
        public List<TEvent> SearchEventByName(int id)
        {
            var evs = context.Event.Where(e => e.EventNameId == id)
               .ToList();
            return evs;
        } // Пошук івенту за іменем 
        public List<TEvent> SearchEventByTheme(string tm)
        {
            var evs = context.Event.Where(e => e.Theme == tm)
               .ToList();
            return evs;
        }    // Пошук івенту за темою
        public List<TEvent> SearchEventByDate(DateOnly dt)
        {
            var evs = context.Event.Where(e => e.EventDate == dt)
               .ToList();
            return evs;
        } // Пошук івенту за датою 
        public void GenerateEvents(int n)
        {
            for (int i = 0; i < n; i++)
            {
                var names = context.EventName.ToList();
                List<int> ids = new List<int>();
                List<int> hosps = new List<int>();
                foreach (TEventName d in names)
                {
                    ids.Add(d.ID);
                }
                Random rnd = new Random();
                int nameid = ids[rnd.Next(0, ids.Count)];

                List<int> ids_ = new List<int>();
                var evs = context.Event.ToList();
                foreach (TEvent ev in evs)
                {
                    ids_.Add(ev.ID);
                }
                int id = ids_.Max() + 1;
                TEvent event_ = new TEvent();
                event_.ID = id;
                event_.EventNameId = nameid;
                event_.EventDate = DateOnly.FromDateTime(Convert.ToDateTime("01.01.2024"));
                event_.Theme = GenerateRandomString(15);

                context.Event.Add(event_);
                context.SaveChanges();
            }
        } //Генерація нових івентів
        //////////////////////////////////////////////////////////////////////////////////////////
        public List<TOwner> GetAllOwner()
        {
            var ow = context.Owner.ToList();
            return ow;
        } // Отримання всіх власників 
        public int AddOwner(TOwner owner)
        {
            List<int> ids = new List<int>();
            var ow = context.Owner.ToList();
            foreach (TOwner o in ow) 
            {
                ids.Add(o.ID);
            }
            owner.ID = ids.Max() + 1;

            context.Owner.Add(owner);
            context.SaveChanges();
            return 1;
        } // Додавання нового власника
        public int DeleteOwner(int index)
        {
            try
            {
                List<int> ids = new List<int>();
                var ows = context.Owner.ToList();
                foreach (TOwner o in ows)
                {
                    ids.Add(o.ID);
                }
                if (!ids.Contains(index))
                {
                    return -1;
                }
                TOwner? owner = context.Owner.Find(index);
                context.Owner.Remove(owner);

                context.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        } // Видалення власника
        public int UpdateOwner(int id, TOwner owner)
        {
            List<int> ids_ = new List<int>();
            var ows = context.Owner.ToList();
            foreach (TOwner o in ows)
            {
                ids_.Add(o.ID);
            }
            if (!ids_.Contains(id))
            {
                return 0;
            }
            TOwner own = context.Owner.Find(id);
            own.Name = owner.Name;
            own.PhoneNumber = owner.PhoneNumber;    
            
            context.SaveChangesAsync();
            context = new ContextClass();
            return 1;
        } // Оновлення власника
        public List<TOwner> SearchOwnerByName(string nm)
        {
            var ows = context.Owner.Where(e => e.Name.Contains(nm))
               .ToList();
            return ows;
        } // Пошук власника за іменем
        public List<TOwner> SearchOwnerByPhone(string ph)
        {
            var ows = context.Owner.Where(e => e.PhoneNumber.Contains(ph))
               .ToList();
            return ows;
        } // Пошук власника за телефоном
        public void GenerateOwner(int n)
        {
            for (int i = 0; i < n; i++)
            {
                List<int> ids = new List<int>();
                var ow = context.Owner.ToList();
                foreach (TOwner o in ow)
                {
                    ids.Add(o.ID);
                }
                int Id = ids.Max() + 1;

                TOwner owner = new TOwner();
                owner.ID = Id;
                owner.Name = GenerateRandomString(5) + GenerateRandomString(5);
                owner.PhoneNumber = GenerateRandomString(10);

                context.Add(owner);
                context.SaveChanges();
            }
        } // Генерація нових власників 
        //////////////////////////////////////////////////////////////////////////////////////////
        public List<TLocation> GetAllLocation()
        {
            var locations = context.Location.ToList();

            return locations;
        }// Отримання всіх локацій
        public int AddLocation(TLocation location)
        {
            List<int> ids = new List<int>();
            var locations = context.Location.ToList();
            foreach (TLocation l in locations) 
            {
                ids.Add(l.ID);
            }
            location.ID = ids.Max() + 1;
            int ownerId = -1;
            var ows = context.Owner.ToList();
            foreach (TOwner o in ows)
            {
                if (o.ID == location.OwnerId) 
                {
                    ownerId = o.ID;
                    break;
                } 
            }
            if (ownerId == -1)
                return 0;

            context.Location.Add(location);
            context.SaveChanges();
            return 1;
        }//  Додавання нової локації 
        public int DeleteLocation(int index)
        {
            try
            {
                List<int> ids = new List<int>();
                var locations = context.Location.ToList();
                foreach (TLocation l in locations)
                {
                    ids.Add(l.ID);
                }
                if (!ids.Contains(index))
                {
                    return -1;
                }
                TLocation? loc = context.Location.Find(index);
                context.Location.Remove(loc);
                context.SaveChanges();

                return 1;
            }
            catch
            {
                return 0;
            }
        }// Видалення локації
        public int UpdateLocation(int id, TLocation location)
        {
            List<int> ids_ = new List<int>();
            var locations = context.Location.ToList();
            foreach (TLocation l in locations)
            {
                ids_.Add(l.ID);
            }
            if (!ids_.Contains(id))
            {
                return 0;
            }
            int ownerId = -1;
            var ows = context.Owner.ToList();
            foreach (TOwner o in ows)
            {
                if (o.ID == location.OwnerId)
                {
                    ownerId = o.ID;
                    break;
                }
            }
            if (ownerId == -1)
                return 0;
            TLocation loc = context.Location.Find(id);
            loc.Address = location.Address;
            loc.OwnerId = location.OwnerId;
            loc.NumberOfSeats = location.NumberOfSeats;


            context.SaveChangesAsync();
            context = new ContextClass();
            return 1;
        }// Оновлення локації
        public List<TLocation> SearchLocationByName(string nm)
        {
            var locations = context.Location.Where(e => e.Name.Contains(nm))
               .ToList();

            return locations;
        }// Пошук локації за назвою
        public List<TLocation> SearchLocationByAddress(string address)
        {
            var locations = context.Location.Where(e => e.Address.Contains(address))
               .ToList();

            return locations;
        }// Пошук локації за адресою
        public List<TLocation> SearchLocationByNumberOfSeats(int minNos, int maxNos)
        {
            var locations = context.Location
                .Where(e => e.NumberOfSeats >= minNos && e.NumberOfSeats <= maxNos)
                .ToList();

            return locations;
        }// Пошук локації за кількістю місць
        public List<TLocation> SearchLocationByOwner(string owner)
        {
            int ownerId = -1;
            var ows = context.Owner.ToList();
            foreach (TOwner o in ows)
            {
                if (o.Name == owner)
                {
                    ownerId = o.ID;
                    break;
                }
            }
            var locations = context.Location
                .Where(e => e.OwnerId == ownerId)
                .ToList();
            return locations.ToList();
        }// Пошук локації за власником
        public void GenerateLocation(int n)
        {
            for (int i = 0; i < n; i++)
            {
                List<int> ids = new List<int>();
                var ow = context.Owner.ToList();
                foreach (TOwner o in ow)
                {
                    ids.Add(o.ID);
                }
                Random rnd = new Random();
                int ownerId = ids[rnd.Next(0, ids.Count)];

                List<int> ids_ = new List<int>();
                var locations = context.Location.ToList();
                foreach (TLocation l in locations)
                {
                    ids_.Add(l.ID);
                }
                int Id = ids_.Max() + 1;

                TLocation location = new TLocation();
                location.ID = Id;
                location.Name = GenerateRandomString(5) + GenerateRandomString(5);
                location.Address = GenerateRandomString(15);
                location.OwnerId = ownerId;
                location.NumberOfSeats = rnd.Next(30, 1000);

                context.Location.Add(location);
                context.SaveChanges();
            }
        } // Генерація нових локацій
        //////////////////////////////////////////////////////////////////////////////////////////
        public List<TEventName> GetAllEventName()
        {
            var ow = context.EventName.ToList();

            return ow.ToList();
        } // Отримання назв івентів 
        public int AddEventName(TEventName eventName)
        {
            List<int> ids = new List<int>();
            var eventNames = context.EventName.ToList();
            foreach (TEventName en in eventNames)
            {
                ids.Add(en.ID);
            }
            eventName.ID = ids.Max() + 1;

            context.EventName.Add(eventName);
            context.SaveChanges();
            
            return 1;
        } //  Додавання нової назви івенту 
        public int DeleteEventName(int index)
        {
            try
            {
                List<int> ids = new List<int>();
                var eventNames = context.EventName.ToList();
                foreach (TEventName en in eventNames)
                {
                    ids.Add(en.ID);
                }
                if (!ids.Contains(index))
                {
                    return -1;
                }
                TEventName? eventName = context.EventName.Find(index);
                context.EventName.Remove(eventName);
                context.SaveChanges();

                return 1;
            }
            catch
            {
                return 0;
            }
        } // Видалення назви івенту
        public int UpdateEventName(int id, TEventName eventName)
        {
            List<int> ids_ = new List<int>();
            var eventNames = context.EventName.ToList();
            foreach (TEventName en in eventNames)
            {
                ids_.Add(en.ID);
            }
            if (!ids_.Contains(id))
            {
                return 0;
            }
            TEventName? updatedEventName = context.EventName.Find(id);
            updatedEventName.Name = eventName.Name;
            context.SaveChanges();

            context = new ContextClass();
            return 1;
        } // Оновлення назви івенту
        public List<TEventName> SearchEventName(string nm)
        {
            var eventNames = context.EventName.Where(e => e.Name.Contains(nm))
               .ToList();
            return eventNames.ToList();
        } // Пошук назви івенту
        public void GenerateEventName(int n)
        {
            for (int i = 0; i < n; i++)
            {
                List<int> ids = new List<int>();
                var eventName = context.EventName.ToList();
                foreach (TEventName en in eventName)
                {
                    ids.Add(en.ID);
                }
                int Id = ids.Max() + 1;

                TEventName eventName1 = new TEventName();
                eventName1.Name = GenerateRandomString(5) + GenerateRandomString(5);
                eventName1.ID = Id;
                context.EventName.Add(eventName1); 

                context.SaveChanges();
            }
        } // Генерація нових назв івентів 
        //////////////////////////////////////////////////////////////////////////////////////////
        public List<TEventLocation> GetAllEventLocation()
        {
            var pd = context.EventLocation.ToList();

            return pd.ToList();
        } // Отримання всіх пар івентів та локацій
        public int AddEventLocation(int eventId, int locationId)
        {
            int eventId_ = -1;
            int locationId_ = -1;
            List<int> idsEvent = new List<int>();
            List<int> idsLocation = new List<int>();

            var evs = context.Event.ToList();
            foreach (TEvent ev in evs)
            {
                if (ev.ID == eventId)
                    eventId_ = eventId;
            }
            var locations = context.Location.ToList();

            foreach (TLocation loc in locations)
            {
                if (loc.ID == locationId)
                    locationId_ = locationId;
            }

            if (eventId_ == -1)
                return -1;
            if (locationId_ == -1)
                return 0;

            List<int> ids = new List<int>();
            var eventLocations = context.EventLocation.ToList();

            foreach (TEventLocation el in eventLocations)
            {
                ids.Add(el.ID);
            }
            int Id = ids.Max(id => id) + 1;

            TEventLocation eventLocation = new TEventLocation();
            eventLocation.ID = Id;
            eventLocation.LocationId = locationId_;
            eventLocation.EventId = eventId_;

            context.EventLocation.Add(eventLocation);
            context.SaveChanges();
            return 1;
        }// Додавання нової пари
        public int DeleteEventLocation(int index)
        {
            List<int> ids = new List<int>();
            var eventLocations = context.EventLocation.ToList();
            foreach (TEventLocation el in eventLocations)
            {
                ids.Add(el.ID);
            }
            if (!ids.Contains(index))
            {
                return -1;
            }
            TEventLocation? eventLocation = context.EventLocation.Find(index);
            context.EventLocation.Remove(eventLocation);
            context.SaveChanges();
            return 1;
        }// Видалення пари
        public int UpdateEventLocation(int eventId, int locationId, int id)
        {
            int eventId_ = -1;
            int locationId_ = -1;
            List<int> idsEvent = new List<int>();
            List<int> idsLocation = new List<int>();
            var evs = context.Event.ToList();
            foreach (TEvent ev in evs)
            {
                if (ev.ID == eventId)
                    eventId_ = eventId;
            }
            var locations = context.Location.ToList();
            foreach (TLocation loc in locations)
            {
                if (loc.ID == locationId)
                    locationId_ = locationId;
            }
            if (eventId_ == -1)
                return -1;
            if (locationId_ == -1)
                return 0;
            List<int> ids = new List<int>();
            var eventLocations = context.EventLocation.ToList();
            foreach (TEventLocation el in eventLocations)
            {
                ids.Add(el.ID);
            }
            if (!ids.Contains(id))
                return -2;
            TEventLocation? eventLocation = context.EventLocation.Find(id);
            eventLocation.LocationId = locationId;
            eventLocation.EventId = eventId;

            context.SaveChangesAsync();
            context = new ContextClass();
            return 1;
        }// Оновлення пари
        public List<TEventLocation> SearchEventLocationByEvent(int id)
        {
            var pd = context.EventLocation.Where(o => o.EventId == id).ToList();

            return pd.ToList();
        }// Пошук пари за івентом
        public List<TEventLocation> SearchEventLocationByLocation(int id)
        {
            var pd = context.EventLocation.Where(e => e.LocationId == id)
                .ToList();
            return pd.ToList();
        }// пошук пари за локацією 
        public void GenerateEventLocation(int n)
        {
            for (int i = 0; i < n; i++)
            { 
                List<int> idsEvent = new List<int>();
                List<int> idsLocation = new List<int>();
                var evs = context.Event.ToList();
                foreach (TEvent ev in evs)
                {
                    idsEvent.Add(ev.ID);
                }
                var locations = context.Location.ToList();
                foreach (TLocation loc in locations)
                {
                    idsLocation.Add(loc.ID);
                }
                List<int> ids = new List<int>();
                var eventLocations = context.EventLocation.ToList();
                foreach (TEventLocation el in eventLocations)
                {
                    ids.Add(el.ID);
                }
                Random rnd = new Random();
                int Id = ids.Max(id => id) + 1;
                int eventId_ = idsEvent[rnd.Next(0, idsEvent.Count)];
                int locationId_ = idsLocation[rnd.Next(0, idsLocation.Count)];

                TEventLocation eventLocation = new TEventLocation();
                eventLocation.ID = Id;
                eventLocation.EventId = eventId_;
                eventLocation.LocationId = locationId_;

                context.EventLocation.Add(eventLocation);
                context.SaveChanges();
            }
        }// Генерація нових пар івентів та локацій
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RGR.ModelClasses;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

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
            var evs = context.Event.FromSqlRaw(
              @"SELECT ""Event"".""ID"" AS Id, 
              ""EventName"".""Name"" AS EventName, 
              ""Event"".""Theme"" AS Theme, 
              ""Event"".""EventDate"" AS EventDate 
              FROM ""Event"", ""EventName""
              WHERE ""Event"".""EventNameId"" = ""EventName"".""ID""
              ORDER BY ""Event"".""ID"" ASC");

            return evs.ToList();
        } // Отримання всіх івентів
        public int AddEvent(TEvent event_)
        {
            var names = context.EventName.FromSqlRaw(
                @"SELECT * FROM ""EventName""");
            List<string> nm = new List<string>();
            List<int> ids = new List<int>();
            int eventNameId = -1;
            foreach (TEventName t in names) 
            {
                if (t.Name == event_.EventName)
                    eventNameId = t.Id;
            }
            if (eventNameId != -1) 
            {
                var evs = context.Event.FromSqlRaw(
                  @"SELECT ""Event"".""ID"" AS Id, 
                  ""EventName"".""Name"" AS EventName, 
                  ""Event"".""Theme"" AS Theme, 
                  ""Event"".""EventDate"" AS EventDate 
                  FROM ""Event"", ""EventName""
                  WHERE ""Event"".""EventNameId"" = ""EventName"".""ID""");
                foreach (TEvent ev in evs) 
                {
                    ids.Add(ev.Id);
                }
                event_.Id = ids.Max() + 1;

                var sqlQuery = $"INSERT INTO \"Event\" VALUES ('{event_.Id}', '{eventNameId}', '{event_.Theme}','{event_.EventDate}')";
                int insert = context.Database.ExecuteSqlRaw(sqlQuery);
                return 1;
            }
            else  return 0;
        } // Додавання нового івенту 
        public int DeleteEvent(int index) 
        { 
            try
            {
                List<int> ids = new List<int>();
                var evs = context.Event.FromSqlRaw(
                  @"SELECT ""Event"".""ID"" AS Id, 
                  ""EventName"".""Name"" AS EventName, 
                  ""Event"".""Theme"" AS Theme, 
                  ""Event"".""EventDate"" AS EventDate 
                  FROM ""Event"", ""EventName""
                  WHERE ""Event"".""EventNameId"" = ""EventName"".""ID""");
                foreach (TEvent ev in evs)
                {
                    ids.Add(ev.Id);
                }
                if (!ids.Contains(index))
                {
                    return -1;
                }
                var sqlQuery = $"DELETE FROM \"Event\" WHERE \"ID\" = {index};";
                int delete = context.Database.ExecuteSqlRaw(sqlQuery);
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
            var evs = context.Event.FromSqlRaw(
              @"SELECT ""Event"".""ID"" AS Id, 
                  ""EventName"".""Name"" AS EventName, 
                  ""Event"".""Theme"" AS Theme, 
                  ""Event"".""EventDate"" AS EventDate 
                  FROM ""Event"", ""EventName""
                  WHERE ""Event"".""EventNameId"" = ""EventName"".""ID""");
            foreach (TEvent ev in evs)
            {
                ids_.Add(ev.Id);
            }
            if (!ids_.Contains(id))
            {
                return 0;
            }
            var names = context.EventName.FromSqlRaw(
                                @"SELECT * FROM ""EventName""");
            List<string> nm = new List<string>();
            List<int> ids = new List<int>();
            int eventNameId = -1;
            foreach (TEventName t in names)
            {
                if (t.Name == event_.EventName)
                    eventNameId = t.Id;
            }
            if (eventNameId != -1)
            {
                var sqlQuery = $"UPDATE \"Event\" SET \"EventNameId\"='{eventNameId}', \"Theme\"='{event_.Theme}', \"EventDate\"='{event_.EventDate}' WHERE \"ID\" = {id}";
                int insert = context.Database.ExecuteSqlRaw(sqlQuery);

                context.SaveChangesAsync();
                context = new ContextClass();
                return 1;
            }
            return -1;
        } // Оновлення івенту
        public List<TEvent> SearchEventByName(string nm)
        {
            var evs = context.Event.FromSqlRaw(
              @"SELECT ""Event"".""ID"" AS Id, 
              ""EventName"".""Name"" AS EventName, 
              ""Event"".""Theme"" AS Theme, 
              ""Event"".""EventDate"" AS EventDate 
              FROM ""Event"", ""EventName""
              WHERE ""Event"".""EventNameId"" = ""EventName"".""ID""
              AND ""EventName"".""Name"" LIKE '%'||{0}||'%'
              ORDER BY ""Event"".""ID"" ASC", nm);
            return evs.ToList();
        } // Пошук івенту за іменем 
        public List<TEvent> SearchEventByTheme(string tm)
        {
            var evs = context.Event.FromSqlRaw(
              @"SELECT ""Event"".""ID"" AS Id, 
              ""EventName"".""Name"" AS EventName, 
              ""Event"".""Theme"" AS Theme, 
              ""Event"".""EventDate"" AS EventDate 
              FROM ""Event"", ""EventName""
              WHERE ""Event"".""EventNameId"" = ""EventName"".""ID""
              AND ""Event"".""Theme"" LIKE '%'||{0}||'%'
              ORDER BY ""Event"".""ID"" ASC", tm);
            return evs.ToList();
        }    // Пошук івенту за темою
        public List<TEvent> SearchEventByDate(DateOnly dt)
        {
            var evs = context.Event.FromSqlRaw(
              @"SELECT ""Event"".""ID"" AS Id, 
              ""EventName"".""Name"" AS EventName, 
              ""Event"".""Theme"" AS Theme, 
              ""Event"".""EventDate"" AS EventDate 
              FROM ""Event"", ""EventName""
              WHERE ""Event"".""EventNameId"" = ""EventName"".""ID""
              AND ""Event"".""EventDate"" = {0}
              ORDER BY ""Event"".""ID"" ASC", dt);

            return evs.ToList();
        } // Пошук івенту за датою 
        public void GenerateEvents(int n)
        {
            for (int i = 0; i < n; i++)
            {
                var names = context.EventName.FromSqlRaw(
                                @"SELECT * FROM ""EventName""");
                List<int> ids = new List<int>();
                List<int> hosps = new List<int>();
                foreach (TEventName d in names)
                {
                    ids.Add(d.Id);
                }
                Random rnd = new Random();
                int nameid = ids[rnd.Next(0, ids.Count)];

                List<int> ids_ = new List<int>();
                var evs = context.Event.FromSqlRaw(
                  @"SELECT ""Event"".""ID"" AS Id, 
                  ""EventName"".""Name"" AS EventName, 
                  ""Event"".""Theme"" AS Theme, 
                  ""Event"".""EventDate"" AS EventDate 
                  FROM ""Event"", ""EventName""
                  WHERE ""Event"".""EventNameId"" = ""EventName"".""ID""");
                foreach (TEvent ev in evs)
                {
                    ids_.Add(ev.Id);
                }
                int id = ids_.Max() + 1;
                var sqlQuery = $"INSERT INTO \"Event\" VALUES ('{id}', '{nameid}', " +
                    $"concat(chr(trunc(65+random()*25)::int),  " +
                    $"chr(trunc(65+random()*25)::int), chr(trunc(65+random()*25)::int), " +
                    $"chr(trunc(65+random()*25)::int)), " +
                    $"(current_date - random() * interval '365 days')::date)";
                int insert = context.Database.ExecuteSqlRaw(sqlQuery);
            }
        } //Генерація нових івентів
        //////////////////////////////////////////////////////////////////////////////////////////
        public List<TOwner> GetAllOwner()
        {
            var ow = context.Owner.FromSqlRaw(
              @"SELECT 
              ""Owner"".""ID"" as Id,
              ""Owner"".""Name"" as Name,
              ""Owner"".""PhoneNumber"" as Phone
              FROM ""Owner""
              ORDER BY ""Owner"".""ID"" ASC");

            return ow.ToList();
        } // Отримання всіх власників 
        public int AddOwner(TOwner owner)
        {
            List<int> ids = new List<int>();
            var ow = context.Owner.FromSqlRaw(
              @"SELECT ""Owner"".""ID"" as Id,
              ""Owner"".""Name"" as Name,
              ""Owner"".""PhoneNumber"" as Phone
              FROM ""Owner""
              ORDER BY ""Owner"".""ID"" ASC");
            foreach (TOwner o in ow) 
            {
                ids.Add(o.Id);
            }
            owner.Id = ids.Max() + 1;

            var sqlQuery = $"INSERT INTO \"Owner\" VALUES ('{owner.Id}', '{owner.Name}', '{owner.Phone}')";
            int insert = context.Database.ExecuteSqlRaw(sqlQuery);
            return 1;
        } // Додавання нового власника
        public int DeleteOwner(int index)
        {
            try
            {
                List<int> ids = new List<int>();
                var ows = context.Owner.FromSqlRaw(
                  @"SELECT ""Owner"".""ID"" as Id,
                  ""Owner"".""Name"" as Name,
                  ""Owner"".""PhoneNumber"" as Phone
                  FROM ""Owner""
                  ORDER BY ""Owner"".""ID"" ASC");
                foreach (TOwner o in ows)
                {
                    ids.Add(o.Id);
                }
                if (!ids.Contains(index))
                {
                    return -1;
                }
                var sqlQuery = $"DELETE FROM \"Owner\" WHERE \"ID\" = {index};";
                int delete = context.Database.ExecuteSqlRaw(sqlQuery);
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
            var ows = context.Owner.FromSqlRaw(
                  @"SELECT ""Owner"".""ID"" as Id,
                  ""Owner"".""Name"" as Name,
                  ""Owner"".""PhoneNumber"" as Phone
                  FROM ""Owner""
                  ORDER BY ""Owner"".""ID"" ASC");
            foreach (TOwner o in ows)
            {
                ids_.Add(o.Id);
            }
            if (!ids_.Contains(id))
            {
                return 0;
            }
            var sqlQuery = $"UPDATE \"Owner\" SET \"Name\"='{owner.Name}', \"PhoneNumber\"='{owner.Phone}' WHERE \"ID\" = {id}";
            int insert = context.Database.ExecuteSqlRaw(sqlQuery);
            context.SaveChangesAsync();
            context = new ContextClass();
            return 1;
        } // Оновлення власника
        public List<TOwner> SearchOwnerByName(string nm)
        {
            var ows = context.Owner.FromSqlRaw(
                  @"SELECT ""Owner"".""ID"" as Id,
                  ""Owner"".""Name"" as Name,
                  ""Owner"".""PhoneNumber"" as Phone
                  FROM ""Owner"" WHERE
                  ""Owner"".""Name"" LIKE '%'||{0}||'%'
                  ORDER BY ""Owner"".""ID"" ASC", nm);
            return ows.ToList();
        } // Пошук власника за іменем
        public List<TOwner> SearchOwnerByPhone(string ph)
        {
            var ows = context.Owner.FromSqlRaw(
                  @"SELECT ""Owner"".""ID"" as Id,
                  ""Owner"".""Name"" as Name,
                  ""Owner"".""PhoneNumber"" as Phone
                  FROM ""Owner"" WHERE
                  ""Owner"".""PhoneNumber"" LIKE '%'||{0}||'%'
                  ORDER BY ""Owner"".""ID"" ASC", ph);
            return ows.ToList();
        } // Пошук власника за телефоном
        public void GenerateOwner(int n)
        {
            for (int i = 0; i < n; i++)
            {
                List<int> ids = new List<int>();
                var ow = context.Owner.FromSqlRaw(
                  @"SELECT ""Owner"".""ID"" as Id,
                  ""Owner"".""Name"" as Name,
                  ""Owner"".""PhoneNumber"" as Phone
                  FROM ""Owner""
                  ORDER BY ""Owner"".""ID"" ASC");
                foreach (TOwner o in ow)
                {
                    ids.Add(o.Id);
                }
                int Id = ids.Max() + 1;

                var sqlQuery = $"INSERT INTO \"Owner\" VALUES ('{Id}', " +
                    $"  concat(chr(trunc(65+random()*25)::int), " +
                    $"chr(trunc(65+random()*25)::int), " +
                    $"chr(trunc(65+random()*25)::int), ' ', " +
                    $"chr(trunc(65+random()*25)::int), " +
                    $"chr(trunc(65+random()*25)::int), " +
                    $"chr(trunc(65+random()*25)::int)), " +
                    $" CONCAT(ROUND(random() * 999), '-', TO_CHAR(ROUND(random() * 9999), 'FM0000')) )";
                int insert = context.Database.ExecuteSqlRaw(sqlQuery);
            }
        } // Генерація нових власників 
        //////////////////////////////////////////////////////////////////////////////////////////
        public List<TLocation> GetAllLocation()
        {
            var locations = context.Location.FromSqlRaw(
              @"SELECT 
              ""Location"".""ID"" as Id,
              ""Location"".""Name"" as Name,
              ""Location"".""Address"" as Address,
              ""Location"".""NumberOfSeats"" as NumberOfSeats,
              ""Owner"".""Name"" as Owner
              FROM ""Location"", ""Owner""
              WHERE ""Location"".""OwnerId"" =  ""Owner"".""ID""
              ORDER BY ""Location"".""ID"" ASC");

            return locations.ToList();
        }// Отримання всіх локацій
        public int AddLocation(TLocation location)
        {
            List<int> ids = new List<int>();
            var locations = context.Location.FromSqlRaw(
              @"SELECT 
              ""Location"".""ID"" as Id,
              ""Location"".""Name"" as Name,
              ""Location"".""Address"" as Address,
              ""Location"".""NumberOfSeats"" as NumberOfSeats,
              ""Owner"".""Name"" as Owner
              FROM ""Location"", ""Owner""
              WHERE ""Location"".""OwnerId"" =  ""Owner"".""ID""
              ORDER BY ""Location"".""ID"" ASC");
            foreach (TLocation l in locations) 
            {
                ids.Add(l.Id);
            }
            location.Id = ids.Max() + 1;
            int ownerId = -1;
            var ows = context.Owner.FromSqlRaw(
                 @"SELECT ""Owner"".""ID"" as Id,
                  ""Owner"".""Name"" as Name,
                  ""Owner"".""PhoneNumber"" as Phone
                  FROM ""Owner""
                  ORDER BY ""Owner"".""ID"" ASC");
            foreach (TOwner o in ows)
            {
                if (o.Name == location.Owner) 
                {
                    ownerId = o.Id;
                    break;
                } 
            }
            if (ownerId == -1)
                return 0;
            var sqlQuery = $"INSERT INTO \"Location\" VALUES (" +
                $"'{location.Id}', '{location.Name}', '{location.Address}', " +
                $" {location.NumberOfSeats}, {ownerId})";
            int insert = context.Database.ExecuteSqlRaw(sqlQuery);
            return 1;
        }//  Додавання нової локації 
        public int DeleteLocation(int index)
        {
            try
            {
                List<int> ids = new List<int>();
                var locations = context.Location.FromSqlRaw(
              @"SELECT 
              ""Location"".""ID"" as Id,
              ""Location"".""Name"" as Name,
              ""Location"".""Address"" as Address,
              ""Location"".""NumberOfSeats"" as NumberOfSeats,
              ""Owner"".""Name"" as Owner
              FROM ""Location"", ""Owner""
              WHERE ""Location"".""OwnerId"" =  ""Owner"".""ID""
              ORDER BY ""Location"".""ID"" ASC");
                foreach (TLocation l in locations)
                {
                    ids.Add(l.Id);
                }
                if (!ids.Contains(index))
                {
                    return -1;
                }
                var sqlQuery = $"DELETE FROM \"Location\" WHERE \"ID\" = {index};";
                int delete = context.Database.ExecuteSqlRaw(sqlQuery);
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
            var locations = context.Location.FromSqlRaw(
              @"SELECT 
              ""Location"".""ID"" as Id,
              ""Location"".""Name"" as Name,
              ""Location"".""Address"" as Address,
              ""Location"".""NumberOfSeats"" as NumberOfSeats,
              ""Owner"".""Name"" as Owner
              FROM ""Location"", ""Owner""
              WHERE ""Location"".""OwnerId"" =  ""Owner"".""ID""
              ORDER BY ""Location"".""ID"" ASC");
            foreach (TLocation l in locations)
            {
                ids_.Add(l.Id);
            }
            if (!ids_.Contains(id))
            {
                return 0;
            }
            int ownerId = -1;
            var ows = context.Owner.FromSqlRaw(
                 @"SELECT ""Owner"".""ID"" as Id,
                  ""Owner"".""Name"" as Name,
                  ""Owner"".""PhoneNumber"" as Phone
                  FROM ""Owner""
                  ORDER BY ""Owner"".""ID"" ASC");
            foreach (TOwner o in ows)
            {
                if (o.Name == location.Owner)
                {
                    ownerId = o.Id;
                    break;
                }
            }
            if (ownerId == -1)
                return 0;
            var sqlQuery = $"UPDATE \"Location\" SET \"Name\"='{location.Name}', \"Address\"='{location.Address}', " +
                $"\"NumberOfSeats\"={location.NumberOfSeats}, \"OwnerId\"={ownerId} WHERE \"ID\" = {id}";
            int insert = context.Database.ExecuteSqlRaw(sqlQuery);
            context.SaveChangesAsync();
            context = new ContextClass();
            return 1;
        }// Оновлення локації
        public List<TLocation> SearchLocationByName(string nm)
        {
            var locations = context.Location.FromSqlRaw(
              @"SELECT 
              ""Location"".""ID"" as Id,
              ""Location"".""Name"" as Name,
              ""Location"".""Address"" as Address,
              ""Location"".""NumberOfSeats"" as NumberOfSeats,
              ""Owner"".""Name"" as Owner
              FROM ""Location"", ""Owner""
              WHERE ""Location"".""OwnerId"" =  ""Owner"".""ID""
              AND ""Location"".""Name"" LIKE '%'||{0}||'%'
              ORDER BY ""Location"".""ID"" ASC", nm);

            return locations.ToList();
        }// Пошук локації за назвою
        public List<TLocation> SearchLocationByAddress(string address)
        {
            var locations = context.Location.FromSqlRaw(
              @"SELECT 
              ""Location"".""ID"" as Id,
              ""Location"".""Name"" as Name,
              ""Location"".""Address"" as Address,
              ""Location"".""NumberOfSeats"" as NumberOfSeats,
              ""Owner"".""Name"" as Owner
              FROM ""Location"", ""Owner""
              WHERE ""Location"".""OwnerId"" =  ""Owner"".""ID""
              AND ""Location"".""Address"" LIKE '%'||{0}||'%'
              ORDER BY ""Location"".""ID"" ASC", address);

            return locations.ToList();
        }// Пошук локації за адресою
        public List<TLocation> SearchLocationByNumberOfSeats(int minNos, int maxNos)
        {
            var locations = context.Location.FromSqlRaw(
              @"SELECT 
              ""Location"".""ID"" as Id,
              ""Location"".""Name"" as Name,
              ""Location"".""Address"" as Address,
              ""Location"".""NumberOfSeats"" as NumberOfSeats,
              ""Owner"".""Name"" as Owner
              FROM ""Location"", ""Owner""
              WHERE ""Location"".""OwnerId"" =  ""Owner"".""ID""
              AND ""Location"".""NumberOfSeats"" >= {0}
              AND ""Location"".""NumberOfSeats"" <= {1}
              ORDER BY ""Location"".""ID"" ASC", minNos, maxNos);
            return locations.ToList();
        }// Пошук локації за кількістю місць
        public List<TLocation> SearchLocationByOwner(string owner)
        {
            int ownerId = -1;
            var ows = context.Owner.FromSqlRaw(
                 @"SELECT ""Owner"".""ID"" as Id,
                  ""Owner"".""Name"" as Name,
                  ""Owner"".""PhoneNumber"" as Phone
                  FROM ""Owner""
                  ORDER BY ""Owner"".""ID"" ASC");
            foreach (TOwner o in ows)
            {
                if (o.Name == owner)
                {
                    ownerId = o.Id;
                    break;
                }
            }
            var locations = context.Location.FromSqlRaw(
              @"SELECT 
              ""Location"".""ID"" as Id,
              ""Location"".""Name"" as Name,
              ""Location"".""Address"" as Address,
              ""Location"".""NumberOfSeats"" as NumberOfSeats,
              ""Owner"".""Name"" as Owner
              FROM ""Location"", ""Owner""
              WHERE ""Location"".""OwnerId"" =  ""Owner"".""ID""
              AND ""Location"".""OwnerId"" = {0}
              ORDER BY ""Location"".""ID"" ASC", ownerId);
            return locations.ToList();
        }// Пошук локації за власником
        public void GenerateLocation(int n)
        {
            for (int i = 0; i < n; i++)
            {
                List<int> ids = new List<int>();
                var ow = context.Owner.FromSqlRaw(
                  @"SELECT ""Owner"".""ID"" as Id,
                  ""Owner"".""Name"" as Name,
                  ""Owner"".""PhoneNumber"" as Phone
                  FROM ""Owner""
                  ORDER BY ""Owner"".""ID"" ASC");
                foreach (TOwner o in ow)
                {
                    ids.Add(o.Id);
                }
                Random rnd = new Random();
                int ownerId = ids[rnd.Next(0, ids.Count)];

                List<int> ids_ = new List<int>();
                var locations = context.Location.FromSqlRaw(
                  @"SELECT 
                  ""Location"".""ID"" as Id,
                  ""Location"".""Name"" as Name,
                  ""Location"".""Address"" as Address,
                  ""Location"".""NumberOfSeats"" as NumberOfSeats,
                  ""Owner"".""Name"" as Owner
                  FROM ""Location"", ""Owner""
                  WHERE ""Location"".""OwnerId"" =  ""Owner"".""ID""
                  ORDER BY ""Location"".""ID"" ASC");
                foreach (TLocation l in locations)
                {
                    ids_.Add(l.Id);
                }
                int Id = ids_.Max() + 1;

                var sqlQuery = $"INSERT INTO \"Location\" VALUES ('{Id}', " +
                    $"  concat(chr(trunc(65+random()*25)::int), " +
                    $"chr(trunc(65+random()*25)::int), " +
                    $"chr(trunc(65+random()*25)::int), ' ', " +
                    $"chr(trunc(65+random()*25)::int), " +
                    $"chr(trunc(65+random()*25)::int), " +
                    $"chr(trunc(65+random()*25)::int)), " +
                    $"  concat(chr(trunc(65+random()*25)::int), " +
                    $"chr(trunc(65+random()*25)::int), " +
                    $"chr(trunc(65+random()*25)::int), ' ', " +
                    $"chr(trunc(65+random()*25)::int), " +
                    $"chr(trunc(65+random()*25)::int), " +
                    $"chr(trunc(65+random()*25)::int), ' ', " +
                    $"trunc(random()*1000)::int), " +
                    $"trunc(random()*1000)::int, {ownerId} )";

                int insert = context.Database.ExecuteSqlRaw(sqlQuery);
            }
        } // Генерація нових локацій
        //////////////////////////////////////////////////////////////////////////////////////////
        public List<TEventName> GetAllEventName()
        {
            var ow = context.EventName.FromSqlRaw(
              @"SELECT *
              FROM ""EventName""
              ORDER BY ""EventName"".""ID"" ASC");

            return ow.ToList();
        } // Отримання назв івентів 
        public int AddEventName(TEventName eventName)
        {
            List<int> ids = new List<int>();
            var eventNames = context.EventName.FromSqlRaw(
              @"SELECT *
              FROM ""EventName""
              ORDER BY ""EventName"".""ID"" ASC");
            foreach (TEventName en in eventNames)
            {
                ids.Add(en.Id);
            }
            eventName.Id = ids.Max() + 1;

            var sqlQuery = $"INSERT INTO \"EventName\" VALUES ('{eventName.Id}', '{eventName.Name}')";
            int insert = context.Database.ExecuteSqlRaw(sqlQuery);
            return 1;
        } //  Додавання нової назви івенту 
        public int DeleteEventName(int index)
        {
            try
            {
                List<int> ids = new List<int>();
                var eventNames = context.EventName.FromSqlRaw(
                  @"SELECT *
                  FROM ""EventName""
                  ORDER BY ""EventName"".""ID"" ASC");
                foreach (TEventName en in eventNames)
                {
                    ids.Add(en.Id);
                }
                if (!ids.Contains(index))
                {
                    return -1;
                }
                var sqlQuery = $"DELETE FROM \"EventName\" WHERE \"ID\" = {index};";
                int delete = context.Database.ExecuteSqlRaw(sqlQuery);
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
            var eventNames = context.EventName.FromSqlRaw(
                  @"SELECT *
                  FROM ""EventName""
                  ORDER BY ""EventName"".""ID"" ASC");
            foreach (TEventName en in eventNames)
            {
                ids_.Add(en.Id);
            }
            if (!ids_.Contains(id))
            {
                return 0;
            }
            var sqlQuery = $"UPDATE \"EventName\" SET \"Name\"='{eventName.Name}' WHERE \"ID\" = {id}";
            int insert = context.Database.ExecuteSqlRaw(sqlQuery);

            context.SaveChangesAsync();
            context = new ContextClass();
            return 1;
        } // Оновлення назви івенту
        public List<TEventName> SearchEventName(string nm)
        {
            var eventNames = context.EventName.FromSqlRaw(
                  @"SELECT *
                  FROM ""EventName"" WHERE
                  ""EventName"".""Name"" LIKE '%'||{0}||'%'
                  ORDER BY ""EventName"".""ID"" ASC", nm);
            return eventNames.ToList();
        } // Пошук назви івенту
        public void GenerateEventName(int n)
        {
            for (int i = 0; i < n; i++)
            {
                List<int> ids = new List<int>();
                var eventName = context.EventName.FromSqlRaw(
                  @"SELECT *
                  FROM ""EventName""
                  ORDER BY ""EventName"".""ID"" ASC");
                foreach (TEventName en in eventName)
                {
                    ids.Add(en.Id);
                }
                int Id = ids.Max() + 1;

                var sqlQuery = $"INSERT INTO \"EventName\" VALUES ('{Id}', " +
                    $"  concat(chr(trunc(65+random()*25)::int), " +
                    $"chr(trunc(65+random()*25)::int), " +
                    $"chr(trunc(65+random()*25)::int), ' ', " +
                    $"chr(trunc(65+random()*25)::int), " +
                    $"chr(trunc(65+random()*25)::int), " +
                    $"chr(trunc(65+random()*25)::int)) )";
                int insert = context.Database.ExecuteSqlRaw(sqlQuery);
            }
        } // Генерація нових назв івентів 
        //////////////////////////////////////////////////////////////////////////////////////////
        public List<TEventLocation> GetAllEventLocation()
        {
            var pd = context.EventLocation.FromSqlRaw(
              @"SELECT ""EventLocation"".""ID"" AS Id, 
              ""EventName"".""Name"" AS EventName, 
              ""Location"".""Name"" AS LocationName  
              FROM ""Event"", ""Location"", ""EventLocation"",""EventName""
              WHERE ""EventLocation"".""EventId"" = ""Event"".""ID""  
              AND ""EventLocation"".""LocationId"" = ""Location"".""ID""
              AND ""EventName"".""ID"" = ""Event"".""ID""");

            return pd.ToList();
        } // Отримання всіх пар івентів та локацій
        public int AddEventLocation(int eventId, int locationId)
        {
            int eventId_ = -1;
            int locationId_ = -1;
            List<int> idsEvent = new List<int>();
            List<int> idsLocation = new List<int>();

            var evs = context.Event.FromSqlRaw(
              @"SELECT ""Event"".""ID"" AS Id, 
              ""EventName"".""Name"" AS EventName, 
              ""Event"".""Theme"" AS Theme, 
              ""Event"".""EventDate"" AS EventDate 
              FROM ""Event"", ""EventName""
              WHERE ""Event"".""EventNameId"" = ""EventName"".""ID""
              ORDER BY ""Event"".""ID"" ASC");
            foreach (TEvent ev in evs)
            {
                if (ev.Id == eventId)
                    eventId_ = eventId;
            }
            var locations = context.Location.FromSqlRaw(
              @"SELECT 
              ""Location"".""ID"" as Id,
              ""Location"".""Name"" as Name,
              ""Location"".""Address"" as Address,
              ""Location"".""NumberOfSeats"" as NumberOfSeats,
              ""Owner"".""Name"" as Owner
              FROM ""Location"", ""Owner""
              WHERE ""Location"".""OwnerId"" =  ""Owner"".""ID""
              ORDER BY ""Location"".""ID"" ASC");

            foreach (TLocation loc in locations)
            {
                if (loc.Id == locationId)
                    locationId_ = locationId;
            }

            if (eventId_ == -1)
                return -1;
            if (locationId_ == -1)
                return 0;

            List<int> ids = new List<int>();
            var eventLocations = context.EventLocation.FromSqlRaw(
             @"SELECT ""EventLocation"".""ID"" AS Id, 
              ""EventName"".""Name"" AS EventName, 
              ""Location"".""Name"" AS LocationName  
              FROM ""Event"", ""Location"", ""EventLocation"",""EventName""
              WHERE ""EventLocation"".""EventId"" = ""Event"".""ID""  
              AND ""EventLocation"".""LocationId"" = ""Location"".""ID""
              AND ""EventName"".""ID"" = ""Event"".""ID""");

            foreach (TEventLocation el in eventLocations)
            {
                ids.Add(el.Id);
            }

            int Id = ids.Max(id => id) + 1;

            var sqlQuery = $"INSERT INTO \"EventLocation\" VALUES ('{Id}', '{eventId_}', '{locationId_}')";
            int insert = context.Database.ExecuteSqlRaw(sqlQuery);
            return 1;
        }// Додавання нової пари
        public int DeleteEventLocation(int index)
        {
            List<int> ids = new List<int>();
            var eventLocations = context.EventLocation.FromSqlRaw(
              @"SELECT ""EventLocation"".""ID"" AS Id, 
              ""EventName"".""Name"" AS EventName, 
              ""Location"".""Name"" AS LocationName  
              FROM ""Event"", ""Location"", ""EventLocation"",""EventName""
              WHERE ""EventLocation"".""EventId"" = ""Event"".""ID""  
              AND ""EventLocation"".""LocationId"" = ""Location"".""ID""
              AND ""EventName"".""ID"" = ""Event"".""ID""");
            foreach (TEventLocation el in eventLocations)
            {
                ids.Add(el.Id);
            }
            if (!ids.Contains(index))
            {
                return -1;
            }
            var sqlQuery = $"DELETE FROM \"EventLocation\" WHERE \"ID\" = {index};";
            int delete = context.Database.ExecuteSqlRaw(sqlQuery);
            return 1;
        }// Видалення пари
        public int UpdateEventLocation(int eventId, int locationId, int id)
        {
            int eventId_ = -1;
            int locationId_ = -1;
            List<int> idsEvent = new List<int>();
            List<int> idsLocation = new List<int>();
            var evs = context.Event.FromSqlRaw(
              @"SELECT ""Event"".""ID"" AS Id, 
              ""EventName"".""Name"" AS EventName, 
              ""Event"".""Theme"" AS Theme, 
              ""Event"".""EventDate"" AS EventDate 
              FROM ""Event"", ""EventName""
              WHERE ""Event"".""EventNameId"" = ""EventName"".""ID""
              ORDER BY ""Event"".""ID"" ASC");
            foreach (TEvent ev in evs)
            {
                if (ev.Id == eventId)
                    eventId_ = eventId;
            }
            var locations = context.Location.FromSqlRaw(
              @"SELECT 
              ""Location"".""ID"" as Id,
              ""Location"".""Name"" as Name,
              ""Location"".""Address"" as Address,
              ""Location"".""NumberOfSeats"" as NumberOfSeats,
              ""Owner"".""Name"" as Owner
              FROM ""Location"", ""Owner""
              WHERE ""Location"".""OwnerId"" =  ""Owner"".""ID""
              ORDER BY ""Location"".""ID"" ASC");
            foreach (TLocation loc in locations)
            {
                if (loc.Id == locationId)
                    locationId_ = locationId;
            }
            if (eventId_ == -1)
                return -1;
            if (locationId_ == -1)
                return 0;
            List<int> ids = new List<int>();
            var eventLocations = context.EventLocation.FromSqlRaw(
             @"SELECT ""EventLocation"".""ID"" AS Id, 
              ""EventName"".""Name"" AS EventName, 
              ""Location"".""Name"" AS LocationName  
              FROM ""Event"", ""Location"", ""EventLocation"",""EventName""
              WHERE ""EventLocation"".""EventId"" = ""Event"".""ID""  
              AND ""EventLocation"".""LocationId"" = ""Location"".""ID""
              AND ""EventName"".""ID"" = ""Event"".""ID""");
            foreach (TEventLocation el in eventLocations)
            {
                ids.Add(el.Id);
            }

            if (!ids.Contains(id))
                return -2;

            var sqlQuery = $"UPDATE \"EventLocation\" SET \"EventId\"='{eventId_}',\"LocationId\"='{locationId_}' WHERE \"ID\" = {id}";
            int insert = context.Database.ExecuteSqlRaw(sqlQuery);

            context.SaveChangesAsync();
            context = new ContextClass();
            return 1;
        }// Оновлення пари
        public List<TEventLocation> SearchEventLocationByEvent(string name)
        {
            var pd = context.EventLocation.FromSqlRaw(
              @"SELECT ""EventLocation"".""ID"" AS Id, 
              ""EventName"".""Name"" AS EventName, 
              ""Location"".""Name"" AS LocationName  
              FROM ""Event"", ""Location"", ""EventLocation"",""EventName""
              WHERE ""EventLocation"".""EventId"" = ""Event"".""ID""  
              AND ""EventLocation"".""LocationId"" = ""Location"".""ID""
              AND ""EventName"".""ID"" = ""Event"".""ID""
              AND ""EventName"".""Name""  LIKE '%'||{0}||'%' ", name);

            return pd.ToList();
        }// Пошук пари за івентом
        public List<TEventLocation> SearchEventLocationByLocation(string name)
        {
            var pd = context.EventLocation.FromSqlRaw(
              @"SELECT ""EventLocation"".""ID"" AS Id, 
              ""EventName"".""Name"" AS EventName, 
              ""Location"".""Name"" AS LocationName  
              FROM ""Event"", ""Location"", ""EventLocation"",""EventName""
              WHERE ""EventLocation"".""EventId"" = ""Event"".""ID""  
              AND ""EventLocation"".""LocationId"" = ""Location"".""ID""
              AND ""EventName"".""ID"" = ""Event"".""ID""
              AND ""Location"".""Name""  LIKE '%'||{0}||'%' ", name);
            return pd.ToList();
        }// пошук пари за локацією 
        public void GenerateEventLocation(int n)
        {
            for (int i = 0; i < n; i++)
            { 
                List<int> idsEvent = new List<int>();
                List<int> idsLocation = new List<int>();
                var evs = context.Event.FromSqlRaw(
                  @"SELECT ""Event"".""ID"" AS Id, 
              ""EventName"".""Name"" AS EventName, 
              ""Event"".""Theme"" AS Theme, 
              ""Event"".""EventDate"" AS EventDate 
              FROM ""Event"", ""EventName""
              WHERE ""Event"".""EventNameId"" = ""EventName"".""ID""
              ORDER BY ""Event"".""ID"" ASC");
                foreach (TEvent ev in evs)
                {
                    idsEvent.Add(ev.Id);
                }
                var locations = context.Location.FromSqlRaw(
                  @"SELECT 
              ""Location"".""ID"" as Id,
              ""Location"".""Name"" as Name,
              ""Location"".""Address"" as Address,
              ""Location"".""NumberOfSeats"" as NumberOfSeats,
              ""Owner"".""Name"" as Owner
              FROM ""Location"", ""Owner""
              WHERE ""Location"".""OwnerId"" =  ""Owner"".""ID""
              ORDER BY ""Location"".""ID"" ASC");
                foreach (TLocation loc in locations)
                {
                    idsLocation.Add(loc.Id);
                }
                List<int> ids = new List<int>();
                var eventLocations = context.EventLocation.FromSqlRaw(
                 @"SELECT ""EventLocation"".""ID"" AS Id, 
                  ""EventName"".""Name"" AS EventName, 
                  ""Location"".""Name"" AS LocationName  
                  FROM ""Event"", ""Location"", ""EventLocation"",""EventName""
                  WHERE ""EventLocation"".""EventId"" = ""Event"".""ID""  
                  AND ""EventLocation"".""LocationId"" = ""Location"".""ID""
                  AND ""EventName"".""ID"" = ""Event"".""ID""");
                foreach (TEventLocation el in eventLocations)
                {
                    ids.Add(el.Id);
                }
                Random rnd = new Random();
                int Id = ids.Max(id => id) + 1;
                int eventId_ = idsEvent[rnd.Next(0, idsEvent.Count)];
                int locationId_ = idsLocation[rnd.Next(0, idsLocation.Count)];
                var sqlQuery = $"INSERT INTO \"EventLocation\" VALUES ('{Id}', " +
                    $"  {eventId_}, {locationId_} )";

                int insert = context.Database.ExecuteSqlRaw(sqlQuery);
            }
        }// Генерація нових пар івентів та локацій
    }
}

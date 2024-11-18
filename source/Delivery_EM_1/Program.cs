using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Delivery_EM_1
{
    internal class Program
    {
        public static DistrictManager DistrictManager = new DistrictManager();
        public static OrderManager OrderManager = new OrderManager();

        /// <summary>
        /// Спросить у пользователя район, по которому будет фильтрация
        /// </summary>
        /// <param name="argDist">Аргумент командной строки (Или пустая строка)</param>
        /// <returns>ID района</returns>
        static string AskDistrict(string argDist)
        {
            if (argDist != string.Empty)
            {
                if (DistrictManager.ContainsValue(argDist))
                    return DistrictManager.GetKey(argDist);

                if (DistrictManager.ContainsKey(argDist))
                    return argDist;
            }

            Console.WriteLine("Type district to filter:");
            string distId = string.Empty;
            while (distId == string.Empty)
            {
                distId = Console.ReadLine();

                // Если ввели не ID, а полное название
                if (DistrictManager.ContainsValue(distId))
                {
                    distId = DistrictManager.GetKey(distId);
                    break;
                }

                if (!DistrictManager.ContainsKey(distId))
                {
                    Console.WriteLine("District not found, try again:");
                    distId = string.Empty;
                }
            }

            return distId;
        }

        /// <summary>
        /// Спросить у пользователя время для фильтрации
        /// </summary>
        /// <param name="argTime">Аргумент командной строки (Или пустая строка)</param>
        /// <returns>Объект DateTime времени</returns>
        static DateTime AskFirstDeliveryTime(string argTime)
        {
            if (argTime != string.Empty)
            {
                try
                {
                    return DateTime.ParseExact(argTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                } catch { }
            }

            Console.WriteLine("\nType First Delivery Time time in format (yyyy-MM-dd HH:mm:ss):");
            string input = string.Empty;
            while (input == string.Empty)
            {
                input = Console.ReadLine();

                try
                {
                    return DateTime.ParseExact(input, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                }
                catch
                {
                    Console.WriteLine("Can't parse time (yyyy-MM-dd HH:mm:ss), try again:");
                    input = string.Empty;
                }
            }

            return DateTime.Now;
        }

        static void Main(string[] args)
        {
            CmdArgsParser parsedArgs = new CmdArgsParser(args);
            List<Order> filtered = new List<Order>();

            string distIdToFilter = AskDistrict(parsedArgs.CityDistrict);
            string distName = DistrictManager.Get(distIdToFilter);
            Console.WriteLine($"Sorting by district: \"{distName}\"");
            filtered = OrderManager.FindAll(o => o.District == distIdToFilter);

            DateTime startTime = AskFirstDeliveryTime(parsedArgs.FirstDelivery);
            DateTime endTime = startTime.AddMinutes(30);
            Console.WriteLine($"Sorting by time: {startTime} - {endTime}");

            using (StreamWriter logOutput = new StreamWriter(parsedArgs.DeliveryLog, true))
            {
                logOutput.WriteLine($"{DateTime.Now} | Sorting | District: [{distIdToFilter}] {distName} | First Delivery Time: {startTime}");
            }

            filtered = filtered.FindAll(o => o.Time >= startTime && o.Time <= endTime);
            if (filtered.Count == 0)
            {
                Console.WriteLine("\nThere is no orders for this filter settings.");
            }
            else
            {
                StreamWriter streamWriter = new StreamWriter(parsedArgs.DeliveryOrder);
                Console.WriteLine("\nFiltered orders:");
                foreach (Order order in filtered)
                {
                    string line = $"{order.Id} - {order.Weight} kg, {order.GetFullDistrict()}, {order.Time}";
                    Console.WriteLine(line);
                    streamWriter?.WriteLine(line);
                }
                streamWriter?.Close();
            }

            Console.WriteLine("Press any key to continue . . .");
            Console.ReadKey();
        }
    }
}

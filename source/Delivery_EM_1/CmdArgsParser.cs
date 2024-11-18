using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery_EM_1
{
    /// <summary>
    /// Чтение аргументов командной строки в виде "-key value -key2 value2"
    /// </summary>
    internal class CmdArgsParser
    {
        public readonly string CityDistrict = string.Empty;
        public readonly string FirstDelivery = string.Empty;

        public readonly string DeliveryLog = "logs.txt";
        public readonly string DeliveryOrder = "output.txt";

        public CmdArgsParser(string[] args)
        {
            // Если меньше 2-х аргументов, то параметров точно нет
            if (args.Length < 2)
                return;

            try
            {
                for (int i = 0; i < args.Length; i++)
                {
                    string arg = args[i].ToLower();
                    switch (arg)
                    {
                        case "-citydistrict":
                            CityDistrict = args[i + 1];
                            i++;
                            break;
                        case "-firstdelivery":
                            FirstDelivery = args[i + 1];
                            i++;
                            break;
                        case "-deliverylog":
                            DeliveryLog = args[i + 1];
                            i++;
                            break;
                        case "-deliveryorder":
                            DeliveryOrder = args[i + 1];
                            i++;
                            break;
                    }
                }
            } catch { }
        }
    }
}

using System.Collections.Generic;
using System.IO;

namespace Delivery_EM_1
{
    /// <summary>
    /// Менеджер со всеми районами в городе.
    /// </summary>
    internal class DistrictManager
    {
        private readonly Dictionary<string, District> districts = new Dictionary<string, District>();

        public DistrictManager() {
            string filePath = "districts.csv";
            foreach (var line in File.ReadLines(filePath))
            {
                if (line == "")
                    continue;

                var parts = line.Split(';');
                if (parts.Length < 2)
                    continue;

                District district = new District(parts[0], parts[1]);
                districts.Add(parts[0], district);
            }
        }

        /// <summary>
        /// Проверить на существование в списке район
        /// </summary>
        /// <param name="key">ID района</param>
        /// <returns>Есть ли район в списке?</returns>
        public bool ContainsKey(string key)
        {
            return districts.ContainsKey(key);
        }

        /// <summary>
        /// Проверить на существование в списке район по его полному названию
        /// </summary>
        /// <param name="val">Полное название района</param>
        /// <returns>Есть ли район в списке?</returns>
        public bool ContainsValue(string val)
        {
            foreach (var item in districts)
            {
                if (item.Value.Name == val)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Получить полное название района
        /// </summary>
        /// <param name="key">ID района</param>
        /// <returns>Полное название района</returns>
        public string Get(string key)
        {
            return districts[key]?.Name ?? string.Empty;
        }

        /// <summary>
        /// Получить ID района по его полному названию
        /// </summary>
        /// <param name="val">Полное название района</param>
        /// <returns>ID района</returns>
        public string GetKey(string val)
        {
            foreach (var item in districts)
            {
                if (item.Value.Name == val)
                    return item.Value.Id;
            }
            return string.Empty;
        }
    }
}

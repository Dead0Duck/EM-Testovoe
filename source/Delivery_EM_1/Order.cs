using System;
using System.Globalization;

namespace Delivery_EM_1
{
    internal class Order
    {
        private static readonly DistrictManager districtManager = Program.DistrictManager;

        /// <summary>
        /// ID заказа
        /// </summary>
        public string Id { get; }
        /// <summary>
        /// Вес заказа (В кг)
        /// </summary>
        public double Weight { get; }
        /// <summary>
        /// ID района
        /// </summary>
        public string District { get; }
        /// <summary>
        /// Время доставки заказа
        /// </summary>
        public DateTime Time { get; }

        public Order(string id, string weight, string district, string time)
        {
            if (!districtManager.ContainsKey(district))
                throw new FormatException();

            Id = id;
            Weight = double.Parse(weight, CultureInfo.InvariantCulture);
            District = district;
            Time = DateTime.ParseExact(time, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Получить полное название района для этого заказа
        /// </summary>
        /// <returns>Полное название района</returns>
        public string GetFullDistrict()
        {
            return districtManager.Get(District);
        }
    }
}

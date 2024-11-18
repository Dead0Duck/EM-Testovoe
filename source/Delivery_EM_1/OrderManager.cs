using System;
using System.Collections.Generic;
using System.IO;

namespace Delivery_EM_1
{
    /// <summary>
    /// Менеджер со всеми заказами.
    /// </summary>
    internal class OrderManager
    {
        private readonly List<Order> Orders = new List<Order>();

        public OrderManager() {
            string filePath = "orders.csv";
            foreach (var line in File.ReadLines(filePath))
            {
                if (line == "")
                    continue;

                var parts = line.Split(';');
                if (parts.Length < 4)
                    continue;

                try
                {
                    Order order = new Order(parts[0], parts[1], parts[2], parts[3]);
                    Orders.Add(order);
                }
                catch {}
            }
        }

        /// <summary>
        /// Получить количество заказов
        /// </summary>
        /// <returns>Количество заказов</returns>
        public int Count()
        {
            return Orders.Count;
        }

        /// <summary>
        /// Получить заказ по его номеру в списке (Не ID)
        /// </summary>
        /// <param name="key">Номер в списке</param>
        /// <returns>Объект заказа</returns>
        public Order Get(int key)
        {
            return Orders[key];
        }

        public Order this[int key]
        {
            get { return Get(key); }
        }

        /// <summary>
        /// Отсортировать заказы и получить их списком
        /// </summary>
        /// <returns>Список отсортированных заказов</returns>
        public List<Order> FindAll(Predicate<Order> predicate)
        {
            return Orders.FindAll(predicate);
        }
    }
}

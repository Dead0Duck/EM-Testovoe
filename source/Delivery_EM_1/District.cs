namespace Delivery_EM_1
{
    /// <summary>
    /// Район города
    /// </summary>
    internal class District
    {
        /// <summary>
        /// ID района
        /// </summary>
        public string Id { get; }
        /// <summary>
        /// Полное название района
        /// </summary>
        public string Name { get; }

        public District(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}

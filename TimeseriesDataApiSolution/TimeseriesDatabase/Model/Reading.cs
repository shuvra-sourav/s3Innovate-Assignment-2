using System;

namespace TimeseriesDatabase.Model
{
    public class Reading
    {
        public int Id { get; set; }
        public Int16 BuildingEntityId { get; set; }
        public byte DataFieldEntityId { get; set; }
        public byte ObjectEntityId { get; set; }
        public decimal Value { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
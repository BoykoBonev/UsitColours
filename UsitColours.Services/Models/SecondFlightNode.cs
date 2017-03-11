using System;

namespace UsitColours.Services.Models
{
    public class SecondFlightNode: IComparable
    {
        public SecondFlightNode(int airportId)
        {
            this.PreviousFlightTime = new DateTime(2030, 10, 22, 22, 22, 0);
            this.AirportId = airportId;
            this.ParentFlightId = -1;
            this.ParentAirportId = -1;
        }

        public int AirportId { get; set; }

        public DateTime PreviousFlightTime { get; set; }

        public int ParentFlightId { get; set; }

        public int ParentAirportId { get; set; }


        public int CompareTo(object obj)
        {
            var other = obj as SecondFlightNode;
            return DateTime.Compare(this.PreviousFlightTime, other.PreviousFlightTime);

        }
    }
}

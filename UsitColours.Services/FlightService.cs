using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UsitColours.Data.Contracts;
using UsitColours.Models;
using UsitColours.Services.Contracts.Factories;
using UsitColours.Services.DataStructures;
using UsitColours.Services.Models;
using UsitColours.Services.Utils;

namespace UsitColours.Services
{
    public class FlightService
    {

        private readonly IMappedClassFactory mappedFlightFactory;

        private readonly IFlightFactory flightFactory;

        private readonly IUsitData usitData;

        public FlightService(IMappedClassFactory mappedFlightFactory, IFlightFactory flightFactory, IUsitData usitData)
        {
            if (usitData == null)
            {
                throw new NullReferenceException("UsitData");
            }

        
            if (mappedFlightFactory == null)
            {
                throw new NullReferenceException("MappedFactory");
            }

            if(flightFactory == null)
            {
                throw new NullReferenceException("FlightFactory");
            }

            this.usitData = usitData;
            this.mappedFlightFactory = mappedFlightFactory;
            this.flightFactory = flightFactory;
        }

        public IEnumerable<PresentationFlight> GetFlights(int currentAirportId, int destinationAirportId, DateTime travelDate, int count)
        {
            var airports = this.usitData.Airports.All.ToList();

            var lowerBoundDate = travelDate.AddDays(-1);
            var upperBOundDate = travelDate.AddDays(1);

            var flights = this.usitData.Flights.All.Where(f => lowerBoundDate <= f.DateOfDeparture && f.DateOfDeparture <= upperBOundDate && f.AvailableSeats >= count).ToList();

            var dict = new Dictionary<SecondFlightNode, List<MappedFlight>>();
            var nodes = new Dictionary<int, SecondFlightNode>();

            foreach (var item in airports)
            {
                nodes.Add(item.Id, this.mappedFlightFactory.CreateSecondFlightNode(item.Id));
            }

            foreach (var item in flights)
            {
                var departureAirport = nodes[item.AirportDepartureId];
                var arrivalAirport = nodes[item.AirportArrivalId];
                var current = this.mappedFlightFactory.CreateMappedFlight(item.Id, departureAirport, arrivalAirport, item.DateOfDeparture, item.DateOfArrival);

                if (!dict.ContainsKey(departureAirport))
                {
                    dict.Add(departureAirport, new List<MappedFlight>());
                }

                dict[departureAirport].Add(current);
            }

            var heap = new PriorityQueue<SecondFlightNode>();

            var currentAirport = nodes[currentAirportId];

            // Source airport set before today
            currentAirport.PreviousFlightTime = new DateTime(1999, 1, 1);

            heap.Enqueue(currentAirport);

            while (heap.Count > 0)
            {
                var current = heap.Dequeue();

                if (!dict.ContainsKey(current))
                {
                    continue;
                }

                foreach (var flight in dict[current])
                {
                    bool isLastFlightBeforeCurrentFlight = current.PreviousFlightTime < flight.DepartureTime;
                    bool isNextAirportLastFlightAfterFlight = nodes[flight.ArrivalAirport.AirportId].PreviousFlightTime > flight.ArrivalTime;
                    if (isLastFlightBeforeCurrentFlight && isNextAirportLastFlightAfterFlight)
                    {
                        nodes[flight.ArrivalAirport.AirportId].PreviousFlightTime = flight.ArrivalTime;
                        nodes[flight.ArrivalAirport.AirportId].ParentAirportId = current.AirportId;
                        nodes[flight.ArrivalAirport.AirportId].ParentFlightId = flight.Id;

                        heap.Enqueue(flight.ArrivalAirport);
                    }
                }
            }

            var lastAirport = nodes[destinationAirportId];
            var trackChilds = new List<int>();
            trackChilds.Add(destinationAirportId);

            var listFLights = new List<int>();
            while (lastAirport.ParentFlightId != -1)
            {
                listFLights.Add(lastAirport.ParentFlightId);
                trackChilds.Add(lastAirport.ParentAirportId);
                lastAirport = nodes[lastAirport.ParentAirportId];
            }

            listFLights.ToArray();

            var resultFlights = new List<PresentationFlight>();

            for (int i = listFLights.Count - 1; i >= 0; i--)
            {
                var currentFlightId = listFLights[i];

                var currentFlight = this.usitData.Flights.GetById(currentFlightId);

                var newMappedFlight = this.mappedFlightFactory.CreatePresentationFlight(currentFlight.Id, currentFlight.AirportDeparture.City.Name, currentFlight.AirportArrival.City.Name, currentFlight.AirportDeparture.Name, currentFlight.AirportArrival.Name, currentFlight.DateOfDeparture, currentFlight.DateOfArrival, currentFlight.Price, currentFlight.Airline.Name);

                resultFlights.Add(newMappedFlight);
            }

            return resultFlights;
        }

        public IEnumerable<Flight> FilterFlights(string type, string filterExpression)
        {
            IEnumerable<Flight> resultQuery = null;

            if (type == "date")
            {
                DateTime queryDate;
                if(!DateTime.TryParse(filterExpression, out queryDate))
                {
                    return null;
                }

                var lowerBound = queryDate.AddDays(-1);
                var upperBound = queryDate.AddDays(1);

                resultQuery = this.usitData.Flights.All
                    .Where(x => lowerBound <= x.DateOfDeparture && x.DateOfDeparture <= upperBound)
                    .OrderBy(x => x.DateOfDeparture)
                    .ToList();
            }
            else if (type == "airline")
            {
                resultQuery = this.usitData.Flights.All
                    .Include(x => x.Airline).Where(x => x.Airline.Name == filterExpression)
                    .OrderBy(x => x.DateOfDeparture)
                    .ToList();
            }
            else if (type == "airport")
            {
                resultQuery = this.usitData.Flights.All.Include(x => x.AirportArrival)
                    .Include(x => x.AirportDeparture).Where(x => x.AirportDeparture.Name == filterExpression || x.AirportArrival.Name == filterExpression)
                    .OrderBy(x => x.DateOfDeparture)
                    .ToList();
            }

            return resultQuery;
        }

        public void UpdateFlight(Flight flight)
        {
            this.usitData.Flights.Update(flight);
            this.usitData.SaveChanges();
        }

        public IEnumerable<Flight> GetCheapestFlights()
        {
            var take = 3;
            var date = TimeProvider.Current.GetDate();

            var flights = this.usitData.Flights.All.Where(f => f.DateOfDeparture > date && f.AvailableSeats > 0).Include(f => f.AirportArrival).Include(f => f.AirportDeparture).OrderBy(f => f.Price).Take(take);

            return flights.ToList();
        }

        public Flight GetDetailedFlight(int id)
        {
            var flight = this.usitData.Flights.GetById(id);
            return flight;
        }

        public void AddFlight(int airportArrivalId, int airportDepartureId, DateTime departureDate, DateTime arrivalDate, decimal price, int airlineId, int availableSeats)
        {
            Flight flight = this.flightFactory.CreateFlight(airlineId, airportArrivalId, airportDepartureId, arrivalDate, departureDate, price, availableSeats);

            this.usitData.Flights.Add(flight);
            this.usitData.SaveChanges();
        }
    }
}

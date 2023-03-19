using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Data
{

	public class Flight
	{
	
		public string FlightCode { get; set; }

	
		public string Origin { get; set; }

	
		public string Destination { get; set; }

		public DayOfWeek DayOfWeek { get; set; }

		
		public string Airline { get; set; }

	
		public TimeSpan Time { get; set; }

	
		public int AvailableSeats { get; set; }

	
		public decimal Cost { get; set; }


		public Flight(string flightCode, string airline, string origin, string destination, DayOfWeek dayOfWeek, TimeSpan time, int availableSeats, decimal cost)
		{
			FlightCode = flightCode;
			Origin = origin;
			Destination = destination;
			DayOfWeek = dayOfWeek;
			Airline = airline;
			Time = time;
			AvailableSeats = availableSeats;
			Cost = cost;
		}

	
		public override string ToString()
		{
			return $"{FlightCode} - {Airline}: {Origin} to {Destination} ({DayOfWeek} {Time:hh\\:mm}) - ${Cost:F2} - {AvailableSeats} seats available";
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Data
{
	/// <summary>
	/// Represents a flight.
	/// </summary>
	public class Flight
	{
		/// <summary>
		/// Each flights uniqye code
		/// </summary>
		public string FlightCode { get; set; }

		/// <summary>
		/// Location from where flight takes off
		/// </summary>
		public string Origin { get; set; }

		/// <summary>
		/// Location of flight landing
		/// </summary>
		public string Destination { get; set; }

		/// <summary>
		/// The day of flight's take off
		/// </summary>
		public DayOfWeek DayOfWeek { get; set; }

		/// <summary>
		/// Name of the Airline
		/// </summary>
		public string Airline { get; set; }

		/// <summary>
		/// Time of flight take off
		/// </summary>
		public TimeSpan Time { get; set; }

		/// <summary>
		/// number of seats available on the flight
		/// </summary>
		public int AvailableSeats { get; set; }

		/// <summary>
		/// The cost of flight's ticket
		/// </summary>
		public decimal Cost { get; set; }

		/// <summary>
		/// Constructor to initialize the properties of the object.
		/// </summary>
		/// <param name="flightCode">flights unique code</param>
		/// <param name="origin">flights take off location</param>
		/// <param name="destination">flights destination location</param>
		/// <param name="dayOfWeek">flights day of take off</param>
		/// <param name="airline">flights airline name</param>
		/// <param name="time">flights take off time</param>
		/// <param name="availableSeats">flights available seats</param>
		/// <param name="cost">flight ticket cost</param>
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

		/// <summary>
		/// Returns a string that represents the current object properties.
		/// </summary>
		/// <returns>Formatted String</returns>
		public override string ToString()
		{
			return $"{FlightCode} - {Airline}: {Origin} to {Destination} ({DayOfWeek} {Time:hh\\:mm}) - ${Cost:F2} - {AvailableSeats} seats available";
		}
	}
}

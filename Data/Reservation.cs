using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Data
{
	/// <summary>
	/// Represents a flight reservation.
	/// </summary>
	public class Reservation
	{
		/// <summary>
		/// Gets or sets the reservation number.
		/// </summary>
		public string ReservationCode { get; set; }

		/// <summary>
		/// Gets or sets the flight object.
		/// </summary>
		public Flight Flight { get; set; }

		/// <summary>
		/// Gets or sets the passenger name.
		/// </summary>
		public string TravelerName { get; set; }

		/// <summary>
		/// Gets or sets the passenger's citizenship.
		/// </summary>
		public string Citizenship { get; set; }

		/// <summary>
		/// Gets or sets the reservation status.
		/// </summary>
		public bool IsActive { get; set; }

		/// <summary>
		/// Constructor for the Reservation class.
		/// </summary>
		/// <param name="reservationCode">Reservation code</param>
		/// <param name="flight">Flight obj</param>
		/// <param name="name">Passernger name</param>
		/// <param name="citizenship">Passenger citizenship</param>
		public Reservation(string reservationCode, Flight flight, string name, string citizenship)
		{
			ReservationCode = reservationCode;
			Flight = flight;
			TravelerName = name;
			Citizenship = citizenship;
			IsActive = true;
		}

		/// <summary>
		/// Method to update passenger info and reservation status.
		/// </summary>
		/// <param name="name">new passenger name</param>
		/// <param name="citizenship">new passenger citizenship</param>
		/// <param name="isActive">new reservation status</param>
		/// <exception cref="ArgumentException"></exception>
		public void Update(string name, string citizenship, bool isActive)
		{
			// if passenger name and citizenship are empty, throw an exception
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("Name cannot be empty.");
			}

			if (string.IsNullOrEmpty(citizenship))
			{
				throw new ArgumentException("Citizenship cannot be empty.");
			}

			TravelerName = name;
			Citizenship = citizenship;
			IsActive = isActive;
		}

		/// <summary>
		/// Mehtod to return a string representation of the reservation object.
		/// </summary>
		/// <returns>Formatted reservation string</returns>
		public override string ToString()
		{
			string status = IsActive ? "Active" : "Inactive";
			return $"Reservation Code: {ReservationCode}, Flight Code: {Flight.FlightCode}, Airline: {Flight.Airline}, " +
				   $"Cost: {Flight.Cost:C}, Name: {TravelerName}, Citizenship: {Citizenship}, Status: {status}";
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Data
{

	public class Reservation
	{
		
		public string ReservationCode { get; set; }

		
		public Flight Flight { get; set; }

		public string TravelerName { get; set; }

		
		public string Citizenship { get; set; }

		
		public bool IsActive { get; set; }

	
		public Reservation(string reservationCode, Flight flight, string name, string citizenship)
		{
			ReservationCode = reservationCode;
			Flight = flight;
			TravelerName = name;
			Citizenship = citizenship;
			IsActive = true;
		}


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

	
		public override string ToString()
		{
			string status = IsActive ? "Active" : "Inactive";
			return $"Reservation Code: {ReservationCode}, Flight Code: {Flight.FlightCode}, Airline: {Flight.Airline}, " +
				   $"Cost: {Flight.Cost:C}, Name: {TravelerName}, Citizenship: {Citizenship}, Status: {status}";
		}
	}
}

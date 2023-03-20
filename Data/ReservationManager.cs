namespace TravelAgency.Data
{
	
	public class ReservationManager
	{

		private static ReservationManager _instance;

		public static ReservationManager Instance
		{
			get
			{
				// If the instance is null, create a new instance.
				if (_instance == null)
				{
					_instance = new ReservationManager();
				}
				return _instance;
			}
		}

		// File names for the flights and reservations.
		private const string RESERVATION_FILE = "reservations.bin";
		private const string FLIGHT_FILE = "flights.csv";
		private const string AIRPORT_FILE = "airports.csv";

	
		private List<Reservation> reservations;

	
		private List<Flight> flights;

		private Dictionary<string, string> airports;


	
		public ReservationManager()
		{
			reservations = new List<Reservation>();
			LoadReservations();
			LoadFlights();
			LoadAirports();
		}

	
		public Dictionary<string, string> Airports
		{
			get { return airports; }
		}

		
		public List<Flight> Flights
		{
			get { return flights; }
		}

	
		public List<Reservation> Reservations
		{
			get { return reservations; }
		}

		
		public List<Reservation> FindReservations(string reservationCode, string airline, string travelerName)
		{
			List<Reservation> matchingReservations = new List<Reservation>();
			foreach (Reservation reservation in reservations)
			{
				
				bool matchesReservationCode = String.IsNullOrEmpty(reservationCode) || reservation.ReservationCode.Contains(reservationCode);
				bool matchesAirline = String.IsNullOrEmpty(airline) || reservation.Flight.Airline.Contains(airline);
				bool matchesTravelerName = String.IsNullOrEmpty(travelerName) || reservation.TravelerName.Contains(travelerName);
				if (matchesReservationCode && matchesAirline && matchesTravelerName)
				{
					matchingReservations.Add(reservation);
				}
			}
			return matchingReservations;
		}


		private string GenerateReservationCode()
		{
			Random random = new Random();
			string reservationCode = "";
			for (int i = 0; i < 5; i++)
			{
				if (i == 0)
				{
					reservationCode += (char)random.Next('A', 'Z' + 1);
				}
				else
				{
					reservationCode += random.Next(0, 9 + 1);
				}
			}
			return reservationCode;
		}

	
		private string GetNewReservationCode()
		{
			// Generate a new reservation code.
			string reservationCode = GenerateReservationCode();
			while (reservations.Exists(r => r.ReservationCode == reservationCode))
			{
				// If the reservation code is already used, generate a new one.
				reservationCode = GenerateReservationCode();
			}
			return reservationCode;
		}

	
		public Reservation MakeReservation(Flight flight, string travelerName, string citizenship)
		{
			// If the passenger name or citizenship were null then throw exception
			if (flight == null || String.IsNullOrEmpty(travelerName) || String.IsNullOrEmpty(citizenship))
			{
				throw new ArgumentException("Invalid reservation data");
			}

			if (flight.AvailableSeats <= 0)
			{
				throw new ArgumentException("No seats available on flight");
			}

			// Make a reservation and add it to the list
			Reservation reservation = new Reservation(GetNewReservationCode(), flight, travelerName, citizenship);
			reservations.Add(reservation);
			SaveReservations();
			return reservation;
		}

		public Reservation UpdateReservation(Reservation reservation, string travelerName, string citizenship, bool isActive)
		{
			// If the passenger name or citizenship were null then throw exception
			if (reservation == null)
			{
				throw new ArgumentException("Invalid reservation");
			}

			if (String.IsNullOrEmpty(travelerName) || String.IsNullOrEmpty(citizenship))
			{
				throw new ArgumentException("Invalid reservation data");
			}

			// Update the reservation
			reservation.Update(travelerName, citizenship, isActive);
			SaveReservations();
			return reservation;
		}

		
		private void LoadFlights()
		{
			flights = new List<Flight>();
			string assetsPath = Path.Combine(AppContext.BaseDirectory, "Assets", FLIGHT_FILE);
			try
			{
				// check if the file exists
				if (File.Exists(assetsPath))
				{
					using (StreamReader reader = new StreamReader(assetsPath))
					{
						// read the flights data and load them to the list
						while (!reader.EndOfStream)
						{
							string line = reader.ReadLine();
							string[] fields = line.Split(',');
							Flight flight = new Flight(fields[0], fields[1], fields[2], fields[3], (DayOfWeek)Enum.Parse(typeof(DayOfWeek), fields[4]), TimeSpan.Parse(fields[5]), int.Parse(fields[6]), decimal.Parse(fields[7]));
							flights.Add(flight);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		
		private void LoadAirports()
		{
			airports = new Dictionary<string, string>();
			string assetsPath = Path.Combine(AppContext.BaseDirectory, "Assets", AIRPORT_FILE);
			try
			{
				// check if the file exists
				if (File.Exists(assetsPath))
				{
					using (StreamReader reader = new StreamReader(assetsPath))
					{
						// read the airports data and load them to the list
						while (!reader.EndOfStream)
						{
							string line = reader.ReadLine();
							string[] fields = line.Split(',');
							airports.Add(fields[0], fields[1]);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		
		public List<Flight> FindFlights(string from, string to, string departureDay)
		{
			LoadFlights();
			List<Flight> matchingFlights = new List<Flight>();
			foreach (Flight flight in flights)
			{
				// If the parameters are not empty and do not match, skip this flights.
				bool matchesDepartureCity = String.IsNullOrEmpty(from) || flight.Origin.Contains(from);
				bool matchesArrivalCity = String.IsNullOrEmpty(to) || flight.Destination.Contains(to);
				bool matchesDepartureDate = departureDay == null || flight.DayOfWeek == (DayOfWeek)Enum.Parse(typeof(DayOfWeek), departureDay);
				if (matchesDepartureCity && matchesArrivalCity && matchesDepartureDate)
				{
					matchingFlights.Add(flight);
				}
			}
			return matchingFlights;
		}

		private void SaveReservations()
		{
			// get the path to the reservation file.
			string assetsPath = Path.Combine(AppContext.BaseDirectory, "Assets", RESERVATION_FILE);
			using (StreamWriter writer = new StreamWriter(assetsPath))
			{
				// save all the reservation parameters, comma seperated.
				foreach (Reservation reservation in reservations)
				{
					writer.Write(reservation.ReservationCode + ",");
					writer.Write(reservation.Flight.FlightCode + ",");
					writer.Write(reservation.Flight.Origin + ",");
					writer.Write(reservation.Flight.Destination + ",");
					writer.Write(reservation.Flight.DayOfWeek.ToString() + ",");
					writer.Write(reservation.Flight.Airline + ",");
					writer.Write(reservation.Flight.Time.ToString() + ",");
					writer.Write(reservation.Flight.AvailableSeats + ",");
					writer.Write(reservation.Flight.Cost + ",");
					writer.Write(reservation.TravelerName + ",");
					writer.Write(reservation.Citizenship + ",");
					writer.Write(reservation.IsActive);
					writer.Write("\n");
				}
			}
		}

		private void LoadReservations()
		{
			// get the path to the reservation file.
			string assetsPath = Path.Combine(AppContext.BaseDirectory, "Assets", RESERVATION_FILE);
			reservations = new List<Reservation>();
			try
			{
				using (StreamReader reader = new StreamReader(assetsPath))
				{
					// load all the reservation parameters, comma seperated. Make a resercation object and add to the list.
					while (!reader.EndOfStream)
					{
						string line = reader.ReadLine();
						string[] fields = line.Split(',');
						string reservationCode = fields[0];
						string flightCode = fields[1];
						string origin = fields[2];
						string dstination = fields[3];
						DayOfWeek dayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), fields[4]);
						string airline = fields[5];
						TimeSpan time = TimeSpan.Parse(fields[6]);
						int availableSeats = int.Parse(fields[7]);
						decimal cost = decimal.Parse(fields[8]);
						string travelerName = fields[9];
						string citizenship = fields[10];
						bool isActive = bool.Parse(fields[11]);

						Flight flight = new Flight(flightCode, airline, origin, dstination, dayOfWeek, time, availableSeats, cost);
						Reservation reservation = new Reservation(reservationCode, flight, travelerName, citizenship);
						reservation.Update(travelerName, citizenship, isActive);
						reservations.Add(reservation);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}

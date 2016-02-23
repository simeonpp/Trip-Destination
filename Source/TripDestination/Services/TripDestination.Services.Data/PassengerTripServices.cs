namespace TripDestination.Services.Data
{
    using Contracts;
    using System;
    using System.Linq;
    using TripDestination.Data.Common;
    using TripDestination.Data.Models;

    public class PassengerTripServices : IPassengerTripServices
    {
        private IDbRepository<PassengerTrip> passengerTripResos;

        public PassengerTripServices(IDbRepository<PassengerTrip> passengerTripResos)
        {
            this.passengerTripResos = passengerTripResos;
        }

        public void Delete(int id)
        {
            var passengerTrip = this.GetById(id);

            if (passengerTrip == null)
            {
                throw new Exception("Passenger trip not found.");
            }

            this.passengerTripResos.Delete(passengerTrip);
            this.passengerTripResos.Save();
        }

        public IQueryable<PassengerTrip> GetAll()
        {
            return this.passengerTripResos.All();
        }

        private PassengerTrip GetById(int id)
        {
            return this.passengerTripResos
                .All()
                .Where(pt => pt.Id == id).
                FirstOrDefault();
        }
    }
}

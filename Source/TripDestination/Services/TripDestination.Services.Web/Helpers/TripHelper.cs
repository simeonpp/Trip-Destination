namespace TripDestination.Services.Web.Helpers
{
    using Contracts;
    using Data.Contracts;
    using Services.Contracts;
    using System;
    using System.Collections.Generic;

    public class TripHelper : ITripHelper
    {
        public TripHelper(ITripServices tripServices)
        {
            this.TripServices = tripServices;
        }

        public ITripServices TripServices { get; set; }

        public IEnumerable<Tuple<string, string>> GetTopDestinations()
        {
            var topTownsFrom = this.TripServices.GetTopTownsDestination();

            // Getting 3 towns names will avoid duplicates even in worst case - FROM[X1, X2], TO[X1, X2, X3]
            var topTownsTo = this.TripServices.GetTopTownsDestination(false, 3);

            var tupleTownsList = new List<Tuple<string, string>>();
            int currentCounter = 0;
            foreach (var fromTown in topTownsFrom)
            {
                foreach (var toTown in topTownsTo)
                {
                    if (currentCounter >= 2)
                    {
                        break;
                    }

                    if (toTown == fromTown)
                    {
                        continue;
                    }

                    tupleTownsList.Add(new Tuple<string, string>(fromTown, toTown));
                    currentCounter++;
                }

                currentCounter = 0;
            }

            return tupleTownsList;
        }
    }
}

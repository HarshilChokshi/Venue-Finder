using Newtonsoft.Json;
using System;

namespace CoffeeFinder.Models
{
    public class VenueAddress
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
		private string _distance;
		public string Distance 
		{
			get
			{
				if (GlobalVariables.unitchange.Equals(1000.00))
				{
					return string.Format("Distance: {0:F} kilometres", Convert.ToDouble(_distance) / GlobalVariables.unitchange);
				}
				else {
					return string.Format("Distance: {0:F} miles", Convert.ToDouble(_distance) / GlobalVariables.unitchange);
				}
				//return string.Format("Distance: {0:F} miles", Convert.ToDouble(_distance)  / GlobalVariables.unitchange);
			}

			set
			{
				_distance = value;
			}
		}
		private string _postalcode;
		public string PostalCode
		{
			get
			{
				return string.Format("PostalCode: {0:D}", _postalcode);
			}

			set
			{
				_postalcode = value;
			}
		}
        [JsonProperty("lat")]
        public double Latitude { get; set; }
        [JsonProperty("lng")]
        public double Longitude { get; set; }
        public override string ToString()
        {
            return string.Format("{0}, {1} {2}", Address, City, State);
        }
    }
}

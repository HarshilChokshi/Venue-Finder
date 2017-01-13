using System.Collections.Generic;
using Newtonsoft.Json;

namespace CoffeeFinder.Models
{
    public class Venue
    {
        public string Id { get; set; }
        public string Name { get; set; }
        [JsonProperty("location")]
        public VenueAddress Address { get; set; }
		[JsonProperty("contact")]
		public VenueContact Contact { get; set; }
		[JsonProperty("categories")]
		public List<VenueCategories> Categories { get; set;}
		public string Url { get; set;}
		[JsonProperty("menu")]
		public VenueMenu Website { get; set; }
    }
}

using System;
using Newtonsoft.Json;

namespace CoffeeFinder
{
	public class VenueCategories
	{
		[JsonProperty("icon")]
		public VenueIcon Icon { get; set; }

	}
}

using System;
using Newtonsoft.Json;

namespace CoffeeFinder
{
	public class VenueIcon
	{
		[JsonProperty("prefix")]
		public String Prefix { get; set; }
		[JsonProperty("suffix")]
		public String Suffix { get; set; }
		public override string ToString()
		{
			return string.Format("{0}{1}{2}", Prefix, "120", Suffix);
		}
	}
}

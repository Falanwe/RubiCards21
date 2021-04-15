using System;
using System.Net.Http;

namespace NavalBattleClient
{
	class Program
	{
		static void Main(string[] args)
		{
			var client = new NavalBattleClient("localhost", new HttpClient());
			var game = await client.GetGameAsync();
		}
	}
	public class NavalBattleClient
	{
		private string v;
		private HttpClient httpClient;

		public NavalBattleClient(string v, HttpClient httpClient)
		{
			this.v = v;
			this.httpClient = httpClient;
		}
	}
}

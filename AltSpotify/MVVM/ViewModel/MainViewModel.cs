using AltSpotify.MVVM.Model;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators.OAuth2;
using System.Collections.ObjectModel;

namespace AltSpotify.MVVM.ViewModel
{
	class MainViewModel
	{
		public ObservableCollection<Track> Songs { get; set; }
		public MainViewModel()
		{
			Songs = new ObservableCollection<Track>();
			PopulateCollection();
		}

		private void PopulateCollection()
		{
			var client = new RestClient();
			client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator("BQBzhet8jMkAV9oQgw16GrxUdkdfCb7xXHU59634eQGMW1LHBofdAl2PHgrQXCNGBm00dbPOOarJ0Hsfd4DppnSrYfT33Xk6g7aBQHqR3nVn09hHWXwAk01tsnkW1OtUHIwSCCJuABK2My5kuH17NXNuYceyv9A", "Bearer");

			var request = new RestRequest("https://api.spotify.com/v1/browse/new-releases", Method.Get);
			request.AddHeader("Accept", "application/json");
			request.AddHeader("Content-Type", "application/json");

			var response = client.GetAsync(request).GetAwaiter().GetResult();
			var data = JsonConvert.DeserializeObject<TrackModel>(response.Content);

			for (int i = 0; i < data.Albums.Limit; i++)
			{
				var track = data.Albums.Items[i];
				track.Duration = "2:32";
				Songs.Add(track);
			}
		}
	}
}

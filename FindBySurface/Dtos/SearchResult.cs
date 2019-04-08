using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace FindBySurface.Dtos
{
    public sealed class SearchResult
    {
        private readonly Parcelle parcelle;
        public string Url => GetUrl(parcelle.Feature.GetCoordinates().First());

        public SearchResult(Parcelle parcelle)
        {
            this.parcelle = parcelle ?? throw new ArgumentNullException(nameof(parcelle));
        }

        private static string GetUrl(Coords coords)
        {
            var culture = CultureInfo.GetCultureInfo("en");
            string lng = coords.Longitude.ToString(culture);
            string lat = coords.Latitude.ToString(culture);
            return $"http://www.google.com/maps/place/{lat},{lng}";
        }

        public void OpenInNavigator()
        {
            Process.Start(@"C:\Program Files (x86)\Mozilla Firefox\firefox.exe", Url);
        }

        public override string ToString()
        {
            Feature feature = parcelle.Feature;

            return Url;
            return JsonConvert.SerializeObject(
                new
                {
                    Url,
                    feature.Properties.Commune,
                    feature.Properties.Section,
                    feature.Properties.Numero,
                    feature.Properties.Contenance
                }
            );
        }
    }
}

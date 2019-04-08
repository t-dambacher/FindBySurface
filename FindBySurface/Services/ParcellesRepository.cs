using FindBySurface.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FindBySurface.Services
{
    public sealed class ParcellesRepository
    {
        public async Task<IEnumerable<Parcelle>> GetAllAsync(Commune commune)
        {
            if (commune == null)
            {
                throw new ArgumentNullException(nameof(commune));
            }

            using (var client = new HttpClient())
            {
                Stream compressedJson = await client.GetStreamAsync($"https://cadastre.data.gouv.fr/data/etalab-cadastre/2019-01-01/geojson/communes/{commune.CodeDepartement}/{commune.CodeInsee}/cadastre-{commune.CodeInsee}-parcelles.json.gz");

                if (compressedJson == null)
                {
                    throw new InvalidOperationException("Impossible de trouver les données du cadastre pour cette commune.");
                }

                using (compressedJson)
                using (var compressedStream = new GZipStream(compressedJson, CompressionMode.Decompress))
                using (var uncompressedJson = new MemoryStream())
                {
                    await compressedStream.CopyToAsync(uncompressedJson);
                    uncompressedJson.Position = 0;

                    using (var sr = new StreamReader(uncompressedJson))
                    {
                        string json = await sr.ReadToEndAsync();
                        FeatureCollection result = JsonConvert.DeserializeObject<FeatureCollection>(json);
                        return (result?.Features ?? Enumerable.Empty<Feature>()).Select(f => new Parcelle(f)).ToList();
                    }
                }
            }
        }
    }
}

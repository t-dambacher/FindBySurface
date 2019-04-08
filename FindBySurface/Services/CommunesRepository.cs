using FindBySurface.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using System.Threading.Tasks;

namespace FindBySurface.Services
{
    public sealed class CommunesRepository
    {
        private static readonly Configuration configuration = new Configuration()
        {
            Delimiter = ";",
            HasHeaderRecord = true
        };

        public async Task<IList<Commune>> GetAllAsync()
        {
            using (var client = new HttpClient())
            {
                Stream csv = await client.GetStreamAsync("https://datanova.legroupe.laposte.fr/explore/dataset/laposte_hexasmal/download/?format=csv&timezone=Europe/Berlin&use_labels_for_header=true");

                if (csv == null)
                {
                    throw new InvalidOperationException(nameof(csv));
                }

                using (csv)
                using (var sr = new StreamReader(csv))
                using (var parser = new CsvParser(sr, configuration))
                {
                    IList<Commune> result = new List<Commune>();

                    await parser.ReadAsync();   // Read and ignore the first line

                    while (true)
                    {
                        string[] row = await parser.ReadAsync();
                        if ((row?.Length ?? 0) == 0)
                        { 
                            break;
                        }

                        result.Add(new Commune(row[0], row[2], row[1]));
                    }

                    return result;
                }
            }
        }
    }
}

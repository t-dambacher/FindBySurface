using FindBySurface.Dtos;
using FindBySurface.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace FindBySurface
{
    public sealed class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                (string communeName, double surface, double margin) = AskFromCli();

                var searchService = new SearchService(new CommunesRepository(), new ParcellesRepository());

                Console.WriteLine("Recherche...");
                IEnumerable<SearchResult> results = await searchService.FindAsync(communeName, surface, margin);

                if (!results.Any())
                {
                    Console.WriteLine("Aucun résultat trouvé...");
                }
                else
                {
                    Console.WriteLine(
                        string.Join(
                            Environment.NewLine, 
                            results.Select(r => r.ToString())
                        )
                    );

                    foreach (SearchResult result in results)
                    {
                        result.OpenInNavigator();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur est survenue : " + ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine();
            }

            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
        }

        private static (string communeName, double surface, double margin) AskFromCli()
        {
            Console.Write("Nom de la commune : ");
            string communeName = Console.ReadLine().Trim();
            Console.Write("Taille (ares) : ");
            double surface = double.Parse(Console.ReadLine().Trim(), CultureInfo.CurrentCulture);
            Console.Write("Marge (ares, defaut 0,1) : ");
            string margin = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(margin))
            {
                margin = "0,1";
            }
            return (communeName, surface, double.Parse(margin, CultureInfo.CurrentCulture));
        }
    }
}

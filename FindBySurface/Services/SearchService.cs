using FindBySurface.Dtos;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FindBySurface.Services
{
    public sealed class SearchService
    {
        private readonly CommunesRepository communesRepository;
        private readonly ParcellesRepository parcellesRepository;

        public SearchService(CommunesRepository communesRepository, ParcellesRepository parcellesRepository)
        {
            this.communesRepository = communesRepository ?? throw new ArgumentNullException(nameof(communesRepository));
            this.parcellesRepository = parcellesRepository ?? throw new ArgumentNullException(nameof(parcellesRepository));
        }

        public async Task<IEnumerable<SearchResult>> FindAsync(string communeName, double surface, double margin = 0.1d)
        {
            if (string.IsNullOrWhiteSpace(communeName))
            {
                throw new ArgumentNullException(nameof(communeName));
            }

            IEnumerable<Commune> communes = await this.communesRepository.GetAllAsync();
            Commune commune = communes.FirstOrDefault(c => string.Equals(communeName, c.Libelle, StringComparison.OrdinalIgnoreCase));
            if (commune == null)
            {
                throw new InvalidOperationException("Impossible de trouver une commune avec ce nom.");
            }

            IEnumerable<Parcelle> parcelles = await parcellesRepository.GetAllAsync(commune);

            return parcelles.Where(p => p.HasSize(surface, margin))
                .Select(p => new SearchResult(p))
                .ToList();
        }
    }
}

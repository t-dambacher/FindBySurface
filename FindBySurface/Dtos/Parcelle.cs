using FindBySurface.Services;
using System;

namespace FindBySurface.Dtos
{
    public sealed class Parcelle
    {
        internal Feature Feature { get; }
        private readonly Lazy<double> size;

        internal Parcelle(Feature feature)
        {
            this.Feature = feature ?? throw new ArgumentNullException(nameof(feature));
            this.size = new Lazy<double>(() => CoordsTools.ComputeArea(this.Feature.Geometry.Coordinates));
        }

        public bool HasSize(double approxSize, double margin = 0.1d)
        {
            double diff = Math.Abs(size.Value - approxSize);
            return diff <= margin;
        }
    }
}

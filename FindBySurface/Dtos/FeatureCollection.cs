using System;
using System.Collections.Generic;
using System.Linq;

namespace FindBySurface.Dtos
{
    internal sealed class FeatureCollection
    {
        public string Type { get; set; }
        public Feature[] Features { get; set; }
    }

    internal sealed class Feature
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public Geometry Geometry { get; set; }
        public Properties Properties { get; set; }

        public IList<Coords> GetCoordinates()
        {
            double[][][] fullCoords = this.Geometry?.Coordinates;
            if (fullCoords == null)
            {
                 Enumerable.Empty<Coords>();
            }

            double[][] coords = fullCoords[0];
            IList<Coords> result = new List<Coords>(coords.Length);

            for (int i = 0; i < coords.Length; ++i)
            {
                double[] point = coords[i];
                if (point.Length != 2)
                {
                    throw new InvalidOperationException("Impossible de convertir les données en coordonnées GPS.");
                }

                result.Add(new Coords(point[1], point[0]));
            }

            return result;
        }
    }

    internal sealed class Geometry
    {
        public string Type { get; set; }
        public double[][][] Coordinates { get; set; }
    }

    internal sealed class Properties
    {
        public string Id { get; set; }
        public string Commune { get; set; }
        public string Prefixe { get; set; }
        public string Section { get; set; }
        public string Numero { get; set; }
        public int Contenance { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}

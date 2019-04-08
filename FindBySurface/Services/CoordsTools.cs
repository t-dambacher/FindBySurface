using FindBySurface.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FindBySurface.Services
{
    internal static class CoordsTools
    {
       
        //public static double ComputeArea(IList<Coords> path)
        //{
        //    if ((path?.Count ?? 0) == 0)
        //    {
        //        throw new ArgumentNullException(nameof(path));
        //    }

        //    return Math.Round(Math.Abs(ComputeSignedArea(path) / 100d), 2, MidpointRounding.AwayFromZero);
        //}

        public static double ComputeArea(double[][][] path)
        {
            if ((path?.Length ?? 0) == 0)
            {
                throw new ArgumentNullException(nameof(path));
            }

            return Math.Round(Math.Abs(ComputeSignedArea(path) / 100d), 2, MidpointRounding.AwayFromZero);
        }

        private static double ComputeSignedArea(double[][][] path)
        {
            double area = 0;
            area += Math.Abs(RingArea(path[0]));

            for (var i = 1; i < path.Length; i++)
            {
                area -= Math.Abs(RingArea(path[i]));
            }

            return area;
        }

        private static double RingArea(double[][] coords)
        {
            int lowerIndex = 0, middleIndex = 0, upperIndex = 0;
            double[] p1, p2, p3;
            double area = 0;
            int coordsLength = coords.Length;

            if (coordsLength > 2)
                {
                for (int i = 0; i < coordsLength; i++)
                {
                    if (i == coordsLength - 2)
                    {// i = N-2
                        lowerIndex = coordsLength - 2;
                        middleIndex = coordsLength - 1;
                        upperIndex = 0;
                    }
                    else if (i == coordsLength - 1)
                    {// i = N-1
                        lowerIndex = coordsLength - 1;
                        middleIndex = 0;
                        upperIndex = 1;
                    }
                    else
                    { // i = 0 to N-3
                        lowerIndex = i;
                        middleIndex = i + 1;
                        upperIndex = i + 2;
                    }
                    p1 = coords[lowerIndex];
                    p2 = coords[middleIndex];
                    p3 = coords[upperIndex];
                    area += (Rad(p3[0]) - Rad(p1[0])) * Math.Sin(Rad(p2[1]));
                }

                area = area * wgs84RADIUS * wgs84RADIUS / 2d;
            }

            return area;
        }

        private const double wgs84RADIUS = 6378137d;

        private static double Rad(double value)
        {
            return value * Math.PI / 180d;
        }
    }
}

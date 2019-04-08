using System;
using FindBySurface.Dtos;
using FindBySurface.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace FindBySurface.Tests
{
    [TestClass]
    public class CoordsToolsTests
    {
        [TestMethod]
        public void Validate_AreaComputation()
        {
            string rawJson = "{\"type\":\"Feature\",\"id\":\"67437000080095\",\"geometry\":{\"type\":\"Polygon\",\"coordinates\":[[[7.3743455,48.7470763],[7.3745741,48.7471921],[7.3745359,48.7472243],[7.3742966,48.7474219],[7.3741234,48.747565],[7.3738999,48.7474454],[7.3740709,48.7473033],[7.3741489,48.7472394],[7.3741969,48.7472006],[7.3743397,48.7470812],[7.3743455,48.7470763]]]},\"properties\":{\"id\":\"67437000080095\",\"commune\":\"67437\",\"prefixe\":\"000\",\"section\":\"8\",\"numero\":\"95\",\"contenance\":1075,\"created\":\"2014 - 02 - 16\",\"updated\":\"2016 - 02 - 25\"}}";
            var feature = JsonConvert.DeserializeObject<Feature>(rawJson);
            double area = CoordsTools.ComputeArea(feature.Geometry.Coordinates);
            Assert.AreEqual(10.75d, area);
        }
    }
}

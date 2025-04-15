using TextTabulator.Adapters.Json;

namespace TextTabulator.Adapters.JsonTests
{
    public class MapNameTransformTests
    {
        [Fact]
        public void When_propertyname_matches_then_return_transformed_name()
        {
            var map = new Dictionary<string, string>
            {
                { "foobar1", "mapped1" },
                { "foobar2", "mapped2" },
                { "foobar3", "mapped3" },
            };

            var name = "foobar2";

            var sut = new MapNameTransform(map);

            var result = sut.Apply(name);

            Assert.Equal("mapped2", result);
        }

        [Fact]
        public void When_propertyname_does_not_match_then_return_transformed_name()
        {
            var map = new Dictionary<string, string>
            {
                { "foobar1", "mapped1" },
                { "foobar2", "mapped2" },
                { "foobar3", "mapped3" },
            };

            var name = "foobar4";

            var sut = new MapNameTransform(map);

            var result = sut.Apply(name);

            Assert.Equal("foobar4", result);
        }
    }
}

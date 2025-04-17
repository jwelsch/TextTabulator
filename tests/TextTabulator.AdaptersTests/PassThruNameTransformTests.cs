using TextTabulator.Adapters;

namespace TextTabulator.AdaptersTests
{
    public class PassThruNameTransformTests
    {
        [Fact]
        public void When_transform_applied_to_name_then_return_transformed_name()
        {
            var name = "foobar";

            var sut = new PassThruNameTransform();

            var result = sut.Apply(name);

            Assert.Equal(name, result);
        }

        [Fact]
        public void When_transform_applied_to_name_with_dashes_then_return_transformed_name()
        {
            var name = "foo-bar";

            var sut = new PassThruNameTransform();

            var result = sut.Apply(name);

            Assert.Equal(name, result);
        }

        [Fact]
        public void When_transform_applied_to_name_with_capitals_then_return_transformed_name()
        {
            var name = "Foobar";

            var sut = new PassThruNameTransform();

            var result = sut.Apply(name);

            Assert.Equal(name, result);
        }
    }
}

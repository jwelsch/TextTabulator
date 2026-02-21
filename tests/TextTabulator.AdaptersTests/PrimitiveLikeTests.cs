using TextTabulator.Adapters;
using TextTabulator.Testing;

namespace TextTabulator.AdaptersTests
{
    public class PrimitiveLikeTests
    {
        [Fact]
        public void When_type_is_sbyte_then_detect_returns_true()
        {
            Assert.True(PrimitiveLike.Detect(typeof(sbyte)));
        }

        [Fact]
        public void When_type_is_int16_then_detect_returns_true()
        {
            Assert.True(PrimitiveLike.Detect(typeof(Int16)));
        }

        [Fact]
        public void When_type_is_int32_then_detect_returns_true()
        {
            Assert.True(PrimitiveLike.Detect(typeof(Int32)));
        }

        [Fact]
        public void When_type_is_int64_then_detect_returns_true()
        {
            Assert.True(PrimitiveLike.Detect(typeof(Int64)));
        }

        [Fact]
        public void When_type_is_byte_then_detect_returns_true()
        {
            Assert.True(PrimitiveLike.Detect(typeof(byte)));
        }

        [Fact]
        public void When_type_is_uint16_then_detect_returns_true()
        {
            Assert.True(PrimitiveLike.Detect(typeof(UInt16)));
        }

        [Fact]
        public void When_type_is_uint32_then_detect_returns_true()
        {
            Assert.True(PrimitiveLike.Detect(typeof(UInt32)));
        }

        [Fact]
        public void When_type_is_uint64_then_detect_returns_true()
        {
            Assert.True(PrimitiveLike.Detect(typeof(UInt64)));
        }

        [Fact]
        public void When_type_is_float_then_detect_returns_true()
        {
            Assert.True(PrimitiveLike.Detect(typeof(float)));
        }

        [Fact]
        public void When_type_is_double_then_detect_returns_true()
        {
            Assert.True(PrimitiveLike.Detect(typeof(double)));
        }

        [Fact]
        public void When_type_is_decimal_then_detect_returns_true()
        {
            Assert.True(PrimitiveLike.Detect(typeof(decimal)));
        }

        [Fact]
        public void When_type_is_bool_then_detect_returns_true()
        {
            Assert.True(PrimitiveLike.Detect(typeof(bool)));
        }

        [Fact]
        public void When_type_is_char_then_detect_returns_true()
        {
            Assert.True(PrimitiveLike.Detect(typeof(char)));
        }

        [Fact]
        public void When_type_is_enum_then_detect_returns_true()
        {
            Assert.True(PrimitiveLike.Detect(typeof(TestEnum)));
        }

        [Fact]
        public void When_type_is_string_then_detect_returns_true()
        {
            Assert.True(PrimitiveLike.Detect(typeof(string)));
        }

        [Fact]
        public void When_type_is_DateTime_then_detect_returns_true()
        {
            Assert.True(PrimitiveLike.Detect(typeof(DateTime)));
        }

        [Fact]
        public void When_type_is_DateTimeOffset_then_detect_returns_true()
        {
            Assert.True(PrimitiveLike.Detect(typeof(DateTimeOffset)));
        }

        [Fact]
        public void When_type_is_TimeSpan_then_detect_returns_true()
        {
            Assert.True(PrimitiveLike.Detect(typeof(TimeSpan)));
        }

        [Fact]
        public void When_type_is_class_then_detect_returns_false()
        {
            Assert.False(PrimitiveLike.Detect(typeof(TestClass1)));
        }

        [Fact]
        public void When_type_is_struct_then_detect_returns_false()
        {
            Assert.False(PrimitiveLike.Detect(typeof(TestStruct1)));
        }

        [Fact]
        public void When_type_is_record_then_detect_returns_false()
        {
            Assert.False(PrimitiveLike.Detect(typeof(TestRecord1)));
        }

        [Fact]
        public void When_type_is_record_struct_then_detect_returns_false()
        {
            Assert.False(PrimitiveLike.Detect(typeof(TestRecordStruct1)));
        }

        [Fact]
        public void When_type_is_interface_then_detect_returns_false()
        {
            Assert.False(PrimitiveLike.Detect(typeof(ITestInterface1)));
        }

        [Fact]
        public void When_type_is_delegate_then_detect_returns_false()
        {
            Assert.False(PrimitiveLike.Detect(typeof(TestDelegate1)));
        }
    }
}

using TextTabulator.Adapters;
using TextTabulator.Testing;

namespace TextTabulator.AdaptersTests
{
    public class ReflectorTests
    {
        [Fact]
        public void When_getfieldinfos_called_with_member_binding_flags_then_return_all_fields()
        {
            var sut = new Reflector(typeof(TestClass9));

            var infos = sut.GetFieldInfos();

            Assert.NotNull(infos);
            Assert.Equal(3, infos.Length);
            Assert.Contains(infos, p => p.Name == "PublicField");
            Assert.Contains(infos, p => p.Name == "ProtectedField");
            Assert.Contains(infos, p => p.Name == "PrivateField");
        }

        [Fact]
        public void When_getfieldinfos_called_with_public_binding_flags_then_return_public_fields()
        {
            var sut = new Reflector(typeof(TestClass9));

            var infos = sut.GetFieldInfos(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            Assert.NotNull(infos);
            Assert.Single(infos);
            Assert.Contains(infos, p => p.Name == "PublicField");
        }

        [Fact]
        public void When_getfieldinfos_called_with_nonpublic_binding_flags_then_return_nonpublic_fields()
        {
            var sut = new Reflector(typeof(TestClass9));

            var infos = sut.GetFieldInfos(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            Assert.NotNull(infos);
            Assert.Equal(2, infos.Length);
            Assert.Contains(infos, p => p.Name == "ProtectedField");
            Assert.Contains(infos, p => p.Name == "PrivateField");
        }

        [Fact]
        public void When_getfieldinfo_called_with_existing_field_name_and_member_binding_flags_then_return_field()
        {
            var name = "PublicField";

            var sut = new Reflector(typeof(TestClass9));

            var info = sut.GetFieldInfo(name);

            Assert.NotNull(info);
            Assert.Equal(name, info.Name);
        }

        [Fact]
        public void When_getfieldinfo_called_with_nonexistent_field_name_and_member_binding_flags_then_return_null()
        {
            var name = "NonexistentField";

            var sut = new Reflector(typeof(TestClass9));

            var info = sut.GetFieldInfo(name);

            Assert.Null(info);
        }

        [Fact]
        public void When_getpropertyinfos_called_with_member_binding_flags_then_return_all_properties()
        {
            var sut = new Reflector(typeof(TestClass9));

            var infos = sut.GetPropertyInfos();

            Assert.NotNull(infos);
            Assert.Equal(3, infos.Length);
            Assert.Contains(infos, p => p.Name == "PublicProperty");
            Assert.Contains(infos, p => p.Name == "ProtectedProperty");
            Assert.Contains(infos, p => p.Name == "PrivateProperty");
        }

        [Fact]
        public void When_getpropertyinfos_called_with_public_binding_flags_then_return_public_properties()
        {
            var sut = new Reflector(typeof(TestClass9));

            var infos = sut.GetPropertyInfos(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            Assert.NotNull(infos);
            Assert.Single(infos);
            Assert.Contains(infos, p => p.Name == "PublicProperty");
        }

        [Fact]
        public void When_getpropertyinfos_called_with_nonpublic_binding_flags_then_return_nonpublic_properties()
        {
            var sut = new Reflector(typeof(TestClass9));

            var infos = sut.GetPropertyInfos(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            Assert.NotNull(infos);
            Assert.Equal(2, infos.Length);
            Assert.Contains(infos, p => p.Name == "ProtectedProperty");
            Assert.Contains(infos, p => p.Name == "PrivateProperty");
        }

        [Fact]
        public void When_getpropertyinfo_called_with_existing_property_name_and_member_binding_flags_then_return_property()
        {
            var name = "PublicProperty";

            var sut = new Reflector(typeof(TestClass9));

            var info = sut.GetPropertyInfo(name);

            Assert.NotNull(info);
            Assert.Equal(name, info.Name);
        }

        [Fact]
        public void When_getpropertyinfo_called_with_nonexistent_property_name_and_member_binding_flags_then_return_null()
        {
            var name = "NonexistentProperty";

            var sut = new Reflector(typeof(TestClass9));

            var info = sut.GetPropertyInfo(name);

            Assert.Null(info);
        }

        [Fact]
        public void When_getmethodinfos_called_with_member_binding_flags_then_return_all_methods()
        {
            var sut = new Reflector(typeof(TestClass9));

            var infos = sut.GetMethodInfos();

            Assert.NotNull(infos);
            Assert.True(infos.Length >= 3);
            Assert.Contains(infos, p => p.Name == "PublicMethod");
            Assert.Contains(infos, p => p.Name == "ProtectedMethod");
            Assert.Contains(infos, p => p.Name == "PrivateMethod");
        }

        [Fact]
        public void When_getmethodinfos_called_with_public_binding_flags_then_return_public_methods()
        {
            var sut = new Reflector(typeof(TestClass9));

            var infos = sut.GetMethodInfos(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            Assert.NotNull(infos);
            Assert.True(infos.Length >= 1);
            Assert.Contains(infos, p => p.Name == "PublicMethod");
        }

        [Fact]
        public void When_getmethodinfos_called_with_nonpublic_binding_flags_then_return_nonpublic_methods()
        {
            var sut = new Reflector(typeof(TestClass9));

            var infos = sut.GetMethodInfos(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            Assert.NotNull(infos);
            Assert.True(infos.Length >= 2);
            Assert.Contains(infos, p => p.Name == "ProtectedMethod");
            Assert.Contains(infos, p => p.Name == "PrivateMethod");
        }

        [Fact]
        public void When_getmethodinfo_called_with_existing_method_name_and_member_binding_flags_then_return_method()
        {
            var name = "PublicMethod";

            var sut = new Reflector(typeof(TestClass9));

            var info = sut.GetMethodInfo(name);

            Assert.NotNull(info);
            Assert.Equal(name, info.Name);
        }

        [Fact]
        public void When_getmethodinfo_called_with_nonexistent_method_name_and_member_binding_flags_then_return_null()
        {
            var name = "NonexistentMethod";

            var sut = new Reflector(typeof(TestClass9));

            var info = sut.GetMethodInfo(name);

            Assert.Null(info);
        }

        [Fact]
        public void When_getmemberinfos_called_with_member_binding_flags_then_return_all_members()
        {
            var sut = new Reflector(typeof(TestClass9));

            var infos = sut.GetMemberInfos();

            Assert.NotNull(infos);
            Assert.True(infos.Length >= 9);
            Assert.Contains(infos, p => p.Name == "PublicField");
            Assert.Contains(infos, p => p.Name == "ProtectedField");
            Assert.Contains(infos, p => p.Name == "PrivateField");
            Assert.Contains(infos, p => p.Name == "PublicProperty");
            Assert.Contains(infos, p => p.Name == "ProtectedProperty");
            Assert.Contains(infos, p => p.Name == "PrivateProperty");
            Assert.Contains(infos, p => p.Name == "PublicMethod");
            Assert.Contains(infos, p => p.Name == "ProtectedMethod");
            Assert.Contains(infos, p => p.Name == "PrivateMethod");
        }

        [Fact]
        public void When_getmemberinfos_called_with_public_binding_flags_then_return_public_members()
        {
            var sut = new Reflector(typeof(TestClass9));

            var infos = sut.GetMemberInfos(MemberType.Member, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            Assert.NotNull(infos);
            Assert.True(infos.Length >= 3);
            Assert.Contains(infos, p => p.Name == "PublicField");
            Assert.Contains(infos, p => p.Name == "PublicProperty");
            Assert.Contains(infos, p => p.Name == "PublicMethod");
        }

        [Fact]
        public void When_getmemberinfos_called_with_nonpublic_binding_flags_then_return_nonpublic_members()
        {
            var sut = new Reflector(typeof(TestClass9));

            var infos = sut.GetMemberInfos(MemberType.Member, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            Assert.NotNull(infos);
            Assert.True(infos.Length >= 6);
            Assert.Contains(infos, p => p.Name == "ProtectedField");
            Assert.Contains(infos, p => p.Name == "PrivateField");
            Assert.Contains(infos, p => p.Name == "ProtectedProperty");
            Assert.Contains(infos, p => p.Name == "PrivateProperty");
            Assert.Contains(infos, p => p.Name == "ProtectedMethod");
            Assert.Contains(infos, p => p.Name == "PrivateMethod");
        }
    }
}

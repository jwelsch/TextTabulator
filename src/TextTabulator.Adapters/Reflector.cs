using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TextTabulator.Adapters
{
    [Flags]
    public enum MemberType
    {
        Property = 0x1,
        Field = 0x2,
        Method = 0x4,
        Member = Property | Field | Method
    }

    public interface IReflector
    {
        FieldInfo? GetFieldInfo(string fieldName, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        FieldInfo[] GetFieldInfos(BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        PropertyInfo? GetPropertyInfo(string propertyName, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        PropertyInfo[] GetPropertyInfos(BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        MethodInfo? GetMethodInfo(string methodName, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        MethodInfo[] GetMethodInfos(BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        MemberInfo[] GetMemberInfos(MemberType memberType = MemberType.Member, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
    }

    public class Reflector : IReflector
    {
        private readonly Type _type;

        public Reflector(Type type)
        {
            _type = type;
        }

        public FieldInfo? GetFieldInfo(string fieldName, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
        {
            // Ignore backing fields.
            var info = fieldName.StartsWith("<") || fieldName.Contains(">k__BackingField") ? null : _type.GetField(fieldName, bindingFlags);
            return info == null || info.GetCustomAttributes<TabulatorIgnoreAttribute>().Any() ? null : info;
        }

        public FieldInfo[] GetFieldInfos(BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
        {
            // Ignore backing fields.
            var infos = _type.GetFields(bindingFlags).Where(i => !i.Name.StartsWith("<") || !i.Name.Contains(">k__BackingField")).ToArray();
            return infos.Where(i => !i.GetCustomAttributes<TabulatorIgnoreAttribute>().Any()).ToArray();
        }

        public PropertyInfo? GetPropertyInfo(string propertyName, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
        {
            var info = _type.GetProperty(propertyName, bindingFlags);
            return info != null && info.GetCustomAttributes<TabulatorIgnoreAttribute>().Any() ? null : info;
        }

        public PropertyInfo[] GetPropertyInfos(BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
        {
            var infos = _type.GetProperties(bindingFlags);
            return infos.Where(i => !i.GetCustomAttributes<TabulatorIgnoreAttribute>().Any()).ToArray();
        }

        public MethodInfo? GetMethodInfo(string methodName, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
        {
            var info = _type.GetMethod(methodName, bindingFlags);
            return info != null && info.GetCustomAttributes<TabulatorIgnoreAttribute>().Any() ? null : info;
        }

        public MethodInfo[] GetMethodInfos(BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
        {
            var infos = _type.GetMethods(bindingFlags);
            return infos.Where(i => !i.GetCustomAttributes<TabulatorIgnoreAttribute>().Any()).ToArray();
        }

        public MemberInfo[] GetMemberInfos(MemberType memberType = MemberType.Member, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
        {
            var memberInfos = new List<MemberInfo>();

            if ((memberType & MemberType.Field) == MemberType.Field)
            {
                var fieldInfos = GetFieldInfos(bindingFlags);

                if (fieldInfos.Length > 0)
                {
                    memberInfos.AddRange(fieldInfos);
                }
            }

            if ((memberType & MemberType.Property) == MemberType.Property)
            {
                var propertyInfos = GetPropertyInfos(bindingFlags);

                if (propertyInfos.Length > 0)
                {
                    memberInfos.AddRange(propertyInfos);
                }
            }

            if ((memberType & MemberType.Method) == MemberType.Method)
            {
                var methodInfos = GetMethodInfos(bindingFlags);

                if (methodInfos.Length > 0)
                {
                    memberInfos.AddRange(methodInfos);
                }
            }

            return memberInfos.ToArray();
        }
    }
}

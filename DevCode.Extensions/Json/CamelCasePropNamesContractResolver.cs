using DevCode.Extensions.DateTiming;
using DevCode.Extensions.Reflection;
using DevCode.Extensions.Timing;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Reflection;

namespace DevCode.Extensions.Json
{
    public class CamelCasePropNamesContractResolver : CamelCasePropertyNamesContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            ModifyProperty(member, property);

            return property;
        }

        protected virtual void ModifyProperty(MemberInfo member, JsonProperty property)
        {
            if (property.PropertyType != typeof(DateTime) && property.PropertyType != typeof(DateTime?))
            {
                return;
            }

            if (ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault<DisableDateTimeNormalizationAttribute>(member) == null)
            {
                property.Converter = new DateTimeConverter();
            }
        }
    }
}
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingYacht.API.Utilities.ContractResolver
{
    public class DynamicContractResolver : CamelCasePropertyNamesContractResolver
    {
        private readonly string _propertyTypeToExclude;

        public DynamicContractResolver(string propertyTypeToExclude)
        {
            _propertyTypeToExclude = propertyTypeToExclude;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);
            properties = properties.Where(p => !p.PropertyType.Name.Contains(_propertyTypeToExclude)).ToList();
            return properties;
        }
    }
}
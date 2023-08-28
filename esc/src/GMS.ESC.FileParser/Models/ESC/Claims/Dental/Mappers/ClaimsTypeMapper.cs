using FlatFiles;
using FlatFiles.TypeMapping;
using System;
using System.Collections.Generic;
using System.Reflection;
using static GMS.ESC.FileParser.Models.ESC.Claims.Dental.Mappers.ClaimTypeMapper;

namespace GMS.ESC.FileParser.Models.ESC.Claims.Dental.Mappers
{
    public static class ClaimsTypeMapper
    {
        private const int numberOfClaimsPerLine = 7;

        public static IFixedLengthTypeMapper<List<Claim>> GetClaimsTypeMapper()
        {
            var mapper = FixedLengthTypeMapper.Define(() => new List<Claim>());

            for (int i = 0; i < numberOfClaimsPerLine; i++)
            {
                mapper.CustomMapping(new FixedLengthComplexColumn($"Claim[{i}]", GetClaimTypeMapper().GetSchema()), 452).WithReader((ctx, claims, value) =>
                {
                    if (value != null)
                    {
                        claims.Add(ConvertToClaim((object[])value));
                    }
                });
            }

            return mapper;
        }

        private static Claim ConvertToClaim(object[] values)
        {
            if (values.Length != 48)
            {
                throw new ArgumentException("The values array should contain 48 elements.");
            }

            Claim claim = new Claim();

            Type claimType = typeof(Claim);
            PropertyInfo[] properties = claimType.GetProperties();

            for (int i = 0; i < properties.Length; i++)
            {
                PropertyInfo property = properties[i];
                Type propertyType = property.PropertyType;

                // Convert the object value to the property type
                object convertedValue = Convert.ChangeType(values[i], propertyType);

                // Set the property value using reflection
                property.SetValue(claim, convertedValue);
            }

            return claim;
        }
    }
}



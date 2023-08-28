using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace GMS.CIMS.BenefitsRemaining
{
    public static class Database<T>
    {
        public static List<T> CallStoredProcedure(string connectionString, string sprocName, string contractID)
        {
            List<T> rows = new();
            using var conn = new SqlConnection(connectionString);
            conn.Open();
            using var cmd = new SqlCommand(sprocName, conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ContractID", contractID));
            using SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                rows.Add(GetRow(rdr));
            }
            return rows;
        }

        private static T GetRow(SqlDataReader rdr)
        {
            T rowObject = (T)Activator.CreateInstance(typeof(T));

            foreach (var propInfo in rowObject.GetType().GetProperties())
            {
                SetValueInObject(rowObject, rdr, propInfo);
            }

            return rowObject;
        }

        private static void SetValueInObject(T obj, SqlDataReader rdr, PropertyInfo propInfo)
        {
            try
            {
                if (rdr[propInfo.Name] is not DBNull)
                {
                    propInfo.SetValue(obj, rdr[propInfo.Name], null);
                }
                else
                {
                    propInfo.SetValue(obj, null, null);
                }
            }
            catch (ArgumentException)
            {
                JObject deserialisedJson = (JObject)JsonConvert.DeserializeAnonymousType((string)rdr[propInfo.Name], Activator.CreateInstance(propInfo.PropertyType));
                propInfo.SetValue(obj, deserialisedJson.ToObject(propInfo.PropertyType), null);
            }
        }
    }
}

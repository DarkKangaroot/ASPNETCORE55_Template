using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace Services.Helpers
{
    public static class ConverterHelper
    {
        public static DateTime ToDateTime(this string date)
        {
            return DateTime.Parse(date);
        }

        public static TimeSpan ToTimeSpan(this string time)
        {
            return DateTime.Parse(time).TimeOfDay;
        }

        public static string ToDateTimeString(this object dateTime, string format)
        {
            return ToDateTime(dateTime.ToString()).ToString(format);
        }

        public static DataTable ToDataTables<T>(this IEnumerable<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
    }
}

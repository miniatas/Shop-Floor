using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Floor.GeneralTools
{
    public static class ExtentionMethods
    {
        public static T ToObject<T>(this DataRow dataRow, DataRow dr)
   where T : new()
        {
            T item = new T();
            foreach (DataColumn column in dataRow.Table.Columns)
            {
                PropertyInfo property = item.GetType().GetProperty(column.ColumnName);

                if (property != null && dataRow[column] != DBNull.Value)
                {
                    object result = Convert.ChangeType(dataRow[column], property.PropertyType);
                    property.SetValue(item, result, null);
                }
            }

            return item;
        }
    }
}

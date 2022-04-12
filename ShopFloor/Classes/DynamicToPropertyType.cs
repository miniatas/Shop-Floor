using ShopFloor.PlateForms.PlateProperties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopFloor.Classes
{
    class DynamicToPropertyType
    {
        public dynamic ReturnTypeForRowFields(DataRow row, Type T, string columnName)
        {



            if (T is int)
            {

                return row.Field<int>(columnName);
               
            } 

            if (T is decimal)
            {

                return row.Field<decimal>(columnName);

            }

            if (T is int?)
            {

                return row.Field<int?>(columnName);

            }

            if (T is decimal?)
            {

                return row.Field<decimal?>(columnName);

            }

            if (T is string)
            {

                return row.Field<string>(columnName);

            }

            if (T is DateTime)
            {

                return row.Field<DateTime>(columnName);

            }
            return null;
        }

        public object CreateListFromType(Type t)
        {
            // Create an array of the required type
            Array values = Array.CreateInstance(t, 50);

            // and fill it with values of the required type
            for (int i = 0; i < 50; i++)
            {
                var emptyClass = Activator.CreateInstance(t);
                values.SetValue(emptyClass, i);
            }

            // Create a list of the required type, passing the values to the constructor
            Type genericListType = typeof(List<>);
            Type concreteListType = genericListType.MakeGenericType(t);

            object list = Activator.CreateInstance(concreteListType, new object[] { values });

            // DO something with list which is now an List<t> filled with 50 ts
            return list;
        }
        public object ReturnPropertyClassList(Type t)
        {
            var list = Activator.CreateInstance<List<PlateTillDimensions>>();
            if (t is PlateTillDimensions)
            {

                List<PlateTillDimensions> plateTillDimensions = new List<PlateTillDimensions>();
                return plateTillDimensions;
            }

            if (t is PlateMakeDetails)
            {

                List<PlateMakeDetails> plateTillDimensions = new List<PlateMakeDetails>();
                return plateTillDimensions;
            }


            return null;
        }
    }
}

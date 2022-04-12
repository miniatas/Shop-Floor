using ShopFloor.PlateForms.PlateProperties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static ShopFloor.Enums.ShopfloorEnums;

namespace ShopFloor.Classes
{
    class ParameterListQuerryLogic
    {

        private DynamicToPropertyType propertyToType = new DynamicToPropertyType();

        public List<dynamic> ReturnSelectList(object propertyClass, SqlConnection connection, string querryString, List<string> parameterIDList,
            List<string> parameterValueList, List<string> columnNames = default(List<string>)) 
            {

            SqlCommand cmd = CreateSqlCommand(parameterIDList, parameterValueList, connection, querryString);
            DataTable dt = SelectStatements(cmd);
           
            if (columnNames != default(List<string>))
                return BindToProperty(propertyClass, dt, columnNames);
            else
                return BindToProperty(propertyClass, dt);

        }

        public int InsertQuerry(SqlConnection connection, string queryString, List<string> parameterIDList,
            List<string> parameterValueList)
        {
            int rowCount = 0;
            SqlCommand cmd = CreateSqlCommand(parameterIDList, parameterValueList, connection, queryString);
            rowCount = ExecuteInsertRowsAffected(cmd);

            return rowCount;
        }

        public int DeleteQuerry(SqlConnection connection, string queryString, List<string> parameterIDList,
    List<string> parameterValueList)
        {
            int rowCount = 0;
            SqlCommand cmd = CreateSqlCommand(parameterIDList, parameterValueList, connection, queryString);
            rowCount = ExecuteDeleteRowsAffected(cmd);

            return rowCount;
        }

        public SqlCommand CreateSqlCommand(List<string> parameterIDList, List<string> parameterValueList, SqlConnection connection, string querryString) {

            SqlCommand cmd = new SqlCommand(querryString, connection);
            
            cmd.CommandType = CommandType.Text;

            if (parameterIDList.Count == parameterValueList.Count)
            {

                for (int x = 0; x < parameterIDList.Count; x++)
                {

                    string parameterID = parameterIDList[x];
                    string parameterValue = parameterValueList[x];

                    // Check for empty string, convert to null for int?
                    if (String.IsNullOrEmpty(parameterValue))
                    {

                        cmd.Parameters.AddWithValue(parameterID, DBNull.Value);
                    }
                    else
                        cmd.Parameters.AddWithValue(parameterID, parameterValue);

                }

            }


            return cmd;
        }
        public DataTable QuerrySelection(SqlCommand cmd, ValueType querryType) {

            DataTable dt = new DataTable();

            if (querryType.Equals(SqlQuerryTypes.Select)) {

                dt = SelectStatements(cmd);

            }

            return dt;
        }
        public DataTable SelectStatements(SqlCommand cmd)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();

            adapter.SelectCommand = cmd;
            try
            {
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                    adapter.Fill(dt);
                

                }
                if (cmd.Connection.State == ConnectionState.Open)
                    cmd.Connection.Close();
            }
            catch (Exception ex) { }

            return dt;
        }

        //public object MapPropertyFromQuerry(IEnumerable<KeyValuePair<string, object>> data, object target)
        //{
        //    Type t = target.GetType();
        //    var publicProperties = t.GetProperties();
        //    var setters = from kp in data
        //                  let prop = publicProperties.SingleOrDefault(p => p.Name == kp.Key)
        //                  where prop != null && prop.CanWrite
        //                  select new { prop, kp.Value };
        //    foreach (var setter in setters)
        //    {
        //        setter.prop.SetValue(target, setter.Value, null);
        //    }

        //    return target;
        //}

        // Binds to a parameter type object
        public List<object> BindToProperty(object type, DataTable dt, List<string> columnNames = default(List<string>))
        {

            Type classType = type.GetType();
            int propertyIndex = 0;
            bool isCustomColumns = false;

            if (columnNames != default(List<string>)){

                isCustomColumns = true;
            }
            
            //PropertyInfo[] properties = classType.GetProperties();

            //var typeList = propertyToType.CreateListFromType(classType) as List<object>;

            //foreach (DataRow row in dt.Rows) {

            //    propertyIndex = 0;

            //    // An Activator is the way to dynimically create a class
            //    var typeCreated = Activator.CreateInstance(classType);
                

                
               var typeCreated = dt.Rows.Cast<object>().ToList();
            //    foreach (PropertyInfo property in properties)
            //    {
            //        var propertyType = property.GetType();
            //        string columnName;

            //        if (!isCustomColumns)
            //            columnName = property.Name;
            //        else
            //            columnName = columnNames[propertyIndex];

            //        // Will set each property in the "type created"
            //        // Need to reference method to dynamically create a list<type>
            //        property.SetValue(typeCreated, propertyToType.ReturnTypeForRowFields(row, propertyType, columnName));

            //    }
            //    typeList.Add(typeCreated);
            //}

            return typeCreated;

        }

        public int ExecuteInsertRowsAffected(SqlCommand cmd) {

            int rowsAffected = 0;
            SqlDataAdapter adapter = new SqlDataAdapter();

            adapter.InsertCommand = cmd;
            try
            {
                if (adapter.InsertCommand.Connection.State == ConnectionState.Closed)
                {
                    adapter.InsertCommand.Connection.Open();
                }

                rowsAffected = adapter.InsertCommand.ExecuteNonQuery();

                if (adapter.InsertCommand.Connection.State == System.Data.ConnectionState.Open)
                    adapter.InsertCommand.Connection.Close();
            }
            catch (Exception ex) {

            }

            return rowsAffected;
        }

        public int ExecuteDeleteRowsAffected(SqlCommand cmd)
        {

            int rowsAffected = 0;
            SqlDataAdapter adapter = new SqlDataAdapter();

            adapter.DeleteCommand = cmd;
            try
            {
                if (adapter.DeleteCommand.Connection.State == ConnectionState.Closed)
                {
                    adapter.DeleteCommand.Connection.Open();
                }

                rowsAffected = adapter.DeleteCommand.ExecuteNonQuery();

                if (adapter.DeleteCommand.Connection.State == System.Data.ConnectionState.Open)
                    adapter.DeleteCommand.Connection.Close();
            }
            catch (Exception ex)
            {

            }

            return rowsAffected;
        }

    }

    

}

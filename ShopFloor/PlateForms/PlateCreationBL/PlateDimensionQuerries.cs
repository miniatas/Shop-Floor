using Microsoft.ReportingServices.ReportProcessing;
using ShopFloor.Classes;
using ShopFloor.PlateForms.PlateProperties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopFloor.PlateForms.PlateCreationBL
{
    class PlateDimensionQuerries
    {
        private const string selectAllTillDimensions = @"
SELECT[PlateTillDimensionsID]
      ,[Length]
      ,[Width]
      ,[ImageableLength]
      ,[ImageableWidth],
        [Master Item No]
      
        FROM [dbo].[PlateTillDimensions]";

        //private List<string> selectTillPropertyIDs = new List<string>(new string[] { "@plateTillDimensions" });
                  

        private List<SearchItem> tillDimensionSearchItems = new List<SearchItem>();



        public List<PlateTillDimensions> SelectAllPlateDimensions(SqlConnection connection) {

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable ds = new DataTable();
            var cmd = new System.Data.SqlClient.SqlCommand();
            List<PlateTillDimensions> plateTillDimensions = new List<PlateTillDimensions>();

    
            cmd.Connection = connection;
            cmd.CommandType = CommandType.Text;

            // Set up to avoid SQL injection attacks
            cmd.CommandText = selectAllTillDimensions;

            adapter.SelectCommand = cmd;
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                    adapter.Fill(ds);
                    plateTillDimensions = BindPlateDimensions(ds);

                }
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            catch (Exception ex) { }



            return plateTillDimensions;
        }

        private List<PlateTillDimensions> BindPlateDimensions(DataTable dt)
        {
            tillDimensionSearchItems.Clear();

            
            List<PlateTillDimensions> plateTillDimensionsList = new List<PlateTillDimensions>();

            foreach (DataRow row in dt.Rows) {

                PlateTillDimensions temp = new PlateTillDimensions();

                temp.PlateTillDimensionID = row.Field<int>("PlateTillDimensionsID");
                temp.Length = row.Field<decimal>("Length");
                temp.Width = row.Field<decimal>("Width");
                temp.ImageableLength = row.Field<decimal>("ImageableLength");
                temp.ImageableWidth = row.Field<decimal>("ImageableWidth");
                temp.MasterItemNumber = row.Field<int>("Master Item No");

                plateTillDimensionsList.Add(temp);
            }

            return plateTillDimensionsList;
        }

        public List<string> GetPlateDimensionDescriptions(SqlConnection connection, List<string> masterItemNumbers) {

            List<string> descriptions = new List<string>();

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable ds = new DataTable();
            var cmd = new System.Data.SqlClient.SqlCommand();
            List<string> plateTillDescriptions = new List<string>();


            cmd.Connection = connection;
            cmd.CommandType = CommandType.Text;

            var parameters = masterItemNumbers.Select((masterItem, index) => new SqlParameter("@session_" + index, SqlDbType.Int) { Value = masterItem }).ToArray();
            // create the query
            cmd.CommandText = $"SELECT [Description] FROM [ovesql01].[Inventory Control].[dbo].[Inventory Master Table] WHERE [Master Item No] IN({string.Join(",", parameters.Select(x => x.ParameterName))})";
            // add parameters to the command
            cmd.Parameters.AddRange(parameters);

            adapter.SelectCommand = cmd;
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                    adapter.Fill(ds);
                    plateTillDescriptions = BindPlateDimensionDescriptions(ds);

                }
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            catch (Exception ex){ }

            return plateTillDescriptions;
        }

        private List<string> BindPlateDimensionDescriptions(DataTable dt)
        {
            List<string> descriptions = new List<string>();
            
            foreach (DataRow row in dt.Rows)
            {
                string temp;

                temp = row.Field<string>("Description");
             
                descriptions.Add(temp);
            }

            return descriptions;

        }
    }
}

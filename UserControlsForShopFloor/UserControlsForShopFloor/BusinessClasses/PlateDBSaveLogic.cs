using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserControlsForShopFloor.Classes;
using UserControlsForShopFloor.PropertyClasses;
using static ShopFloor.PlateCreationEntry;

namespace UserControlsForShopFloor.BusinessClasses
{
    public class PlateDBSaveLogic
    {

        private SqlDataAdapter sqlDataAdapter;

        private string insertPlateTiffInfo = @"INSERT INTO [Manufacturing].[dbo].[PlateTiffInfo]
           ([ProductionPlateID]
           ,[TiffLength]
           ,[TiffWidth]
           ,[CroppedLength]
           ,[CroppedWidth]
           ,[ExtendedCutLength])
     VALUES (@productionPlateID, @tiffLength, @tiffWidth, @croppedLength, @croppedWidth, @extraCutLength) SELECT SCOPE_IDENTITY()";

        private const string updatePlateTiffInfo = @"

        UPDATE [Manufacturing].[dbo].[PlateTiffInfo]
        SET [ProductionPlateID] = @productionPlateID
            ,[TiffLength] = @tiffLength
            ,[TiffWidth] = @tiffWidth
            ,[CroppedLength] = @croppedLength
            ,[CroppedWidth] = @croppedWidth
            ,[ExtendedCutLength] = @extraCutLength
        WHERE PlateTiffInfoID = @plateTiffInfo";

        private const string deletePlateTiffInfo = @"

            Delete From [Manufacturing].[dbo].[PlateTiffInfo]
            where PlateTiffInfoID = @plateTiffInfo";


        private const string insertPlateTillJob = "INSERT INTO [Manufacturing].[dbo].[PlateTillJobs] DEFAULT VALUES SELECT SCOPE_IDENTITY()";
        private const string deletePlateTillJob = "Delete From [Manufacturing].[dbo].[PlateTillJobs] where PlateTillJobID = @plateTillJobID";


        private const string parameterDeletePlateTill = "@plateTillJobID";
        private List<string> parametersUpdatePlateTiffInfo = new List<string> {
            "@plateTiffInfo",
            "@productionPlateID",
            "@tiffLength",
            "@tiffWidth",
            "@croppedLength",
            "@croppedWidth",
            "@extraCutLength"
        };
        private List<string> parameterIdInsertPlateTiffInfo = new List<string> {

            "@productionPlateID",
            "@tiffLength",
            "@tiffWidth",
            "@croppedLength",
            "@croppedWidth",
            "@extraCutLength"
            };

        private List<string> parameterIdDeletePlateTiffInfo = new List<string> {
         "@plateTiffInfo"
         };
        private string insertPlateTillTiffInfo = @"Insert into [Manufacturing].[dbo].[PlateTillTiffCombos] 
            (
                [PlateTillDimensionsID],
                [PlateTiffInfoID],
                [PlateTillJobID])
             VALUES (@plateTillDimensionsID, @plateTiffInfoID, @plateTillJobID) ";

        private string updatePlateTillTiffInfo = @"Update [Manufacturing].[dbo].[PlateTillTiffCombos]
        SET  [PlateTillDimensionsID] = @plateTillDimensionsID,
                [PlateTiffInfoID] = @plateTiffInfoID

             where [PlateTillJobID] = @plateTillJobID";


        private List<string> parameterIdInsertUpdateTillTiffCombo = new List<string> {

            "@plateTillDimensionsID",
            "@plateTiffInfoID",
            "@plateTillJobID"

            };

        private List<string> parameterIdDeletePlateTillTiffInfo = new List<string>();
        private bool isSaved = false;


        List<string> parameterValueList = new List<string>();
        List<string> parameterIDList = new List<string>();

        private PlateTiffInfo currentPlateTiffInfo = new PlateTiffInfo();
        private PlateTillTiffCombos currentTillTiffCombo = new PlateTillTiffCombos();
        public PlateDBSaveLogic() {


            // For testing purposes

            
            

        }

        #region ModifyTillJob
        public SqlDataAdapter InsertPlateTillJob() {

            try
            {
                sqlDataAdapter = new SqlDataAdapter();
                var cmd = new System.Data.SqlClient.SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = insertPlateTillJob;
                cmd.Connection = Connection;

                sqlDataAdapter.InsertCommand = cmd;

            }
            catch (Exception ex) {

                throw ex;

            }

            return sqlDataAdapter;
        }

        public SqlDataAdapter DeletePlateTillJob(int plateTillJobID) {

            sqlDataAdapter = new SqlDataAdapter();
            var cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = deletePlateTillJob;
            cmd.Connection = Connection;

            ClearParameterValueList();

            parameterValueList.Add(plateTillJobID.ToString());
            parameterIDList.Add(parameterDeletePlateTill);

            cmd = AddParameters(cmd, parameterIDList, parameterValueList);

            sqlDataAdapter.SelectCommand = cmd;

            return sqlDataAdapter;

        }
        #endregion
        private void ClearParameterValueList() {

            parameterValueList.Clear();
        

        }

        private SqlCommand AddParameters(SqlCommand cmd, List<string> parameterIDList, List<string> parameterValueList) {

            SqlCommand changedCommand = cmd;

            if (parameterIDList.Count == parameterValueList.Count) {

                for (int x = 0; x < parameterIDList.Count; x++) {

                    string parameterID = parameterIDList[x];
                    string parameterValue = parameterValueList[x];

                    // Check for empty string, convert to null for int?
                    if (String.IsNullOrEmpty(parameterValue)) {

                        changedCommand.Parameters.AddWithValue(parameterID, DBNull.Value);
                    }else
                    changedCommand.Parameters.AddWithValue(parameterID, parameterValue);

                }

            }

            return changedCommand;
        }

        #region TillTiffComboUpdate

        public SqlDataAdapter ChangePlateTillTiffCombo(PlateTillTiffCombos plateTillTiffCombo, ValueType querryType) {

            sqlDataAdapter = new SqlDataAdapter();
            var cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = Connection;

            ClearParameterValueList();

            if (querryType.Equals(SqlQuerryTypes.Insert)) {

                cmd = InsertPlateTiffTill(plateTillTiffCombo, cmd);
            }

            if (querryType.Equals(SqlQuerryTypes.Update))
            {
                cmd = UpdatePlateTiffTill(plateTillTiffCombo, cmd);

            }

            if (querryType.Equals(SqlQuerryTypes.Delete))
            {
                cmd = DeletePlateTiffTill(plateTillTiffCombo, cmd);
                

            }

            sqlDataAdapter.SelectCommand = cmd;

            return sqlDataAdapter;

        }

        private SqlCommand DeletePlateTiffTill(PlateTillTiffCombos plateTillTiffCombo, SqlCommand cmd)
        {
            cmd.CommandText = updatePlateTillTiffInfo;
            parameterIDList = parameterIdInsertUpdateTillTiffCombo;

            parameterValueList.Add(plateTillTiffCombo.PlateTillJobID.ToString());

            return AddParameters(cmd, parameterIDList, parameterValueList);

        }

        private SqlCommand UpdatePlateTiffTill(PlateTillTiffCombos plateTillTiffCombo, SqlCommand cmd)
        {

            cmd.CommandText = updatePlateTillTiffInfo;
            parameterIDList = parameterIdInsertUpdateTillTiffCombo;

            parameterValueList.Add(plateTillTiffCombo.PlateTillDimensionsID.ToString());
            parameterValueList.Add(plateTillTiffCombo.PlateTiffInfoID.ToString());
            parameterValueList.Add(plateTillTiffCombo.PlateTillJobID.ToString());

            return AddParameters(cmd, parameterIDList, parameterValueList);

        }


        private SqlCommand InsertPlateTiffTill(PlateTillTiffCombos plateTillTiffCombo, SqlCommand cmd) {

            cmd.CommandText = insertPlateTillTiffInfo;
            parameterIDList = parameterIdInsertUpdateTillTiffCombo;

            parameterValueList.Add(plateTillTiffCombo.PlateTiffInfoID.ToString());
            parameterValueList.Add(plateTillTiffCombo.PlateTillDimensionsID.ToString());
            parameterValueList.Add(plateTillTiffCombo.PlateTillJobID.ToString());

            return AddParameters(cmd, parameterIDList, parameterValueList);

        }

        #endregion


        #region plateTiffUpdates

        public bool DeletePlateTiff(PlateTiffInfo plateTiffInfo) { 
            bool isDeleteSuccess = false;
            try
            {
                SqlDataAdapter sqlDataAdapter = ChangePlateTiff(plateTiffInfo, SqlQuerryTypes.Delete);
                isDeleteSuccess = ExecuteSqlRowsAffectedCheck(sqlDataAdapter);
            }
            catch {

                return isDeleteSuccess;
            }

            CurrentPlateTiffInfo = new PlateTiffInfo();
            return isDeleteSuccess;
        }
        public SqlDataAdapter ChangePlateTiff(PlateTiffInfo plateTiffInfo, ValueType querryType) {

            sqlDataAdapter = new SqlDataAdapter();
            var cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = Connection;

            ClearParameterValueList();

            if (querryType.Equals(SqlQuerryTypes.Insert)) {

                cmd = InsertPlateTiff(plateTiffInfo, cmd);
            }

            if (querryType.Equals(SqlQuerryTypes.Update))
            {
                cmd = UpdatePlateTiff(plateTiffInfo, cmd);

            }

            if (querryType.Equals(SqlQuerryTypes.Delete))
            {
                cmd = DeletePlateTiff(plateTiffInfo, cmd);

            }

            sqlDataAdapter.InsertCommand = cmd;

            return sqlDataAdapter;
        }

        private SqlCommand UpdatePlateTiff(PlateTiffInfo plateTiffInfo, SqlCommand cmd)
        {
            cmd.CommandText = updatePlateTiffInfo;

            parameterIDList = parametersUpdatePlateTiffInfo;

            parameterValueList.Add(plateTiffInfo.PlateTiffInfoID.ToString());
            parameterValueList.Add(plateTiffInfo.ProductionPlateID.ToString());
            parameterValueList.Add(plateTiffInfo.TiffLength.ToString());
            parameterValueList.Add(plateTiffInfo.TiffWidth.ToString());
            parameterValueList.Add(plateTiffInfo.CroppedLength.ToString());
            parameterValueList.Add(plateTiffInfo.CroppedWidth.ToString());
            parameterValueList.Add(plateTiffInfo.ExtendedCutLength.ToString());

            return AddParameters(cmd, parameterIDList, parameterValueList);

        }

        private SqlCommand InsertPlateTiff(PlateTiffInfo plateTiffInfo, SqlCommand cmd)
        {
            cmd.CommandText = insertPlateTiffInfo;

            parameterIDList = parameterIdInsertPlateTiffInfo;

            parameterValueList.Add(plateTiffInfo.ProductionPlateID.Value.ToString());
            parameterValueList.Add(plateTiffInfo.TiffLength.ToString());
            parameterValueList.Add(plateTiffInfo.TiffWidth.ToString());
            parameterValueList.Add(plateTiffInfo.CroppedLength.ToString());
            parameterValueList.Add(plateTiffInfo.CroppedWidth.ToString());
            parameterValueList.Add(plateTiffInfo.ExtendedCutLength.ToString());

            return AddParameters(cmd, parameterIDList, parameterValueList);
        }

        private SqlCommand DeletePlateTiff(PlateTiffInfo plateTiffInfo, SqlCommand cmd)
        {
            cmd.CommandText = deletePlateTiffInfo;

            parameterIDList = parameterIdDeletePlateTiffInfo;

            parameterValueList.Add(plateTiffInfo.PlateTiffInfoID.ToString());

            return AddParameters(cmd, parameterIDList, parameterValueList);
        }
        #endregion

        private decimal ExecuteSqlInsertReturnID(SqlDataAdapter adapter)
        {
          
            using (DataSet ds = new DataSet())
            {
                decimal result;
                if (Connection.State == ConnectionState.Closed) {
                    Connection.Open();
                }

                //adapter.Fill(ds);

                result = (decimal)adapter.InsertCommand.ExecuteScalar();
                var restulType = result.GetType();

                if (Connection.State == System.Data.ConnectionState.Open) Connection.Close();

                return result;
            }

        }        

        private bool ExecuteSqlRowsAffectedCheck(SqlDataAdapter adapter)
        {
            try
            {
               
                    if (Connection.State == ConnectionState.Closed)
                    {
                        Connection.Open();
                    }

                  int rowsAffected = adapter.InsertCommand.ExecuteNonQuery();

                if (Connection.State == System.Data.ConnectionState.Open) Connection.Close();

                if (rowsAffected > 0)
                    return true;
                else return false;
                       
            }
            catch {

                return false;
            }
        }

        public bool SaveTiff(PlateTiffInfo plateTiffInfo, PlateTillTiffCombos tillTiffComboInfo) {

            CurrentTillTiffCombo = tillTiffComboInfo;
            CurrentPlateTiffInfo = plateTiffInfo;

            if (CurrentTillTiffCombo.PlateTillDimensionsID == 0) {

                return false;

            }

            if (CurrentTillTiffCombo.PlateTillJobID == 0) {

                SqlDataAdapter adapter = InsertPlateTillJob();
                CurrentTillTiffCombo.PlateTillJobID = (Int32)ExecuteSqlInsertReturnID(adapter);

                if (CurrentTillTiffCombo.PlateTillJobID < 1 ) { 
                                        
                    return false;

                } 

            }
                

                    if (CurrentTillTiffCombo.PlateTiffInfoID > 0)
                    {

                        try
                        {
                            SqlDataAdapter adapter = ChangePlateTiff(plateTiffInfo, SqlQuerryTypes.Update);
                    return ExecuteSqlRowsAffectedCheck(adapter);
                        }

                        catch { return false; }

                    }
                    else
                    {

                        try
                        {
                            SqlDataAdapter adapter = ChangePlateTiff(plateTiffInfo, SqlQuerryTypes.Insert);
                    CurrentTillTiffCombo.PlateTiffInfoID = (Int32)ExecuteSqlInsertReturnID(adapter);
                        }
                        catch (Exception ex){

                        var exception = ex;
                        return false; }
                    }
            isSaved = true;
            
            if (isSaved == true) {

         

            }

            return true;
        }


               
        private bool CheckIfPlateTillJobID(PlateTillTiffCombos tillTiffComboInfo)
        {

            if (tillTiffComboInfo .PlateTillJobID == 0)
                return false;
            else
                return true;
        }

      
        public SqlConnection Connection { get; set; }
        public PlateTiffInfo CurrentPlateTiffInfo
        {
            get; set;
        }
        public PlateTillTiffCombos CurrentTillTiffCombo
        {
            get { return currentTillTiffCombo; }
            set
            {

                // Check to make sure previous platetillID is not accidently being overwritten

                if (value.PlateTillJobID != 0)
                {

                    if (currentTillTiffCombo.PlateTillJobID != value.PlateTillJobID)
                    {

                        throw new Exception();
                    }

                }
                else { currentTillTiffCombo.PlateTillJobID = value.PlateTillJobID; }

                currentTillTiffCombo.PlateTillDimensionsID = value.PlateTillDimensionsID;

                // Check to make sure previous plateTiffInfoID is not accidently being overwritten

                if (value.PlateTillJobID != 0)
                {

                    if (currentTillTiffCombo.PlateTiffInfoID != value.PlateTiffInfoID)
                    {

                        throw new Exception();
                    }

                }
                else { currentTillTiffCombo.PlateTiffInfoID = value.PlateTiffInfoID; }
            }
        }
    }
}

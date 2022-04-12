using Shop_Floor.PlateForms.PlateProperties;
using ShopFloor.PlateForms.PlateProperties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserControlsForShopFloor.Classes;
using UserControlsForShopFloor.PropertyClasses;

namespace Shop_Floor.PlateForms.PlateCreationBL
{
    class FinalizePlateProduction
    {
        private int jobID;
        private SqlDataAdapter sqlDataAdapter;

        private string insertNewMake = @"INSERT INTO [dbo].[PlateMakeDetails]
           ([ProductionPlateID]
           ,[PlateMakeID]
           ,[PlateTiffInfoID]
          )
     VALUES
           ( @productionPlateIDMake, @plateMakeID, @plateTiffInfoIDMake
         )";


        private List<string> insertMakeParameters = new List<string> { "@productionPlateIDMake", "@plateMakeID", "@plateTiffInfoIDMake" };

        private string insertPlateTiffInfo = @"INSERT INTO [Manufacturing].[dbo].[PlateTiffInfo]
           ([ProductionPlateID]
           ,[TiffLength]
           ,[TiffWidth]
           ,[CroppedLength]
           ,[CroppedWidth]
           ,[ExtendedCutLength])
     VALUES (@productionPlateID, @tiffLength, @tiffWidth, @croppedLength, @croppedWidth, @extraCutLength) SELECT SCOPE_IDENTITY()";


        private const string insertPlateTillJob = "INSERT INTO [Manufacturing].[dbo].[PlateTillJobs] DEFAULT VALUES SELECT SCOPE_IDENTITY()";

        private List<string> parameterIdInsertPlateTiffInfo = new List<string> {

            "@productionPlateID",
            "@tiffLength",
            "@tiffWidth",
            "@croppedLength",
            "@croppedWidth",
            "@extraCutLength"
            };

        private List<string> parameterIDInsertPlateMake = new List<string> { "@productionPlateID", "@plateMakeID", "@plateTiffInfoID" };

        private string insertPlateTillTiffInfo = @"Insert into [Manufacturing].[dbo].[PlateTillTiffCombos] 
            (
                [PlateTillDimensionsID],
                [PlateTiffInfoID],
                [PlateTillJobID])
             VALUES (@plateTillDimensionsID, @plateTiffInfoID, @plateTillJobID) ";

        List<string> parameterValueList = new List<string>();
        List<string> parameterIDList = new List<string>();


        private List<string> parameterIdInsertUpdateTillTiffCombo = new List<string> {

            "@plateTillDimensionsID",
            "@plateTiffInfoID",
            "@plateTillJobID"

            };




        private SqlCommand AddParameters(SqlCommand cmd, List<string> parameterIDList, List<string> parameterValueList, int index)
        {

            SqlCommand changedCommand = cmd;

            if (parameterIDList.Count == parameterValueList.Count)
            {

                for (int x = 0; x < parameterIDList.Count; x++)
                {

                    string parameterID = parameterIDList[x];
                    string parameterValue = parameterValueList[x];

                    // Check for empty string, convert to null for int?
                    if (index == 0)
                    {
                        if (String.IsNullOrEmpty(parameterValue))
                        {

                            changedCommand.Parameters.AddWithValue(parameterID, DBNull.Value);
                        }
                        else
                            changedCommand.Parameters.AddWithValue(parameterID, parameterValue);
                    }
                    else {
                        if (String.IsNullOrEmpty(parameterValue))
                        {
                            changedCommand.Parameters[x].Value = DBNull.Value;
                        }else
                            changedCommand.Parameters[x].Value = parameterValue;


                    }
                }

            }

            return changedCommand;
        }

        private void ClearParameterValueList()
        {

            parameterValueList.Clear();


        }

        private SqlCommand InsertPlateTiffTill(PlateTillTiffCombos plateTillTiffCombo, SqlCommand cmd, int index)
        {

            cmd.CommandText = insertPlateTillTiffInfo;
            parameterIDList = parameterIdInsertUpdateTillTiffCombo;
            parameterValueList.Clear();
            parameterValueList.Add(plateTillTiffCombo.PlateTillDimensionsID.ToString());
            parameterValueList.Add(plateTillTiffCombo.PlateTiffInfoID.ToString());
            parameterValueList.Add(plateTillTiffCombo.PlateTillJobID.ToString());
            if (index == 0)
                return AddParameters(cmd, parameterIDList, parameterValueList, index);
            else
                return cmd;
        }

        private SqlCommand InsertPlateTiff(PlateTiffInfo plateTiffInfo, SqlCommand cmd, int index)
        {
            cmd.CommandText = insertPlateTiffInfo;

            parameterIDList = parameterIdInsertPlateTiffInfo;
            parameterValueList.Clear();
            parameterValueList.Add(plateTiffInfo.ProductionPlateID.Value.ToString());
            parameterValueList.Add(plateTiffInfo.TiffLength.ToString());
            parameterValueList.Add(plateTiffInfo.TiffWidth.ToString());
            parameterValueList.Add(plateTiffInfo.CroppedLength.ToString());
            parameterValueList.Add(plateTiffInfo.CroppedWidth.ToString());
            parameterValueList.Add(plateTiffInfo.ExtendedCutLength.ToString());

                return AddParameters(cmd, parameterIDList, parameterValueList, index);

            }

        public SqlCommand InsertNextMakeNumber(PlateTiffRowItems plateMakeDetails, SqlCommand cmd, int tiffID, int index)
        {
            // Make sure PlateTiffINfoID is inserted too
            int rowCount = 0;

            cmd.CommandText = insertNewMake;

            parameterIDList = insertMakeParameters;
            parameterValueList.Clear();
            parameterValueList.Add(plateMakeDetails.PlateNumber.ToString());
            parameterValueList.Add(plateMakeDetails.MakeNumber.ToString());
            parameterValueList.Add(tiffID.ToString());

            if (index == 0)
                return AddParameters(cmd, parameterIDList, parameterValueList, index);
            else
                return cmd;
        }
        private decimal ExecuteSqlInsertReturnID(SqlCommand adapter)
        {

            using (DataSet ds = new DataSet())
            {
                decimal result;
                if (adapter.Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }

                //adapter.Fill(ds);

                result = (decimal)adapter.ExecuteScalar();
                var restulType = result.GetType();

                return result;
            }

        }

        private bool ExecuteSqlRowsAffectedCheck(SqlCommand adapter)
        {
            try
            {

                if (adapter.Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }

                int rowsAffected = adapter.ExecuteNonQuery();

                if (rowsAffected > 0)
                    return true;
                else return false;

            }
            catch
            {
                return false;
            }
        }

        public SqlConnection Connection { get; set; }
        public PlateTiffInfo CurrentPlateTiffInfo
        {
            get; set;
        }
        public PlateTillTiffCombos CurrentTillTiffCombo
        {
            get; set;
        }

        public List<PlateTillTiffCombos> TillTiffComboList { get; set; } = new List<PlateTillTiffCombos>();
        public List<PlateTiffInfo> PlateTiffInfoList { get; set; } = new List<PlateTiffInfo>();

        public bool TheFinalQuery(List<PlateTiffInfo> plateTiffInfoList, PlateTillTiffCombos plateTillTiffCombo, 
            List<PlateTiffRowItems> plateTiffRowItems, SqlConnection mysqlConnection)
        {
        
            bool isSuccess = true;
            PlateTiffInfoList = plateTiffInfoList;
            PlateTillTiffCombos _plateTillTiffCombo = plateTillTiffCombo;
            try {
                using (SqlConnection sqlConnection = mysqlConnection)
                {
                    bool isRollback = false;

                    sqlConnection.Open();

                    using (SqlTransaction sqlTrans = sqlConnection.BeginTransaction())
                    {
                        try
                        {
                            SqlCommand sqlCommand = new SqlCommand(insertPlateTillJob, sqlConnection, sqlTrans);

                            // Gets Job ID
                            _plateTillTiffCombo.PlateTillJobID = Convert.ToInt32(ExecuteSqlInsertReturnID(sqlCommand));


                            // Inserts the Tiff Information

                            int plateTiffIndex = 0;
                            List<PlateTiffInfo> tempPlateList = new List<PlateTiffInfo>();
                            PlateTiffInfoList.ForEach(plateTiffInfo =>
                            {
                      
                                    sqlCommand = InsertPlateTiff(plateTiffInfo, sqlCommand, plateTiffIndex);
                                    plateTiffInfo.PlateTiffInfoID = Convert.ToInt32(ExecuteSqlInsertReturnID(sqlCommand));

                                if (plateTiffInfo.PlateTiffInfoID == null)
                                {
                                    sqlTrans.Rollback();
                                    sqlConnection.Close();
                                    sqlConnection.Dispose();
                                    isSuccess = false;
                                }

                                tempPlateList.Add(plateTiffInfo);
                                plateTiffIndex++;

                            });

                            // Transfers the templist to the permalist
                            PlateTiffInfoList = tempPlateList;

                            // Inserts the TillTiffInformation
                            for (int plateTillTiffIndex = 0; plateTillTiffIndex < plateTiffIndex; plateTillTiffIndex++)
                            {

                                _plateTillTiffCombo.PlateTiffInfoID = PlateTiffInfoList[plateTillTiffIndex].PlateTiffInfoID.Value;

                                sqlCommand = InsertPlateTiffTill(_plateTillTiffCombo, sqlCommand, plateTillTiffIndex);
                                if (plateTillTiffIndex > 0)
                                {
                                    int parameterCount = sqlCommand.Parameters.Count;
                                    int indexToAccess = parameterCount - parameterIDList.Count;
                                    int parameterValueIndex = 0;

                                    while (indexToAccess < parameterCount)
                                    {
                                        sqlCommand.Parameters[indexToAccess].Value = parameterValueList[parameterValueIndex].ToString();
                                        indexToAccess++;
                                        parameterValueIndex++;
                                    }
                                }
                                    int rowsAffected = sqlCommand.ExecuteNonQuery();

                                    if (rowsAffected == 0)
                                    {
                                        sqlTrans.Rollback();
                                        sqlConnection.Close();
                                        sqlConnection.Dispose();
                                        isSuccess = false;

                                    }
                                
                            }

                            // Inserts the Make of the plates
                            int makeRowsAffected = 0;
                            int tiffIdIndex = 0;
                            plateTiffRowItems.ForEach(plateTiffRowItem =>
                            {
                                int tiffID = PlateTiffInfoList[tiffIdIndex].PlateTiffInfoID.Value;
                                sqlCommand = InsertNextMakeNumber(plateTiffRowItem, sqlCommand, tiffID, tiffIdIndex);

                                if (tiffIdIndex > 0)
                                {
                                    int parameterCount = sqlCommand.Parameters.Count;
                                    int indexToAccess = parameterCount - parameterIDList.Count;
                                    int parameterValueIndex = 0;

                                    while (indexToAccess < parameterCount)
                                    {
                                        sqlCommand.Parameters[indexToAccess].Value = parameterValueList[parameterValueIndex].ToString();
                                        indexToAccess++;
                                        parameterValueIndex++;

                                    }
                                }



                                makeRowsAffected = sqlCommand.ExecuteNonQuery();

                                if (makeRowsAffected == 0)
                                {
                                    sqlTrans.Rollback();
                                    sqlConnection.Close();
                                    sqlConnection.Dispose();
                                    isSuccess = false;

                                }
                                tiffIdIndex++;
                            });


                            sqlTrans.Commit();
                        }
                        catch (Exception ex)
                        {
                            sqlTrans.Rollback();
                            sqlConnection.Close();
                            sqlConnection.Dispose();
                            isSuccess = false;

                        }

                        sqlConnection.Close();

                    }
                }
            } catch (Exception ex) {
                isSuccess = false;
                

            }
            
            
            return isSuccess;
        }
    }


   
}

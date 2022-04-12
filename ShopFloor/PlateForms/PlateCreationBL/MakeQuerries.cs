using Shop_Floor.GeneralTools;
using ShopFloor.Classes;
using ShopFloor.PlateForms.PlateProperties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Shop_Floor.GeneralTools;



namespace ShopFloor.PlateForms.PlateCreationBL
{

    class MakeQuerries
    {
        private const string selectLastMake = @"
select * from PlateMakeDetails where ProductionPlateID = @plateProductionID AND
PlateMakeID = (SELECT MAX(PlateMakeID) FROM PlateMakeDetails)";

        private List<string> selectMakeParameters = new List<string> { "@plateProductionID" };


        private const string insertNewMake = @"INSERT INTO [dbo].[PlateMakeDetails]
           ([ProductionPlateID]
           ,[PlateMakeID]
           ,[PlateTiffInfoID]
          )
     VALUES
           ( @productionPlateID, @plateMakeID, @plateTiffInfoID
         )";

        private List<string> insertMakeParameters = new List<string> { "@plateProductionID", "@nextPlateMake", "@plateTiffInfoID" };

        private ParameterListQuerryLogic parameterListQuerryLogic = new ParameterListQuerryLogic();

        private List<string> parameterValueList = new List<string>();
        private List<string> parameterIDList = new List<string>();
        private List<string> parameterIDInsertPlateMake = new List<string> { "@productionPlateID", "@plateMakeID", "@plateTiffInfoID" };
        public PlateMakeDetails GetMakeNumber(PlateMakeDetails plateMakeDetails, SqlConnection connection) {

            int firstTimeMakeNumber = 0;
                     

                List<PlateMakeDetails> plateMakeList = new List<PlateMakeDetails>();

                parameterValueList.Clear();
                parameterIDList.Clear();

                parameterIDList = selectMakeParameters;
                parameterValueList.Add(plateMakeDetails.ProductionPlateID.ToString());

   
            PlateMakeDetails makeDetails = new PlateMakeDetails();


            try
                {
                makeDetails = plateMakeDetails;
                    var plateMakeObject = parameterListQuerryLogic.ReturnSelectList(plateMakeDetails, connection, selectLastMake,
                          parameterIDList, parameterValueList);

                if (plateMakeObject != null)
                {
                                      
                    DataRow dr = plateMakeObject[0];

               

                    makeDetails = dr.ToObject<PlateMakeDetails>(dr);
                
                }
                else {

                    makeDetails = plateMakeDetails;
                    makeDetails.PlateMakeID = firstTimeMakeNumber;


                }
         
            }
                catch (Exception ex) {

                makeDetails.PlateMakeID = 0;
            }
        
                   return makeDetails;
        }

    
        public int InsertNextMakeNumber(PlateMakeDetails plateMakeDetails, SqlConnection connection)
        {
            // Make sure PlateTiffINfoID is inserted too
            int rowCount = 0;
        
                parameterValueList.Clear();
                parameterIDList.Clear();

                parameterIDList = insertMakeParameters;

                parameterValueList.Add(plateMakeDetails.ProductionPlateID.ToString());
                parameterValueList.Add(plateMakeDetails.PlateMakeID.ToString());

              
            return rowCount;
        }

        public int DeleteMakeNumber() {

            throw new NotImplementedException();

        }





    }
}

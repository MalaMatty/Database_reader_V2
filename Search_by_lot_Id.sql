Create Procedure [dbo].[Search_by_lot_id] 
(
@Lot_id nvarchar (50)
)
as  
begin  
   select * from RPT_Lots_Report where Rep_Lot_Id=@Lot_id
End
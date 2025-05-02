#nullable disable

using Acumatica.RESTClient.ContractBasedApi.Model;

namespace RevisionTwoApp.RestApi.Models.Default;
/// <summary>
///  Models of Acumatica Default EndPoints. 
///  These classes are custom models from the complete Models of Acumatica <see cref="RestClient"></see> .
///  These Models are used for Rest API communications with the Acumatica Instance. They are not for EntityFramework
///  Nuget Package - Acumatica Models all available with RestClient
/// </summary>

/// <summary>
///  mapping class for standard output form
/// </summary>
public class Bill_Model
{
    #region Bill_Model
    public StringValue Type { get; set; }
    public StringValue ReferenceNbr { get; set; }
    public StringValue Status { get; set; }
    public DateTimeValue Date { get; set; }
    public StringValue PostPeriod { get; set; }
    public StringValue Vendor { get; set; }
    public StringValue Description { get; set; }
    public StringValue VendorRef { get; set; }
    public DecimalValue Amount { get; set; }
    public StringValue CurrencyID { get; set; }
    public DateTimeValue LastModifiedDateTime { get; set; }
    #endregion
}
/// <summary>
///  mapping class for standard output form
/// </summary>
public class BillDetail_Model
{
    #region BillDetail_Model
    public StringValue Branch { get; set; }
    public StringValue InventoryID { get; set; }
    public StringValue TransactionDescription { get; set; }
    public DecimalValue Qty { get; set; }
    public DecimalValue UnitCost { get; set; }
    public DecimalValue Amount { get; set; }
    public StringValue Account { get; set; }
    public StringValue Description { get; set; }
    public StringValue CostCode { get; set; }
    public StringValue POOrderNbr { get; set; }
    #endregion
}
/// <summary>
///  mapping class for standard output form
/// </summary>
public class Case_Model
{
    #region Case_Model   
    public StringValue CaseID { get; set; }
    public StringValue Subject { get; set; }
    public StringValue BusinessAccount { get; set; }
    public StringValue BusinessAccountName { get; set; }
    public StringValue Status { get; set; }
    public StringValue Reason { get; set; }
    public StringValue Severity { get; set; }
    public StringValue Priority { get; set; }
    public StringValue Owner { get; set; }
    public StringValue Workgroup { get; set; }
    public StringValue ClassID { get; set; }
    public DateTimeValue DateReported { get; set; }
    public DateTimeValue LastActivityDate { get; set; }
    public DateTimeValue LastModifiedDateTime { get; set; }
    #endregion
}
/// <summary>
///  mapping class for standard output form
/// </summary>
public class Contact_Model
{
    #region Contact_Model
    public BooleanValue Active { get; set; }
    public IntValue ContactID { get; set; }
    public StringValue ContactClass { get; set; }
    public StringValue DisplayName { get; set; }
    public StringValue BusinessAccount { get; set; }
    public StringValue JobTitle { get; set; }
    public StringValue Owner { get; set; }
    public StringValue Status { get; set; }
    public DateTimeValue LastModifiedDateTime { get; set; }
    #endregion
}
/// <summary>
///  mapping class for standard output form
/// </summary>
public class Opportunity_Model
{
    #region Opportunity_Model
    public StringValue OpportunityID { get; set; }
    public StringValue Subject { get; set; }
    public StringValue Status { get; set; }
    public StringValue Stage { get; set; }
    public DateTimeValue Estimation { get; set; }
    public StringValue CurrencyID { get; set; }
    public DecimalValue Total { get; set; }
    public StringValue ClassID { get; set; }
    public StringValue Owner { get; set; }
    public IntValue ContactID { get; set; }
    public StringValue ContactDisplayName { get; set; }
    public StringValue BusinessAccount { get; set; }
    public DateTimeValue LastModifiedDateTime { get; set; }
    #endregion
}
/// <summary>
///  mapping class for standard output form
/// </summary>
public class SalesOrder_Model
{
    #region SalesOrder_Model
    public StringValue OrderType { get; set; }
    public StringValue OrderNbr { get; set; }
    public StringValue Status { get; set; }
    public DateTimeValue Date { get; set; }
    public StringValue CustomerID { get; set; }
    public StringValue CustomerName { get; set; }
    public DecimalValue OrderedQty { get; set; }
    public DecimalValue OrderTotal { get; set; }
    public StringValue CurrencyID { get; set; }
    public DateTimeValue LastModified { get; set; }
    public DateTimeValue ShipmentDate { get; set; }
    #endregion
}
/// <summary>
///  mapping class for standard output form
/// </summary>
public class Shipment_Model
{
    #region Shipments_Model
    public StringValue Type { get; set; }
    public StringValue ShipmentNbr { get; set; }
    public StringValue Status { get; set; }
    public DateTimeValue ShipmentDate { get; set; }
    public StringValue erID { get; set; }
    public StringValue erName { get; set; }
    public StringValue WarehouseID { get; set; }
    public DecimalValue ShippedQty { get; set; }
    public DecimalValue ShippedWeight { get; set; }
    public DateTimeValue CreatedDateTime { get; set; }
    public DateTimeValue LastModifiedDateTime { get; set; }
    #endregion
}
/// <summary>
///  mapping class for standard output form
/// </summary>
public class ShipmentDetail_Model
{
    #region ShipmentDetail_Model
    public StringValue OrderType { get; set; }
    public StringValue OrderNbr { get; set; }
    public StringValue InventoryID { get; set; }
    public StringValue WarehouseID { get; set; }
    public DecimalValue ShippedQty { get; set; }
    public DecimalValue OrderedQty { get; set; }
    public StringValue Description { get; set; }
    #endregion
}
#nullable disable

using Acumatica.RESTClient.ContractBasedApi.Model;

namespace RevisionTwoApp.RestApi.Entity_DTOs;
/// <summary>
///  Entity models of Acumatica Default EndPoints. 
///  These classes are custom models from the complete Models of Acumatica <see cref="RestClient"></see> .
///  These Models are used for ReST API communications with the Acumatica Instance. They are not for EntityFramework
///  Nuget Package - Acumatica Models all available with RestClient
/// </summary>

#region Bill
/// <summary>
///  mapping class for standard output form
/// </summary>
public class Bill
{
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
}
#endregion

#region BillDetail
/// <summary>
///  mapping class for standard output form
/// </summary>
public class BillDetail
{
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
}
#endregion

#region Case   
/// <summary>
///  mapping class for standard output form
/// </summary>
public class Case
{
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
    public DateTimeValue Date { get; set; }
    public DateTimeValue LastActivityDate { get; set; }
    public DateTimeValue LastModifiedDateTime { get; set; }
}
#endregion

#region Contact
/// <summary>
///  mapping class for standard output form
/// </summary>
public class Contact
{
    public BooleanValue Active { get; set; }
    public IntValue ContactID { get; set; }
    public StringValue DisplayName { get; set; }
    public StringValue BusinessAccount { get; set; }
    public StringValue JobTitle { get; set; }
    public StringValue Owner { get; set; }
    public StringValue CompanyName { get; set; }
    public StringValue ContactClass { get; set; }
    public StringValue Email { get; set; }
    public StringValue Phone1 { get; set; }
    public DateTimeValue LastModifiedDateTime { get; set; }
}
#endregion

#region Opportunity
/// <summary>
///  mapping class for standard output form
/// </summary>
public class Opportunity
{
    public StringValue OpportunityID { get; set; }
    public StringValue Subject { get; set; }
    public StringValue Status { get; set; }
    public StringValue Stage { get; set; }
    public StringValue CurrencyID { get; set; }
    public DecimalValue Total { get; set; }
    public StringValue ClassID { get; set; }
    public StringValue Owner { get; set; }
    public IntValue ContactID { get; set; }
    public StringValue ContactDisplayName { get; set; }
    public StringValue BusinessAccount { get; set; }
    public DateTimeValue LastModifiedDateTime { get; set; }
}
#endregion

#region SalesOrder
/// <summary>
///  mapping class for standard output form
/// </summary>
public class SalesOrder
{
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
}
#endregion

#region Shipments
/// <summary>
///  mapping class for standard output form
/// </summary>
public class Shipment
{
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
}
#endregion

#region ShipmentDetail
/// <summary>
///  mapping class for standard output form
/// </summary>
public class ShipmentDetail
{
    public StringValue OrderType { get; set; }
    public StringValue OrderNbr { get; set; }
    public StringValue InventoryID { get; set; }
    public StringValue WarehouseID { get; set; }
    public DecimalValue ShippedQty { get; set; }
    public DecimalValue OrderedQty { get; set; }
    public StringValue Description { get; set; }
}
#endregion

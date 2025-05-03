#nullable disable

using Acumatica.RESTClient.ContractBasedApi.Model;

namespace RevisionTwoApp.RestApi.Models.Default;

#region Bill_Model
/// <summary>
///  mapping class for standard output form
/// </summary>
public class Bill_Model
{
    #region Bill_Model
    /// <summary>
    /// Gets or sets the type of the bill.
    /// </summary>
    public StringValue Type { get; set; }

    /// <summary>
    /// Gets or sets the reference number of the bill.
    /// </summary>
    public StringValue ReferenceNbr { get; set; }

    /// <summary>
    /// Gets or sets the status of the bill.
    /// </summary>
    public StringValue Status { get; set; }

    /// <summary>
    /// Gets or sets the date of the bill.
    /// </summary>
    public DateTimeValue Date { get; set; }

    /// <summary>
    /// Gets or sets the post period of the bill.
    /// </summary>
    public StringValue PostPeriod { get; set; }

    /// <summary>
    /// Gets or sets the vendor associated with the bill.
    /// </summary>
    public StringValue Vendor { get; set; }

    /// <summary>
    /// Gets or sets the description of the bill.
    /// </summary>
    public StringValue Description { get; set; }

    /// <summary>
    /// Gets or sets the vendor reference of the bill.
    /// </summary>
    public StringValue VendorRef { get; set; }

    /// <summary>
    /// Gets or sets the amount of the bill.
    /// </summary>
    public DecimalValue Amount { get; set; }

    /// <summary>
    /// Gets or sets the currency ID of the bill.
    /// </summary>
    public StringValue CurrencyID { get; set; }

    /// <summary>
    /// Gets or sets the last modified date and time of the bill.
    /// </summary>
    public DateTimeValue LastModifiedDateTime { get; set; }

    #endregion
}
#endregion

#region BillDetail_Model
/// <summary>
///  mapping class for standard output form
/// </summary>
public class BillDetail_Model
{
    #region BillDetail_Model
    /// <summary>
    /// Gets or sets the branch associated with the bill detail.
    /// </summary>
    public StringValue Branch { get; set; }

    /// <summary>
    /// Gets or sets the inventory ID associated with the bill detail.
    /// </summary>
    public StringValue InventoryID { get; set; }

    /// <summary>
    /// Gets or sets the transaction description of the bill detail.
    /// </summary>
    public StringValue TransactionDescription { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the bill detail.
    /// </summary>
    public DecimalValue Qty { get; set; }

    /// <summary>
    /// Gets or sets the unit cost of the bill detail.
    /// </summary>
    public DecimalValue UnitCost { get; set; }

    /// <summary>
    /// Gets or sets the amount of the bill detail.
    /// </summary>
    public DecimalValue Amount { get; set; }

    /// <summary>
    /// Gets or sets the account associated with the bill detail.
    /// </summary>
    public StringValue Account { get; set; }

    /// <summary>
    /// Gets or sets the description of the bill detail.
    /// </summary>
    public StringValue Description { get; set; }

    /// <summary>
    /// Gets or sets the cost code associated with the bill detail.
    /// </summary>
    public StringValue CostCode { get; set; }

    /// <summary>
    /// Gets or sets the purchase order number associated with the bill detail.
    /// </summary>
    public StringValue POOrderNbr { get; set; }

    #endregion
}
#endregion

#region Case_Model   
/// <summary>
///  mapping class for standard output form
/// </summary>
public class Case_Model
{
    #region Case_Model   
    /// <summary>
    /// Gets or sets the unique identifier for the case.
    /// </summary>
    public StringValue CaseID { get; set; }

    /// <summary>
    /// Gets or sets the subject of the case.
    /// </summary>
    public StringValue Subject { get; set; }

    /// <summary>
    /// Gets or sets the business account associated with the case.
    /// </summary>
    public StringValue BusinessAccount { get; set; }

    /// <summary>
    /// Gets or sets the name of the business account associated with the case.
    /// </summary>
    public StringValue BusinessAccountName { get; set; }

    /// <summary>
    /// Gets or sets the status of the case.
    /// </summary>
    public StringValue Status { get; set; }

    /// <summary>
    /// Gets or sets the reason for the case.
    /// </summary>
    public StringValue Reason { get; set; }

    /// <summary>
    /// Gets or sets the severity of the case.
    /// </summary>
    public StringValue Severity { get; set; }

    /// <summary>
    /// Gets or sets the priority of the case.
    /// </summary>
    public StringValue Priority { get; set; }

    /// <summary>
    /// Gets or sets the owner of the case.
    /// </summary>
    public StringValue Owner { get; set; }

    /// <summary>
    /// Gets or sets the workgroup associated with the case.
    /// </summary>
    public StringValue Workgroup { get; set; }

    /// <summary>
    /// Gets or sets the class ID of the case.
    /// </summary>
    public StringValue ClassID { get; set; }

    /// <summary>
    /// Gets or sets the date the case was reported.
    /// </summary>
    public DateTimeValue DateReported { get; set; }

    /// <summary>
    /// Gets or sets the date of the last activity on the case.
    /// </summary>
    public DateTimeValue LastActivityDate { get; set; }

    /// <summary>
    /// Gets or sets the last modified date and time of the case.
    /// </summary>
    public DateTimeValue LastModifiedDateTime { get; set; }

    #endregion
}
#endregion

#region Contact_Model
/// <summary>
///  mapping class for standard output form
/// </summary>
public class Contact_Model
{
    #region Contact_Model
    /// <summary>
    /// Gets or sets a value indicating whether the contact is active.
    /// </summary>
    public BooleanValue Active { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the contact.
    /// </summary>
    public IntValue ContactID { get; set; }

    /// <summary>
    /// Gets or sets the class of the contact.
    /// </summary>
    public StringValue ContactClass { get; set; }

    /// <summary>
    /// Gets or sets the display name of the contact.
    /// </summary>
    public StringValue DisplayName { get; set; }

    /// <summary>
    /// Gets or sets the business account associated with the contact.
    /// </summary>
    public StringValue BusinessAccount { get; set; }

    /// <summary>
    /// Gets or sets the job title of the contact.
    /// </summary>
    public StringValue JobTitle { get; set; }

    /// <summary>
    /// Gets or sets the owner of the contact.
    /// </summary>
    public StringValue Owner { get; set; }

    /// <summary>
    /// Gets or sets the status of the contact.
    /// </summary>
    public StringValue Status { get; set; }

    /// <summary>
    /// Gets or sets the last modified date and time of the contact.
    /// </summary>
    public DateTimeValue LastModifiedDateTime { get; set; }

    #endregion
}
#endregion

#region Opportunity_Model
/// <summary>
///  Mapping class for standard output form
/// </summary>
public class Opportunity_Model
{
    #region Opportunity_Model
    /// <summary>
    /// Gets or sets the unique identifier for the opportunity.
    /// </summary>
    public StringValue OpportunityID { get; set; }

    /// <summary>
    /// Gets or sets the subject of the opportunity.
    /// </summary>
    public StringValue Subject { get; set; }

    /// <summary>
    /// Gets or sets the status of the opportunity.
    /// </summary>
    public StringValue Status { get; set; }

    /// <summary>
    /// Gets or sets the stage of the opportunity.
    /// </summary>
    public StringValue Stage { get; set; }

    /// <summary>
    /// Gets or sets the estimated date for the opportunity.
    /// </summary>
    public DateTimeValue Estimation { get; set; }

    /// <summary>
    /// Gets or sets the currency ID of the opportunity.
    /// </summary>
    public StringValue CurrencyID { get; set; }

    /// <summary>
    /// Gets or sets the total amount of the opportunity.
    /// </summary>
    public DecimalValue Total { get; set; }

    /// <summary>
    /// Gets or sets the class ID of the opportunity.
    /// </summary>
    public StringValue ClassID { get; set; }

    /// <summary>
    /// Gets or sets the owner of the opportunity.
    /// </summary>
    public StringValue Owner { get; set; }

    /// <summary>
    /// Gets or sets the contact ID associated with the opportunity.
    /// </summary>
    public IntValue ContactID { get; set; }

    /// <summary>
    /// Gets or sets the display name of the contact associated with the opportunity.
    /// </summary>
    public StringValue ContactDisplayName { get; set; }

    /// <summary>
    /// Gets or sets the business account associated with the opportunity.
    /// </summary>
    public StringValue BusinessAccount { get; set; }

    /// <summary>
    /// Gets or sets the last modified date and time of the opportunity.
    /// </summary>
    public DateTimeValue LastModifiedDateTime { get; set; }

    #endregion
}
#endregion

#region SalesOrder_Model
/// <summary>
///  Mapping class for standard output form
/// </summary>
public class SalesOrder_Model
{
    #region SalesOrder_Model
    /// <summary>
    /// Gets or sets the type of the sales order.
    /// </summary>
    public StringValue OrderType { get; set; }

    /// <summary>
    /// Gets or sets the number of the sales order.
    /// </summary>
    public StringValue OrderNbr { get; set; }

    /// <summary>
    /// Gets or sets the status of the sales order.
    /// </summary>
    public StringValue Status { get; set; }

    /// <summary>
    /// Gets or sets the date of the sales order.
    /// </summary>
    public DateTimeValue Date { get; set; }

    /// <summary>
    /// Gets or sets the customer ID associated with the sales order.
    /// </summary>
    public StringValue CustomerID { get; set; }

    /// <summary>
    /// Gets or sets the name of the customer associated with the sales order.
    /// </summary>
    public StringValue CustomerName { get; set; }

    /// <summary>
    /// Gets or sets the ordered quantity of the sales order.
    /// </summary>
    public DecimalValue OrderedQty { get; set; }

    /// <summary>
    /// Gets or sets the total amount of the sales order.
    /// </summary>
    public DecimalValue OrderTotal { get; set; }

    /// <summary>
    /// Gets or sets the currency ID of the sales order.
    /// </summary>
    public StringValue CurrencyID { get; set; }

    /// <summary>
    /// Gets or sets the last modified date and time of the sales order.
    /// </summary>
    public DateTimeValue LastModified { get; set; }

    /// <summary>
    /// Gets or sets the shipment date of the sales order.
    /// </summary>
    public DateTimeValue ShipmentDate { get; set; }

    #endregion
}
#endregion

#region Shipments_Model
/// <summary>
///  Mapping class for standard output form
/// </summary>
public class Shipment_Model
{
    #region Shipments_Model
    /// <summary>
    /// Gets or sets the type of the shipment.
    /// </summary>
    public StringValue Type { get; set; }

    /// <summary>
    /// Gets or sets the shipment number.
    /// </summary>
    public StringValue ShipmentNbr { get; set; }

    /// <summary>
    /// Gets or sets the status of the shipment.
    /// </summary>
    public StringValue Status { get; set; }

    /// <summary>
    /// Gets or sets the shipment date.
    /// </summary>
    public DateTimeValue ShipmentDate { get; set; }

    /// <summary>
    /// Gets or sets the erID of the shipment.
    /// </summary>
    public StringValue erID { get; set; }

    /// <summary>
    /// Gets or sets the erName of the shipment.
    /// </summary>
    public StringValue erName { get; set; }

    /// <summary>
    /// Gets or sets the warehouse ID of the shipment.
    /// </summary>
    public StringValue WarehouseID { get; set; }

    /// <summary>
    /// Gets or sets the shipped quantity.
    /// </summary>
    public DecimalValue ShippedQty { get; set; }

    /// <summary>
    /// Gets or sets the shipped weight.
    /// </summary>
    public DecimalValue ShippedWeight { get; set; }

    /// <summary>
    /// Gets or sets the created date and time of the shipment.
    /// </summary>
    public DateTimeValue CreatedDateTime { get; set; }

    /// <summary>
    /// Gets or sets the last modified date and time of the shipment.
    /// </summary>
    public DateTimeValue LastModifiedDateTime { get; set; }

    #endregion
}
#endregion

#region ShipmentDetail_Model
/// <summary>
///  Mapping class for standard output form
/// </summary>
public class ShipmentDetail_Model
{
    #region ShipmentDetail_Model
    /// <summary>
    /// Gets or sets the type of the order associated with the shipment detail.
    /// </summary>
    public StringValue OrderType { get; set; }

    /// <summary>
    /// Gets or sets the number of the order associated with the shipment detail.
    /// </summary>
    public StringValue OrderNbr { get; set; }

    /// <summary>
    /// Gets or sets the inventory ID associated with the shipment detail.
    /// </summary>
    public StringValue InventoryID { get; set; }

    /// <summary>
    /// Gets or sets the warehouse ID associated with the shipment detail.
    /// </summary>
    public StringValue WarehouseID { get; set; }

    /// <summary>
    /// Gets or sets the shipped quantity of the shipment detail.
    /// </summary>
    public DecimalValue ShippedQty { get; set; }

    /// <summary>
    /// Gets or sets the ordered quantity of the shipment detail.
    /// </summary>
    public DecimalValue OrderedQty { get; set; }

    /// <summary>
    /// Gets or sets the description of the shipment detail.
    /// </summary>
    public StringValue Description { get; set; }

    #endregion
}
#endregion
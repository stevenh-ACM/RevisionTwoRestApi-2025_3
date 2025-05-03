 #nullable disable

using Acumatica.RESTClient.ContractBasedApi.Model;

namespace RevisionTwoApp.RestApi.Entity_DTOs;

#region Bill
/// <summary>
///  mapping class for standard output form
/// </summary>
public class Bill
{
    #region Bill
    /// <summary>
    /// The type of the bill.
    /// </summary>
    public StringValue Type { get; set; }

    /// <summary>
    /// The reference number of the bill.
    /// </summary>
    public StringValue ReferenceNbr { get; set; }

    /// <summary>
    /// The status of the bill.
    /// </summary>
    public StringValue Status { get; set; }

    /// <summary>
    /// The date of the bill.
    /// </summary>
    public DateTimeValue Date { get; set; }

    /// <summary>
    /// The posting period of the bill.
    /// </summary>
    public StringValue PostPeriod { get; set; }

    /// <summary>
    /// The vendor associated with the bill.
    /// </summary>
    public StringValue Vendor { get; set; }

    /// <summary>
    /// The description of the bill.
    /// </summary>
    public StringValue Description { get; set; }

    /// <summary>
    /// The vendor reference for the bill.
    /// </summary>
    public StringValue VendorRef { get; set; }

    /// <summary>
    /// The total amount of the bill.
    /// </summary>
    public DecimalValue Amount { get; set; }

    /// <summary>
    /// The currency ID of the bill.
    /// </summary>
    public StringValue CurrencyID { get; set; }

    /// <summary>
    /// The last modified date and time of the bill.
    /// </summary>
    public DateTimeValue LastModifiedDateTime { get; set; }

    #endregion
}
#endregion

#region BillDetail
/// <summary>
///  Mapping class for standard output form
/// </summary>
public class BillDetail
{
    #region BillDetail
    /// <summary>
    /// The branch associated with the bill detail.
    /// </summary>
    public StringValue Branch { get; set; }

    /// <summary>
    /// The inventory ID associated with the bill detail.
    /// </summary>
    public StringValue InventoryID { get; set; }

    /// <summary>
    /// The transaction description for the bill detail.
    /// </summary>
    public StringValue TransactionDescription { get; set; }

    /// <summary>
    /// The quantity of the bill detail.
    /// </summary>
    public DecimalValue Qty { get; set; }

    /// <summary>
    /// The unit cost of the bill detail.
    /// </summary>
    public DecimalValue UnitCost { get; set; }

    /// <summary>
    /// The total amount of the bill detail.
    /// </summary>
    public DecimalValue Amount { get; set; }

    /// <summary>
    /// The account associated with the bill detail.
    /// </summary>
    public StringValue Account { get; set; }

    /// <summary>
    /// The description of the bill detail.
    /// </summary>
    public StringValue Description { get; set; }

    /// <summary>
    /// The cost code associated with the bill detail.
    /// </summary>
    public StringValue CostCode { get; set; }

    /// <summary>
    /// The purchase order number associated with the bill detail.
    /// </summary>
    public StringValue POOrderNbr { get; set; }

    #endregion
}
#endregion

#region Case   
/// <summary>
///  Mapping class for standard output form
/// </summary>
public class Case
{
    #region Case   
    /// <summary>
    /// The unique identifier for the case.
    /// </summary>
    public StringValue CaseID { get; set; }

    /// <summary>
    /// The subject of the case.
    /// </summary>
    public StringValue Subject { get; set; }

    /// <summary>
    /// The business account associated with the case.
    /// </summary>
    public StringValue BusinessAccount { get; set; }

    /// <summary>
    /// The name of the business account associated with the case.
    /// </summary>
    public StringValue BusinessAccountName { get; set; }

    /// <summary>
    /// The status of the case.
    /// </summary>
    public StringValue Status { get; set; }

    /// <summary>
    /// The reason for the case.
    /// </summary>
    public StringValue Reason { get; set; }

    /// <summary>
    /// The severity of the case.
    /// </summary>
    public StringValue Severity { get; set; }

    /// <summary>
    /// The priority of the case.
    /// </summary>
    public StringValue Priority { get; set; }

    /// <summary>
    /// The owner of the case.
    /// </summary>
    public StringValue Owner { get; set; }

    /// <summary>
    /// The workgroup associated with the case.
    /// </summary>
    public StringValue Workgroup { get; set; }

    /// <summary>
    /// The class ID of the case.
    /// </summary>
    public StringValue ClassID { get; set; }

    /// <summary>
    /// The date the case was created.
    /// </summary>
    public DateTimeValue Date { get; set; }

    /// <summary>
    /// The date of the last activity on the case.
    /// </summary>
    public DateTimeValue LastActivityDate { get; set; }

    /// <summary>
    /// The date and time the case was last modified.
    /// </summary>
    public DateTimeValue LastModifiedDateTime { get; set; }

    #endregion
}
#endregion

#region Contact
/// <summary>
///  Mapping class for standard output form
/// </summary>
public class Contact
{
    #region Contact
    /// <summary>
    /// Indicates whether the contact is active.
    /// </summary>
    public BooleanValue Active { get; set; }

    /// <summary>
    /// The unique identifier for the contact.
    /// </summary>
    public IntValue ContactID { get; set; }

    /// <summary>
    /// The display name of the contact.
    /// </summary>
    public StringValue DisplayName { get; set; }

    /// <summary>
    /// The business account associated with the contact.
    /// </summary>
    public StringValue BusinessAccount { get; set; }

    /// <summary>
    /// The job title of the contact.
    /// </summary>
    public StringValue JobTitle { get; set; }

    /// <summary>
    /// The owner of the contact.
    /// </summary>
    public StringValue Owner { get; set; }

    /// <summary>
    /// The company name associated with the contact.
    /// </summary>
    public StringValue CompanyName { get; set; }

    /// <summary>
    /// The contact class of the contact.
    /// </summary>
    public StringValue ContactClass { get; set; }

    /// <summary>
    /// The email address of the contact.
    /// </summary>
    public StringValue Email { get; set; }

    /// <summary>
    /// The primary phone number of the contact.
    /// </summary>
    public StringValue Phone1 { get; set; }

    /// <summary>
    /// The date and time the contact was last modified.
    /// </summary>
    public DateTimeValue LastModifiedDateTime { get; set; }

    #endregion
}
#endregion

#region Opportunity
/// <summary>
///  Mapping class for standard output form
/// </summary>
public class Opportunity
{
    #region Opportunity
    /// <summary>
    /// The unique identifier for the opportunity.
    /// </summary>
    public StringValue OpportunityID { get; set; }

    /// <summary>
    /// The subject of the opportunity.
    /// </summary>
    public StringValue Subject { get; set; }

    /// <summary>
    /// The status of the opportunity.
    /// </summary>
    public StringValue Status { get; set; }

    /// <summary>
    /// The stage of the opportunity.
    /// </summary>
    public StringValue Stage { get; set; }

    /// <summary>
    /// The currency ID of the opportunity.
    /// </summary>
    public StringValue CurrencyID { get; set; }

    /// <summary>
    /// The total amount of the opportunity.
    /// </summary>
    public DecimalValue Total { get; set; }

    /// <summary>
    /// The class ID of the opportunity.
    /// </summary>
    public StringValue ClassID { get; set; }

    /// <summary>
    /// The owner of the opportunity.
    /// </summary>
    public StringValue Owner { get; set; }

    /// <summary>
    /// The contact ID associated with the opportunity.
    /// </summary>
    public IntValue ContactID { get; set; }

    /// <summary>
    /// The display name of the contact associated with the opportunity.
    /// </summary>
    public StringValue ContactDisplayName { get; set; }

    /// <summary>
    /// The business account associated with the opportunity.
    /// </summary>
    public StringValue BusinessAccount { get; set; }

    /// <summary>
    /// The date and time the opportunity was last modified.
    /// </summary>
    public DateTimeValue LastModifiedDateTime { get; set; }

    #endregion
}
#endregion

#region SalesOrder
/// <summary>
///  Mapping class for standard output form
/// </summary>
public class SalesOrder
{
    #region SalesOrder
    /// <summary>
    /// The type of the sales order.
    /// </summary>
    public StringValue OrderType { get; set; }

    /// <summary>
    /// The order number of the sales order.
    /// </summary>
    public StringValue OrderNbr { get; set; }

    /// <summary>
    /// The status of the sales order.
    /// </summary>
    public StringValue Status { get; set; }

    /// <summary>
    /// The date of the sales order.
    /// </summary>
    public DateTimeValue Date { get; set; }

    /// <summary>
    /// The customer ID associated with the sales order.
    /// </summary>
    public StringValue CustomerID { get; set; }

    /// <summary>
    /// The name of the customer associated with the sales order.
    /// </summary>
    public StringValue CustomerName { get; set; }

    /// <summary>
    /// The quantity ordered in the sales order.
    /// </summary>
    public DecimalValue OrderedQty { get; set; }

    /// <summary>
    /// The total amount of the sales order.
    /// </summary>
    public DecimalValue OrderTotal { get; set; }

    /// <summary>
    /// The currency ID of the sales order.
    /// </summary>
    public StringValue CurrencyID { get; set; }

    /// <summary>
    /// The date and time the sales order was last modified.
    /// </summary>
    public DateTimeValue LastModified { get; set; }

    /// <summary>
    /// The shipment date of the sales order.
    /// </summary>
    public DateTimeValue ShipmentDate { get; set; }

    #endregion
}
#endregion

#region Shipments
/// <summary>
///  Mapping class for standard output form
/// </summary>
public class Shipment
{
    #region Shipments
    /// <summary>
    /// The type of the shipment.
    /// </summary>
    public StringValue Type { get; set; }

    /// <summary>
    /// The shipment number.
    /// </summary>
    public StringValue ShipmentNbr { get; set; }

    /// <summary>
    /// The status of the shipment.
    /// </summary>
    public StringValue Status { get; set; }

    /// <summary>
    /// The date of the shipment.
    /// </summary>
    public DateTimeValue ShipmentDate { get; set; }

    /// <summary>
    /// The erID associated with the shipment.
    /// </summary>
    public StringValue erID { get; set; }

    /// <summary>
    /// The erName associated with the shipment.
    /// </summary>
    public StringValue erName { get; set; }

    /// <summary>
    /// The warehouse ID where the shipment is stored.
    /// </summary>
    public StringValue WarehouseID { get; set; }

    /// <summary>
    /// The quantity shipped.
    /// </summary>
    public DecimalValue ShippedQty { get; set; }

    /// <summary>
    /// The weight of the shipment.
    /// </summary>
    public DecimalValue ShippedWeight { get; set; }

    /// <summary>
    /// The date and time the shipment was created.
    /// </summary>
    public DateTimeValue CreatedDateTime { get; set; }

    /// <summary>
    /// The date and time the shipment was last modified.
    /// </summary>
    public DateTimeValue LastModifiedDateTime { get; set; }

    #endregion
}
#endregion

#region ShipmentDetail
/// <summary>
///  Mapping class for standard output form
/// </summary>
public class ShipmentDetail
{
    /// <summary>
    /// The type of the order associated with the shipment detail.
    /// </summary>
    public StringValue OrderType { get; set; }

    /// <summary>
    /// The order number associated with the shipment detail.
    /// </summary>
    public StringValue OrderNbr { get; set; }

    /// <summary>
    /// The inventory ID associated with the shipment detail.
    /// </summary>
    public StringValue InventoryID { get; set; }

    /// <summary>
    /// The warehouse ID associated with the shipment detail.
    /// </summary>
    public StringValue WarehouseID { get; set; }

    /// <summary>
    /// The quantity shipped in the shipment detail.
    /// </summary>
    public DecimalValue ShippedQty { get; set; }

    /// <summary>
    /// The quantity ordered in the shipment detail.
    /// </summary>
    public DecimalValue OrderedQty { get; set; }

    /// <summary>
    /// The description of the shipment detail.
    /// </summary>
    public StringValue Description { get; set; }
}
#endregion

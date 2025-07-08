#nullable disable

using Acumatica.Default_24_200_001.Model;
using Acumatica.RESTClient.ContractBasedApi.Model;

using RevisionTwoApp.RestApi.Models.App;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace RevisionTwoApp.RestApi.DTOs.Conversions;

#region ConvertToAddr
/// <summary>
/// Converts an Address object to an Addr object.
/// </summary>
public class ConvertToAddr:Addr
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConvertToAddr"/> class.
    /// </summary>
    /// <param name="ad">The Address object to convert.</param>
    public ConvertToAddr(Address ad)
    {
        AddressLine1 = ad.AddressLine1.Value;
        AddressLine2 = ad.AddressLine1.Value;
        City = ad.AddressLine1.Value;
        Country = ad.AddressLine1.Value;
        PostalCode = ad.AddressLine1.Value;
        State = ad.AddressLine1.Value;
    }
}
#endregion

#region ConvertToAddress
/// <summary>
/// Converts an Addr object to an Address object.
/// </summary>
public class ConvertToAddress:Address
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConvertToAddress"/> class.
    /// </summary>
    /// <param name="ad">The Addr object to convert.</param>
    public ConvertToAddress(Addr ad)
    {
        AddressLine1 = ad.AddressLine1;
        AddressLine2 = ad.AddressLine2;
        City = ad.City;
        Country = ad.Country;
        PostalCode = ad.PostalCode;
        State = ad.State;
    }
}
#endregion

#region ConvertToAR_Bill
/// <summary>
/// Conversion to App model from Default model using inheritance 
/// </summary>
public class ConvertToAR_Bill:Bill_App
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConvertToAR_Bill"/> class.
    /// </summary>
    /// <param name="bill">The Bill object to convert.</param>
    public ConvertToAR_Bill(Bill bill)
    {
        Type = bill.Type.Value;
        ReferenceNbr = bill.ReferenceNbr.Value;
        Status = bill.Status.Value;
        Date = (DateTime)bill.Date.Value;
        PostPeriod = bill.PostPeriod.Value;
        Vendor = bill.Vendor.Value;
        Description = bill.Description.Value;
        VendorRef = bill.VendorRef.Value;
        Amount = (decimal)bill.Amount.Value;
        CurrencyID = bill.CurrencyID.Value;
        LastModifiedDateTime = (DateTime)bill.LastModifiedDateTime.Value;
    }
}
#endregion

#region ConvertToBill
/// <summary>
/// Conversion to Default model from App model using inheritance
/// </summary>
public class ConvertToBill:Bill
{
    public ConvertToBill(Bill_App bill)
    {
        Type = bill.Type;
        ReferenceNbr = bill.ReferenceNbr;
        Status = bill.Status;
        Date = bill.Date;
        PostPeriod = bill.PostPeriod;
        Vendor = bill.Vendor;
        Description = bill.Description;
        VendorRef = bill.VendorRef;
        Amount = bill.Amount;
        CurrencyID = bill.CurrencyID;
        LastModifiedDateTime = bill.LastModifiedDateTime;
    }
}
#endregion

#region ConvertToAR_BillDetail
/// <summary>
/// Conversion to App model from Default model using inheritance 
/// </summary>
public class ConvertToAR_BillDetail:BillDetail_App
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConvertToAR_BillDetail"/> class.
    /// </summary>
    /// <param name="bill">The BillDetail object to convert.</param>
    public ConvertToAR_BillDetail(BillDetail bill)
    {
        Branch = bill.Branch.Value;
        InventoryID = bill.InventoryID.Value;
        TransactionDescription = bill.TransactionDescription.Value;
        Qty = bill.Qty.Value;
        UnitCost = bill.UnitCost.Value;
        Amount = bill.Amount.Value;
        Account = bill.Account.Value;
        Description = bill.Description.Value;
        CostCode = bill.CostCode.Value;
        POOrderNbr = bill.POOrderNbr.Value;
    }
}
#endregion

#region ConvertToBillDetail
/// <summary>
/// Conversion to App model from Default model using inheritance 
/// </summary>
public class ConvertToBillDetail:BillDetail_App
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConvertToBillDetail"/> class.
    /// </summary>
    /// <param name="bill">The BillDetail_App object to convert.</param>
    public ConvertToBillDetail(BillDetail_App bill)
    {
        Branch = bill.Branch;
        InventoryID = bill.InventoryID;
        TransactionDescription = bill.TransactionDescription;
        Qty = bill.Qty;
        UnitCost = bill.UnitCost;
        Amount = bill.Amount;
        Account = bill.Account;
        Description = bill.Description;
        CostCode = bill.CostCode;
        POOrderNbr = bill.POOrderNbr;
    }
}
#endregion

#region ConverttoCRCase
/// <summary>
/// Conversion to App model from Default model using inheritance 
/// </summary>
public class ConvertToCase_App:Case_App
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConvertToCase_App"/> class.
    /// </summary>
    /// <param name="cr">The Case object to convert.</param>
    public ConvertToCase_App(Case cr)
    {
        CaseID = cr.CaseID.Value;
        Subject = cr.Subject.Value;
        BusinessAccount = cr.BusinessAccount.Value;
        BusinessAccountName = cr.BusinessAccountName.Value;
        Status = cr.Status.Value;
        Reason = cr.Reason.Value;
        Severity = cr.Severity.Value;
        Priority = cr.Priority.Value;
        Owner = cr.Owner.Value;
        Workgroup = cr.Workgroup.Value;
        ClassID = cr.ClassID.Value;
        DateReported = (DateTime)cr.DateReported.Value;
        LastActivityDate = (DateTime)cr.LastActivityDate.Value;
        LastModifiedDateTime = (DateTime)cr.LastModifiedDateTime.Value;
    }
}
#endregion

#region ConverttoCase
/// <summary>
/// Conversion to Default model from App model using inheritance
/// </summary>
public class ConvertToCase:Case
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConvertToCase"/> class.
    /// </summary>
    /// <param name="cr">The Case_App object to convert.</param>
    public ConvertToCase(Case_App cr)
    {
        CaseID = cr.CaseID;
        Subject = cr.Subject;
        BusinessAccount = cr.BusinessAccount;
        BusinessAccountName = cr.BusinessAccountName;
        Status = cr.Status;
        Reason = cr.Reason;
        Severity = cr.Severity;
        Priority = cr.Priority;
        Owner = cr.Owner;
        Workgroup = cr.Workgroup;
        ClassID = cr.ClassID;
        DateReported = cr.DateReported;
        LastActivityDate = cr.LastActivityDate;
        LastModifiedDateTime = cr.LastModifiedDateTime;
    }
}
#endregion

#region ConvertToCO
/// <summary>
/// Conversion to App model from Default model using inheritance 
/// </summary>
public class ConvertToCO:Contact_App
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConvertToCO"/> class.
    /// </summary>
    /// <param name="co">The Contact object to convert.</param>
    public ConvertToCO(Contact co)
    {
        Active = (bool)co.Active.Value;
        ContactID = co.ContactID.Value;
        DisplayName = co.DisplayName.Value;
        ContactClass = co.ContactClass.Value;
        BusinessAccount = co.BusinessAccount.Value;
        JobTitle = co.JobTitle.Value;
        Owner = co.Owner.Value;
        Status = co.Status.Value;
        LastModifiedDateTime = (DateTime)co.LastModifiedDateTime.Value;
    }
}
#endregion

#region ConverttoContact
/// <summary>
/// Conversion to Default model from App model using inheritance
/// </summary>
public class ConvertToContact:Contact
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConvertToContact"/> class.
    /// </summary>
    /// <param name="co">The Contact_App object to convert.</param>
    public ConvertToContact(Contact_App co)
    {
        Active = (bool)co.Active;
        ContactID = co.ContactID.Value;
        DisplayName = co.DisplayName;
        ContactClass = co.ContactClass;
        BusinessAccount = co.BusinessAccount;
        JobTitle = co.JobTitle;
        Owner = co.Owner;
        Status = co.Status;
        LastModifiedDateTime = co.LastModifiedDateTime.Value;
    }
}
#endregion

#region ConvertToCU

public class ConvertToCU: Customer_App
{
    public ConvertToCU(Customer cu)
    {
        CustomerID = cu.CustomerID.Value;
        CustomerName = cu.CustomerName.Value;
    }
}
#endregion

#region ConvertToOP
/// <summary>
/// Conversion to App model from Default model using inheritance 
/// </summary>
public class ConvertToOP:Opportunity_App
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConvertToOP"/> class.
    /// </summary>
    /// <param name="op">The Opportunity object to convert.</param>
    public ConvertToOP(Opportunity op)
    {
        OpportunityID = op.OpportunityID.Value;
        Subject = op.Subject.Value;
        Status = op.Status.Value;
        Stage = op.Stage.Value;
        CurrencyID = op.CurrencyID.Value;
        Total = (decimal)op.Total.Value;
        ClassID = op.ClassID.Value;
        Owner = op.Owner.Value;
        ContactDisplayName = op.ContactDisplayName.Value;
        BusinessAccount = op.BusinessAccount.Value;
        LastModifiedDateTime = (DateTime)op.LastModifiedDateTime.Value;
    }
}
#endregion

#region ConvertToOpportunity
/// <summary>
/// Conversion to Default model from App model using inheritance
/// </summary>
public class ConvertToOpportunity:Opportunity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConvertToOpportunity"/> class.
    /// </summary>
    /// <param name="op">The Opportunity_App object to convert.</param>
    public ConvertToOpportunity(Opportunity_App op)
    {
        OpportunityID = op.OpportunityID;
        Subject = op.Subject;
        Status = op.Status;
        Stage = op.Stage;
        CurrencyID = op.CurrencyID;
        Total = op.Total;
        ClassID = op.ClassID;
        Owner = op.Owner;
        ContactDisplayName = op.ContactDisplayName;
        BusinessAccount = op.BusinessAccount;
        LastModifiedDateTime = op.LastModifiedDateTime;
    }
}
#endregion

#region ConvertToSO
/// <summary>
/// Conversion to App model from Default model using inheritance 
/// </summary>
public class ConvertToSO:SalesOrder_App
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConvertToSO"/> class.
    /// </summary>
    /// <param name="so">The SalesOrder object to convert.</param>
    /// <param name="ba">The BusinessAccount object to use for customer name.</param>
    /// <param name="sp">The DateTimeValue object representing the shipment date.</param>
    public ConvertToSO(SalesOrder so,BusinessAccount ba,DateTimeValue sp)
    {
        OrderType = so.OrderType.Value;
        OrderNbr = so.OrderNbr.Value;
        Status = so.Status.Value;
        Date = (DateTime)so.Date.Value;
        CustomerID = so.CustomerID.Value;
        CustomerName = ba.Name.Value;
        OrderedQty = (decimal)so.OrderedQty.Value;
        OrderTotal = (decimal)so.OrderTotal.Value;
        CurrencyID = so.CurrencyID.Value;
        ShipmentDate = (DateTime)sp.Value;
        LastModified = (DateTime)so.LastModified.Value;
    }
}
#endregion

#region ConverttoSalesOrder
/// <summary>
/// Conversion to Default model from App model using inheritance
/// </summary>
public class ConvertToSalesOrder:SalesOrder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConvertToSalesOrder"/> class.
    /// </summary>
    /// <param name="so">The SalesOrder_App object to convert.</param>
    public ConvertToSalesOrder(SalesOrder_App so)
    {
        OrderType = so.OrderType;
        OrderNbr = so.OrderNbr;
        Status = so.Status;
        Date = so.Date;
        CustomerID = so.CustomerID;
        OrderedQty = so.OrderedQty;
        OrderTotal = so.OrderTotal;
        CurrencyID = so.CurrencyID;
        LastModified = so.LastModified;
    }
}
#endregion

#region ConvertToSP

/// <summary>
/// Conversion to App model from Default model using inheritance 
/// </summary>
public class ConvertToSP:Shipment_App
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConvertToSP"/> class.
    /// </summary>
    /// <param name="sp">The Shipment object to convert.</param>
    public ConvertToSP(Shipment sp)
    {
        @Type = sp.Type.Value;
        ShipmentNbr = sp.ShipmentNbr.Value;
        Status = sp.Status.Value;
        ShipmentDate = (DateTime)sp.ShipmentDate.Value;
        CustomerID = sp.CustomerID.Value;
        WarehouseID = sp.WarehouseID.Value;
        ShippedQty = (decimal)sp.ShippedQty.Value;
        ShippedWeight = (decimal)sp.ShippedWeight.Value;
        CreatedDateTime = (DateTime)sp.CreatedDateTime.Value;
        LastModifiedDateTime = (DateTime)sp.LastModifiedDateTime.Value;
    }
}
#endregion

#region ConvertToShipment
/// <summary>
/// Conversion to Default model from App model using inheritance
/// </summary>
public class ConvertToShipment:Shipment
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConvertToShipment"/> class.
    /// </summary>
    /// <param name="sp">The Shipment_App object to convert.</param>
    public ConvertToShipment(Shipment_App sp)
    {
        @Type = sp.Type;
        ShipmentNbr = sp.ShipmentNbr;
        Status = sp.Status;
        ShipmentDate = sp.ShipmentDate;
        CustomerID = sp.CustomerID;
        WarehouseID = sp.WarehouseID;
        ShippedQty = sp.ShippedQty;
        ShippedWeight = sp.ShippedWeight;
        CreatedDateTime = sp.CreatedDateTime;
        LastModifiedDateTime = sp.LastModifiedDateTime;
    }
}
#endregion

#region ConvertToSPDetail
/// <summary>
/// Conversion to App model from Default model using inheritance 
/// </summary>
public class ConvertToSPDetail:ShipmentDetail_App
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConvertToSPDetail"/> class.
    /// </summary>
    /// <param name="sp">The ShipmentDetail object to convert.</param>
    public ConvertToSPDetail(ShipmentDetail sp)
    {
        OrderType = sp.OrderType.Value;
        OrderNbr = sp.OrderNbr.Value;
        InventoryID = sp.InventoryID.Value;
        WarehouseID = sp.WarehouseID.Value;
        ShippedQty = sp.ShippedQty.Value;
        OrderedQty = sp.OrderedQty.Value;
        Description = sp.Description.Value;
    }
}
#endregion

#region ConvertToShipmentDetail
/// <summary>
/// Conversion to Default model from App model using inheritance
/// </summary>
public class ConvertToShipmentDetail:ShipmentDetail
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConvertToShipmentDetail"/> class.
    /// </summary>
    /// <param name="sp">The ShipmentDetail_App object to convert.</param>
    public ConvertToShipmentDetail(ShipmentDetail_App sp)
    {
        OrderType = sp.OrderType;
        OrderNbr = sp.OrderNbr;
        InventoryID = sp.InventoryID;
        WarehouseID = sp.WarehouseID;
        ShippedQty = sp.ShippedQty;
        OrderedQty = sp.OrderedQty;
        Description = sp.Description;
    }
}
#endregion
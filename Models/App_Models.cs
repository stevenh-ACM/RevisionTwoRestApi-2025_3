#nullable enable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevisionTwoApp.RestApi.Models.App;

#region Addr
/// <summary>
///  mapping class for standard output form
/// </summary>
public class Addr
{
    #region Addr
    /// <summary>
    /// Gets or sets the unique identifier for the bill detail.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the first line of the address, such as street address.
    /// </summary>
    public string? AddressLine1 { get; set; }

    /// <summary>
    /// Gets or sets the second line of the address, such as apartment, suite, or building.
    /// </summary>
    public string? AddressLine2 { get; set; }

    /// <summary>
    /// Gets or sets the city of the address.
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// Gets or sets the country of the address.
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// Gets or sets the postal code of the address.
    /// </summary>
    public string? PostalCode { get; set; }

    /// <summary>
    /// Gets or sets the state or province of the address.
    /// </summary>
    public string? State { get; set; }
    #endregion
}
#endregion

#region Bills_App
/// <summary>
///  mapping class for standard output form
/// </summary>
public class Bill_App
{
    #region Bills_App

    /// <summary>
    /// Gets or sets the unique identifier for the bill.
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the type of the bill.
    /// </summary>
    [StringLength(128)]
    public string? Type { get; set; }

    /// <summary>
    /// Gets or sets the reference number of the bill.
    /// </summary>
    [Display(Name = "Ref Nbr")]
    [StringLength(128)]
    public string? ReferenceNbr { get; set; }

    /// <summary>
    /// Gets or sets the status of the bill.
    /// </summary>
    [StringLength(128)]
    public string? Status { get; set; }

    /// <summary>
    /// Gets or sets the date of the bill.
    /// </summary>
    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",ApplyFormatInEditMode = true)]
    public DateTime? Date { get; set; }

    /// <summary>
    /// Gets or sets the post period of the bill.
    /// </summary>
    [Display(Name = "Post Period")]
    [StringLength(128)]
    public string? PostPeriod { get; set; }

    /// <summary>
    /// Gets or sets the vendor associated with the bill.
    /// </summary>
    [StringLength(128)]
    public string? Vendor { get; set; }

    /// <summary>
    /// Gets or sets the description of the bill.
    /// </summary>
    [StringLength(128)]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the vendor reference number of the bill.
    /// </summary>
    [Display(Name = "Vendor Ref Nbr")]
    [StringLength(128)]
    public string? VendorRef { get; set; }

    /// <summary>
    /// Gets or sets the amount of the bill.
    /// </summary>
    [Column(TypeName = "decimal(18, 6)")]
    [DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:C2}")]
    public decimal? Amount { get; set; }

    /// <summary>
    /// Gets or sets the currency identifier for the bill.
    /// </summary>
    [Display(Name = "Curr Denom")]
    [StringLength(128)]
    public string? CurrencyID { get; set; }

    /// <summary>
    /// Gets or sets the last modified date and time of the bill.
    /// </summary>
    [Display(Name = "Last Modified")]
    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",ApplyFormatInEditMode = true)]
    public DateTime? LastModifiedDateTime { get; set; }

    #endregion
}
#endregion

#region BillDetail_App
/// <summary>
///  mapping class for standard output form
/// </summary>
public class BillDetail_App
{
    #region BillDetail_App

    /// <summary>
    /// Gets or sets the unique identifier for the bill detail.
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the branch associated with the bill detail.
    /// </summary>
    [StringLength(128)]
    public string? Branch { get; set; }

    /// <summary>
    /// Gets or sets the inventory ID associated with the bill detail.
    /// </summary>
    [Display(Name = "Inventory ID")]
    [StringLength(128)]
    public string? InventoryID { get; set; }

    /// <summary>
    /// Gets or sets the transaction description for the bill detail.
    /// </summary>
    [Display(Name = "Trans Desc")]
    [StringLength(128)]
    public string? TransactionDescription { get; set; }

    /// <summary>
    /// Gets or sets the quantity for the bill detail.
    /// </summary>
    [Display(Name = "Quantity")]
    [Column(TypeName = "decimal(18, 6)")]
    [DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:N0}")]
    public decimal? Qty { get; set; }

    /// <summary>
    /// Gets or sets the unit cost for the bill detail.
    /// </summary>
    [Display(Name = "Unit Cost")]
    [Column(TypeName = "decimal(18, 6)")]
    [DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:C2}")]
    public decimal? UnitCost { get; set; }

    /// <summary>
    /// Gets or sets the amount for the bill detail.
    /// </summary>
    [Column(TypeName = "decimal(18, 6)")]
    [DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:C2}")]
    public decimal? Amount { get; set; }

    /// <summary>
    /// Gets or sets the account associated with the bill detail.
    /// </summary>
    [StringLength(128)]
    public string? Account { get; set; }

    /// <summary>
    /// Gets or sets the description for the bill detail.
    /// </summary>
    [StringLength(128)]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the cost code for the bill detail.
    /// </summary>
    [Display(Name = "Cost Code")]
    [StringLength(128)]
    public string? CostCode { get; set; }

    /// <summary>
    /// Gets or sets the purchase order number for the bill detail.
    /// </summary>
    [Display(Name = "PO OrderNbr")]
    [StringLength(128)]
    public string? POOrderNbr { get; set; }

    #endregion
}
#endregion

#region Case_App
/// <summary>
///  mapping class for standard output form
/// </summary>
public class Case_App
{
    #region Case_App

    /// <summary>
    /// Gets or sets the unique identifier for the case.
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the case ID.
    /// </summary>
    [StringLength(128)]
    public string? CaseID { get; set; }

    /// <summary>
    /// Gets or sets the subject of the case.
    /// </summary>
    [StringLength(128)]
    public string? Subject { get; set; }

    /// <summary>
    /// Gets or sets the business account ID associated with the case.
    /// </summary>
    [Display(Name = "Account ID")]
    [StringLength(128)]
    public string? BusinessAccount { get; set; }

    /// <summary>
    /// Gets or sets the name of the business account associated with the case.
    /// </summary>
    [StringLength(128)]
    [Display(Name = "Account Name")]
    public string? BusinessAccountName { get; set; }

    /// <summary>
    /// Gets or sets the status of the case.
    /// </summary>
    [StringLength(128)]
    public string? Status { get; set; }

    /// <summary>
    /// Gets or sets the reason for the case.
    /// </summary>
    [StringLength(128)]
    public string? Reason { get; set; }

    /// <summary>
    /// Gets or sets the severity of the case.
    /// </summary>
    [StringLength(128)]
    public string? Severity { get; set; }

    /// <summary>
    /// Gets or sets the priority of the case.
    /// </summary>
    [StringLength(128)]
    public string? Priority { get; set; }

    /// <summary>
    /// Gets or sets the owner of the case.
    /// </summary>
    [StringLength(128)]
    public string? Owner { get; set; }

    /// <summary>
    /// Gets or sets the workgroup associated with the case.
    /// </summary>
    [StringLength(128)]
    public string? Workgroup { get; set; }

    /// <summary>
    /// Gets or sets the class ID of the case.
    /// </summary>
    [Display(Name = "Class")]
    [StringLength(128)]
    public string? ClassID { get; set; }

    /// <summary>
    /// Gets or sets the date the case was reported.
    /// </summary>
    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",ApplyFormatInEditMode = true)]
    public DateTime? DateReported { get; set; }

    /// <summary>
    /// Gets or sets the date of the last activity on the case.
    /// </summary>
    [Display(Name = "Last Activity")]
    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",ApplyFormatInEditMode = true)]
    public DateTime? LastActivityDate { get; set; }

    /// <summary>
    /// Gets or sets the last modified date and time of the case.
    /// </summary>
    [Display(Name = "Last Modified")]
    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",ApplyFormatInEditMode = true)]
    public DateTime? LastModifiedDateTime { get; set; }

    #endregion
}
#endregion

#region Contact_App
/// <summary>
///  mapping class for standard output form
/// </summary>
public class Contact_App
{
    #region Contact_App

    /// <summary>
    /// Gets or sets the unique identifier for the contact.
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the contact is active.
    /// </summary>
    public bool Active { get; set; }

    /// <summary>
    /// Gets or sets the contact ID.
    /// </summary>
    [Display(Name = "Contact ID")]
    [StringLength(128)]
    public int? ContactID { get; set; }

    /// <summary>
    /// Gets or sets the display name of the contact.
    /// </summary>
    [StringLength(128)]
    [Display(Name = "Contact Name")]
    public string? DisplayName { get; set; }

    /// <summary>
    /// Gets or sets the class of the contact.
    /// </summary>
    [StringLength(128)]
    [Display(Name = "Class")]
    public string? ContactClass { get; set; }

    /// <summary>
    /// Gets or sets the business account associated with the contact.
    /// </summary>
    [StringLength(128)]
    [Display(Name = "Account ID")]
    public string? BusinessAccount { get; set; }

    /// <summary>
    /// Gets or sets the job title of the contact.
    /// </summary>
    [Display(Name = "Job Title")]
    [StringLength(128)]
    public string? JobTitle { get; set; }

    /// <summary>
    /// Gets or sets the owner of the contact.
    /// </summary>
    [StringLength(128)]
    public string? Owner { get; set; }

    /// <summary>
    /// Gets or sets the status of the contact.
    /// </summary>
    [StringLength(128)]
    [Display(Name = "Status")]
    public string? Status { get; set; }

    /// <summary>
    /// Gets or sets the last modified date and time of the contact.
    /// </summary>
        [Display(Name = "Last Modified")]
    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",ApplyFormatInEditMode = true)]
    public DateTime? LastModifiedDateTime { get; set; }
    #endregion
}
#endregion

#region Opportunity_App
/// <summary>
///  mapping class for standard output form
/// </summary>
public class Opportunity_App
{
    #region Opportunity_App

    /// <summary>
    /// Gets or sets the unique identifier for the opportunity.
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the opportunity ID.
    /// </summary>
    [Display(Name = "Oppty ID")]
    [StringLength(128)]
    public string? OpportunityID { get; set; }

    /// <summary>
    /// Gets or sets the subject of the opportunity.
    /// </summary>
    [StringLength(128)]
    public string? Subject { get; set; }

    /// <summary>
    /// Gets or sets the status of the opportunity.
    /// </summary>
    [StringLength(128)]
    public string? Status { get; set; }

    /// <summary>
    /// Gets or sets the stage of the opportunity.
    /// </summary>
    [StringLength(128)]
    public string? Stage { get; set; }

    /// <summary>
    /// Gets or sets the estimated close date of the opportunity.
    /// </summary>
    [Display(Name = "Close Date")]
    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",ApplyFormatInEditMode = true)]
    public DateTime? Estimation { get; set; }

    /// <summary>
    /// Gets or sets the currency identifier for the opportunity.
    /// </summary>
    [Display(Name = "Curr Denom")]
    [StringLength(128)]
    public string? CurrencyID { get; set; }

    /// <summary>
    /// Gets or sets the total amount of the opportunity.
    /// </summary>
    [DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:C2}")]
    public decimal? Total { get; set; }

    /// <summary>
    /// Gets or sets the class ID of the opportunity.
    /// </summary>
    [Display(Name = "Class")]
    [StringLength(128)]
    public string? ClassID { get; set; }

    /// <summary>
    /// Gets or sets the owner of the opportunity.
    /// </summary>
    [StringLength(128)]
    public string? Owner { get; set; }

    /// <summary>
    /// Gets or sets the contact ID associated with the opportunity.
    /// </summary>
    [Display(Name = "Contact ID")]
    [StringLength(128)]
    public int? ContactID { get; set; }

    /// <summary>
    /// Gets or sets the display name of the contact associated with the opportunity.
    /// </summary>
    [Display(Name = "Contact Name")]
    [StringLength(20)]
    public string? ContactDisplayName { get; set; }

    /// <summary>
    /// Gets or sets the business account associated with the opportunity.
    /// </summary>
    [Display(Name = "Account ID")]
    [StringLength(128)]
    public string? BusinessAccount { get; set; }

    /// <summary>
    /// Gets or sets the last modified date and time of the opportunity.
    /// </summary>
    [Display(Name = "Last Modified")]
    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",ApplyFormatInEditMode = true)]
    public DateTime? LastModifiedDateTime { get; set; }
    #endregion
}
#endregion

#region SalesOrder_App
/// <summary>
/// Represents a sales order in the application.
/// </summary>
public class SalesOrder_App
{
    #region SalesOrder_App
    /// <summary>
    /// Gets or sets the unique identifier for the sales order.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the type of the order.
    /// </summary>
    public string? OrderType { get; set; }

    /// <summary>
    /// Gets or sets the order number.
    /// </summary>
    public string? OrderNbr { get; set; }

    /// <summary>
    /// Gets or sets the status of the order.
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Gets or sets the date of the order.
    /// </summary>
    [DisplayFormat(DataFormatString = "{0:d}")]
    public DateTime? Date { get; set; }

    /// <summary>
    /// Gets or sets the customer ID associated with the order.
    /// </summary>
    public string? CustomerID { get; set; }

    /// <summary>
    /// Gets or sets the name of the customer associated with the order.
    /// </summary>
    public string? CustomerName { get; set; }

    /// <summary>
    /// Gets or sets the quantity of items ordered.
    /// </summary>
    public decimal? OrderedQty { get; set; }

    /// <summary>
    /// Gets or sets the total amount of the order.
    /// </summary>
    public decimal? OrderTotal { get; set; }

    /// <summary>
    /// Gets or sets the currency identifier for the order.
    /// </summary>
    public string? CurrencyID { get; set; }

    /// <summary>
    /// Gets or sets the shipment date for the order.
    /// </summary>
    [DisplayFormat(DataFormatString = "{0:d}")]
    public DateTime? ShipmentDate { get; set; }

    /// <summary>
    /// Gets or sets the last modified date of the order.
    /// </summary>
    [DisplayFormat(DataFormatString = "{0:d}")]
    public DateTime? LastModified { get; set; }
    #endregion
}
#endregion

#region Shipment_App
/// <summary>
/// Represents a shipment in the application.
/// </summary>
public class Shipment_App
{
    #region Shipments_App

    /// <summary>
    /// Gets or sets the unique identifier for the shipment.
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the type of the shipment.
    /// </summary>
    [StringLength(128)]
    public string? Type { get; set; }

    /// <summary>
    /// Gets or sets the shipment number.
    /// </summary>
    [Display(Name = "Shipment Nbr")]
    [StringLength(128)]
    public string? ShipmentNbr { get; set; }

    /// <summary>
    /// Gets or sets the status of the shipment.
    /// </summary>
    [StringLength(128)]
    public string? Status { get; set; }

    /// <summary>
    /// Gets or sets the shipment date.
    /// </summary>
    [Display(Name = "Ship Date")]
    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",ApplyFormatInEditMode = true)]
    public DateTime? ShipmentDate { get; set; }

    /// <summary>
    /// Gets or sets the customer ID associated with the shipment.
    /// </summary>
    [Display(Name = "Customer ID")]
    [StringLength(128)]
    public string? CustomerID { get; set; }

    /// <summary>
    /// Gets or sets the customer name associated with the shipment.
    /// </summary>
    [Display(Name = "Customer Name")]
    [StringLength(128)]
    public string? CustomerName { get; set; }

    /// <summary>
    /// Gets or sets the warehouse ID associated with the shipment.
    /// </summary>
    [Display(Name = "Warehouse ID")]
    [StringLength(128)]
    public string? WarehouseID { get; set; }

    /// <summary>
    /// Gets or sets the quantity shipped.
    /// </summary>
    [Display(Name = "Qty Shipped")]
    [Column(TypeName = "decimal(18, 6)")]
    [DisplayFormat(DataFormatString = "{0:N0}")]
    public decimal? ShippedQty { get; set; }

    /// <summary>
    /// Gets or sets the weight of the shipment.
    /// </summary>
    [Display(Name = "Shipment Weight")]
    [Column(TypeName = "decimal(18, 6)")]
    [DisplayFormat(DataFormatString = "{0:N0}")]
    public decimal? ShippedWeight { get; set; }

    /// <summary>
    /// Gets or sets the date the shipment was created.
    /// </summary>
    [Display(Name = "Date Created")]
    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",ApplyFormatInEditMode = true)]
    public DateTime? CreatedDateTime { get; set; }

    /// <summary>
    /// Gets or sets the last modified date and time of the shipment.
    /// </summary>
    [Display(Name = "Last Modified")]
    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",ApplyFormatInEditMode = true)]
    public DateTime? LastModifiedDateTime { get; set; }
    #endregion
}
#endregion

#region ShipmentDetail_App  
/// <summary>  
/// Represents the details of a shipment in the application.  
/// </summary>  
public class ShipmentDetail_App
{
    #region ShipmentDetail_App  

    /// <summary>  
    /// Gets or sets the unique identifier for the shipment detail.  
    /// </summary>  
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>  
    /// Gets or sets the type of the order associated with the shipment detail.  
    /// </summary>  
    [Display(Name = "Order Type")]
    [StringLength(128)]
    public string? OrderType { get; set; }

    /// <summary>  
    /// Gets or sets the order number associated with the shipment detail.  
    /// </summary>  
    [StringLength(128)]
    public string? OrderNbr { get; set; }

    /// <summary>  
    /// Gets or sets the inventory ID associated with the shipment detail.  
    /// </summary>  
    [Display(Name = "Inventory ID")]
    [StringLength(128)]
    public string? InventoryID { get; set; }

    /// <summary>  
    /// Gets or sets the warehouse ID associated with the shipment detail.  
    /// </summary>  
    [Display(Name = "Warehouse ID")]
    [StringLength(128)]
    public string? WarehouseID { get; set; }

    /// <summary>  
    /// Gets or sets the quantity shipped in the shipment detail.  
    /// </summary>  
    [Display(Name = "Qty Shipped")]
    [Column(TypeName = "decimal(18, 6)")]
    [DisplayFormat(DataFormatString = "{0:N0}")]
    public decimal? ShippedQty { get; set; }

    /// <summary>  
    /// Gets or sets the quantity ordered in the shipment detail.  
    /// </summary>  
    [Display(Name = "Qty Ordered")]
    [Column(TypeName = "decimal(18, 6)")]
    [DisplayFormat(DataFormatString = "{0:N0}")]
    public decimal? OrderedQty { get; set; }

    /// <summary>  
    /// Gets or sets the description of the shipment detail.  
    /// </summary>  
    [StringLength(128)]
    public string? Description { get; set; }

    #endregion
}
#endregion

#region Address_App
/// <summary>
///  mapping class for standard output form
/// </summary>
public class Address_App
{
    #region Address_App
    /// <summary>
    /// Gets or sets the unique identifier for the address.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the first line of the address, such as street address.
    /// </summary>
    [Display(Name = "Street Addr")]
    [StringLength(128)]
    public string? AddressLine1 { get; set; }   

    /// <summary>
    /// Gets or sets the second line of the address, such as apartment, suite, or building.
    /// </summary>
    [Display(Name = "STE, Bldg")]
    [StringLength(128)]
    public string? AddressLine2 { get; set; }

    /// <summary>
    /// Gets or sets the city of the address.
    /// </summary>
    [StringLength(128)]
    public string? City { get; set; }

    /// <summary>
    /// Gets or sets the country of the address.
    /// </summary>
    [StringLength(128)]
    public string? Country { get; set; }

    /// <summary>
    /// Gets or sets the postal code of the address.
    /// </summary>
    [Display(Name = "Zip Code")]
    [DataType(DataType.PostalCode)]
    public string? PostalCode { get; set; }

    /// <summary>
    /// Gets or sets the state or province of the address.
    /// </summary>
    [StringLength(128)]
    public string? State { get; set; }

    #endregion  
}
#endregion
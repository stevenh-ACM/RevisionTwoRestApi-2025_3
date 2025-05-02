#nullable enable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevisionTwoApp.RestApi.Models.App;

/// <summary>
///  Models for RevisionTwoApp.RestApi that use standard C# types. 
///  Generally used for input/output of Razor Pages.
///
/// Custom classes with std C# types that map to the Default classes
/// <see cref="Conversion"/>
/// </summary>
/// 

#region Addr
/// <summary>
///  mapping class for standard output form
/// </summary>
public class Addr
{
    #region Addr 

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Display(Name = "Street Addr")]
    [StringLength(50)]
    public string? AddressLine1 { get; set; }

    [StringLength(50)]
    [Display(Name = "STE, Bldg")]
    public string? AddressLine2 { get; set; }

    [StringLength(50)]
    public string? City { get; set; }

    [StringLength(50)]
    public string? Country { get; set; }

    [Display(Name = "Zip Code")]
    [DataType(DataType.PostalCode)]
    public string? PostalCode { get; set; }

    [StringLength(50)]
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

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [StringLength(128)]
    public string? Type { get; set; }

    [Display(Name = "Ref Nbr")]
    [StringLength(128)]
    public string? ReferenceNbr { get; set; }

    [StringLength(128)]
    public string? Status { get; set; }

    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",ApplyFormatInEditMode = true)]
    public DateTime? Date { get; set; }

    [Display(Name = "Post Period")]
    [StringLength(128)]
    public string? PostPeriod { get; set; }

    [StringLength(128)]
    public string? Vendor { get; set; }

    [StringLength(128)]
    public string? Description { get; set; }

    [Display(Name = "Vendor Ref Nbr")]
    [StringLength(128)]
    public string? VendorRef { get; set; }

    [Column(TypeName = "decimal(18, 6)")]
    [DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:C2}")]
    public decimal? Amount { get; set; }

    [Display(Name = "Curr Denom")]
    [StringLength(128)]
    public string? CurrencyID { get; set; }

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

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [StringLength(128)]
    public string? Branch { get; set; }

    [Display(Name = "Inventory ID")]
    [StringLength(128)]
    public string? InventoryID { get; set; }

    [Display(Name = "Trans Desc")]
    [StringLength(128)]
    public string? TransactionDescription { get; set; }

    [Display(Name = "Quantity")]
    [Column(TypeName = "decimal(18, 6)")]
    [DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:N0}")]
    public decimal? Qty { get; set; }

    [Display(Name = "Unit Cost")]
    [Column(TypeName = "decimal(18, 6)")]
    [DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:C2}")]
    public decimal? UnitCost { get; set; }

    [Column(TypeName = "decimal(18, 6)")]
    [DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:C2}")]
    public decimal? Amount { get; set; }

    [StringLength(128)]
    public string? Account { get; set; }

    [StringLength(128)]
    public string? Description { get; set; }

    [Display(Name = "Cost Code")]
    [StringLength(128)]
    public string? CostCode { get; set; }

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

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [StringLength(128)]
    public string? CaseID { get; set; }

    [StringLength(128)]
    public string? Subject { get; set; }

    [Display(Name = "Account ID")]
    [StringLength(128)]
    public string? BusinessAccount { get; set; }

    [StringLength(128)]
    [Display(Name = "Account Name")]
    public string? BusinessAccountName { get; set; }

    [StringLength(128)]
    public string? Status { get; set; }

    [StringLength(128)]
    public string? Reason { get; set; }

    [StringLength(128)]
    public string? Severity { get; set; }

    [StringLength(128)]
    public string? Priority { get; set; }

    [StringLength(128)]
    public string? Owner { get; set; }

    [StringLength(128)]
    public string? Workgroup { get; set; }

    [Display(Name = "Class")]
    [StringLength(128)]
    public string? ClassID { get; set; }

    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",ApplyFormatInEditMode = true)]
    public DateTime? DateReported { get; set; }

    [Display(Name = "Last Activity")]
    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",ApplyFormatInEditMode = true)]
    public DateTime? LastActivityDate { get; set; }

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

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public bool Active { get; set; }

    [Display(Name = "Contact ID")]
    [StringLength(128)]
    public int? ContactID { get; set; }

    [StringLength(128)]
    [Display(Name = "Contact Name")]
    public string? DisplayName { get; set; }

    [StringLength(128)]
    [Display(Name = "Class")]
    public string? ContactClass { get; set; }

    [StringLength(128)]
    [Display(Name = "Account ID")]
    public string? BusinessAccount { get; set; }

    [Display(Name = "Job Title")]
    [StringLength(128)]
    public string? JobTitle { get; set; }

    [StringLength(128)]
    public string? Owner { get; set; }

    [StringLength(128)]
    [Display(Name = "Status")]
    public string? Status { get; set; }

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

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Display(Name = "Oppty ID")]
    [StringLength(128)]
    public string? OpportunityID { get; set; }

    [StringLength(128)]
    public string? Subject { get; set; }

    [StringLength(128)]
    public string? Status { get; set; }

    [StringLength(128)]
    public string? Stage { get; set; }

    [Display(Name = "Close Date")]
    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",ApplyFormatInEditMode = true)]
    public DateTime? Estimation { get; set; }

    [Display(Name = "Curr Denom")]
    [StringLength(128)]
    public string? CurrencyID { get; set; }

    [DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:C2}")]
    public decimal? Total { get; set; }

    [Display(Name = "Class")]
    [StringLength(128)]
    public string? ClassID { get; set; }

    [StringLength(128)]
    public string? Owner { get; set; }

    [Display(Name = "Contact ID")]
    [StringLength(128)]
    public int? ContactID { get; set; }

    [Display(Name = "Contact Name")]
    [StringLength(20)]
    public string? ContactDisplayName { get; set; }

    [Display(Name = "Account ID")]
    [StringLength(128)]
    public string? BusinessAccount { get; set; }

    [Display(Name = "Last Modified")]
    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",ApplyFormatInEditMode = true)]
    public DateTime? LastModifiedDateTime { get; set; }

    #endregion
}
#endregion

#region SalesOrder_App
/// <summary>
///  mapping class for standard output form
/// </summary>
public class SalesOrder_App
{
    #region SalesOrder_App

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Display(Name = "Order Type")]
    [StringLength(10)]
    public string? OrderType { get; set; }

    [StringLength(10)]
    public string? OrderNbr { get; set; }

    [StringLength(10)]
    public string? Status { get; set; }

    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",ApplyFormatInEditMode = true)]
    public DateTime? Date { get; set; }

    [Display(Name = "Customer ID")]
    [StringLength(10)]
    [RegularExpression(@"^[a-zA-Z0-9_]*$", ErrorMessage ="up to 10 AlphaNumeric.")]
    public string? CustomerID { get; set; }

    [Display(Name = "Customer Name")]
    [StringLength(128)]
    [RegularExpression(@"^[A-Za-z]+([ '-][A-Za-z]+)*$", ErrorMessage = "Alpha characters only.")]
    public string? CustomerName { get; set; }

    [Display(Name = "Qty Ordered")]
    [Column(TypeName = "decimal(18, 6)")]
    [DisplayFormat(DataFormatString = "{0:N0}")]
    [RegularExpression(@"^\$?-?\d+(,\d{3})*(\.\d{1,2})?$", ErrorMessage = "Numbers only!")]
    public decimal? OrderedQty { get; set; }

    [Display(Name = "Order Total")]
    [Column(TypeName = "decimal(18, 6)")]
    [DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:C2}")]
    [RegularExpression(@"^\$?-?\d+(,\d{3})*(\.\d{1,2})?$",ErrorMessage = "Numbers only!")]
    public decimal? OrderTotal { get; set; }

    [Display(Name = "Curr Denom")]
    [StringLength(10)]
    public string? CurrencyID { get; set; }

    [Display(Name = "Ship Date")]
    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",ApplyFormatInEditMode = true)]
    public DateTime? ShipmentDate { get; set; }

    [Display(Name = "Last Modified")]

    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",ApplyFormatInEditMode = true)]
    public DateTime? LastModified { get; set; }

    #endregion
}
#endregion

#region Shipment_App
/// <summary>
///  mapping class for standard output form
/// </summary>
public class Shipment_App
{
    #region Shipments_App

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [StringLength(128)]
    public string? Type { get; set; }

    [Display(Name = "Shipment Nbr")]
    [StringLength(128)]
    public string? ShipmentNbr { get; set; }

    [StringLength(128)]
    public string? Status { get; set; }

    [Display(Name = "Ship Date")]
    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",ApplyFormatInEditMode = true)]
    public DateTime? ShipmentDate { get; set; }

    [Display(Name = "Customer ID")]
    [StringLength(128)]
    public string? CustomerID { get; set; }

    [Display(Name = "Customer Name")]
    [StringLength(128)]
    public string? CustomerName { get; set; }

    [Display(Name = "Warehouse ID")]
    [StringLength(128)]
    public string? WarehouseID { get; set; }

    [Display(Name = "Qty Shipped")]
    [Column(TypeName = "decimal(18, 6)")]
    [DisplayFormat(DataFormatString = "{0:N0}")]
    public decimal? ShippedQty { get; set; }

    [Display(Name = "Shipment Weight")]
    [Column(TypeName = "decimal(18, 6)")]
    [DisplayFormat(DataFormatString = "{0:N0}")]
    public decimal? ShippedWeight { get; set; }

    [Display(Name = "Date Created")]
    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",ApplyFormatInEditMode = true)]
    public DateTime? CreatedDateTime { get; set; }

    [Display(Name = "Last Modified")]
    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",ApplyFormatInEditMode = true)]
    public DateTime? LastModifiedDateTime { get; set; }

    #endregion
}
#endregion

#region ShipmentDetail_App
/// <summary>
///  mapping class for standard output form
/// </summary>
public class ShipmentDetail_App
{
    #region ShipmentDetail_App

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Display(Name = "Order Type")]
    [StringLength(128)]
    public string? OrderType { get; set; }

    [StringLength(128)]
    public string? OrderNbr { get; set; }

    [Display(Name = "Inventory ID")]
    [StringLength(128)]
    public string? InventoryID { get; set; }

    [Display(Name = "Warehouse ID")]
    [StringLength(128)]
    public string? WarehouseID { get; set; }

    [Display(Name = "Qty Shipped")]
    [Column(TypeName = "decimal(18, 6)")]
    [DisplayFormat(DataFormatString = "{0:N0}")]
    public decimal? ShippedQty { get; set; }

    [Display(Name = "Qty Ordered")]
    [Column(TypeName = "decimal(18, 6)")]
    [DisplayFormat(DataFormatString = "{0:N0}")]
    public decimal? OrderedQty { get; set; }

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

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Display(Name = "Street Addr")]
    [StringLength(128)]
    public string? AddressLine1 { get; set; }

    [Display(Name = "STE, Bldg")]
    [StringLength(128)]
    public string? AddressLine2 { get; set; }

    [StringLength(128)]
    public string? City { get; set; }

    [StringLength(128)]
    public string? Country { get; set; }

    [Display(Name = "Zip Code")]
    [DataType(DataType.PostalCode)]
    public string? PostalCode { get; set; }

    [StringLength(128)]
    public string? State { get; set; }

    #endregion
}
#endregion
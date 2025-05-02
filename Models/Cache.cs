#nullable enable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace RevisionTwoApp.RestApi.Models;

/// <summary>
///  EF Data Models. 
///  Generally used for input/output to Storage.
///

#region AR_Bill
/// Custom classes with std C# types that map to the Default classes
/// <see cref="Conversion"/>
/// </summary>
public class AR_Bill
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [StringLength(20)]
    public string? Type { get; set; }

    [Display(Name = "Ref Nbr")]
    [StringLength(20)]
    public string? ReferenceNbr { get; set; }

    [StringLength(20)]
    public string? Status { get; set; }

    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",ApplyFormatInEditMode = true)]
    public DateTime? Date { get; set; }

    [Display(Name = "Post Period")]
    [StringLength(20)]
    public string? PostPeriod { get; set; }

    [StringLength(20)]
    public string? Vendor { get; set; }

    [StringLength(50)]
    public string? Description { get; set; }

    [Display(Name = "Vendor Ref Nbr")]
    [StringLength(20)]
    public string? VendorRef { get; set; }

    [Precision(19,2)]
    [DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:c}")]
    public decimal? Amount { get; set; }

    [Display(Name = "Currency Denom.")]
    [StringLength(20)]
    public string? CurrencyID { get; set; }

    [Display(Name = "Last Modified")]
    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",ApplyFormatInEditMode = true)]
    public DateTime? LastModifiedDateTime { get; set; }
}
#endregion

#region AR_BillDetail
/// <summary>
///  mapping class for standard output form
/// </summary>
public class AR_BillDetail
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [StringLength(20)]
    public string? Branch { get; set; }

    [Display(Name = "Inventory ID")]
    [StringLength(20)]
    public string? InventoryID { get; set; }

    [Display(Name = "Trans Descrip")]
    [StringLength(50)]
    public string? TransactionDescription { get; set; }

    [Precision(19,2)]
    [DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:N2}")]
    public decimal? Qty { get; set; }

    [Display(Name = "Unit Cost")]
    [Precision(19,2)]
    [DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:c}")]
    public decimal? UnitCost { get; set; }

    [Precision(19,2)]
    [DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:c}")]
    public decimal? Amount { get; set; }

    [StringLength(20)]
    public string? Account { get; set; }

    [StringLength(50)]
    public string? Description { get; set; }

    [Display(Name = "Cost Code")]
    [StringLength(20)]
    public string? CostCode { get; set; }

    [Display(Name = "PO OrderNbr")]
    [StringLength(20)]
    public string? POOrderNbr { get; set; }
}
#endregion

#region CRCase
/// <summary>
///  mapping class for standard output form
/// </summary>
public class CRCase
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [StringLength(10)]
    [Display(Name = "Case CD")]
    public string? CaseID { get; set; }

    [StringLength(50)]
    public string? Subject { get; set; }

    [StringLength(10)]
    [Display(Name = "Account ID")]
    public string? BusinessAccount { get; set; }

    [StringLength(50)]
    [Display(Name = "Account Name")]
    public string? BusinessAccountName { get; set; }

    [StringLength(50)]
    public string? Status { get; set; }

    [StringLength(20)]
    public string? Reason { get; set; }

    [StringLength(20)]
    public string? Severity { get; set; }

    [StringLength(20)]
    public string? Priority { get; set; }

    [StringLength(50)]
    public string? Owner { get; set; }

    [StringLength(50)]
    public string? Workgroup { get; set; }

    [Display(Name = "Class")]
    [StringLength(10)]
    public string? ClassID { get; set; }

    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",ApplyFormatInEditMode = true)]
    public DateTime? DateReported { get; set; }

    [Display(Name = "Last Activity")]
    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",ApplyFormatInEditMode = true)]
    public DateTime? LastActivityDate { get; set; }

    [Display(Name = "Last Modified")]
    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",ApplyFormatInEditMode = true)]
    public DateTime? LastModifiedDateTime { get; set; }
}
#endregion

#region CO
/// <summary>
///  mapping class for standard output form
/// </summary>
public class CO
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public bool Active { get; set; }

    [Display(Name = "Contact ID")]
    [StringLength(20)]
    public int? ContactID { get; set; }

    [Display(Name = "Contact Name")]
    [StringLength(50)]
    public string? DisplayName { get; set; }

    [Display(Name = "Account ID")]
    [StringLength(30)]
    public string? BusinessAccount { get; set; }

    [Display(Name = "Job Title")]
    [StringLength(50)]
    public string? JobTitle { get; set; }

    [StringLength(50)]
    public string? Owner { get; set; }

    [Display(Name = "Company Name")]
    [StringLength(50)]
    public string? CompanyName { get; set; }

    [Display(Name = "Class")]
    [StringLength(20)]
    public string? ContactClass { get; set; }

    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }

    [Display(Name = "Phone")]
    [DataType(DataType.PhoneNumber)]
    public string? Phone1 { get; set; }

    [Display(Name = "Last Modified")]
    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",ApplyFormatInEditMode = true)]
    public DateTime? LastModifiedDateTime { get; set; }
}
#endregion

#region OP
/// <summary>
///  mapping class for standard output form
/// </summary>
public class OP
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Display(Name = "Oppty ID")]
    [StringLength(10)]
    public string? OpportunityID { get; set; }

    [StringLength(256)]
    public string? Subject { get; set; }

    [StringLength(10)]
    public string? Status { get; set; }

    [StringLength(10)]
    public string? Stage { get; set; }

    [Display(Name = "Currency Denom.")]
    [StringLength(5)]
    public string? CurrencyID { get; set; }

    [Precision(19,2)]
    [DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:c}")]
    public decimal? Total { get; set; }

    [Display(Name = "Class")]
    [StringLength(10)]
    public string? ClassID { get; set; }

    [StringLength(20)]
    public string? Owner { get; set; }

    [Display(Name = "Contact ID")]
    [StringLength(20)]
    public int? ContactID { get; set; }

    [Display(Name = "Contact Name")]
    [StringLength(256)]
    public string? ContactDisplayName { get; set; }

    [Display(Name = "Account ID")]
    [StringLength(20)]
    public string? BusinessAccount { get; set; }

    [Display(Name = "Last Modified")]
    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",ApplyFormatInEditMode = true)]
    public DateTime? LastModifiedDateTime { get; set; }

}
#endregion

#region SO
/// <summary>
///  mapping class for standard output form
/// </summary>
public class SO
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Display(Name = "Order Type")]
    [StringLength(20)]
    public string? OrderType { get; set; }

    [StringLength(20)]
    public string? OrderNbr { get; set; }

    [StringLength(20)]
    public string? Status { get; set; }

    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",ApplyFormatInEditMode = true)]
    public DateTime? Date { get; set; }

    [Display(Name = "Customer ID")]
    [StringLength(20)]
    public string? CustomerID { get; set; }

    [StringLength(50)]
    [Display(Name = "Customer Name")]
    public string? CustomerName { get; set; }

    [Display(Name = "Qty Ordered")]
    [Precision(19,2)]
    [DisplayFormat(DataFormatString = "{0:N2}")]
    public decimal? OrderedQty { get; set; }

    [Display(Name = "Order Total")]
    [Precision(19,2)]
    [DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:c}")]
    public decimal? OrderTotal { get; set; }

    [Display(Name = "Currency Denom.")]
    [StringLength(20)]
    public string? CurrencyID { get; set; }

    [Display(Name = "Ship Date")]

    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",ApplyFormatInEditMode = true)]
    public DateTime? ShipmentDate { get; set; }

    [Display(Name = "Last Modified")]

    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",ApplyFormatInEditMode = true)]
    public DateTime? LastModified { get; set; }
}
#endregion

#region SP
/// <summary>
///  mapping class for standard output form
/// </summary>
public class SP
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [StringLength(20)]
    public string? Type { get; set; }

    [Display(Name = "Shipment Nbr")]
    [StringLength(20)]
    public string? ShipmentNbr { get; set; }

    [StringLength(20)]
    public string? Status { get; set; }

    [Display(Name = "Ship Date")]
    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",ApplyFormatInEditMode = true)]
    public DateTime? ShipmentDate { get; set; }

    [Display(Name = "Customer ID")]
    [StringLength(10)]
    public string? CustomerID { get; set; }

    [Display(Name = "Customer Name")]
    [StringLength(50)]
    public string? CustomerName { get; set; } //from BAAcount

    [Display(Name = "Warehouse ID")]
    [StringLength(20)]
    public string? WarehouseID { get; set; }

    [Display(Name = "Qty Shipped")]
    [Precision(19,2)]
    [DisplayFormat(DataFormatString = "{0:N2}")]
    public decimal? ShippedQty { get; set; }

    [Display(Name = "Shipment Weight")]
    [Precision(19,2)]
    [DisplayFormat(DataFormatString = "{0:N2}")]
    public decimal? ShippedWeight { get; set; }

    [Display(Name = "Date Created")]
    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",ApplyFormatInEditMode = true)]
    public DateTime? CreatedDateTime { get; set; }

    [Display(Name = "Last Modified")]
    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",ApplyFormatInEditMode = true)]
    public DateTime? LastModifiedDateTime { get; set; }
}
#endregion

#region SPDetail
/// <summary>
///  mapping class for standard output form
/// </summary>
public class SPDetail
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Display(Name = "Order Type")]
    [StringLength(20)]
    public string? OrderType { get; set; }

    [StringLength(20)]
    public string? OrderNbr { get; set; }

    [Display(Name = "Inventory ID")]
    [StringLength(20)]
    public string? InventoryID { get; set; }

    [Display(Name = "Warehouse ID")]
    [StringLength(50)]
    public string? WarehouseID { get; set; }

    [Display(Name = "Qty Shipped")]
    [Precision(19,2)]
    [DisplayFormat(DataFormatString = "{0:N2}")]
    public decimal? ShippedQty { get; set; }

    [Display(Name = "Qty Ordered")]
    [Precision(19,2)]
    [DisplayFormat(DataFormatString = "{0:N2}")]
    public decimal? OrderedQty { get; set; }

    [StringLength(50)]
    public string? Description { get; set; }
}
#endregion

#region Addr
/// <summary>
///  mapping class for standard output form
/// </summary>
public class Addr
{
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
}
#endregion


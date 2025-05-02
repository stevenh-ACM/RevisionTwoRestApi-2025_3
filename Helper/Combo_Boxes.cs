#nullable disable

using Microsoft.AspNetCore.Mvc.Rendering;


namespace RevisionTwoApp.RestApi.Helper;

public class Combo_Boxes
{
    /// <summary>
    /// Combo boxes for Bill creation
    /// </summary>
    public IEnumerable<string> Bill_Types { get; set; }
    public IEnumerable<string> Bill_Statuses { get; set; }

    /// <summary>
    /// Combo boxes for Case creation
    /// </summary>
    public IEnumerable<string> Case_Statuses { get; set; }
    public IEnumerable<string> Case_Reasons { get; set; }
    public IEnumerable<string> Case_Severity { get; set; }
    public IEnumerable<string> Case_Priorities { get; set; }
    public IEnumerable<string> Case_ClassIDs { get; set; }

    /// <summary>
    /// Combo boxes for Contact
    /// </summary>
    public IEnumerable<string> Contact_Classes { get; set; }

    /// <summary>
    /// Combo boxes for Opportunity
    /// </summary>
    public IEnumerable<string> Opportunity_Statuses { get; set; }
    public IEnumerable<string> Opportunity_Stages { get; set; }
    public IEnumerable<string> Opportunity_ClassIDs { get; set; }

    /// <summary>
    /// Combo Boxes for SalesOrder
    /// </summary>
    public IEnumerable<string> SalesOrder_Types { get; set; }
    public IEnumerable<string> SalesOrder_Statuses { get; set; }

    /// <summary>
    /// Combo Boxes for Shipments
    /// </summary>
    public IEnumerable<string> Shipment_Types { get; set; }
    public IEnumerable<string> Shipment_Statuses { get; set; }

    public List<SelectListItem> ComboBox_Bill_Types { get; } =
    [
        new SelectListItem { Value = "INV", Text = "Bill" },
        new SelectListItem { Value = "ACR", Text = "Credit Adj." },
        new SelectListItem { Value = "ADR", Text = "Debit Adj."},
        new SelectListItem { Value = "PPM", Text = "Prepayment"}
    ];
    public List<SelectListItem> ComboBox_Bill_Statuses { get; } = 
    [
        new SelectListItem { Value = "H", Text = "On Hold" },
        new SelectListItem { Value = "B", Text = "Balanced" },
        new SelectListItem { Value = "V", Text = "Voided" },
        new SelectListItem { Value = "S", Text = "Scheduled" },
        new SelectListItem { Value = "N", Text = "Open" },
        new SelectListItem { Value = "C", Text = "Closed" },
        new SelectListItem { Value = "P", Text = "Printed" },
        new SelectListItem { Value = "K", Text = "Pre-Released" },
        new SelectListItem { Value = "E", Text = "Pending Approval" },
        new SelectListItem { Value = "R", Text = "Rejected" },
        new SelectListItem { Value = "Z", Text = "Reserved" },
        new SelectListItem { Value = "G", Text = "Pending Print" },
        new SelectListItem { Value = "X", Text = "Under Reclassification"}
    ];
    public List<SelectListItem> ComboBox_Case_Statuses { get; } =
    [
        new SelectListItem { Value = "N", Text = "New" },
        new SelectListItem { Value = "O", Text = "Open" },
        new SelectListItem { Value = "C", Text = "Closed"},
        new SelectListItem { Value = "R", Text = "Released"},
        new SelectListItem { Value = "P", Text = "Pending Customer"}
    ];
    public List<SelectListItem> ComboBox_Case_Reasons { get; } =
    [
        new SelectListItem { Value = "RJ", Text = "Rejected" },
        new SelectListItem { Value = "RD", Text = "Resolved" },
        new SelectListItem { Value = "MI", Text = "More Info Requested"},
        new SelectListItem { Value = "IP", Text = "In Process"},
        new SelectListItem { Value = "IN", Text = "Internal"},
        new SelectListItem { Value = "ES", Text = "In Escalation"},
        new SelectListItem { Value = "DP", Text = "Duplicate"},
        new SelectListItem { Value = "CR", Text = "Waiting Confirmation"},
        new SelectListItem { Value = "CP", Text = "Customer Postpone"},
        new SelectListItem { Value = "CL", Text = "Canceled"},
        new SelectListItem { Value = "CC", Text = "Pending Closure"},
        new SelectListItem { Value = "CA", Text = "Abandoned"},
        new SelectListItem { Value = "AS", Text = "Unassigned"},
        new SelectListItem { Value = "AA", Text = "Assigned"},
        new SelectListItem { Value = "AD", Text = "Updated"},
        new SelectListItem { Value = "PC", Text = "Closed on Portal"},
        new SelectListItem { Value = "PO", Text = "Opened on Portal"}
    ];
    public List<SelectListItem> ComboBox_Case_Severities { get; } =
    [
        new SelectListItem { Value = "L", Text = "Low" },
        new SelectListItem { Value = "M", Text = "Medium" },
        new SelectListItem { Value = "H", Text = "High"},
        new SelectListItem { Value = "U", Text = "Urgent"}
    ];
    public List<SelectListItem> ComboBox_Case_Priorities { get; } =
    [
        new SelectListItem { Value = "L", Text = "Low" },
        new SelectListItem { Value = "M", Text = "Medium" },
        new SelectListItem { Value = "H", Text = "High"}
    ];
    public List<SelectListItem> ComboBox_Case_ClassIDs { get; } =
    [
        new SelectListItem { Value = "BILLING", Text = "Billing" },
        new SelectListItem { Value = "INTERNAL", Text = "Internal support case" },
        new SelectListItem { Value = "OTHER", Text = "Other Injury"},
        new SelectListItem { Value = "PRODSUP", Text = "Product Support with Contract"},
        new SelectListItem { Value = "PRODSUPINC", Text = "Product Support - Incident"}
    ];
    public List<SelectListItem> ComboBox_Contact_Classes { get; } =
    [
        new SelectListItem { Value = "LEADBUS", Text = "Sales Lead - Business" },
        new SelectListItem { Value = "LEADBUSSVC", Text = "Sales Lead - Business Services" },
        new SelectListItem { Value = "LEADCON", Text = "Sales Lead - Customer"},
        new SelectListItem { Value = "LEADIMP", Text = "Sales Lead - Imported or External"}
    ];
    public List<SelectListItem> ComboBox_Opportunity_Statuses { get; } =
    [
        new SelectListItem { Value = "N", Text = "New" },
        new SelectListItem { Value = "O", Text = "Open" },
        new SelectListItem { Value = "W", Text = "Won"},
        new SelectListItem { Value = "L", Text = "Lost"}
    ];
    public List<SelectListItem> ComboBox_Opportunity_Stages { get; } =
    [
        new SelectListItem { Value = "L", Text = "Prospect" },
        new SelectListItem { Value = "N", Text = "Nurture" },
        new SelectListItem { Value = "P", Text = "Qualification" },
        new SelectListItem { Value = "Q", Text = "Develpment" },
        new SelectListItem { Value = "V", Text = "Solution" },
        new SelectListItem { Value = "A", Text = "Proof" },
        new SelectListItem { Value = "R", Text = "Negotiation" },
        new SelectListItem { Value = "W", Text = "Won" }
    ];
    public List<SelectListItem> ComboBox_Opportunity_ClassIDs { get; } =
    [
        new SelectListItem { Value = "PARTNER", Text = "Partner Sales" },
        new SelectListItem { Value = "PRODUCT", Text = "Product Sales Opportunity" },
        new SelectListItem { Value = "SERVICE", Text = "Service Opportunity"}
    ];
    public List<SelectListItem> ComboBox_SalesOrder_Types { get; } =
    [
        new SelectListItem { Value = "CM", Text = "(CM) Credit Memo" },
        new SelectListItem { Value = "CS", Text = "(CS) Cash Sale" },
        new SelectListItem { Value = "CT", Text = "(CT) Counter Sale"},
        new SelectListItem { Value = "EO", Text = "(EO) BigCommerce Order"},
        new SelectListItem { Value = "ER", Text = "(ER) eCommerce RMA Order"},
        new SelectListItem { Value = "IN", Text = "(IN) Invoice"},
        new SelectListItem { Value = "PR", Text = "(PR) Sales Order for Project Billing"},
        new SelectListItem { Value = "QT", Text = "(QT) Quote"},
        new SelectListItem { Value = "RC", Text = "(RC) Return for Credit"},
        new SelectListItem { Value = "RM", Text = "(RM) RMA Order"},
        new SelectListItem { Value = "RR", Text = "(RR) Return with Replacement"},
        new SelectListItem { Value = "SC", Text = "(SC) Sales Order - Commerce"},
        new SelectListItem { Value = "SD", Text = "(SD) Shopify POS Direct"},
        new SelectListItem { Value = "SF", Text = "(SF) Sales Order - Field Service"},
        new SelectListItem { Value = "SH", Text = "(SH) Shopify Order"},
        new SelectListItem { Value = "SI", Text = "(SI) Sales Order - related company"},
        new SelectListItem { Value = "SO", Text = "(SO) Sales Order"},
        new SelectListItem { Value = "SS", Text = "(SS) Shopify POS To Ship Order"},
        new SelectListItem { Value = "TR", Text = "(TR) Transfer"}
    ];
    public List<SelectListItem> ComboBox_SalesOrder_Statuses { get; } =
    [
        new SelectListItem { Value = "N", Text = "Open" },
        new SelectListItem { Value = "H", Text = "On Hold" },
        new SelectListItem { Value = "P", Text = "Pending Approval" },
        new SelectListItem { Value = "V", Text = "Rejected" },
        new SelectListItem { Value = "E", Text = "Pending Processing" },
        new SelectListItem { Value = "A", Text = "Awaiting Payment" },
        new SelectListItem { Value = "R", Text = "Credit Hold" },
        new SelectListItem { Value = "C", Text = "Completed" },
        new SelectListItem { Value = "L", Text = "Canceled" },
        new SelectListItem { Value = "B", Text = "Back Order" },
        new SelectListItem { Value = "S", Text = "Shipping" },
        new SelectListItem { Value = "I", Text = "Invoiced" },
        new SelectListItem { Value = "D", Text = "Expired"}
    ];
    public List<SelectListItem> ComboBox_Shipment_Types { get; } =
    [
        new SelectListItem { Value = "I", Text = "Shipment" },
        new SelectListItem { Value = "T", Text = "Transfer" }
    ];
    public List<SelectListItem> ComboBox_Shipment_Statuses { get; } =
    [
        new SelectListItem { Value = "N", Text = "Open" },
        new SelectListItem { Value = "H", Text = "On Hold" },
        new SelectListItem { Value = "C", Text = "Completed" },
        new SelectListItem { Value = "L", Text = "Canceled" },
        new SelectListItem { Value = "F", Text = "Confirmed" },
        new SelectListItem { Value = "I", Text = "Invoiced" },
        new SelectListItem { Value = "Y", Text = "Partially Invoiced" },
        new SelectListItem { Value = "R", Text = "Receipted" },
        new SelectListItem { Value = "A", Text = "Auto-Generated" }
    ];
}

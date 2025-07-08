#nullable disable

using System.Runtime.Serialization;

using Acumatica.Default_24_200_001.Model;
using Acumatica.RESTClient.ContractBasedApi;
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
/// Represents a contact entity with various attributes such as status, display name, and associated business account.
/// </summary>
/// <remarks>This model is typically used to store and manage information about a contact in a business context.
/// It includes properties for identifying the contact, tracking its status, and associating it with other
/// entities.</remarks>
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

#region Customer_Model
/// <summary>
/// Represents a customer entity, including details about the customer's account, preferences, and associated data.
/// </summary>
/// <remarks>The <see cref="Customer_Model.Customer"/> class provides properties and methods to manage
/// customer-related information, such as account identifiers, billing and shipping details, financial preferences, and
/// other attributes. This class is primarily used in business applications to handle customer data and
/// interactions.</remarks>
public class Customer_Model
{
/// <summary>
/// Represents a customer entity, including details about the customer's account, preferences, and associated data.
/// </summary>
/// <remarks>The <see cref="Customer"/> class provides properties and methods to manage customer-related
/// information,  such as account identifiers, billing and shipping details, financial preferences, and other
/// attributes. This class is primarily used in business applications to handle customer data and
/// interactions.</remarks>
    [DataContract]
    public class Customer: Entity, ITopLevelEntity
    {

        /// <summary>
        /// The external reference number of the business account.
        /// DAC Field Name: AcctReferenceNbr 
        /// DAC: PX.Objects.AR.Customer 
        /// Display Name: Ext Ref Nbr 
        /// SQL Type: nvarchar(50) 
        /// </summary>
        /// <remarks>
        /// It can be an additional number of the business account used in external integration.            
        /// </remarks>
        [DataMember(Name = "AccountRef", EmitDefaultValue = false)]
        public StringValue AccountRef { get; set; }

        /// <summary>
        /// If set to true, indicates that financial chargescan be calculated for the customer.
        /// DAC Field Name: FinChargeApply 
        /// DAC: PX.Objects.AR.Customer 
        /// Display Name: Apply Overdue Charges 
        /// </summary>
        [DataMember(Name = "ApplyOverdueCharges", EmitDefaultValue = false)]
        public BooleanValue ApplyOverdueCharges { get; set; }
        /// <summary>
        /// Gets or sets the collection of attribute values associated with the entity.
        /// </summary>
        [DataMember(Name = "Attributes", EmitDefaultValue = false)]
        public List<AttributeValue> Attributes { get; set; }

        /// <summary>
        /// If set to true, indicates that the payments of the customershould be automatically applied to the open invoices upon release.
        /// DAC: PX.Objects.AR.Customer 
        /// Display Name: Auto-Apply Payments 
        /// </summary>
        [DataMember(Name = "AutoApplyPayments", EmitDefaultValue = false)]
        public BooleanValue AutoApplyPayments { get; set; }

        /// <summary>
        /// The identifier of the related business account.Along with ContactID, this field is used as an additional reference,but unlike RefNoteID and DocumentNoteID it is used for specific entities.
        /// DAC: PX.Objects.CR.CRPMTimeActivity 
        /// Display Name: Related Account 
        /// </summary>
        [DataMember(Name = "BAccountID", EmitDefaultValue = false)]
        public IntValue BAccountID { get; set; }

        /// <summary>
        /// A calculated field. If set to false, indicates thatthe customer's billing address is the same as the customer'sdefault address.The field is populated by a formula, working only in the scope of the Customers (AR303000) form. See CustomerBillSharedAddressOverrideGraphExt"
        /// DAC Field Name: OverrideBillAddress 
        /// DAC: PX.Objects.AR.Customer 
        /// Display Name: Override 
        /// </summary>
        [DataMember(Name = "BillingAddressOverride", EmitDefaultValue = false)]
        public BooleanValue BillingAddressOverride { get; set; }
        
        /// <summary>
        /// Gets or sets the billing contact information associated with the account.
        /// </summary>
        [DataMember(Name = "BillingContact", EmitDefaultValue = false)]
        public Contact BillingContact { get; set; }

        /// <summary>
        /// A calculated field. If set to false, indicates that the customer's billing contact is the same as the customer'sdefault contact.The field is populated by a formula, working only in the scope of the Customers (AR303000) form. See CustomerBillSharedContactOverrideGraphExt"
        /// DAC Field Name: OverrideBillContact 
        /// DAC: PX.Objects.AR.Customer 
        /// Display Name: Override 
        /// </summary>
        [DataMember(Name = "BillingContactOverride", EmitDefaultValue = false)]
        public BooleanValue BillingContactOverride { get; set; }

        /// <summary>
        /// Gets or sets the collection of customer contacts associated with the entity.
        /// </summary>
        [DataMember(Name = "Contacts", EmitDefaultValue = false)]
        public List<CustomerContact> Contacts { get; set; }

        /// <summary>
        /// The date and time when the record was created.
        /// DAC: PX.Objects.CR.Standalone.Location 
        /// </summary>
        [DataMember(Name = "CreatedDateTime", EmitDefaultValue = false)]
        public DateTimeValue CreatedDateTime { get; set; }

        /// <summary>
        /// Gets or sets the credit verification rules used to validate credit-related operations.
        /// </summary>
        [DataMember(Name = "CreditVerificationRules", EmitDefaultValue = false)]
        public CreditVerificationRules CreditVerificationRules { get; set; }

        /// <summary>
        /// The identifier of the Currency,which is applied to the documents of the customer.
        /// DAC Field Name: CuryID 
        /// DAC: PX.Objects.AR.Customer 
        /// Display Name: Currency ID 
        /// SQL Type: nvarchar(5) 
        /// </summary>
        [DataMember(Name = "CurrencyID", EmitDefaultValue = false)]
        public StringValue CurrencyID { get; set; }

        /// <summary>
        /// The identifier of the currency rate type,which is applied to the documents of the customer.
        /// DAC Field Name: CuryRateTypeID 
        /// DAC: PX.Objects.AR.Customer 
        /// Display Name: Curr. Rate Type 
        /// SQL Type: nvarchar(6) 
        /// </summary>
        [DataMember(Name = "CurrencyRateType", EmitDefaultValue = false)]
        public StringValue CurrencyRateType { get; set; }

        /// <summary>
        /// Identifier of the customer class to which the customer belongs.
        /// DAC Field Name: CustomerClassID 
        /// DAC: PX.Objects.AR.Customer 
        /// Display Name: Customer Class 
        /// SQL Type: nvarchar(10) 
        /// </summary>
        [DataMember(Name = "CustomerClass", EmitDefaultValue = false)]
        public StringValue CustomerClass { get; set; }

        /// <summary>
        /// The human-readable identifier of the customer account, which isspecified by the user or defined by the auto-numbering sequence duringcreation of the customer. This field is a natural key, as opposedto the surrogate key BAccountID.
        /// DAC Field Name: AcctCD 
        /// DAC: PX.Objects.AR.Customer 
        /// Display Name: Customer ID 
        /// SQL Type: nvarchar(30) 
        /// Key Field
        /// </summary>
        [DataMember(Name = "CustomerID", EmitDefaultValue = false)]
        public StringValue CustomerID { get; set; }

        /// <summary>
        /// The customer kind, indicating whether the customer is an individual (I) or an organization (O).
        /// DAC: PX.Objects.AR.Customer 
        /// Display Name: Customer Category 
        /// SQL Type: char(1) 
        /// </summary>
        [DataMember(Name = "CustomerCategory", EmitDefaultValue = false)]
        public StringValue CustomerCategory { get; set; }

        /// <summary>
        /// The full business account name (as opposed to the short identifier provided by AcctCD).
        /// DAC Field Name: AcctName 
        /// DAC: PX.Objects.AR.Customer 
        /// Display Name: Customer Name 
        /// SQL Type: nvarchar(255) 
        /// </summary>
        [DataMember(Name = "CustomerName", EmitDefaultValue = false)]
        public StringValue CustomerName { get; set; }

        /// <summary>
        /// DAC: PX.Objects.CS.NotificationRecipient 
        /// SQL Type: nvarchar(MAX) 
        /// </summary>
        [DataMember(Name = "Email", EmitDefaultValue = false)]
        public StringValue Email { get; set; }

        /// <summary>
        /// If set to true, indicates that the currency of customer documents (which is specified by CuryID)can be overridden by a user during document entry.
        /// DAC Field Name: AllowOverrideCury 
        /// DAC: PX.Objects.AR.Customer 
        /// Display Name: Enable Currency Override 
        /// </summary>
        [DataMember(Name = "EnableCurrencyOverride", EmitDefaultValue = false)]
        public BooleanValue EnableCurrencyOverride { get; set; }

        /// <summary>
        /// If set to true, indicates that the currency ratefor customer documents (which is calculated by the system from the currency rate history) can be overridden by a user during document entry.
        /// DAC Field Name: AllowOverrideRate 
        /// DAC: PX.Objects.AR.Customer 
        /// Display Name: Enable Rate Override 
        /// </summary>
        [DataMember(Name = "EnableRateOverride", EmitDefaultValue = false)]
        public BooleanValue EnableRateOverride { get; set; }

        /// <summary>
        /// If set to true, indicates that small balancewrite-offs are allowed for the customer.
        /// DAC Field Name: SmallBalanceAllow 
        /// DAC: PX.Objects.AR.Customer 
        /// Display Name: Enable Write-Offs 
        /// </summary>
        [DataMember(Name = "EnableWriteOffs", EmitDefaultValue = false)]
        public BooleanValue EnableWriteOffs { get; set; }

        /// <summary>
        /// The customer's FOB (free on board) shipping point.
        /// DAC Field Name: CFOBPointID 
        /// DAC: PX.Objects.CR.Standalone.Location 
        /// Display Name: FOB Point 
        /// SQL Type: nvarchar(15) 
        /// </summary>
        [DataMember(Name = "FOBPoint", EmitDefaultValue = false)]
        public StringValue FOBPoint { get; set; }

        /// <summary>
        /// The date and time when the record was last modified.
        /// DAC: PX.Objects.CR.Standalone.Location 
        /// </summary>
        [DataMember(Name = "LastModifiedDateTime", EmitDefaultValue = false)]
        public DateTimeValue LastModifiedDateTime { get; set; }

        /// <summary>
        /// The amount of lead days (the time in days from the moment when the production was finished to the moment when the customer's order was delivered).
        /// DAC Field Name: CLeadTime 
        /// DAC: PX.Objects.CR.Standalone.Location 
        /// Display Name: Lead Time (Days) 
        /// </summary>
        [DataMember(Name = "LeadTimedays", EmitDefaultValue = false)]
        public ShortValue LeadTimedays { get; set; }

        /// <summary>
        /// The name of the location.
        /// DAC Field Name: Descr 
        /// DAC: PX.Objects.CR.Standalone.Location 
        /// Display Name: Location Name 
        /// SQL Type: nvarchar(60) 
        /// </summary>
        [DataMember(Name = "LocationName", EmitDefaultValue = false)]
        public StringValue LocationName { get; set; }

        /// <summary>
        /// Gets or sets the primary contact associated with this entity.
        /// </summary>
        [DataMember(Name = "MainContact", EmitDefaultValue = false)]
        public Contact MainContact { get; set; }

        /// <summary>
        /// If set to true, indicates that customerstatements should be generated for the customer in multi-currency format.
        /// DAC Field Name: PrintCuryStatements 
        /// DAC: PX.Objects.AR.Customer 
        /// Display Name: Multi-Currency Statements 
        /// </summary>
        [DataMember(Name = "MultiCurrencyStatements", EmitDefaultValue = false)]
        public BooleanValue MultiCurrencyStatements { get; set; }

        /// <summary>
        /// The order priority of the customer's location.
        /// DAC Field Name: COrderPriority 
        /// DAC: PX.Objects.CR.Standalone.Location 
        /// Display Name: Order Priority 
        /// </summary>
        [DataMember(Name = "OrderPriority", EmitDefaultValue = false)]
        public ShortValue OrderPriority { get; set; }

        /// <summary>
        /// The identifier of the parent business account.
        /// DAC Field Name: ParentBAccountID 
        /// DAC: PX.Objects.AR.Customer 
        /// Display Name: Parent Account 
        /// </summary>
        [DataMember(Name = "ParentRecord", EmitDefaultValue = false)]
        public StringValue ParentRecord { get; set; }

        /// <summary>
        /// Gets or sets the collection of payment instructions associated with the business account.
        /// </summary>
        [DataMember(Name = "PaymentInstructions", EmitDefaultValue = false)]
        public List<BusinessAccountPaymentInstructionDetail> PaymentInstructions { get; set; }

        /// <summary>
        /// The price class of the customer.
        /// DAC Field Name: CPriceClassID 
        /// DAC: PX.Objects.CR.Standalone.Location 
        /// Display Name: Price Class 
        /// SQL Type: nvarchar(10) 
        /// </summary>
        [DataMember(Name = "PriceClassID", EmitDefaultValue = false)]
        public StringValue PriceClassID { get; set; }

        /// <summary>
        /// Gets or sets the primary contact associated with this entity.
        /// </summary>
        [DataMember(Name = "PrimaryContact", EmitDefaultValue = false)]
        public Contact PrimaryContact { get; set; }

        /// <summary>
        /// The identifier of the Contact object linked with the business account and marked as primary.
        /// DAC: PX.Objects.AR.Customer 
        /// Display Name: Primary Contact 
        /// </summary>
        /// <remarks>
        /// Also, the Contact.BAccountID value must equal tothe BAccount.BAccountID value of the current business account.
        /// </remarks>
        [DataMember(Name = "PrimaryContactID", EmitDefaultValue = false)]
        public IntValue PrimaryContactID { get; set; }

        /// <summary>
        /// If set to true, indicates that dunning letters should be printed for the customer.
        /// DAC: PX.Objects.AR.Customer 
        /// Display Name: Print Dunning Letters 
        /// </summary>
        [DataMember(Name = "PrintDunningLetters", EmitDefaultValue = false)]
        public BooleanValue PrintDunningLetters { get; set; }

        /// <summary>
        /// If set to true, indicates that invoicesshould be printed for the customer.
        /// DAC: PX.Objects.AR.Customer 
        /// Display Name: Print Invoices 
        /// </summary>
        [DataMember(Name = "PrintInvoices", EmitDefaultValue = false)]
        public BooleanValue PrintInvoices { get; set; }

        /// <summary>
        /// If set to true, indicates that customerstatements should be printed for the customer.
        /// DAC: PX.Objects.AR.Customer 
        /// Display Name: Print Statements 
        /// </summary>
        [DataMember(Name = "PrintStatements", EmitDefaultValue = false)]
        public BooleanValue PrintStatements { get; set; }

        /// <summary>
        /// This field indicates whether the residential delivery is available in this location.
        /// DAC Field Name: CResedential 
        /// DAC: PX.Objects.CR.Standalone.Location 
        /// Display Name: Residential Delivery 
        /// </summary>
        [DataMember(Name = "ResidentialDelivery", EmitDefaultValue = false)]
        public BooleanValue ResidentialDelivery { get; set; }

        /// <summary>
        /// Gets or sets the collection of salespersons associated with the customer.
        /// </summary>
        [DataMember(Name = "Salespersons", EmitDefaultValue = false)]
        public List<CustomerSalesPerson> Salespersons { get; set; }

        /// <summary>
        /// This field indicates whether the Saturday delivery is available in this location.
        /// DAC Field Name: CSaturdayDelivery 
        /// DAC: PX.Objects.CR.Standalone.Location 
        /// Display Name: Saturday Delivery 
        /// </summary>
        [DataMember(Name = "SaturdayDelivery", EmitDefaultValue = false)]
        public BooleanValue SaturdayDelivery { get; set; }

        /// <summary>
        /// If set to true, indicates that dunning letters should be sent to the customer by email.
        /// DAC Field Name: MailDunningLetters 
        /// DAC: PX.Objects.AR.Customer 
        /// Display Name: Send Dunning Letters by Email 
        /// </summary>
        [DataMember(Name = "SendDunningLettersbyEmail", EmitDefaultValue = false)]
        public BooleanValue SendDunningLettersbyEmail { get; set; }

        /// <summary>
        /// If set to true, indicates that invoicesshould be sent to the customer by email.
        /// DAC Field Name: MailInvoices 
        /// DAC: PX.Objects.AR.Customer 
        /// Display Name: Send Invoices by Email 
        /// </summary>
        [DataMember(Name = "SendInvoicesbyEmail", EmitDefaultValue = false)]
        public BooleanValue SendInvoicesbyEmail { get; set; }

        /// <summary>
        /// DAC Field Name: SendStatementByEmail 
        /// DAC: PX.Objects.AR.Customer 
        /// Display Name: Send Statements by Email 
        /// </summary>
        [DataMember(Name = "SendStatementsbyEmail", EmitDefaultValue = false)]
        public BooleanValue SendStatementsbyEmail { get; set; }

        /// <summary>
        /// If set to true, indicates that the addressoverrides the default Address record, which isreferenced by DefAddressID.
        /// DAC Field Name: OverrideAddress 
        /// DAC: PX.Objects.CR.Standalone.Location 
        /// Display Name: Override 
        /// </summary>
        [DataMember(Name = "ShippingAddressOverride", EmitDefaultValue = false)]
        public BooleanValue ShippingAddressOverride { get; set; }

        /// <summary>
        /// The identifier of the default branch of the customer location.
        /// DAC Field Name: CBranchID 
        /// DAC: PX.Objects.CR.Standalone.Location 
        /// Display Name: Shipping Branch 
        /// </summary>
        [DataMember(Name = "ShippingBranch", EmitDefaultValue = false)]
        public StringValue ShippingBranch { get; set; }

        /// <summary>
        /// Gets or sets the contact information for shipping purposes.
        /// </summary>
        [DataMember(Name = "ShippingContact", EmitDefaultValue = false)]
        public Contact ShippingContact { get; set; }

        /// <summary>
        /// If set to true, indicates that the addressoverrides the default Contact record, which isreferenced by DefContactID.
        /// DAC Field Name: OverrideContact 
        /// DAC: PX.Objects.CR.Standalone.Location 
        /// Display Name: Override 
        /// </summary>
        [DataMember(Name = "ShippingContactOverride", EmitDefaultValue = false)]
        public BooleanValue ShippingContactOverride { get; set; }

        /// <summary>
        /// The shipping rule of the customer location.
        /// DAC Field Name: CShipComplete 
        /// DAC: PX.Objects.CR.Standalone.Location 
        /// Display Name: Shipping Rule 
        /// SQL Type: char(1) 
        /// </summary>
        [DataMember(Name = "ShippingRule", EmitDefaultValue = false)]
        public StringValue ShippingRule { get; set; }

        /// <summary>
        /// The customer's shipping terms.
        /// DAC Field Name: CShipTermsID 
        /// DAC: PX.Objects.CR.Standalone.Location 
        /// Display Name: Shipping Terms 
        /// SQL Type: nvarchar(10) 
        /// </summary>
        [DataMember(Name = "ShippingTerms", EmitDefaultValue = false)]
        public StringValue ShippingTerms { get; set; }

        /// <summary>
        /// The customer's shipping zone.
        /// DAC Field Name: CShipZoneID 
        /// DAC: PX.Objects.CR.Standalone.Location 
        /// Display Name: Shipping Zone 
        /// SQL Type: nvarchar(15) 
        /// </summary>
        [DataMember(Name = "ShippingZoneID", EmitDefaultValue = false)]
        public StringValue ShippingZoneID { get; set; }

        /// <summary>
        /// The shipping carrier for the vendor location.
        /// DAC Field Name: CCarrierID 
        /// DAC: PX.Objects.CR.Standalone.Location 
        /// Display Name: Ship Via 
        /// SQL Type: nvarchar(15) 
        /// </summary>
        [DataMember(Name = "ShipVia", EmitDefaultValue = false)]
        public StringValue ShipVia { get; set; }

        /// <summary>
        /// The identifier of the statement cycleto which the customer is assigned.
        /// DAC Field Name: StatementCycleId 
        /// DAC: PX.Objects.AR.Customer 
        /// Display Name: Statement Cycle ID 
        /// SQL Type: nvarchar(10) 
        /// </summary>
        [DataMember(Name = "StatementCycleID", EmitDefaultValue = false)]
        public StringValue StatementCycleID { get; set; }

        /// <summary>
        /// The type of customer statements generated for the customer.The list of possible values of the field is determined by StatementTypeAttribute.
        /// DAC: PX.Objects.AR.Customer 
        /// Display Name: Statement Type 
        /// SQL Type: char(1) 
        /// </summary>
        [DataMember(Name = "StatementType", EmitDefaultValue = false)]
        public StringValue StatementType { get; set; }

        /// <summary>
        /// DAC: PX.Objects.AR.Customer 
        /// Display Name: Customer Status 
        /// SQL Type: char(1) 
        /// </summary>
        [DataMember(Name = "Status", EmitDefaultValue = false)]
        public StringValue Status { get; set; }

        /// <summary>
        /// The registration ID of the company in the state tax authority.
        /// DAC: PX.Objects.CR.Standalone.Location 
        /// Display Name: Tax Registration ID 
        /// SQL Type: nvarchar(50) 
        /// </summary>
        [DataMember(Name = "TaxRegistrationID", EmitDefaultValue = false)]
        public StringValue TaxRegistrationID { get; set; }

        /// <summary>
        /// The customer's tax zone.
        /// DAC Field Name: CTaxZoneID 
        /// DAC: PX.Objects.CR.Standalone.Location 
        /// Display Name: Tax Zone 
        /// SQL Type: nvarchar(10) 
        /// </summary>
        [DataMember(Name = "TaxZone", EmitDefaultValue = false)]
        public StringValue TaxZone { get; set; }

        /// <summary>
        /// The identifier of the default terms, which are applied to the documents of the customer.
        /// DAC Field Name: TermsID 
        /// DAC: PX.Objects.AR.Customer 
        /// SQL Type: nvarchar(10) 
        /// </summary>
        [DataMember(Name = "Terms", EmitDefaultValue = false)]
        public StringValue Terms { get; set; }

        /// <summary>
        /// The warehouse identifier of the customer location.
        /// DAC Field Name: CSiteID 
        /// DAC: PX.Objects.CR.Standalone.Location 
        /// Display Name: Warehouse 
        /// </summary>
        [DataMember(Name = "WarehouseID", EmitDefaultValue = false)]
        public StringValue WarehouseID { get; set; }

        /// <summary>
        /// If SmallBalanceAllow is set to true, thefield determines the maximum small balance write-off limit for customer documents.
        /// DAC Field Name: SmallBalanceLimit 
        /// DAC: PX.Objects.AR.Customer 
        /// Display Name: Write-Off Limit 
        /// </summary>
        [DataMember(Name = "WriteOffLimit", EmitDefaultValue = false)]
        public DecimalValue WriteOffLimit { get; set; }

        /// <summary>
        /// DAC Field Name: COrgBAccountID 
        /// DAC: PX.Objects.AR.Customer 
        /// Display Name: Restrict Visibility To 
        /// </summary>
        [DataMember(Name = "RestrictVisibilityTo", EmitDefaultValue = false)]
        public StringValue RestrictVisibilityTo { get; set; }

        /// <summary>
        /// If CreditRule enables verification by credit limit,this field determines the maximum amount of credit allowed for the customer.
        /// DAC: PX.Objects.AR.Customer 
        /// Display Name: Credit Limit 
        /// </summary>
        [DataMember(Name = "CreditLimit", EmitDefaultValue = false)]
        public DecimalValue CreditLimit { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the note.
        /// </summary>
        [DataMember(Name = "NoteID", EmitDefaultValue = false)]
        public GuidValue NoteID { get; set; }

        /// <summary>
        /// The customer's entity type for reporting purposes. This field is used if the system is integrated with External Tax Calculationand the External Tax Calculation Integration feature is enabled.
        /// DAC Field Name: CAvalaraCustomerUsageType 
        /// DAC: PX.Objects.CR.Standalone.Location 
        /// Display Name: Tax Exemption Type 
        /// SQL Type: char(1) 
        /// </summary>
        [DataMember(Name = "EntityUsageType", EmitDefaultValue = false)]
        public StringValue EntityUsageType { get; set; }

        /// <summary>
        /// The Avalara Exemption number of the customer location.
        /// DAC Field Name: CAvalaraExemptionNumber 
        /// DAC: PX.Objects.CR.Standalone.Location 
        /// Display Name: Tax Exemption Number 
        /// SQL Type: nvarchar(30) 
        /// </summary>
        [DataMember(Name = "TaxExemptionNumber", EmitDefaultValue = false)]
        public StringValue TaxExemptionNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer is a guest.
        /// </summary>
        [DataMember(Name = "IsGuestCustomer", EmitDefaultValue = false)]
        public BooleanValue IsGuestCustomer { get; set; }
        
        /// <summary>
        /// Retrieves the default endpoint path for the entity.
        /// </summary>
        /// <remarks>This method returns a predefined endpoint path and does not perform any dynamic
        /// computation. It can be overridden in derived classes to provide a custom endpoint path.</remarks>
        /// <returns>A string representing the endpoint path in the format "entity/Default/24.200.001".</returns>
        public virtual string GetEndpointPath( )
        {
            return "entity/Default/24.200.001";
        }
    }
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
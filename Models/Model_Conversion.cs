#nullable disable

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS1587 // XML comment is not placed on a valid language element
#pragma warning disable IDE0290 // Use primary constructor
#pragma warning disable IDE0060 // Use primary constructorsing Acumatica.Default_24_200_001.Model;
 
using RevisionTwoApp.RestApi.Models.Default;

namespace RevisionTwoApp.RestApi.Models;

#region classes
/// <summary>
/// Conversion classes using inheritance of custom models
/// </summary>


#region toBill_App
/// <summary>
/// Conversion to App model from Default model using inheritance 
/// </summary>
public class ToBill_App:Bill_App
{
    #region toBill_App
    public ToBill_App(Bill_Model Bill_Model)
    {

    }
    #endregion
}
#endregion

#region toBill_Model
/// <summary>
/// Conversion to Default model from App model using inheritance
/// </summary>
public class ToBill_Model:Bill_Model
{
    #region toBill_Model
    public ToBill_Model(SalesOrder_App Bill_App)
    {

    }
    #endregion
}
#endregion

#region Create_Bill_Model
/// <summary>
/// Create custom Default model from API Response, add new properties
/// potentially from other Default Models by constructor
/// </summary>
public class Create_Bill_Model:Bill
{
    #region Create_Bill_Model
    //ctor
    public Create_Bill_Model()
    { }
    public Create_Bill_Model(Bill sp_value)
    { }
    #endregion
}
#endregion

#region toBillDetail_App
/// <summary>
/// Conversion to App model from Default model using inheritance 
/// </summary>
public class ToBillDetail_App:BillDetail_App
{
    #region toBillDetail_App
    public ToBillDetail_App(BillDetail_Model BillDetail_Model)
    {

    }
    #endregion
}
#endregion

#region toBillDetail_Model
/// <summary>
/// Conversion to Default model from App model using inheritance
/// </summary>
public class ToBillDetail_Model:BillDetail_Model
{
    #region toBillDetail_Model
    public ToBillDetail_Model(SalesOrder_App BillDetail_App)
    {

    }
    #endregion
}
#endregion

#region Create_BillDetail_Model
/// <summary>
/// Create custom Default model from API Response, add new properties
/// potentially from other Default Models by constructor
/// </summary>
public class Create_BillDetail_Model:BillDetail
{
    #region Create_BillDetail_Model
    //ctor
    public Create_BillDetail_Model()
    { }
    public Create_BillDetail_Model(BillDetail sp_value)
    { }
    #endregion
}
#endregion

#region toCase_App
/// <summary>
/// Conversion to App model from Default model using inheritance 
/// </summary>
public class ToCase_App:Case_App
{
    #region toCase_App
    public ToCase_App(Case_Model Case_Model)
    {

    }
    #endregion
}
#endregion

#region toCase_Model
/// <summary>
/// Conversion to Default model from App model using inheritance
/// </summary>
public class ToCase_Model:Case_Model
{
    #region toCase_Model
    /// <summary>
    /// Initializes a new instance of the <see cref="ToCase_Model"/> class.
    /// </summary>
    /// <param name="Case_App">The SalesOrder_App instance. Currently unused.</param>
    public ToCase_Model(SalesOrder_App Case_App)
    {
        // Parameter 'Case_App' is currently unused. If it is not required, consider removing it.
    }
    #endregion
}
#endregion

#region toContact_App
/// <summary>
/// Conversion to App model from Default model using inheritance 
/// </summary>
public class ToContact_App:Contact_App
{
    #region toContact_App
    public ToContact_App(Contact_Model Contact_Model)
    {

    }
    #endregion
}
#endregion

#region toContact_Model
/// <summary>
/// Conversion to Default model from App model using inheritance
/// </summary>
public class ToContact_Model:Contact_Model
{
    #region toContact_Model
    public ToContact_Model(SalesOrder_App Contact_App)
    {

    }
    #endregion
}
#endregion

#region Create_Contact_Model
/// <summary>
/// Create custom Default model from API Response, add new properties
/// potentially from other Default Models by constructor
/// </summary>
public class Create_Contact_Model:Contact
{
    #region Create_Contact_Model
    //ctor
    public Create_Contact_Model()
    { }
    public Create_Contact_Model(Contact sp_value)
    { }
    #endregion
}
#endregion

#region toOpportunity_App
/// <summary>
/// Conversion to App model from Default model using inheritance 
/// </summary>
public class ToOpportunity_App:Opportunity_App
{
    #region toOpportunity_App
    public ToOpportunity_App(Opportunity_Model Opportunity_Model)
    {

    }
    #endregion
}
#endregion

#region toOpportunity_Model
/// <summary>
/// Conversion to Default model from App model using inheritance
/// </summary>
public class ToOpportunity_Model:Opportunity_Model
{
    #region toOpportunity_Model
    public ToOpportunity_Model(SalesOrder_App Opportunity_App)
    {

    }
    #endregion
}
#endregion

#region Create_Opportunity_Model
/// <summary>
/// Create custom Default model from API Response, add new properties
/// potentially from other Default Models by constructor
/// </summary>
public class Create_Opportunity_Model:Opportunity
{
    #region Create_Opportunity_Model
    //ctor
    public Create_Opportunity_Model()
    { }
    public Create_Opportunity_Model(Opportunity sp_value)
    { }
    #endregion
}
#endregion

#region toSalesOrder_App
/// <summary>
/// Conversion to App model from Default model using inheritance 
/// </summary>
public class ToSalesOrder_App:SalesOrder_App
{
    #region toSalesOrder_App
    public ToSalesOrder_App(SalesOrder_Model salesOrder_Model)
    {
        OrderType = salesOrder_Model.OrderType.Value;
        OrderNbr = salesOrder_Model.OrderNbr.Value;
        Status = salesOrder_Model.Status.Value;
        Date = (DateTime)salesOrder_Model.Date.Value;
        CustomerID = salesOrder_Model.CustomerID.Value;
        CustomerName = salesOrder_Model.CustomerName.Value;
        OrderedQty = (decimal)salesOrder_Model.OrderedQty.Value;
        OrderTotal = (decimal)salesOrder_Model.OrderTotal.Value;
        CurrencyID = salesOrder_Model.CurrencyID.Value;
        ShipmentDate = (DateTime)salesOrder_Model.ShipmentDate.Value;
        LastModified = (DateTime)salesOrder_Model.LastModified.Value;
    }
    #endregion
}
#endregion

#region toSalesOrder_Model
/// <summary>
/// Conversion to Default model from App model using inheritance and
/// </summary>
public class ToSalesOrder_Model:SalesOrder_Model
{
    #region toSalesOrder_Model
    public ToSalesOrder_Model(SalesOrder_App salesOrder_App)
    {
        OrderType = salesOrder_App.OrderType;
        OrderNbr = salesOrder_App.OrderNbr;
        Status = salesOrder_App.Status;
        Date = salesOrder_App.Date;
        CustomerID = salesOrder_App.CustomerID;
        OrderedQty = salesOrder_App.OrderedQty;
        OrderTotal = salesOrder_App.OrderTotal;
        CurrencyID = salesOrder_App.CurrencyID;
        LastModified = salesOrder_App.LastModified;
        ShipmentDate = salesOrder_App.ShipmentDate;
        CustomerName = salesOrder_App.CustomerName;
    }
    #endregion
}
#endregion

#region Create_SalesOrder_Model
/// <summary>
/// Create custom Default model from API Response, add new properties
/// potentially from other Default Models by constructor
/// </summary>
public class Create_SalesOrder_Model:SalesOrder_Model
{
    #region Create_SalesOrder_Model
    //ctor
    public Create_SalesOrder_Model()
    { }
    public Create_SalesOrder_Model(SalesOrder so_value,DateTimeValue sp_value,StringValue ba_value)
    {
        OrderType = so_value.OrderType;
        OrderNbr = so_value.OrderNbr;
        Status = so_value.Status;
        Date = so_value.Date;
        CustomerID = so_value.CustomerID;
        CustomerName = ba_value;
        OrderedQty = so_value.OrderedQty;
        OrderTotal = so_value.OrderTotal;
        CurrencyID = so_value.CurrencyID;
        ShipmentDate = sp_value;
        LastModified = so_value.LastModified;
    }
    #endregion
}
#endregion

#region toShipment_App
/// <summary>
/// Conversion to App model from Default model using inheritance 
/// </summary>
public class ToShipment_App:Shipment_App
{
    #region toShipment_App
    public ToShipment_App(Shipment_Model shipment_Model)
    {

    }
    #endregion
}
#endregion

#region toShipment_Model
/// <summary>
/// Conversion to Default model from App model using inheritance
/// </summary>
public class ToShipment_Model:Shipment_Model
{
    #region toShipment_Model
    public ToShipment_Model(SalesOrder_App shipment_App)
    {

    }
    #endregion
}
#endregion

#region Create_Shipment_Model
/// <summary>
/// Create custom Default model from API Response, add new properties
/// potentially from other Default Models by constructor
/// </summary>
public class Create_Shipment_Model:Shipment
{
    #region Create_Shipment_Model
    //ctor
    public Create_Shipment_Model()
    { }
    public Create_Shipment_Model(Shipment sp_value)
    { }
    #endregion
}
#endregion

#region toShipmentDetail_App
/// <summary>
/// Conversion to App model from Default model using inheritance 
/// </summary>
public class ToShipmentDetail_App:ShipmentDetail_App
{
    #region toShipmentDetail_App
    public ToShipmentDetail_App(ShipmentDetail_Model ShipmentDetail_Model)
    {

    }
    #endregion
}
#endregion

#region toShipmentDetail_Model
/// <summary>
/// Conversion to Default model from App model using inheritance
/// </summary>
public class ToShipmentDetail_Model:ShipmentDetail_Model
{
    #region toShipmentDetail_Model
    public ToShipmentDetail_Model(SalesOrder_App ShipmentDetail_App)
    {

    }
    #endregion
}
#endregion

#region Create_ShipmentDetail_Model
/// <summary>
/// Create custom Default model from API Response, add new properties
/// potentially from other Default Models by constructor
/// </summary>
public class Create_ShipmentDetail_Model:ShipmentDetail
{
    #region Create_ShipmentDetail_Model
    //ctor
    public Create_ShipmentDetail_Model()
    { }
    public Create_ShipmentDetail_Model(ShipmentDetail sp_value)
    { }
    #endregion
}
#endregion

#endregion
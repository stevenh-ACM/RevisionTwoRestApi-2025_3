#nullable disable

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Home;

#region AboutModel
/// <summary>
/// Represents the model for the About page.
/// </summary>
[BindProperties]
public class AboutModel(ILogger<AboutModel> logger): PageModel
{
    #region ctor
    /// <summary>
    /// Initializes a new instance of the class with the specified database context.
    /// </summary>
    private readonly ILogger<AboutModel> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    #endregion ctor

    #region properties
    /// <summary>
    /// Gets or sets the number of employees to display.
    /// </summary>
    public int DisplayNumOfEmployees { get; set; }

    /// <summary>
    /// Gets or sets the total revenue to display.
    /// </summary>
    public double DisplayTotalRevenue { get; set; }

    /// <summary>
    /// Gets or sets the start year to display.
    /// </summary>
    public string DisplayStartYear { get; set; }
    #endregion

    #region methods
    /// <summary>
    /// Handles GET requests for the About page.
    /// </summary>
    public void OnGet()
    {
        _logger.LogInformation("Home/About Page");
        YearValue();
        EmpValue();
        RevValue();
    }

    /// <summary>
    /// Generates a random start year.
    /// </summary>
    /// <returns>The generated start year as a string.</returns>
    public string YearValue()
    {
        Random rand = new Random();
        return DisplayStartYear = (rand.Next(2,10) + 2008).ToString();
    }

    /// <summary>
    /// Generates a random number of employees.
    /// </summary>
    /// <returns>The generated number of employees.</returns>
    public int EmpValue()
    {
        Random rand = new Random();
        return DisplayNumOfEmployees = rand.Next(50,151);
    }

    /// <summary>
    /// Generates a random total revenue.
    /// </summary>
    /// <returns>The generated total revenue.</returns>
    public double RevValue()
    {
        Random rand = new Random();
        return DisplayTotalRevenue = Math.Round((rand.NextDouble() * 113) + 50,0);
    }
    #endregion
}
#endregion
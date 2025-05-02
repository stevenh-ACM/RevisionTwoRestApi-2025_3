#nullable disable

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Home;

[BindProperties]
public class AboutModel:PageModel
{
    private readonly ILogger<AboutModel> _logger;

    public AboutModel(ILogger<AboutModel> logger) => _logger = logger;

    public int DisplayNumOfEmployees { get; set; }
    public double DisplayTotalRevenue { get; set; }
    public string DisplayStartYear { get; set; }

    public void OnGet()
    {
        _logger.LogInformation("Home/About Page");
        YearValue();
        EmpValue();
        RevValue();
    }

    public string YearValue()
    {
        Random rand = new Random();
        return DisplayStartYear = (rand.Next(2,10) + 2008).ToString();
    }
    public int EmpValue()
    {
        Random rand = new Random();
        return DisplayNumOfEmployees = rand.Next(50,151);
    }
    public double RevValue()
    {
        Random rand = new Random();
        return DisplayTotalRevenue = Math.Round((rand.NextDouble() * 113) + 50,0);
    }
}


#nullable disable

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using RevisionTwoApp.RestApi.Data;
using RevisionTwoApp.RestApi.Models;

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Credentials;

public class IndexModel:PageModel
{
    #region ctor
    private readonly ILogger<IndexModel> _logger;
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context,ILogger<IndexModel> logger)
    {
        _context = context;
        _logger = logger;
    }
    #endregion

    public IList<Credential> Credentials { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Credentials = await _context.Credentials.ToListAsync();
        _logger.LogInformation("Credentials/Index Page");
    }
}
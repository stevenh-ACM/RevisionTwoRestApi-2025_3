#nullable disable

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using RevisionTwoApp.RestApi.Data;
using RevisionTwoApp.RestApi.Models;

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Credentials;
public class DetailsModel:PageModel
{
    private readonly AppDbContext _context;

    public DetailsModel(AppDbContext context)
    {
        _context = context;
    }

    public Credential Credentials { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if(id == null)
        {
            return NotFound();
        }

        var credentials = await _context.Credentials.FirstOrDefaultAsync(m => m.Id == id);
        if(credentials == null)
        {
            return NotFound();
        }
        else
        {
            Credentials = credentials;
        }
        return Page();
    }
}

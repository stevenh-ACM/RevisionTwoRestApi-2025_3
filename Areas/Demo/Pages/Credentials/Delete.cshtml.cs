#nullable disable

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using RevisionTwoApp.RestApi.Data;
using RevisionTwoApp.RestApi.Models;

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Credentials;
public class DeleteModel:PageModel
{
    private readonly AppDbContext _context;

    public DeleteModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if(id == null)
        {
            return NotFound();
        }

        var credential = await _context.Credentials.FindAsync(id);
        if(credential != null)
        {
            Credentials = credential;
            _context.Credentials.Remove(Credentials);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}

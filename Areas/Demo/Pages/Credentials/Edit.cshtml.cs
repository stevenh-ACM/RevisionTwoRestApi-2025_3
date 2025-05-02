#nullable disable

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using RevisionTwoApp.RestApi.Data;
using RevisionTwoApp.RestApi.Models;

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Credentials;
public class EditModel:PageModel
{
    private readonly AppDbContext _context;

    public EditModel(AppDbContext context)
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

        var credential = await _context.Credentials.FirstOrDefaultAsync(m => m.Id == id);
        if(credential == null)
        {
            return NotFound();
        }
        Credentials = credential;
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if(!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(Credentials).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch(DbUpdateConcurrencyException)
        {
            if(!CredentialExists(Credentials.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private bool CredentialExists(int id)
    {
        return _context.Credentials.Any(e => e.Id == id);
    }
}

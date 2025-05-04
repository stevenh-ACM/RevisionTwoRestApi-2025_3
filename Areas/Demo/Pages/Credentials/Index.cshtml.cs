﻿#nullable disable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using RevisionTwoApp.RestApi.Data;
using RevisionTwoApp.RestApi.Models;

namespace RevisionTwoApp.RestApi.Areas.Demo.Pages.Credentials;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS1587 // XML comment is not placed on a valid language element

[Authorize]

/// <summary>
/// Represents the model for the Credentials Index page.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="logger">The logger instance.</param>
public class IndexModel(AppDbContext context, ILogger<IndexModel> logger):PageModel
#pragma warning restore CS1587 // XML comment is not placed on a valid language element
{
    #region ctor   
    /// <summary>
    /// Initializes a new instance of the <see cref="IndexModel"/> class.
    /// </summary>
    private readonly ILogger<IndexModel> _logger = logger;
    private readonly AppDbContext _context = context;
    #endregion

    #region methods
    /// <summary>
    /// Gets or sets the list of credentials.
    /// </summary>
    public IList<Credential> Credentials { get; set; } = default!;

    /// <summary>
    /// Handles the GET request for the Credentials Index page.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task OnGetAsync()
    {
        Credentials = await _context.Credentials.ToListAsync();
        _logger.LogInformation("Credentials/Index Page");
    }
    #endregion
}

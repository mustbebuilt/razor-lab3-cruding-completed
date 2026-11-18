using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using rz.Data;
using rz.Models;

namespace rz.Pages.Staff;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CreateModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Models.Staff Staff { get; set; } = new();

    public IActionResult OnGet()
    {
        Staff = new Models.Staff
        {
            StartDate = DateTime.Today.ToString("yyyy-MM-dd"),
            IsActive = 1
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Staff.Add(Staff);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] =
            $"Staff member '{Staff.FirstName} {Staff.LastName}' was successfully added.";

        return RedirectToPage("/Staff/Index");
    }
}

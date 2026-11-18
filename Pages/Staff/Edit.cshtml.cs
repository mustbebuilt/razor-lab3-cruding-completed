using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using rz.Data;
using rz.Models;

namespace rz.Pages.Staff;

public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public EditModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Models.Staff Staff { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var staff = await _context.Staff.FindAsync(id);

        if (staff == null)
        {
            return NotFound();
        }

        Staff = staff;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(Staff).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Staff.Any(e => e.StaffId == Staff.StaffId))
            {
                return NotFound();
            }

            throw;
        }

        TempData["SuccessMessage"] =
            $"Staff record for '{Staff.FirstName} {Staff.LastName}' was successfully updated.";

        return RedirectToPage("/Staff/Index");
    }
}

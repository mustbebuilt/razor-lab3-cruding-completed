using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using rz.Data;
using rz.Models;

namespace rz.Pages.Staff;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
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

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var staff = await _context.Staff.FindAsync(id);

        if (staff != null)
        {
            _context.Staff.Remove(staff);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] =
                $"Staff member '{staff.FirstName} {staff.LastName}' was permanently deleted.";
        }

        return RedirectToPage("/Staff/Index");
    }
}

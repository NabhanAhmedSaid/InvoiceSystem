using System.Diagnostics;
using Ganss.Xss;
using Microsoft.AspNetCore.Mvc;
using invoices.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace invoices.Controllers;

public class HomeController : Controller
{
    private readonly ContactsDbContext _context;

    public HomeController(ContactsDbContext context)
    {
        _context = context;
    }

    public IActionResult AddContact()
    {
        return View();
    }

    public IActionResult CreateInvoice()
    {
        ViewBag.Contacts = new SelectList(_context.Contacts, "Phone", "Phone");
        return View();
    }
    
    [HttpPost]
    
    public async Task<IActionResult> CreateInvoice(InvoiceViewModel invoiceVM)
    {
        if (ModelState.IsValid)
        {
            var invoice = new Invoice()
            {
                Phone = invoiceVM.Phone,
                Amount =  invoiceVM.Amount,
                Company =  invoiceVM.Company,
                Notes =  invoiceVM.Notes,
                InvoiceDate =  invoiceVM.InvoiceDate,
                Invoicenumber =  invoiceVM.Invoicenumber,
              

            };
            _context.Add(invoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Products = new SelectList(_context.Contacts, "Id", "Name", invoiceVM.Phone);
        return View("Index");
    }
    public IActionResult Contact(int id)
    {
        var contact = _context.Contacts.FirstOrDefault(c => c.Id == id);
        return View(contact);
    }
    public IActionResult Contacts()
    {
        var contacts = _context.Contacts.ToList();
        return View(contacts);
    }
    public IActionResult Invoices()
    {
        var contacts = _context.Invoices.ToList();
        return View(contacts);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateContacts(ContactViewModel contactVM)
    {
        var sanitizer = new HtmlSanitizer();
        
        if (ModelState.IsValid)
        {
            var contact = new Contact()
            {
                Name =sanitizer.Sanitize( contactVM.Name),
                Phone = contactVM.Phone,
                City = contactVM.City,
                Governorate = contactVM.Governorate,

            };
            try
            {
                _context.Contacts.Add(contact);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log without exposing sensitive data
                //_logger.LogError(ex, "Error creating contact");

                ModelState.AddModelError("", "حدث خطأ أثناء الحفظ");
                return RedirectToAction("Index");
            }
          
          
        }
        return RedirectToAction("Index");
    }

    
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

using System.Diagnostics;
using Ganss.Xss;
using Microsoft.AspNetCore.Mvc;
using invoices.Models;
using invoices.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace invoices.Controllers;

public class HomeController : Controller
{
    private readonly IInvoicesRepository _repo;

    public HomeController(IInvoicesRepository repo)
    {
        _repo = repo;
    }
    

    public IActionResult AddContact()
    {
        return View();
    }

    public IActionResult CreateInvoice()
    {
        var contacts = _repo.Contacts();
        ViewBag.Contacts = new SelectList(contacts, "Phone", "Phone");
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
            await _repo.AddAsyncInvoices(invoice);
            await _repo.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
        var contacts = _repo.Contacts();
    
        ViewBag.Products = new SelectList(contacts, "Id", "Name", invoiceVM.Phone);
        return View("Index");
    }
    public IActionResult Contact(int id)
    {
        var contact = _repo.Contact(id);
        return View(contact);
    }
    public IActionResult Contacts()
    {
        var contacts = _repo.Contacts();
        return View(contacts);
    }
    public IActionResult Invoices()
    {
        var contacts = _repo.Invoices();
        return View(contacts);
    }
    // [HttpPost]
    // [ValidateAntiForgeryToken]
    // public IActionResult CreateContacts(ContactViewModel contactVM)
    // {
    //     var sanitizer = new HtmlSanitizer();
    //     
    //     if (ModelState.IsValid)
    //     {
    //         var contact = new Contact()
    //         {
    //             Name =sanitizer.Sanitize( contactVM.Name),
    //             Phone = contactVM.Phone,
    //             City = contactVM.City,
    //             Governorate = contactVM.Governorate,
    //
    //         };
    //         try
    //         {
    //             _context.Contacts.Add(contact);
    //             _context.SaveChanges();
    //         }
    //         catch (Exception ex)
    //         {
    //             // Log without exposing sensitive data
    //             //_logger.LogError(ex, "Error creating contact");
    //
    //             ModelState.AddModelError("", "حدث خطأ أثناء الحفظ");
    //             return RedirectToAction("Index");
    //         }
    //       
    //       
    //     }
    //     return RedirectToAction("Index");
    // }
    //
    //
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

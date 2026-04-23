using System.Diagnostics;
using Ganss.Xss;
using Microsoft.AspNetCore.Mvc;
using invoices.Models;
using invoices.Repositories;
using invoices.Service;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace invoices.Controllers;

public class HomeController : Controller
{
    private readonly IInvoiceService _service;
    private readonly IInvoicesRepository _repo;

    public HomeController(IInvoicesRepository repo, IInvoiceService service)
    {
        _service = service;
        _repo = repo;
    }
    

    public IActionResult AddContact()
    {
        return View();
    }

    public IActionResult CreateInvoice()
    {
        var contacts = _repo.Contacts().OrderBy(c=>c.Phone).ToList();
        ViewBag.Contacts = new SelectList(contacts, "Phone", "Phone");
        return View();
    }
    
    [HttpPost]
    
    public async Task<IActionResult> CreateInvoice(InvoiceViewModel invoiceVM)
    {
        if (ModelState.IsValid)
        {
            _service.CreateInvoice(invoiceVM);
        }
        var contacts = _repo.Contacts();
    
        ViewBag.Products = new SelectList(contacts, "Id", "Name", invoiceVM.Phone);
        return View("Index");
    }
    public IActionResult Contact(int id)
    {
        var contact = _service.Contact(id);
        return View(contact);
    }
    public IActionResult Contacts()
    {
        var contacts = _service.Contacts();
        return View(contacts);
    }
    public IActionResult Invoices()
    {
        var contacts = _service.Invoices();
        return View(contacts);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateContacts(ContactViewModel contactVM)
    {
        var sanitizer = new HtmlSanitizer();
        
        if (ModelState.IsValid)
        {
           
            try
            {
                _service.CreateContacts(contactVM);
                return RedirectToAction("Index");
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
    public IActionResult SearchPage(string query)
    {
        var result = _service.SearchContact(query);
        return View(result);
    }
}

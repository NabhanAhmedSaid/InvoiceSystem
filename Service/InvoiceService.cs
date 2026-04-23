using Ganss.Xss;
using invoices.Models;
using invoices.Repositories;

namespace invoices.Service;

public class InvoiceService : IInvoiceService
{
    private readonly IInvoicesRepository _repo;

    public InvoiceService(IInvoicesRepository repo)
    {
        _repo = repo;
    }
    public Contact Contact(int id)
    {
        return _repo.Contact(id);
    }

    public List<Contact> Contacts()
    {
        return _repo.Contacts();
        
    }

    public List<Contact> Invoices()
    {
       return _repo.Invoices();
    }

    public async Task CreateInvoice(InvoiceViewModel invoiceVM)
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
        
    }

    public Contact CreateContacts(ContactViewModel contactVM)
    {
        var sanitizer = new HtmlSanitizer();
        var contact = new Contact()
        {
            Name =sanitizer.Sanitize( contactVM.Name),
            Phone = contactVM.Phone,
            City = contactVM.City,
            Governorate = contactVM.Governorate,
    
        };
         try
            {
                _repo.Add(contact);
                _repo.Save();
                return contact;
            }
            catch (Exception ex)
            {
                // Log without exposing sensitive data
                //_logger.LogError(ex, "Error creating contact");

                throw;
            }
        
    }

    public List<Contact> SearchContact(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return new List<Contact>();

        return _repo.Search(query);
    }
}
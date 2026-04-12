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
}
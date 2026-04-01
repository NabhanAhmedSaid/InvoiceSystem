using invoices.Models;

namespace invoices.Repositories;

public class InvoicesRepository: IInvoicesRepository
{
    private readonly ContactsDbContext _context;

    public InvoicesRepository(ContactsDbContext context)
    {
        _context = context;
    }
    public Contact Contact(int id)
    {
        var contact = _context.Contacts.FirstOrDefault(c => c.Id == id);
        return contact;    }

    public List<Contact> Contacts()
    {
       return _context.Contacts.ToList();
    }

    public List<Invoice> Invoices()
    {
      return  _context.Invoices.ToList();
    }

   
    public async Task AddAsyncInvoices(Invoice invoice)
    {
        await _context.Invoices.AddAsync(invoice);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
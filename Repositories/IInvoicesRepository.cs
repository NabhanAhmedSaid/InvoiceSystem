using invoices.Models;

namespace invoices.Repositories;

public interface IInvoicesRepository
{
   Contact Contact(int id);
   List<Contact> Contacts();
   List<Invoice> Invoices();
   
   Task AddAsyncInvoices(Invoice invoice);
   Task SaveAsync();
}
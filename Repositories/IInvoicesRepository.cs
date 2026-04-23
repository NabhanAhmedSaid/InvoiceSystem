using invoices.Models;

namespace invoices.Repositories;

public interface IInvoicesRepository
{
   Contact Contact(int id);
   List<Contact> Contacts();
   List<Contact> Invoices();
   
   Task AddAsyncInvoices(Invoice invoice);
   Task SaveAsync();
   void Add(Contact contact);
   void Save();
   List<Contact> Search(string query);
}
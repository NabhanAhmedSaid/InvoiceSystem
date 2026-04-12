using invoices.Models;

namespace invoices.Service;

public interface IInvoiceService
{
    Contact Contact(int id);
    List<Contact> Contacts();
    List<Contact> Invoices();

}
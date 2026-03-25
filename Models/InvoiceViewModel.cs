using System;
using System.Collections.Generic;

namespace invoices.Models;

public partial class InvoiceViewModel
{
    public int Invoicenumber { get; set; }

    public string? Company { get; set; }

    public DateOnly? InvoiceDate { get; set; }

    public decimal? Amount { get; set; }

    public string? Notes { get; set; }

    public string? Phone { get; set; }


}

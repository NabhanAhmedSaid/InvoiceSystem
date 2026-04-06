using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace invoices.Models;

public partial class InvoiceViewModel
{
    public int Invoicenumber { get; set; }

    public string? Company { get; set; }

    public DateOnly? InvoiceDate { get; set; }

    [Required]
    [Range(0, 9999999.999, ErrorMessage = "القيمة يجب أن تكون أقل من 9,999,999.999")]
    public decimal? Amount { get; set; }

    public string? Notes { get; set; }

    public string? Phone { get; set; }


}

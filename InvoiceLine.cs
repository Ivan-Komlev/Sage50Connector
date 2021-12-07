/*
 * Sage50 - Customers API Server
 * This application runs as a webserver and provides the data from Sage50 database in JSON format.
 * @author Ivan komlev <ivankomlev@gmail.com>
 * @github https://github.com/Ivan-Komlev/Sage50Connector
 * @copyright Copyright(C) 2020.All Rights Reserved
 * @license GNU/GPL Version 2 or later - http://www.gnu.org/licenses/gpl-2.0.html
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sage.Peachtree.API;
using Sage.Peachtree.API.Collections.Generic;

public struct InvoiceLine
{
    public decimal Amount;
    public decimal AmountPaid;
    public string Description;
    public decimal DiscountAmount;
    public string InvoiceKey;

    public InvoiceLine(decimal Amount_, decimal AmountPaid_)
    {
        Amount = Amount_;
        AmountPaid = AmountPaid_;
        Description = "";
        DiscountAmount = 0;
        InvoiceKey = "";
    }
}

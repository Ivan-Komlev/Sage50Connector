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

public struct InvoiceDetails
{
    public decimal Amount;
    public decimal AmountDue;
    public string Date;
    public string DateDue;
    public decimal DiscountAmount;
    public string DiscountDate;
    public string ReferenceNumber;

    public InvoiceDetails(decimal Amount_, decimal AmountDue_)
    {
        Amount = Amount_;
        AmountDue = AmountDue_;
        Date = "";
        DateDue = "";
        DiscountAmount = 0;
        DiscountDate = "";
        ReferenceNumber = "";
    }
}

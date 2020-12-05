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

public struct CustomerDetails
{
        public string ID;
        public string AccountNumber;

        public decimal Balance;
        public string Email;

        public decimal LastInvoiceAmount;
        public string LastInvoiceDate;
        public string Name;
        public Sage.Peachtree.API.PhoneNumberCollection PhoneNumbers;
        public decimal LastPaymentAmount;
        public string LastPaymentDate;
        public string PaymentMethod;
        public string CustomerSince;
        //var CustomFieldValues;
        public decimal AverageDaysToPayInvoices;
        //BillToContact;
        public string Category;
        public string LastStatementDate;

        public CustomerDetails(string ID_)
        {
            ID = ID_;
            AccountNumber = "";


            Balance = 0;
            Email = "";
        
            LastInvoiceAmount = 0;
            LastInvoiceDate = "";
            Name = "";
            PhoneNumbers = null;
            LastPaymentAmount = 0;
            LastPaymentDate = "";
            PaymentMethod = "";
            CustomerSince = "";;
        
            AverageDaysToPayInvoices = 0;
        
            Category = "";
            LastStatementDate = "";
        }

    }

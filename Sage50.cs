/*
 * Sage50 - Customers API Server
 * This application runs as a webserver and provides the data from Sage50 database in JSON format.
 * @author Ivan komlev <ivankomlev@gmail.com>
 * @github https://github.com/Ivan-Komlev/Sage50Connector
 * @copyright Copyright(C) 2020.All Rights Reserved
 * @license GNU/GPL Version 2 or later - http://www.gnu.org/licenses/gpl-2.0.html
*/

using Sage.Peachtree.API;
using Sage.Peachtree.API.Collections.Generic;
using System;
using System.Linq;
using System.Web.Script.Serialization;
using System.Collections.Generic;

namespace Sage50
{
    public class Sage50
    {
        public AuthorizationResult connectionStatus;
        private PeachtreeSession session;
        private configuration config;

        public Sage50(configuration config_)
        {
            config = config_;
        }

        public string getCompany(string serverName, string databaseName)
        {
            CompanyIdentifier companyIdentifier = session.LookupCompanyIdentifier(serverName, databaseName);
            var json = new JavaScriptSerializer().Serialize(companyIdentifier);
            return json.ToString();
        }
        
        public string getCompanies()
        {
            CompanyIdentifierList companiesList = session.CompanyList();
            var json = new JavaScriptSerializer().Serialize(companiesList);
            return json.ToString();
        }

        public string findCompany(string companyName)
        {
            CompanyIdentifierList companiesList = session.CompanyList();

            List<CompanyIdentifier> Identifiers = new List<CompanyIdentifier>();

            foreach (CompanyIdentifier companyIdentifier in companiesList)
            {
                if (companyIdentifier.CompanyName.Contains(companyName)) //"PRE-ESCOLAR OXFORD"
                {
                    Identifiers.Add(companyIdentifier);
                }
            }

            var json = new JavaScriptSerializer().Serialize(Identifiers);
            return json.ToString();
        }

        public string countCustomers(string serverName, string databaseName)
        {
            CompanyIdentifier companyIdentifier = session.LookupCompanyIdentifier(serverName, databaseName);

            connectionStatus = session.RequestAccess(companyIdentifier);
            Console.WriteLine("Result {0}", connectionStatus.ToString());

            if (connectionStatus == AuthorizationResult.Granted)
            {
                Company company = session.Open(companyIdentifier);
                Console.WriteLine("Company {0}", company.CompanyIdentifier.CompanyName);
                var customers = company.Factories.CustomerFactory.List();
                FilterExpression filter = FilterExpression.Equal(
                FilterExpression.Property("Customer.IsInactive"),
                FilterExpression.Constant(false));
                LoadModifiers modifiers = LoadModifiers.Create();
                modifiers.Filters = filter;
                customers.Load(modifiers);

                int count = customers.Count;
                session.Close(company);

                return "{\"count\":\"" + count + "\"}";
            }

            return "{\"error\":\"Access: " + connectionStatus.ToString() + "\"}";
        }

        public string getCustomers(string serverName, string databaseName, int page)
        {
            CompanyIdentifier companyIdentifier = session.LookupCompanyIdentifier(serverName, databaseName);
            
            connectionStatus = session.RequestAccess(companyIdentifier);
            Console.WriteLine("Result {0}", connectionStatus.ToString());

            if (connectionStatus == AuthorizationResult.Granted)
            {
                    Company company = session.Open(companyIdentifier);
                    Console.WriteLine("Company {0}", company.CompanyIdentifier.CompanyName);
                    var customers = company.Factories.CustomerFactory.List();
                    FilterExpression filter = FilterExpression.Equal(
                    FilterExpression.Property("Customer.IsInactive"),
                    FilterExpression.Constant(false));
                    LoadModifiers modifiers = LoadModifiers.Create();
                    modifiers.Filters = filter;
                    customers.Load(modifiers);
                    var cs = customers.ToList();

                    List<CustomerDetails> customerDetailsList = new List<CustomerDetails>();

                int start_index = page * config.recordsPerPage;

                for (int index = start_index; index < start_index + config.recordsPerPage; index++)
                {
                    if (index >= cs.Count)
                        break;

                    Customer customer = cs[index];

                    CustomerDetails customerDetails = new CustomerDetails(customer.ID)
                    {
                        AccountNumber = customer.AccountNumber,
                        Balance = customer.Balance,
                        Email = customer.Email,
                        LastInvoiceAmount = customer.LastInvoiceAmount,
                        
                        Name = customer.Name,

                        LastPaymentAmount = customer.LastPaymentAmount,
                        
                        PaymentMethod = customer.PaymentMethod,
                        
                        
                        Category = customer.Category
                        
                    };

                    if (customer.AverageDaysToPayInvoices != null)
                        customerDetails.AverageDaysToPayInvoices = (decimal)customer.AverageDaysToPayInvoices;

                    customerDetails.PhoneNumbers = customer.PhoneNumbers;

                    if (customer.CustomerSince != null)
                        customerDetails.CustomerSince = customer.CustomerSince.ToString();

                    if (customer.LastPaymentDate!=null)
                        customerDetails.LastPaymentDate = customer.LastPaymentDate.ToString();

                    if (customer.LastStatementDate != null)
                        customerDetails.LastStatementDate = customer.LastStatementDate.ToString();

                    if (customer.LastInvoiceDate != null)
                        customerDetails.LastInvoiceDate = customer.LastInvoiceDate.ToString();

                    customerDetailsList.Add(customerDetails);
                }

                session.Close(company);

                var json = new JavaScriptSerializer().Serialize(customerDetailsList);
                return json.ToString();
            }

            

            return "{\"error\":\"Access: " + connectionStatus.ToString() + "\"}";
        }

        public void Connect2Sage50()
        {
            connectionStatus = AuthorizationResult.None;

            Console.WriteLine("Starting session");
            session = new PeachtreeSession();

            session.Begin(config.applicationIdentifier);

            Console.WriteLine("Session started");
        }
    }
}
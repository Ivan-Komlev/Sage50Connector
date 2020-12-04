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

        public string getCustomers(string serverName, string databaseName)
        {
            CompanyIdentifier companyIdentifier = session.LookupCompanyIdentifier(serverName, databaseName);
            
            connectionStatus = session.RequestAccess(companyIdentifier);
            Console.WriteLine("Result {0}", connectionStatus.ToString());

            if (connectionStatus == AuthorizationResult.Granted)
            {
                    Company company = session.Open(companyIdentifier);
                    Console.WriteLine("Company test {0}", company.CompanyIdentifier.CompanyName);
                    var customers = company.Factories.CustomerFactory.List();
                    FilterExpression filter = FilterExpression.Equal(
                    FilterExpression.Property("Customer.IsInactive"),
                    FilterExpression.Constant(false));
                    LoadModifiers modifiers = LoadModifiers.Create();
                    modifiers.Filters = filter;
                    customers.Load(modifiers);
                    var cs = customers.ToList();
                    foreach (Customer customer in cs)
                    {
                        Console.WriteLine("Name {0} ID {1}", customer.Name, customer.ID);

                        var json = new JavaScriptSerializer().Serialize(customer);
                        return json.ToString();
                    }
                }

            return "{\"error\":\"Access: " + connectionStatus.ToString() + "\"}";
        }

        public void Connect2Sage50()
        {
            connectionStatus = AuthorizationResult.None;

            Console.WriteLine("Starting session");
            session = new PeachtreeSession();
            session.Begin("gTSHuV5yGO7JWy8T/KGEmZBg+iPuDMYSqyznuxS4kSoRysz//Bqctg==Apd+xI0DANnYL47i+jTPPo6dbS/Ifveze27P+jgTJJginl7DTolyii6iw1KE+i4ttMsKJRBgX0fRpvEHEefMl4KRaUCRwEXh4Bz4hOzNZzT7DgA7Uo7DrAjGW6Pk0mYgqHAjlH9U+rK0V4OclHn0Lx96SAPxxp/7ktL/5tM4V+S4MXTeHsbtAwlXthZ8fDJh9AojWGV64HaGjriOStfoc05pW7Luj6k4yIZeKEFfcZqyp4Pz3RCZlAfzhIFzbi6U");

            Console.WriteLine("Session started");
        }
    }
}
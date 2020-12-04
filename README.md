# Sage50Connector
Sage50 - Customers API Server.
This application runs as a webserver and provides the data from Sage50 database in JSON format.

Usage example:
------------------------------------------------

Get list of companies from Sage50 server:

params:
query=companies

http://localhost:8080/?query=companies

Response in JSON

[{"Guid":"72eb7347-e176-47e1-9bc3-3cccc2a678e3","DatabaseName":"oxfordsms","Path":"c:\\sage\\peachtree\\company\\oxfordsms\\panama\\","CompanyName":"OxfordSMS, S. A.","ServerName":"SVRDEV","SchemaVersion":"29.1"},{"Guid":"3223b7347-43r6-47e1-934rc3-3cccc2a678e3","DatabaseName":"oxfordsms2","Path":"c:\\sage\\peachtree\\company\\oxfordsms2\\panama\\","CompanyName":"OxfordSMS, S. A. (backup)","ServerName":"SVRDEV","SchemaVersion":"29.1"}]

------------------------------------------------

Get list single company:

params:
query=company
server=SVRDEV
database=oxfordsms

http://localhost:8080/?query=company&server=SVRDEV&database=oxfordsms

Response in JSON

[{"Guid":"72eb7347-e176-47e1-9bc3-3cccc2a678e3","DatabaseName":"oxfordsms","Path":"c:\\sage\\peachtree\\company\\oxfordsms\\panama\\","CompanyName":"OxfordSMS, S. A.","ServerName":"SVRDEV","SchemaVersion":"29.1"}]

------------------------------------------------

Find companies:

params:
query=findcompany
name=Oxford

http://localhost:8080/?query=company&server=SVRRDP&database=newpreescolaroxford2

Response in JSON

[{"Guid":"72eb7347-e176-47e1-9bc3-3cccc2a678e3","DatabaseName":"oxfordsms","Path":"c:\\sage\\peachtree\\company\\oxfordsms\\panama\\","CompanyName":"OxfordSMS, S. A.","ServerName":"SVRDEV","SchemaVersion":"29.1"}]



------------------------------------------------

Get number of customers:

params:
query=countcustomers
server=SVRDEV
database=oxfordsms

http://localhost:8080/?query=countcustomers&server=SVRRDP&database=newpreescolaroxford2

Response in JSON

{"count":"2"}

------------------------------------------------

Get list of customers:

params:
query=customers
server=SVRDEV
database=oxfordsms
page=from 0 to number_of_customers / 100 (100 records per page)

http://localhost:8080/?query=customers&server=SVRDEV&database=oxfordsms

Response in JSON

[{"Number":"","Key":3}],"LastPaymentAmount":2000.0000000000000000000,"LastPaymentDate":"\/Date(1487221200000)\/","PaymentMethod":"2CHECKOUT","CustomerSince":"\/Date(1487221200000)\/","AverageDaysToPayInvoices":0,"Category":"","LastStatementDate":"\/Date(-62135578800000)\/"},{"ID":"JOHN-SMITH-1","AccountNumber":"","Balance":0.0000000000000000000,"Email":"info@oxfordsms.com","LastInvoiceAmount":0.0000000000000000000,"LastInvoiceDate":"\/Date(-62135578800000)\/","Name":"SMITH,JOHN","PhoneNumbers":[{"Number":"6084-8737","Key":1},{"Number":"","Key":2},{"Number":"","Key":3}],"LastPaymentAmount":0.0000000000000000000,"LastPaymentDate":"\/Date(-62135578800000)\/","PaymentMethod":"2CHECKOUT","CustomerSince":"\/Date(1491800400000)\/","AverageDaysToPayInvoices":0,"Category":"","LastStatementDate":"\/Date(-62135578800000)\/"},{"ID":"REED MARIA-NU-1","AccountNumber":"","Balance":426.0000000000000000000,"Email":"support@oxfordsms.com","LastInvoiceAmount":213.0000000000000000000,"LastInvoiceDate":"\/Date(1606798800000)\/","Name":"READ MARIA","PhoneNumbers":[{"Number":"6084-8737","Key":1},{"Number":"","Key":2}]




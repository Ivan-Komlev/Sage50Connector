# Sage50Connector
Sage50 - Customers API Server.
This application runs as a webserver and provides the data from Sage50 database in JSON format.

--------------
config.ini file:

port=8080<br/>
applicationIdentifier=Sage50 partner application identifier<br/>
APIKey=MYAPIKEY123 -- a key to be used with every query<br/>
Password=b14ca589814e4133bbce2ea2315a1916  -- 32byte ecryptonion key<br/>
host=http://localhost -- host to listen

Usage example:
------------------------------------------------

Get list of companies from Sage50 server:

params:
apikey=MYAPIKEY123
query=companies

optional param:
encrypt=1   - 1 to encrypt the output, 0 not to encrypt

http://localhost:8080/?query=companies&apikey=MYAPIKEY123&encrypt=1 

Response in JSON

[{"Guid":"72eb7347-e176-47e1-9bc3-3cccc2a678e3","DatabaseName":"oxfordsms","Path":"c:\\sage\\peachtree\\company\\oxfordsms\\panama\\","CompanyName":"OxfordSMS, S. A.","ServerName":"SVRDEV","SchemaVersion":"29.1"},{"Guid":"3223b7347-43r6-47e1-934rc3-3cccc2a678e3","DatabaseName":"oxfordsms2","Path":"c:\\sage\\peachtree\\company\\oxfordsms2\\panama\\","CompanyName":"OxfordSMS, S. A. (backup)","ServerName":"SVRDEV","SchemaVersion":"29.1"}]

------------------------------------------------

Get single company details:

params:
apikey=MYAPIKEY123
query=company,
server=SVRDEV,
database=oxfordsms

optional param:
encrypt=1   - 1 to encrypt the output, 0 not to encrypt

http://localhost:8080/?query=company&server=SVRDEV&database=oxfordsms&apikey=MYAPIKEY123&encrypt=0

Response in JSON

[{"Guid":"72eb7347-e176-47e1-9bc3-3cccc2a678e3","DatabaseName":"oxfordsms","Path":"c:\\sage\\peachtree\\company\\oxfordsms\\panama\\","CompanyName":"OxfordSMS, S. A.","ServerName":"SVRDEV","SchemaVersion":"29.1"}]

------------------------------------------------

Find companies:

params:
apikey=MYAPIKEY123
query=findcompany,
name=Oxford

optional param:
encrypt=1   - 1 to encrypt the output, 0 not to encrypt

http://localhost:8080/?query=company&server=SVRRDP&database=newpreescolaroxford2&apikey=MYAPIKEY123

Response in JSON

[{"Guid":"72eb7347-e176-47e1-9bc3-3cccc2a678e3","DatabaseName":"oxfordsms","Path":"c:\\sage\\peachtree\\company\\oxfordsms\\panama\\","CompanyName":"OxfordSMS, S. A.","ServerName":"SVRDEV","SchemaVersion":"29.1"}]



------------------------------------------------

Get number of customers:

params:
apikey=MYAPIKEY123
query=countcustomers,
server=SVRDEV,
database=oxfordsms

optional param:
encrypt=1   - 1 to encrypt the output, 0 not to encrypt

http://localhost:8080/?query=countcustomers&server=SVRRDP&database=newpreescolaroxford2&apikey=MYAPIKEY123

Response in JSON

{"count":"2"}

If the access to specified database is not granted you will get an error message like this:

{"error":"Access: Pending"}



------------------------------------------------

Get list of customers:

params:
apikey=MYAPIKEY123
query=customers,
server=SVRDEV,
database=oxfordsms,
page=from 0 to number_of_customers / 100 (100 records per page)

optional param:
encrypt=1   - 1 to encrypt the output, 0 not to encrypt

http://localhost:8080/?query=customers&server=SVRDEV&database=oxfordsms&apikey=MYAPIKEY123&encrypt=1

Response in JSON

[{"Number":"","Key":3}],"LastPaymentAmount":2000.0000000000000000000,"LastPaymentDate":"\/Date(1487221200000)\/","PaymentMethod":"2CHECKOUT","CustomerSince":"\/Date(1487221200000)\/","AverageDaysToPayInvoices":0,"Category":"","LastStatementDate":"\/Date(-62135578800000)\/"},{"ID":"JOHN-SMITH-1","AccountNumber":"","Balance":0.0000000000000000000,"Email":"info@oxfordsms.com","LastInvoiceAmount":0.0000000000000000000,"LastInvoiceDate":"\/Date(-62135578800000)\/","Name":"SMITH,JOHN","PhoneNumbers":[{"Number":"6084-8737","Key":1},{"Number":"","Key":2},{"Number":"","Key":3}],"LastPaymentAmount":0.0000000000000000000,"LastPaymentDate":"\/Date(-62135578800000)\/","PaymentMethod":"2CHECKOUT","CustomerSince":"\/Date(1491800400000)\/","AverageDaysToPayInvoices":0,"Category":"","LastStatementDate":"\/Date(-62135578800000)\/"},{"ID":"REED MARIA-NU-1","AccountNumber":"","Balance":426.0000000000000000000,"Email":"support@oxfordsms.com","LastInvoiceAmount":213.0000000000000000000,"LastInvoiceDate":"\/Date(1606798800000)\/","Name":"READ MARIA","PhoneNumbers":[{"Number":"6084-8737","Key":1},{"Number":"","Key":2}]



------------------------------------------------

Get list of invoices:

params:
apikey=MYAPIKEY123
server=SVRDEV,
database=oxfordsms,
query=invoices,
encrypt=1   - 1 to encrypt the output, 0 not to encrypt
customer=CUSTOMRER ID,
datefrom=DATE FROM (iso8601) Example:20080501T08:30:52
dateto==DATE TO (iso8601) Example:20220501T08:30:52

http://localhost:8080/?query=invoices&server=SVRDEV&database=oxfordsms&apikey=MYAPIKEY123&encrypt=1&datefrom=20080501T08:30:52&dateto=20220501T08:30:52

Response in JSON

[{"Amount":60.0000000000000000000,"AmountDue":0.0000000000000000000,"Date":"5/10/2021 12:00:00 AM","DateDue":"6/9/2021 12:00:00 AM","DiscountAmount":0.0000000000000000000,"DiscountDate":"5/10/2021 12:00:00 AM"}]



------------------------------------------------

Get list of receipts:

params:
apikey=MYAPIKEY123
server=SVRDEV,
database=oxfordsms,
query=receipts,
encrypt=1   - 1 to encrypt the output, 0 not to encrypt
customer=CUSTOMRER ID,
datefrom=DATE FROM (iso8601) Example:20080501T08:30:52
dateto==DATE TO (iso8601) Example:20220501T08:30:52

http://localhost:8080/?query=receipts&server=SVRDEV&database=oxfordsms&apikey=MYAPIKEY123&encrypt=1&datefrom=20080501T08:30:52&dateto=20220501T08:30:52

Response in JSON

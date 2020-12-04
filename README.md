# Sage50Connector
Sage50 - Customers API Server
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

Get list of customers:

params:
query=customers
server=SVRDEV
database=oxfordsms

http://localhost:8080/?query=customers&server=SVRDEV&database=oxfordsms

Response in JSON

[] - Not implemented yet




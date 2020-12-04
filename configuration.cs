/*
 * Sage50 - Customers API Server
 * This application runs as a webserver and provides the data from Sage50 database in JSON format.
 * @author Ivan komlev <ivankomlev@gmail.com>
 * @github https://github.com/Ivan-Komlev/Sage50Connector
 * @copyright Copyright(C) 2020.All Rights Reserved
 * @license GNU/GPL Version 2 or later - http://www.gnu.org/licenses/gpl-2.0.html
*/

using System;
using System.IO;
public struct configuration
{
    public int port;
    public string host;
    public string applicationIdentifier;
    public string APIKey;
    public string EncriptionPassword;

    public configuration(string filename)
    {
        port = 80;
        host = "";
        applicationIdentifier = "";
        APIKey = "";
        EncriptionPassword = "";

        if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "/" + filename))
        {
            string[] lines = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + filename);
            foreach (string line in lines)
            {
                string[] params_ = line.Split('=');
                if (params_.Length > 1)
                {
                    string paramName = params_[0].Trim().ToLower();

                    //Value may contain "=" character, example APYKey=askjdlkjh=asdkj;lja
                    //Concat the value back to one string
                    string value = "";
                    for(int i = 1; i< params_.Length;i++)
                    {
                        if (value != "")
                            value += "=";

                        value += params_[i];
                    }

                    switch (paramName)
                    {
                        case "port":
                            port = int.Parse(value);
                            break;

                        case "host":
                            host = value.Trim();
                            break;

                        case "applicationidentifier":
                            applicationIdentifier = value.Trim();
                            break;

                        case "apikey":
                            APIKey = value.Trim();
                            break;

                        case "encriptionpassword":
                            EncriptionPassword = value.Trim();
                            break;

                    }
                }
            }
        }
    }
}
﻿///
/// falkonry-csharp-client
/// Copyright(c) 2016 Falkonry Inc
/// MIT Licensed
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Net.Http;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net.Http.Headers;
using falkonry_csharp_client.helper.models;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using falkonry_csharp_client.src.helper.models;
using System.Collections.Specialized;

namespace falkonry_csharp_client.service
{
    public class HttpService
    {
        private string host;
        private string token;
        private string user_agent="falkonry/csharp-client";
        public bool RemoteCertificateValidationCallback(

                  Object sender,

                  X509Certificate certificate,

                  X509Chain chain,

                  SslPolicyErrors sslPolicyErrors

           )
        { return true; }


        public HttpService(string host, string token)
        {
          
          
            this.host = host == null ? "https://service.falkonry.io" : host;

            this.token = token;
           
        ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidationCallback;
        }

        public string get (string path)
        {
            try 
            {   
                var url = this.host + path;
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.ServicePoint.Expect100Continue = false;
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Headers.Add("Authorization", "Bearer "+this.token);
			    request.Method = "GET";
                request.ContentType = "application/json";
                
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                
                string resp = new StreamReader(response.GetResponseStream()).ReadToEnd();

                if ( Convert.ToInt32(response.StatusCode) == 401 )
                {
                    return "Unauthorized : Invalid token " + Convert.ToString(response.StatusCode);
                }
                else if ( Convert.ToInt32(response.StatusCode) >= 400 )
                {
                    return Convert.ToString(response.StatusDescription);
                }
                else
                {
                    return resp;
                }
                
                 
            
                }
            catch ( Exception E)
            {
                return E.Message.ToString();
            }
        
       }

        public string post (string path,string data)
        {
            var resp = "";
            try 
            {

                var url = this.host + path;

                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.ServicePoint.Expect100Continue = false;
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Headers.Add("Authorization", "Bearer "+this.token);
			    request.Method = "POST";
                request.ContentType = "application/json";
                

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    
                    
                    
                    streamWriter.Write(data);
                    
                    streamWriter.Flush();
                    
                    streamWriter.Close();
                }
                try
                {
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    resp = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    return resp;
                }
                catch(WebException e)
                {
                    using (WebResponse response = e.Response)
                    {
                        HttpWebResponse httpResponse = (HttpWebResponse)response;
                        
                        using (Stream data1 = response.GetResponseStream())
                        using (var reader = new StreamReader(data1))
                        {
                            string text = reader.ReadToEnd();
                            return text;
                        }
                    }
                }
            }
            catch ( Exception E)
            {
                

                return E.Message.ToString();
            }
        }

        public string put (string path,string data)
        {
            try 
            {
                var url = this.host + path;
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.ServicePoint.Expect100Continue = false;
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Headers.Add("Authorization", "Bearer "+this.token);
			    request.Method = "PUT";
                request.ContentType = "application/json";
                
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    
                    streamWriter.Write(data);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                
                var resp = new StreamReader(response.GetResponseStream()).ReadToEnd();
                
                
                if (Convert.ToInt32(response.StatusCode) == 401)
                {
                    return "Unauthorized : Invalid token " + Convert.ToString(response.StatusCode);
                }
                else if (Convert.ToInt32(response.StatusCode) >= 400)
                {
                    return Convert.ToString(response.StatusDescription);
                }
                else
                {
                    return resp;
                }
            }
            catch ( Exception E)
            {
                return E.Message.ToString();
            }
        }
        
        async public Task<string> fpost (string path,SortedDictionary<string,string> options,byte[] stream)
        {

            try
            {


                Random rnd = new Random();
                string random_number = Convert.ToString(rnd.Next(1, 200));
                //HttpClient httpClient = new HttpClient();
                string temp_file_name = "";
                var url = this.host + path;


                string sd = "";
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                HttpClient client = new HttpClient();


                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + this.token);
                client.DefaultRequestHeaders.ExpectContinue = false;
                using (MultipartFormDataContent form = new MultipartFormDataContent())
                {


                    form.Add(new StringContent(options["name"]), "name");

                    form.Add(new StringContent(options["timeIdentifier"]), "timeIdentifier");

                    form.Add(new StringContent(options["timeFormat"]), "timeFormat");

                    if (stream != null)
                    {

                        temp_file_name = "input" + random_number + "." + options["fileFormat"];

                        ByteArrayContent bytearraycontent = new ByteArrayContent(stream);
                        bytearraycontent.Headers.Add("Content-Type", "text/" + options["fileFormat"]);
                        form.Add(bytearraycontent, "data", temp_file_name);
                    }

                    var result = client.PostAsync(url, form).Result;

                    sd = await result.Content.ReadAsStringAsync();


                }

                return sd;
            }
            catch(Exception E)
            {
                return E.Message.ToString();
            }

            }
         
        

        public string delete (string path)
        {
            try 
            {
                var url = this.host + path;
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.ServicePoint.Expect100Continue = false;
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Headers.Add("Authorization", "Bearer "+this.token);
			    request.Method = "DELETE";
                request.ContentType = "application/json";
                
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                var resp = new StreamReader(response.GetResponseStream()).ReadToEnd();
                if (Convert.ToInt32(response.StatusCode) == 401)
                {
                    return "Unauthorized : Invalid token " + Convert.ToString(response.StatusCode);
                }
                else if (Convert.ToInt32(response.StatusCode) >= 400)
                {
                    return Convert.ToString(response.StatusDescription);
                }
                else
                {
                    return resp;
                }
            }
            catch ( Exception E)
            {
                return E.Message.ToString();
            }
        }

        public string upstream(string path,byte[] data)
        {
            try
            {
                var url = this.host + path;
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.ServicePoint.Expect100Continue = false;

                request.Credentials = CredentialCache.DefaultCredentials;
                request.Method="POST";
                request.Headers.Add("Authorization", "Bearer " + this.token);
                request.ContentType = "text/plain";
                // Set the ContentLength property of the WebRequest.
                request.ContentLength = data.Length;
                // Get the request stream.
               
                Stream dataStream = request.GetRequestStream();
                // Write the data to the request stream.
               
                dataStream.Write(data, 0, data.Length);
                // Close the Stream object.
               
                dataStream.Close();
                // Get the response.
               
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                
                var resp = new StreamReader(response.GetResponseStream()).ReadToEnd();
               
                if (Convert.ToInt32(response.StatusCode) == 401)
                {
                    return "Unauthorized : Invalid token " + Convert.ToString(response.StatusCode);
                }
                else if (Convert.ToInt32(response.StatusCode) >= 400)
                {
                    return Convert.ToString(response.StatusDescription);
                }
                else
                {
                    return resp;
                }
            }
            catch (Exception E)
            {
                return E.Message.ToString();
            }
        }

        public EventSource downstream(string path)
        {
            try
            {
                var url = host + path;
                var eventSource = new EventSource(url)
                {
                    Headers = new NameValueCollection {{ "Authorization", "Bearer " + this.token }}
                };

                eventSource.Connect();

                return eventSource; 
            }
            catch (Exception E)
            {
                
                return null;
            }
        }
        
        public string postData(string path, string data)
        {
            string resp = "";
            try { 
                var url = this.host + path;

                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.ServicePoint.Expect100Continue = false;
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Headers.Add("Authorization", "Bearer " + this.token);
                request.Method = "POST";
                request.ContentType = "text/plain";
                
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    //initiate the request
                    streamWriter.Write(data);
                    
                    streamWriter.Flush();
                    
                    streamWriter.Close();
                }
                
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                
                resp = new StreamReader(response.GetResponseStream()).ReadToEnd();

                if (Convert.ToInt32(response.StatusCode) == 401)
                {
                    return "Unauthorized : Invalid token " + Convert.ToString(response.StatusCode);
                }
                else if (Convert.ToInt32(response.StatusCode) >= 400)
                {
                    return Convert.ToString(response.StatusDescription);
                }
                else
                {
                    return resp;
                }
            }
            catch (Exception E)
            {
                

                
                return E.Message.ToString();
            }
        



        }

        public HttpResponse getOutput(string path, string responseFormat)
        {
            HttpResponse httpResponse = new HttpResponse();
            try
            {
                var url = this.host + path;
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.ServicePoint.Expect100Continue = false;
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Headers.Add("Authorization", "Bearer " + this.token);
                request.Headers.Add("accept", responseFormat);
                request.Method = "GET";
                request.ContentType = "application/json";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                string resp = new StreamReader(response.GetResponseStream()).ReadToEnd();
                httpResponse.statusCode = Convert.ToInt32(response.StatusCode);
                httpResponse.response = resp;

                return httpResponse;
            }
            catch (Exception E)
            {
                httpResponse.statusCode =500;
                httpResponse.response = E.Message.ToString();
                return httpResponse;
            }

        }
    }
}

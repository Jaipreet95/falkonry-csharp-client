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


namespace falkonry_csharp_client.service
{
    public class HttpService
    {
        private string host;
        private string token;
        private string user_agent="falkonry/csharp-client";

        public HttpService(string host, string token)
        {
          
          
            this.host = host == null ? "https://service.falkonry.io" : host;
            
            this.token = System.Convert.ToBase64String(Encoding.UTF8.GetBytes(token));
        }

        public string get (string path)
        {
            try 
            {   
                var url = this.host + path;
                WebRequest request = WebRequest.Create(url);
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Headers.Add("Authorization", "Token "+this.token);
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
                WebRequest request = WebRequest.Create(url);
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Headers.Add("Authorization", "Token "+this.token);
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
                WebRequest request = WebRequest.Create(url);
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Headers.Add("Authorization", "Token "+this.token);
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
                    return resp;
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
            
                
                Random rnd = new Random();
                string random_number = Convert.ToString(rnd.Next(1, 200));
                //HttpClient httpClient = new HttpClient();
                string temp_file_name = "";
                var url = this.host + path;
                
                
                string sd = "";
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                HttpClient client = new HttpClient();               
                
                
                client.DefaultRequestHeaders.Add("Authorization", "Token "+this.token);
                
                using (MultipartFormDataContent form = new MultipartFormDataContent())
                    {
                        
                        
                        form.Add(new StringContent(options["name"]), "name");
                        
                        form.Add(new StringContent(options["timeIdentifier"]), "timeIdentifier");
                        
                        form.Add(new StringContent(options["timeFormat"]), "timeFormat");
                        
                        if (stream != null)
                        {
                        
                        temp_file_name = "input" + random_number + "." + options["fileFormat"];
                        
                        ByteArrayContent bytearraycontent = new ByteArrayContent(stream);
                        bytearraycontent.Headers.Add("Content-Type", "text/"+options["fileFormat"]);
                        form.Add(bytearraycontent, "data", temp_file_name);
                        }
                      
                        var result = client.PostAsync(url, form).Result;
                       
                        sd =await result.Content.ReadAsStringAsync();
                     

                    }

                    return sd; 
            
            }
         
        

        public string delete (string path)
        {
            try 
            {
                var url = this.host + path;
                WebRequest request = WebRequest.Create(url);
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Headers.Add("Authorization", "Token "+this.token);
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
                WebRequest request = WebRequest.Create(url);

                request.Credentials = CredentialCache.DefaultCredentials;
                request.Method="POST";
                request.Headers.Add("Authorization", "Token " + this.token);
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

        public Stream downstream(string path)
        {
            try
            {
                var url = this.host + path;
                WebRequest request = WebRequest.Create(url);
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Headers.Add("Authorization", "Token " + this.token);
                
                request.Method = "GET";
                request.ContentType = "application/x-json-stream";
               
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                var resp = response.GetResponseStream();
                
                return resp; 
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
                
                WebRequest request = WebRequest.Create(url);
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Headers.Add("Authorization", "Token " + this.token);
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
    }
}

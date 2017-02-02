﻿///
/// falkonry-csharp-client
/// Copyright(c) 2016 Falkonry Inc
/// MIT Licensed
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace falkonry_csharp_client.helper.models
{
    public class Input
    {
        //private string[] InputSignalTypes = { "Numeric", "Categorical" };
        public string key
        {
            get;
            set;
        }
        public string name
        {
            get;
            set;
        }
        public ValueType valueType
        {
            get;
            set;
            /*{
                if(value is string && InputSignalTypes.Contains(value))
                {
                    valueType= new Dictionary<string, string> {
                    {"type", (string)value}
                };
                }
            }*/
        }
        /*public string toJSON()
        {
            return new JavaScriptSerializer().Serialize(this);
        }
        */
        public EventType eventType
        {
            get;
            set;
        }



    }
}

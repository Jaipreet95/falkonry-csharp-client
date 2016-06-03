﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace falkonry_csharp_client.helper.models
{
    class Subscription
    {
        //public void Subscription(Subscription subscription)
        //{
        //    //  var _this = this;
        //    //  _this.raw = subscription || {};
        //}

        public string key
        {
            get;
            set;
        }
        public string type
        {
            get;
            set;
        }
        public string topic
        {
            get;
            set;
        }
        public string path
        {
            get;
            set;
        }
        public string username
        {
            get;
            set;
        }
        public string password
        {
            get;
            set;
        }
        public string timeIdentifier
        {
            get;
            set;
        }
        public string timeFormat
        {
            get;
            set;
        }
        public string streaming
        {
            get;
            set;
        }
        public string isHistorian
        {
            get;
            set;
        }
        public string signalsTagField
        {
            get;
            set;
        }
        public string signalsLocation
        {
            get;
            set;
        }
        public string signalsLocation
        {
            get;
            set;
        }
        public string valueColumn
        {
            get;
            set;
        }
        public string toJSON()
        {
            return new JavaScriptSerializer().Serialize(this);
        }

    }
}

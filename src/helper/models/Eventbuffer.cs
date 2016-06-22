﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace falkonry_csharp_client.helper.models
{
    public class Eventbuffer
    {
        



        public string id
        {
            get;
            set;
            
            
        }

        public string sourceId
        {
            get;
            set;

        }

        public string name
        {
            get;
            set;

        }

        public string tenant
        {
            get;
            set;

        }

        public long createTime
        {
            get;
            set;

        }

        public string createdBy
        {
            get;
            set;

        }

        public long updateTime
        {
            get;
            set;

        }

        public string updatedBy
        {
            get;
            set;

        }

        public List<Object> schemaList
        {
            get;
            set;

        }

        public List<Subscription> subscriptionList
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

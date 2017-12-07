using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;

namespace WebAPIClient
{
    [DataContract(Name="repo")]
    public class Repository
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "html_url")]
        public Uri GitHubHomeUrl { get; set; }

        [DataMember(Name ="homepage")]
        public Uri Homepage { get; set; }

        [DataMember(Name = "watchers")]
        public int Watchers { get; set; }

        [IgnoreDataMember]
        public DateTime LastPush { get; set; }

        /**
         * serializer for LastPush
         */
        [DataMember(Name ="pushed_at")]
        private string JsonDate
        {
            get { return LastPush.ToString(); }

            set
            {
                this.LastPush = DateTime.ParseExact(value, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
            }
        }
        
    }
}

﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WebAPIClient
{
    [DataContract(Name="result")]
    public class SearchResult
    {
        [DataMember(Name ="total_count")]
        public int Count { get; set; }

        // not sure what this means in the github json
        // count will be thousands, and items will be 30, but incomplete is false
        // also I think I'm binding it backwards, incomplete == !complete
        [DataMember(Name ="incomplete_results")]
        public Boolean Complete { get; set; }

        [DataMember(Name ="items")]
        public List<Repository> items { get; set; }
    }
}

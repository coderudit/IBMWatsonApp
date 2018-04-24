using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IBMWatsonApp.Models
{
    public class WatsonContent
    {
        public string[] Intents { get; set; }

        public string[] Entities { get; set; }

        public Input Input { get; set; }

        public Output Output { get; set; }

        public Context Context { get; set; }

    }

    public class Input
    {
        public Dictionary<string, object> InputText { get; set; }
    }

    public class Output
    {
        public Dictionary<string, object> OutputText { get; set; }
    }

    public class Context
    {
        public Dictionary<string, object> ContextText { get; set; }
    }
}

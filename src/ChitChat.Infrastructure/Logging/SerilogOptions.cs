using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat.Infrastructure.Logging
{
    public class SerilogOptions
    {
        public bool Enabled { get; set; }

        public bool LogToConsole { get; set; }

        public long FileSizeLimitInBytes { get; set; } = 1024 * 2; //2Mb

        public int FlushFileToDiskInSeconds { get; set; } = 1;

        public SerilogSeqOptions? SeqOptions { get; set; }

        public SerilogApplicationInsightsOptions? ApplicationInsightsOptions { get; set; }
    }


    public class SerilogSeqOptions
    {
        public bool Enabled { set; get; }
        public string SeqUrl { get; set; } = string.Empty;
        public string SeqApiKey { get; set; } = string.Empty;
    }

    public class SerilogApplicationInsightsOptions
    {
        public bool Enabled { set; get; }
        public string ConnectionString { get; set; } = string.Empty;
    }
}

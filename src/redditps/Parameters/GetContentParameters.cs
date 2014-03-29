using System.Management.Automation;

namespace redditps.Parameters
{
    public class GetContentParameters
    {
        [Parameter(Mandatory = false)]
        public SwitchParameter InBrowser { get; set; }
    }
}
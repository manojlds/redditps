using System.Management.Automation;

namespace redditps.Provider.Parameters
{
    public class GetContentParameters
    {
        [Parameter(Mandatory = false)]
        public SwitchParameter InBrowser { get; set; }
    }
}
using System.Management.Automation;
using redditps.Enums;

namespace redditps.Provider.Parameters
{
    public class GetChildItemParameters
    {
        [Parameter(Mandatory = false)]
        public PostListType Type { get; set; }
        
        [Parameter(Mandatory = false)]
        public SwitchParameter All { get; set; }
    }
}
using System.Management.Automation;

namespace redditps.Parameters
{
    public class GetChildItemParameters
    {
        [Parameter(Mandatory = false)]
        public PostListType Type { get; set; }
    }
}
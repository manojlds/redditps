using System.Management.Automation;

namespace redditps
{
    public class GetChildItemParameters
    {
        [Parameter(Mandatory = false)]
        public PostListType Type { get; set; }
    }
}
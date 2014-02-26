using System.ComponentModel;
using System.Management.Automation;

namespace redditps
{
    [RunInstaller(true)]
    public class RedditPs : PSSnapIn
    {
        public override string Name
        {
            get { return "RedditPS"; }
        }

        public override string Vendor
        {
            get { return "StackToHeap"; }
        }

        public override string Description
        {
            get { return "Reddit provider for Powershell"; }
        }
    }
}

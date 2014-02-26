using System.Management.Automation;

namespace redditps
{
    public class RedditDriveInfo : PSDriveInfo
    {
        public RedditDriveInfo(string name, ProviderInfo provider, string root, string description, PSCredential credential, bool persist) : base(name, provider, root, description, credential, persist)
        {
        }
    }
}
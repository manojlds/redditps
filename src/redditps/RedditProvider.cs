using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Provider;
using RedditSharp;

namespace redditps
{
    [CmdletProvider("Reddit", ProviderCapabilities.None)]
    public partial class RedditProvider : NavigationCmdletProvider, IContentCmdletProvider
    {
        private readonly IRedditApi _api;
        private const string PathSeparator = @"\";

        public RedditProvider():this(new RedditApi())
        {
        }

        public RedditProvider(IRedditApi api = null)
        {
            _api = api;
        }

        protected override bool IsItemContainer(string path)
        {
            if (PathIsDrive(path))
            {
                return true;
            }

            Subreddit subreddit;
            PostListType type;
            var pathType = GetPathType(path, out subreddit, out type);

            if (pathType == PathType.Subreddit || pathType == PathType.SubredditWithType)
            {
                return true;
            }

            return false;
        }

        protected override string GetChildName(string path)
        {
            return base.GetChildName(path);
        }

        protected override string GetParentPath(string path, string root)
        {
            return base.GetParentPath(path, root);
        }

        public IContentReader GetContentReader(string path)
        {
            throw new System.NotImplementedException();
        }

        public object GetContentReaderDynamicParameters(string path)
        {
            throw new System.NotImplementedException();
        }

        public IContentWriter GetContentWriter(string path)
        {
            throw new System.NotImplementedException();
        }

        protected override void GetChildItems(string path, bool recurse)
        {
            if (PathIsDrive(path)) return;

            Subreddit subreddit;
            PostListType type;
            var pathType = GetPathType(path, out subreddit, out type);

            if (pathType == PathType.Invalid) return;

            if (pathType == PathType.Subreddit)
            {
                foreach (var item in _api.GetSubRedditItems(subreddit))
                {
                    WriteItemObject(item, path, true);
                }
                
            }
        }

        protected override object GetChildItemsDynamicParameters(string path, bool recurse)
        {
            return base.GetChildItemsDynamicParameters(path, recurse);
        }

        protected override void GetChildNames(string path, ReturnContainers returnContainers)
        {
            base.GetChildNames(path, returnContainers);
        }

        protected override object GetChildNamesDynamicParameters(string path)
        {
            return base.GetChildNamesDynamicParameters(path);
        }

        protected override bool HasChildItems(string path)
        {
            return base.HasChildItems(path);
        }

        protected override void GetItem(string path)
        {
            if (PathIsDrive(path))
            {
                WriteItemObject(PSDriveInfo, path, true);
                return;
            }
            Subreddit subreddit;
            PostListType type;
            var pathType = GetPathType(path, out subreddit, out type);

            if (pathType == PathType.Invalid) return;

            if (pathType == PathType.Subreddit)
            {
                WriteItemObject(subreddit, path, true);
            }
            
        }

        protected override object GetItemDynamicParameters(string path)
        {
            return base.GetItemDynamicParameters(path);
        }

        protected override bool ItemExists(string path)
        {
            if (PathIsDrive(path))
            {
                return true;
            }

            var pathType = GetPathType(path);
            return pathType != PathType.Invalid;
        }

        protected override object ItemExistsDynamicParameters(string path)
        {
            return base.ItemExistsDynamicParameters(path);
        }

        protected override Collection<PSDriveInfo> InitializeDefaultDrives()
        {
            return new Collection<PSDriveInfo>
                       {
                           new RedditDriveInfo("reddit", ProviderInfo, "reddit:", "Reddit Provider", null, true)
                       };
        }

        public object GetContentWriterDynamicParameters(string path)
        {
            throw new System.NotImplementedException();
        }

        public void ClearContent(string path)
        {
            throw new System.NotImplementedException();
        }

        public object ClearContentDynamicParameters(string path)
        {
            throw new System.NotImplementedException();
        }
    }
}

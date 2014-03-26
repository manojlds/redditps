using System.Collections.ObjectModel;
using System.Linq;
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
        private const string Drive = "reddit:";

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

            return pathType == PathType.Subreddit || pathType == PathType.SubredditWithType;
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

        protected override void GetChildItems(string path, bool recurse)
        {
            var dynamicParameters = DynamicParameters as GetChildItemParameters;
            if (PathIsDrive(path)) path=@"reddit:\frontpage";

            Subreddit subreddit;
            PostListType type;

            var pathType = GetPathType(path, out subreddit, out type);

            if (pathType == PathType.Invalid) return;

            if (dynamicParameters != null && dynamicParameters.Type != PostListType.None)
            {
                type = dynamicParameters.Type;
            }

            if (pathType != PathType.Subreddit && pathType != PathType.SubredditWithType) return;

            foreach (var item in _api.GetSubRedditItems(subreddit, type))
            {
                WriteItemObject(item, path, true);
            }
        }

        protected override object GetChildItemsDynamicParameters(string path, bool recurse)
        {
            return new GetChildItemParameters();
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

        protected override bool ItemExists(string path)
        {
            if (PathIsDrive(path))
            {
                return true;
            }

            var pathType = GetPathType(path);
            return pathType != PathType.Invalid;
        }

        protected override Collection<PSDriveInfo> InitializeDefaultDrives()
        {
            return new Collection<PSDriveInfo>
                       {
                           new RedditDriveInfo("reddit", ProviderInfo, Drive, "Reddit Provider", null, true)
                       };
        }
    }
}

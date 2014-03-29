using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Management.Automation;
using System.Management.Automation.Provider;
using redditps.Parameters;
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
            Post post;
            var pathType = GetPathType(path, out subreddit, out type, out post);

            return pathType == PathType.Subreddit || 
                   pathType == PathType.SubredditWithType || 
                   pathType == PathType.Item;
        }

        public IContentReader GetContentReader(string path)
        {
            if (PathIsDrive(path))
            {
                return null;
            }

            Subreddit subreddit;
            PostListType type;
            Post post;
            var pathType = GetPathType(path, out subreddit, out type, out post);

            if (pathType != PathType.Item) return null;

            var dynamicParameters = DynamicParameters as GetContentParameters;
            if (dynamicParameters != null && dynamicParameters.InBrowser.IsPresent)
            {
                Process.Start(post.Url);
                return null;
            }

            return new ItemContentReader(post);
        }

        public object GetContentReaderDynamicParameters(string path)
        {
            return new GetContentParameters();
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
            Post post;
            var pathType = GetPathType(path, out subreddit, out type, out post);

            if (pathType == PathType.Invalid) return;

            if (dynamicParameters != null && dynamicParameters.Type != PostListType.None)
            {
                type = dynamicParameters.Type;
            }

            if (pathType != PathType.Subreddit && pathType != PathType.SubredditWithType) return;

            var position = 1;
            foreach (var item in _api.GetSubRedditItems(subreddit, type))
            {
                _api.CachePost(position, item);
                WriteItemObject(item, path, true);
                position++;
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
            Post post;
            var pathType = GetPathType(path, out subreddit, out type, out post);

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

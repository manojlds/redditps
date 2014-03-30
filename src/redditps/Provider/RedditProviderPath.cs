using System;
using System.Linq;
using redditps.Enums;
using RedditSharp;

namespace redditps.Provider
{
    public partial class RedditProvider
    {
        protected override bool IsValidPath(string path)
        {
            if (PathIsDrive(path))
            {
                return true;
            }

            var pathChunks = path.Split(PathSeparator.ToCharArray());

            return pathChunks.All(pathChunk => pathChunk.Length != 0);
        }

        private bool PathIsDrive(string path)
        {
            return String.IsNullOrEmpty(path.Replace(this.PSDriveInfo.Root, string.Empty)) ||
                   String.IsNullOrEmpty(path.Replace(this.PSDriveInfo.Root + PathSeparator, string.Empty));
        }

        private string[] ChunkPath(string path)
        {
            var normalPath = NormalizePath(path);
            var pathNoDrive = normalPath.Replace(PSDriveInfo.Root + PathSeparator, string.Empty);

            return pathNoDrive.Split(PathSeparator.ToCharArray());
        }

        private static string NormalizePath(string path)
        {
            var result = path;

            if (!String.IsNullOrEmpty(path))
            {
                result = path.Replace("/", PathSeparator);
            }

            return result;
        }

        private string RemoveDriveFromPath(string path)
        {
            var result = path;

            var root = PSDriveInfo == null ? String.Empty : PSDriveInfo.Root;

            if (result == null)
            {
                result = String.Empty;
            }

            if (result.Contains(root))
            {
                result = result.Substring(result.IndexOf(root, StringComparison.OrdinalIgnoreCase) + root.Length);
            }

            return result;
        }

        private void ThrowTerminatingInvalidPathException(string path)
        {
            var message = String.Format("Path must represent either a subreddit or a item : {0}", path);

            throw new ArgumentException(message);
        }

        private PathType GetPathType(string path)
        {
            PostListType postListType;
            Subreddit subreddit;
            Post post;
            return GetPathType(path, out subreddit, out postListType, out post);
        }

        private PathType GetPathType(string path, out Subreddit subreddit, out PostListType postListType, out Post post)
        {
            postListType = PostListType.None;
            subreddit = null;
            post = null;
            string subredditName;

            if (PathIsDrive(path))
            {
                return PathType.Root;
            }

            var pathChunks = ChunkPath(path);

            switch (pathChunks.Length)
            {
                case 1:
                    subredditName = pathChunks[0];
                    if (_api.IsValidSubReddit(subredditName, out subreddit))
                    {
                        return PathType.Subreddit;
                    }
                    break;
                case 2:
                    subredditName = pathChunks[0];
                    if (_api.IsValidSubReddit(subredditName, out subreddit))
                    {
                        var nextPart = pathChunks[1];
                        int itemPosition;
                        if (Int32.TryParse(nextPart, out itemPosition))
                        {
                            post = _api.GetPost(itemPosition);
                            if (post != null)
                            {
                                return PathType.Item;
                            }
                        }
                        else if (IsPathChunkPostListType(nextPart, out postListType))
                        {
                            return PathType.SubredditWithType;
                        }
                    }
                    break;
            }
            return PathType.Invalid;
        }

        private bool IsPathChunkPostListType(string nextPart, out PostListType listType)
        {
            return Enum.TryParse(nextPart, true, out listType);
        }
    }
}
using System.Collections.Generic;
using redditps.Enums;
using RedditSharp;

namespace redditps.Api
{
    public interface IRedditApi
    {
        IEnumerable<Post> GetSubRedditItems(Subreddit subreddit, PostListType type, bool all = false);
        bool IsValidSubReddit(string sub, out Subreddit subreddit);
        void CachePost(int position, Post item);
        Post GetPost(int position);
    }
}
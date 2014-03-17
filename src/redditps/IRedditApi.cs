using System.Collections.Generic;
using RedditSharp;

namespace redditps
{
    public interface IRedditApi
    {
        IEnumerable<Post> GetSubRedditItems(Subreddit subreddit, PostListType type);
        bool IsValidSubReddit(string sub, out Subreddit subreddit);
    }
}
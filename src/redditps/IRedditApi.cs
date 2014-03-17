using System.Collections.Generic;
using RedditSharp;

namespace redditps
{
    public interface IRedditApi
    {
        IEnumerable<Post> GetSubRedditItems(Subreddit subreddit, PostListType type = PostListType.Hot);
        bool IsValidSubReddit(string sub, out Subreddit subreddit);
    }
}
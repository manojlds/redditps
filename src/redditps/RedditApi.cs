using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using RedditSharp;

namespace redditps
{
    public class RedditApi : IRedditApi
    {
        private readonly Reddit _reddit;
        private readonly ObjectCache _cache = MemoryCache.Default;

        public RedditApi()
        {
            _reddit = new Reddit();
        }

        public IEnumerable<Post> GetSubRedditItems(Subreddit subreddit, PostListType type)
        {
            if (type == PostListType.None || type == PostListType.Hot)
            {
                return subreddit.GetHot();
            }

            if (type == PostListType.New)
            {
                return subreddit.GetNew();
            }

            return Enumerable.Empty<Post>();
        }

        public bool IsValidSubReddit(string sub, out Subreddit subreddit)
        {
            GetFromCache(sub, out subreddit);

            if (subreddit != null) return true;

            try
            {
                subreddit=_reddit.GetSubreddit(sub);
                SetCache(sub, subreddit);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

        }

        private void SetCache(string sub, Subreddit subreddit)
        {
            sub = sub.ToLowerInvariant();
            _cache[sub] = subreddit;
        }

        private void GetFromCache(string sub, out Subreddit subreddit)
        {
            sub = sub.ToLowerInvariant();
            subreddit = _cache[sub] as Subreddit;
        }
    }
}
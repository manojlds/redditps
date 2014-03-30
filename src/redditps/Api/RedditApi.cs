using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using redditps.Enums;
using RedditSharp;

namespace redditps.Api
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
            var posts = Enumerable.Empty<Post>();
            if (type == PostListType.None || type == PostListType.Hot)
            {
                posts = subreddit.GetHot();
            }

            if (type == PostListType.New)
            {
                posts = subreddit.GetNew();
            }

            return posts;
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

        public void CachePost(int position, Post item)
        {
            _cache[GetRecentPostKey(position)] = item;
        }

        public Post GetPost(int position)
        {
            return _cache[GetRecentPostKey(position)] as Post;
        }

        private static string GetRecentPostKey(int position)
        {
            return string.Format("recentpost_{0}", position);
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
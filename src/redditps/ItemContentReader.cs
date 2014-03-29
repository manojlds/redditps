using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation.Provider;
using RedditSharp;

namespace redditps
{
    public class ItemContentReader : IContentReader
    {
        private long _currentOffset;
        private readonly List<string> _postContent; 

        public ItemContentReader(Post post)
        {
            _postContent = new List<string>
            {
                post.Title,
                "====================",
                string.Format("by: {0}", post.AuthorName)
            };
            if (post.IsSelfPost)
            {
                _postContent.InsertRange(_postContent.Count, post.SelfText.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None));
            }
            else
            {
                _postContent.Add(post.Url);
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IList Read(long readCount)
        {
            if (_currentOffset < 0 || _currentOffset >= _postContent.Count)
            {
                return null;
            }

            var rowsRead = 0;
            var finalOutput = new List<string>();

            while (rowsRead < readCount && _currentOffset < _postContent.Count)
            {
                finalOutput.Add(_postContent[(int) _currentOffset]);
                rowsRead++;
                _currentOffset++;
            }

            return finalOutput;
        }

        public void Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
        }
    }
}
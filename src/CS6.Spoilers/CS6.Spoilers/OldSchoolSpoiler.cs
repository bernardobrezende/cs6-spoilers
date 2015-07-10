using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CS6.Spoilers
{
    /// <summary>
    /// C# 5 code-style Spoiler implementation.
    /// </summary>
    class OldSchoolSpoiler : ISpoiler
    {
        private readonly DateTime timestamp = DateTime.Now;
        private string autor = "Unknown";

        public DateTime Timestamp
        {
            get { return timestamp; }
        }

        public string Author
        {
            get { return autor; }
            set { autor = value; }
        }

        public string Description { get; set; }

        private IList<string> tags = new List<string>();
        public IEnumerable<string> Tags
        {
            get { return tags; }
        }

        public override string ToString()
        {
            return string.Format("!!!{0} SPOILER ALERT !!! - [{1}] - {2} - by {3}",
                tags != null && tags.Contains("Death content") ? " HIGH" : string.Empty
                , Timestamp, Description, Author);
        }

        public void AddTag(string tag)
        {
            if (Tags != null)
            {
                tags.Add(tag);
            }
        }

        public async Task AddTagAsync(string tag)
        {
            Exception capturedException = null;
            try
            {
                if (String.IsNullOrEmpty(tag))
                {
                    throw new ArgumentNullException("tag");
                }
                AddTag(tag);
            }
            catch (ArgumentNullException exception)
            {
                capturedException = exception;
            }

            if (capturedException != null)
            {
                await LogAsync(capturedException);
                throw capturedException;
            }
        }

        private Task LogAsync(Exception error)
        {
            return Task.Run(() =>
            {
                Debug.WriteLine(error);
            });
        }
    }
}

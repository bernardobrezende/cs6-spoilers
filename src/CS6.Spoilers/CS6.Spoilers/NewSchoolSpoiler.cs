using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using static System.DateTime;

namespace CS6.Spoilers
{
    /// <summary>
    /// C# 6 code-style Spoiler implementation.
    /// </summary>
    class NewSchoolSpoiler : ISpoiler
    {
        public DateTime Timestamp { get; } = Now;
        public string Author { get; set; } = "Unknown";
        public string Description { get; set; }
        public IList<string> Tags { get; } = new List<string>();

        public override string ToString() => $"!!!{(Tags.Contains("Death content") ? " HIGH" : string.Empty)} SPOILER ALERT !!! - [{Timestamp}] - {Description} - by {Author}";

        public void AddTag(string tag) => Tags?.Add(tag);

        public async Task AddTagAsync(string tag)
        {
            try
            {
                if (String.IsNullOrEmpty(tag))
                {
                    throw new ArgumentNullException(nameof(tag));
                }
                AddTag(tag);
            }
            catch (Exception exception)
            {
                await LogAsync(exception);
                throw exception;
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

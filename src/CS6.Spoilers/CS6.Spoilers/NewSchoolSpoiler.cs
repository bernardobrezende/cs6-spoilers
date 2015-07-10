using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using static System.DateTime;

namespace CS6.Spoilers
{
    class NewSchoolSpoiler : ISpoiler
    {
        public string Author { get; set; } = "Unknown";

        public string Description { get; set; }

        public IList<string> Tags { get; } = new List<string>();

        public DateTime Timestamp { get; } = Now;

        public void AddTag(string tag)
        {
            Tags?.Add(tag);
        }

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
            catch (ArgumentNullException exception) when (exception.ParamName == nameof(tag))
            {
                await LogAsync(exception);
                throw;
            }
        }

        public override string ToString() => $"!!!{(Tags?.Contains("Death content") ?? false ? " HIGH" : string.Empty)} SPOILER ALERT !!! - [{Timestamp}] - {Description} - by {Author}";

        private Task LogAsync(Exception error)
        {
            return Task.Run(() =>
            {
                Debug.WriteLine(error);
            });
        }
    }
}

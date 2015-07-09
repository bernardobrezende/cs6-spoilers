using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CS6.Spoilers
{
    interface ISpoiler
    {
        DateTime Timestamp { get; }
        string Author { get; set; }
        string Description { get; set; }
        IEnumerable<string> Tags { get; }
        void AddTag(string tag);
        Task AddTagAsync(string tag);
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CS6.Spoilers
{
    [TestClass]
    public class SpoilerTest
    {
        //private ISpoiler spoiler = new OldSchoolSpoiler();
        private ISpoiler spoiler = new NewSchoolSpoiler();

        [TestMethod]
        public void SpoilerHasTimestamp()
        {
            // This is insanely wrong since it can be falsy anytime. The right thing to do is to create a date mockable object
            Assert.AreEqual(DateTime.Now.ToShortDateString(), spoiler.Timestamp.ToShortDateString());
        }

        [TestMethod]
        public void SpoilerHasUnknownAuthorWhenCreated()
        {
            Assert.AreEqual("Unknown", spoiler.Author);
        }

        [TestMethod]
        public void SpoilerToString()
        {
            spoiler.Author = "Zeca Camargo";
            spoiler.Description = "A bela e a fera ficam juntos no final";

            Assert.AreEqual(
                String.Format("!!! SPOILER ALERT !!! - [{0}] - A bela e a fera ficam juntos no final - by Zeca Camargo", spoiler.Timestamp)
                , spoiler.ToString()
            );
        }

        [TestMethod]
        public void AddTag()
        {
            spoiler.Description = "Alemanha 7 x 1 Brasil";
            spoiler.AddTag("Sports");

            CollectionAssert.AreEqual(new[] { "Sports" }, spoiler.Tags.ToArray());
        }

        [TestMethod]
        public void ToStringHighSpoilerAlert()
        {
            spoiler.Author = "Zeca Camargo";
            spoiler.Description = "Snape mata Dumbledore";
            spoiler.AddTag("Harry Potter");
            spoiler.AddTag("Death content");

            Assert.AreEqual(
                String.Format("!!! HIGH SPOILER ALERT !!! - [{0}] - Snape mata Dumbledore - by Zeca Camargo", spoiler.Timestamp)
                , spoiler.ToString()
            );
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task AddTagAsyncThrowsArgumentNullException()
        {
            await spoiler.AddTagAsync(null);
        }
    }
}

using Notabledle.NotableModel;

namespace Notabledle.Test
{
    public class UnitTest1
    {
        [Fact]
        public void NotableListTest()
        {
            var notables = NotableList.Value;
            Assert.NotEmpty(notables);
        }
    }
}
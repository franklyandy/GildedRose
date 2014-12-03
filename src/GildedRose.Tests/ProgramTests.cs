using GildedRose.Console;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GildedRose.Tests
{
  [TestFixture]
  class ProgramTests
  {
    protected Program _program;

    [SetUp]
    public void SetUp()
    {
      _program = new Program();
    }

    protected class UpdateQualityTests : ProgramTests
    {
      [Test]
      public void TestExpiredItemDegradesTwiceAsFast()
      {
        var item = new Item { SellIn = -1, Quality = 2 };
        var items = new List<Item> { item };

        Program.UpdateQuality(items);
        Assert.AreEqual(0, item.Quality);
      }
    }
  }
}

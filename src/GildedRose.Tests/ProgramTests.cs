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
        When_UpdateQuality(item);
        Then_Quality_should_be(0, item);
      }

      [Test]
      public void TestNonExpiredItemDegradesByOne()
      {
        var item = new Item { SellIn = 1, Quality = 1 };
        When_UpdateQuality(item);
        Then_Quality_should_be(0, item);
      }

      private void Then_Quality_should_be(int expectedQuality, Item item)
      {
        Assert.AreEqual(expectedQuality, item.Quality);
      }

      private void When_UpdateQuality(Item item)
      {
        var items = new List<Item> { item }; 
        Program.UpdateQuality(items);
      }
    }
  }
}

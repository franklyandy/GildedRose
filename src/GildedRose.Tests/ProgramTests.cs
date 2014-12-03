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

      [Test]
      public void TestNonExpiredItemQualityCannotBeLessThanZero()
      {
        var item = new Item { SellIn = 2, Quality = 0 };
        When_UpdateQuality(item);
        Then_Quality_should_not_be_negative(item);
      }

      [Test]
      public void TestExpiredItemQualityCannotBeLessThanZero()
      {
        var item = new Item { SellIn = -1, Quality = 0 };
        When_UpdateQuality(item);
        Then_Quality_should_not_be_negative(item);
      }

      private void Then_Quality_should_be(int expectedQuality, Item item)
      {
        Assert.AreEqual(expectedQuality, item.Quality);
      }

      private void Then_Quality_should_not_be_negative(Item item)
      {
        Assert.IsTrue(item.Quality >= 0);
      }

      private void When_UpdateQuality(Item item)
      {
        var items = new List<Item> { item }; 
        Program.UpdateQuality(items);
      }
    }
  }
}

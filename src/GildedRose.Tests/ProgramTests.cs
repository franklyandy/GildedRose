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
    [SetUp]
    public virtual void SetUp() { }

    protected class UpdateQualityTests : ProgramTests
    {
      private Item _item;

      protected class TestNonExpiredItems : UpdateQualityTests
      {
        [SetUp]
        public override void SetUp()
        {
          base.SetUp();
          _item = new Item { SellIn = 1 };
        }

        [Test]
        public void TestNonExpiredItemDegradesByOne()
        {
          _item.Quality = 1;
          When_UpdateQuality();
          Then_Quality_should_be(0);
        }

        [Test]
        public void TestNonExpiredItemQualityCannotBeLessThanZero()
        {
          _item.Quality = 0;
          When_UpdateQuality();
          Then_Quality_should_not_be_negative();
        }
      }

      protected class TestExpiredItems : UpdateQualityTests
      {
        [SetUp]
        public override void SetUp()
        {
          base.SetUp();
          _item = new Item { SellIn = -1 };
        }

        [Test]
        public void QualityCannotBeLessThanZero()
        {
          _item.Quality = 0;
          When_UpdateQuality();
          Then_Quality_should_not_be_negative();
        }

        [Test]
        public void QualityDegradesTwiceAsFast()
        {
          _item.Quality = 2;
          When_UpdateQuality();
          Then_Quality_should_be(0);
        }
      }

      protected class TestBrie : UpdateQualityTests
      {
        [SetUp]
        public override void SetUp()
        {
          base.SetUp();
          _item = new Item { Name = "Aged Brie", SellIn = 1 };
        }

        [Test]
        public void AgedBrieQualityIncreases()
        {
          _item.Quality = 1;
          When_UpdateQuality();
          Then_Quality_should_be(2);
        }

        [Test]
        public void AgedBrieQualityNeverExceedsFifty()
        {
          _item.Quality = 50;
          When_UpdateQuality();
          Then_Quality_should_be(50);
        }
      }

      protected class TestSulfuras : UpdateQualityTests
      {
        [SetUp]
        public override void SetUp()
        {
          base.SetUp();
          _item = new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 1 };
        }

        [Test]
        public void SulfurasNeverHasToBeSold()
        {
          When_UpdateQuality();
          Then_SellIn_should_be(1);
        }

        [Test]
        public void SulfurasNeverDecreasesInQuality()
        {
          _item.Quality = 1;
          When_UpdateQuality();
          Then_Quality_should_be(1);
        }
      }

      private void Then_Quality_should_be(int expectedQuality)
      {
        Assert.AreEqual(expectedQuality, _item.Quality);
      }

      private void Then_Quality_should_not_be_negative()
      {
        Assert.IsTrue(_item.Quality >= 0);
      }

      private void Then_SellIn_should_be(int expectedSellIn)
      {
        Assert.AreEqual(expectedSellIn, _item.SellIn);
      }

      private void When_UpdateQuality()
      {
        var items = new List<Item> { _item };
        Program.UpdateQuality(items);
      }
    }
  }
}

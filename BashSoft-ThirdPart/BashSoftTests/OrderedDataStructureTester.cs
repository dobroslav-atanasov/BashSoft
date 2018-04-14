namespace BashSoftTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BashSoft.Contracts;
    using BashSoft.DataStructures;
    using NUnit.Framework;

    public class OrderedDataStructureTester
    {
        private ISimpleOrderedBag<string> names;
        private const int DefaultCapacity = 16;
        private const int DefaultSize = 0;
        private const int InitialCapacity = 30;
        private const string CourseName = "C#_2015_Feb";
        private readonly IList<string> coursesNames = new List<string>() { "Java_May_2015", "C#_Feb_2015", "PHP_Aug_2016" };

        [SetUp]
        public void Initialized()
        {
            this.names = new SimpleSortedList<string>();
        }

        [Test]
        public void TestEmptyCtor()
        {
            this.names = new SimpleSortedList<string>();

            Assert.That(this.names.Capacity, Is.EqualTo(DefaultCapacity));
            Assert.That(this.names.Size, Is.EqualTo(DefaultSize));
        }

        [Test]
        public void TestCtorWithInitialComparer()
        {
            this.names = new SimpleSortedList<string>(StringComparer.OrdinalIgnoreCase);

            Assert.That(this.names.Capacity, Is.EqualTo(DefaultCapacity));
            Assert.That(this.names.Size, Is.EqualTo(DefaultSize));
        }

        [Test]
        public void TestCtorWithInitialCapacity()
        {
            this.names = new SimpleSortedList<string>(InitialCapacity);

            Assert.That(this.names.Capacity, Is.EqualTo(InitialCapacity));
            Assert.That(this.names.Size, Is.EqualTo(DefaultSize));
        }

        [Test]
        public void TestCtroWithInitialComparerAndCapacity()
        {
            this.names = new SimpleSortedList<string>(StringComparer.OrdinalIgnoreCase, InitialCapacity);

            Assert.That(this.names.Capacity, Is.EqualTo(InitialCapacity));
            Assert.That(this.names.Size, Is.EqualTo(DefaultSize));
        }

        [Test]
        public void TestAddIncreasesSize()
        {
            this.names.Add(CourseName);

            Assert.That(this.names.Size, Is.EqualTo(1));
        }

        [Test]
        public void TestAddNullThrowsException()
        {
            Assert.That(() => this.names.Add(null), Throws.ArgumentNullException);
        }

        [Test]
        public void TestAddUnsortedDataIsHeldSorted()
        {
            string[] expectedCoursesNames = new string[] { "C#_Feb_2015", "Java_May_2015", "PHP_Aug_2016" };

            this.names.AddAll(this.coursesNames);

            for (int i = 0; i < this.names.Size; i++)
            {
                Assert.That(this.names[i], Is.EqualTo(expectedCoursesNames[i]));
            }
        }

        [Test]
        public void TestAddingMoreThanInitialCapacity()
        {
            string[] items = new string[17].Select(i => "item").ToArray();

            this.names.AddAll(items);

            Assert.That(this.names.Size, Is.EqualTo(17));
            Assert.That(this.names.Capacity, Is.Not.EqualTo(16));
        }

        [Test]
        public void TestAddingAllFromCollectionIncreasesSize()
        {
            List<string> coursesName = new List<string>() { "C#_Feb_2015", "Java_May_2015" };

            this.names.AddAll(coursesName);

            Assert.That(this.names.Size, Is.EqualTo(2));
        }

        [Test]
        public void TestAddingAllFromNullThrowsException()
        {
            Assert.That(() => this.names.AddAll(null), Throws.ArgumentNullException);
        }

        [Test]
        public void TestAddAllKeepsSorted()
        {
            List<string> orderCoursesNames = this.coursesNames.OrderBy(c => c).ToList();

            this.names.AddAll(this.coursesNames);

            CollectionAssert.AreEqual(orderCoursesNames, this.names);
        }

        [Test]
        public void TestRemoveValidElementDecreasesSize()
        {
            this.names.Add(CourseName);
            this.names.Remove(CourseName);

            Assert.That(this.names.Size, Is.EqualTo(0));
        }

        [Test]
        public void TestRemoveValidElementRemovesSelectedOne()
        {
            this.names.AddAll(this.coursesNames);
            this.names.Remove(CourseName);

            Assert.That(this.names.Contains(CourseName), Is.False);
        }

        [Test]
        public void TestRemovingNullThrowsException()
        {
            Assert.That(() => this.names.Remove(null), Throws.ArgumentNullException);
        }

        [Test]
        public void TestJoinWithNull()
        {
            this.names.AddAll(this.coursesNames);

            Assert.That(() => this.names.JoinWith(null), Throws.ArgumentNullException);
        }

        [Test]
        public void TestJoinWorksFine()
        {
            string expectedString = "C#_Feb_2015, Java_May_2015, PHP_Aug_2016";

            this.names.AddAll(this.coursesNames);

            Assert.That(() => this.names.JoinWith(", "), Is.EqualTo(expectedString));
        }
    }
}
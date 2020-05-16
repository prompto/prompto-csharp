using NUnit.Framework;
using prompto.value;
using System.Collections.Generic;
using System;

namespace prompto.reader
{

    [TestFixture]
    public class TestCSVReader
    {

        [Test]
        public void testNullRetursnEmptyDocument()
        {
            IEnumerator<prompto.value.DocumentValue> iter = CSVReader.iterator((String)null, null, ',', '"');
            Assert.IsFalse(iter.MoveNext());
        }

        [Test]
        public void testEmptyRetursnEmptyDocument()
        {
            IEnumerator<DocumentValue> iter = CSVReader.iterator("", null, ',', '"');
            Assert.IsFalse(iter.MoveNext());
        }


        [Test]
        public void testSimpleNoQuotes()
        {
            String csv = "id,name\n1,John\n2,Sylvie\n";
            IEnumerator<DocumentValue> iter = CSVReader.iterator(csv, null, ',', '"');
            Assert.IsTrue(iter.MoveNext());
            DocumentValue doc = iter.Current;
            Assert.IsNotNull(doc);
            Assert.AreEqual(doc.GetMember("id", false), "1");
            Assert.AreEqual(doc.GetMember("name", false), "John");
            Assert.IsTrue(iter.MoveNext());
            doc = iter.Current;
            Assert.IsNotNull(doc);
            Assert.AreEqual(doc.GetMember("id", false), "2");
            Assert.AreEqual(doc.GetMember("name", false), "Sylvie");
        }

        [Test]
        public void testEscapeNoQuotes()
        {
            String csv = "id,name\n1,John\n2,Riou\\, Sylvie\n";
            IEnumerator<DocumentValue> iter = CSVReader.iterator(csv, null, ',', '"');
            Assert.IsTrue(iter.MoveNext());
            DocumentValue doc = iter.Current;
            Assert.IsNotNull(doc);
            Assert.AreEqual(doc.GetMember("id", false), "1");
            Assert.AreEqual(doc.GetMember("name", false), "John");
            Assert.IsTrue(iter.MoveNext());
            doc = iter.Current;
            Assert.IsNotNull(doc);
            Assert.AreEqual(doc.GetMember("id", false), "2");
            Assert.AreEqual(doc.GetMember("name", false), "Riou, Sylvie");
        }

        [Test]
        public void testSimpleQuotes()
        {
            String csv = "\"id\",\"name\"\n1,\"John\"\n2,\"Sylvie\"\n";
            IEnumerator<DocumentValue> iter = CSVReader.iterator(csv, null, ',', '"');
            Assert.IsTrue(iter.MoveNext());
            DocumentValue doc = iter.Current;
            Assert.IsNotNull(doc);
            Assert.IsNotNull(doc);
            Assert.AreEqual(doc.GetMember("id", false), "1");
            Assert.AreEqual(doc.GetMember("name", false), "John");
            Assert.IsTrue(iter.MoveNext());
            doc = iter.Current;
            Assert.IsNotNull(doc);
            Assert.IsNotNull(doc);
            Assert.AreEqual(doc.GetMember("id", false), "2");
            Assert.AreEqual(doc.GetMember("name", false), "Sylvie");
        }


        [Test]
        public void testEmptyValue()
        {
            String csv = "\"id\",\"name\"\n,\"John\"\n2,\n";
            IEnumerator<DocumentValue> iter = CSVReader.iterator(csv, null, ',', '"');
            Assert.IsTrue(iter.MoveNext());
            DocumentValue doc = iter.Current;
            Assert.IsNotNull(doc);
            Assert.AreEqual(doc.GetMember("id", false), NullValue.Instance);
            Assert.AreEqual(doc.GetMember("name", false), "John");
            Assert.IsTrue(iter.MoveNext());
            doc = iter.Current;
            Assert.IsNotNull(doc);
            Assert.AreEqual(doc.GetMember("id", false), "2");
            Assert.AreEqual(doc.GetMember("name", false), NullValue.Instance);
        }

        [Test]
        public void testMissingValue()
        {
            String csv = "\"id\",\"name\"\n1\n2,\"Sylvie\"\n";
            IEnumerator<DocumentValue> iter = CSVReader.iterator(csv, null, ',', '"');
            Assert.IsTrue(iter.MoveNext());
            DocumentValue doc = iter.Current;
            Assert.IsNotNull(doc);
            Assert.AreEqual(doc.GetMember("id", false), "1");
            Assert.AreEqual(doc.GetMember("name", false), NullValue.Instance);
            Assert.IsTrue(iter.MoveNext());
            doc = iter.Current;
            Assert.IsNotNull(doc);
            Assert.AreEqual(doc.GetMember("id", false), "2");
            Assert.AreEqual(doc.GetMember("name", false), "Sylvie");
        }

        [Test]
        public void testExtraValue()
        {
            String csv = "\"id\",\"name\"\n1,\"John\",Doe\n2,\"Sylvie\"\n";
            IEnumerator<DocumentValue> iter = CSVReader.iterator(csv, null, ',', '"');
            Assert.IsTrue(iter.MoveNext());
            DocumentValue doc = iter.Current;
            Assert.IsNotNull(doc);
            Assert.AreEqual(doc.GetMember("id", false), "1");
            Assert.AreEqual(doc.GetMember("name", false), "John");
            Assert.IsTrue(iter.MoveNext());
            doc = iter.Current;
            Assert.IsNotNull(doc);
            Assert.AreEqual(doc.GetMember("id", false), "2");
            Assert.AreEqual(doc.GetMember("name", false), "Sylvie");
        }

        [Test]
        public void testInnerQuote()
        {
            String csv = "id,name\n1,Jo\"hn\n2,Sylvie\n";
            IEnumerator<DocumentValue> iter = CSVReader.iterator(csv, null, ',', '"');
            Assert.IsTrue(iter.MoveNext());
            DocumentValue doc = iter.Current;
            Assert.IsNotNull(doc);
            Assert.AreEqual(doc.GetMember("id", false), "1");
            Assert.AreEqual(doc.GetMember("name", false), "Jo\"hn");
            Assert.IsTrue(iter.MoveNext());
            doc = iter.Current;
            Assert.IsNotNull(doc);
            Assert.AreEqual(doc.GetMember("id", false), "2");
            Assert.AreEqual(doc.GetMember("name", false), "Sylvie");
        }

        [Test]
        public void testQuotedInnerQuote()
        {
            String csv = "id,name\n1,\"Jo\"\"hn\"\n2,Sylvie\n";
            IEnumerator<DocumentValue> iter = CSVReader.iterator(csv, null, ',', '"');
            Assert.IsTrue(iter.MoveNext());
            DocumentValue doc = iter.Current;
            Assert.IsNotNull(doc);
            Assert.AreEqual(doc.GetMember("id", false), "1");
            Assert.AreEqual(doc.GetMember("name", false), "Jo\"hn");
            Assert.IsTrue(iter.MoveNext());
            doc = iter.Current;
            Assert.IsNotNull(doc);
            Assert.AreEqual(doc.GetMember("id", false), "2");
            Assert.AreEqual(doc.GetMember("name", false), "Sylvie");
        }


        [Test]
        public void testQuotedInnerNewLine()
        {
            String csv = "id,name\n1,\"Jo\nhn\"\n2,Sylvie\n";
            IEnumerator<DocumentValue> iter = CSVReader.iterator(csv, null, ',', '"');
            Assert.IsTrue(iter.MoveNext());
            DocumentValue doc = iter.Current;
            Assert.IsNotNull(doc);
            Assert.AreEqual(doc.GetMember("id", false), "1");
            Assert.AreEqual(doc.GetMember("name", false), "Jo\nhn");
            Assert.IsTrue(iter.MoveNext());
            doc = iter.Current;
            Assert.IsNotNull(doc);
            Assert.AreEqual(doc.GetMember("id", false), "2");
            Assert.AreEqual(doc.GetMember("name", false), "Sylvie");
        }

    }
}

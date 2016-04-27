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
		public void testNullRetursnEmptyDocument ()
		{
			IEnumerator<prompto.value.Document> iter = CSVReader.iterator ((String)null, ',', '"');
			Assert.IsFalse (iter.MoveNext ());
		}

		[Test]
		public void testEmptyRetursnEmptyDocument ()
		{
			IEnumerator<Document> iter = CSVReader.iterator ("", ',', '"');
			Assert.IsFalse (iter.MoveNext ());
		}


		[Test]
		public void testSimpleNoQuotes ()
		{
			String csv = "id,name\n1,John\n2,Sylvie\n";
			IEnumerator<Document> iter = CSVReader.iterator (csv, ',', '"');
			Assert.IsTrue (iter.MoveNext ());
			Document doc = iter.Current;
			Assert.IsNotNull (doc);
			Assert.AreEqual (doc.GetMember ("id"), "1");
			Assert.AreEqual (doc.GetMember ("name"), "John");
			Assert.IsTrue (iter.MoveNext ());
			doc = iter.Current;
			Assert.IsNotNull (doc);
			Assert.AreEqual (doc.GetMember ("id"), "2");
			Assert.AreEqual (doc.GetMember ("name"), "Sylvie");
		}

		[Test]
		public void testEscapeNoQuotes ()
		{
			String csv = "id,name\n1,John\n2,Riou\\, Sylvie\n";
			IEnumerator<Document> iter = CSVReader.iterator (csv, ',', '"');
			Assert.IsTrue (iter.MoveNext ());
			Document doc = iter.Current;
			Assert.IsNotNull (doc);
			Assert.AreEqual (doc.GetMember ("id"), "1");
			Assert.AreEqual (doc.GetMember ("name"), "John");
			Assert.IsTrue (iter.MoveNext ());
			doc = iter.Current;
			Assert.IsNotNull (doc);
			Assert.AreEqual (doc.GetMember ("id"), "2");
			Assert.AreEqual (doc.GetMember ("name"), "Riou, Sylvie");
		}

		[Test]
		public void testSimpleQuotes ()
		{
			String csv = "\"id\",\"name\"\n1,\"John\"\n2,\"Sylvie\"\n";
			IEnumerator<Document> iter = CSVReader.iterator (csv, ',', '"');
			Assert.IsTrue (iter.MoveNext ());
			Document doc = iter.Current;
			Assert.IsNotNull (doc);
			Assert.IsNotNull (doc);
			Assert.AreEqual (doc.GetMember ("id"), "1");
			Assert.AreEqual (doc.GetMember ("name"), "John");
			Assert.IsTrue (iter.MoveNext ());
			doc = iter.Current;
			Assert.IsNotNull (doc);
			Assert.IsNotNull (doc);
			Assert.AreEqual (doc.GetMember ("id"), "2");
			Assert.AreEqual (doc.GetMember ("name"), "Sylvie");
		}


		[Test]
		public void testEmptyValue ()
		{
			String csv = "\"id\",\"name\"\n,\"John\"\n2,\n";
			IEnumerator<Document> iter = CSVReader.iterator (csv, ',', '"');
			Assert.IsTrue (iter.MoveNext ());
			Document doc = iter.Current;
			Assert.IsNotNull (doc);
			Assert.AreEqual (doc.GetMember ("id"), NullValue.Instance); 
			Assert.AreEqual (doc.GetMember ("name"), "John");
			Assert.IsTrue (iter.MoveNext ());
			doc = iter.Current;
			Assert.IsNotNull (doc);
			Assert.AreEqual (doc.GetMember ("id"), "2");
			Assert.AreEqual (doc.GetMember ("name"), NullValue.Instance);
		}

		[Test]
		public void testMissingValue ()
		{
			String csv = "\"id\",\"name\"\n1\n2,\"Sylvie\"\n";
			IEnumerator<Document> iter = CSVReader.iterator (csv, ',', '"');
			Assert.IsTrue (iter.MoveNext ());
			Document doc = iter.Current;
			Assert.IsNotNull (doc);
			Assert.AreEqual (doc.GetMember ("id"), "1");
			Assert.AreEqual (doc.GetMember ("name"), NullValue.Instance);
			Assert.IsTrue (iter.MoveNext ());
			doc = iter.Current;
			Assert.IsNotNull (doc);
			Assert.AreEqual (doc.GetMember ("id"), "2");
			Assert.AreEqual (doc.GetMember ("name"), "Sylvie");
		}

		[Test]
		public void testExtraValue ()
		{
			String csv = "\"id\",\"name\"\n1,\"John\",Doe\n2,\"Sylvie\"\n";
			IEnumerator<Document> iter = CSVReader.iterator (csv, ',', '"');
			Assert.IsTrue (iter.MoveNext ());
			Document doc = iter.Current;
			Assert.IsNotNull (doc);
			Assert.AreEqual (doc.GetMember ("id"), "1");
			Assert.AreEqual (doc.GetMember ("name"), "John");
			Assert.IsTrue (iter.MoveNext ());
			doc = iter.Current;
			Assert.IsNotNull (doc);
			Assert.AreEqual (doc.GetMember ("id"), "2");
			Assert.AreEqual (doc.GetMember ("name"), "Sylvie");
		}
	
	}
}

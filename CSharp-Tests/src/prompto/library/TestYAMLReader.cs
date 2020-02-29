using NUnit.Framework;
using prompto.writer;
using System.Collections.Generic;
using System;

namespace prompto.reader
{

    [TestFixture]
    public class TestYAMLReader
    {

        [Test]
        public void testRoundtrip()
        {
            String yaml1 = "field: 122\ndata:\n  list:\n  - field: 123\n  - doc:\n      name: John\n      doc:\n        list:\n        - 122\n        - 233\n        - 344";
            var docs1 = YAMLReader.read(yaml1);
            String yaml2 = YAMLWriter.write(docs1);
            var docs2 = YAMLReader.read(yaml2);
            Assert.IsTrue(docs1.Equals(docs2));
        }

    }
}

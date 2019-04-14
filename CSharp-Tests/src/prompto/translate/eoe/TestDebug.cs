using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestDebug : BaseEParserTest
	{

		[Test]
		public void testStack()
		{
			compareResourceEOE("debug/stack.pec");
		}

		[Test]
		public void testVariable_boolean()
		{
			compareResourceEOE("debug/variable-boolean.pec");
		}

		[Test]
		public void testVariable_category()
		{
			compareResourceEOE("debug/variable-category.pec");
		}

		[Test]
		public void testVariable_character()
		{
			compareResourceEOE("debug/variable-character.pec");
		}

		[Test]
		public void testVariable_css()
		{
			compareResourceEOE("debug/variable-css.pec");
		}

		[Test]
		public void testVariable_cursor()
		{
			compareResourceEOE("debug/variable-cursor.pec");
		}

		[Test]
		public void testVariable_date()
		{
			compareResourceEOE("debug/variable-date.pec");
		}

		[Test]
		public void testVariable_dateTime()
		{
			compareResourceEOE("debug/variable-dateTime.pec");
		}

		[Test]
		public void testVariable_decimal()
		{
			compareResourceEOE("debug/variable-decimal.pec");
		}

		[Test]
		public void testVariable_dictionary()
		{
			compareResourceEOE("debug/variable-dictionary.pec");
		}

		[Test]
		public void testVariable_document()
		{
			compareResourceEOE("debug/variable-document.pec");
		}

		[Test]
		public void testVariable_integer()
		{
			compareResourceEOE("debug/variable-integer.pec");
		}

		[Test]
		public void testVariable_iterator()
		{
			compareResourceEOE("debug/variable-iterator.pec");
		}

		[Test]
		public void testVariable_list()
		{
			compareResourceEOE("debug/variable-list.pec");
		}

		[Test]
		public void testVariable_null()
		{
			compareResourceEOE("debug/variable-null.pec");
		}

		[Test]
		public void testVariable_range()
		{
			compareResourceEOE("debug/variable-range.pec");
		}

		[Test]
		public void testVariable_set()
		{
			compareResourceEOE("debug/variable-set.pec");
		}

		[Test]
		public void testVariable_text()
		{
			compareResourceEOE("debug/variable-text.pec");
		}

		[Test]
		public void testVariable_time()
		{
			compareResourceEOE("debug/variable-time.pec");
		}

		[Test]
		public void testVariable_tuple()
		{
			compareResourceEOE("debug/variable-tuple.pec");
		}

		[Test]
		public void testVariable_uuid()
		{
			compareResourceEOE("debug/variable-uuid.pec");
		}

		[Test]
		public void testVariable_version()
		{
			compareResourceEOE("debug/variable-version.pec");
		}

		[Test]
		public void testVariables()
		{
			compareResourceEOE("debug/variables.pec");
		}

	}
}


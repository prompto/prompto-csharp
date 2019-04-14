using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestDebug : BaseEParserTest
	{

		[Test]
		public void testStack()
		{
			compareResourceEME("debug/stack.pec");
		}

		[Test]
		public void testVariable_boolean()
		{
			compareResourceEME("debug/variable-boolean.pec");
		}

		[Test]
		public void testVariable_category()
		{
			compareResourceEME("debug/variable-category.pec");
		}

		[Test]
		public void testVariable_character()
		{
			compareResourceEME("debug/variable-character.pec");
		}

		[Test]
		public void testVariable_css()
		{
			compareResourceEME("debug/variable-css.pec");
		}

		[Test]
		public void testVariable_cursor()
		{
			compareResourceEME("debug/variable-cursor.pec");
		}

		[Test]
		public void testVariable_date()
		{
			compareResourceEME("debug/variable-date.pec");
		}

		[Test]
		public void testVariable_dateTime()
		{
			compareResourceEME("debug/variable-dateTime.pec");
		}

		[Test]
		public void testVariable_decimal()
		{
			compareResourceEME("debug/variable-decimal.pec");
		}

		[Test]
		public void testVariable_dictionary()
		{
			compareResourceEME("debug/variable-dictionary.pec");
		}

		[Test]
		public void testVariable_document()
		{
			compareResourceEME("debug/variable-document.pec");
		}

		[Test]
		public void testVariable_integer()
		{
			compareResourceEME("debug/variable-integer.pec");
		}

		[Test]
		public void testVariable_iterator()
		{
			compareResourceEME("debug/variable-iterator.pec");
		}

		[Test]
		public void testVariable_list()
		{
			compareResourceEME("debug/variable-list.pec");
		}

		[Test]
		public void testVariable_null()
		{
			compareResourceEME("debug/variable-null.pec");
		}

		[Test]
		public void testVariable_range()
		{
			compareResourceEME("debug/variable-range.pec");
		}

		[Test]
		public void testVariable_set()
		{
			compareResourceEME("debug/variable-set.pec");
		}

		[Test]
		public void testVariable_text()
		{
			compareResourceEME("debug/variable-text.pec");
		}

		[Test]
		public void testVariable_time()
		{
			compareResourceEME("debug/variable-time.pec");
		}

		[Test]
		public void testVariable_tuple()
		{
			compareResourceEME("debug/variable-tuple.pec");
		}

		[Test]
		public void testVariable_uuid()
		{
			compareResourceEME("debug/variable-uuid.pec");
		}

		[Test]
		public void testVariable_version()
		{
			compareResourceEME("debug/variable-version.pec");
		}

		[Test]
		public void testVariables()
		{
			compareResourceEME("debug/variables.pec");
		}

	}
}


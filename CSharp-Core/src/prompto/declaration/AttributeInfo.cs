using System;
using System.Collections.Generic;
using prompto.type;

namespace prompto.declaration
{
	public class AttributeInfo
	{

		public const String KEY = "key";
		public const String VALUE = "value";
		public const String WORDS = "words";

		protected String name;
		protected TypeFamily family;
		protected bool collection;
		protected bool key = false;
		protected bool value = false;
		protected bool words = false;

		public AttributeInfo(String name, TypeFamily family, bool collection, ICollection<String> indexTypes)
		{
			this.name = name;
			this.family = family;
			this.collection = collection;
			if (indexTypes != null)
			{
				key = indexTypes.Contains(KEY);
				value = indexTypes.Contains(VALUE);
				words = indexTypes.Contains(WORDS);
			}
		}

		public AttributeInfo(String name, TypeFamily family, bool collection, bool key, bool value, bool words)
		{
			this.name = name;
			this.family = family;
			this.collection = collection;
			this.key = key;
			this.value = value;
			this.words = words;
		}

		public AttributeInfo(AttributeInfo info)
		{
			this.name = info.getName();
			this.family = info.getFamily();
			this.collection = info.isCollection();
			this.key = info.isKey();
			this.value = info.isValue();
			this.words = info.isWords();
		}

		public String getName()
		{
			return name;
		}

		public TypeFamily getFamily()
		{
			return family;
		}

		public bool isCollection()
		{
			return collection;
		}

		public bool isKey()
		{
			return key;
		}

		public bool isValue()
		{
			return value;
		}

		public bool isWords()
		{
			return words;
		}
	}
}

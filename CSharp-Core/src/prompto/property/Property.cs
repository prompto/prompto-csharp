using System;
using prompto.jsx;
using prompto.runtime;

namespace prompto.property
{
    public class Property
    {
		String name;
		String help;
		IPropertyValidator validator = AlwaysValidator.Instance;

		public String GetName()
		{
			return name;
		}

		public void SetName(String name)
		{
			this.name = name;
		}

		public bool IsRequired()
		{
			return validator.IsRequired();
		}

		public void SetRequired(bool set)
		{
			validator = set ? validator.Required() : validator.Optional();
		}

		public void SetRequiredForAccessibility(bool set)
		{
			validator = set ? validator.RequiredForAccessibility() : validator.OptionalForAccessibility();
		}


		public IPropertyValidator GetValidator()
		{
			return validator;
		}

		public void SetValidator(IPropertyValidator validator)
		{
			if (this.validator.IsRequired())
				this.validator = validator.Required();
			else
				this.validator = validator.Optional();
		}

		public Property WithValidator(IPropertyValidator validator)
		{
			SetValidator(validator);
			return this;
		}

		public void Validate(Context context, JsxProperty prop)
		{
			validator.Validate(context, prop);
		}

		public String GetHelp()
		{
			return help;
		}

		public void SetHelp(String help)
		{
			this.help = help;
		}

    	public override String ToString()
		{
			return "name=" + name + ", type=" + validator + ", help=" + help;
		}

		/*
		public void ToLiteral(Writer writer)
		{
			try
			{
				writer.append(nameToKey()).append(": ");
				if (help != null || validator.isRequired())
				{
					writer.append("{ ")
						.append(validator.getKeyName())
						.append(": ")
						.append(validator.toLiteral());
					if (help != null)
					{
						String escaped = help.replaceAll("\"", "'");
						writer.append(", help: \"").append(escaped).append("\"");
					}
					if (validator.isRequired())
						writer.append(", required: true");
					writer.append('}');
				}
				else
					writer.append(getValidator().toLiteral());
			}
			catch (IOException e)
			{
				throw new RuntimeException(e);
			}
		}

		private String nameToKey()
		{
			String value = StringUtils.trimEnclosingQuotes(name);
			if (value.contains("-"))
				return '"' + value + '"';
			else
				return value;
		}
         */

	}
}

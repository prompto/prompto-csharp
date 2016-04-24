
using prompto.runtime;
using System;
using prompto.parser;
using System.Collections.Generic;
using prompto.grammar;
using prompto.type;
using prompto.utils;
using prompto.expression;

namespace prompto.declaration
{

    public class EnumeratedCategoryDeclaration : ConcreteCategoryDeclaration, IEnumeratedDeclaration<CategorySymbol>
    {

        CategorySymbolList symbols;
		EnumeratedCategoryType type;

        public EnumeratedCategoryDeclaration(String name)
            : base(name)
        {
			type = new EnumeratedCategoryType(name);
        }

        public EnumeratedCategoryDeclaration(String name, IdentifierList attrs, IdentifierList derived, CategorySymbolList symbols)
            : base(name, attrs, derived, null)
        {
			type = new EnumeratedCategoryType(name);
            setSymbols(symbols);
        }

        public SymbolList<CategorySymbol> getSymbols()
        {
            return symbols;
        }

        public void setSymbols(CategorySymbolList symbols)
        {
            this.symbols = symbols;
            foreach (CategorySymbol s in symbols)
                s.SetIType(type);
			this.symbols.SetIType(new ListType (type));
        }

        override
        public void register(Context context)
        {
            context.registerDeclaration(this);
            foreach (CategorySymbol symbol in symbols)
                context.registerValue(symbol);
        }

        override
        public IType check(Context context)
        {
            base.check(context);
            foreach (CategorySymbol s in symbols)
            {
                s.check(context); // TODO
            }
            return GetIType(context);
        }

        override
        public IType GetIType(Context context)
        {
            return new EnumeratedCategoryType(name);
        }

		override
		protected void toODialect(CodeWriter writer) {
			writer.append("enumerated category ");
			writer.append(name);
			if(attributes!=null) {
				writer.append('(');
				attributes.ToDialect(writer, true);
				writer.append(")");
			}
			if(derivedFrom!=null) {
				writer.append(" extends ");
				derivedFrom.ToDialect(writer, true);
			}
			writer.append(" {\n");
			writer.indent();
			foreach(Object symbol in symbols) {
				((CategorySymbol)symbol).ToDialect(writer);
				writer.append(";\n");
			}
			writer.dedent();
			writer.append("}\n");
		}

		override
		protected void toEDialect(CodeWriter writer) {
			writer.append("define ");
			writer.append(name);
			writer.append(" as enumerated ");
			if(derivedFrom!=null)
				derivedFrom.ToDialect(writer, true);
			else 
				writer.append("category");
			if(attributes!=null && attributes.Count>0) {
				if(attributes.Count==1)
					writer.append(" with attribute ");
				else
					writer.append(" with attributes ");
				attributes.ToDialect(writer, true);
				if(symbols!=null && symbols.Count>0)
					writer.append(", and symbols:\n");
			}
			else
				writer.append(" with symbols:\n");
			writer.indent();
			foreach(Object symbol in symbols) {
				((CategorySymbol)symbol).ToDialect(writer);
				writer.append("\n");
			}
			writer.dedent();
		}


		protected override void toSDialect(CodeWriter writer) {
			writer.append("enum ");
			writer.append(name);
			writer.append("(");
			if(derivedFrom!=null) {
				derivedFrom.ToDialect(writer, false);
				if(attributes!=null && attributes.Count>0)
					writer.append(", ");
			}
			if(attributes!=null && attributes.Count>0)
				attributes.ToDialect(writer, false);
			writer.append("):\n");
			writer.indent();
			foreach(Object symbol in symbols) {
				((CategorySymbol)symbol).ToDialect(writer);
				writer.append("\n");
			}
			writer.dedent();
		}
    }

}
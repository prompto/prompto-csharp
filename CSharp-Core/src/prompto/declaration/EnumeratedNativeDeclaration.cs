using System;
using prompto.runtime;
using prompto.parser;
using System.Collections.Generic;
using prompto.grammar;
using prompto.type;
using prompto.utils;
using prompto.expression;


namespace prompto.declaration
{

    public class EnumeratedNativeDeclaration : BaseDeclaration, IEnumeratedDeclaration<NativeSymbol>
    {

		NativeSymbolList symbols;
        EnumeratedNativeType type;

        public EnumeratedNativeDeclaration(String name, NativeType derivedFrom, NativeSymbolList symbols)
            : base(name)
        {
            this.type = new EnumeratedNativeType(name, derivedFrom);
			setSymbols(symbols);
        }

        public SymbolList<NativeSymbol> getSymbols()
        {
            return symbols;
        }

		public void setSymbols(NativeSymbolList symbols)
		{
			this.symbols = symbols;
			foreach (NativeSymbol s in symbols)
				s.setType(type);
			this.symbols.Type = new ListType (type);
		}

		override
        public void register(Context context)
        {
            context.registerDeclaration(this);
            foreach (NativeSymbol s in symbols)
                s.register(context);
        }

        override
        public IType check(Context context)
        {
            foreach (NativeSymbol s in symbols)
                s.check(context);
            return type;
        }

        override
        public IType GetIType(Context context)
        {
            return type;
        }

		override
		public void ToDialect(CodeWriter writer) {
			switch(writer.getDialect()) {
			case Dialect.E:
				toEDialect(writer);
				break;
			case Dialect.O:
				toODialect(writer);
				break;
			case Dialect.S:
				toPDialect(writer);
				break;
			}
		}

		private void toPDialect(CodeWriter writer) {
			writer.append("enum ");
			writer.append(name);
			writer.append('(');
			type.getDerivedFrom().ToDialect(writer);
			writer.append("):\n");
			writer.indent();
			foreach(Object symbol in symbols) {
				((NativeSymbol)symbol).ToDialect(writer);
				writer.append("\n");
			}
			writer.dedent();
		}

		private void toODialect(CodeWriter writer) {
			writer.append("enumerated ");
			writer.append(name);
			writer.append('(');
			type.getDerivedFrom().ToDialect(writer);
			writer.append(") {\n");
			writer.indent();
			foreach(Object symbol in symbols) {
				((NativeSymbol)symbol).ToDialect(writer);
				writer.append(";\n");
			}
			writer.dedent();
			writer.append("}\n");
		}

		private void toEDialect(CodeWriter writer) {
			writer.append("define ");
			writer.append(name);
			writer.append(" as enumerated ");
			type.getDerivedFrom().ToDialect(writer);
			writer.append(" with symbols:\n");
			writer.indent();
			foreach(Object symbol in symbols) {
				((NativeSymbol)symbol).ToDialect(writer);
				writer.append("\n");
			}
			writer.dedent();
		}
    }

}

using presto.utils;
using presto.parser;

namespace presto.grammar {

    public class NativeCategoryMappingList : ObjectList<NativeCategoryMapping>
    {

        public NativeCategoryMappingList()
        {

        }

        public NativeCategoryMappingList(NativeCategoryMapping mapping)
        {
            this.Add(mapping);
        }

		public void ToDialect(CodeWriter writer) {
			switch(writer.getDialect()) {
			case Dialect.E:
				toEDialect(writer);
				break;
			case Dialect.O:
				toODialect(writer);
				break;
			case Dialect.P:
				toPDialect(writer);
				break;
			}
		}

		private void toEDialect(CodeWriter writer) {
			writer.append("define category mappings as:\n");
			writer.indent();
			foreach(NativeCategoryMapping m in this) {
				m.ToDialect(writer);
				writer.newLine();
			}
			writer.dedent();	
		}

		private void toPDialect(CodeWriter writer) {
			writer.append("mappings:\n");
			writer.indent();
			foreach(NativeCategoryMapping m in this) {
				m.ToDialect(writer);
				writer.newLine();
			}
			writer.dedent();	
		}

		private void toODialect(CodeWriter writer) {
			writer.append("category mappings {\n");
			writer.indent();
			foreach(NativeCategoryMapping m in this) {
				m.ToDialect(writer);
				writer.append(';');
				writer.newLine();
			}
			writer.dedent();	
			writer.append("}");
		}

    }

}

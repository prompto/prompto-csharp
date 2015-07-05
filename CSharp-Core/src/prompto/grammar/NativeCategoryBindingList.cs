using prompto.utils;
using prompto.parser;

namespace prompto.grammar {

    public class NativeCategoryBindingList : ObjectList<NativeCategoryBinding>
    {

        public NativeCategoryBindingList()
        {

        }

        public NativeCategoryBindingList(NativeCategoryBinding binding)
        {
            this.Add(binding);
        }

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

		private void toEDialect(CodeWriter writer) {
			writer.append("define category bindings as:\n");
			writer.indent();
			foreach(NativeCategoryBinding m in this) {
				m.ToDialect(writer);
				writer.newLine();
			}
			writer.dedent();	
		}

		private void toPDialect(CodeWriter writer) {
			writer.append("def category bindings:\n");
			writer.indent();
			foreach(NativeCategoryBinding m in this) {
				m.ToDialect(writer);
				writer.newLine();
			}
			writer.dedent();	
		}

		private void toODialect(CodeWriter writer) {
			writer.append("category bindings {\n");
			writer.indent();
			foreach(NativeCategoryBinding m in this) {
				m.ToDialect(writer);
				writer.append(';');
				writer.newLine();
			}
			writer.dedent();	
			writer.append("}");
		}

    }

}

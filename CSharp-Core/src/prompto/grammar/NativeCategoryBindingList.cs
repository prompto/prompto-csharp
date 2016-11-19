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
				ToEDialect(writer);
				break;
			case Dialect.O:
				ToODialect(writer);
				break;
			case Dialect.M:
				toPDialect(writer);
				break;
			}
		}

		private void ToEDialect(CodeWriter writer) {
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

		private void ToODialect(CodeWriter writer) {
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

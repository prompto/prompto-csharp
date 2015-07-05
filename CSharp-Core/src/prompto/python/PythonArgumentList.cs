using prompto.utils;

namespace prompto.python
{

	public class PythonArgumentList  : ObjectList<PythonArgument>
    {

		public PythonArgumentList() {
		}

		public PythonArgumentList(PythonArgument arg) {
			this.add(arg);
		}

		public void ToDialect(CodeWriter writer) {
			if(this.Count>0) {
				foreach(PythonArgument arg in this) {
					arg.ToDialect(writer);
					writer.append(", ");
				}
				writer.trimLast(2);
			}
		}
    }
}

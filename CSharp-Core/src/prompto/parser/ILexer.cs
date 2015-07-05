using Antlr4.Runtime;

namespace prompto.parser {

public interface ILexer : ITokenSource {
    Dialect Dialect { get; }
}

}

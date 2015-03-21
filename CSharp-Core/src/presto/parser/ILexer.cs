using Antlr4.Runtime;

namespace presto.parser {

public interface ILexer : ITokenSource {
    Dialect Dialect { get; }
}

}

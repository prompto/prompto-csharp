//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.5
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from ArgsParser.g4 by ANTLR 4.5

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591

namespace presto.utils {
using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="ArgsParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.5")]
[System.CLSCompliant(false)]
public interface IArgsParserListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="ArgsParser.parse"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterParse([NotNull] ArgsParser.ParseContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ArgsParser.parse"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitParse([NotNull] ArgsParser.ParseContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ArgsParser.entry"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterEntry([NotNull] ArgsParser.EntryContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ArgsParser.entry"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitEntry([NotNull] ArgsParser.EntryContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ArgsParser.key"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterKey([NotNull] ArgsParser.KeyContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ArgsParser.key"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitKey([NotNull] ArgsParser.KeyContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>ELEMENT</c>
	/// labeled alternative in <see cref="ArgsParser.value"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterELEMENT([NotNull] ArgsParser.ELEMENTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>ELEMENT</c>
	/// labeled alternative in <see cref="ArgsParser.value"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitELEMENT([NotNull] ArgsParser.ELEMENTContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>STRING</c>
	/// labeled alternative in <see cref="ArgsParser.value"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSTRING([NotNull] ArgsParser.STRINGContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>STRING</c>
	/// labeled alternative in <see cref="ArgsParser.value"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSTRING([NotNull] ArgsParser.STRINGContext context);
}
} // namespace presto.utils
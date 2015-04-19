using Antlr4.Runtime.Tree;
using Antlr4.Runtime;
using presto.parser;
using System;
using presto.grammar;
using presto.expression;
using presto.literal;
using presto.type;
using presto.declaration;
using presto.statement;
using presto.java;
using presto.python;
using presto.csharp;
using Antlr4.Runtime.Misc;
using System.Collections.Generic;
using presto.javascript;
using presto.utils;

namespace presto.parser
{

	public class EPrestoBuilder : EParserBaseListener
	{

		ParseTreeProperty<object> nodeValues = new ParseTreeProperty<object> ();
		ITokenStream input;
		string path = "";

		public EPrestoBuilder (ECleverParser parser)
		{
			this.input = (ITokenStream)parser.InputStream;
			this.path = parser.Path;
		}

		public T GetNodeValue<T> (IParseTree node)
		{
			if (node == null)
				return default(T);
			object o = nodeValues.Get (node);
			if (o == null)
				return default(T);
			if (o is T)
				return (T)o;
			else
				throw new Exception ("Unexpected");
		}

		public void SetNodeValue (IParseTree node, object value)
		{
			nodeValues.Put (node, value);
		}

		public void SetNodeValue (ParserRuleContext node, Section value)
		{
			nodeValues.Put (node, value);
			BuildSection (node, value);
		}

		public void BuildSection (ParserRuleContext node, Section section)
		{
			IToken first = FindFirstValidToken (node.Start.TokenIndex);
			IToken last = FindLastValidToken (node.Stop.TokenIndex);
			section.SetFrom (path, first, last, Dialect.E);
		}

		private IToken FindFirstValidToken (int idx)
		{
			if (idx == -1) // happens because input.index() is called before any other read operation (bug?)
			idx = 0;
			do {
				IToken token = ReadValidToken (idx++);
				if (token != null)
					return token;
			} while(idx < input.Size);
			return null;
		}

		private IToken FindLastValidToken (int idx)
		{
			if (idx == -1) // happens because input.index() is called before any other read operation (bug?)
			idx = 0;
			while (idx >= 0) {
				IToken token = ReadValidToken (idx--);
				if (token != null)
					return token;
			}
			return null;
		}

		private IToken ReadValidToken (int idx)
		{
			IToken token = input.Get (idx);
			string text = token.Text;
			if (text != null && text.Length > 0 && !Char.IsWhiteSpace (text [0]))
				return token;
			else
				return null;
		}

		
		public override void ExitIdentifierExpression (EParser.IdentifierExpressionContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.exp);
			SetNodeValue (ctx, new UnresolvedIdentifier (name));
		}

		
		public override void ExitTypeIdentifier (EParser.TypeIdentifierContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, name);
		}

		
		public override void ExitMethodCallExpression (EParser.MethodCallExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			ArgumentAssignmentList args = this.GetNodeValue<ArgumentAssignmentList> (ctx.args);
			Object call = new UnresolvedCall (exp, args);
			SetNodeValue (ctx, call);
		}

		
		public override void ExitUnresolvedExpression (EParser.UnresolvedExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitUnresolvedIdentifier (EParser.UnresolvedIdentifierContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new UnresolvedIdentifier (name));
		}

		
		public override void ExitUnresolvedSelector (EParser.UnresolvedSelectorContext ctx)
		{
			IExpression parent = this.GetNodeValue<IExpression> (ctx.parent);
			SelectorExpression selector = this.GetNodeValue<SelectorExpression> (ctx.selector);
			selector.setParent (parent);
			SetNodeValue (ctx, selector);
		}

		
		public override void ExitUnresolved_selector (EParser.Unresolved_selectorContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new MemberSelector (name));
		}

		
		public override void ExitAtomicLiteral (EParser.AtomicLiteralContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitCollectionLiteral (EParser.CollectionLiteralContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitListLiteral (EParser.ListLiteralContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitBooleanLiteral (EParser.BooleanLiteralContext ctx)
		{
			SetNodeValue (ctx, new BooleanLiteral (ctx.t.Text));
		}

		
		public override void ExitMinIntegerLiteral (EParser.MinIntegerLiteralContext ctx)
		{
			SetNodeValue (ctx, new MinIntegerLiteral ());
		}

		
		public override void ExitMaxIntegerLiteral (EParser.MaxIntegerLiteralContext ctx)
		{
			SetNodeValue (ctx, new MaxIntegerLiteral ());
		}

		
		public override void ExitIntegerLiteral (EParser.IntegerLiteralContext ctx)
		{
			SetNodeValue (ctx, new IntegerLiteral (ctx.t.Text));
		}

		
		public override void ExitDecimalLiteral (EParser.DecimalLiteralContext ctx)
		{
			SetNodeValue (ctx, new DecimalLiteral (ctx.t.Text));
		}

		
		public override void ExitHexadecimalLiteral (EParser.HexadecimalLiteralContext ctx)
		{
			SetNodeValue (ctx, new HexaLiteral (ctx.t.Text));
		}

		
		public override void ExitCharacterLiteral (EParser.CharacterLiteralContext ctx)
		{
			SetNodeValue (ctx, new CharacterLiteral (ctx.t.Text));
		}

		
		public override void ExitDateLiteral (EParser.DateLiteralContext ctx)
		{
			SetNodeValue (ctx, new DateLiteral (ctx.t.Text));
		}

		
		public override void ExitDateTimeLiteral (EParser.DateTimeLiteralContext ctx)
		{
			SetNodeValue (ctx, new DateTimeLiteral (ctx.t.Text));
		}

		public override void ExitTernaryExpression (EParser.TernaryExpressionContext ctx)
		{
			IExpression condition = this.GetNodeValue<IExpression> (ctx.test);
			IExpression ifTrue = this.GetNodeValue<IExpression> (ctx.ifTrue);
			IExpression ifFalse = this.GetNodeValue<IExpression> (ctx.ifFalse);
			TernaryExpression exp = new TernaryExpression (condition, ifTrue, ifFalse);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitTest_method_declaration(EParser.Test_method_declarationContext ctx) {
			String name = ctx.name.Text;
			StatementList stmts = this.GetNodeValue<StatementList>(ctx.stmts);
			ExpressionList exps = this.GetNodeValue<ExpressionList>(ctx.exps);
			String errorName = this.GetNodeValue<String>(ctx.error);
			SymbolExpression error = errorName==null ? null : new SymbolExpression(errorName);
			SetNodeValue(ctx, new TestMethodDeclaration(name, stmts, exps, error));
		}

		public override void ExitTestMethod(EParser.TestMethodContext ctx) {
			IDeclaration decl = this.GetNodeValue<IDeclaration>(ctx.decl);
			SetNodeValue(ctx, decl);
		}

		public override void ExitTextLiteral (EParser.TextLiteralContext ctx)
		{
			SetNodeValue (ctx, new TextLiteral (ctx.t.Text));
		}

		
		public override void ExitTimeLiteral (EParser.TimeLiteralContext ctx)
		{
			SetNodeValue (ctx, new TimeLiteral (ctx.t.Text));
		}

		
		public override void ExitPeriodLiteral (EParser.PeriodLiteralContext ctx)
		{
			SetNodeValue (ctx, new PeriodLiteral (ctx.t.Text));
		}

		
		public override void ExitVariable_identifier (EParser.Variable_identifierContext ctx)
		{
			SetNodeValue (ctx, ctx.GetText ());
		}

		
		public override void ExitList_literal (EParser.List_literalContext ctx)
		{
			ExpressionList items = this.GetNodeValue<ExpressionList> (ctx.items);
			IExpression value = items == null ? new ListLiteral () : new ListLiteral (items);
			SetNodeValue (ctx, value);
		}

		
		public override void ExitDict_literal (EParser.Dict_literalContext ctx)
		{
			DictEntryList items = this.GetNodeValue<DictEntryList> (ctx.items);
			IExpression value = items == null ? new DictLiteral () : new DictLiteral (items);
			SetNodeValue (ctx, value);
		}

		
		public override void ExitTuple_literal (EParser.Tuple_literalContext ctx)
		{
			ExpressionList items = this.GetNodeValue<ExpressionList> (ctx.items);
			IExpression value = items == null ? new TupleLiteral () : new TupleLiteral (items);
			SetNodeValue (ctx, value);
		}

		
		public override void ExitTupleLiteral (EParser.TupleLiteralContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}


		
		public override void ExitRange_literal (EParser.Range_literalContext ctx)
		{
			IExpression low = this.GetNodeValue<IExpression> (ctx.low);
			IExpression high = this.GetNodeValue<IExpression> (ctx.high);
			SetNodeValue (ctx, new RangeLiteral (low, high));
		}

		
		public override void ExitRangeLiteral (EParser.RangeLiteralContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitDictLiteral (EParser.DictLiteralContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitDictEntryList (EParser.DictEntryListContext ctx)
		{
			DictEntry item = this.GetNodeValue<DictEntry> (ctx.item);
			SetNodeValue (ctx, new DictEntryList (item));
		}

		
		public override void ExitDictEntryListItem (EParser.DictEntryListItemContext ctx)
		{
			DictEntryList items = this.GetNodeValue<DictEntryList> (ctx.items);
			DictEntry item = this.GetNodeValue<DictEntry> (ctx.item);
			items.add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitDict_entry (EParser.Dict_entryContext ctx)
		{
			IExpression key = this.GetNodeValue<IExpression> (ctx.key);
			IExpression value = this.GetNodeValue<IExpression> (ctx.value);
			DictEntry entry = new DictEntry (key, value);
			SetNodeValue (ctx, entry);
		}

		
		public override void ExitLiteralExpression (EParser.LiteralExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitVariableIdentifier (EParser.VariableIdentifierContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, name);
		}

		
		public override void ExitValueList (EParser.ValueListContext ctx)
		{
			IExpression item = this.GetNodeValue<IExpression> (ctx.item);
			SetNodeValue (ctx, new ExpressionList (item));
		}

		
		public override void ExitValueListItem (EParser.ValueListItemContext ctx)
		{
			ExpressionList items = this.GetNodeValue<ExpressionList> (ctx.items);
			IExpression item = this.GetNodeValue<IExpression> (ctx.item);
			items.add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitValueTuple (EParser.ValueTupleContext ctx)
		{
			IExpression item = this.GetNodeValue<IExpression> (ctx.item);
			SetNodeValue (ctx, new ExpressionList (item));
		}

		
		public override void ExitValueTupleItem (EParser.ValueTupleItemContext ctx)
		{
			ExpressionList items = this.GetNodeValue<ExpressionList> (ctx.items);
			IExpression item = this.GetNodeValue<IExpression> (ctx.item);
			items.add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitSymbol_identifier (EParser.Symbol_identifierContext ctx)
		{
			SetNodeValue (ctx, ctx.GetText ());
		}

		
		public override void ExitNative_symbol (EParser.Native_symbolContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new NativeSymbol (name, exp));
		}

		
		public override void ExitSymbolIdentifier (EParser.SymbolIdentifierContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, name);
		}

		
		public override void ExitBooleanType (EParser.BooleanTypeContext ctx)
		{
			SetNodeValue (ctx, BooleanType.Instance);
		}

		
		public override void ExitCharacterType (EParser.CharacterTypeContext ctx)
		{
			SetNodeValue (ctx, CharacterType.Instance);
		}

		
		public override void ExitTextType (EParser.TextTypeContext ctx)
		{
			SetNodeValue (ctx, TextType.Instance);
		}

		public override void ExitThisExpression (EParser.ThisExpressionContext ctx)
		{
			SetNodeValue (ctx, new ThisExpression ());
		}
		
		public override void ExitIntegerType (EParser.IntegerTypeContext ctx)
		{
			SetNodeValue (ctx, IntegerType.Instance);
		}

		
		public override void ExitDecimalType (EParser.DecimalTypeContext ctx)
		{
			SetNodeValue (ctx, DecimalType.Instance);
		}

		
		public override void ExitDateType (EParser.DateTypeContext ctx)
		{
			SetNodeValue (ctx, DateType.Instance);
		}

		
		public override void ExitDateTimeType (EParser.DateTimeTypeContext ctx)
		{
			SetNodeValue (ctx, TextType.Instance);
		}

		
		public override void ExitTimeType (EParser.TimeTypeContext ctx)
		{
			SetNodeValue (ctx, TimeType.Instance);
		}

		
		public override void ExitCodeType (EParser.CodeTypeContext ctx)
		{
			SetNodeValue (ctx, CodeType.Instance);
		}

		
		public override void ExitPrimaryType (EParser.PrimaryTypeContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.p);
			SetNodeValue (ctx, type);
		}

		
		public override void ExitAttribute_declaration (EParser.Attribute_declarationContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			IType type = this.GetNodeValue<IType> (ctx.typ);
			IAttributeConstraint match = this.GetNodeValue<IAttributeConstraint> (ctx.match);
			SetNodeValue (ctx, new AttributeDeclaration (name, type, match));
		}

		
		public override void ExitNativeType (EParser.NativeTypeContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.n);
			SetNodeValue (ctx, type);
		}

		
		public override void ExitCategoryType (EParser.CategoryTypeContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.c);
			SetNodeValue (ctx, type);
		}

		
		public override void ExitCategory_type (EParser.Category_typeContext ctx)
		{
			String name = ctx.GetText ();
			SetNodeValue (ctx, new CategoryType (name));
		}

		
		public override void ExitListType (EParser.ListTypeContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.l);
			SetNodeValue (ctx, new ListType (type));
		}

		
		public override void ExitDictType (EParser.DictTypeContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.d);
			SetNodeValue (ctx, new DictType (type));
		}

		
		public override void ExitAttributeList (EParser.AttributeListContext ctx)
		{
			String item = this.GetNodeValue<String> (ctx.item);
			SetNodeValue (ctx, new IdentifierList (item));
		}

		
		public override void ExitAttributeListItem (EParser.AttributeListItemContext ctx)
		{
			IdentifierList items = this.GetNodeValue<IdentifierList> (ctx.items);
			String item = this.GetNodeValue<String> (ctx.item);
			items.Add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitVariableList (EParser.VariableListContext ctx)
		{
			String item = this.GetNodeValue<String> (ctx.item);
			SetNodeValue (ctx, new IdentifierList (item));
		}

		
		public override void ExitVariableListItem (EParser.VariableListItemContext ctx)
		{
			IdentifierList items = this.GetNodeValue<IdentifierList> (ctx.items);
			String item = this.GetNodeValue<String> (ctx.item);
			items.Add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitConcrete_category_declaration (EParser.Concrete_category_declarationContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			IdentifierList attrs = this.GetNodeValue<IdentifierList> (ctx.attrs);
			IdentifierList derived = this.GetNodeValue<IdentifierList> (ctx.derived);
			CategoryMethodDeclarationList methods = this.GetNodeValue<CategoryMethodDeclarationList> (ctx.methods);
			SetNodeValue (ctx, new ConcreteCategoryDeclaration (name, attrs, derived, methods));
		}

		
		public override void ExitConcreteCategoryDeclaration (EParser.ConcreteCategoryDeclarationContext ctx)
		{
			ConcreteCategoryDeclaration decl = this.GetNodeValue<ConcreteCategoryDeclaration> (ctx.decl);
			SetNodeValue (ctx, decl);
		}

	
		
		public override void ExitType_identifier (EParser.Type_identifierContext ctx)
		{
			SetNodeValue (ctx, ctx.GetText ());
		}

		
		public override void ExitDerivedList (EParser.DerivedListContext ctx)
		{
			IdentifierList items = this.GetNodeValue<IdentifierList> (ctx.items);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitDerivedListItem (EParser.DerivedListItemContext ctx)
		{
			IdentifierList items = this.GetNodeValue<IdentifierList> (ctx.items);
			String item = this.GetNodeValue<String> (ctx.item);
			items.Add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitTypeIdentifierList (EParser.TypeIdentifierListContext ctx)
		{
			String item = this.GetNodeValue<String> (ctx.item);
			SetNodeValue (ctx, new IdentifierList (item));
		}

		
		public override void ExitTypeIdentifierListItem (EParser.TypeIdentifierListItemContext ctx)
		{
			IdentifierList items = this.GetNodeValue<IdentifierList> (ctx.items);
			String item = this.GetNodeValue<String> (ctx.item);
			items.Add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitInstanceExpression (EParser.InstanceExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitSelectableExpression (EParser.SelectableExpressionContext ctx)
		{
			IExpression parent = this.GetNodeValue<IExpression> (ctx.parent);
			SetNodeValue (ctx, parent);
		}

		
		public override void ExitSelectorExpression (EParser.SelectorExpressionContext ctx)
		{
			IExpression parent = this.GetNodeValue<IExpression> (ctx.parent);
			SelectorExpression selector = this.GetNodeValue<SelectorExpression> (ctx.selector);
			selector.setParent (parent);
			SetNodeValue (ctx, selector);
		}

		public override void ExitSet_literal (EParser.Set_literalContext ctx)
		{
			ExpressionList items = this.GetNodeValue<ExpressionList>(ctx.items);
			SetLiteral set = items==null ? new SetLiteral() : new SetLiteral(items);
			SetNodeValue(ctx, set);
		}

		public override void ExitSetLiteral (EParser.SetLiteralContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression>(ctx.exp);
			SetNodeValue(ctx, exp);
		}

		
		public override void ExitMemberSelector (EParser.MemberSelectorContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new MemberSelector (name));
		}

		
		public override void ExitItemSelector (EParser.ItemSelectorContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new ItemSelector (exp));
		}

		
		public override void ExitSliceSelector (EParser.SliceSelectorContext ctx)
		{
			IExpression slice = this.GetNodeValue<IExpression> (ctx.xslice);
			SetNodeValue (ctx, slice);
		}

		
		public override void ExitTyped_argument (EParser.Typed_argumentContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.typ);
			String name = this.GetNodeValue<String> (ctx.name);
			IdentifierList attrs = this.GetNodeValue<IdentifierList> (ctx.attrs);
			CategoryArgument arg = new CategoryArgument (type, name, attrs);
			IExpression exp = this.GetNodeValue<IExpression> (ctx.value);
			arg.DefaultValue = exp;
			SetNodeValue (ctx, arg);
		}

		
		public override void ExitTypedArgument (EParser.TypedArgumentContext ctx)
		{
			IArgument arg = this.GetNodeValue<IArgument> (ctx.arg); 
			SetNodeValue (ctx, arg);
		}

		
		public override void ExitNamedArgument (EParser.NamedArgumentContext ctx)
		{
			IArgument arg = this.GetNodeValue<IArgument> (ctx.arg);
			SetNodeValue (ctx, arg);
		}

		
		public override void ExitCodeArgument (EParser.CodeArgumentContext ctx)
		{
			IArgument arg = this.GetNodeValue<IArgument> (ctx.arg);
			SetNodeValue (ctx, arg);
		}

		
		public override void ExitCategoryArgumentType (EParser.CategoryArgumentTypeContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.typ);
			SetNodeValue (ctx, type);
		}

		
		public override void ExitArgumentList (EParser.ArgumentListContext ctx)
		{
			IArgument item = this.GetNodeValue<IArgument> (ctx.item); 
			SetNodeValue (ctx, new ArgumentList (item));
		}

		
		public override void ExitArgumentListItem (EParser.ArgumentListItemContext ctx)
		{
			ArgumentList items = this.GetNodeValue<ArgumentList> (ctx.items); 
			IArgument item = this.GetNodeValue<IArgument> (ctx.item); 
			items.Add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitFull_argument_list (EParser.Full_argument_listContext ctx)
		{
			ArgumentList items = this.GetNodeValue<ArgumentList> (ctx.items); 
			IArgument item = this.GetNodeValue<IArgument> (ctx.item); 
			if (item != null)
				items.Add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitMethodTypeIdentifier (EParser.MethodTypeIdentifierContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, name);
		}

		
		public override void ExitMethodVariableIdentifier (EParser.MethodVariableIdentifierContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, name);
		}

		
		public override void ExitArgument_assignment (EParser.Argument_assignmentContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			IArgument arg = new UnresolvedArgument (name);
			SetNodeValue (ctx, new ArgumentAssignment (arg, exp));
		}

		
		public override void ExitArgumentAssignmentListExpression (EParser.ArgumentAssignmentListExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			ArgumentAssignmentList items = this.GetNodeValue<ArgumentAssignmentList> (ctx.items);
			if (items == null)
				items = new ArgumentAssignmentList ();
			items.Insert (0, new ArgumentAssignment (null, exp));
			ArgumentAssignment item = this.GetNodeValue<ArgumentAssignment> (ctx.item);
			if (item != null)
				items.add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitArgumentAssignmentListNoExpression (EParser.ArgumentAssignmentListNoExpressionContext ctx)
		{
			ArgumentAssignmentList items = this.GetNodeValue<ArgumentAssignmentList> (ctx.items);
			ArgumentAssignment item = this.GetNodeValue<ArgumentAssignment> (ctx.item);
			if (item != null)
				items.add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitArgumentAssignmentList (EParser.ArgumentAssignmentListContext ctx)
		{
			ArgumentAssignment item = this.GetNodeValue<ArgumentAssignment> (ctx.item);
			ArgumentAssignmentList items = new ArgumentAssignmentList (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitArgumentAssignmentListItem (EParser.ArgumentAssignmentListItemContext ctx)
		{
			ArgumentAssignment item = this.GetNodeValue<ArgumentAssignment> (ctx.item);
			ArgumentAssignmentList items = this.GetNodeValue<ArgumentAssignmentList> (ctx.items);
			items.add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitUnresolvedWithArgsStatement (EParser.UnresolvedWithArgsStatementContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			ArgumentAssignmentList args = this.GetNodeValue<ArgumentAssignmentList> (ctx.args);
			SetNodeValue (ctx, new UnresolvedCall (exp, args));
		}


		
		public override void ExitAddExpression (EParser.AddExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			IExpression exp = ctx.op.Type == EParser.PLUS ? 
            (IExpression)new AddExpression (left, right) 
            : (IExpression)new SubtractExpression (left, right);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitCategoryMethodList (EParser.CategoryMethodListContext ctx)
		{
			ICategoryMethodDeclaration item = this.GetNodeValue<ICategoryMethodDeclaration> (ctx.item);
			CategoryMethodDeclarationList items = new CategoryMethodDeclarationList (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitCategoryMethodListItem (EParser.CategoryMethodListItemContext ctx)
		{
			ICategoryMethodDeclaration item = this.GetNodeValue<ICategoryMethodDeclaration> (ctx.item);
			CategoryMethodDeclarationList items = this.GetNodeValue<CategoryMethodDeclarationList> (ctx.items);
			items.add (item);
			SetNodeValue (ctx, items);
		}

		public override void ExitSetter_method_declaration (EParser.Setter_method_declarationContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new SetterMethodDeclaration (name, stmts));
		}

		
		public override void ExitGetter_method_declaration (EParser.Getter_method_declarationContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new GetterMethodDeclaration (name, stmts));
		}

		public override void ExitAbstractMemberMethod (EParser.AbstractMemberMethodContext ctx)
		{
			IDeclaration decl = this.GetNodeValue<IDeclaration> (ctx.decl);
			SetNodeValue (ctx, decl);
		}

		public override void ExitConcreteMemberMethod (EParser.ConcreteMemberMethodContext ctx)
		{
			IDeclaration decl = this.GetNodeValue<IDeclaration> (ctx.decl);
			SetNodeValue (ctx, decl);
		}

		
		public override void ExitSetterMemberMethod (EParser.SetterMemberMethodContext ctx)
		{
			IDeclaration decl = this.GetNodeValue<IDeclaration> (ctx.decl);
			SetNodeValue (ctx, decl);
		}

		
		public override void ExitGetterMemberMethod (EParser.GetterMemberMethodContext ctx)
		{
			IDeclaration decl = this.GetNodeValue<IDeclaration> (ctx.decl);
			SetNodeValue (ctx, decl);
		}

		
		public override void ExitStatementList (EParser.StatementListContext ctx)
		{
			IStatement item = this.GetNodeValue<IStatement> (ctx.item);
			SetNodeValue (ctx, new StatementList (item));
		}

		
		public override void ExitStatementListItem (EParser.StatementListItemContext ctx)
		{
			IStatement item = this.GetNodeValue<IStatement> (ctx.item);
			StatementList items = this.GetNodeValue<StatementList> (ctx.items);
			items.add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitAbstract_method_declaration (EParser.Abstract_method_declarationContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.typ);
			String name = this.GetNodeValue<String> (ctx.name);
			ArgumentList args = this.GetNodeValue<ArgumentList> (ctx.args);
			SetNodeValue (ctx, new AbstractMethodDeclaration (name, args, type));
		}

		
		public override void ExitConcrete_method_declaration (EParser.Concrete_method_declarationContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.typ);
			String name = this.GetNodeValue<String> (ctx.name);
			ArgumentList args = this.GetNodeValue<ArgumentList> (ctx.args);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new ConcreteMethodDeclaration (name, args, type, stmts));
		}

		
		public override void ExitMethodCallStatement (EParser.MethodCallStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitConstructorFrom (EParser.ConstructorFromContext ctx)
		{
			bool mutable = ctx.MUTABLE() != null;
			CategoryType type = this.GetNodeValue<CategoryType> (ctx.typ);
			ArgumentAssignmentList args = this.GetNodeValue<ArgumentAssignmentList> (ctx.args);
			if (args == null)
				args = new ArgumentAssignmentList ();
			IExpression firstArg = this.GetNodeValue<IExpression> (ctx.firstArg);
			args.Insert (0, new ArgumentAssignment (null, firstArg));
			ArgumentAssignment arg = this.GetNodeValue<ArgumentAssignment> (ctx.arg);
			if (arg != null)
				args.add (arg);
			SetNodeValue (ctx, new ConstructorExpression (type, mutable, args));
		}

		
		public override void ExitConstructorNoFrom (EParser.ConstructorNoFromContext ctx)
		{
			bool mutable = ctx.MUTABLE() != null;
			CategoryType type = this.GetNodeValue<CategoryType> (ctx.typ);
			ArgumentAssignmentList args = this.GetNodeValue<ArgumentAssignmentList> (ctx.args);
			if (args == null)
				args = new ArgumentAssignmentList ();
			ArgumentAssignment arg = this.GetNodeValue<ArgumentAssignment> (ctx.arg);
			if (arg != null)
				args.add (arg);
			SetNodeValue (ctx, new ConstructorExpression (type, mutable, args));
		}

		public override void ExitAssertion(EParser.AssertionContext ctx) {
			IExpression exp = this.GetNodeValue<IExpression>(ctx.exp);
			SetNodeValue(ctx, exp);
		}

		public override void ExitAssertionList(EParser.AssertionListContext ctx) {
			IExpression item = this.GetNodeValue<IExpression>(ctx.item);
			ExpressionList items = new ExpressionList(item);
			SetNodeValue(ctx, items);
		}

		public override void ExitAssertionListItem(EParser.AssertionListItemContext ctx) {
			IExpression item = this.GetNodeValue<IExpression>(ctx.item);
			ExpressionList items = this.GetNodeValue<ExpressionList>(ctx.items);
			items.add(item);
			SetNodeValue(ctx, items);
		}
			
		public override void ExitAssign_instance_statement (EParser.Assign_instance_statementContext ctx)
		{
			IAssignableInstance inst = this.GetNodeValue<IAssignableInstance> (ctx.inst);
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new AssignInstanceStatement (inst, exp));
		}

		
		public override void ExitAssignInstanceStatement (EParser.AssignInstanceStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitAssign_variable_statement (EParser.Assign_variable_statementContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new AssignVariableStatement (name, exp));
		}

		
		public override void ExitAssign_tuple_statement (EParser.Assign_tuple_statementContext ctx)
		{
			IdentifierList items = this.GetNodeValue<IdentifierList> (ctx.items);
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new AssignTupleStatement (items, exp));
		}

		
		public override void ExitRootInstance (EParser.RootInstanceContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new VariableInstance (name));
		}

		public override void ExitRoughlyEqualsExpression (EParser.RoughlyEqualsExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new EqualsExpression (left, EqOp.ROUGHLY, right));
		}

		
		public override void ExitChildInstance (EParser.ChildInstanceContext ctx)
		{
			IAssignableInstance parent = this.GetNodeValue<IAssignableInstance> (ctx.parent);
			IAssignableSelector child = this.GetNodeValue<IAssignableSelector> (ctx.child);
			child.SetParent (parent);
			SetNodeValue (ctx, child);
		}

		
		public override void ExitMemberInstance (EParser.MemberInstanceContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new MemberInstance (name));
		}

		
		public override void ExitItemInstance (EParser.ItemInstanceContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new ItemInstance (exp));
		}

		
		public override void ExitConstructorExpression (EParser.ConstructorExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitNativeStatementList (EParser.NativeStatementListContext ctx)
		{
			IStatement item = this.GetNodeValue<IStatement> (ctx.item);
			StatementList items = new StatementList (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitNativeStatementListItem (EParser.NativeStatementListItemContext ctx)
		{
			IStatement item = this.GetNodeValue<IStatement> (ctx.item);
			StatementList items = this.GetNodeValue<StatementList> (ctx.items);
			items.add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitJava_identifier (EParser.Java_identifierContext ctx)
		{
			SetNodeValue (ctx, ctx.GetText ());
		}

		
		public override void ExitCsharp_identifier (EParser.Csharp_identifierContext ctx)
		{
			SetNodeValue (ctx, ctx.GetText ());
		}

		
		public override void ExitPython_identifier (EParser.Python_identifierContext ctx)
		{
			SetNodeValue (ctx, ctx.GetText ());
		}

		
		public override void ExitJavaIdentifier (EParser.JavaIdentifierContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new JavaIdentifierExpression (name));
		}

		
		public override void ExitCSharpIdentifier (EParser.CSharpIdentifierContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new CSharpIdentifierExpression (name));
		}

		public override void ExitCSharpPrestoIdentifier (EParser.CSharpPrestoIdentifierContext ctx)
		{
			String name = ctx.DOLLAR_IDENTIFIER().GetText();
			SetNodeValue (ctx, new CSharpIdentifierExpression (name));
		}
		
		public override void ExitCSharpPrimaryExpression (EParser.CSharpPrimaryExpressionContext ctx)
		{
			CSharpExpression exp = this.GetNodeValue<CSharpExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitPythonIdentifier (EParser.PythonIdentifierContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new PythonIdentifierExpression (name));
		}

    
		
		public override void ExitJavaIdentifierExpression (EParser.JavaIdentifierExpressionContext ctx)
		{
			JavaExpression exp = this.GetNodeValue<JavaIdentifierExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitJavaPrimaryExpression (EParser.JavaPrimaryExpressionContext ctx)
		{
			JavaExpression exp = this.GetNodeValue<JavaExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitJavaChildIdentifier (EParser.JavaChildIdentifierContext ctx)
		{
			JavaIdentifierExpression parent = this.GetNodeValue<JavaIdentifierExpression> (ctx.parent);
			String name = this.GetNodeValue<String> (ctx.name);
			JavaIdentifierExpression child = new JavaIdentifierExpression (parent, name);
			SetNodeValue (ctx, child);
		}

		
		public override void ExitCSharpChildIdentifier (EParser.CSharpChildIdentifierContext ctx)
		{
			CSharpIdentifierExpression parent = this.GetNodeValue<CSharpIdentifierExpression> (ctx.parent);
			String name = this.GetNodeValue<String> (ctx.name);
			CSharpIdentifierExpression child = new CSharpIdentifierExpression (parent, name);
			SetNodeValue (ctx, child);
		}

		
		public override void ExitPythonChildIdentifier (EParser.PythonChildIdentifierContext ctx)
		{
			PythonIdentifierExpression parent = this.GetNodeValue<PythonIdentifierExpression> (ctx.parent);
			String name = this.GetNodeValue<String> (ctx.name);
			PythonIdentifierExpression child = new PythonIdentifierExpression (parent, name);
			SetNodeValue (ctx, child);
		}

	
		
		public override void ExitJavaClassIdentifier (EParser.JavaClassIdentifierContext ctx)
		{
			JavaIdentifierExpression klass = this.GetNodeValue<JavaIdentifierExpression> (ctx.klass);
			SetNodeValue (ctx, klass);
		}

		
		public override void ExitJavaChildClassIdentifier (EParser.JavaChildClassIdentifierContext ctx)
		{
			JavaIdentifierExpression parent = this.GetNodeValue<JavaIdentifierExpression> (ctx.parent);
			JavaIdentifierExpression child = new JavaIdentifierExpression (parent, ctx.name.Text);
			SetNodeValue (ctx, child);
		}

		
		public override void ExitCSharpSelectorExpression (EParser.CSharpSelectorExpressionContext ctx)
		{
			CSharpExpression parent = this.GetNodeValue<CSharpExpression> (ctx.parent);
			CSharpSelectorExpression child = this.GetNodeValue<CSharpSelectorExpression> (ctx.child);
			child.SetParent (parent);
			SetNodeValue (ctx, child);
		}

		
		public override void ExitJavaSelectorExpression (EParser.JavaSelectorExpressionContext ctx)
		{
			JavaExpression parent = this.GetNodeValue<JavaExpression> (ctx.parent);
			JavaSelectorExpression child = this.GetNodeValue<JavaSelectorExpression> (ctx.child);
			child.SetParent (parent);
			SetNodeValue (ctx, child);
		}

		 
		public override void ExitCsharp_item_expression (EParser.Csharp_item_expressionContext ctx)
		{
			CSharpExpression exp = this.GetNodeValue<CSharpExpression> (ctx.exp);
			SetNodeValue (ctx, new CSharpItemExpression (exp));
		}

		
		public override void ExitJava_item_expression (EParser.Java_item_expressionContext ctx)
		{
			JavaExpression exp = this.GetNodeValue<JavaExpression> (ctx.exp);
			SetNodeValue (ctx, new JavaItemExpression (exp));
		}

		public override void ExitCSharpIdentifierExpression (EParser.CSharpIdentifierExpressionContext ctx)
		{
			CSharpExpression exp = this.GetNodeValue<CSharpExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		public override void ExitCsharp_method_expression (EParser.Csharp_method_expressionContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			CSharpExpressionList args = this.GetNodeValue<CSharpExpressionList> (ctx.args);
			SetNodeValue (ctx, new CSharpMethodExpression (name, args));
		}

		public override void ExitCSharpMethodExpression (EParser.CSharpMethodExpressionContext ctx)
		{
			CSharpExpression exp = this.GetNodeValue<CSharpExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitCSharpItemExpression (EParser.CSharpItemExpressionContext ctx)
		{
			CSharpExpression exp = this.GetNodeValue<CSharpExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitJavaItemExpression (EParser.JavaItemExpressionContext ctx)
		{
			JavaExpression exp = this.GetNodeValue<JavaExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitJavaStatement (EParser.JavaStatementContext ctx)
		{
			JavaExpression exp = this.GetNodeValue<JavaExpression> (ctx.exp);
			SetNodeValue (ctx, new JavaStatement (exp, false));
		}

		
		public override void ExitCSharpStatement (EParser.CSharpStatementContext ctx)
		{
			CSharpExpression exp = this.GetNodeValue<CSharpExpression> (ctx.exp);
			SetNodeValue (ctx, new CSharpStatement (exp, false));
		}

		
		public override void ExitPythonStatement (EParser.PythonStatementContext ctx)
		{
			PythonExpression exp = this.GetNodeValue<PythonExpression> (ctx.exp);
			SetNodeValue (ctx, new PythonStatement (exp, false));
		}

		
		public override void ExitJavaReturnStatement (EParser.JavaReturnStatementContext ctx)
		{
			JavaExpression exp = this.GetNodeValue<JavaExpression> (ctx.exp);
			SetNodeValue (ctx, new JavaStatement (exp, true));
		}

		
		public override void ExitCSharpReturnStatement (EParser.CSharpReturnStatementContext ctx)
		{
			CSharpExpression exp = this.GetNodeValue<CSharpExpression> (ctx.exp);
			SetNodeValue (ctx, new CSharpStatement (exp, true));
		}

		
		public override void ExitPythonReturnStatement (EParser.PythonReturnStatementContext ctx)
		{
			PythonExpression exp = this.GetNodeValue<PythonExpression> (ctx.exp);
			SetNodeValue (ctx, new PythonStatement (exp, true));
		}

		
		public override void ExitJavaNativeStatement (EParser.JavaNativeStatementContext ctx)
		{
			JavaStatement stmt = this.GetNodeValue<JavaStatement> (ctx.stmt);
			SetNodeValue (ctx, new JavaNativeCall (stmt));
		}

		
		public override void ExitCSharpNativeStatement (EParser.CSharpNativeStatementContext ctx)
		{
			CSharpStatement stmt = this.GetNodeValue<CSharpStatement> (ctx.stmt);
			SetNodeValue (ctx, new CSharpNativeCall (stmt));
		}

		
		public override void ExitPython2NativeStatement (EParser.Python2NativeStatementContext ctx)
		{
			PythonStatement stmt = this.GetNodeValue<PythonStatement> (ctx.stmt);
			SetNodeValue (ctx, new Python2NativeCall (stmt));
		}

		
		public override void ExitPython3NativeStatement (EParser.Python3NativeStatementContext ctx)
		{
			PythonStatement stmt = this.GetNodeValue<PythonStatement> (ctx.stmt);
			SetNodeValue (ctx, new Python3NativeCall (stmt));
		}

		
		public override void ExitNative_method_declaration (EParser.Native_method_declarationContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.typ);
			String name = this.GetNodeValue<String> (ctx.name);
			ArgumentList args = this.GetNodeValue<ArgumentList> (ctx.args);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new NativeMethodDeclaration (name, args, type, stmts));
		}

		public override void ExitCSharpArgumentList (EParser.CSharpArgumentListContext ctx)
		{
			CSharpExpression item = this.GetNodeValue<CSharpExpression> (ctx.item);
			SetNodeValue (ctx, new CSharpExpressionList (item));
		}

		public override void ExitCSharpArgumentListItem (EParser.CSharpArgumentListItemContext ctx)
		{
			CSharpExpression item = this.GetNodeValue<CSharpExpression> (ctx.item);
			CSharpExpressionList items = this.GetNodeValue<CSharpExpressionList> (ctx.items);
			items.Add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitJavaArgumentList (EParser.JavaArgumentListContext ctx)
		{
			JavaExpression item = this.GetNodeValue<JavaExpression> (ctx.item);
			SetNodeValue (ctx, new JavaExpressionList (item));
		}

		
		public override void ExitJavaArgumentListItem (EParser.JavaArgumentListItemContext ctx)
		{
			JavaExpression item = this.GetNodeValue<JavaExpression> (ctx.item);
			JavaExpressionList items = this.GetNodeValue<JavaExpressionList> (ctx.items);
			items.Add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitJava_method_expression (EParser.Java_method_expressionContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			JavaExpressionList args = this.GetNodeValue<JavaExpressionList> (ctx.args);
			SetNodeValue (ctx, new JavaMethodExpression (name, args));
		}

		
		public override void ExitJavaMethodExpression (EParser.JavaMethodExpressionContext ctx)
		{
			JavaExpression exp = this.GetNodeValue<JavaExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitFullDeclarationList (EParser.FullDeclarationListContext ctx)
		{
			DeclarationList items = this.GetNodeValue<DeclarationList> (ctx.items);
			if (items == null)
				items = new DeclarationList ();
			SetNodeValue (ctx, items);
		}

		
		public override void ExitDeclarationList (EParser.DeclarationListContext ctx)
		{
			IDeclaration item = this.GetNodeValue<IDeclaration> (ctx.item);
			DeclarationList items = new DeclarationList (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitDeclarationListItem (EParser.DeclarationListItemContext ctx)
		{
			IDeclaration item = this.GetNodeValue<IDeclaration> (ctx.item);
			DeclarationList items = this.GetNodeValue<DeclarationList> (ctx.items);
			items.Add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitMethodDeclaration (EParser.MethodDeclarationContext ctx)
		{
			IDeclaration decl = this.GetNodeValue<IDeclaration> (ctx.decl);
			SetNodeValue (ctx, decl);
		}

		
		public override void ExitNativeMethod (EParser.NativeMethodContext ctx)
		{
			IDeclaration decl = this.GetNodeValue<IDeclaration> (ctx.decl);
			SetNodeValue (ctx, decl);
		}

		
		public override void ExitConcreteMethod (EParser.ConcreteMethodContext ctx)
		{
			IDeclaration decl = this.GetNodeValue<IDeclaration> (ctx.decl);
			SetNodeValue (ctx, decl);
		}

		
		public override void ExitAbstractMethod (EParser.AbstractMethodContext ctx)
		{
			IDeclaration decl = this.GetNodeValue<IDeclaration> (ctx.decl);
			SetNodeValue (ctx, decl);
		}

		
		public override void ExitJavaBooleanLiteral (EParser.JavaBooleanLiteralContext ctx)
		{
			SetNodeValue (ctx, new JavaBooleanLiteral (ctx.GetText ()));
		}

		
		public override void ExitJavaIntegerLiteral (EParser.JavaIntegerLiteralContext ctx)
		{
			SetNodeValue (ctx, new JavaIntegerLiteral (ctx.GetText ()));
		}

		
		public override void ExitJavaDecimalLiteral (EParser.JavaDecimalLiteralContext ctx)
		{
			SetNodeValue (ctx, new JavaDecimalLiteral (ctx.GetText ()));
		}

		
		public override void ExitJavaCharacterLiteral (EParser.JavaCharacterLiteralContext ctx)
		{
			SetNodeValue (ctx, new JavaCharacterLiteral (ctx.GetText ()));
		}

		
		public override void ExitJavaTextLiteral (EParser.JavaTextLiteralContext ctx)
		{
			SetNodeValue (ctx, new JavaTextLiteral (ctx.GetText ()));
		}

		
		public override void ExitCSharpBooleanLiteral (EParser.CSharpBooleanLiteralContext ctx)
		{
			SetNodeValue (ctx, new CSharpBooleanLiteral (ctx.GetText ()));
		}

		
		public override void ExitCSharpIntegerLiteral (EParser.CSharpIntegerLiteralContext ctx)
		{
			SetNodeValue (ctx, new CSharpIntegerLiteral (ctx.GetText ()));
		}

		
		public override void ExitCSharpDecimalLiteral (EParser.CSharpDecimalLiteralContext ctx)
		{
			SetNodeValue (ctx, new CSharpDecimalLiteral (ctx.GetText ()));
		}

		
		public override void ExitCSharpCharacterLiteral (EParser.CSharpCharacterLiteralContext ctx)
		{
			SetNodeValue (ctx, new CSharpCharacterLiteral (ctx.GetText ()));
		}

		
		public override void ExitCSharpTextLiteral (EParser.CSharpTextLiteralContext ctx)
		{
			SetNodeValue (ctx, new CSharpTextLiteral (ctx.GetText ()));
		}

		
		public override void ExitPythonBooleanLiteral (EParser.PythonBooleanLiteralContext ctx)
		{
			SetNodeValue (ctx, new PythonBooleanLiteral (ctx.GetText ()));
		}

		public override void ExitPythonCharacterLiteral (EParser.PythonCharacterLiteralContext ctx)
		{
			SetNodeValue (ctx, new PythonCharacterLiteral (ctx.t.Text));
		}

		
		public override void ExitPythonIntegerLiteral (EParser.PythonIntegerLiteralContext ctx)
		{
			SetNodeValue (ctx, new PythonIntegerLiteral (ctx.GetText ()));
		}

		
		public override void ExitPythonDecimalLiteral (EParser.PythonDecimalLiteralContext ctx)
		{
			SetNodeValue (ctx, new PythonDecimalLiteral (ctx.GetText ()));
		}

		
		public override void ExitPythonTextLiteral (EParser.PythonTextLiteralContext ctx)
		{
			SetNodeValue (ctx, new PythonTextLiteral (ctx.GetText ()));
		}

		
		public override void ExitJavaLiteralExpression (EParser.JavaLiteralExpressionContext ctx)
		{
			JavaExpression exp = this.GetNodeValue<JavaExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}


		
		public override void ExitCSharpLiteralExpression (EParser.CSharpLiteralExpressionContext ctx)
		{
			CSharpExpression exp = this.GetNodeValue<CSharpExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitPythonLiteralExpression (EParser.PythonLiteralExpressionContext ctx)
		{
			PythonExpression exp = this.GetNodeValue<PythonExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitJavaCategoryBinding (EParser.JavaCategoryBindingContext ctx)
		{
			JavaIdentifierExpression map = this.GetNodeValue<JavaIdentifierExpression> (ctx.binding);
			SetNodeValue (ctx, new JavaNativeCategoryBinding (map));
		}

		
		public override void ExitCSharpCategoryBinding (EParser.CSharpCategoryBindingContext ctx)
		{
			CSharpIdentifierExpression map = this.GetNodeValue<CSharpIdentifierExpression> (ctx.binding);
			SetNodeValue (ctx, new CSharpNativeCategoryBinding (map));
		}


		
		public override void ExitPython_module (EParser.Python_moduleContext ctx)
		{
			List<String> ids = new List<String> ();
			foreach (EParser.IdentifierContext ic in ctx.identifier())
				ids.Add (ic.GetText ());
			PythonModule module = new PythonModule (ids);
			SetNodeValue (ctx, module);
		}

		
		public override void ExitPython_native_statement (EParser.Python_native_statementContext ctx)
		{
			PythonStatement stmt = this.GetNodeValue<PythonStatement> (ctx.stmt);
			PythonModule module = this.GetNodeValue<PythonModule> (ctx.module);
			stmt.setModule (module);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitPython2CategoryBinding (EParser.Python2CategoryBindingContext ctx)
		{
			PythonNativeCategoryBinding map = this.GetNodeValue<PythonNativeCategoryBinding> (ctx.binding);
			SetNodeValue (ctx, new Python2NativeCategoryBinding (map));
		}

		
		public override void ExitPython3CategoryBinding (EParser.Python3CategoryBindingContext ctx)
		{
			PythonNativeCategoryBinding map = this.GetNodeValue<PythonNativeCategoryBinding> (ctx.binding);
			SetNodeValue (ctx, new Python3NativeCategoryBinding (map));
		}

		
		public override void ExitPython_category_binding (EParser.Python_category_bindingContext ctx)
		{
			String identifier = ctx.identifier ().GetText ();
			PythonModule module = this.GetNodeValue<PythonModule> (ctx.python_module ());
			PythonNativeCategoryBinding map = new PythonNativeCategoryBinding (identifier, module);
			SetNodeValue (ctx, map);
		}

		 
		public override void ExitPythonGlobalMethodExpression (EParser.PythonGlobalMethodExpressionContext ctx)
		{
			PythonMethodExpression exp = this.GetNodeValue<PythonMethodExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitPython_method_expression (EParser.Python_method_expressionContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			PythonArgumentList args = this.GetNodeValue<PythonArgumentList> (ctx.args);
			PythonMethodExpression method = new PythonMethodExpression (name);
			method.setArguments (args);
			SetNodeValue (ctx, method);
		}

		
		public override void ExitPythonIdentifierExpression (EParser.PythonIdentifierExpressionContext ctx)
		{
			PythonIdentifierExpression exp = this.GetNodeValue<PythonIdentifierExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitPythonNamedArgumentList (EParser.PythonNamedArgumentListContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			PythonExpression exp = this.GetNodeValue<PythonExpression> (ctx.exp);
			PythonNamedArgument arg = new PythonNamedArgument (name, exp);
			SetNodeValue (ctx, new PythonArgumentList (arg));
		}

		
		public override void ExitPythonNamedArgumentListItem (EParser.PythonNamedArgumentListItemContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			PythonExpression exp = this.GetNodeValue<PythonExpression> (ctx.exp);
			PythonNamedArgument arg = new PythonNamedArgument (name, exp);
			PythonArgumentList items = this.GetNodeValue<PythonArgumentList> (ctx.items);
			items.Add (arg);
			SetNodeValue (ctx, items);
		}

		public override void ExitPythonOrdinalArgumentList (EParser.PythonOrdinalArgumentListContext ctx)
		{
			PythonExpression exp = this.GetNodeValue<PythonExpression> (ctx.item);
			PythonOrdinalArgument arg = new PythonOrdinalArgument (exp);
			SetNodeValue (ctx, new PythonArgumentList (arg));
		}


		public override void ExitPythonOrdinalArgumentListItem (EParser.PythonOrdinalArgumentListItemContext ctx)
		{
			PythonExpression exp = this.GetNodeValue<PythonExpression> (ctx.item);
			PythonOrdinalArgument arg = new PythonOrdinalArgument (exp);
			PythonArgumentList items = this.GetNodeValue<PythonArgumentList> (ctx.items);
			items.Add (arg);
			SetNodeValue (ctx, items);
		}

		public override void ExitPythonOrdinalOnlyArgumentList (EParser.PythonOrdinalOnlyArgumentListContext ctx)
		{
			PythonArgumentList ordinal = this.GetNodeValue<PythonArgumentList> (ctx.ordinal);
			SetNodeValue (ctx, ordinal);
		}


		public override void ExitPythonSelectorExpression (EParser.PythonSelectorExpressionContext ctx)
		{
			PythonExpression parent = this.GetNodeValue<PythonExpression> (ctx.parent);
			PythonSelectorExpression selector = this.GetNodeValue<PythonSelectorExpression> (ctx.child);
			selector.setParent (parent);
			SetNodeValue (ctx, selector);
		}

		
		public override void ExitPythonArgumentList (EParser.PythonArgumentListContext ctx)
		{
			PythonArgumentList ordinal = this.GetNodeValue<PythonArgumentList> (ctx.ordinal);
			PythonArgumentList named = this.GetNodeValue<PythonArgumentList> (ctx.named);
			ordinal.AddRange (named);
			SetNodeValue (ctx, ordinal);
		}

		
		public override void ExitPythonMethodExpression (EParser.PythonMethodExpressionContext ctx)
		{
			PythonMethodExpression exp = this.GetNodeValue<PythonMethodExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitPythonNamedOnlyArgumentList (EParser.PythonNamedOnlyArgumentListContext ctx)
		{
			PythonArgumentList named = this.GetNodeValue<PythonArgumentList> (ctx.named);
			SetNodeValue (ctx, named);
		}

		
		public override void ExitPythonPrestoIdentifier (EParser.PythonPrestoIdentifierContext ctx)
		{
			String name = ctx.DOLLAR_IDENTIFIER ().GetText ();
			SetNodeValue (ctx, new PythonIdentifierExpression(name));
		}

		public override void ExitPythonPrimaryExpression (EParser.PythonPrimaryExpressionContext ctx)
		{
			PythonExpression exp = this.GetNodeValue<PythonExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitNativeCategoryBindingList (EParser.NativeCategoryBindingListContext ctx)
		{
			NativeCategoryBinding item = this.GetNodeValue<NativeCategoryBinding> (ctx.item);
			NativeCategoryBindingList items = new NativeCategoryBindingList (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitNativeCategoryBindingListItem (EParser.NativeCategoryBindingListItemContext ctx)
		{
			NativeCategoryBinding item = this.GetNodeValue<NativeCategoryBinding> (ctx.item);
			NativeCategoryBindingList items = this.GetNodeValue<NativeCategoryBindingList> (ctx.items);
			items.Add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitNative_category_bindings (EParser.Native_category_bindingsContext ctx)
		{
			NativeCategoryBindingList items = this.GetNodeValue<NativeCategoryBindingList> (ctx.items);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitNative_category_declaration (EParser.Native_category_declarationContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			IdentifierList attrs = this.GetNodeValue<IdentifierList> (ctx.attrs);
			NativeCategoryBindingList bindings = this.GetNodeValue<NativeCategoryBindingList> (ctx.bindings);
			SetNodeValue (ctx, new NativeCategoryDeclaration (name, attrs, bindings, null));
		}

		
		public override void ExitNativeCategoryDeclaration (EParser.NativeCategoryDeclarationContext ctx)
		{
			IDeclaration decl = this.GetNodeValue<IDeclaration> (ctx.decl);
			SetNodeValue (ctx, decl);
		}

		
		public override void ExitNative_resource_declaration (EParser.Native_resource_declarationContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			IdentifierList attrs = this.GetNodeValue<IdentifierList> (ctx.attrs);
			NativeCategoryBindingList bindings = this.GetNodeValue<NativeCategoryBindingList> (ctx.bindings);
			SetNodeValue (ctx, new NativeResourceDeclaration (name, attrs, bindings, null));
		}

		
		public override void ExitResource_declaration (EParser.Resource_declarationContext ctx)
		{
			IDeclaration decl = this.GetNodeValue<IDeclaration> (ctx.decl);
			SetNodeValue (ctx, decl);
		}

		
		public override void ExitResourceDeclaration (EParser.ResourceDeclarationContext ctx)
		{
			IDeclaration decl = this.GetNodeValue<IDeclaration> (ctx.decl);
			SetNodeValue (ctx, decl);
		}

		
		public override void ExitCategoryDeclaration (EParser.CategoryDeclarationContext ctx)
		{
			IDeclaration decl = this.GetNodeValue<IDeclaration> (ctx.decl);
			SetNodeValue (ctx, decl);
		}

		
		public override void ExitAttributeDeclaration (EParser.AttributeDeclarationContext ctx)
		{
			IDeclaration decl = this.GetNodeValue<IDeclaration> (ctx.decl);
			SetNodeValue (ctx, decl);
		}

		
		public override void ExitEnumCategoryDeclaration (EParser.EnumCategoryDeclarationContext ctx)
		{
			IDeclaration decl = this.GetNodeValue<IDeclaration> (ctx.decl);
			SetNodeValue (ctx, decl);
		}

		
		public override void ExitEnumNativeDeclaration (EParser.EnumNativeDeclarationContext ctx)
		{
			IDeclaration decl = this.GetNodeValue<IDeclaration> (ctx.decl);
			SetNodeValue (ctx, decl);
		}

		
		public override void ExitEnumDeclaration (EParser.EnumDeclarationContext ctx)
		{
			IDeclaration decl = this.GetNodeValue<IDeclaration> (ctx.decl);
			SetNodeValue (ctx, decl);
		}

		
		public override void ExitParenthesis_expression (EParser.Parenthesis_expressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new ParenthesisExpression (exp));
		}

		
		public override void ExitParenthesisExpression (EParser.ParenthesisExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitNativeSymbolList (EParser.NativeSymbolListContext ctx)
		{
			NativeSymbol item = this.GetNodeValue<NativeSymbol> (ctx.item);
			SetNodeValue (ctx, new NativeSymbolList (item));
		}

		
		public override void ExitNativeSymbolListItem (EParser.NativeSymbolListItemContext ctx)
		{
			NativeSymbol item = this.GetNodeValue<NativeSymbol> (ctx.item);
			NativeSymbolList items = this.GetNodeValue<NativeSymbolList> (ctx.items);
			items.add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitEnum_native_declaration (EParser.Enum_native_declarationContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			NativeType type = this.GetNodeValue<NativeType> (ctx.typ);
			NativeSymbolList symbols = this.GetNodeValue<NativeSymbolList> (ctx.symbols);
			SetNodeValue (ctx, new EnumeratedNativeDeclaration (name, type, symbols));
		}

		
		public override void ExitFor_each_statement (EParser.For_each_statementContext ctx)
		{
			String name1 = this.GetNodeValue<String> (ctx.name1);
			String name2 = this.GetNodeValue<String> (ctx.name2);
			IExpression source = this.GetNodeValue<IExpression> (ctx.source);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new ForEachStatement (name1, name2, source, stmts));
		}

		
		public override void ExitForEachStatement (EParser.ForEachStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitSymbols_token (EParser.Symbols_tokenContext ctx)
		{
			SetNodeValue (ctx, ctx.GetText ());
		}

		
		public override void ExitKey_token (EParser.Key_tokenContext ctx)
		{
			SetNodeValue (ctx, ctx.GetText ());
		}

		
		public override void ExitValue_token (EParser.Value_tokenContext ctx)
		{
			SetNodeValue (ctx, ctx.GetText ());
		}

		
		public override void ExitNamed_argument (EParser.Named_argumentContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			UnresolvedArgument arg = new UnresolvedArgument (name);
			IExpression exp = this.GetNodeValue<IExpression> (ctx.value);
			arg.DefaultValue = exp;
			SetNodeValue (ctx, arg);
		}

		
		public override void ExitClosureStatement (EParser.ClosureStatementContext ctx)
		{
			ConcreteMethodDeclaration decl = this.GetNodeValue<ConcreteMethodDeclaration> (ctx.decl);
			SetNodeValue (ctx, new DeclarationInstruction<ConcreteMethodDeclaration> (decl));
		}

		
		public override void ExitReturn_statement (EParser.Return_statementContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new ReturnStatement (exp));
		}

		
		public override void ExitReturnStatement (EParser.ReturnStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitClosureExpression (EParser.ClosureExpressionContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new MethodExpression (name));
		}

		
		public override void ExitIf_statement (EParser.If_statementContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			IfElementList elseIfs = this.GetNodeValue<IfElementList> (ctx.elseIfs);
			StatementList elseStmts = this.GetNodeValue<StatementList> (ctx.elseStmts);
			SetNodeValue (ctx, new IfStatement (exp, stmts, elseIfs, elseStmts));
		}

		
		public override void ExitElseIfStatementList (EParser.ElseIfStatementListContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			IfElement elem = new IfElement (exp, stmts);
			SetNodeValue (ctx, new IfElementList (elem));
		}

		
		public override void ExitElseIfStatementListItem (EParser.ElseIfStatementListItemContext ctx)
		{
			IfElementList items = this.GetNodeValue<IfElementList> (ctx.items);
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			IfElement elem = new IfElement (exp, stmts);
			items.Add (elem);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitIfStatement (EParser.IfStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitSwitchStatement (EParser.SwitchStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitAssignTupleStatement (EParser.AssignTupleStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitRaiseStatement (EParser.RaiseStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitWriteStatement (EParser.WriteStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitWithResourceStatement (EParser.WithResourceStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitWhileStatement (EParser.WhileStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitDoWhileStatement (EParser.DoWhileStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitTryStatement (EParser.TryStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitEqualsExpression (EParser.EqualsExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new EqualsExpression (left, EqOp.EQUALS, right));
		}

		
		public override void ExitNotEqualsExpression (EParser.NotEqualsExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new EqualsExpression (left, EqOp.NOT_EQUALS, right));
		}

		
		public override void ExitGreaterThanExpression (EParser.GreaterThanExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new CompareExpression (left, CmpOp.GT, right));
		}

		
		public override void ExitGreaterThanOrEqualExpression (EParser.GreaterThanOrEqualExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new CompareExpression (left, CmpOp.GTE, right));
		}

		
		public override void ExitLessThanExpression (EParser.LessThanExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new CompareExpression (left, CmpOp.LT, right));
		}

		
		public override void ExitLessThanOrEqualExpression (EParser.LessThanOrEqualExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new CompareExpression (left, CmpOp.LTE, right));
		}

		
		public override void ExitAtomicSwitchCase (EParser.AtomicSwitchCaseContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new AtomicSwitchCase (exp, stmts));
		}

		
		public override void ExitCollectionSwitchCase (EParser.CollectionSwitchCaseContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new CollectionSwitchCase (exp, stmts));
		}

		
		public override void ExitSwitchCaseStatementList (EParser.SwitchCaseStatementListContext ctx)
		{
			SwitchCase item = this.GetNodeValue<SwitchCase> (ctx.item);
			SetNodeValue (ctx, new SwitchCaseList (item));
		}

		
		public override void ExitSwitchCaseStatementListItem (EParser.SwitchCaseStatementListItemContext ctx)
		{
			SwitchCase item = this.GetNodeValue<SwitchCase> (ctx.item);
			SwitchCaseList items = this.GetNodeValue<SwitchCaseList> (ctx.items);
			items.Add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitSwitch_statement (EParser.Switch_statementContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SwitchCaseList cases = this.GetNodeValue<SwitchCaseList> (ctx.cases);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SwitchStatement stmt = new SwitchStatement (exp, cases, stmts);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitLiteralRangeLiteral (EParser.LiteralRangeLiteralContext ctx)
		{
			IExpression low = this.GetNodeValue<IExpression> (ctx.low);
			IExpression high = this.GetNodeValue<IExpression> (ctx.high);
			SetNodeValue (ctx, new RangeLiteral (low, high));
		}

		
		public override void ExitLiteralSetLiteral (EParser.LiteralSetLiteralContext ctx)
		{
			ExpressionList items = this.GetNodeValue<ExpressionList>(ctx.exp);
			SetNodeValue(ctx, new SetLiteral(items));
		}

		public override void ExitLiteralListLiteral (EParser.LiteralListLiteralContext ctx)
		{
			ExpressionList exp = this.GetNodeValue<ExpressionList> (ctx.exp);
			SetNodeValue (ctx, new ListLiteral (exp));
		}

		
		public override void ExitLiteralList (EParser.LiteralListContext ctx)
		{
			IExpression item = this.GetNodeValue<IExpression> (ctx.item);
			SetNodeValue (ctx, new ExpressionList (item));
		}

		
		public override void ExitLiteralListItem (EParser.LiteralListItemContext ctx)
		{
			ExpressionList items = this.GetNodeValue<ExpressionList> (ctx.items);
			IExpression item = this.GetNodeValue<IExpression> (ctx.item);
			items.add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitInExpression (EParser.InExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new ContainsExpression (left, ContOp.IN, right));
		}

		
		public override void ExitNotInExpression (EParser.NotInExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new ContainsExpression (left, ContOp.NOT_IN, right));
		}

		public override void ExitIsATypeExpression (EParser.IsATypeExpressionContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.typ);
			IExpression exp = new TypeExpression (type);
			SetNodeValue (ctx, exp);
		}

		public override void ExitIsOtherExpression (EParser.IsOtherExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		public override void ExitIsExpression (EParser.IsExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			EqOp op = right is TypeExpression ? EqOp.IS_A : EqOp.IS;
			SetNodeValue (ctx, new EqualsExpression (left, op, right));
		}

		public override void ExitIsNotExpression (EParser.IsNotExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			EqOp op = right is TypeExpression ? EqOp.IS_NOT_A : EqOp.IS_NOT;
			SetNodeValue (ctx, new EqualsExpression (left, op, right));
		}

		
		public override void ExitContainsAllExpression (EParser.ContainsAllExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new ContainsExpression (left, ContOp.CONTAINS_ALL, right));
		}

		
		public override void ExitNotContainsAllExpression (EParser.NotContainsAllExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new ContainsExpression (left, ContOp.NOT_CONTAINS_ALL, right));
		}

		
		public override void ExitContainsAnyExpression (EParser.ContainsAnyExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new ContainsExpression (left, ContOp.CONTAINS_ANY, right));
		}

		
		public override void ExitNotContainsAnyExpression (EParser.NotContainsAnyExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new ContainsExpression (left, ContOp.NOT_CONTAINS_ANY, right));
		}

		
		public override void ExitContainsExpression (EParser.ContainsExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new ContainsExpression (left, ContOp.CONTAINS, right));
		}

		
		public override void ExitNotContainsExpression (EParser.NotContainsExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new ContainsExpression (left, ContOp.NOT_CONTAINS, right));
		}

		
		public override void ExitDivideExpression (EParser.DivideExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new DivideExpression (left, right));
		}

		
		public override void ExitIntDivideExpression (EParser.IntDivideExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new IntDivideExpression (left, right));
		}

		
		public override void ExitModuloExpression (EParser.ModuloExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new ModuloExpression (left, right));
		}

		
		public override void ExitAndExpression (EParser.AndExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new AndExpression (left, right));
		}

		public override void ExitNullLiteral (EParser.NullLiteralContext ctx)
		{
			SetNodeValue (ctx, NullLiteral.Instance);
		}

		public override void ExitOperatorArgument(EParser.OperatorArgumentContext ctx) {
			bool mutable = ctx.MUTABLE () != null;
			IArgument arg = this.GetNodeValue<IArgument>(ctx.arg);
			arg.setMutable (mutable);
			SetNodeValue(ctx, arg);
		}

		public override void ExitOperatorPlus(EParser.OperatorPlusContext ctx) {
			SetNodeValue(ctx, Operator.PLUS);
		}

		public override void ExitOperatorMinus(EParser.OperatorMinusContext ctx) {
			SetNodeValue(ctx, Operator.MINUS);
		}

		public override void ExitOperatorMultiply(EParser.OperatorMultiplyContext ctx) {
			SetNodeValue(ctx, Operator.MULTIPLY);
		}

		public override void ExitOperatorDivide(EParser.OperatorDivideContext ctx) {
			SetNodeValue(ctx, Operator.DIVIDE);
		}

		public override void ExitOperatorIDivide(EParser.OperatorIDivideContext ctx) {
			SetNodeValue(ctx, Operator.IDIVIDE);
		}

		public override void ExitOperatorModulo(EParser.OperatorModuloContext ctx) {
			SetNodeValue(ctx, Operator.MODULO);
		}

		public override void ExitOperator_method_declaration(EParser.Operator_method_declarationContext ctx) {
			Operator op = this.GetNodeValue<Operator>(ctx.op);
			IArgument arg = this.GetNodeValue<IArgument>(ctx.arg);
			IType typ = this.GetNodeValue<IType>(ctx.typ);
			StatementList stmts = this.GetNodeValue<StatementList>(ctx.stmts);
			OperatorMethodDeclaration decl = new OperatorMethodDeclaration(op, arg, typ, stmts);
			SetNodeValue(ctx, decl);
		}

		public override void ExitOperatorMemberMethod(EParser.OperatorMemberMethodContext ctx) {
			OperatorMethodDeclaration decl = this.GetNodeValue<OperatorMethodDeclaration>(ctx.decl);
			SetNodeValue(ctx, decl);
		}
		
		public override void ExitOrExpression (EParser.OrExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new OrExpression (left, right));
		}

		
		public override void ExitMultiplyExpression (EParser.MultiplyExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new MultiplyExpression (left, right));
		}

		
		public override void ExitMinusExpression (EParser.MinusExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new MinusExpression (exp));
		}

		
		public override void ExitNotExpression (EParser.NotExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new NotExpression (exp));
		}

		
		public override void ExitWhile_statement (EParser.While_statementContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new WhileStatement (exp, stmts));
		}

		
		public override void ExitDo_while_statement (EParser.Do_while_statementContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new DoWhileStatement (exp, stmts));
		}

		public override void ExitSingleton_category_declaration(EParser.Singleton_category_declarationContext ctx) {
			String name = this.GetNodeValue<String>(ctx.name);
			IdentifierList attrs = this.GetNodeValue<IdentifierList>(ctx.attrs);
			CategoryMethodDeclarationList methods = this.GetNodeValue<CategoryMethodDeclarationList>(ctx.methods);
			SetNodeValue(ctx, new SingletonCategoryDeclaration(name, attrs, methods));
		}

		public override void ExitSingletonCategoryDeclaration(EParser.SingletonCategoryDeclarationContext ctx) {
			IDeclaration decl = this.GetNodeValue<IDeclaration>(ctx.decl);
			SetNodeValue(ctx, decl);
		}

		public override void ExitSliceFirstAndLast (EParser.SliceFirstAndLastContext ctx)
		{
			IExpression first = this.GetNodeValue<IExpression> (ctx.first);
			IExpression last = this.GetNodeValue<IExpression> (ctx.last);
			SetNodeValue (ctx, new SliceSelector (first, last));
		}

		
		public override void ExitSliceFirstOnly (EParser.SliceFirstOnlyContext ctx)
		{
			IExpression first = this.GetNodeValue<IExpression> (ctx.first);
			SetNodeValue (ctx, new SliceSelector (first, null));
		}

		
		public override void ExitSliceLastOnly (EParser.SliceLastOnlyContext ctx)
		{
			IExpression last = this.GetNodeValue<IExpression> (ctx.last);
			SetNodeValue (ctx, new SliceSelector (null, last));
		}

		
		public override void ExitSorted_expression (EParser.Sorted_expressionContext ctx)
		{
			IExpression source = this.GetNodeValue<IExpression> (ctx.source);
			IExpression key = this.GetNodeValue<IExpression> (ctx.key);
			SetNodeValue (ctx, new SortedExpression (source, key));
		}

		
		public override void ExitSortedExpression (EParser.SortedExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitDocumentExpression (EParser.DocumentExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitDocument_expression (EParser.Document_expressionContext ctx)
		{
			SetNodeValue (ctx, new DocumentExpression ());
		}

		
		public override void ExitDocument_type (EParser.Document_typeContext ctx)
		{
			SetNodeValue (ctx, DocumentType.Instance);
		}

		
		public override void ExitFetchExpression (EParser.FetchExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitFetch_expression (EParser.Fetch_expressionContext ctx)
		{
			String itemName = this.GetNodeValue<String> (ctx.name);
			IExpression source = this.GetNodeValue<IExpression> (ctx.source);
			IExpression filter = this.GetNodeValue<IExpression> (ctx.xfilter);
			SetNodeValue (ctx, new FetchExpression (itemName, source, filter));
		}

		
		public override void ExitCode_type (EParser.Code_typeContext ctx)
		{
			SetNodeValue (ctx, CodeType.Instance);
		}

		
		public override void ExitExecuteExpression (EParser.ExecuteExpressionContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new ExecuteExpression (name));
		}

		
		public override void ExitCodeExpression (EParser.CodeExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new CodeExpression (exp));
		}

		
		public override void ExitCode_argument (EParser.Code_argumentContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new CodeArgument (name));
		}

		
		public override void ExitCategory_symbol (EParser.Category_symbolContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			ArgumentAssignmentList args = this.GetNodeValue<ArgumentAssignmentList> (ctx.args);
			ArgumentAssignment arg = this.GetNodeValue<ArgumentAssignment> (ctx.arg);
			if (arg != null)
				args.add (arg);
			SetNodeValue (ctx, new CategorySymbol (name, args));
		}

		
		public override void ExitCategorySymbolList (EParser.CategorySymbolListContext ctx)
		{
			CategorySymbol item = this.GetNodeValue<CategorySymbol> (ctx.item);
			SetNodeValue (ctx, new CategorySymbolList (item));
		}

		
		public override void ExitCategorySymbolListItem (EParser.CategorySymbolListItemContext ctx)
		{
			CategorySymbol item = this.GetNodeValue<CategorySymbol> (ctx.item);
			CategorySymbolList items = this.GetNodeValue<CategorySymbolList> (ctx.items);
			items.add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitEnum_category_declaration (EParser.Enum_category_declarationContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			IdentifierList attrs = this.GetNodeValue<IdentifierList> (ctx.attrs);
			String parent = this.GetNodeValue<String> (ctx.derived);
			IdentifierList derived = parent == null ? null : new IdentifierList (parent);
			CategorySymbolList symbols = this.GetNodeValue<CategorySymbolList> (ctx.symbols);
			SetNodeValue (ctx, new EnumeratedCategoryDeclaration (name, attrs, derived, symbols));
		}

		
		public override void ExitRead_expression (EParser.Read_expressionContext ctx)
		{
			IExpression source = this.GetNodeValue<IExpression> (ctx.source);
			SetNodeValue (ctx, new ReadExpression (source));
		}

		
		public override void ExitReadExpression (EParser.ReadExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitWrite_statement (EParser.Write_statementContext ctx)
		{
			IExpression what = this.GetNodeValue<IExpression> (ctx.what);
			IExpression target = this.GetNodeValue<IExpression> (ctx.target);
			SetNodeValue (ctx, new WriteStatement (what, target));
		}

		
		public override void ExitWith_resource_statement (EParser.With_resource_statementContext ctx)
		{
			AssignVariableStatement stmt = this.GetNodeValue<AssignVariableStatement> (ctx.stmt);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new WithResourceStatement (stmt, stmts));
		}

		public override void ExitWith_singleton_statement(EParser.With_singleton_statementContext ctx) {
			String name = this.GetNodeValue<String>(ctx.typ);
			CategoryType type = new CategoryType(name);
			StatementList stmts = this.GetNodeValue<StatementList>(ctx.stmts);
			SetNodeValue(ctx, new WithSingletonStatement(type, stmts));
		}

		public override void ExitWithSingletonStatement(EParser.WithSingletonStatementContext ctx) {
			IStatement stmt = this.GetNodeValue<IStatement>(ctx.stmt);
			SetNodeValue(ctx, stmt);
		}

		
		public override void ExitAnyType (EParser.AnyTypeContext ctx)
		{
			SetNodeValue (ctx, AnyType.Instance);
		}

		
		public override void ExitAnyListType (EParser.AnyListTypeContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.typ);
			SetNodeValue (ctx, new ListType (type));
		}

		
		public override void ExitAnyDictType (EParser.AnyDictTypeContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.typ);
			SetNodeValue (ctx, new DictType (type));
		}

		
		public override void ExitAnyArgumentType (EParser.AnyArgumentTypeContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.typ);
			SetNodeValue (ctx, type);
		}

		public override void ExitCastExpression (EParser.CastExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IType type = this.GetNodeValue<IType> (ctx.right);
			SetNodeValue (ctx, new CastExpression (left, type));
		}

		public override void ExitCatchAtomicStatement (EParser.CatchAtomicStatementContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new AtomicSwitchCase (new SymbolExpression (name), stmts));
		}

		
		public override void ExitCatchCollectionStatement (EParser.CatchCollectionStatementContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new CollectionSwitchCase (exp, stmts));
		}

		
		public override void ExitCatchStatementList (EParser.CatchStatementListContext ctx)
		{
			SwitchCase item = this.GetNodeValue<SwitchCase> (ctx.item);
			SetNodeValue (ctx, new SwitchCaseList (item));
		}

		
		public override void ExitCatchStatementListItem (EParser.CatchStatementListItemContext ctx)
		{
			SwitchCase item = this.GetNodeValue<SwitchCase> (ctx.item);
			SwitchCaseList items = this.GetNodeValue<SwitchCaseList> (ctx.items);
			items.Add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitTry_statement (EParser.Try_statementContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SwitchCaseList handlers = this.GetNodeValue<SwitchCaseList> (ctx.handlers);
			StatementList anyStmts = this.GetNodeValue<StatementList> (ctx.anyStmts);
			StatementList finalStmts = this.GetNodeValue<StatementList> (ctx.finalStmts);
			SwitchErrorStatement stmt = new SwitchErrorStatement (name, stmts, handlers, anyStmts, finalStmts);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitRaise_statement (EParser.Raise_statementContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new RaiseStatement (exp));
		}

		
		public override void ExitMatchingList (EParser.MatchingListContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.source);
			SetNodeValue (ctx, new MatchingCollectionConstraint (exp));
		}

		
		public override void ExitMatchingRange (EParser.MatchingRangeContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.source);
			SetNodeValue (ctx, new MatchingCollectionConstraint (exp));
		}

		
		public override void ExitMatchingExpression (EParser.MatchingExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new MatchingExpressionConstraint (exp));
		}

		
		public override void ExitMatchingPattern (EParser.MatchingPatternContext ctx)
		{
			SetNodeValue (ctx, new MatchingPatternConstraint (new TextLiteral (ctx.text.Text)));
		}

		public override void ExitMatchingSet (EParser.MatchingSetContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression>(ctx.source);
			SetNodeValue(ctx, new MatchingCollectionConstraint(exp));
		}
			
		public override void ExitInvocation_expression (EParser.Invocation_expressionContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			MethodSelector select = new MethodSelector (name);
			SetNodeValue (ctx, new MethodCall (select));
		}

		
		public override void ExitInvocationExpression (EParser.InvocationExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitInvokeStatement (EParser.InvokeStatementContext ctx)
		{
			IStatement exp = this.GetNodeValue<IStatement> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		public override void ExitJavascriptBooleanLiteral (EParser.JavascriptBooleanLiteralContext ctx)
		{
			String text = ctx.t.Text;
			SetNodeValue (ctx, new JavaScriptBooleanLiteral (text));		
		}

		
		public override void ExitJavascript_category_binding (EParser.Javascript_category_bindingContext ctx)
		{
			String identifier = ctx.identifier ().GetText ();
			JavaScriptModule module = this.GetNodeValue<JavaScriptModule> (ctx.javascript_module ());
			JavaScriptNativeCategoryBinding map = new JavaScriptNativeCategoryBinding (identifier, module);
			SetNodeValue (ctx, map);
		}

		
		public override void ExitJavascript_identifier (EParser.Javascript_identifierContext ctx)
		{
			String name = ctx.GetText ();
			SetNodeValue (ctx, name);
		}

		
		public override void ExitJavascript_method_expression (EParser.Javascript_method_expressionContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			JavaScriptMethodExpression method = new JavaScriptMethodExpression (name);
			JavaScriptExpressionList args = this.GetNodeValue<JavaScriptExpressionList> (ctx.args);
			method.setArguments (args);
			SetNodeValue (ctx, method);
		}

		
		public override void ExitJavascript_module (EParser.Javascript_moduleContext ctx)
		{
			List<String> ids = new List<String> ();
			foreach (EParser.Javascript_identifierContext ic in ctx.javascript_identifier())
				ids.Add (ic.GetText ());
			JavaScriptModule module = new JavaScriptModule (ids);
			SetNodeValue (ctx, module);
		}

		
		public override void ExitJavascript_native_statement (EParser.Javascript_native_statementContext ctx)
		{
			JavaScriptStatement stmt = this.GetNodeValue<JavaScriptStatement> (ctx.stmt);
			JavaScriptModule module = this.GetNodeValue<JavaScriptModule> (ctx.module);
			stmt.setModule (module);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitJavascriptArgumentList (EParser.JavascriptArgumentListContext ctx)
		{
			JavaScriptExpression exp = this.GetNodeValue<JavaScriptExpression> (ctx.item);
			JavaScriptExpressionList list = new JavaScriptExpressionList (exp);
			SetNodeValue (ctx, list);
		}

		
		public override void ExitJavascriptArgumentListItem (EParser.JavascriptArgumentListItemContext ctx)
		{
			JavaScriptExpression exp = this.GetNodeValue<JavaScriptExpression> (ctx.item);
			JavaScriptExpressionList list = this.GetNodeValue<JavaScriptExpressionList> (ctx.items);
			list.Add (exp);
			SetNodeValue (ctx, list);
		}

		
		public override void ExitJavaScriptCategoryBinding (EParser.JavaScriptCategoryBindingContext ctx)
		{
			SetNodeValue (ctx, this.GetNodeValue<Object> (ctx.binding));
		}

		public override void ExitJavascriptCharacterLiteral (EParser.JavascriptCharacterLiteralContext ctx)
		{
			String text = ctx.t.Text;
			SetNodeValue (ctx, new JavaScriptCharacterLiteral (text));		
		}

		
		public override void ExitJavascriptChildIdentifier (EParser.JavascriptChildIdentifierContext ctx)
		{
			JavaScriptIdentifierExpression parent = this.GetNodeValue<JavaScriptIdentifierExpression> (ctx.parent);
			String name = this.GetNodeValue<String> (ctx.name);
			JavaScriptIdentifierExpression exp = new JavaScriptIdentifierExpression (parent, name);
			SetNodeValue (ctx, exp);
		}

		public override void ExitJavascriptDecimalLiteral (EParser.JavascriptDecimalLiteralContext ctx)
		{
				String text = ctx.t.Text;
				SetNodeValue (ctx, new JavaScriptDecimalLiteral (text));		
		}

		public override void ExitJavascriptIdentifier (EParser.JavascriptIdentifierContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new JavaScriptIdentifierExpression (name));
		}

		
		public override void ExitJavascriptIdentifierExpression (EParser.JavascriptIdentifierExpressionContext ctx)
		{
			JavaScriptIdentifierExpression exp = this.GetNodeValue<JavaScriptIdentifierExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		public override void ExitJavascriptIntegerLiteral (EParser.JavascriptIntegerLiteralContext ctx)
		{
			String text = ctx.t.Text;
			SetNodeValue (ctx, new JavaScriptIntegerLiteral (text));		
		}

		public override void ExitJavascriptLiteralExpression (EParser.JavascriptLiteralExpressionContext ctx)
		{
			JavaScriptExpression exp = this.GetNodeValue<JavaScriptExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitJavascriptMethodExpression (EParser.JavascriptMethodExpressionContext ctx)
		{
			JavaScriptMethodExpression method = this.GetNodeValue<JavaScriptMethodExpression> (ctx.exp);
			SetNodeValue (ctx, method);
		}

		
		public override void ExitJavaScriptNativeStatement (EParser.JavaScriptNativeStatementContext ctx)
		{
			JavaScriptStatement stmt = this.GetNodeValue<JavaScriptStatement> (ctx.stmt);
			SetNodeValue (ctx, new JavaScriptNativeCall (stmt));
		}

		
		public override void ExitJavascriptPrimaryExpression (EParser.JavascriptPrimaryExpressionContext ctx)
		{
			JavaScriptExpression exp = this.GetNodeValue<JavaScriptExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitJavascriptReturnStatement (EParser.JavascriptReturnStatementContext ctx)
		{
			JavaScriptExpression exp = this.GetNodeValue<JavaScriptExpression> (ctx.exp);
			SetNodeValue (ctx, new JavaScriptStatement (exp, true));
		}

		
		public override void ExitJavascriptSelectorExpression (EParser.JavascriptSelectorExpressionContext ctx)
		{
			JavaScriptExpression parent = this.GetNodeValue<JavaScriptExpression> (ctx.parent);
			JavaScriptSelectorExpression child = this.GetNodeValue<JavaScriptSelectorExpression> (ctx.child);
			child.setParent (parent);
			SetNodeValue (ctx, child);
		}

		public override void ExitJavascriptTextLiteral (EParser.JavascriptTextLiteralContext ctx)
		{
			String text = ctx.t.Text;
			SetNodeValue (ctx, new JavaScriptTextLiteral (text));		
		}

		public override void ExitJavascriptStatement (EParser.JavascriptStatementContext ctx)
		{
			JavaScriptExpression exp = this.GetNodeValue<JavaScriptExpression> (ctx.exp);
			SetNodeValue (ctx, new JavaScriptStatement (exp, false));
		}

	
	}
}
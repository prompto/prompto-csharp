using Antlr4.Runtime.Tree;
using Antlr4.Runtime;
using prompto.expression;
using System;
using prompto.literal;
using prompto.grammar;
using prompto.type;
using prompto.declaration;
using prompto.statement;
using prompto.java;
using prompto.csharp;
using prompto.python;
using prompto.javascript;
using System.Collections.Generic;
using prompto.utils;

namespace prompto.parser
{

	public class SPrestoBuilder : SParserBaseListener
	{

		ParseTreeProperty<object> nodeValues = new ParseTreeProperty<object> ();
		ITokenStream input;
		string path = "";

		public SPrestoBuilder (SCleverParser parser)
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
			section.SetFrom (path, first, last, Dialect.S);
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
			if (!string.IsNullOrEmpty (text) && !Char.IsWhiteSpace (text [0]))
				return token;
			else
				return null;
		}

		
		public override void ExitFullDeclarationList (SParser.FullDeclarationListContext ctx)
		{
			DeclarationList items = this.GetNodeValue<DeclarationList> (ctx.items);
			if (items == null)
				items = new DeclarationList ();
			SetNodeValue (ctx, items);
		}

		
		public override void ExitSelectorExpression (SParser.SelectorExpressionContext ctx)
		{
			IExpression parent = this.GetNodeValue<IExpression> (ctx.parent);
			SelectorExpression selector = this.GetNodeValue<SelectorExpression> (ctx.selector);
			selector.setParent (parent);
			SetNodeValue (ctx, selector);
		}

		
		public override void ExitSelectableExpression (SParser.SelectableExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.parent);
			SetNodeValue (ctx, exp);
		}

		public override void ExitSet_literal (SParser.Set_literalContext ctx)
		{
			ExpressionList items = this.GetNodeValue<ExpressionList>(ctx.items);
			SetLiteral set = items==null ? new SetLiteral() : new SetLiteral(items);
			SetNodeValue(ctx, set);
		}

		public override void ExitSetLiteral (SParser.SetLiteralContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression>(ctx.exp);
			SetNodeValue(ctx, exp);
		}

		public override void ExitSetType(SParser.SetTypeContext ctx) {
			IType itemType = this.GetNodeValue<IType>(ctx.s);
			SetNodeValue(ctx, new SetType(itemType));
		}

		public override void ExitAtomicLiteral (SParser.AtomicLiteralContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitCollectionLiteral (SParser.CollectionLiteralContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitListLiteral (SParser.ListLiteralContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitBooleanLiteral (SParser.BooleanLiteralContext ctx)
		{
			SetNodeValue (ctx, new BooleanLiteral (ctx.t.Text));
		}

		
		public override void ExitMinIntegerLiteral (SParser.MinIntegerLiteralContext ctx)
		{
			SetNodeValue (ctx, new MinIntegerLiteral ());
		}

		
		public override void ExitMaxIntegerLiteral (SParser.MaxIntegerLiteralContext ctx)
		{
			SetNodeValue (ctx, new MaxIntegerLiteral ());
		}

		
		public override void ExitIntegerLiteral (SParser.IntegerLiteralContext ctx)
		{
			SetNodeValue (ctx, new IntegerLiteral (ctx.t.Text));
		}

		
		public override void ExitDecimalLiteral (SParser.DecimalLiteralContext ctx)
		{
			SetNodeValue (ctx, new DecimalLiteral (ctx.t.Text));
		}

		
		public override void ExitHexadecimalLiteral (SParser.HexadecimalLiteralContext ctx)
		{
			SetNodeValue (ctx, new HexaLiteral (ctx.t.Text));
		}

		
		public override void ExitCharacterLiteral (SParser.CharacterLiteralContext ctx)
		{
			SetNodeValue (ctx, new CharacterLiteral (ctx.t.Text));
		}

		
		public override void ExitDateLiteral (SParser.DateLiteralContext ctx)
		{
			SetNodeValue (ctx, new DateLiteral (ctx.t.Text));
		}

		
		public override void ExitDateTimeLiteral (SParser.DateTimeLiteralContext ctx)
		{
			SetNodeValue (ctx, new DateTimeLiteral (ctx.t.Text));
		}

		public override void ExitTernaryExpression (SParser.TernaryExpressionContext ctx)
		{
			IExpression condition = this.GetNodeValue<IExpression> (ctx.test);
			IExpression ifTrue = this.GetNodeValue<IExpression> (ctx.ifTrue);
			IExpression ifFalse = this.GetNodeValue<IExpression> (ctx.ifFalse);
			TernaryExpression exp = new TernaryExpression (condition, ifTrue, ifFalse);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitTest_method_declaration(SParser.Test_method_declarationContext ctx) {
			String name = ctx.name.Text;
			StatementList stmts = this.GetNodeValue<StatementList>(ctx.stmts);
			ExpressionList exps = this.GetNodeValue<ExpressionList>(ctx.exps);
			String errorName = this.GetNodeValue<String>(ctx.error);
			SymbolExpression error = errorName==null ? null : new SymbolExpression(errorName);
			SetNodeValue(ctx, new TestMethodDeclaration(name, stmts, exps, error));
		}

		public override void ExitTestMethod(SParser.TestMethodContext ctx) {
			IDeclaration decl = this.GetNodeValue<IDeclaration>(ctx.decl);
			SetNodeValue(ctx, decl);
		}

		public override void ExitTextLiteral (SParser.TextLiteralContext ctx)
		{
			SetNodeValue (ctx, new TextLiteral (ctx.t.Text));
		}

		
		public override void ExitTimeLiteral (SParser.TimeLiteralContext ctx)
		{
			SetNodeValue (ctx, new TimeLiteral (ctx.t.Text));
		}

		
		public override void ExitPeriodLiteral (SParser.PeriodLiteralContext ctx)
		{
			SetNodeValue (ctx, new PeriodLiteral (ctx.t.Text));
		}

		
		public override void ExitVariable_identifier (SParser.Variable_identifierContext ctx)
		{
			SetNodeValue (ctx, ctx.GetText ());
		}

		
		public override void ExitList_literal (SParser.List_literalContext ctx)
		{
			ExpressionList items = this.GetNodeValue<ExpressionList> (ctx.items);
			IExpression value = items == null ? new ListLiteral () : new ListLiteral (items);
			SetNodeValue (ctx, value);
		}

		
		public override void ExitDict_literal (SParser.Dict_literalContext ctx)
		{
			DictEntryList items = this.GetNodeValue<DictEntryList> (ctx.items);
			IExpression value = items == null ? new DictLiteral () : new DictLiteral (items);
			SetNodeValue (ctx, value);
		}

		
		public override void ExitTuple_literal (SParser.Tuple_literalContext ctx)
		{
			ExpressionList items = this.GetNodeValue<ExpressionList> (ctx.items);
			IExpression value = items == null ? new TupleLiteral () : new TupleLiteral (items);
			SetNodeValue (ctx, value);
		}

		
		public override void ExitTupleLiteral (SParser.TupleLiteralContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}


		
		public override void ExitRange_literal (SParser.Range_literalContext ctx)
		{
			IExpression low = this.GetNodeValue<IExpression> (ctx.low);
			IExpression high = this.GetNodeValue<IExpression> (ctx.high);
			SetNodeValue (ctx, new RangeLiteral (low, high));
		}

		
		public override void ExitRangeLiteral (SParser.RangeLiteralContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitDictLiteral (SParser.DictLiteralContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitDictEntryList (SParser.DictEntryListContext ctx)
		{
			DictEntry item = this.GetNodeValue<DictEntry> (ctx.item);
			SetNodeValue (ctx, new DictEntryList (item));
		}

		
		public override void ExitDictEntryListItem (SParser.DictEntryListItemContext ctx)
		{
			DictEntryList items = this.GetNodeValue<DictEntryList> (ctx.items);
			DictEntry item = this.GetNodeValue<DictEntry> (ctx.item);
			items.add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitDict_entry (SParser.Dict_entryContext ctx)
		{
			IExpression key = this.GetNodeValue<IExpression> (ctx.key);
			IExpression value = this.GetNodeValue<IExpression> (ctx.value);
			DictEntry entry = new DictEntry (key, value);
			SetNodeValue (ctx, entry);
		}

		
		public override void ExitLiteralExpression (SParser.LiteralExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitIdentifierExpression (SParser.IdentifierExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitVariableIdentifier (SParser.VariableIdentifierContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new InstanceExpression (name));
		}

		
		public override void ExitInstanceExpression (SParser.InstanceExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitValueList (SParser.ValueListContext ctx)
		{
			IExpression item = this.GetNodeValue<IExpression> (ctx.item);
			SetNodeValue (ctx, new ExpressionList (item));
		}

		
		public override void ExitValueListItem (SParser.ValueListItemContext ctx)
		{
			ExpressionList items = this.GetNodeValue<ExpressionList> (ctx.items);
			IExpression item = this.GetNodeValue<IExpression> (ctx.item);
			items.add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitValueTuple (SParser.ValueTupleContext ctx)
		{
			IExpression item = this.GetNodeValue<IExpression> (ctx.item);
			SetNodeValue (ctx, new ExpressionList (item));
		}

		
		public override void ExitValueTupleItem (SParser.ValueTupleItemContext ctx)
		{
			ExpressionList items = this.GetNodeValue<ExpressionList> (ctx.items);
			IExpression item = this.GetNodeValue<IExpression> (ctx.item);
			items.add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitSymbol_identifier (SParser.Symbol_identifierContext ctx)
		{
			SetNodeValue (ctx, ctx.GetText ());
		}

		
		public override void ExitNative_symbol (SParser.Native_symbolContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new NativeSymbol (name, exp));
		}

		public override void ExitNative_member_method_declaration (SParser.Native_member_method_declarationContext ctx)
		{
			IDeclaration decl = this.GetNodeValue<IDeclaration>(ctx.GetChild(0));
			SetNodeValue(ctx, decl);
		}


		public override void ExitTypeIdentifier (SParser.TypeIdentifierContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new UnresolvedIdentifier (name));
		}

		
		public override void ExitSymbolIdentifier (SParser.SymbolIdentifierContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new SymbolExpression (name));
		}


		
		public override void ExitBooleanType (SParser.BooleanTypeContext ctx)
		{
			SetNodeValue (ctx, BooleanType.Instance);
		}

		
		public override void ExitCharacterType (SParser.CharacterTypeContext ctx)
		{
			SetNodeValue (ctx, CharacterType.Instance);
		}

		
		public override void ExitTextType (SParser.TextTypeContext ctx)
		{
			SetNodeValue (ctx, TextType.Instance);
		}

		
		public override void ExitThisExpression (SParser.ThisExpressionContext ctx)
		{
			SetNodeValue (ctx, new ThisExpression ());
		}

		public override void ExitIntegerType (SParser.IntegerTypeContext ctx)
		{
			SetNodeValue (ctx, IntegerType.Instance);
		}

		
		public override void ExitDecimalType (SParser.DecimalTypeContext ctx)
		{
			SetNodeValue (ctx, DecimalType.Instance);
		}

		
		public override void ExitDateType (SParser.DateTypeContext ctx)
		{
			SetNodeValue (ctx, DateType.Instance);
		}

		
		public override void ExitDateTimeType (SParser.DateTimeTypeContext ctx)
		{
			SetNodeValue (ctx, TextType.Instance);
		}

		
		public override void ExitTimeType (SParser.TimeTypeContext ctx)
		{
			SetNodeValue (ctx, TimeType.Instance);
		}

		
		public override void ExitCodeType (SParser.CodeTypeContext ctx)
		{
			SetNodeValue (ctx, CodeType.Instance);
		}

		
		public override void ExitPrimaryType (SParser.PrimaryTypeContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.p);
			SetNodeValue (ctx, type);
		}

		
		public override void ExitAttribute_declaration (SParser.Attribute_declarationContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			IType type = this.GetNodeValue<IType> (ctx.typ);
			IAttributeConstraint match = this.GetNodeValue<IAttributeConstraint> (ctx.match);
			AttributeDeclaration decl = new AttributeDeclaration (name, type, match);
			decl.Storable = ctx.STORABLE () != null;
			SetNodeValue (ctx, decl);
		}

		
		public override void ExitNativeType (SParser.NativeTypeContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.n);
			SetNodeValue (ctx, type);
		}

		
		public override void ExitCategoryType (SParser.CategoryTypeContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.c);
			SetNodeValue (ctx, type);
		}

		
		public override void ExitCategory_type (SParser.Category_typeContext ctx)
		{
			String name = ctx.GetText ();
			SetNodeValue (ctx, new CategoryType (name));
		}

		
		public override void ExitListType (SParser.ListTypeContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.l);
			SetNodeValue (ctx, new ListType (type));
		}

		
		public override void ExitDictType (SParser.DictTypeContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.d);
			SetNodeValue (ctx, new DictType (type));
		}

		
		public override void ExitAttribute_list (SParser.Attribute_listContext ctx)
		{
			IdentifierList items = this.GetNodeValue<IdentifierList> (ctx.items);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitConcrete_category_declaration (SParser.Concrete_category_declarationContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			IdentifierList attrs = this.GetNodeValue<IdentifierList> (ctx.attrs);
			IdentifierList derived = this.GetNodeValue<IdentifierList> (ctx.derived);
			MethodDeclarationList methods = this.GetNodeValue<MethodDeclarationList> (ctx.methods);
			ConcreteCategoryDeclaration decl = new ConcreteCategoryDeclaration (name, attrs, derived, methods);
			decl.Storable = ctx.STORABLE () != null;
			SetNodeValue (ctx, decl);
		}

		
		public override void ExitConcreteCategoryDeclaration (SParser.ConcreteCategoryDeclarationContext ctx)
		{
			ConcreteCategoryDeclaration decl = this.GetNodeValue<ConcreteCategoryDeclaration> (ctx.decl);
			SetNodeValue (ctx, decl);
		}

		public override void ExitDerived_list (SParser.Derived_listContext ctx)
		{
			IdentifierList items = this.GetNodeValue<IdentifierList> (ctx.items);
			SetNodeValue (ctx, items);
		}


		
		public override void ExitType_identifier (SParser.Type_identifierContext ctx)
		{
			SetNodeValue (ctx, ctx.GetText ());
		}

		
		public override void ExitTypeIdentifierList (SParser.TypeIdentifierListContext ctx)
		{
			String item = this.GetNodeValue<String> (ctx.item);
			SetNodeValue (ctx, new IdentifierList (item));
		}

		
		public override void ExitTypeIdentifierListItem (SParser.TypeIdentifierListItemContext ctx)
		{
			IdentifierList items = this.GetNodeValue<IdentifierList> (ctx.items);
			String item = this.GetNodeValue<String> (ctx.item);
			items.Add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitMemberSelector (SParser.MemberSelectorContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new MemberSelector (name));
		}

		
		public override void ExitIsATypeExpression(SParser.IsATypeExpressionContext ctx) {
			IType type = this.GetNodeValue<IType>(ctx.typ);
			IExpression exp = new TypeExpression(type);
			SetNodeValue(ctx, exp);
		}

		public override void ExitIsOtherExpression(SParser.IsOtherExpressionContext ctx) {
			IExpression exp = this.GetNodeValue<IExpression>(ctx.exp);
			SetNodeValue(ctx, exp);
		}

		public override void ExitIsExpression(SParser.IsExpressionContext ctx) {
			IExpression left = this.GetNodeValue<IExpression>(ctx.left);
			IExpression right = this.GetNodeValue<IExpression>(ctx.right);
			EqOp op = right is TypeExpression ? EqOp.IS_A : EqOp.IS;
			SetNodeValue(ctx, new EqualsExpression(left, op, right));
		}

		public override void ExitIsNotExpression(SParser.IsNotExpressionContext ctx) {
			IExpression left = this.GetNodeValue<IExpression>(ctx.left);
			IExpression right = this.GetNodeValue<IExpression>(ctx.right);
			EqOp op = right is TypeExpression ? EqOp.IS_NOT_A : EqOp.IS_NOT;
			SetNodeValue(ctx, new EqualsExpression(left, op, right));
		}		


		
		public override void ExitItemSelector (SParser.ItemSelectorContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new ItemSelector (exp));
		}

		
		public override void ExitSliceSelector (SParser.SliceSelectorContext ctx)
		{
			IExpression slice = this.GetNodeValue<IExpression> (ctx.xslice);
			SetNodeValue (ctx, slice);
		}

		
		public override void ExitTyped_argument (SParser.Typed_argumentContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.typ);
			String name = this.GetNodeValue<String> (ctx.name);
			IdentifierList attrs = this.GetNodeValue<IdentifierList> (ctx.attrs);
			CategoryArgument arg = new CategoryArgument (type, name, attrs);
			IExpression exp = this.GetNodeValue<IExpression>(ctx.value);
			arg.DefaultValue = exp;
			SetNodeValue(ctx, arg);
		}

		
		public override void ExitTypedArgument (SParser.TypedArgumentContext ctx)
		{
			IArgument arg = this.GetNodeValue<IArgument> (ctx.arg); 
			SetNodeValue (ctx, arg);
		}

		
		public override void ExitNamedArgument (SParser.NamedArgumentContext ctx)
		{
			IArgument arg = this.GetNodeValue<IArgument> (ctx.arg);
			SetNodeValue (ctx, arg);
		}

		
		public override void ExitCodeArgument (SParser.CodeArgumentContext ctx)
		{
			IArgument arg = this.GetNodeValue<IArgument> (ctx.arg);
			SetNodeValue (ctx, arg);
		}

		
		public override void ExitCategoryArgumentType (SParser.CategoryArgumentTypeContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.typ);
			SetNodeValue (ctx, type);
		}

		
		public override void ExitArgumentList (SParser.ArgumentListContext ctx)
		{
			IArgument item = this.GetNodeValue<IArgument> (ctx.item); 
			SetNodeValue (ctx, new ArgumentList (item));
		}

		
		public override void ExitArgumentListItem (SParser.ArgumentListItemContext ctx)
		{
			ArgumentList items = this.GetNodeValue<ArgumentList> (ctx.items); 
			IArgument item = this.GetNodeValue<IArgument> (ctx.item); 
			items.Add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitMethodTypeIdentifier (SParser.MethodTypeIdentifierContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, name);
		}

		
		public override void ExitMethodVariableIdentifier (SParser.MethodVariableIdentifierContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, name);
		}

		
		public override void ExitMethodName (SParser.MethodNameContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new UnresolvedIdentifier (name));
		}

	
		
		public override void ExitMethodParent (SParser.MethodParentContext ctx)
		{
			IExpression parent = this.GetNodeValue<IExpression> (ctx.parent);
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new MethodSelector (parent, name));
		}

	
		
		public override void ExitMethod_call (SParser.Method_callContext ctx)
		{
			IExpression method = this.GetNodeValue<IExpression> (ctx.method);
			ArgumentAssignmentList args = this.GetNodeValue<ArgumentAssignmentList> (ctx.args);
			SetNodeValue (ctx, new UnresolvedCall (method, args));
		}

		public override void ExitExpressionAssignmentList (SParser.ExpressionAssignmentListContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			ArgumentAssignment item = new ArgumentAssignment (null, exp);
			ArgumentAssignmentList items = new ArgumentAssignmentList (item);
			SetNodeValue (ctx, items);
		}

		 
		public override void ExitArgument_assignment (SParser.Argument_assignmentContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			IArgument arg = new UnresolvedArgument (name);
			SetNodeValue (ctx, new ArgumentAssignment (arg, exp));
		}

		 
		public override void ExitArgumentAssignmentList (SParser.ArgumentAssignmentListContext ctx)
		{
			ArgumentAssignment item = this.GetNodeValue<ArgumentAssignment> (ctx.item);
			ArgumentAssignmentList items = new ArgumentAssignmentList (item);
			SetNodeValue (ctx, items);
		}

 
		
		public override void ExitArgumentAssignmentListItem (SParser.ArgumentAssignmentListItemContext ctx)
		{
			ArgumentAssignment item = this.GetNodeValue<ArgumentAssignment> (ctx.item);
			ArgumentAssignmentList items = this.GetNodeValue<ArgumentAssignmentList> (ctx.items);
			items.add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitCallableRoot (SParser.CallableRootContext ctx)
		{
			IExpression name = this.GetNodeValue<IExpression>(ctx.name);
			SetNodeValue(ctx, name);
		}

		
		public override void ExitCallableSelector (SParser.CallableSelectorContext ctx)
		{
			IExpression parent = this.GetNodeValue<IExpression> (ctx.parent);
			SelectorExpression select = this.GetNodeValue<SelectorExpression> (ctx.select);
			select.setParent (parent);
			SetNodeValue (ctx, select);
		}

		
		public override void ExitCallableMemberSelector (SParser.CallableMemberSelectorContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new MemberSelector (name));
		}

		
		public override void ExitCallableItemSelector (SParser.CallableItemSelectorContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new ItemSelector (exp));
		}

		
		public override void ExitAddExpression (SParser.AddExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			IExpression exp = ctx.op.Type == SParser.PLUS ? 
            (IExpression)new AddExpression (left, right) 
            : (IExpression)new SubtractExpression (left, right);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitCategoryMethodList (SParser.CategoryMethodListContext ctx)
		{
			IMethodDeclaration item = this.GetNodeValue<IMethodDeclaration> (ctx.item);
			MethodDeclarationList items = new MethodDeclarationList (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitCategoryMethodListItem (SParser.CategoryMethodListItemContext ctx)
		{
			IMethodDeclaration item = this.GetNodeValue<IMethodDeclaration> (ctx.item);
			MethodDeclarationList items = this.GetNodeValue<MethodDeclarationList> (ctx.items);
			items.add (item);
			SetNodeValue (ctx, items);
		}

		public override void ExitNativeCategoryMethodList (SParser.NativeCategoryMethodListContext ctx)
		{
			IMethodDeclaration item = this.GetNodeValue<IMethodDeclaration> (ctx.item);
			MethodDeclarationList items = new MethodDeclarationList (item);
			SetNodeValue (ctx, items);
		}


		public override void ExitNativeCategoryMethodListItem (SParser.NativeCategoryMethodListItemContext ctx)
		{
			IMethodDeclaration item = this.GetNodeValue<IMethodDeclaration> (ctx.item);
			MethodDeclarationList items = this.GetNodeValue<MethodDeclarationList> (ctx.items);
			items.add (item);
			SetNodeValue (ctx, items);
		}

		public override void ExitSetter_method_declaration (SParser.Setter_method_declarationContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new SetterMethodDeclaration (name, stmts));
		}

		
		public override void ExitGetter_method_declaration (SParser.Getter_method_declarationContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new GetterMethodDeclaration (name, stmts));
		}

		public override void ExitMember_method_declaration (SParser.Member_method_declarationContext ctx)
		{
			IDeclaration decl = this.GetNodeValue<IDeclaration> (ctx.GetChild(0));
			SetNodeValue (ctx, decl);
		}


		public override void ExitStatementList (SParser.StatementListContext ctx)
		{
			IStatement item = this.GetNodeValue<IStatement> (ctx.item);
			SetNodeValue (ctx, new StatementList (item));
		}

		
		public override void ExitStatementListItem (SParser.StatementListItemContext ctx)
		{
			IStatement item = this.GetNodeValue<IStatement> (ctx.item);
			StatementList items = this.GetNodeValue<StatementList> (ctx.items);
			items.add (item);
			SetNodeValue (ctx, items);
		}

		public override void ExitStoreStatement (SParser.StoreStatementContext ctx)
		{
			SetNodeValue (ctx, this.GetNodeValue<Object> (ctx.stmt));
		}

		public override void ExitStore_statement (SParser.Store_statementContext ctx)
		{
			ExpressionList exps = this.GetNodeValue<ExpressionList>(ctx.exps);
			StoreStatement stmt = new StoreStatement(exps);
			SetNodeValue(ctx, stmt);
		}

		
		public override void ExitAbstract_method_declaration (SParser.Abstract_method_declarationContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.typ);
			String name = this.GetNodeValue<String> (ctx.name);
			ArgumentList args = this.GetNodeValue<ArgumentList> (ctx.args);
			SetNodeValue (ctx, new AbstractMethodDeclaration (name, args, type));
		}

		
		public override void ExitConcrete_method_declaration (SParser.Concrete_method_declarationContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.typ);
			String name = this.GetNodeValue<String> (ctx.name);
			ArgumentList args = this.GetNodeValue<ArgumentList> (ctx.args);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new ConcreteMethodDeclaration (name, args, type, stmts));
		}

		
		public override void ExitMethodCallStatement (SParser.MethodCallStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitMethodCallExpression (SParser.MethodCallExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitConstructor_expression (SParser.Constructor_expressionContext ctx)
		{
			bool mutable = ctx.MUTABLE() != null;
			CategoryType type = this.GetNodeValue<CategoryType> (ctx.typ);
			ArgumentAssignmentList args = this.GetNodeValue<ArgumentAssignmentList> (ctx.args);
			SetNodeValue (ctx, new ConstructorExpression (type, mutable, args));
		}

		
		public override void ExitAssertion(SParser.AssertionContext ctx) {
			IExpression exp = this.GetNodeValue<IExpression>(ctx.exp);
			SetNodeValue(ctx, exp);
		}

		public override void ExitAssertionList(SParser.AssertionListContext ctx) {
			IExpression item = this.GetNodeValue<IExpression>(ctx.item);
			ExpressionList items = new ExpressionList(item);
			SetNodeValue(ctx, items);
		}

		public override void ExitAssertionListItem(SParser.AssertionListItemContext ctx) {
			IExpression item = this.GetNodeValue<IExpression>(ctx.item);
			ExpressionList items = this.GetNodeValue<ExpressionList>(ctx.items);
			items.add(item);
			SetNodeValue(ctx, items);
		}

		public override void ExitAssign_instance_statement (SParser.Assign_instance_statementContext ctx)
		{
			IAssignableInstance inst = this.GetNodeValue<IAssignableInstance> (ctx.inst);
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new AssignInstanceStatement (inst, exp));
		}

		
		public override void ExitAssignInstanceStatement (SParser.AssignInstanceStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitAssign_variable_statement (SParser.Assign_variable_statementContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new AssignVariableStatement (name, exp));
		}

		
		public override void ExitAssign_tuple_statement (SParser.Assign_tuple_statementContext ctx)
		{
			IdentifierList items = this.GetNodeValue<IdentifierList> (ctx.items);
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new AssignTupleStatement (items, exp));
		}

		
		public override void ExitVariableList (SParser.VariableListContext ctx)
		{
			String item = this.GetNodeValue<String> (ctx.item);
			SetNodeValue (ctx, new IdentifierList (item));
		}

		
		public override void ExitVariableListItem (SParser.VariableListItemContext ctx)
		{
			String item = this.GetNodeValue<String> (ctx.item);
			IdentifierList items = this.GetNodeValue<IdentifierList> (ctx.items);
			items.Add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitRootInstance (SParser.RootInstanceContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new VariableInstance (name));
		}

		public override void ExitRoughlyEqualsExpression (SParser.RoughlyEqualsExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new EqualsExpression (left, EqOp.ROUGHLY, right));
		}

		
		public override void ExitChildInstance (SParser.ChildInstanceContext ctx)
		{
			IAssignableInstance parent = this.GetNodeValue<IAssignableInstance> (ctx.parent);
			IAssignableSelector child = this.GetNodeValue<IAssignableSelector> (ctx.child);
			child.SetParent (parent);
			SetNodeValue (ctx, child);
		}

		
		public override void ExitMemberInstance (SParser.MemberInstanceContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new MemberInstance (name));
		}

		
		public override void ExitItemInstance (SParser.ItemInstanceContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new ItemInstance (exp));
		}

		
		public override void ExitMethodExpression (SParser.MethodExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitConstructorExpression (SParser.ConstructorExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitNativeStatementList (SParser.NativeStatementListContext ctx)
		{
			IStatement item = this.GetNodeValue<IStatement> (ctx.item);
			StatementList items = new StatementList (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitNativeStatementListItem (SParser.NativeStatementListItemContext ctx)
		{
			IStatement item = this.GetNodeValue<IStatement> (ctx.item);
			StatementList items = this.GetNodeValue<StatementList> (ctx.items);
			items.add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitJava_identifier (SParser.Java_identifierContext ctx)
		{
			SetNodeValue (ctx, ctx.GetText ());
		}

		
		public override void ExitCsharp_identifier (SParser.Csharp_identifierContext ctx)
		{
			SetNodeValue (ctx, ctx.GetText ());
		}

		public override void ExitCSharpPromptoIdentifier (SParser.CSharpPromptoIdentifierContext ctx)
		{
			String name = ctx.DOLLAR_IDENTIFIER().GetText();
			SetNodeValue (ctx, new CSharpIdentifierExpression (name));
		}

	
		public override void ExitCSharpPrimaryExpression (SParser.CSharpPrimaryExpressionContext ctx)
		{
			CSharpExpression exp = this.GetNodeValue<CSharpExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitPython_identifier (SParser.Python_identifierContext ctx)
		{
			SetNodeValue (ctx, ctx.GetText ());
		}


		public override void ExitPythonPromptoIdentifier (SParser.PythonPromptoIdentifierContext ctx)
		{
			String name = ctx.DOLLAR_IDENTIFIER ().GetText ();
			SetNodeValue (ctx, new PythonIdentifierExpression(name));
		}
			
		
		public override void ExitPythonPrimaryExpression (SParser.PythonPrimaryExpressionContext ctx)
		{
			PythonExpression exp = this.GetNodeValue<PythonExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitJavaIdentifier (SParser.JavaIdentifierContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new JavaIdentifierExpression (name));
		}

		public override void ExitJava_primary_expression (SParser.Java_primary_expressionContext ctx)
		{
			JavaExpression exp = this.GetNodeValue<JavaExpression> (ctx.GetChild(0));
			SetNodeValue (ctx, exp);
		}
		
		public override void ExitJavaPrimaryExpression (SParser.JavaPrimaryExpressionContext ctx)
		{
			JavaExpression exp = this.GetNodeValue<JavaExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		public override void ExitJava_this_expression (SParser.Java_this_expressionContext ctx)
		{
			SetNodeValue (ctx, new JavaThisExpression());
		}



		
		public override void ExitCSharpIdentifier (SParser.CSharpIdentifierContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new CSharpIdentifierExpression (name));
		}

		
		public override void ExitPythonIdentifier (SParser.PythonIdentifierContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new PythonIdentifierExpression (name));
		}

		
		public override void ExitJavaChildIdentifier (SParser.JavaChildIdentifierContext ctx)
		{
			JavaIdentifierExpression parent = this.GetNodeValue<JavaIdentifierExpression> (ctx.parent);
			String name = this.GetNodeValue<String> (ctx.name);
			JavaIdentifierExpression child = new JavaIdentifierExpression (parent, name);
			SetNodeValue (ctx, child);
		}

		
		public override void ExitCSharpChildIdentifier (SParser.CSharpChildIdentifierContext ctx)
		{
			CSharpIdentifierExpression parent = this.GetNodeValue<CSharpIdentifierExpression> (ctx.parent);
			String name = this.GetNodeValue<String> (ctx.name);
			CSharpIdentifierExpression child = new CSharpIdentifierExpression (parent, name);
			SetNodeValue (ctx, child);
		}

		
		public override void ExitPythonChildIdentifier (SParser.PythonChildIdentifierContext ctx)
		{
			PythonIdentifierExpression parent = this.GetNodeValue<PythonIdentifierExpression> (ctx.parent);
			String name = this.GetNodeValue<String> (ctx.name);
			PythonIdentifierExpression child = new PythonIdentifierExpression (parent, name);
			SetNodeValue (ctx, child);
		}

	
		
		public override void ExitJavaClassIdentifier (SParser.JavaClassIdentifierContext ctx)
		{
			JavaIdentifierExpression klass = this.GetNodeValue<JavaIdentifierExpression> (ctx.klass);
			SetNodeValue (ctx, klass);
		}

		
		public override void ExitJavaChildClassIdentifier (SParser.JavaChildClassIdentifierContext ctx)
		{
			JavaIdentifierExpression parent = this.GetNodeValue<JavaIdentifierExpression> (ctx.parent);
			JavaIdentifierExpression child = new JavaIdentifierExpression (parent, ctx.name.Text);
			SetNodeValue (ctx, child);
		}

		
		public override void ExitJavaSelectorExpression (SParser.JavaSelectorExpressionContext ctx)
		{
			JavaExpression parent = this.GetNodeValue<JavaExpression> (ctx.parent);
			JavaSelectorExpression child = this.GetNodeValue<JavaSelectorExpression> (ctx.child);
			child.SetParent (parent);
			SetNodeValue (ctx, child);
		}

		
		public override void ExitJavaStatement (SParser.JavaStatementContext ctx)
		{
			JavaExpression exp = this.GetNodeValue<JavaExpression> (ctx.exp);
			SetNodeValue (ctx, new JavaStatement (exp, false));
		}

		
		public override void ExitCSharpStatement (SParser.CSharpStatementContext ctx)
		{
			CSharpExpression exp = this.GetNodeValue<CSharpExpression> (ctx.exp);
			SetNodeValue (ctx, new CSharpStatement (exp, false));
		}

		
		public override void ExitPythonStatement (SParser.PythonStatementContext ctx)
		{
			PythonExpression exp = this.GetNodeValue<PythonExpression> (ctx.exp);
			SetNodeValue (ctx, new PythonStatement (exp, false));
		}

		
		public override void ExitJavaReturnStatement (SParser.JavaReturnStatementContext ctx)
		{
			JavaExpression exp = this.GetNodeValue<JavaExpression> (ctx.exp);
			SetNodeValue (ctx, new JavaStatement (exp, true));
		}

		
		public override void ExitCSharpReturnStatement (SParser.CSharpReturnStatementContext ctx)
		{
			CSharpExpression exp = this.GetNodeValue<CSharpExpression> (ctx.exp);
			SetNodeValue (ctx, new CSharpStatement (exp, true));
		}

		
		public override void ExitPythonReturnStatement (SParser.PythonReturnStatementContext ctx)
		{
			PythonExpression exp = this.GetNodeValue<PythonExpression> (ctx.exp);
			SetNodeValue (ctx, new PythonStatement (exp, true));
		}

		
		public override void ExitJavaNativeStatement (SParser.JavaNativeStatementContext ctx)
		{
			JavaStatement stmt = this.GetNodeValue<JavaStatement> (ctx.stmt);
			SetNodeValue (ctx, new JavaNativeCall (stmt));
		}

		
		public override void ExitCSharpNativeStatement (SParser.CSharpNativeStatementContext ctx)
		{
			CSharpStatement stmt = this.GetNodeValue<CSharpStatement> (ctx.stmt);
			SetNodeValue (ctx, new CSharpNativeCall (stmt));
		}

		
		public override void ExitPython2NativeStatement (SParser.Python2NativeStatementContext ctx)
		{
			PythonStatement stmt = this.GetNodeValue<PythonStatement> (ctx.stmt);
			SetNodeValue (ctx, new Python2NativeCall (stmt));
		}

		
		public override void ExitPython3NativeStatement (SParser.Python3NativeStatementContext ctx)
		{
			PythonStatement stmt = this.GetNodeValue<PythonStatement> (ctx.stmt);
			SetNodeValue (ctx, new Python3NativeCall (stmt));
		}

		
		public override void ExitNative_method_declaration (SParser.Native_method_declarationContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.typ);
			String name = this.GetNodeValue<String> (ctx.name);
			ArgumentList args = this.GetNodeValue<ArgumentList> (ctx.args);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new NativeMethodDeclaration (name, args, type, stmts));
		}

		
		public override void ExitJavaArgumentList (SParser.JavaArgumentListContext ctx)
		{
			JavaExpression item = this.GetNodeValue<JavaExpression> (ctx.item);
			SetNodeValue (ctx, new JavaExpressionList (item));
		}

		
		public override void ExitJavaArgumentListItem (SParser.JavaArgumentListItemContext ctx)
		{
			JavaExpression item = this.GetNodeValue<JavaExpression> (ctx.item);
			JavaExpressionList items = this.GetNodeValue<JavaExpressionList> (ctx.items);
			items.Add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitJava_method_expression (SParser.Java_method_expressionContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			JavaExpressionList args = this.GetNodeValue<JavaExpressionList> (ctx.args);
			SetNodeValue (ctx, new JavaMethodExpression (name, args));
		}

		
		public override void ExitJavaMethodExpression (SParser.JavaMethodExpressionContext ctx)
		{
			JavaExpression exp = this.GetNodeValue<JavaExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitDeclaration (SParser.DeclarationContext ctx)
		{

			List<CommentStatement> stmts = null;
			foreach(SParser.Comment_statementContext csc in ctx.comment_statement()) {
				if(csc==null)
					continue;
				if(stmts==null)
					stmts = new List<CommentStatement>();
				stmts.add((CommentStatement)this.GetNodeValue<CommentStatement>(csc));
			}
			ParserRuleContext ctx_ = ctx.attribute_declaration();
			if(ctx_==null)
				ctx_ = ctx.category_declaration();
			if(ctx_==null)
				ctx_ = ctx.enum_declaration();
			if(ctx_==null)
				ctx_ = ctx.method_declaration();
			if(ctx_==null)
				ctx_ = ctx.resource_declaration();
			IDeclaration decl = this.GetNodeValue<IDeclaration>(ctx_);
			if(decl!=null) {
				decl.Comments = stmts;
				SetNodeValue(ctx, decl);
			}		
		}


		public override void ExitDeclarationList (SParser.DeclarationListContext ctx)
		{
			IDeclaration item = this.GetNodeValue<IDeclaration> (ctx.item);
			DeclarationList items = new DeclarationList (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitDeclarationListItem (SParser.DeclarationListItemContext ctx)
		{
			IDeclaration item = this.GetNodeValue<IDeclaration> (ctx.item);
			DeclarationList items = this.GetNodeValue<DeclarationList> (ctx.items);
			items.Add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitNativeMethod (SParser.NativeMethodContext ctx)
		{
			IDeclaration decl = this.GetNodeValue<IDeclaration> (ctx.decl);
			SetNodeValue (ctx, decl);
		}

		
		public override void ExitConcreteMethod (SParser.ConcreteMethodContext ctx)
		{
			IDeclaration decl = this.GetNodeValue<IDeclaration> (ctx.decl);
			SetNodeValue (ctx, decl);
		}

		
		public override void ExitAbstractMethod (SParser.AbstractMethodContext ctx)
		{
			IDeclaration decl = this.GetNodeValue<IDeclaration> (ctx.decl);
			SetNodeValue (ctx, decl);
		}

		
		public override void ExitJavaBooleanLiteral (SParser.JavaBooleanLiteralContext ctx)
		{
			SetNodeValue (ctx, new JavaBooleanLiteral (ctx.GetText ()));
		}

		
		public override void ExitJavaIntegerLiteral (SParser.JavaIntegerLiteralContext ctx)
		{
			SetNodeValue (ctx, new JavaIntegerLiteral (ctx.GetText ()));
		}

		
		public override void ExitJavaDecimalLiteral (SParser.JavaDecimalLiteralContext ctx)
		{
			SetNodeValue (ctx, new JavaDecimalLiteral (ctx.GetText ()));
		}

		
		public override void ExitJavaCharacterLiteral (SParser.JavaCharacterLiteralContext ctx)
		{
			SetNodeValue (ctx, new JavaCharacterLiteral (ctx.GetText ()));
		}

		
		public override void ExitJavaTextLiteral (SParser.JavaTextLiteralContext ctx)
		{
			SetNodeValue (ctx, new JavaTextLiteral (ctx.GetText ()));
		}

		
		public override void ExitCSharpBooleanLiteral (SParser.CSharpBooleanLiteralContext ctx)
		{
			SetNodeValue (ctx, new CSharpBooleanLiteral (ctx.GetText ()));
		}

		
		public override void ExitCSharpIntegerLiteral (SParser.CSharpIntegerLiteralContext ctx)
		{
			SetNodeValue (ctx, new CSharpIntegerLiteral (ctx.GetText ()));
		}

		
		public override void ExitCSharpDecimalLiteral (SParser.CSharpDecimalLiteralContext ctx)
		{
			SetNodeValue (ctx, new CSharpDecimalLiteral (ctx.GetText ()));
		}

		
		public override void ExitCSharpCharacterLiteral (SParser.CSharpCharacterLiteralContext ctx)
		{
			SetNodeValue (ctx, new CSharpCharacterLiteral (ctx.GetText ()));
		}

		
		public override void ExitCSharpTextLiteral (SParser.CSharpTextLiteralContext ctx)
		{
			SetNodeValue (ctx, new CSharpTextLiteral (ctx.GetText ()));
		}

		
		public override void ExitPythonBooleanLiteral (SParser.PythonBooleanLiteralContext ctx)
		{
			SetNodeValue (ctx, new PythonBooleanLiteral (ctx.GetText ()));
		}

		public override void ExitPythonCharacterLiteral (SParser.PythonCharacterLiteralContext ctx)
		{
			SetNodeValue (ctx, new PythonCharacterLiteral (ctx.t.Text));
		}

		
		public override void ExitPythonIntegerLiteral (SParser.PythonIntegerLiteralContext ctx)
		{
			SetNodeValue (ctx, new PythonIntegerLiteral (ctx.GetText ()));
		}

		
		public override void ExitPythonDecimalLiteral (SParser.PythonDecimalLiteralContext ctx)
		{
			SetNodeValue (ctx, new PythonDecimalLiteral (ctx.GetText ()));
		}

		
		public override void ExitPythonTextLiteral (SParser.PythonTextLiteralContext ctx)
		{
			SetNodeValue (ctx, new PythonTextLiteral (ctx.GetText ()));
		}

		
		public override void ExitPythonLiteralExpression (SParser.PythonLiteralExpressionContext ctx)
		{
			PythonExpression exp = this.GetNodeValue<PythonExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitJavaCategoryBinding (SParser.JavaCategoryBindingContext ctx)
		{
			JavaIdentifierExpression map = this.GetNodeValue<JavaIdentifierExpression> (ctx.binding);
			SetNodeValue (ctx, new JavaNativeCategoryBinding (map));
		}

		
		public override void ExitCSharpCategoryBinding (SParser.CSharpCategoryBindingContext ctx)
		{
			CSharpIdentifierExpression map = this.GetNodeValue<CSharpIdentifierExpression> (ctx.binding);
			SetNodeValue (ctx, new CSharpNativeCategoryBinding (map));
		}

		
		public override void ExitPython_module (SParser.Python_moduleContext ctx)
		{
			List<String> ids = new List<String> ();
			foreach (SParser.IdentifierContext ic in ctx.identifier())
				ids.Add (ic.GetText ());
			PythonModule module = new PythonModule (ids);
			SetNodeValue (ctx, module);
		}


		
		public override void ExitPython_native_statement (SParser.Python_native_statementContext ctx)
		{
			PythonStatement stmt = this.GetNodeValue<PythonStatement> (ctx.stmt);
			PythonModule module = this.GetNodeValue<PythonModule> (ctx.module);
			stmt.setModule (module);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitPython2CategoryBinding (SParser.Python2CategoryBindingContext ctx)
		{
			PythonNativeCategoryBinding map = this.GetNodeValue<PythonNativeCategoryBinding> (ctx.binding);
			SetNodeValue (ctx, new Python2NativeCategoryBinding (map));
		}

		
		public override void ExitPython3CategoryBinding (SParser.Python3CategoryBindingContext ctx)
		{
			PythonNativeCategoryBinding map = this.GetNodeValue<PythonNativeCategoryBinding> (ctx.binding);
			SetNodeValue (ctx, new Python3NativeCategoryBinding (map));
		}

		
		public override void ExitNativeCategoryBindingList (SParser.NativeCategoryBindingListContext ctx)
		{
			NativeCategoryBinding item = this.GetNodeValue<NativeCategoryBinding> (ctx.item);
			NativeCategoryBindingList items = new NativeCategoryBindingList (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitNativeCategoryBindingListItem (SParser.NativeCategoryBindingListItemContext ctx)
		{
			NativeCategoryBinding item = this.GetNodeValue<NativeCategoryBinding> (ctx.item);
			NativeCategoryBindingList items = this.GetNodeValue<NativeCategoryBindingList> (ctx.items);
			items.Add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitNative_category_bindings (SParser.Native_category_bindingsContext ctx)
		{
			NativeCategoryBindingList items = this.GetNodeValue<NativeCategoryBindingList> (ctx.items);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitNative_category_declaration (SParser.Native_category_declarationContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			IdentifierList attrs = this.GetNodeValue<IdentifierList> (ctx.attrs);
			NativeCategoryBindingList bindings = this.GetNodeValue<NativeCategoryBindingList> (ctx.bindings);
			MethodDeclarationList methods = this.GetNodeValue<MethodDeclarationList> (ctx.methods);
			NativeCategoryDeclaration decl = new NativeCategoryDeclaration (name, attrs, bindings, null, methods);
			decl.Storable = ctx.STORABLE () != null;
			SetNodeValue (ctx, decl);
		}

		
		public override void ExitNativeCategoryDeclaration (SParser.NativeCategoryDeclarationContext ctx)
		{
			IDeclaration decl = this.GetNodeValue<IDeclaration> (ctx.decl);
			SetNodeValue (ctx, decl);
		}

		
		public override void ExitNative_resource_declaration (SParser.Native_resource_declarationContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			IdentifierList attrs = this.GetNodeValue<IdentifierList> (ctx.attrs);
			NativeCategoryBindingList bindings = this.GetNodeValue<NativeCategoryBindingList> (ctx.bindings);
			MethodDeclarationList methods = this.GetNodeValue<MethodDeclarationList> (ctx.methods);
			SetNodeValue (ctx, new NativeResourceDeclaration (name, attrs, bindings, null, methods));
		}

		
		public override void ExitResource_declaration (SParser.Resource_declarationContext ctx)
		{
			IDeclaration decl = this.GetNodeValue<IDeclaration> (ctx.decl);
			SetNodeValue (ctx, decl);
		}

		
		public override void ExitEnumCategoryDeclaration (SParser.EnumCategoryDeclarationContext ctx)
		{
			IDeclaration decl = this.GetNodeValue<IDeclaration> (ctx.decl);
			SetNodeValue (ctx, decl);
		}

		
		public override void ExitEnumNativeDeclaration (SParser.EnumNativeDeclarationContext ctx)
		{
			IDeclaration decl = this.GetNodeValue<IDeclaration> (ctx.decl);
			SetNodeValue (ctx, decl);
		}

		
		public override void ExitParenthesis_expression (SParser.Parenthesis_expressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new ParenthesisExpression (exp));
		}

		
		public override void ExitParenthesisExpression (SParser.ParenthesisExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitNativeSymbolList (SParser.NativeSymbolListContext ctx)
		{
			NativeSymbol item = this.GetNodeValue<NativeSymbol> (ctx.item);
			SetNodeValue (ctx, new NativeSymbolList (item));
		}

		
		public override void ExitNativeSymbolListItem (SParser.NativeSymbolListItemContext ctx)
		{
			NativeSymbol item = this.GetNodeValue<NativeSymbol> (ctx.item);
			NativeSymbolList items = this.GetNodeValue<NativeSymbolList> (ctx.items);
			items.add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitEnum_native_declaration (SParser.Enum_native_declarationContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			NativeType type = this.GetNodeValue<NativeType> (ctx.typ);
			NativeSymbolList symbols = this.GetNodeValue<NativeSymbolList> (ctx.symbols);
			SetNodeValue (ctx, new EnumeratedNativeDeclaration (name, type, symbols));
		}

		
		public override void ExitFor_each_statement (SParser.For_each_statementContext ctx)
		{
			String name1 = this.GetNodeValue<String> (ctx.name1);
			String name2 = this.GetNodeValue<String> (ctx.name2);
			IExpression source = this.GetNodeValue<IExpression> (ctx.source);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new ForEachStatement (name1, name2, source, stmts));
		}

		
		public override void ExitForEachStatement (SParser.ForEachStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitKey_token (SParser.Key_tokenContext ctx)
		{
			SetNodeValue (ctx, ctx.GetText ());
		}

		
		public override void ExitValue_token (SParser.Value_tokenContext ctx)
		{
			SetNodeValue (ctx, ctx.GetText ());
		}

		
		public override void ExitNamed_argument (SParser.Named_argumentContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			UnresolvedArgument arg = new UnresolvedArgument(name);
			IExpression exp = this.GetNodeValue<IExpression>(ctx.value);
			arg.DefaultValue = exp;
			SetNodeValue(ctx, arg);
		}

		
		public override void ExitClosureStatement (SParser.ClosureStatementContext ctx)
		{
			ConcreteMethodDeclaration decl = this.GetNodeValue<ConcreteMethodDeclaration> (ctx.decl);
			SetNodeValue (ctx, new DeclarationInstruction<ConcreteMethodDeclaration> (decl));
		}

		
		public override void ExitReturn_statement (SParser.Return_statementContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new ReturnStatement (exp));
		}

		
		public override void ExitReturnStatement (SParser.ReturnStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitClosure_expression (SParser.Closure_expressionContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new MethodExpression (name));
		}

		
		public override void ExitClosureExpression (SParser.ClosureExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitIf_statement (SParser.If_statementContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			IfElementList elseIfs = this.GetNodeValue<IfElementList> (ctx.elseIfs);
			StatementList elseStmts = this.GetNodeValue<StatementList> (ctx.elseStmts);
			SetNodeValue (ctx, new IfStatement (exp, stmts, elseIfs, elseStmts));
		}

		
		public override void ExitElseIfStatementList (SParser.ElseIfStatementListContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			IfElement elem = new IfElement (exp, stmts);
			SetNodeValue (ctx, new IfElementList (elem));
		}

		
		public override void ExitElseIfStatementListItem (SParser.ElseIfStatementListItemContext ctx)
		{
			IfElementList items = this.GetNodeValue<IfElementList> (ctx.items);
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			IfElement elem = new IfElement (exp, stmts);
			items.Add (elem);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitIfStatement (SParser.IfStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitSwitchStatement (SParser.SwitchStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitAssignTupleStatement (SParser.AssignTupleStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitRaiseStatement (SParser.RaiseStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitWriteStatement (SParser.WriteStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitWithResourceStatement (SParser.WithResourceStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		public override void ExitWith_singleton_statement(SParser.With_singleton_statementContext ctx) {
			String name = this.GetNodeValue<String>(ctx.typ);
			CategoryType type = new CategoryType(name);
			StatementList stmts = this.GetNodeValue<StatementList>(ctx.stmts);
			SetNodeValue(ctx, new WithSingletonStatement(type, stmts));
		}

		public override void ExitWithSingletonStatement(SParser.WithSingletonStatementContext ctx) {
			IStatement stmt = this.GetNodeValue<IStatement>(ctx.stmt);
			SetNodeValue(ctx, stmt);
		}
		
		public override void ExitWhileStatement (SParser.WhileStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitDoWhileStatement (SParser.DoWhileStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitTryStatement (SParser.TryStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitEqualsExpression (SParser.EqualsExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new EqualsExpression (left, EqOp.EQUALS, right));
		}

		
		public override void ExitNotEqualsExpression (SParser.NotEqualsExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new EqualsExpression (left, EqOp.NOT_EQUALS, right));
		}

		
		public override void ExitGreaterThanExpression (SParser.GreaterThanExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new CompareExpression (left, CmpOp.GT, right));
		}

		
		public override void ExitGreaterThanOrEqualExpression (SParser.GreaterThanOrEqualExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new CompareExpression (left, CmpOp.GTE, right));
		}

		
		public override void ExitLessThanExpression (SParser.LessThanExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new CompareExpression (left, CmpOp.LT, right));
		}

		
		public override void ExitLessThanOrEqualExpression (SParser.LessThanOrEqualExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new CompareExpression (left, CmpOp.LTE, right));
		}

		
		public override void ExitAtomicSwitchCase (SParser.AtomicSwitchCaseContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new AtomicSwitchCase (exp, stmts));
		}

		public override void ExitCommentStatement(SParser.CommentStatementContext ctx) {
			SetNodeValue(ctx, this.GetNodeValue<Object>(ctx.comment_statement()));
		}

		public override void ExitComment_statement(SParser.Comment_statementContext ctx) {
			SetNodeValue(ctx, new CommentStatement(ctx.GetText()));
		}


		public override void ExitCollectionSwitchCase (SParser.CollectionSwitchCaseContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new CollectionSwitchCase (exp, stmts));
		}

		
		public override void ExitSwitchCaseStatementList (SParser.SwitchCaseStatementListContext ctx)
		{
			SwitchCase item = this.GetNodeValue<SwitchCase> (ctx.item);
			SetNodeValue (ctx, new SwitchCaseList (item));
		}

		
		public override void ExitSwitchCaseStatementListItem (SParser.SwitchCaseStatementListItemContext ctx)
		{
			SwitchCase item = this.GetNodeValue<SwitchCase> (ctx.item);
			SwitchCaseList items = this.GetNodeValue<SwitchCaseList> (ctx.items);
			items.Add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitSwitch_statement (SParser.Switch_statementContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SwitchCaseList cases = this.GetNodeValue<SwitchCaseList> (ctx.cases);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SwitchStatement stmt = new SwitchStatement (exp, cases, stmts);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitLiteralRangeLiteral (SParser.LiteralRangeLiteralContext ctx)
		{
			IExpression low = this.GetNodeValue<IExpression> (ctx.low);
			IExpression high = this.GetNodeValue<IExpression> (ctx.high);
			SetNodeValue (ctx, new RangeLiteral (low, high));
		}

		public override void ExitLiteralSetLiteral (SParser.LiteralSetLiteralContext ctx)
		{
			ExpressionList items = this.GetNodeValue<ExpressionList>(ctx.exp);
			SetNodeValue(ctx, new SetLiteral(items));
		}

		public override void ExitLiteralListLiteral (SParser.LiteralListLiteralContext ctx)
		{
			ExpressionList exp = this.GetNodeValue<ExpressionList> (ctx.exp);
			SetNodeValue (ctx, new ListLiteral (exp));
		}

		
		public override void ExitLiteralList (SParser.LiteralListContext ctx)
		{
			IExpression item = this.GetNodeValue<IExpression> (ctx.item);
			SetNodeValue (ctx, new ExpressionList (item));
		}

		
		public override void ExitLiteralListItem (SParser.LiteralListItemContext ctx)
		{
			ExpressionList items = this.GetNodeValue<ExpressionList> (ctx.items);
			IExpression item = this.GetNodeValue<IExpression> (ctx.item);
			items.add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitInExpression (SParser.InExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new ContainsExpression (left, ContOp.IN, right));
		}

		
		public override void ExitNotInExpression (SParser.NotInExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new ContainsExpression (left, ContOp.NOT_IN, right));
		}

		
		public override void ExitContainsAllExpression (SParser.ContainsAllExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new ContainsExpression (left, ContOp.CONTAINS_ALL, right));
		}

		
		public override void ExitNotContainsAllExpression (SParser.NotContainsAllExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new ContainsExpression (left, ContOp.NOT_CONTAINS_ALL, right));
		}

		
		public override void ExitContainsAnyExpression (SParser.ContainsAnyExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new ContainsExpression (left, ContOp.CONTAINS_ANY, right));
		}

		
		public override void ExitNotContainsAnyExpression (SParser.NotContainsAnyExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new ContainsExpression (left, ContOp.NOT_CONTAINS_ANY, right));
		}

		
		public override void ExitContainsExpression (SParser.ContainsExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new ContainsExpression (left, ContOp.CONTAINS, right));
		}

		
		public override void ExitNotContainsExpression (SParser.NotContainsExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new ContainsExpression (left, ContOp.NOT_CONTAINS, right));
		}

		
		public override void ExitDivideExpression (SParser.DivideExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new DivideExpression (left, right));
		}

		
		public override void ExitIntDivideExpression (SParser.IntDivideExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new IntDivideExpression (left, right));
		}

		
		public override void ExitModuloExpression (SParser.ModuloExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new ModuloExpression (left, right));
		}

		
		public override void ExitAndExpression (SParser.AndExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new AndExpression (left, right));
		}

		public override void ExitNullLiteral (SParser.NullLiteralContext ctx)
		{
			SetNodeValue (ctx, NullLiteral.Instance);
		}

		public override void ExitOperatorArgument(SParser.OperatorArgumentContext ctx) {
			bool mutable = ctx.MUTABLE () != null;
			IArgument arg = this.GetNodeValue<IArgument>(ctx.arg);
			arg.setMutable (mutable);
			SetNodeValue(ctx, arg);
		}

		public override void ExitOperatorPlus(SParser.OperatorPlusContext ctx) {
			SetNodeValue(ctx, Operator.PLUS);
		}

		public override void ExitOperatorMinus(SParser.OperatorMinusContext ctx) {
			SetNodeValue(ctx, Operator.MINUS);
		}

		public override void ExitOperatorMultiply(SParser.OperatorMultiplyContext ctx) {
			SetNodeValue(ctx, Operator.MULTIPLY);
		}

		public override void ExitOperatorDivide(SParser.OperatorDivideContext ctx) {
			SetNodeValue(ctx, Operator.DIVIDE);
		}

		public override void ExitOperatorIDivide(SParser.OperatorIDivideContext ctx) {
			SetNodeValue(ctx, Operator.IDIVIDE);
		}

		public override void ExitOperatorModulo(SParser.OperatorModuloContext ctx) {
			SetNodeValue(ctx, Operator.MODULO);
		}

		public override void ExitOperator_method_declaration(SParser.Operator_method_declarationContext ctx) {
			Operator op = this.GetNodeValue<Operator>(ctx.op);
			IArgument arg = this.GetNodeValue<IArgument>(ctx.arg);
			IType typ = this.GetNodeValue<IType>(ctx.typ);
			StatementList stmts = this.GetNodeValue<StatementList>(ctx.stmts);
			OperatorMethodDeclaration decl = new OperatorMethodDeclaration(op, arg, typ, stmts);
			SetNodeValue(ctx, decl);
		}

		public override void ExitOrder_by(SParser.Order_byContext ctx) {
			IdentifierList names = new IdentifierList();
			foreach(SParser.Variable_identifierContext ctx_ in ctx.variable_identifier())
				names.add(this.GetNodeValue<string>(ctx_));
			OrderByClause clause = new OrderByClause(names, ctx.DESC()!=null);
			SetNodeValue(ctx, clause);
		}

		public override void ExitOrder_by_list(SParser.Order_by_listContext ctx) {
			OrderByClauseList list = new OrderByClauseList();
			foreach(SParser.Order_byContext ctx_ in ctx.order_by())
				list.add(this.GetNodeValue<OrderByClause>(ctx_));
			SetNodeValue(ctx, list);
		}

		public override void ExitOrExpression (SParser.OrExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new OrExpression (left, right));
		}

		
		public override void ExitMultiplyExpression (SParser.MultiplyExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new MultiplyExpression (left, right));
		}

		
		public override void ExitMinusExpression (SParser.MinusExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new MinusExpression (exp));
		}

		
		public override void ExitNotExpression (SParser.NotExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new NotExpression (exp));
		}

		
		public override void ExitWhile_statement (SParser.While_statementContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new WhileStatement (exp, stmts));
		}

		
		public override void ExitDo_while_statement (SParser.Do_while_statementContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new DoWhileStatement (exp, stmts));
		}

		public override void ExitSingleton_category_declaration(SParser.Singleton_category_declarationContext ctx) {
			String name = this.GetNodeValue<String>(ctx.name);
			IdentifierList attrs = this.GetNodeValue<IdentifierList>(ctx.attrs);
			MethodDeclarationList methods = this.GetNodeValue<MethodDeclarationList>(ctx.methods);
			SetNodeValue(ctx, new SingletonCategoryDeclaration(name, attrs, methods));
		}

		public override void ExitSingletonCategoryDeclaration(SParser.SingletonCategoryDeclarationContext ctx) {
			IDeclaration decl = this.GetNodeValue<IDeclaration>(ctx.decl);
			SetNodeValue(ctx, decl);
		}

		public override void ExitSliceFirstAndLast (SParser.SliceFirstAndLastContext ctx)
		{
			IExpression first = this.GetNodeValue<IExpression> (ctx.first);
			IExpression last = this.GetNodeValue<IExpression> (ctx.last);
			SetNodeValue (ctx, new SliceSelector (first, last));
		}

		
		public override void ExitSliceFirstOnly (SParser.SliceFirstOnlyContext ctx)
		{
			IExpression first = this.GetNodeValue<IExpression> (ctx.first);
			SetNodeValue (ctx, new SliceSelector (first, null));
		}

		
		public override void ExitSliceLastOnly (SParser.SliceLastOnlyContext ctx)
		{
			IExpression last = this.GetNodeValue<IExpression> (ctx.last);
			SetNodeValue (ctx, new SliceSelector (null, last));
		}

		
		public override void ExitSorted_expression (SParser.Sorted_expressionContext ctx)
		{
			IExpression source = this.GetNodeValue<IExpression> (ctx.source);
			IExpression key = this.GetNodeValue<IExpression> (ctx.key);
			SetNodeValue (ctx, new SortedExpression (source, key));
		}

		
		public override void ExitSortedExpression (SParser.SortedExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitDocumentExpression (SParser.DocumentExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitDocument_expression (SParser.Document_expressionContext ctx)
		{
			SetNodeValue (ctx, new DocumentExpression ());
		}

		
		public override void ExitDocumentType (SParser.DocumentTypeContext ctx)
		{
			SetNodeValue (ctx, DocumentType.Instance);
		}

		
		public override void ExitFetchExpression (SParser.FetchExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitFetchList (SParser.FetchListContext ctx)
		{
			String itemName = this.GetNodeValue<String> (ctx.name);
			IExpression source = this.GetNodeValue<IExpression> (ctx.source);
			IExpression filter = this.GetNodeValue<IExpression> (ctx.xfilter);
			SetNodeValue (ctx, new FetchExpression (itemName, source, filter));
		}

		public override void ExitFetchOne (SParser.FetchOneContext ctx)
		{
			CategoryType category = this.GetNodeValue<CategoryType>(ctx.typ);
			IExpression filter = this.GetNodeValue<IExpression>(ctx.xfilter);
			SetNodeValue(ctx, new FetchOneExpression(category, filter));
		}

		public override void ExitFetchAll (SParser.FetchAllContext ctx)
		{
			CategoryType category = this.GetNodeValue<CategoryType>(ctx.typ);
			IExpression filter = this.GetNodeValue<IExpression>(ctx.xfilter);
			IExpression start = this.GetNodeValue<IExpression>(ctx.xstart);
			IExpression stop = this.GetNodeValue<IExpression>(ctx.xstop);
			OrderByClauseList orderBy = this.GetNodeValue<OrderByClauseList>(ctx.xorder);
			SetNodeValue(ctx, new FetchAllExpression(category, filter, start, stop, orderBy));
		}

		
		public override void ExitCode_type (SParser.Code_typeContext ctx)
		{
			SetNodeValue (ctx, CodeType.Instance);
		}

		
		public override void ExitExecuteExpression (SParser.ExecuteExpressionContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new ExecuteExpression (name));
		}

		
		public override void ExitCodeExpression (SParser.CodeExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new CodeExpression (exp));
		}

		
		public override void ExitCode_argument (SParser.Code_argumentContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new CodeArgument (name));
		}

		
		public override void ExitCategory_symbol (SParser.Category_symbolContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			ArgumentAssignmentList args = this.GetNodeValue<ArgumentAssignmentList> (ctx.args);
			SetNodeValue (ctx, new CategorySymbol (name, args));
		}

		
		public override void ExitCategorySymbolList (SParser.CategorySymbolListContext ctx)
		{
			CategorySymbol item = this.GetNodeValue<CategorySymbol> (ctx.item);
			SetNodeValue (ctx, new CategorySymbolList (item));
		}

		
		public override void ExitCategorySymbolListItem (SParser.CategorySymbolListItemContext ctx)
		{
			CategorySymbol item = this.GetNodeValue<CategorySymbol> (ctx.item);
			CategorySymbolList items = this.GetNodeValue<CategorySymbolList> (ctx.items);
			items.add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitEnum_category_declaration (SParser.Enum_category_declarationContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			IdentifierList attrs = this.GetNodeValue<IdentifierList> (ctx.attrs);
			String parent = this.GetNodeValue<String> (ctx.derived);
			IdentifierList derived = parent == null ? null : new IdentifierList (parent);
			CategorySymbolList symbols = this.GetNodeValue<CategorySymbolList> (ctx.symbols);
			SetNodeValue (ctx, new EnumeratedCategoryDeclaration (name, attrs, derived, symbols));
		}

		
		public override void ExitRead_expression (SParser.Read_expressionContext ctx)
		{
			IExpression source = this.GetNodeValue<IExpression> (ctx.source);
			SetNodeValue (ctx, new ReadExpression (source));
		}

		
		public override void ExitReadExpression (SParser.ReadExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitWrite_statement (SParser.Write_statementContext ctx)
		{
			IExpression what = this.GetNodeValue<IExpression> (ctx.what);
			IExpression target = this.GetNodeValue<IExpression> (ctx.target);
			SetNodeValue (ctx, new WriteStatement (what, target));
		}

		
		public override void ExitWith_resource_statement (SParser.With_resource_statementContext ctx)
		{
			AssignVariableStatement stmt = this.GetNodeValue<AssignVariableStatement> (ctx.stmt);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new WithResourceStatement (stmt, stmts));
		}

		
		public override void ExitAnyType (SParser.AnyTypeContext ctx)
		{
			SetNodeValue (ctx, AnyType.Instance);
		}

		
		public override void ExitAnyListType (SParser.AnyListTypeContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.typ);
			SetNodeValue (ctx, new ListType (type));
		}

		
		public override void ExitAnyDictType (SParser.AnyDictTypeContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.typ);
			SetNodeValue (ctx, new DictType (type));
		}

		
		public override void ExitAnyArgumentType (SParser.AnyArgumentTypeContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.typ);
			SetNodeValue (ctx, type);
		}

		public override void ExitCastExpression (SParser.CastExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IType type = this.GetNodeValue<IType> (ctx.right);
			SetNodeValue (ctx, new CastExpression (left, type));
		}

		public override void ExitCatchAtomicStatement (SParser.CatchAtomicStatementContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new AtomicSwitchCase (new SymbolExpression (name), stmts));
		}

		
		public override void ExitCatchCollectionStatement (SParser.CatchCollectionStatementContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new CollectionSwitchCase (exp, stmts));
		}

		
		public override void ExitCatchStatementList (SParser.CatchStatementListContext ctx)
		{
			SwitchCase item = this.GetNodeValue<SwitchCase> (ctx.item);
			SetNodeValue (ctx, new SwitchCaseList (item));
		}

		
		public override void ExitCatchStatementListItem (SParser.CatchStatementListItemContext ctx)
		{
			SwitchCase item = this.GetNodeValue<SwitchCase> (ctx.item);
			SwitchCaseList items = this.GetNodeValue<SwitchCaseList> (ctx.items);
			items.Add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitTry_statement (SParser.Try_statementContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SwitchCaseList handlers = this.GetNodeValue<SwitchCaseList> (ctx.handlers);
			StatementList anyStmts = this.GetNodeValue<StatementList> (ctx.anyStmts);
			StatementList finalStmts = this.GetNodeValue<StatementList> (ctx.finalStmts);
			SwitchErrorStatement stmt = new SwitchErrorStatement (name, stmts, handlers, anyStmts, finalStmts);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitRaise_statement (SParser.Raise_statementContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new RaiseStatement (exp));
		}

		
		public override void ExitMatchingList (SParser.MatchingListContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.source);
			SetNodeValue (ctx, new MatchingCollectionConstraint (exp));
		}

		
		public override void ExitMatchingRange (SParser.MatchingRangeContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.source);
			SetNodeValue (ctx, new MatchingCollectionConstraint (exp));
		}

		
		public override void ExitMatchingExpression (SParser.MatchingExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new MatchingExpressionConstraint (exp));
		}

		
		public override void ExitMatchingPattern (SParser.MatchingPatternContext ctx)
		{
			SetNodeValue (ctx, new MatchingPatternConstraint (new TextLiteral (ctx.text.Text)));
		}

		public override void ExitMatchingSet (SParser.MatchingSetContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression>(ctx.source);
			SetNodeValue(ctx, new MatchingCollectionConstraint(exp));
		}

		public override void ExitCsharp_item_expression (SParser.Csharp_item_expressionContext ctx)
		{
			CSharpExpression exp = this.GetNodeValue<CSharpExpression> (ctx.exp);
			SetNodeValue (ctx, new CSharpItemExpression (exp));
		}

		public override void ExitCsharp_method_expression (SParser.Csharp_method_expressionContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			CSharpExpressionList args = this.GetNodeValue<CSharpExpressionList> (ctx.args);
			SetNodeValue (ctx, new CSharpMethodExpression (name, args));
		}

		public override void ExitCSharpArgumentList (SParser.CSharpArgumentListContext ctx)
		{
			CSharpExpression item = this.GetNodeValue<CSharpExpression> (ctx.item);
			SetNodeValue (ctx, new CSharpExpressionList (item));
		}

		public override void ExitCSharpArgumentListItem (SParser.CSharpArgumentListItemContext ctx)
		{
			CSharpExpression item = this.GetNodeValue<CSharpExpression> (ctx.item);
			CSharpExpressionList items = this.GetNodeValue<CSharpExpressionList> (ctx.items);
			items.Add (item);
			SetNodeValue (ctx, items);
		}

		public override void ExitCSharpItemExpression (SParser.CSharpItemExpressionContext ctx)
		{
			CSharpExpression exp = this.GetNodeValue<CSharpExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		public override void ExitCSharpMethodExpression (SParser.CSharpMethodExpressionContext ctx)
		{
			CSharpExpression exp = this.GetNodeValue<CSharpExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		public override void ExitCSharpSelectorExpression (SParser.CSharpSelectorExpressionContext ctx)
		{
			CSharpExpression parent = this.GetNodeValue<CSharpExpression> (ctx.parent);
			CSharpSelectorExpression child = this.GetNodeValue<CSharpSelectorExpression> (ctx.child);
			child.SetParent (parent);
			SetNodeValue (ctx, child);
		}

		public override void ExitCsharp_primary_expression (SParser.Csharp_primary_expressionContext ctx)
		{
			CSharpExpression exp = this.GetNodeValue<CSharpExpression> (ctx.GetChild(0));
			SetNodeValue (ctx, exp);
		}

		public override void ExitCsharp_this_expression (SParser.Csharp_this_expressionContext ctx)
		{
			SetNodeValue (ctx, new CSharpThisExpression());
		}

			
		public override void ExitJavascript_category_binding (SParser.Javascript_category_bindingContext ctx)
		{
			String identifier = ctx.identifier ().GetText ();
			JavaScriptModule module = this.GetNodeValue<JavaScriptModule> (ctx.javascript_module ());
			JavaScriptNativeCategoryBinding map = new JavaScriptNativeCategoryBinding (identifier, module);
			SetNodeValue (ctx, map);
		}

		public override void ExitJavaScriptMemberExpression (SParser.JavaScriptMemberExpressionContext ctx)
		{
			String name = ctx.name.GetText ();
			SetNodeValue (ctx, new JavaScriptMemberExpression(name));
		}

		public override void ExitJavascript_primary_expression (SParser.Javascript_primary_expressionContext ctx)
		{
			JavaScriptExpression exp = this.GetNodeValue<JavaScriptExpression> (ctx.GetChild(0));
			SetNodeValue (ctx, exp);
		}

		public override void ExitJavaScriptMethodExpression (SParser.JavaScriptMethodExpressionContext ctx)
		{
			JavaScriptExpression method = this.GetNodeValue<JavaScriptExpression> (ctx.method);
			SetNodeValue (ctx, method);
		}

		public override void ExitJavascript_this_expression (SParser.Javascript_this_expressionContext ctx)
		{
			SetNodeValue (ctx, new JavaScriptThisExpression ());
		}


		public override void ExitJavascript_identifier (SParser.Javascript_identifierContext ctx)
		{
			String name = ctx.GetText ();
			SetNodeValue (ctx, name);
		}

		
		public override void ExitJavascript_method_expression (SParser.Javascript_method_expressionContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			JavaScriptMethodExpression method = new JavaScriptMethodExpression (name);
			JavaScriptExpressionList args = this.GetNodeValue<JavaScriptExpressionList> (ctx.args);
			method.setArguments (args);
			SetNodeValue (ctx, method);
		}

		public override void ExitJavascriptDecimalLiteral (SParser.JavascriptDecimalLiteralContext ctx)
		{
			String text = ctx.t.Text;
			SetNodeValue (ctx, new JavaScriptDecimalLiteral (text));		
		}

		public override void ExitJavascriptTextLiteral (SParser.JavascriptTextLiteralContext ctx)
		{
			String text = ctx.t.Text;
			SetNodeValue (ctx, new JavaScriptTextLiteral (text));		
		}

		public override void ExitJavascriptIntegerLiteral (SParser.JavascriptIntegerLiteralContext ctx)
		{
			String text = ctx.t.Text;
			SetNodeValue (ctx, new JavaScriptIntegerLiteral (text));		
		}

		public override void ExitJavascript_module (SParser.Javascript_moduleContext ctx)
		{
			List<String> ids = new List<String> ();
			foreach (SParser.Javascript_identifierContext ic in ctx.javascript_identifier())
				ids.Add (ic.GetText ());
			JavaScriptModule module = new JavaScriptModule (ids);
			SetNodeValue (ctx, module);
		}

		
		public override void ExitJavascript_native_statement (SParser.Javascript_native_statementContext ctx)
		{
			JavaScriptStatement stmt = this.GetNodeValue<JavaScriptStatement> (ctx.stmt);
			JavaScriptModule module = this.GetNodeValue<JavaScriptModule> (ctx.module);
			stmt.setModule (module);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitJavascriptArgumentList (SParser.JavascriptArgumentListContext ctx)
		{
			JavaScriptExpression exp = this.GetNodeValue<JavaScriptExpression> (ctx.item);
			JavaScriptExpressionList list = new JavaScriptExpressionList (exp);
			SetNodeValue (ctx, list);
		}

		
		public override void ExitJavascriptArgumentListItem (SParser.JavascriptArgumentListItemContext ctx)
		{
			JavaScriptExpression exp = this.GetNodeValue<JavaScriptExpression> (ctx.item);
			JavaScriptExpressionList list = this.GetNodeValue<JavaScriptExpressionList> (ctx.items);
			list.Add (exp);
			SetNodeValue (ctx, list);
		}

		public override void ExitJavascriptBooleanLiteral (SParser.JavascriptBooleanLiteralContext ctx)
		{
			SetNodeValue (ctx, new JavaScriptBooleanLiteral (ctx.t.Text));
		}

		
		public override void ExitJavaScriptCategoryBinding (SParser.JavaScriptCategoryBindingContext ctx)
		{
			SetNodeValue (ctx, this.GetNodeValue<Object> (ctx.binding));
		}

		public override void ExitJavascriptCharacterLiteral (SParser.JavascriptCharacterLiteralContext ctx)
		{
			String text = ctx.t.Text;
			SetNodeValue (ctx, new JavaScriptCharacterLiteral (text));		
		}


		public override void ExitJavascript_identifier_expression (SParser.Javascript_identifier_expressionContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new JavaScriptIdentifierExpression (name));
		}


		public override void ExitJavaScriptNativeStatement (SParser.JavaScriptNativeStatementContext ctx)
		{
			JavaScriptStatement stmt = this.GetNodeValue<JavaScriptStatement> (ctx.stmt);
			SetNodeValue (ctx, new JavaScriptNativeCall (stmt));
		}

		
		public override void ExitJavascriptPrimaryExpression (SParser.JavascriptPrimaryExpressionContext ctx)
		{
			JavaScriptExpression exp = this.GetNodeValue<JavaScriptExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitJavascriptReturnStatement (SParser.JavascriptReturnStatementContext ctx)
		{
			JavaScriptExpression exp = this.GetNodeValue<JavaScriptExpression> (ctx.exp);
			SetNodeValue (ctx, new JavaScriptStatement (exp, true));
		}

		
		public override void ExitJavascriptSelectorExpression (SParser.JavascriptSelectorExpressionContext ctx)
		{
			JavaScriptExpression parent = this.GetNodeValue<JavaScriptExpression> (ctx.parent);
			JavaScriptSelectorExpression child = this.GetNodeValue<JavaScriptSelectorExpression> (ctx.child);
			child.setParent (parent);
			SetNodeValue (ctx, child);
		}

		
		public override void ExitJavascriptStatement (SParser.JavascriptStatementContext ctx)
		{
			JavaScriptExpression exp = this.GetNodeValue<JavaScriptExpression> (ctx.exp);
			SetNodeValue (ctx, new JavaScriptStatement (exp, false));
		}


		
		public override void ExitPython_category_binding (SParser.Python_category_bindingContext ctx)
		{
			String identifier = ctx.identifier ().GetText ();
			PythonModule module = this.GetNodeValue<PythonModule> (ctx.python_module ());
			PythonNativeCategoryBinding map = new PythonNativeCategoryBinding (identifier, module);
			SetNodeValue (ctx, map);
		}

		 
		public override void ExitPythonGlobalMethodExpression (SParser.PythonGlobalMethodExpressionContext ctx)
		{
			PythonMethodExpression exp = this.GetNodeValue<PythonMethodExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitPython_method_expression (SParser.Python_method_expressionContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			PythonArgumentList args = this.GetNodeValue<PythonArgumentList> (ctx.args);
			PythonMethodExpression method = new PythonMethodExpression (name);
			method.setArguments (args);
			SetNodeValue (ctx, method);
		}

		
		public override void ExitPythonIdentifierExpression (SParser.PythonIdentifierExpressionContext ctx)
		{
			PythonIdentifierExpression exp = this.GetNodeValue<PythonIdentifierExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitPythonNamedArgumentList (SParser.PythonNamedArgumentListContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			PythonExpression exp = this.GetNodeValue<PythonExpression> (ctx.exp);
			PythonNamedArgument arg = new PythonNamedArgument (name, exp);
			SetNodeValue (ctx, new PythonArgumentList (arg));
		}

		
		public override void ExitPythonNamedArgumentListItem (SParser.PythonNamedArgumentListItemContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			PythonExpression exp = this.GetNodeValue<PythonExpression> (ctx.exp);
			PythonNamedArgument arg = new PythonNamedArgument (name, exp);
			PythonArgumentList items = this.GetNodeValue<PythonArgumentList> (ctx.items);
			items.Add (arg);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitPythonSelectorExpression (SParser.PythonSelectorExpressionContext ctx)
		{
			PythonExpression parent = this.GetNodeValue<PythonExpression> (ctx.parent);
			PythonSelectorExpression selector = this.GetNodeValue<PythonSelectorExpression> (ctx.child);
			selector.setParent (parent);
			SetNodeValue (ctx, selector);
		}

		
		public override void ExitPythonArgumentList (SParser.PythonArgumentListContext ctx)
		{
			PythonArgumentList ordinal = this.GetNodeValue<PythonArgumentList> (ctx.ordinal);
			PythonArgumentList named = this.GetNodeValue<PythonArgumentList> (ctx.named);
			ordinal.AddRange (named);
			SetNodeValue (ctx, ordinal);
		}

		
		public override void ExitPythonMethodExpression (SParser.PythonMethodExpressionContext ctx)
		{
			PythonMethodExpression exp = this.GetNodeValue<PythonMethodExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitPythonNamedOnlyArgumentList (SParser.PythonNamedOnlyArgumentListContext ctx)
		{
			PythonArgumentList named = this.GetNodeValue<PythonArgumentList> (ctx.named);
			SetNodeValue (ctx, named);
		}

		public override void ExitPythonOrdinalArgumentList (SParser.PythonOrdinalArgumentListContext ctx)
		{
			PythonExpression exp = this.GetNodeValue<PythonExpression> (ctx.item);
			PythonOrdinalArgument arg = new PythonOrdinalArgument (exp);
			SetNodeValue (ctx, new PythonArgumentList (arg));
		}


		public override void ExitPythonOrdinalArgumentListItem (SParser.PythonOrdinalArgumentListItemContext ctx)
		{
			PythonExpression exp = this.GetNodeValue<PythonExpression> (ctx.item);
			PythonOrdinalArgument arg = new PythonOrdinalArgument (exp);
			PythonArgumentList items = this.GetNodeValue<PythonArgumentList> (ctx.items);
			items.Add (arg);
			SetNodeValue (ctx, items);
		}

		public override void ExitPythonOrdinalOnlyArgumentList (SParser.PythonOrdinalOnlyArgumentListContext ctx)
		{
			PythonArgumentList ordinal = this.GetNodeValue<PythonArgumentList> (ctx.ordinal);
			SetNodeValue (ctx, ordinal);
		}

	}
}
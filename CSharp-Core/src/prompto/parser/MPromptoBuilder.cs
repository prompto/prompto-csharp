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
using prompto.argument;
using prompto.constraint;
using prompto.instance;
using prompto.jsx;
using prompto.css;
using System.Text;

namespace prompto.parser
{

	public class MPromptoBuilder : MParserBaseListener
	{

		ParseTreeProperty<object> nodeValues = new ParseTreeProperty<object> ();
		ITokenStream input;
		string path = "";

		public MPromptoBuilder (MCleverParser parser)
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
			section.SetFrom (path, first, last, Dialect.M);
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


		public override void ExitFlush_statement (MParser.Flush_statementContext ctx)
		{
			SetNodeValue (ctx, new FlushStatement());
		}


		public override void ExitFlushStatement (MParser.FlushStatementContext ctx)
		{
			SetNodeValue (ctx, GetNodeValue<IStatement> (ctx.stmt));
		}

		
		public override void ExitFullDeclarationList (MParser.FullDeclarationListContext ctx)
		{
			DeclarationList items = this.GetNodeValue<DeclarationList> (ctx.declarations());
			if (items == null)
				items = new DeclarationList ();
			SetNodeValue (ctx, items);
		}

		
		public override void ExitSelectorExpression (MParser.SelectorExpressionContext ctx)
		{
			IExpression parent = this.GetNodeValue<IExpression> (ctx.parent);
			SelectorExpression selector = this.GetNodeValue<SelectorExpression> (ctx.selector);
			selector.setParent (parent);
			SetNodeValue (ctx, selector);
		}

		
		public override void ExitSelectableExpression (MParser.SelectableExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.parent);
			SetNodeValue (ctx, exp);
		}

		public override void ExitSet_literal (MParser.Set_literalContext ctx)
		{
			ExpressionList items = this.GetNodeValue<ExpressionList>(ctx.expression_list());
			SetLiteral set = items==null ? new SetLiteral() : new SetLiteral(items);
			SetNodeValue(ctx, set);
		}

		public override void ExitSetType(MParser.SetTypeContext ctx) {
			IType itemType = this.GetNodeValue<IType>(ctx.s);
			SetNodeValue(ctx, new SetType(itemType));
		}


		public override void ExitBlob_expression(MParser.Blob_expressionContext ctx) {
			IExpression exp = this.GetNodeValue<IExpression>(ctx.expression());
			SetNodeValue(ctx, new BlobExpression(exp));
		}


		public override void ExitBlobType(MParser.BlobTypeContext ctx) {
			SetNodeValue(ctx, BlobType.Instance);
		}

		public override void ExitBooleanLiteral (MParser.BooleanLiteralContext ctx)
		{
			SetNodeValue (ctx, new BooleanLiteral (ctx.t.Text));
		}


		public override void ExitBreakStatement(MParser.BreakStatementContext ctx)
		{
			SetNodeValue(ctx, new BreakStatement());
		}


		public override void ExitMinIntegerLiteral (MParser.MinIntegerLiteralContext ctx)
		{
			SetNodeValue (ctx, new MinIntegerLiteral ());
		}

		
		public override void ExitMaxIntegerLiteral (MParser.MaxIntegerLiteralContext ctx)
		{
			SetNodeValue (ctx, new MaxIntegerLiteral ());
		}

		
		public override void ExitIntegerLiteral (MParser.IntegerLiteralContext ctx)
		{
			SetNodeValue (ctx, new IntegerLiteral (ctx.t.Text));
		}

		
		public override void ExitDecimalLiteral (MParser.DecimalLiteralContext ctx)
		{
			SetNodeValue (ctx, new DecimalLiteral (ctx.t.Text));
		}

		
		public override void ExitHexadecimalLiteral (MParser.HexadecimalLiteralContext ctx)
		{
			SetNodeValue (ctx, new HexaLiteral (ctx.t.Text));
		}

		
		public override void ExitCharacterLiteral (MParser.CharacterLiteralContext ctx)
		{
			SetNodeValue (ctx, new CharacterLiteral (ctx.t.Text));
		}

		
		public override void ExitDateLiteral (MParser.DateLiteralContext ctx)
		{
			SetNodeValue (ctx, new DateLiteral (ctx.t.Text));
		}

		
		public override void ExitDateTimeLiteral (MParser.DateTimeLiteralContext ctx)
		{
			SetNodeValue (ctx, new DateTimeLiteral (ctx.t.Text));
		}

		public override void ExitTernaryExpression (MParser.TernaryExpressionContext ctx)
		{
			IExpression condition = this.GetNodeValue<IExpression> (ctx.test);
			IExpression ifTrue = this.GetNodeValue<IExpression> (ctx.ifTrue);
			IExpression ifFalse = this.GetNodeValue<IExpression> (ctx.ifFalse);
			TernaryExpression exp = new TernaryExpression (condition, ifTrue, ifFalse);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitTest_method_declaration(MParser.Test_method_declarationContext ctx) {
			String name = ctx.name.Text;
			StatementList stmts = this.GetNodeValue<StatementList>(ctx.stmts);
			ExpressionList exps = this.GetNodeValue<ExpressionList>(ctx.exps);
			String errorName = this.GetNodeValue<String>(ctx.error);
			SymbolExpression error = errorName==null ? null : new SymbolExpression(errorName);
			SetNodeValue(ctx, new TestMethodDeclaration(name, stmts, exps, error));
		}

		public override void ExitTextLiteral (MParser.TextLiteralContext ctx)
		{
			SetNodeValue (ctx, new TextLiteral (ctx.t.Text));
		}

		
		public override void ExitTimeLiteral (MParser.TimeLiteralContext ctx)
		{
			SetNodeValue (ctx, new TimeLiteral (ctx.t.Text));
		}

		
		public override void ExitPeriodLiteral (MParser.PeriodLiteralContext ctx)
		{
			SetNodeValue (ctx, new PeriodLiteral (ctx.t.Text));
		}


		public override void ExitPeriodType(MParser.PeriodTypeContext ctx)
		{
			SetNodeValue(ctx, PeriodType.Instance);
		}


		public override void ExitVersionLiteral(MParser.VersionLiteralContext ctx)
		{
			SetNodeValue(ctx, new VersionLiteral(ctx.t.Text));
		}


		public override void ExitVersionType(MParser.VersionTypeContext ctx)
		{
			SetNodeValue(ctx, VersionType.Instance);	
		}


		public override void ExitAttribute_identifier (MParser.Attribute_identifierContext ctx)
		{
			SetNodeValue (ctx, ctx.GetText ());
		}


		public override void ExitUUIDLiteral(MParser.UUIDLiteralContext ctx)
		{
			SetNodeValue(ctx, new UUIDLiteral(ctx.t.Text));
		}


		public override void ExitUUIDType(MParser.UUIDTypeContext ctx)
		{
			SetNodeValue(ctx, UUIDType.Instance);
		}



		public override void ExitVariable_identifier (MParser.Variable_identifierContext ctx)
		{
			SetNodeValue (ctx, ctx.GetText ());
		}

		
		public override void ExitList_literal (MParser.List_literalContext ctx)
		{
			bool mutable = ctx.MUTABLE () != null;
			ExpressionList items = this.GetNodeValue<ExpressionList> (ctx.expression_list());
			IExpression value = items == null ? new ListLiteral (mutable) : new ListLiteral (items, mutable);
			SetNodeValue (ctx, value);
		}

		
		public override void ExitDict_literal (MParser.Dict_literalContext ctx)
		{
			bool mutable = ctx.MUTABLE () != null;
			DictEntryList items = this.GetNodeValue<DictEntryList> (ctx.dict_entry_list());
			IExpression value = items == null ? new DictLiteral (mutable) : new DictLiteral (items, mutable);
			SetNodeValue (ctx, value);
		}

		
		public override void ExitTuple_literal (MParser.Tuple_literalContext ctx)
		{
			bool mutable = ctx.MUTABLE() != null;
			ExpressionList items = this.GetNodeValue<ExpressionList> (ctx.expression_tuple());
			IExpression value = items == null ? new TupleLiteral (mutable) : new TupleLiteral (items, mutable);
			SetNodeValue (ctx, value);
		}

		
		public override void ExitRange_literal (MParser.Range_literalContext ctx)
		{
			IExpression low = this.GetNodeValue<IExpression> (ctx.low);
			IExpression high = this.GetNodeValue<IExpression> (ctx.high);
			SetNodeValue (ctx, new RangeLiteral (low, high));
		}

		
		public override void ExitDict_entry_list (MParser.Dict_entry_listContext ctx)
		{
			DictEntryList items = new DictEntryList ();
			foreach (ParserRuleContext entry in ctx.dict_entry()) {
				DictEntry item = this.GetNodeValue<DictEntry> (entry);
				items.add (item);
			}
			SetNodeValue (ctx, items);
		}
		
		public override void ExitDict_entry (MParser.Dict_entryContext ctx)
		{
			IExpression key = this.GetNodeValue<IExpression> (ctx.key);
			IExpression value = this.GetNodeValue<IExpression> (ctx.value);
			DictEntry entry = new DictEntry (key, value);
			SetNodeValue (ctx, entry);
		}

		
		public override void ExitLiteralExpression (MParser.LiteralExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitIdentifierExpression (MParser.IdentifierExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitVariableIdentifier (MParser.VariableIdentifierContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.variable_identifier());
			SetNodeValue (ctx, new InstanceExpression (name));
		}

		
		public override void ExitInstanceExpression (MParser.InstanceExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitExpression_list (MParser.Expression_listContext ctx)
		{
			ExpressionList items = new ExpressionList ();
			foreach (ParserRuleContext rule in ctx.expression()) {
				IExpression item = this.GetNodeValue<IExpression> (rule);
				items.add (item);
			}
			SetNodeValue (ctx, items);
		}


		public override void ExitExpression_tuple (MParser.Expression_tupleContext ctx)
		{
			ExpressionList items = new ExpressionList ();
			foreach (ParserRuleContext rule in ctx.expression()) {
				IExpression item = this.GetNodeValue<IExpression> (rule);
				items.add (item);
			}
			SetNodeValue (ctx, items);
		}

		
		public override void ExitSymbol_identifier (MParser.Symbol_identifierContext ctx)
		{
			SetNodeValue (ctx, ctx.GetText ());
		}

		
		public override void ExitNative_symbol (MParser.Native_symbolContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new NativeSymbol (name, exp));
		}

		public override void ExitNative_member_method_declaration (MParser.Native_member_method_declarationContext ctx)
		{
			IDeclaration decl = this.GetNodeValue<IDeclaration>(ctx.GetChild(0));
			SetNodeValue(ctx, decl);
		}


		public override void ExitTypeIdentifier (MParser.TypeIdentifierContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.type_identifier());
			SetNodeValue (ctx, new UnresolvedIdentifier (name, Dialect.M));
		}

		
		public override void ExitSymbolIdentifier (MParser.SymbolIdentifierContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.symbol_identifier());
			SetNodeValue (ctx, new SymbolExpression (name));
		}


		
		public override void ExitBooleanType (MParser.BooleanTypeContext ctx)
		{
			SetNodeValue (ctx, BooleanType.Instance);
		}

		
		public override void ExitCharacterType (MParser.CharacterTypeContext ctx)
		{
			SetNodeValue (ctx, CharacterType.Instance);
		}

		
		public override void ExitTextType (MParser.TextTypeContext ctx)
		{
			SetNodeValue (ctx, TextType.Instance);
		}

		
		public override void ExitHtmlType(MParser.HtmlTypeContext ctx)
		{
			SetNodeValue(ctx, HtmlType.Instance);
		}

		public override void ExitThisExpression (MParser.ThisExpressionContext ctx)
		{
			SetNodeValue (ctx, new ThisExpression ());
		}

		public override void ExitIntegerType (MParser.IntegerTypeContext ctx)
		{
			SetNodeValue (ctx, IntegerType.Instance);
		}

		
		public override void ExitDecimalType (MParser.DecimalTypeContext ctx)
		{
			SetNodeValue (ctx, DecimalType.Instance);
		}

		
		public override void ExitDateType (MParser.DateTypeContext ctx)
		{
			SetNodeValue (ctx, DateType.Instance);
		}

		
		public override void ExitDateTimeType (MParser.DateTimeTypeContext ctx)
		{
			SetNodeValue (ctx, DateTimeType.Instance);
		}

		
		public override void ExitTimeType (MParser.TimeTypeContext ctx)
		{
			SetNodeValue (ctx, TimeType.Instance);
		}

		
		public override void ExitCodeType (MParser.CodeTypeContext ctx)
		{
			SetNodeValue (ctx, CodeType.Instance);
		}

		
		public override void ExitPrimaryType (MParser.PrimaryTypeContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.p);
			SetNodeValue (ctx, type);
		}

		
		public override void ExitAttribute_declaration (MParser.Attribute_declarationContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			IType type = this.GetNodeValue<IType> (ctx.typ);
			IAttributeConstraint match = this.GetNodeValue<IAttributeConstraint> (ctx.match);
			IdentifierList indices = ctx.index_clause()!=null ? 
				this.GetNodeValue<IdentifierList>(ctx.index_clause()) : null;
			AttributeDeclaration decl = new AttributeDeclaration (name, type, match, indices);
			decl.Storable = ctx.STORABLE () != null;
			SetNodeValue (ctx, decl);
		}


		public override void ExitIndex_clause(MParser.Index_clauseContext ctx) {
			IdentifierList indices = ctx.indices!=null ? 
				this.GetNodeValue<IdentifierList>(ctx.indices) :
				new IdentifierList();
			SetNodeValue(ctx, indices);
		}

		public override void ExitNativeType (MParser.NativeTypeContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.n);
			SetNodeValue (ctx, type);
		}

		
		public override void ExitCategoryType (MParser.CategoryTypeContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.c);
			SetNodeValue (ctx, type);
		}

		
		public override void ExitCategory_type (MParser.Category_typeContext ctx)
		{
			String name = ctx.GetText ();
			SetNodeValue (ctx, new CategoryType (name));
		}

		
		public override void ExitListType (MParser.ListTypeContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.l);
			SetNodeValue (ctx, new ListType (type));
		}

		
		public override void ExitDictType (MParser.DictTypeContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.d);
			SetNodeValue (ctx, new DictType (type));
		}

		
		public override void ExitConcrete_category_declaration (MParser.Concrete_category_declarationContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			IdentifierList attrs = this.GetNodeValue<IdentifierList> (ctx.attrs);
			IdentifierList derived = this.GetNodeValue<IdentifierList> (ctx.derived);
			MethodDeclarationList methods = this.GetNodeValue<MethodDeclarationList> (ctx.methods);
			ConcreteCategoryDeclaration decl = new ConcreteCategoryDeclaration (name, attrs, derived, methods);
			decl.Storable = ctx.STORABLE () != null;
			SetNodeValue (ctx, decl);
		}

		
		public override void ExitConcrete_widget_declaration(MParser.Concrete_widget_declarationContext ctx)
		{
			String name = this.GetNodeValue<String>(ctx.name);
			String derived = this.GetNodeValue<String>(ctx.derived);
			MethodDeclarationList methods = this.GetNodeValue<MethodDeclarationList>(ctx.methods);
			ConcreteWidgetDeclaration decl = new ConcreteWidgetDeclaration(name, derived, methods);
			SetNodeValue(ctx, decl);
		}

		public override void ExitConcreteCategoryDeclaration(MParser.ConcreteCategoryDeclarationContext ctx)
		{
			SetNodeValue(ctx, this.GetNodeValue<Object>(ctx.decl));
		}


		public override void ExitConcreteWidgetDeclaration(MParser.ConcreteWidgetDeclarationContext ctx)
		{
			SetNodeValue(ctx, this.GetNodeValue<Object>(ctx.decl));
		}


		public override void ExitNativeWidgetDeclaration(MParser.NativeWidgetDeclarationContext ctx)
		{
			SetNodeValue(ctx, this.GetNodeValue<Object>(ctx.decl));
		}


		public override void ExitDerived_list (MParser.Derived_listContext ctx)
		{
			IdentifierList items = this.GetNodeValue<IdentifierList> (ctx.items);
			SetNodeValue (ctx, items);
		}

		public override void ExitType_identifier (MParser.Type_identifierContext ctx)
		{
			SetNodeValue (ctx, ctx.GetText ());
		}

		
		public override void ExitType_identifier_list (MParser.Type_identifier_listContext ctx)
		{
			IdentifierList items = new IdentifierList ();
			foreach (ParserRuleContext rule in ctx.type_identifier()) {
				String item = this.GetNodeValue<String> (rule);
				items.Add (item);
			}
			SetNodeValue (ctx, items);
		}


		
		public override void ExitMemberSelector (MParser.MemberSelectorContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new MemberSelector (name));
		}

		
		public override void ExitIsATypeExpression(MParser.IsATypeExpressionContext ctx) {
			IType type = this.GetNodeValue<IType>(ctx.category_or_any_type());
			IExpression exp = new TypeExpression(type);
			SetNodeValue(ctx, exp);
		}

		public override void ExitIsOtherExpression(MParser.IsOtherExpressionContext ctx) {
			IExpression exp = this.GetNodeValue<IExpression>(ctx.expression());
			SetNodeValue(ctx, exp);
		}

		public override void ExitIsExpression(MParser.IsExpressionContext ctx) {
			IExpression left = this.GetNodeValue<IExpression>(ctx.left);
			IExpression right = this.GetNodeValue<IExpression>(ctx.right);
			EqOp op = right is TypeExpression ? EqOp.IS_A : EqOp.IS;
			SetNodeValue(ctx, new EqualsExpression(left, op, right));
		}

		public override void ExitIsNotExpression(MParser.IsNotExpressionContext ctx) {
			IExpression left = this.GetNodeValue<IExpression>(ctx.left);
			IExpression right = this.GetNodeValue<IExpression>(ctx.right);
			EqOp op = right is TypeExpression ? EqOp.IS_NOT_A : EqOp.IS_NOT;
			SetNodeValue(ctx, new EqualsExpression(left, op, right));
		}		


		
		public override void ExitItemSelector (MParser.ItemSelectorContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new ItemSelector (exp));
		}

		
		public override void ExitSliceSelector (MParser.SliceSelectorContext ctx)
		{
			IExpression slice = this.GetNodeValue<IExpression> (ctx.xslice);
			SetNodeValue (ctx, slice);
		}

		
		public override void ExitTyped_argument (MParser.Typed_argumentContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.typ);
			String name = this.GetNodeValue<String> (ctx.name);
			IdentifierList attrs = this.GetNodeValue<IdentifierList> (ctx.attrs);
			CategoryArgument arg = attrs == null ?
				new CategoryArgument (type, name) :
				new ExtendedArgument (type, name, attrs);
			IExpression exp = this.GetNodeValue<IExpression>(ctx.value);
			arg.DefaultValue = exp;
			SetNodeValue(ctx, arg);
		}

		
		public override void ExitCodeArgument (MParser.CodeArgumentContext ctx)
		{
			IArgument arg = this.GetNodeValue<IArgument> (ctx.arg);
			SetNodeValue (ctx, arg);
		}

		
		public override void ExitArgument_list (MParser.Argument_listContext ctx)
		{
			ArgumentList items = new ArgumentList ();
			foreach (ParserRuleContext rule in ctx.argument()) {
				IArgument item = this.GetNodeValue<IArgument> (rule); 
				items.Add (item);
			}
			SetNodeValue (ctx, items);
		}


		
		public override void ExitMethodName (MParser.MethodNameContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new UnresolvedIdentifier (name, Dialect.M));
		}

	
		
		public override void ExitMethodParent (MParser.MethodParentContext ctx)
		{
			IExpression parent = this.GetNodeValue<IExpression> (ctx.parent);
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new MethodSelector (parent, name));
		}

	
		
		public override void ExitMethod_call (MParser.Method_callContext ctx)
		{
			IExpression method = this.GetNodeValue<IExpression> (ctx.method);
			ArgumentAssignmentList args = this.GetNodeValue<ArgumentAssignmentList> (ctx.args);
			SetNodeValue (ctx, new UnresolvedCall (method, args));
		}

		public override void ExitExpressionAssignmentList (MParser.ExpressionAssignmentListContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			ArgumentAssignment item = new ArgumentAssignment (null, exp);
			ArgumentAssignmentList items = new ArgumentAssignmentList ();
			items.Add(item);
			SetNodeValue (ctx, items);
		}

		 
		public override void ExitArgument_assignment (MParser.Argument_assignmentContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			IArgument arg = new UnresolvedArgument (name);
			SetNodeValue (ctx, new ArgumentAssignment (arg, exp));
		}

		 
		public override void ExitArgumentAssignmentList (MParser.ArgumentAssignmentListContext ctx)
		{
			ArgumentAssignment item = this.GetNodeValue<ArgumentAssignment> (ctx.item);
			ArgumentAssignmentList items = new ArgumentAssignmentList ();
			items.Add(item);
			SetNodeValue (ctx, items);
		}

 
		
		public override void ExitArgumentAssignmentListItem (MParser.ArgumentAssignmentListItemContext ctx)
		{
			ArgumentAssignment item = this.GetNodeValue<ArgumentAssignment> (ctx.item);
			ArgumentAssignmentList items = this.GetNodeValue<ArgumentAssignmentList> (ctx.items);
			items.add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitCallableRoot (MParser.CallableRootContext ctx)
		{
			IExpression name = this.GetNodeValue<IExpression>(ctx.name);
			SetNodeValue(ctx, name);
		}

		
		public override void ExitCallableSelector (MParser.CallableSelectorContext ctx)
		{
			IExpression parent = this.GetNodeValue<IExpression> (ctx.parent);
			SelectorExpression select = this.GetNodeValue<SelectorExpression> (ctx.select);
			select.setParent (parent);
			SetNodeValue (ctx, select);
		}

		
		public override void ExitCallableMemberSelector (MParser.CallableMemberSelectorContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new MemberSelector (name));
		}

		
		public override void ExitCallableItemSelector (MParser.CallableItemSelectorContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new ItemSelector (exp));
		}

		
		public override void ExitAddExpression (MParser.AddExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			IExpression exp = ctx.op.Type == MParser.PLUS ? 
            (IExpression)new PlusExpression (left, right) 
            : (IExpression)new SubtractExpression (left, right);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitMember_method_declaration_list (MParser.Member_method_declaration_listContext ctx)
		{
			MethodDeclarationList items = new MethodDeclarationList ();
			foreach(ParserRuleContext rule in ctx.member_method_declaration()) {
				IMethodDeclaration item = this.GetNodeValue<IMethodDeclaration> (rule);
				items.add (item);
			}
			SetNodeValue (ctx, items);
		}

		public override void ExitNative_member_method_declaration_list (MParser.Native_member_method_declaration_listContext ctx)
		{
			MethodDeclarationList items = new MethodDeclarationList ();
			foreach(ParserRuleContext rule in ctx.native_member_method_declaration()) {
				IMethodDeclaration item = this.GetNodeValue<IMethodDeclaration> (rule);
				items.add (item);
			}
			SetNodeValue (ctx, items);
		}

		public override void ExitSetter_method_declaration (MParser.Setter_method_declarationContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new SetterMethodDeclaration (name, stmts));
		}

		
		public override void ExitGetter_method_declaration (MParser.Getter_method_declarationContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new GetterMethodDeclaration (name, stmts));
		}

		public override void ExitNative_setter_declaration (MParser.Native_setter_declarationContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new NativeSetterMethodDeclaration (name, stmts));
		}


		public override void ExitNative_getter_declaration (MParser.Native_getter_declarationContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new NativeGetterMethodDeclaration (name, stmts));
		}



		public override void ExitMember_method_declaration (MParser.Member_method_declarationContext ctx)
		{
			IDeclaration decl = this.GetNodeValue<IDeclaration> (ctx.GetChild(0));
			SetNodeValue (ctx, decl);
		}


		public override void ExitStatement_list (MParser.Statement_listContext ctx)
		{
			StatementList items = new StatementList ();
			foreach(ParserRuleContext rule in ctx.statement()) {
				IStatement item = this.GetNodeValue<IStatement> (rule);
				items.add (item);
			}
			SetNodeValue (ctx, items);
		}

		public override void ExitStoreStatement (MParser.StoreStatementContext ctx)
		{
			SetNodeValue (ctx, this.GetNodeValue<Object> (ctx.stmt));
		}

		public override void ExitStore_statement (MParser.Store_statementContext ctx)
		{
			ExpressionList del = this.GetNodeValue<ExpressionList>(ctx.to_del);
			ExpressionList add = this.GetNodeValue<ExpressionList>(ctx.to_add);
			StoreStatement stmt = new StoreStatement(del, add);
			SetNodeValue(ctx, stmt);
		}

		
		public override void ExitAbstract_method_declaration (MParser.Abstract_method_declarationContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.typ);
			String name = this.GetNodeValue<String> (ctx.name);
			ArgumentList args = this.GetNodeValue<ArgumentList> (ctx.args);
			SetNodeValue (ctx, new AbstractMethodDeclaration (name, args, type));
		}

		
		public override void ExitConcrete_method_declaration (MParser.Concrete_method_declarationContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.typ);
			String name = this.GetNodeValue<String> (ctx.name);
			ArgumentList args = this.GetNodeValue<ArgumentList> (ctx.args);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new ConcreteMethodDeclaration (name, args, type, stmts));
		}


		public override void ExitMethod_expression (MParser.Method_expressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.GetChild(0));
			SetNodeValue (ctx, exp);
		}


		public override void ExitMethodCallStatement (MParser.MethodCallStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitConstructorFrom(MParser.ConstructorFromContext ctx)
		{
			CategoryType type = this.GetNodeValue<CategoryType>(ctx.typ);
			IExpression copyFrom = this.GetNodeValue<IExpression>(ctx.copyExp);
			ArgumentAssignmentList args = this.GetNodeValue<ArgumentAssignmentList>(ctx.args);
			SetNodeValue(ctx, new ConstructorExpression(type, copyFrom, args, true));
		}

		public override void ExitConstructorNoFrom(MParser.ConstructorNoFromContext ctx)
		{
			CategoryType type = this.GetNodeValue<CategoryType>(ctx.typ);
			ArgumentAssignmentList args = this.GetNodeValue<ArgumentAssignmentList>(ctx.args);
			SetNodeValue(ctx, new ConstructorExpression(type, null, args, true));
		}


		public override void ExitCopy_from(MParser.Copy_fromContext ctx)
		{
			SetNodeValue(ctx, this.GetNodeValue<IExpression>(ctx.exp));
		}


		public override void ExitAssertion(MParser.AssertionContext ctx) {
			IExpression exp = this.GetNodeValue<IExpression>(ctx.exp);
			SetNodeValue(ctx, exp);
		}

		public override void ExitAssertion_list (MParser.Assertion_listContext ctx)
		{
			ExpressionList items = new ExpressionList();
			foreach(ParserRuleContext rule in ctx.assertion()) {
				IExpression item = this.GetNodeValue<IExpression>(rule);
				items.add(item);
			}
			SetNodeValue(ctx, items);
		}


		public override void ExitAssign_instance_statement (MParser.Assign_instance_statementContext ctx)
		{
			IAssignableInstance inst = this.GetNodeValue<IAssignableInstance> (ctx.inst);
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new AssignInstanceStatement (inst, exp));
		}

		
		public override void ExitAssignInstanceStatement (MParser.AssignInstanceStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitAssign_variable_statement (MParser.Assign_variable_statementContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.variable_identifier());
			IExpression exp = this.GetNodeValue<IExpression> (ctx.expression());
			SetNodeValue (ctx, new AssignVariableStatement (name, exp));
		}

		
		public override void ExitAssign_tuple_statement (MParser.Assign_tuple_statementContext ctx)
		{
			IdentifierList items = this.GetNodeValue<IdentifierList> (ctx.items);
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new AssignTupleStatement (items, exp));
		}

		
		public override void ExitAttribute_identifier_list (MParser.Attribute_identifier_listContext ctx)
		{
			IdentifierList list = new IdentifierList ();
			foreach(MParser.Attribute_identifierContext c in ctx.attribute_identifier())
			{
				String item = this.GetNodeValue<String> (c);
				list.Add (item);
			}
			SetNodeValue (ctx, list);
		}

		public override void ExitVariable_identifier_list (MParser.Variable_identifier_listContext ctx)
		{
			IdentifierList list = new IdentifierList ();
			foreach(MParser.Variable_identifierContext c in ctx.variable_identifier())
			{
				String item = this.GetNodeValue<String> (c);
				list.Add (item);
			}
			SetNodeValue (ctx, list);
		}


		
		public override void ExitRootInstance (MParser.RootInstanceContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.variable_identifier());
			SetNodeValue (ctx, new VariableInstance (name));
		}

		public override void ExitRoughlyEqualsExpression (MParser.RoughlyEqualsExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new EqualsExpression (left, EqOp.ROUGHLY, right));
		}

		
		public override void ExitChildInstance (MParser.ChildInstanceContext ctx)
		{
			IAssignableInstance parent = this.GetNodeValue<IAssignableInstance> (ctx.assignable_instance());
			IAssignableSelector child = this.GetNodeValue<IAssignableSelector> (ctx.child_instance());
			child.SetParent (parent);
			SetNodeValue (ctx, child);
		}

		
		public override void ExitMemberInstance (MParser.MemberInstanceContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new MemberInstance (name));
		}

		
		public override void ExitItemInstance (MParser.ItemInstanceContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new ItemInstance (exp));
		}

		
		public override void ExitMethodExpression (MParser.MethodExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitNative_statement_list (MParser.Native_statement_listContext ctx)
		{
			StatementList items = new StatementList ();
			foreach(ParserRuleContext rule in ctx.native_statement()) {
				IStatement item = this.GetNodeValue<IStatement> (rule);
				items.add (item);
			}
			SetNodeValue (ctx, items);
		}


		
		public override void ExitIteratorExpression(MParser.IteratorExpressionContext ctx) 
		{
			IExpression exp = this.GetNodeValue<IExpression>(ctx.exp);
			string name = this.GetNodeValue<string>(ctx.name);
			IExpression source = this.GetNodeValue<IExpression>(ctx.source);
			SetNodeValue(ctx, new IteratorExpression(name, source, exp));
		}

		public override void ExitIteratorType (MParser.IteratorTypeContext ctx)
		{
			IType type = this.GetNodeValue<IType>(ctx.i);
			SetNodeValue(ctx, new IteratorType(type));
		}


		public override void ExitJava_identifier (MParser.Java_identifierContext ctx)
		{
			SetNodeValue (ctx, ctx.GetText ());
		}

		
		public override void ExitCsharp_identifier (MParser.Csharp_identifierContext ctx)
		{
			SetNodeValue (ctx, ctx.GetText ());
		}

		public override void ExitCSharpPromptoIdentifier (MParser.CSharpPromptoIdentifierContext ctx)
		{
			String name = ctx.DOLLAR_IDENTIFIER().GetText();
			SetNodeValue (ctx, new CSharpIdentifierExpression (name));
		}

	
		public override void ExitCSharpPrimaryExpression (MParser.CSharpPrimaryExpressionContext ctx)
		{
			CSharpExpression exp = this.GetNodeValue<CSharpExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitPython_identifier (MParser.Python_identifierContext ctx)
		{
			SetNodeValue (ctx, ctx.GetText ());
		}


		public override void ExitPythonPromptoIdentifier (MParser.PythonPromptoIdentifierContext ctx)
		{
			String name = ctx.DOLLAR_IDENTIFIER ().GetText ();
			SetNodeValue (ctx, new PythonIdentifierExpression(name));
		}
			
		
		public override void ExitPythonPrimaryExpression (MParser.PythonPrimaryExpressionContext ctx)
		{
			PythonExpression exp = this.GetNodeValue<PythonExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitJavaIdentifier (MParser.JavaIdentifierContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new JavaIdentifierExpression (name));
		}

		public override void ExitJava_primary_expression (MParser.Java_primary_expressionContext ctx)
		{
			JavaExpression exp = this.GetNodeValue<JavaExpression> (ctx.GetChild(0));
			SetNodeValue (ctx, exp);
		}
		
		public override void ExitJavaPrimaryExpression (MParser.JavaPrimaryExpressionContext ctx)
		{
			JavaExpression exp = this.GetNodeValue<JavaExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		public override void ExitJava_this_expression (MParser.Java_this_expressionContext ctx)
		{
			SetNodeValue (ctx, new JavaThisExpression());
		}



		public override void ExitPythonSelfExpression(MParser.PythonSelfExpressionContext ctx)
		{
			SetNodeValue(ctx, new PythonSelfExpression());
		}


		public override void ExitCSharpIdentifier (MParser.CSharpIdentifierContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new CSharpIdentifierExpression (name));
		}

		
		public override void ExitPythonIdentifier (MParser.PythonIdentifierContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new PythonIdentifierExpression (name));
		}

		
		public override void ExitJavaChildIdentifier (MParser.JavaChildIdentifierContext ctx)
		{
			JavaIdentifierExpression parent = this.GetNodeValue<JavaIdentifierExpression> (ctx.parent);
			String name = this.GetNodeValue<String> (ctx.name);
			JavaIdentifierExpression child = new JavaIdentifierExpression (parent, name);
			SetNodeValue (ctx, child);
		}

		
		public override void ExitCSharpChildIdentifier (MParser.CSharpChildIdentifierContext ctx)
		{
			CSharpIdentifierExpression parent = this.GetNodeValue<CSharpIdentifierExpression> (ctx.parent);
			String name = this.GetNodeValue<String> (ctx.name);
			CSharpIdentifierExpression child = new CSharpIdentifierExpression (parent, name);
			SetNodeValue (ctx, child);
		}

		
		public override void ExitPythonChildIdentifier (MParser.PythonChildIdentifierContext ctx)
		{
			PythonIdentifierExpression parent = this.GetNodeValue<PythonIdentifierExpression> (ctx.parent);
			String name = this.GetNodeValue<String> (ctx.name);
			PythonIdentifierExpression child = new PythonIdentifierExpression (parent, name);
			SetNodeValue (ctx, child);
		}

	
		
		public override void ExitJavaClassIdentifier (MParser.JavaClassIdentifierContext ctx)
		{
			JavaIdentifierExpression klass = this.GetNodeValue<JavaIdentifierExpression> (ctx.klass);
			SetNodeValue (ctx, klass);
		}

		
		public override void ExitJavaChildClassIdentifier (MParser.JavaChildClassIdentifierContext ctx)
		{
			JavaIdentifierExpression parent = this.GetNodeValue<JavaIdentifierExpression> (ctx.parent);
			JavaIdentifierExpression child = new JavaIdentifierExpression (parent, ctx.name.Text);
			SetNodeValue (ctx, child);
		}

		
		public override void ExitJavaSelectorExpression (MParser.JavaSelectorExpressionContext ctx)
		{
			JavaExpression parent = this.GetNodeValue<JavaExpression> (ctx.parent);
			JavaSelectorExpression child = this.GetNodeValue<JavaSelectorExpression> (ctx.child);
			child.SetParent (parent);
			SetNodeValue (ctx, child);
		}

		
		public override void ExitJavaStatement (MParser.JavaStatementContext ctx)
		{
			JavaExpression exp = this.GetNodeValue<JavaExpression> (ctx.exp);
			SetNodeValue (ctx, new JavaStatement (exp, false));
		}

		
		public override void ExitCSharpStatement (MParser.CSharpStatementContext ctx)
		{
			CSharpExpression exp = this.GetNodeValue<CSharpExpression> (ctx.exp);
			SetNodeValue (ctx, new CSharpStatement (exp, false));
		}

		
		public override void ExitPythonStatement (MParser.PythonStatementContext ctx)
		{
			PythonExpression exp = this.GetNodeValue<PythonExpression> (ctx.exp);
			SetNodeValue (ctx, new PythonStatement (exp, false));
		}

		
		public override void ExitJavaReturnStatement (MParser.JavaReturnStatementContext ctx)
		{
			JavaExpression exp = this.GetNodeValue<JavaExpression> (ctx.exp);
			SetNodeValue (ctx, new JavaStatement (exp, true));
		}

		
		public override void ExitCSharpReturnStatement (MParser.CSharpReturnStatementContext ctx)
		{
			CSharpExpression exp = this.GetNodeValue<CSharpExpression> (ctx.exp);
			SetNodeValue (ctx, new CSharpStatement (exp, true));
		}

		
		public override void ExitPythonReturnStatement (MParser.PythonReturnStatementContext ctx)
		{
			PythonExpression exp = this.GetNodeValue<PythonExpression> (ctx.exp);
			SetNodeValue (ctx, new PythonStatement (exp, true));
		}

		
		public override void ExitJavaNativeStatement (MParser.JavaNativeStatementContext ctx)
		{
			JavaStatement stmt = this.GetNodeValue<JavaStatement> (ctx.java_statement());
			SetNodeValue (ctx, new JavaNativeCall (stmt));
		}

		
		public override void ExitCSharpNativeStatement (MParser.CSharpNativeStatementContext ctx)
		{
			CSharpStatement stmt = this.GetNodeValue<CSharpStatement> (ctx.csharp_statement());
			SetNodeValue (ctx, new CSharpNativeCall (stmt));
		}

		
		public override void ExitPython2NativeStatement (MParser.Python2NativeStatementContext ctx)
		{
			PythonStatement stmt = this.GetNodeValue<PythonStatement> (ctx.python_native_statement());
			SetNodeValue (ctx, new Python2NativeCall (stmt));
		}

		
		public override void ExitPython3NativeStatement (MParser.Python3NativeStatementContext ctx)
		{
			PythonStatement stmt = this.GetNodeValue<PythonStatement> (ctx.python_native_statement());
			SetNodeValue (ctx, new Python3NativeCall (stmt));
		}

		
		public override void ExitNative_method_declaration (MParser.Native_method_declarationContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.typ);
			String name = this.GetNodeValue<String> (ctx.name);
			ArgumentList args = this.GetNodeValue<ArgumentList> (ctx.args);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new NativeMethodDeclaration (name, args, type, stmts));
		}

		
		public override void ExitJavaArgumentList (MParser.JavaArgumentListContext ctx)
		{
			JavaExpression item = this.GetNodeValue<JavaExpression> (ctx.item);
			SetNodeValue (ctx, new JavaExpressionList (item));
		}

		
		public override void ExitJavaArgumentListItem (MParser.JavaArgumentListItemContext ctx)
		{
			JavaExpression item = this.GetNodeValue<JavaExpression> (ctx.item);
			JavaExpressionList items = this.GetNodeValue<JavaExpressionList> (ctx.items);
			items.Add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitJava_method_expression (MParser.Java_method_expressionContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			JavaExpressionList args = this.GetNodeValue<JavaExpressionList> (ctx.args);
			SetNodeValue (ctx, new JavaMethodExpression (name, args));
		}

		
		public override void ExitJavaMethodExpression (MParser.JavaMethodExpressionContext ctx)
		{
			JavaExpression exp = this.GetNodeValue<JavaExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitDeclaration (MParser.DeclarationContext ctx)
		{
			List<CommentStatement> comments = null;
			foreach(MParser.Comment_statementContext csc in ctx.comment_statement()) {
				if(csc==null)
					continue;
				if(comments==null)
					comments = new List<CommentStatement>();
				comments.add((CommentStatement)this.GetNodeValue<CommentStatement>(csc));
			}
			List<Annotation> annotations = null;
			foreach(MParser.Annotation_constructorContext acs in ctx.annotation_constructor()) {
				if(acs==null)
					continue;
				if(annotations==null)
					annotations = new List<Annotation>();
				annotations.add((Annotation)this.GetNodeValue<Annotation>(acs));
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
			if(ctx_==null)
				ctx_ = ctx.widget_declaration();
			IDeclaration decl = this.GetNodeValue<IDeclaration>(ctx_);
			if(decl!=null) {
				decl.Comments = comments;
				decl.Annotations = annotations;
				SetNodeValue(ctx, decl);
			}		
		}


		public override void ExitDeclarations (MParser.DeclarationsContext ctx)
		{
			DeclarationList items = new DeclarationList ();
			foreach(ParserRuleContext rule in ctx.declaration()) {
				IDeclaration item = this.GetNodeValue<IDeclaration> (rule);
				items.Add (item);
			}
			SetNodeValue (ctx, items);
		}


		
	
		public override void ExitJavaBooleanLiteral (MParser.JavaBooleanLiteralContext ctx)
		{
			SetNodeValue (ctx, new JavaBooleanLiteral (ctx.GetText ()));
		}

		
		public override void ExitJavaIntegerLiteral (MParser.JavaIntegerLiteralContext ctx)
		{
			SetNodeValue (ctx, new JavaIntegerLiteral (ctx.GetText ()));
		}

		
		public override void ExitJavaDecimalLiteral (MParser.JavaDecimalLiteralContext ctx)
		{
			SetNodeValue (ctx, new JavaDecimalLiteral (ctx.GetText ()));
		}

		
		public override void ExitJavaCharacterLiteral (MParser.JavaCharacterLiteralContext ctx)
		{
			SetNodeValue (ctx, new JavaCharacterLiteral (ctx.GetText ()));
		}

		
		public override void ExitJavaTextLiteral (MParser.JavaTextLiteralContext ctx)
		{
			SetNodeValue (ctx, new JavaTextLiteral (ctx.GetText ()));
		}

		
		public override void ExitCSharpBooleanLiteral (MParser.CSharpBooleanLiteralContext ctx)
		{
			SetNodeValue (ctx, new CSharpBooleanLiteral (ctx.GetText ()));
		}

		
		public override void ExitCSharpIntegerLiteral (MParser.CSharpIntegerLiteralContext ctx)
		{
			SetNodeValue (ctx, new CSharpIntegerLiteral (ctx.GetText ()));
		}

		
		public override void ExitCSharpDecimalLiteral (MParser.CSharpDecimalLiteralContext ctx)
		{
			SetNodeValue (ctx, new CSharpDecimalLiteral (ctx.GetText ()));
		}

		
		public override void ExitCSharpCharacterLiteral (MParser.CSharpCharacterLiteralContext ctx)
		{
			SetNodeValue (ctx, new CSharpCharacterLiteral (ctx.GetText ()));
		}

		
		public override void ExitCSharpTextLiteral (MParser.CSharpTextLiteralContext ctx)
		{
			SetNodeValue (ctx, new CSharpTextLiteral (ctx.GetText ()));
		}

		
		public override void ExitPythonBooleanLiteral (MParser.PythonBooleanLiteralContext ctx)
		{
			SetNodeValue (ctx, new PythonBooleanLiteral (ctx.GetText ()));
		}

		public override void ExitPythonCharacterLiteral (MParser.PythonCharacterLiteralContext ctx)
		{
			SetNodeValue (ctx, new PythonCharacterLiteral (ctx.t.Text));
		}

		
		public override void ExitPythonIntegerLiteral (MParser.PythonIntegerLiteralContext ctx)
		{
			SetNodeValue (ctx, new PythonIntegerLiteral (ctx.GetText ()));
		}

		
		public override void ExitPythonDecimalLiteral (MParser.PythonDecimalLiteralContext ctx)
		{
			SetNodeValue (ctx, new PythonDecimalLiteral (ctx.GetText ()));
		}

		
		public override void ExitPythonTextLiteral (MParser.PythonTextLiteralContext ctx)
		{
			SetNodeValue (ctx, new PythonTextLiteral (ctx.GetText ()));
		}

		
		public override void ExitPythonLiteralExpression (MParser.PythonLiteralExpressionContext ctx)
		{
			PythonExpression exp = this.GetNodeValue<PythonExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitJavaCategoryBinding (MParser.JavaCategoryBindingContext ctx)
		{
			JavaIdentifierExpression map = this.GetNodeValue<JavaIdentifierExpression> (ctx.binding);
			SetNodeValue (ctx, new JavaNativeCategoryBinding (map));
		}

		
		public override void ExitCSharpCategoryBinding (MParser.CSharpCategoryBindingContext ctx)
		{
			CSharpIdentifierExpression map = this.GetNodeValue<CSharpIdentifierExpression> (ctx.binding);
			SetNodeValue (ctx, new CSharpNativeCategoryBinding (map));
		}

		
		public override void ExitPython_module (MParser.Python_moduleContext ctx)
		{
			List<String> ids = new List<String> ();
			foreach (MParser.IdentifierContext ic in ctx.identifier())
				ids.Add (ic.GetText ());
			PythonModule module = new PythonModule (ids);
			SetNodeValue (ctx, module);
		}


		
		public override void ExitPython_native_statement (MParser.Python_native_statementContext ctx)
		{
			PythonStatement stmt = this.GetNodeValue<PythonStatement> (ctx.python_statement());
			PythonModule module = this.GetNodeValue<PythonModule> (ctx.python_module());
			stmt.setModule (module);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitPython2CategoryBinding (MParser.Python2CategoryBindingContext ctx)
		{
			PythonNativeCategoryBinding map = this.GetNodeValue<PythonNativeCategoryBinding> (ctx.binding);
			SetNodeValue (ctx, new Python2NativeCategoryBinding (map));
		}

		
		public override void ExitPython3CategoryBinding (MParser.Python3CategoryBindingContext ctx)
		{
			PythonNativeCategoryBinding map = this.GetNodeValue<PythonNativeCategoryBinding> (ctx.binding);
			SetNodeValue (ctx, new Python3NativeCategoryBinding (map));
		}

		
		public override void ExitNativeCategoryBindingList (MParser.NativeCategoryBindingListContext ctx)
		{
			NativeCategoryBinding item = this.GetNodeValue<NativeCategoryBinding> (ctx.item);
			NativeCategoryBindingList items = new NativeCategoryBindingList (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitNativeCategoryBindingListItem (MParser.NativeCategoryBindingListItemContext ctx)
		{
			NativeCategoryBinding item = this.GetNodeValue<NativeCategoryBinding> (ctx.item);
			NativeCategoryBindingList items = this.GetNodeValue<NativeCategoryBindingList> (ctx.items);
			items.Add (item);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitNative_category_bindings (MParser.Native_category_bindingsContext ctx)
		{
			NativeCategoryBindingList items = this.GetNodeValue<NativeCategoryBindingList> (ctx.items);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitNative_category_declaration (MParser.Native_category_declarationContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			IdentifierList attrs = this.GetNodeValue<IdentifierList> (ctx.attrs);
			NativeCategoryBindingList bindings = this.GetNodeValue<NativeCategoryBindingList> (ctx.bindings);
			MethodDeclarationList methods = this.GetNodeValue<MethodDeclarationList> (ctx.methods);
			NativeCategoryDeclaration decl = new NativeCategoryDeclaration (name, attrs, bindings, null, methods);
			decl.Storable = ctx.STORABLE () != null;
			SetNodeValue (ctx, decl);
		}

		
		public override void ExitNative_widget_declaration(MParser.Native_widget_declarationContext ctx)
		{
			String name = this.GetNodeValue<String>(ctx.name);
			NativeCategoryBindingList bindings = this.GetNodeValue<NativeCategoryBindingList>(ctx.bindings);
			MethodDeclarationList methods = this.GetNodeValue<MethodDeclarationList>(ctx.methods);
			SetNodeValue(ctx, new NativeWidgetDeclaration(name, bindings, methods));
		}


		public override void ExitNativeCategoryDeclaration (MParser.NativeCategoryDeclarationContext ctx)
		{
			IDeclaration decl = this.GetNodeValue<IDeclaration> (ctx.decl);
			SetNodeValue (ctx, decl);
		}

		
		public override void ExitNative_resource_declaration (MParser.Native_resource_declarationContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			IdentifierList attrs = this.GetNodeValue<IdentifierList> (ctx.attrs);
			NativeCategoryBindingList bindings = this.GetNodeValue<NativeCategoryBindingList> (ctx.bindings);
			MethodDeclarationList methods = this.GetNodeValue<MethodDeclarationList> (ctx.methods);
			NativeResourceDeclaration decl = new NativeResourceDeclaration(name, attrs, bindings, null, methods);
			decl.Storable = ctx.STORABLE() != null;
			SetNodeValue(ctx, decl);
		}

		
		public override void ExitResource_declaration (MParser.Resource_declarationContext ctx)
		{
			IDeclaration decl = this.GetNodeValue<IDeclaration> (ctx.native_resource_declaration());
			SetNodeValue (ctx, decl);
		}

		

		
		public override void ExitParenthesis_expression (MParser.Parenthesis_expressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.expression());
			SetNodeValue (ctx, new ParenthesisExpression (exp));
		}

		
		public override void ExitParenthesisExpression (MParser.ParenthesisExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitNative_symbol_list (MParser.Native_symbol_listContext ctx)
		{
			NativeSymbolList items = new NativeSymbolList ();
			foreach(ParserRuleContext rule in ctx.native_symbol()) {
				NativeSymbol item = this.GetNodeValue<NativeSymbol> (rule);
				items.add (item);
			}
			SetNodeValue (ctx, items);
		}

		
		public override void ExitEnum_native_declaration (MParser.Enum_native_declarationContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			NativeType type = this.GetNodeValue<NativeType> (ctx.typ);
			NativeSymbolList symbols = this.GetNodeValue<NativeSymbolList> (ctx.symbols);
			SetNodeValue (ctx, new EnumeratedNativeDeclaration (name, type, symbols));
		}

		
		public override void ExitFor_each_statement (MParser.For_each_statementContext ctx)
		{
			String name1 = this.GetNodeValue<String> (ctx.name1);
			String name2 = this.GetNodeValue<String> (ctx.name2);
			IExpression source = this.GetNodeValue<IExpression> (ctx.source);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new ForEachStatement (name1, name2, source, stmts));
		}

		
		public override void ExitForEachStatement (MParser.ForEachStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitKey_token (MParser.Key_tokenContext ctx)
		{
			SetNodeValue (ctx, ctx.GetText ());
		}

		
		public override void ExitValue_token (MParser.Value_tokenContext ctx)
		{
			SetNodeValue (ctx, ctx.GetText ());
		}

		
		public override void ExitNamed_argument (MParser.Named_argumentContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.variable_identifier());
			UnresolvedArgument arg = new UnresolvedArgument(name);
			IExpression exp = this.GetNodeValue<IExpression>(ctx.literal_expression());
			arg.DefaultValue = exp;
			SetNodeValue(ctx, arg);
		}

		
		public override void ExitClosureStatement (MParser.ClosureStatementContext ctx)
		{
			ConcreteMethodDeclaration decl = this.GetNodeValue<ConcreteMethodDeclaration> (ctx.decl);
			SetNodeValue (ctx, new DeclarationStatement<ConcreteMethodDeclaration> (decl));
		}

		
		public override void ExitReturn_statement (MParser.Return_statementContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new ReturnStatement (exp));
		}

		
		public override void ExitReturnStatement (MParser.ReturnStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitClosure_expression (MParser.Closure_expressionContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new MethodExpression (name));
		}

		
		public override void ExitClosureExpression (MParser.ClosureExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitIf_statement (MParser.If_statementContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			IfElementList elseIfs = this.GetNodeValue<IfElementList> (ctx.elseIfs);
			StatementList elseStmts = this.GetNodeValue<StatementList> (ctx.elseStmts);
			SetNodeValue (ctx, new IfStatement (exp, stmts, elseIfs, elseStmts));
		}

		
		public override void ExitElseIfStatementList (MParser.ElseIfStatementListContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			IfElement elem = new IfElement (exp, stmts);
			SetNodeValue (ctx, new IfElementList (elem));
		}

		
		public override void ExitElseIfStatementListItem (MParser.ElseIfStatementListItemContext ctx)
		{
			IfElementList items = this.GetNodeValue<IfElementList> (ctx.items);
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			IfElement elem = new IfElement (exp, stmts);
			items.Add (elem);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitIfStatement (MParser.IfStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitSwitchStatement (MParser.SwitchStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitAssignTupleStatement (MParser.AssignTupleStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitRaiseStatement (MParser.RaiseStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitWriteStatement (MParser.WriteStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitWithResourceStatement (MParser.WithResourceStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		public override void ExitWith_singleton_statement(MParser.With_singleton_statementContext ctx) {
			String name = this.GetNodeValue<String>(ctx.typ);
			CategoryType type = new CategoryType(name);
			StatementList stmts = this.GetNodeValue<StatementList>(ctx.stmts);
			SetNodeValue(ctx, new WithSingletonStatement(type, stmts));
		}

		public override void ExitWithSingletonStatement(MParser.WithSingletonStatementContext ctx) {
			IStatement stmt = this.GetNodeValue<IStatement>(ctx.stmt);
			SetNodeValue(ctx, stmt);
		}
		
		public override void ExitWhileStatement (MParser.WhileStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitDoWhileStatement (MParser.DoWhileStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitTryStatement (MParser.TryStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitEqualsExpression (MParser.EqualsExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new EqualsExpression (left, EqOp.EQUALS, right));
		}

		
		public override void ExitNotEqualsExpression (MParser.NotEqualsExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new EqualsExpression (left, EqOp.NOT_EQUALS, right));
		}

		
		public override void ExitGreaterThanExpression (MParser.GreaterThanExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new CompareExpression (left, CmpOp.GT, right));
		}

		
		public override void ExitGreaterThanOrEqualExpression (MParser.GreaterThanOrEqualExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new CompareExpression (left, CmpOp.GTE, right));
		}

		
		public override void ExitLessThanExpression (MParser.LessThanExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new CompareExpression (left, CmpOp.LT, right));
		}

		
		public override void ExitLessThanOrEqualExpression (MParser.LessThanOrEqualExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new CompareExpression (left, CmpOp.LTE, right));
		}

		
		public override void ExitAtomicSwitchCase (MParser.AtomicSwitchCaseContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new AtomicSwitchCase (exp, stmts));
		}

		public override void ExitCommentStatement(MParser.CommentStatementContext ctx) {
			SetNodeValue(ctx, this.GetNodeValue<Object>(ctx.comment_statement()));
		}

		public override void ExitComment_statement(MParser.Comment_statementContext ctx) {
			SetNodeValue(ctx, new CommentStatement(ctx.GetText()));
		}


		public override void ExitCollectionSwitchCase (MParser.CollectionSwitchCaseContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new CollectionSwitchCase (exp, stmts));
		}

		
		public override void ExitSwitch_case_statement_list (MParser.Switch_case_statement_listContext ctx)
		{
			SwitchCaseList items = new SwitchCaseList ();
			foreach(ParserRuleContext rule in ctx.switch_case_statement()) {
				SwitchCase item = this.GetNodeValue<SwitchCase> (rule);
				items.Add (item);
			}
			SetNodeValue (ctx, items);
		}


		
		public override void ExitSwitch_statement (MParser.Switch_statementContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SwitchCaseList cases = this.GetNodeValue<SwitchCaseList> (ctx.cases);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SwitchStatement stmt = new SwitchStatement (exp, cases, stmts);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitLiteralRangeLiteral (MParser.LiteralRangeLiteralContext ctx)
		{
			IExpression low = this.GetNodeValue<IExpression> (ctx.low);
			IExpression high = this.GetNodeValue<IExpression> (ctx.high);
			SetNodeValue (ctx, new RangeLiteral (low, high));
		}

		public override void ExitLiteralSetLiteral (MParser.LiteralSetLiteralContext ctx)
		{
			ExpressionList items = this.GetNodeValue<ExpressionList>(ctx.literal_list_literal());
			SetNodeValue(ctx, new SetLiteral(items));
		}

		public override void ExitLiteralListLiteral (MParser.LiteralListLiteralContext ctx)
		{
			ExpressionList exp = this.GetNodeValue<ExpressionList> (ctx.literal_list_literal());
			SetNodeValue (ctx, new ListLiteral (exp, false));
		}

		
		public override void ExitLiteral_list_literal (MParser.Literal_list_literalContext ctx)
		{
			ExpressionList items = new ExpressionList ();
			foreach(ParserRuleContext rule in ctx.atomic_literal()) {
				IExpression item = this.GetNodeValue<IExpression> (rule);
				items.add (item);
			}
			SetNodeValue (ctx, items);
		}

		
		public override void ExitInExpression (MParser.InExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new ContainsExpression (left, ContOp.IN, right));
		}

		
		public override void ExitNotInExpression (MParser.NotInExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new ContainsExpression (left, ContOp.NOT_IN, right));
		}

		
		public override void ExitHasExpression(MParser.HasExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression>(ctx.left);
			IExpression right = this.GetNodeValue<IExpression>(ctx.right);
			SetNodeValue(ctx, new ContainsExpression(left, ContOp.HAS, right));
		}


		public override void ExitHasAllExpression(MParser.HasAllExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression>(ctx.left);
			IExpression right = this.GetNodeValue<IExpression>(ctx.right);
			SetNodeValue(ctx, new ContainsExpression(left, ContOp.HAS_ALL, right));
		}


		public override void ExitNotHasAllExpression(MParser.NotHasAllExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression>(ctx.left);
			IExpression right = this.GetNodeValue<IExpression>(ctx.right);
			SetNodeValue(ctx, new ContainsExpression(left, ContOp.NOT_HAS_ALL, right));
		}


		public override void ExitHasAnyExpression(MParser.HasAnyExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression>(ctx.left);
			IExpression right = this.GetNodeValue<IExpression>(ctx.right);
			SetNodeValue(ctx, new ContainsExpression(left, ContOp.HAS_ANY, right));
		}


		public override void ExitNotHasAnyExpression(MParser.NotHasAnyExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression>(ctx.left);
			IExpression right = this.GetNodeValue<IExpression>(ctx.right);
			SetNodeValue(ctx, new ContainsExpression(left, ContOp.NOT_HAS_ANY, right));
		}


		public override void ExitContainsExpression(MParser.ContainsExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression>(ctx.left);
			IExpression right = this.GetNodeValue<IExpression>(ctx.right);
			SetNodeValue(ctx, new EqualsExpression(left, EqOp.CONTAINS, right));
		}


		public override void ExitNotContainsExpression(MParser.NotContainsExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression>(ctx.left);
			IExpression right = this.GetNodeValue<IExpression>(ctx.right);
			SetNodeValue(ctx, new EqualsExpression(left, EqOp.NOT_CONTAINS, right));
		}

		
		public override void ExitDivideExpression (MParser.DivideExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new DivideExpression (left, right));
		}

		
		public override void ExitIntDivideExpression (MParser.IntDivideExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new IntDivideExpression (left, right));
		}

		
		public override void ExitModuloExpression (MParser.ModuloExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new ModuloExpression (left, right));
		}

		
		public override void ExitAnnotation_constructor(MParser.Annotation_constructorContext ctx)
		{
			String name = this.GetNodeValue<String>(ctx.name);
			IExpression exp = this.GetNodeValue<IExpression>(ctx.exp);
			SetNodeValue(ctx, new Annotation(name, exp));
		}


		public override void ExitAnnotation_identifier(MParser.Annotation_identifierContext ctx)
		{
			String name = ctx.GetText();
			SetNodeValue(ctx, name);
		}


		public override void ExitAndExpression (MParser.AndExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new AndExpression (left, right));
		}

		public override void ExitNullLiteral (MParser.NullLiteralContext ctx)
		{
			SetNodeValue (ctx, NullLiteral.Instance);
		}

		public override void ExitOperatorArgument(MParser.OperatorArgumentContext ctx) {
			bool mutable = ctx.MUTABLE () != null;
			IArgument arg = this.GetNodeValue<IArgument>(ctx.arg);
			arg.setMutable (mutable);
			SetNodeValue(ctx, arg);
		}

		public override void ExitOperatorPlus(MParser.OperatorPlusContext ctx) {
			SetNodeValue(ctx, Operator.PLUS);
		}

		public override void ExitOperatorMinus(MParser.OperatorMinusContext ctx) {
			SetNodeValue(ctx, Operator.MINUS);
		}

		public override void ExitOperatorMultiply(MParser.OperatorMultiplyContext ctx) {
			SetNodeValue(ctx, Operator.MULTIPLY);
		}

		public override void ExitOperatorDivide(MParser.OperatorDivideContext ctx) {
			SetNodeValue(ctx, Operator.DIVIDE);
		}

		public override void ExitOperatorIDivide(MParser.OperatorIDivideContext ctx) {
			SetNodeValue(ctx, Operator.IDIVIDE);
		}

		public override void ExitOperatorModulo(MParser.OperatorModuloContext ctx) {
			SetNodeValue(ctx, Operator.MODULO);
		}

		public override void ExitOperator_method_declaration(MParser.Operator_method_declarationContext ctx) {
			Operator op = this.GetNodeValue<Operator>(ctx.op);
			IArgument arg = this.GetNodeValue<IArgument>(ctx.arg);
			IType typ = this.GetNodeValue<IType>(ctx.typ);
			StatementList stmts = this.GetNodeValue<StatementList>(ctx.stmts);
			OperatorMethodDeclaration decl = new OperatorMethodDeclaration(op, arg, typ, stmts);
			SetNodeValue(ctx, decl);
		}

		public override void ExitOrder_by(MParser.Order_byContext ctx) {
			IdentifierList names = new IdentifierList();
			foreach(MParser.Variable_identifierContext ctx_ in ctx.variable_identifier())
				names.add(this.GetNodeValue<string>(ctx_));
			OrderByClause clause = new OrderByClause(names, ctx.DESC()!=null);
			SetNodeValue(ctx, clause);
		}

		public override void ExitOrder_by_list(MParser.Order_by_listContext ctx) {
			OrderByClauseList list = new OrderByClauseList();
			foreach(MParser.Order_byContext ctx_ in ctx.order_by())
				list.add(this.GetNodeValue<OrderByClause>(ctx_));
			SetNodeValue(ctx, list);
		}

		public override void ExitOrExpression (MParser.OrExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new OrExpression (left, right));
		}

		
		public override void ExitMultiplyExpression (MParser.MultiplyExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new MultiplyExpression (left, right));
		}


		public override void ExitMutable_category_type (MParser.Mutable_category_typeContext ctx)
		{
			CategoryType typ = this.GetNodeValue<CategoryType> (ctx.category_type ());
			typ.Mutable = ctx.MUTABLE() != null;
			SetNodeValue (ctx, typ);
		}


		public override void ExitMinusExpression (MParser.MinusExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new MinusExpression (exp));
		}

		
		public override void ExitNotExpression (MParser.NotExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new NotExpression (exp));
		}

		
		public override void ExitWhile_statement (MParser.While_statementContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new WhileStatement (exp, stmts));
		}

		
		public override void ExitDo_while_statement (MParser.Do_while_statementContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new DoWhileStatement (exp, stmts));
		}

		public override void ExitSingleton_category_declaration(MParser.Singleton_category_declarationContext ctx) {
			String name = this.GetNodeValue<String>(ctx.name);
			IdentifierList attrs = this.GetNodeValue<IdentifierList>(ctx.attrs);
			MethodDeclarationList methods = this.GetNodeValue<MethodDeclarationList>(ctx.methods);
			SetNodeValue(ctx, new SingletonCategoryDeclaration(name, attrs, methods));
		}

		public override void ExitSingletonCategoryDeclaration(MParser.SingletonCategoryDeclarationContext ctx) {
			IDeclaration decl = this.GetNodeValue<IDeclaration>(ctx.decl);
			SetNodeValue(ctx, decl);
		}

		public override void ExitSliceFirstAndLast (MParser.SliceFirstAndLastContext ctx)
		{
			IExpression first = this.GetNodeValue<IExpression> (ctx.first);
			IExpression last = this.GetNodeValue<IExpression> (ctx.last);
			SetNodeValue (ctx, new SliceSelector (first, last));
		}

		
		public override void ExitSliceFirstOnly (MParser.SliceFirstOnlyContext ctx)
		{
			IExpression first = this.GetNodeValue<IExpression> (ctx.first);
			SetNodeValue (ctx, new SliceSelector (first, null));
		}

		
		public override void ExitSliceLastOnly (MParser.SliceLastOnlyContext ctx)
		{
			IExpression last = this.GetNodeValue<IExpression> (ctx.last);
			SetNodeValue (ctx, new SliceSelector (null, last));
		}

		
		public override void ExitSorted_expression (MParser.Sorted_expressionContext ctx)
		{
			IExpression source = this.GetNodeValue<IExpression> (ctx.source);
			bool descending = ctx.DESC() != null;
			IExpression key = this.GetNodeValue<IExpression> (ctx.key);
			SetNodeValue (ctx, new SortedExpression (source, descending, key));
		}

		

		public override void ExitDocument_expression (MParser.Document_expressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression>(ctx.expression());
			SetNodeValue (ctx, new DocumentExpression (exp));
		}

		
		public override void ExitDocumentType (MParser.DocumentTypeContext ctx)
		{
			SetNodeValue (ctx, DocumentType.Instance);
		}

		public override void ExitFetchOne (MParser.FetchOneContext ctx)
		{
			CategoryType category = this.GetNodeValue<CategoryType>(ctx.typ);
			IExpression filter = this.GetNodeValue<IExpression>(ctx.predicate);
			SetNodeValue(ctx, new FetchOneExpression(category, filter));
		}

		public override void ExitFetchMany (MParser.FetchManyContext ctx)
		{
			CategoryType category = this.GetNodeValue<CategoryType>(ctx.typ);
			IExpression filter = this.GetNodeValue<IExpression>(ctx.predicate);
			IExpression start = this.GetNodeValue<IExpression>(ctx.xstart);
			IExpression stop = this.GetNodeValue<IExpression>(ctx.xstop);
			OrderByClauseList orderBy = this.GetNodeValue<OrderByClauseList>(ctx.orderby);
			SetNodeValue(ctx, new FetchManyExpression(category, filter, start, stop, orderBy));
		}


		public override void ExitFilteredListExpression(MParser.FilteredListExpressionContext ctx)
		{
			FilteredExpression fetch = this.GetNodeValue<FilteredExpression>(ctx.filtered_list_suffix());
			IExpression source = this.GetNodeValue<IExpression>(ctx.src);
			fetch.Source = source;
			SetNodeValue(ctx, fetch);
		}


		public override void ExitFiltered_list_suffix(MParser.Filtered_list_suffixContext ctx)
		{
			String itemName = this.GetNodeValue<String>(ctx.name);
			IExpression predicate = this.GetNodeValue<IExpression>(ctx.predicate);
			SetNodeValue(ctx, new FilteredExpression(itemName, null, predicate));
		}
		
		public override void ExitCode_type (MParser.Code_typeContext ctx)
		{
			SetNodeValue (ctx, CodeType.Instance);
		}

		
		public override void ExitExecuteExpression (MParser.ExecuteExpressionContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new ExecuteExpression (name));
		}

		
		public override void ExitCodeExpression (MParser.CodeExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new CodeExpression (exp));
		}

		
		public override void ExitCode_argument (MParser.Code_argumentContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new CodeArgument (name));
		}

		
		public override void ExitCategory_symbol (MParser.Category_symbolContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			ArgumentAssignmentList args = this.GetNodeValue<ArgumentAssignmentList> (ctx.args);
			SetNodeValue (ctx, new CategorySymbol (name, args));
		}

		
		public override void ExitCategory_symbol_list (MParser.Category_symbol_listContext ctx)
		{
			CategorySymbolList items = new CategorySymbolList ();
			foreach(ParserRuleContext rule in ctx.category_symbol()) {
				CategorySymbol item = this.GetNodeValue<CategorySymbol> (rule);
				items.add (item);
			}
			SetNodeValue (ctx, items);
		}



		
		public override void ExitEnum_category_declaration (MParser.Enum_category_declarationContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			IdentifierList attrs = this.GetNodeValue<IdentifierList> (ctx.attrs);
			String parent = this.GetNodeValue<String> (ctx.derived);
			IdentifierList derived = parent == null ? null : new IdentifierList (parent);
			CategorySymbolList symbols = this.GetNodeValue<CategorySymbolList> (ctx.symbols);
			SetNodeValue (ctx, new EnumeratedCategoryDeclaration (name, attrs, derived, symbols));
		}

		
		public override void ExitRead_all_expression (MParser.Read_all_expressionContext ctx)
		{
			IExpression source = this.GetNodeValue<IExpression> (ctx.source);
			SetNodeValue (ctx, new ReadAllExpression (source));
		}

		
		public override void ExitRead_one_expression(MParser.Read_one_expressionContext ctx)
		{
			IExpression source = this.GetNodeValue<IExpression>(ctx.source);
			SetNodeValue(ctx, new ReadOneExpression(source));
		}



		public override void ExitWrite_statement (MParser.Write_statementContext ctx)
		{
			IExpression what = this.GetNodeValue<IExpression> (ctx.what);
			IExpression target = this.GetNodeValue<IExpression> (ctx.target);
			SetNodeValue (ctx, new WriteStatement (what, target));
		}

		
		public override void ExitWith_resource_statement (MParser.With_resource_statementContext ctx)
		{
			AssignVariableStatement stmt = this.GetNodeValue<AssignVariableStatement> (ctx.stmt);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new WithResourceStatement (stmt, stmts));
		}

		
		public override void ExitAnyType (MParser.AnyTypeContext ctx)
		{
			SetNodeValue (ctx, AnyType.Instance);
		}

		
		public override void ExitAnyListType (MParser.AnyListTypeContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.any_type());
			SetNodeValue (ctx, new ListType (type));
		}

		
		public override void ExitAnyDictType (MParser.AnyDictTypeContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.any_type());
			SetNodeValue (ctx, new DictType (type));
		}

		
		public override void ExitCastExpression (MParser.CastExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IType type = this.GetNodeValue<IType> (ctx.right);
			SetNodeValue (ctx, new CastExpression (left, type));
		}

		public override void ExitCatchAtomicStatement (MParser.CatchAtomicStatementContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new AtomicSwitchCase (new SymbolExpression (name), stmts));
		}

		
		public override void ExitCatchCollectionStatement (MParser.CatchCollectionStatementContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new CollectionSwitchCase (exp, stmts));
		}

		
		public override void ExitCatch_statement_list (MParser.Catch_statement_listContext ctx)
		{
			SwitchCaseList items = new SwitchCaseList ();
			foreach(ParserRuleContext rule in ctx.catch_statement()) {
				SwitchCase item = this.GetNodeValue<SwitchCase> (rule);
				items.Add (item);
			}
			SetNodeValue (ctx, items);
		}



		
		public override void ExitTry_statement (MParser.Try_statementContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SwitchCaseList handlers = this.GetNodeValue<SwitchCaseList> (ctx.handlers);
			StatementList anyStmts = this.GetNodeValue<StatementList> (ctx.anyStmts);
			StatementList finalStmts = this.GetNodeValue<StatementList> (ctx.finalStmts);
			SwitchErrorStatement stmt = new SwitchErrorStatement (name, stmts, handlers, anyStmts, finalStmts);
			SetNodeValue (ctx, stmt);
		}

		
		public override void ExitRaise_statement (MParser.Raise_statementContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new RaiseStatement (exp));
		}

		
		public override void ExitMatchingList (MParser.MatchingListContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.source);
			SetNodeValue (ctx, new MatchingCollectionConstraint (exp));
		}

		
		public override void ExitMatchingRange (MParser.MatchingRangeContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.source);
			SetNodeValue (ctx, new MatchingCollectionConstraint (exp));
		}

		
		public override void ExitMatchingExpression (MParser.MatchingExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new MatchingExpressionConstraint (exp));
		}

		
		public override void ExitMatchingPattern (MParser.MatchingPatternContext ctx)
		{
			SetNodeValue (ctx, new MatchingPatternConstraint (new TextLiteral (ctx.text.Text)));
		}

		public override void ExitMatchingSet (MParser.MatchingSetContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression>(ctx.source);
			SetNodeValue(ctx, new MatchingCollectionConstraint(exp));
		}

		public override void ExitCsharp_item_expression (MParser.Csharp_item_expressionContext ctx)
		{
			CSharpExpression exp = this.GetNodeValue<CSharpExpression> (ctx.exp);
			SetNodeValue (ctx, new CSharpItemExpression (exp));
		}

		public override void ExitCsharp_method_expression (MParser.Csharp_method_expressionContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			CSharpExpressionList args = this.GetNodeValue<CSharpExpressionList> (ctx.args);
			SetNodeValue (ctx, new CSharpMethodExpression (name, args));
		}

		public override void ExitCSharpArgumentList (MParser.CSharpArgumentListContext ctx)
		{
			CSharpExpression item = this.GetNodeValue<CSharpExpression> (ctx.item);
			SetNodeValue (ctx, new CSharpExpressionList (item));
		}

		public override void ExitCSharpArgumentListItem (MParser.CSharpArgumentListItemContext ctx)
		{
			CSharpExpression item = this.GetNodeValue<CSharpExpression> (ctx.item);
			CSharpExpressionList items = this.GetNodeValue<CSharpExpressionList> (ctx.items);
			items.Add (item);
			SetNodeValue (ctx, items);
		}

		public override void ExitCSharpItemExpression (MParser.CSharpItemExpressionContext ctx)
		{
			CSharpExpression exp = this.GetNodeValue<CSharpExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		public override void ExitCSharpMethodExpression (MParser.CSharpMethodExpressionContext ctx)
		{
			CSharpExpression exp = this.GetNodeValue<CSharpExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		public override void ExitCSharpSelectorExpression (MParser.CSharpSelectorExpressionContext ctx)
		{
			CSharpExpression parent = this.GetNodeValue<CSharpExpression> (ctx.parent);
			CSharpSelectorExpression child = this.GetNodeValue<CSharpSelectorExpression> (ctx.child);
			child.SetParent (parent);
			SetNodeValue (ctx, child);
		}

		public override void ExitCsharp_primary_expression (MParser.Csharp_primary_expressionContext ctx)
		{
			CSharpExpression exp = this.GetNodeValue<CSharpExpression> (ctx.GetChild(0));
			SetNodeValue (ctx, exp);
		}

		public override void ExitCsharp_this_expression (MParser.Csharp_this_expressionContext ctx)
		{
			SetNodeValue (ctx, new CSharpThisExpression());
		}

			
		public override void ExitJavascript_category_binding (MParser.Javascript_category_bindingContext ctx)
		{
			StringBuilder sb = new StringBuilder();
			foreach (MParser.IdentifierContext cx in ctx.identifier())
				sb.Append(cx.GetText());
			String identifier = sb.ToString();
			JavaScriptModule module = this.GetNodeValue<JavaScriptModule> (ctx.javascript_module ());
			JavaScriptNativeCategoryBinding map = new JavaScriptNativeCategoryBinding (identifier, module);
			SetNodeValue (ctx, map);
		}

		public override void ExitJavaScriptMemberExpression (MParser.JavaScriptMemberExpressionContext ctx)
		{
			String name = ctx.name.GetText ();
			SetNodeValue (ctx, new JavaScriptMemberExpression(name));
		}

		public override void ExitJavascript_primary_expression (MParser.Javascript_primary_expressionContext ctx)
		{
			JavaScriptExpression exp = this.GetNodeValue<JavaScriptExpression> (ctx.GetChild(0));
			SetNodeValue (ctx, exp);
		}

		public override void ExitJavaScriptMethodExpression (MParser.JavaScriptMethodExpressionContext ctx)
		{
			JavaScriptExpression method = this.GetNodeValue<JavaScriptExpression> (ctx.method);
			SetNodeValue (ctx, method);
		}

		public override void ExitJavascript_this_expression (MParser.Javascript_this_expressionContext ctx)
		{
			SetNodeValue (ctx, new JavaScriptThisExpression ());
		}


		public override void ExitJavascript_identifier (MParser.Javascript_identifierContext ctx)
		{
			String name = ctx.GetText ();
			SetNodeValue (ctx, name);
		}

		
		public override void ExitJavascript_method_expression (MParser.Javascript_method_expressionContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			JavaScriptMethodExpression method = new JavaScriptMethodExpression (name);
			JavaScriptExpressionList args = this.GetNodeValue<JavaScriptExpressionList> (ctx.args);
			method.setArguments (args);
			SetNodeValue (ctx, method);
		}

		public override void ExitJavascriptDecimalLiteral (MParser.JavascriptDecimalLiteralContext ctx)
		{
			String text = ctx.t.Text;
			SetNodeValue (ctx, new JavaScriptDecimalLiteral (text));		
		}

		public override void ExitJavascriptTextLiteral (MParser.JavascriptTextLiteralContext ctx)
		{
			String text = ctx.t.Text;
			SetNodeValue (ctx, new JavaScriptTextLiteral (text));		
		}

		public override void ExitJavascriptIntegerLiteral (MParser.JavascriptIntegerLiteralContext ctx)
		{
			String text = ctx.t.Text;
			SetNodeValue (ctx, new JavaScriptIntegerLiteral (text));		
		}

		public override void ExitJavascript_module (MParser.Javascript_moduleContext ctx)
		{
			List<String> ids = new List<String> ();
			foreach (MParser.Javascript_identifierContext ic in ctx.javascript_identifier())
				ids.Add (ic.GetText ());
			JavaScriptModule module = new JavaScriptModule (ids);
			SetNodeValue (ctx, module);
		}

		
		public override void ExitJavascript_native_statement (MParser.Javascript_native_statementContext ctx)
		{
			JavaScriptStatement stmt = this.GetNodeValue<JavaScriptStatement> (ctx.javascript_statement());
			JavaScriptModule module = this.GetNodeValue<JavaScriptModule> (ctx.javascript_module());
			stmt.setModule (module);
			SetNodeValue (ctx, stmt);
		}


		public override void ExitJavascript_new_expression (MParser.Javascript_new_expressionContext ctx)
		{
			JavaScriptMethodExpression method = this.GetNodeValue<JavaScriptMethodExpression> (ctx.javascript_method_expression());
			SetNodeValue (ctx, new JavaScriptNewExpression(method));
		}

		
		public override void ExitJavascriptArgumentList (MParser.JavascriptArgumentListContext ctx)
		{
			JavaScriptExpression exp = this.GetNodeValue<JavaScriptExpression> (ctx.item);
			JavaScriptExpressionList list = new JavaScriptExpressionList (exp);
			SetNodeValue (ctx, list);
		}

		
		public override void ExitJavascriptArgumentListItem (MParser.JavascriptArgumentListItemContext ctx)
		{
			JavaScriptExpression exp = this.GetNodeValue<JavaScriptExpression> (ctx.item);
			JavaScriptExpressionList list = this.GetNodeValue<JavaScriptExpressionList> (ctx.items);
			list.Add (exp);
			SetNodeValue (ctx, list);
		}

		public override void ExitJavascriptBooleanLiteral (MParser.JavascriptBooleanLiteralContext ctx)
		{
			SetNodeValue (ctx, new JavaScriptBooleanLiteral (ctx.t.Text));
		}

		
		public override void ExitJavaScriptCategoryBinding (MParser.JavaScriptCategoryBindingContext ctx)
		{
			SetNodeValue (ctx, this.GetNodeValue<Object> (ctx.binding));
		}

		public override void ExitJavascriptCharacterLiteral (MParser.JavascriptCharacterLiteralContext ctx)
		{
			String text = ctx.t.Text;
			SetNodeValue (ctx, new JavaScriptCharacterLiteral (text));		
		}


		public override void ExitJavascript_identifier_expression (MParser.Javascript_identifier_expressionContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new JavaScriptIdentifierExpression (name));
		}


		public override void ExitJavaScriptNativeStatement (MParser.JavaScriptNativeStatementContext ctx)
		{
			JavaScriptStatement stmt = this.GetNodeValue<JavaScriptStatement> (ctx.javascript_native_statement());
			SetNodeValue (ctx, new JavaScriptNativeCall (stmt));
		}

		
		public override void ExitJavascriptPrimaryExpression (MParser.JavascriptPrimaryExpressionContext ctx)
		{
			JavaScriptExpression exp = this.GetNodeValue<JavaScriptExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitJavascriptReturnStatement (MParser.JavascriptReturnStatementContext ctx)
		{
			JavaScriptExpression exp = this.GetNodeValue<JavaScriptExpression> (ctx.exp);
			SetNodeValue (ctx, new JavaScriptStatement (exp, true));
		}

		
		public override void ExitJavascriptSelectorExpression (MParser.JavascriptSelectorExpressionContext ctx)
		{
			JavaScriptExpression parent = this.GetNodeValue<JavaScriptExpression> (ctx.parent);
			JavaScriptSelectorExpression child = this.GetNodeValue<JavaScriptSelectorExpression> (ctx.child);
			child.setParent (parent);
			SetNodeValue (ctx, child);
		}

		
		public override void ExitJavascriptStatement (MParser.JavascriptStatementContext ctx)
		{
			JavaScriptExpression exp = this.GetNodeValue<JavaScriptExpression> (ctx.exp);
			SetNodeValue (ctx, new JavaScriptStatement (exp, false));
		}


		
		public override void ExitPython_category_binding (MParser.Python_category_bindingContext ctx)
		{
			String identifier = ctx.identifier ().GetText ();
			PythonModule module = this.GetNodeValue<PythonModule> (ctx.python_module ());
			PythonNativeCategoryBinding map = new PythonNativeCategoryBinding (identifier, module);
			SetNodeValue (ctx, map);
		}

		 
		public override void ExitPythonGlobalMethodExpression (MParser.PythonGlobalMethodExpressionContext ctx)
		{
			PythonMethodExpression exp = this.GetNodeValue<PythonMethodExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitPython_method_expression (MParser.Python_method_expressionContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			PythonArgumentList args = this.GetNodeValue<PythonArgumentList> (ctx.args);
			PythonMethodExpression method = new PythonMethodExpression (name);
			method.setArguments (args);
			SetNodeValue (ctx, method);
		}

		
		public override void ExitPythonIdentifierExpression (MParser.PythonIdentifierExpressionContext ctx)
		{
			PythonIdentifierExpression exp = this.GetNodeValue<PythonIdentifierExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitPythonNamedArgumentList (MParser.PythonNamedArgumentListContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			PythonExpression exp = this.GetNodeValue<PythonExpression> (ctx.exp);
			PythonNamedArgument arg = new PythonNamedArgument (name, exp);
			SetNodeValue (ctx, new PythonArgumentList (arg));
		}

		
		public override void ExitPythonNamedArgumentListItem (MParser.PythonNamedArgumentListItemContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			PythonExpression exp = this.GetNodeValue<PythonExpression> (ctx.exp);
			PythonNamedArgument arg = new PythonNamedArgument (name, exp);
			PythonArgumentList items = this.GetNodeValue<PythonArgumentList> (ctx.items);
			items.Add (arg);
			SetNodeValue (ctx, items);
		}

		
		public override void ExitPythonSelectorExpression (MParser.PythonSelectorExpressionContext ctx)
		{
			PythonExpression parent = this.GetNodeValue<PythonExpression> (ctx.parent);
			PythonSelectorExpression selector = this.GetNodeValue<PythonSelectorExpression> (ctx.child);
			selector.setParent (parent);
			SetNodeValue (ctx, selector);
		}

		
		public override void ExitPythonArgumentList (MParser.PythonArgumentListContext ctx)
		{
			PythonArgumentList ordinal = this.GetNodeValue<PythonArgumentList> (ctx.ordinal);
			PythonArgumentList named = this.GetNodeValue<PythonArgumentList> (ctx.named);
			ordinal.AddRange (named);
			SetNodeValue (ctx, ordinal);
		}

		
		public override void ExitPythonMethodExpression (MParser.PythonMethodExpressionContext ctx)
		{
			PythonMethodExpression exp = this.GetNodeValue<PythonMethodExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		
		public override void ExitPythonNamedOnlyArgumentList (MParser.PythonNamedOnlyArgumentListContext ctx)
		{
			PythonArgumentList named = this.GetNodeValue<PythonArgumentList> (ctx.named);
			SetNodeValue (ctx, named);
		}

		public override void ExitPythonOrdinalArgumentList (MParser.PythonOrdinalArgumentListContext ctx)
		{
			PythonExpression exp = this.GetNodeValue<PythonExpression> (ctx.item);
			PythonOrdinalArgument arg = new PythonOrdinalArgument (exp);
			SetNodeValue (ctx, new PythonArgumentList (arg));
		}


		public override void ExitPythonOrdinalArgumentListItem (MParser.PythonOrdinalArgumentListItemContext ctx)
		{
			PythonExpression exp = this.GetNodeValue<PythonExpression> (ctx.item);
			PythonOrdinalArgument arg = new PythonOrdinalArgument (exp);
			PythonArgumentList items = this.GetNodeValue<PythonArgumentList> (ctx.items);
			items.Add (arg);
			SetNodeValue (ctx, items);
		}

		public override void ExitPythonOrdinalOnlyArgumentList (MParser.PythonOrdinalOnlyArgumentListContext ctx)
		{
			PythonArgumentList ordinal = this.GetNodeValue<PythonArgumentList> (ctx.ordinal);
			SetNodeValue (ctx, ordinal);
		}

		public override void ExitLiteral_expression (MParser.Literal_expressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression>(ctx.GetChild(0));
			SetNodeValue (ctx, exp); 	
		}

		public override void ExitMethod_declaration (MParser.Method_declarationContext ctx)
		{
			IDeclaration exp = this.GetNodeValue<IDeclaration>(ctx.GetChild(0));
			SetNodeValue (ctx, exp); 	
		}

		public override void ExitMethod_identifier (MParser.Method_identifierContext ctx)
		{
			Object exp = this.GetNodeValue<Object>(ctx.GetChild(0));
			SetNodeValue (ctx, exp); 	
		}

		public override void ExitOperator_argument (MParser.Operator_argumentContext ctx)
		{
			IArgument exp = this.GetNodeValue<IArgument>(ctx.GetChild(0));
			SetNodeValue (ctx, exp); 	
		}

		public override void ExitCategory_or_any_type (MParser.Category_or_any_typeContext ctx)
		{
			IType exp = this.GetNodeValue<IType>(ctx.GetChild(0));
			SetNodeValue (ctx, exp); 	
		}

		public override void ExitCollection_literal (MParser.Collection_literalContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression>(ctx.GetChild(0));
			SetNodeValue (ctx, exp); 	
		}

		public override void ExitCursorType (MParser.CursorTypeContext context)
		{
			throw new NotImplementedException();
		}

		public override void ExitEnum_declaration (MParser.Enum_declarationContext ctx)
		{
			IDeclaration exp = this.GetNodeValue<IDeclaration>(ctx.GetChild(0));
			SetNodeValue (ctx, exp); 	
		}

		public override void ExitSymbol_list (MParser.Symbol_listContext context)
		{
			throw new NotImplementedException();
		}


		public override void ExitJsxChild(MParser.JsxChildContext ctx)
		{
			SetNodeValue(ctx, this.GetNodeValue<Object>(ctx.jsx));
		}


		public override void ExitJsxCode(MParser.JsxCodeContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression>(ctx.exp);
			SetNodeValue(ctx, new JsxCode(exp));
		}


		public override void ExitJsxExpression(MParser.JsxExpressionContext ctx)
		{
			SetNodeValue(ctx, this.GetNodeValue<Object>(ctx.exp));
		}


		public override void ExitJsxElement(MParser.JsxElementContext ctx)
		{
			JsxElement elem = this.GetNodeValue<JsxElement>(ctx.jsx);
			List<IJsxExpression> children = this.GetNodeValue<List<IJsxExpression>>(ctx.children_);
			elem.setChildren(children);
			SetNodeValue(ctx, elem);
		}

		public override void ExitJsxSelfClosing(MParser.JsxSelfClosingContext ctx)
		{
			SetNodeValue(ctx, this.GetNodeValue<Object>(ctx.jsx));
		}


		public override void ExitJsxText(MParser.JsxTextContext ctx)
		{
			String text = ParserUtils.GetFullText(ctx.text);
			SetNodeValue(ctx, new JsxText(text));
		}


		public override void ExitJsxValue(MParser.JsxValueContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression>(ctx.exp);
			SetNodeValue(ctx, new JsxExpression(exp));
		}

		public override void ExitJsx_attribute(MParser.Jsx_attributeContext ctx)
		{
			String name = this.GetNodeValue<String>(ctx.name);
			IJsxValue value = this.GetNodeValue<IJsxValue>(ctx.value);
			SetNodeValue(ctx, new JsxAttribute(name, value));
		}


		public override void ExitJsx_children(MParser.Jsx_childrenContext ctx)
		{
			List<IJsxExpression> list = new List<IJsxExpression>();
			foreach (ParserRuleContext child in ctx.jsx_child())
				list.Add(this.GetNodeValue<IJsxExpression>(child));
			SetNodeValue(ctx, list);
		}

		public override void ExitJsx_element_name(MParser.Jsx_element_nameContext ctx)
		{
			String name = ctx.GetText();
			SetNodeValue(ctx, name);
		}

		public override void ExitJsx_expression(MParser.Jsx_expressionContext ctx)
		{
			SetNodeValue(ctx, this.GetNodeValue<Object>(ctx.GetChild(0)));
		}

		public override void ExitJsx_identifier(MParser.Jsx_identifierContext ctx)
		{
			String name = ctx.GetText();
			SetNodeValue(ctx, name);
		}

		public override void ExitJsxLiteral(MParser.JsxLiteralContext ctx)
		{
			String text = ctx.GetText();
			SetNodeValue(ctx, new JsxLiteral(text));
		}

		public override void ExitJsx_opening(MParser.Jsx_openingContext ctx)
		{
			String name = this.GetNodeValue<String>(ctx.name);
			List<JsxAttribute> attributes = new List<JsxAttribute>();
			foreach (ParserRuleContext child in ctx.jsx_attribute())
				attributes.Add(this.GetNodeValue<JsxAttribute>(child));
			SetNodeValue(ctx, new JsxElement(name, attributes));
		}

		public override void ExitJsx_self_closing(MParser.Jsx_self_closingContext ctx)
		{
			String name = this.GetNodeValue<String>(ctx.name);
			List<JsxAttribute> attributes = new List<JsxAttribute>();
			foreach (ParserRuleContext child in ctx.jsx_attribute())
				attributes.Add(this.GetNodeValue<JsxAttribute>(child));
			SetNodeValue(ctx, new JsxSelfClosing(name, attributes));
		}


		public override void ExitCssExpression(MParser.CssExpressionContext ctx)
		{
			SetNodeValue(ctx, this.GetNodeValue<Object>(ctx.exp));
		}


		public override void ExitCss_expression(MParser.Css_expressionContext ctx)
		{
			CssExpression exp = new CssExpression();
			foreach (ParserRuleContext child in ctx.css_field())
			{
				exp.AddField(this.GetNodeValue<CssField>(child));
			}
			SetNodeValue(ctx, exp);
		}


		public override void ExitCss_field(MParser.Css_fieldContext ctx)
		{
			String name = ctx.name.GetText();
			ICssValue value = this.GetNodeValue<ICssValue>(ctx.value);
			SetNodeValue(ctx, new CssField(name, value));
		}



		public override void ExitCssText(MParser.CssTextContext ctx)
		{
			String text = ctx.text.GetText();
			SetNodeValue(ctx, new CssText(text));
		}


		public override void ExitCssValue(MParser.CssValueContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression>(ctx.exp);
			SetNodeValue(ctx, new CssCode(exp));  
		}

	}
}
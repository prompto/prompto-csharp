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
using System.Collections.Generic;
using prompto.javascript;
using prompto.utils;
using prompto.argument;
using prompto.constraint;
using prompto.instance;


namespace prompto.parser
{

	public class OPrestoBuilder : OParserBaseListener
	{

		ParseTreeProperty<object> nodeValues = new ParseTreeProperty<object> ();
		ITokenStream input;
		string path = "";

		public OPrestoBuilder (OCleverParser parser)
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
			section.SetFrom (path, first, last, Dialect.O);
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

		public override void ExitFullDeclarationList (OParser.FullDeclarationListContext ctx)
		{
			DeclarationList items = this.GetNodeValue<DeclarationList> (ctx.declarations());
			if (items == null)
				items = new DeclarationList ();
			SetNodeValue (ctx, items);
		}

		public override void ExitSelectorExpression (OParser.SelectorExpressionContext ctx)
		{
			IExpression parent = this.GetNodeValue<IExpression> (ctx.parent);
			SelectorExpression selector = this.GetNodeValue<SelectorExpression> (ctx.selector);
			selector.setParent (parent);
			SetNodeValue (ctx, selector);
		}

		public override void ExitSelectableExpression (OParser.SelectableExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.parent);
			SetNodeValue (ctx, exp);
		}

		public override void ExitSet_literal (OParser.Set_literalContext ctx)
		{
			ExpressionList items = this.GetNodeValue<ExpressionList>(ctx.expression_list());
			SetLiteral set = items==null ? new SetLiteral() : new SetLiteral(items);
			SetNodeValue(ctx, set);
		}
			
		public override void ExitBlob_expression (OParser.Blob_expressionContext ctx)
		{
			IExpression source = this.GetNodeValue<IExpression> (ctx.expression());
			SetNodeValue (ctx, new BlobExpression(source));
		}

		public override void ExitBlobType (OParser.BlobTypeContext ctx)
		{
			SetNodeValue (ctx, BlobType.Instance);
		}

		public override void ExitBooleanLiteral (OParser.BooleanLiteralContext ctx)
		{
			SetNodeValue (ctx, new BooleanLiteral (ctx.t.Text));
		}

		public override void ExitMinIntegerLiteral (OParser.MinIntegerLiteralContext ctx)
		{
			SetNodeValue (ctx, new MinIntegerLiteral ());
		}

		public override void ExitMaxIntegerLiteral (OParser.MaxIntegerLiteralContext ctx)
		{
			SetNodeValue (ctx, new MaxIntegerLiteral ());
		}

		public override void ExitIntegerLiteral (OParser.IntegerLiteralContext ctx)
		{
			SetNodeValue (ctx, new IntegerLiteral (ctx.t.Text));
		}

		public override void ExitDecimalLiteral (OParser.DecimalLiteralContext ctx)
		{
			SetNodeValue (ctx, new DecimalLiteral (ctx.t.Text));
		}

		public override void ExitHexadecimalLiteral (OParser.HexadecimalLiteralContext ctx)
		{
			SetNodeValue (ctx, new HexaLiteral (ctx.t.Text));
		}

		public override void ExitCharacterLiteral (OParser.CharacterLiteralContext ctx)
		{
			SetNodeValue (ctx, new CharacterLiteral (ctx.t.Text));
		}

		public override void ExitDateLiteral (OParser.DateLiteralContext ctx)
		{
			SetNodeValue (ctx, new DateLiteral (ctx.t.Text));
		}

		public override void ExitDateTimeLiteral (OParser.DateTimeLiteralContext ctx)
		{
			SetNodeValue (ctx, new DateTimeLiteral (ctx.t.Text));
		}

		public override void ExitTernaryExpression (OParser.TernaryExpressionContext ctx)
		{
			IExpression condition = this.GetNodeValue<IExpression> (ctx.test);
			IExpression ifTrue = this.GetNodeValue<IExpression> (ctx.ifTrue);
			IExpression ifFalse = this.GetNodeValue<IExpression> (ctx.ifFalse);
			TernaryExpression exp = new TernaryExpression (condition, ifTrue, ifFalse);
			SetNodeValue (ctx, exp);
		}

		public override void ExitTest_method_declaration(OParser.Test_method_declarationContext ctx) {
			String name = ctx.name.Text;
			StatementList stmts = this.GetNodeValue<StatementList>(ctx.stmts);
			ExpressionList exps = this.GetNodeValue<ExpressionList>(ctx.exps);
			String errorName = this.GetNodeValue<String>(ctx.error);
			SymbolExpression error = errorName==null ? null : new SymbolExpression(errorName);
			SetNodeValue(ctx, new TestMethodDeclaration(name, stmts, exps, error));
		}

		public override void ExitTextLiteral (OParser.TextLiteralContext ctx)
		{
			SetNodeValue (ctx, new TextLiteral (ctx.t.Text));
		}

		public override void ExitTimeLiteral (OParser.TimeLiteralContext ctx)
		{
			SetNodeValue (ctx, new TimeLiteral (ctx.t.Text));
		}

		public override void ExitPeriodLiteral (OParser.PeriodLiteralContext ctx)
		{
			SetNodeValue (ctx, new PeriodLiteral (ctx.t.Text));
		}

		public override void ExitAttribute_identifier (OParser.Attribute_identifierContext ctx)
		{
			SetNodeValue (ctx, ctx.GetText ());
		}


		public override void ExitVariable_identifier (OParser.Variable_identifierContext ctx)
		{
			SetNodeValue (ctx, ctx.GetText ());
		}

		public override void ExitList_literal (OParser.List_literalContext ctx)
		{
			bool mutable = ctx.MUTABLE () != null;
			ExpressionList items = this.GetNodeValue<ExpressionList> (ctx.expression_list());
			IExpression value = items == null ? new ListLiteral (mutable) : new ListLiteral (items, mutable);
			SetNodeValue (ctx, value);
		}

		public override void ExitDict_literal (OParser.Dict_literalContext ctx)
		{
			bool mutable = ctx.MUTABLE () != null;
			DictEntryList items = this.GetNodeValue<DictEntryList> (ctx.dict_entry_list());
			IExpression value = items == null ? new DictLiteral (mutable) : new DictLiteral (items, mutable);
			SetNodeValue (ctx, value);
		}

		public override void ExitTuple_literal (OParser.Tuple_literalContext ctx)
		{
			bool mutable = ctx.MUTABLE() != null;
			ExpressionList items = this.GetNodeValue<ExpressionList> (ctx.expression_tuple());
			IExpression value = items == null ? new TupleLiteral (mutable) : new TupleLiteral (items, mutable);
			SetNodeValue (ctx, value);
		}

		public override void ExitRange_literal (OParser.Range_literalContext ctx)
		{
			IExpression low = this.GetNodeValue<IExpression> (ctx.low);
			IExpression high = this.GetNodeValue<IExpression> (ctx.high);
			SetNodeValue (ctx, new RangeLiteral (low, high));
		}

		public override void ExitDict_entry_list (OParser.Dict_entry_listContext ctx)
		{
			DictEntryList items = new DictEntryList ();
			foreach (ParserRuleContext entry in ctx.dict_entry()) {
				DictEntry item = this.GetNodeValue<DictEntry> (entry);
				items.add (item);
			}
			SetNodeValue (ctx, items);
		}


		public override void ExitDict_entry (OParser.Dict_entryContext ctx)
		{
			IExpression key = this.GetNodeValue<IExpression> (ctx.key);
			IExpression value = this.GetNodeValue<IExpression> (ctx.value);
			DictEntry entry = new DictEntry (key, value);
			SetNodeValue (ctx, entry);
		}

		public override void ExitLiteralExpression (OParser.LiteralExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		public override void ExitIdentifierExpression (OParser.IdentifierExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		public override void ExitVariableIdentifier (OParser.VariableIdentifierContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.variable_identifier());
			SetNodeValue (ctx, new InstanceExpression (name));
		}

		public override void ExitInstanceExpression (OParser.InstanceExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		public override void ExitExpression_list (OParser.Expression_listContext ctx)
		{
			ExpressionList items = new ExpressionList ();
			foreach (ParserRuleContext rule in ctx.expression()) {
				IExpression item = this.GetNodeValue<IExpression> (rule);
				items.add (item);
			}
			SetNodeValue (ctx, items);
		}


		public override void ExitExpression_tuple (OParser.Expression_tupleContext ctx)
		{
			ExpressionList items = new ExpressionList ();
			foreach (ParserRuleContext rule in ctx.expression()) {
				IExpression item = this.GetNodeValue<IExpression> (rule);
				items.add (item);
			}
			SetNodeValue (ctx, items);
		}

		public override void ExitSymbol_identifier (OParser.Symbol_identifierContext ctx)
		{
			SetNodeValue (ctx, ctx.GetText ());
		}

		public override void ExitNative_symbol (OParser.Native_symbolContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new NativeSymbol (name, exp));
		}

		public override void ExitNative_member_method_declaration (OParser.Native_member_method_declarationContext ctx)
		{
			IDeclaration decl = this.GetNodeValue<IDeclaration>(ctx.GetChild(0));
			SetNodeValue(ctx, decl);
		}


		public override void ExitTypeIdentifier (OParser.TypeIdentifierContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.type_identifier());
			SetNodeValue (ctx, new UnresolvedIdentifier (name));
		}

		public override void ExitSymbolIdentifier (OParser.SymbolIdentifierContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.symbol_identifier());
			SetNodeValue (ctx, new SymbolExpression (name));
		}


		public override void ExitBooleanType (OParser.BooleanTypeContext ctx)
		{
			SetNodeValue (ctx, BooleanType.Instance);
		}

		public override void ExitCharacterType (OParser.CharacterTypeContext ctx)
		{
			SetNodeValue (ctx, CharacterType.Instance);
		}

		public override void ExitTextType (OParser.TextTypeContext ctx)
		{
			SetNodeValue (ctx, TextType.Instance);
		}

		public override void ExitThisExpression (OParser.ThisExpressionContext ctx)
		{
			SetNodeValue (ctx, new ThisExpression ());
		}

		public override void ExitIntegerType (OParser.IntegerTypeContext ctx)
		{
			SetNodeValue (ctx, IntegerType.Instance);
		}

		public override void ExitDecimalType (OParser.DecimalTypeContext ctx)
		{
			SetNodeValue (ctx, DecimalType.Instance);
		}

		public override void ExitDateType (OParser.DateTypeContext ctx)
		{
			SetNodeValue (ctx, DateType.Instance);
		}

		public override void ExitDateTimeType (OParser.DateTimeTypeContext ctx)
		{
			SetNodeValue (ctx, DateTimeType.Instance);
		}

		public override void ExitTimeType (OParser.TimeTypeContext ctx)
		{
			SetNodeValue (ctx, TimeType.Instance);
		}

		public override void ExitCodeType (OParser.CodeTypeContext ctx)
		{
			SetNodeValue (ctx, CodeType.Instance);
		}

		public override void ExitPrimaryType (OParser.PrimaryTypeContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.p);
			SetNodeValue (ctx, type);
		}

		public override void ExitAttribute_declaration (OParser.Attribute_declarationContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			IType type = this.GetNodeValue<IType> (ctx.typ);
			IAttributeConstraint match = this.GetNodeValue<IAttributeConstraint> (ctx.match);
			IdentifierList indices = ctx.INDEX()!=null ? new IdentifierList() : null;
			if(ctx.indices!=null)
				indices.AddRange(this.GetNodeValue<IdentifierList>(ctx.indices));
			AttributeDeclaration decl = new AttributeDeclaration (name, type, match, indices);
			decl.Storable = ctx.STORABLE () != null;
			SetNodeValue (ctx, decl);
		}

		public override void ExitNativeType (OParser.NativeTypeContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.n);
			SetNodeValue (ctx, type);
		}

		public override void ExitCategoryType (OParser.CategoryTypeContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.c);
			SetNodeValue (ctx, type);
		}

		public override void ExitCategory_type (OParser.Category_typeContext ctx)
		{
			String name = ctx.GetText ();
			SetNodeValue (ctx, new CategoryType (name));
		}

		public override void ExitListType (OParser.ListTypeContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.l);
			SetNodeValue (ctx, new ListType (type));
		}

		public override void ExitDictType (OParser.DictTypeContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.d);
			SetNodeValue (ctx, new DictType (type));
		}

		public override void ExitAttribute_identifier_list (OParser.Attribute_identifier_listContext ctx)
		{
			IdentifierList list = new IdentifierList ();
			foreach(OParser.Attribute_identifierContext c in ctx.attribute_identifier())
			{
				String item = this.GetNodeValue<String> (c);
				list.Add (item);
			}
			SetNodeValue (ctx, list);
		}

		public override void ExitVariable_identifier_list (OParser.Variable_identifier_listContext ctx)
		{
			IdentifierList list = new IdentifierList ();
			foreach(OParser.Variable_identifierContext c in ctx.variable_identifier())
			{
				String item = this.GetNodeValue<String> (c);
				list.Add (item);
			}
			SetNodeValue (ctx, list);
		}

		public override void ExitConcrete_category_declaration (OParser.Concrete_category_declarationContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			IdentifierList attrs = this.GetNodeValue<IdentifierList> (ctx.attrs);
			IdentifierList derived = this.GetNodeValue<IdentifierList> (ctx.derived);
			MethodDeclarationList methods = this.GetNodeValue<MethodDeclarationList> (ctx.methods);
			ConcreteCategoryDeclaration decl = new ConcreteCategoryDeclaration (name, attrs, derived, methods);
			decl.Storable = ctx.STORABLE () != null;
			SetNodeValue (ctx, decl);
		}

		public override void ExitConcreteCategoryDeclaration (OParser.ConcreteCategoryDeclarationContext ctx)
		{
			ConcreteCategoryDeclaration decl = this.GetNodeValue<ConcreteCategoryDeclaration> (ctx.decl);
			SetNodeValue (ctx, decl);
		}

		public override void ExitDerivedList (OParser.DerivedListContext ctx)
		{
			String item = this.GetNodeValue<String> (ctx.item);
			IdentifierList items = new IdentifierList (item);
			SetNodeValue (ctx, items);
		}

		public override void ExitDerivedListItem (OParser.DerivedListItemContext ctx)
		{
			IdentifierList items = this.GetNodeValue<IdentifierList> (ctx.items);
			String item = this.GetNodeValue<String> (ctx.item);
			items.Add (item);
			SetNodeValue (ctx, items);
		}

		public override void ExitType_identifier (OParser.Type_identifierContext ctx)
		{
			SetNodeValue (ctx, ctx.GetText ());
		}

		public override void ExitType_identifier_list (OParser.Type_identifier_listContext ctx)
		{
			IdentifierList items = new IdentifierList ();
			foreach (ParserRuleContext rule in ctx.type_identifier()) {
				String item = this.GetNodeValue<String> (rule);
				items.Add (item);
			}
			SetNodeValue (ctx, items);
		}


		public override void ExitMemberSelector (OParser.MemberSelectorContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new MemberSelector (name));
		}

		public override void ExitIsAnExpression (OParser.IsAnExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression>(ctx.left);
			IType type = this.GetNodeValue<IType>(ctx.right);
			IExpression right = new TypeExpression(type);
			SetNodeValue(ctx, new EqualsExpression(left, EqOp.IS_A, right));
		}


		public override void ExitIsNotAnExpression (OParser.IsNotAnExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression>(ctx.left);
			IType type = this.GetNodeValue<IType>(ctx.right);
			IExpression right = new TypeExpression(type);
			SetNodeValue(ctx, new EqualsExpression(left, EqOp.IS_NOT_A, right));
		}

		public override void ExitIsExpression(OParser.IsExpressionContext ctx) {
			IExpression left = this.GetNodeValue<IExpression>(ctx.left);
			IExpression right = this.GetNodeValue<IExpression>(ctx.right);
			SetNodeValue(ctx, new EqualsExpression(left, EqOp.IS, right));
		}

		public override void ExitIsNotExpression(OParser.IsNotExpressionContext ctx) {
			IExpression left = this.GetNodeValue<IExpression>(ctx.left);
			IExpression right = this.GetNodeValue<IExpression>(ctx.right);
			SetNodeValue(ctx, new EqualsExpression(left, EqOp.IS_NOT, right));
		}		


		public override void ExitItemSelector (OParser.ItemSelectorContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new ItemSelector (exp));
		}

		public override void ExitSliceSelector (OParser.SliceSelectorContext ctx)
		{
			IExpression slice = this.GetNodeValue<IExpression> (ctx.xslice);
			SetNodeValue (ctx, slice);
		}

		public override void ExitTyped_argument (OParser.Typed_argumentContext ctx)
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


		public override void ExitCodeArgument (OParser.CodeArgumentContext ctx)
		{
			IArgument arg = this.GetNodeValue<IArgument> (ctx.arg);
			SetNodeValue (ctx, arg);
		}


		public override void ExitArgument_list (OParser.Argument_listContext ctx)
		{
			ArgumentList items = new ArgumentList ();
			foreach (ParserRuleContext rule in ctx.argument()) {
				IArgument item = this.GetNodeValue<IArgument> (rule); 
				items.Add (item);
			}
			SetNodeValue (ctx, items);
		}


		public override void ExitMethodName (OParser.MethodNameContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new UnresolvedIdentifier (name));
		}

	
		public override void ExitMethodParent (OParser.MethodParentContext ctx)
		{
			IExpression parent = this.GetNodeValue<IExpression> (ctx.parent);
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new MethodSelector (parent, name));
		}

	
		public override void ExitMethod_call (OParser.Method_callContext ctx)
		{
			IExpression method = this.GetNodeValue<IExpression> (ctx.method);
			ArgumentAssignmentList args = this.GetNodeValue<ArgumentAssignmentList> (ctx.args);
			SetNodeValue (ctx, new UnresolvedCall (method, args));
		}

		public override void ExitExpressionAssignmentList (OParser.ExpressionAssignmentListContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			ArgumentAssignment item = new ArgumentAssignment (null, exp);
			ArgumentAssignmentList items = new ArgumentAssignmentList (item);
			SetNodeValue (ctx, items);
		}

		override 
    public void ExitArgument_assignment (OParser.Argument_assignmentContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			IArgument arg = new UnresolvedArgument (name);
			SetNodeValue (ctx, new ArgumentAssignment (arg, exp));
		}

		override 
    public void ExitArgumentAssignmentList (OParser.ArgumentAssignmentListContext ctx)
		{
			ArgumentAssignment item = this.GetNodeValue<ArgumentAssignment> (ctx.item);
			ArgumentAssignmentList items = new ArgumentAssignmentList (item);
			SetNodeValue (ctx, items);
		}

 
		public override void ExitArgumentAssignmentListItem (OParser.ArgumentAssignmentListItemContext ctx)
		{
			ArgumentAssignment item = this.GetNodeValue<ArgumentAssignment> (ctx.item);
			ArgumentAssignmentList items = this.GetNodeValue<ArgumentAssignmentList> (ctx.items);
			items.add (item);
			SetNodeValue (ctx, items);
		}

		public override void ExitCallableRoot (OParser.CallableRootContext ctx)
		{
			IExpression name = this.GetNodeValue<IExpression>(ctx.name);
			SetNodeValue(ctx, name);
		}

		public override void ExitCallableSelector (OParser.CallableSelectorContext ctx)
		{
			IExpression parent = this.GetNodeValue<IExpression> (ctx.parent);
			SelectorExpression select = this.GetNodeValue<SelectorExpression> (ctx.select);
			select.setParent (parent);
			SetNodeValue (ctx, select);
		}

		public override void ExitCallableMemberSelector (OParser.CallableMemberSelectorContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new MemberSelector (name));
		}

		public override void ExitCallableItemSelector (OParser.CallableItemSelectorContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new ItemSelector (exp));
		}

		public override void ExitAddExpression (OParser.AddExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			IExpression exp = ctx.op.Type == OParser.PLUS ? 
            (IExpression)new AddExpression (left, right) 
            : (IExpression)new SubtractExpression (left, right);
			SetNodeValue (ctx, exp);
		}

		public override void ExitMember_method_declaration_list (OParser.Member_method_declaration_listContext ctx)
		{
			MethodDeclarationList items = new MethodDeclarationList ();
			foreach(ParserRuleContext rule in ctx.member_method_declaration()) {
				IMethodDeclaration item = this.GetNodeValue<IMethodDeclaration> (rule);
				items.add (item);
			}
			SetNodeValue (ctx, items);
		}

		public override void ExitNative_member_method_declaration_list (OParser.Native_member_method_declaration_listContext ctx)
		{
			MethodDeclarationList items = new MethodDeclarationList ();
			foreach(ParserRuleContext rule in ctx.native_member_method_declaration()) {
				IMethodDeclaration item = this.GetNodeValue<IMethodDeclaration> (rule);
				items.add (item);
			}
			SetNodeValue (ctx, items);
		}

		public override void ExitEmptyCategoryMethodList (OParser.EmptyCategoryMethodListContext ctx)
		{
			SetNodeValue (ctx, new MethodDeclarationList ());
		}

		public override void ExitCurlyCategoryMethodList (OParser.CurlyCategoryMethodListContext ctx)
		{
			MethodDeclarationList items = this.GetNodeValue<MethodDeclarationList> (ctx.items);
			SetNodeValue (ctx, items);
		}

		public override void ExitSetter_method_declaration (OParser.Setter_method_declarationContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new SetterMethodDeclaration (name, stmts));
		}

		public override void ExitGetter_method_declaration (OParser.Getter_method_declarationContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new GetterMethodDeclaration (name, stmts));
		}

		public override void ExitNative_setter_declaration (OParser.Native_setter_declarationContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new NativeSetterMethodDeclaration (name, stmts));
		}


		public override void ExitNative_getter_declaration (OParser.Native_getter_declarationContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new NativeGetterMethodDeclaration (name, stmts));
		}



		public override void ExitMember_method_declaration (OParser.Member_method_declarationContext ctx)
		{
			IDeclaration decl = this.GetNodeValue<IDeclaration> (ctx.GetChild(0));
			SetNodeValue (ctx, decl);
		}

		public override void ExitSetType(OParser.SetTypeContext ctx) {
			IType itemType = this.GetNodeValue<IType>(ctx.s);
			SetNodeValue(ctx, new SetType(itemType));
		}


		public override void ExitSingleton_category_declaration(OParser.Singleton_category_declarationContext ctx) {
			String name = this.GetNodeValue<String>(ctx.name);
			IdentifierList attrs = this.GetNodeValue<IdentifierList>(ctx.attrs);
			MethodDeclarationList methods = this.GetNodeValue<MethodDeclarationList>(ctx.methods);
			SetNodeValue(ctx, new SingletonCategoryDeclaration(name, attrs, methods));
		}

		public override void ExitSingletonCategoryDeclaration(OParser.SingletonCategoryDeclarationContext ctx) {
			IDeclaration decl = this.GetNodeValue<IDeclaration>(ctx.decl);
			SetNodeValue(ctx, decl);
		}

		public override void ExitSingleStatement (OParser.SingleStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, new StatementList (stmt));
		}

		public override void ExitCurlyStatementList (OParser.CurlyStatementListContext ctx)
		{
			StatementList items = this.GetNodeValue<StatementList> (ctx.items);
			SetNodeValue (ctx, items);
		}

		public override void ExitStatement_list (OParser.Statement_listContext ctx)
		{
			StatementList items = new StatementList ();
			foreach(ParserRuleContext rule in ctx.statement()) {
				IStatement item = this.GetNodeValue<IStatement> (rule);
				items.add (item);
			}
			SetNodeValue (ctx, items);
		}

		public override void ExitStoreStatement (OParser.StoreStatementContext ctx)
		{
			SetNodeValue (ctx, this.GetNodeValue<Object> (ctx.stmt));
		}

		public override void ExitStore_statement (OParser.Store_statementContext ctx)
		{
			ExpressionList del = this.GetNodeValue<ExpressionList>(ctx.to_del);
			ExpressionList add = this.GetNodeValue<ExpressionList>(ctx.to_add);
			StoreStatement stmt = new StoreStatement(del, add);
			SetNodeValue(ctx, stmt);
		}

		public override void ExitAbstract_method_declaration (OParser.Abstract_method_declarationContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.typ);
			String name = this.GetNodeValue<String> (ctx.name);
			ArgumentList args = this.GetNodeValue<ArgumentList> (ctx.args);
			SetNodeValue (ctx, new AbstractMethodDeclaration (name, args, type));
		}

		public override void ExitConcrete_method_declaration (OParser.Concrete_method_declarationContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.typ);
			String name = this.GetNodeValue<String> (ctx.name);
			ArgumentList args = this.GetNodeValue<ArgumentList> (ctx.args);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new ConcreteMethodDeclaration (name, args, type, stmts));
		}


		public override void ExitMethod_expression (OParser.Method_expressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.GetChild(0));
			SetNodeValue (ctx, exp);
		}


		public override void ExitMethodCallStatement (OParser.MethodCallStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}


		public override void ExitConstructor_expression (OParser.Constructor_expressionContext ctx)
		{
			CategoryType type = this.GetNodeValue<CategoryType> (ctx.typ);
			ArgumentAssignmentList args = this.GetNodeValue<ArgumentAssignmentList> (ctx.args);
			SetNodeValue (ctx, new ConstructorExpression (type, args));
		}

		public override void ExitAn_expression (OParser.An_expressionContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.typ);
			SetNodeValue (ctx, type);
		}

		public override void ExitAssertion(OParser.AssertionContext ctx) {
			IExpression exp = this.GetNodeValue<IExpression>(ctx.exp);
			SetNodeValue(ctx, exp);
		}

		public override void ExitAssertion_list (OParser.Assertion_listContext ctx)
		{
			ExpressionList items = new ExpressionList();
			foreach(ParserRuleContext rule in ctx.assertion()) {
				IExpression item = this.GetNodeValue<IExpression>(rule);
				items.add(item);
			}
			SetNodeValue(ctx, items);
		}


		public override void ExitAssign_instance_statement (OParser.Assign_instance_statementContext ctx)
		{
			IAssignableInstance inst = this.GetNodeValue<IAssignableInstance> (ctx.inst);
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new AssignInstanceStatement (inst, exp));
		}

		public override void ExitAssignInstanceStatement (OParser.AssignInstanceStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		public override void ExitAssign_variable_statement (OParser.Assign_variable_statementContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.variable_identifier());
			IExpression exp = this.GetNodeValue<IExpression> (ctx.expression());
			SetNodeValue (ctx, new AssignVariableStatement (name, exp));
		}

		public override void ExitAssign_tuple_statement (OParser.Assign_tuple_statementContext ctx)
		{
			IdentifierList items = this.GetNodeValue<IdentifierList> (ctx.items);
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new AssignTupleStatement (items, exp));
		}

		public override void ExitRootInstance (OParser.RootInstanceContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.variable_identifier());
			SetNodeValue (ctx, new VariableInstance (name));
		}

		public override void ExitRoughlyEqualsExpression (OParser.RoughlyEqualsExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new EqualsExpression (left, EqOp.ROUGHLY, right));
		}

		public override void ExitChildInstance (OParser.ChildInstanceContext ctx)
		{
			IAssignableInstance parent = this.GetNodeValue<IAssignableInstance> (ctx.assignable_instance());
			IAssignableSelector child = this.GetNodeValue<IAssignableSelector> (ctx.child_instance());
			child.SetParent (parent);
			SetNodeValue (ctx, child);
		}

		public override void ExitMemberInstance (OParser.MemberInstanceContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new MemberInstance (name));
		}

		public override void ExitItemInstance (OParser.ItemInstanceContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new ItemInstance (exp));
		}

		public override void ExitMethodExpression (OParser.MethodExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		public override void ExitNative_statement_list (OParser.Native_statement_listContext ctx)
		{
			StatementList items = new StatementList ();
			foreach(ParserRuleContext rule in ctx.native_statement()) {
				IStatement item = this.GetNodeValue<IStatement> (rule);
				items.add (item);
			}
			SetNodeValue (ctx, items);
		}

		public override void ExitIteratorExpression(OParser.IteratorExpressionContext ctx) 
		{
			IExpression exp = this.GetNodeValue<IExpression>(ctx.exp);
			string name = this.GetNodeValue<string>(ctx.name);
			IExpression source = this.GetNodeValue<IExpression>(ctx.source);
			SetNodeValue(ctx, new IteratorExpression(name, source, exp));
		}

		public override void ExitIteratorType (OParser.IteratorTypeContext ctx)
		{
			IType type = this.GetNodeValue<IType>(ctx.i);
			SetNodeValue(ctx, new IteratorType(type));
		}


		public override void ExitJava_identifier (OParser.Java_identifierContext ctx)
		{
			SetNodeValue (ctx, ctx.GetText ());
		}

		public override void ExitCsharp_identifier (OParser.Csharp_identifierContext ctx)
		{
			SetNodeValue (ctx, ctx.GetText ());
		}

		public override void ExitCSharpPromptoIdentifier (OParser.CSharpPromptoIdentifierContext ctx)
		{
			String name = ctx.DOLLAR_IDENTIFIER().GetText();
			SetNodeValue (ctx, new CSharpIdentifierExpression (name));
		}

		public override void ExitCSharpPrimaryExpression (OParser.CSharpPrimaryExpressionContext ctx)
		{
			CSharpExpression exp = this.GetNodeValue<CSharpExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		public override void ExitPython_identifier (OParser.Python_identifierContext ctx)
		{
			SetNodeValue (ctx, ctx.GetText ());
		}

		public override void ExitPythonPromptoIdentifier (OParser.PythonPromptoIdentifierContext ctx)
		{
			String name = ctx.DOLLAR_IDENTIFIER ().GetText ();
			SetNodeValue (ctx, new PythonIdentifierExpression(name));
		}


		public override void ExitPythonPrimaryExpression (OParser.PythonPrimaryExpressionContext ctx)
		{
			PythonExpression exp = this.GetNodeValue<PythonExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		public override void ExitJavaIdentifier (OParser.JavaIdentifierContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new JavaIdentifierExpression (name));
		}

		public override void ExitJava_primary_expression (OParser.Java_primary_expressionContext ctx)
		{
			JavaExpression exp = this.GetNodeValue<JavaExpression> (ctx.GetChild(0));
			SetNodeValue (ctx, exp);
		}

		public override void ExitJavaPrimaryExpression (OParser.JavaPrimaryExpressionContext ctx)
		{
			JavaExpression exp = this.GetNodeValue<JavaExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		public override void ExitJava_this_expression (OParser.Java_this_expressionContext ctx)
		{
			SetNodeValue (ctx, new JavaThisExpression());
		}


		public override void ExitCSharpIdentifier (OParser.CSharpIdentifierContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new CSharpIdentifierExpression (name));
		}

		public override void ExitPythonIdentifier (OParser.PythonIdentifierContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new PythonIdentifierExpression (name));
		}

		public override void ExitJavaChildIdentifier (OParser.JavaChildIdentifierContext ctx)
		{
			JavaIdentifierExpression parent = this.GetNodeValue<JavaIdentifierExpression> (ctx.parent);
			String name = this.GetNodeValue<String> (ctx.name);
			JavaIdentifierExpression child = new JavaIdentifierExpression (parent, name);
			SetNodeValue (ctx, child);
		}

		public override void ExitCSharpChildIdentifier (OParser.CSharpChildIdentifierContext ctx)
		{
			CSharpIdentifierExpression parent = this.GetNodeValue<CSharpIdentifierExpression> (ctx.parent);
			String name = this.GetNodeValue<String> (ctx.name);
			CSharpIdentifierExpression child = new CSharpIdentifierExpression (parent, name);
			SetNodeValue (ctx, child);
		}

		public override void ExitPythonCharacterLiteral (OParser.PythonCharacterLiteralContext ctx)
		{
			SetNodeValue (ctx, new PythonCharacterLiteral (ctx.t.Text));
		}

		public override void ExitPythonChildIdentifier (OParser.PythonChildIdentifierContext ctx)
		{
			PythonIdentifierExpression parent = this.GetNodeValue<PythonIdentifierExpression> (ctx.parent);
			String name = this.GetNodeValue<String> (ctx.name);
			PythonIdentifierExpression child = new PythonIdentifierExpression (parent, name);
			SetNodeValue (ctx, child);
		}

	
		public override void ExitJavaClassIdentifier (OParser.JavaClassIdentifierContext ctx)
		{
			JavaIdentifierExpression klass = this.GetNodeValue<JavaIdentifierExpression> (ctx.klass);
			SetNodeValue (ctx, klass);
		}

		public override void ExitJavaChildClassIdentifier (OParser.JavaChildClassIdentifierContext ctx)
		{
			JavaIdentifierExpression parent = this.GetNodeValue<JavaIdentifierExpression> (ctx.parent);
			JavaIdentifierExpression child = new JavaIdentifierExpression (parent, ctx.name.Text);
			SetNodeValue (ctx, child);
		}

		public override void ExitJavaSelectorExpression (OParser.JavaSelectorExpressionContext ctx)
		{
			JavaExpression parent = this.GetNodeValue<JavaExpression> (ctx.parent);
			JavaSelectorExpression child = this.GetNodeValue<JavaSelectorExpression> (ctx.child);
			child.SetParent (parent);
			SetNodeValue (ctx, child);
		}

		public override void ExitJavaStatement (OParser.JavaStatementContext ctx)
		{
			JavaExpression exp = this.GetNodeValue<JavaExpression> (ctx.exp);
			SetNodeValue (ctx, new JavaStatement (exp, false));
		}

		public override void ExitCSharpStatement (OParser.CSharpStatementContext ctx)
		{
			CSharpExpression exp = this.GetNodeValue<CSharpExpression> (ctx.exp);
			SetNodeValue (ctx, new CSharpStatement (exp, false));
		}

		public override void ExitPythonStatement (OParser.PythonStatementContext ctx)
		{
			PythonExpression exp = this.GetNodeValue<PythonExpression> (ctx.exp);
			SetNodeValue (ctx, new PythonStatement (exp, false));
		}

		public override void ExitJavaReturnStatement (OParser.JavaReturnStatementContext ctx)
		{
			JavaExpression exp = this.GetNodeValue<JavaExpression> (ctx.exp);
			SetNodeValue (ctx, new JavaStatement (exp, true));
		}

		public override void ExitCSharpReturnStatement (OParser.CSharpReturnStatementContext ctx)
		{
			CSharpExpression exp = this.GetNodeValue<CSharpExpression> (ctx.exp);
			SetNodeValue (ctx, new CSharpStatement (exp, true));
		}

		public override void ExitPythonReturnStatement (OParser.PythonReturnStatementContext ctx)
		{
			PythonExpression exp = this.GetNodeValue<PythonExpression> (ctx.exp);
			SetNodeValue (ctx, new PythonStatement (exp, true));
		}

		public override void ExitJavaNativeStatement (OParser.JavaNativeStatementContext ctx)
		{
			JavaStatement stmt = this.GetNodeValue<JavaStatement> (ctx.java_statement());
			SetNodeValue (ctx, new JavaNativeCall (stmt));
		}

		public override void ExitCSharpNativeStatement (OParser.CSharpNativeStatementContext ctx)
		{
			CSharpStatement stmt = this.GetNodeValue<CSharpStatement> (ctx.csharp_statement());
			SetNodeValue (ctx, new CSharpNativeCall (stmt));
		}

		public override void ExitPython2NativeStatement (OParser.Python2NativeStatementContext ctx)
		{
			PythonStatement stmt = this.GetNodeValue<PythonStatement> (ctx.python_native_statement());
			SetNodeValue (ctx, new Python2NativeCall (stmt));
		}

		public override void ExitPython3NativeStatement (OParser.Python3NativeStatementContext ctx)
		{
			PythonStatement stmt = this.GetNodeValue<PythonStatement> (ctx.python_native_statement());
			SetNodeValue (ctx, new Python3NativeCall (stmt));
		}

		public override void ExitNative_method_declaration (OParser.Native_method_declarationContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.typ);
			String name = this.GetNodeValue<String> (ctx.name);
			ArgumentList args = this.GetNodeValue<ArgumentList> (ctx.args);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new NativeMethodDeclaration (name, args, type, stmts));
		}

		public override void ExitJavaArgumentList (OParser.JavaArgumentListContext ctx)
		{
			JavaExpression item = this.GetNodeValue<JavaExpression> (ctx.item);
			SetNodeValue (ctx, new JavaExpressionList (item));
		}

		public override void ExitJavaArgumentListItem (OParser.JavaArgumentListItemContext ctx)
		{
			JavaExpression item = this.GetNodeValue<JavaExpression> (ctx.item);
			JavaExpressionList items = this.GetNodeValue<JavaExpressionList> (ctx.items);
			items.Add (item);
			SetNodeValue (ctx, items);
		}

		public override void ExitJava_method_expression (OParser.Java_method_expressionContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			JavaExpressionList args = this.GetNodeValue<JavaExpressionList> (ctx.args);
			SetNodeValue (ctx, new JavaMethodExpression (name, args));
		}

		public override void ExitJavaMethodExpression (OParser.JavaMethodExpressionContext ctx)
		{
			JavaExpression exp = this.GetNodeValue<JavaExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		public override void ExitDeclaration (OParser.DeclarationContext ctx)
		{

			List<CommentStatement> stmts = null;
			foreach(OParser.Comment_statementContext csc in ctx.comment_statement()) {
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

		public override void ExitDeclarations (OParser.DeclarationsContext ctx)
		{
			DeclarationList items = new DeclarationList ();
			foreach(ParserRuleContext rule in ctx.declaration()) {
				IDeclaration item = this.GetNodeValue<IDeclaration> (rule);
				items.Add (item);
			}
			SetNodeValue (ctx, items);
		}


		public override void ExitJavaBooleanLiteral (OParser.JavaBooleanLiteralContext ctx)
		{
			SetNodeValue (ctx, new JavaBooleanLiteral (ctx.GetText ()));
		}

		public override void ExitJavaIntegerLiteral (OParser.JavaIntegerLiteralContext ctx)
		{
			SetNodeValue (ctx, new JavaIntegerLiteral (ctx.GetText ()));
		}

		public override void ExitJavaDecimalLiteral (OParser.JavaDecimalLiteralContext ctx)
		{
			SetNodeValue (ctx, new JavaDecimalLiteral (ctx.GetText ()));
		}

		public override void ExitJavaCharacterLiteral (OParser.JavaCharacterLiteralContext ctx)
		{
			SetNodeValue (ctx, new JavaCharacterLiteral (ctx.GetText ()));
		}

		public override void ExitJavaTextLiteral (OParser.JavaTextLiteralContext ctx)
		{
			SetNodeValue (ctx, new JavaTextLiteral (ctx.GetText ()));
		}

		public override void ExitCSharpBooleanLiteral (OParser.CSharpBooleanLiteralContext ctx)
		{
			SetNodeValue (ctx, new CSharpBooleanLiteral (ctx.GetText ()));
		}

		public override void ExitCSharpIntegerLiteral (OParser.CSharpIntegerLiteralContext ctx)
		{
			SetNodeValue (ctx, new CSharpIntegerLiteral (ctx.GetText ()));
		}

		public override void ExitCSharpDecimalLiteral (OParser.CSharpDecimalLiteralContext ctx)
		{
			SetNodeValue (ctx, new CSharpDecimalLiteral (ctx.GetText ()));
		}

		public override void ExitCSharpCharacterLiteral (OParser.CSharpCharacterLiteralContext ctx)
		{
			SetNodeValue (ctx, new CSharpCharacterLiteral (ctx.GetText ()));
		}

		public override void ExitCSharpTextLiteral (OParser.CSharpTextLiteralContext ctx)
		{
			SetNodeValue (ctx, new CSharpTextLiteral (ctx.GetText ()));
		}

		public override void ExitPythonBooleanLiteral (OParser.PythonBooleanLiteralContext ctx)
		{
			SetNodeValue (ctx, new PythonBooleanLiteral (ctx.GetText ()));
		}

		public override void ExitPythonIntegerLiteral (OParser.PythonIntegerLiteralContext ctx)
		{
			SetNodeValue (ctx, new PythonIntegerLiteral (ctx.GetText ()));
		}

		public override void ExitPythonDecimalLiteral (OParser.PythonDecimalLiteralContext ctx)
		{
			SetNodeValue (ctx, new PythonDecimalLiteral (ctx.GetText ()));
		}

		public override void ExitPythonTextLiteral (OParser.PythonTextLiteralContext ctx)
		{
			SetNodeValue (ctx, new PythonTextLiteral (ctx.GetText ()));
		}

		public override void ExitPythonLiteralExpression (OParser.PythonLiteralExpressionContext ctx)
		{
			PythonExpression exp = this.GetNodeValue<PythonExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		public override void ExitJavaCategoryBinding (OParser.JavaCategoryBindingContext ctx)
		{
			JavaIdentifierExpression map = this.GetNodeValue<JavaIdentifierExpression> (ctx.binding);
			SetNodeValue (ctx, new JavaNativeCategoryBinding (map));
		}

		public override void ExitCSharpCategoryBinding (OParser.CSharpCategoryBindingContext ctx)
		{
			CSharpIdentifierExpression map = this.GetNodeValue<CSharpIdentifierExpression> (ctx.binding);
			SetNodeValue (ctx, new CSharpNativeCategoryBinding (map));
		}

		public override void ExitPython_module (OParser.Python_moduleContext ctx)
		{
			List<String> ids = new List<String> ();
			foreach (OParser.IdentifierContext ic in ctx.identifier())
				ids.Add (ic.GetText ());
			PythonModule module = new PythonModule (ids);
			SetNodeValue (ctx, module);
		}

		public override void ExitPython_native_statement (OParser.Python_native_statementContext ctx)
		{
			PythonStatement stmt = this.GetNodeValue<PythonStatement> (ctx.python_statement());
			PythonModule module = this.GetNodeValue<PythonModule> (ctx.python_module());
			stmt.setModule (module);
			SetNodeValue (ctx, stmt);
		}

		public override void ExitPython2CategoryBinding (OParser.Python2CategoryBindingContext ctx)
		{
			PythonNativeCategoryBinding map = this.GetNodeValue<PythonNativeCategoryBinding> (ctx.binding);
			SetNodeValue (ctx, new Python2NativeCategoryBinding (map));
		}

		public override void ExitPython3CategoryBinding (OParser.Python3CategoryBindingContext ctx)
		{
			PythonNativeCategoryBinding map = this.GetNodeValue<PythonNativeCategoryBinding> (ctx.binding);
			SetNodeValue (ctx, new Python3NativeCategoryBinding (map));
		}

		public override void ExitPython_category_binding (OParser.Python_category_bindingContext ctx)
		{
			String identifier = ctx.identifier ().GetText ();
			PythonModule module = this.GetNodeValue<PythonModule> (ctx.python_module ());
			PythonNativeCategoryBinding map = new PythonNativeCategoryBinding (identifier, module);
			SetNodeValue (ctx, map);
		}

		override 
	public void ExitPythonGlobalMethodExpression (OParser.PythonGlobalMethodExpressionContext ctx)
		{
			PythonMethodExpression exp = this.GetNodeValue<PythonMethodExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		public override void ExitPython_method_expression (OParser.Python_method_expressionContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			PythonArgumentList args = this.GetNodeValue<PythonArgumentList> (ctx.args);
			PythonMethodExpression method = new PythonMethodExpression (name);
			method.setArguments (args);
			SetNodeValue (ctx, method);
		}

		public override void ExitPythonIdentifierExpression (OParser.PythonIdentifierExpressionContext ctx)
		{
			PythonIdentifierExpression exp = this.GetNodeValue<PythonIdentifierExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		public override void ExitPythonNamedArgumentList (OParser.PythonNamedArgumentListContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			PythonExpression exp = this.GetNodeValue<PythonExpression> (ctx.exp);
			PythonNamedArgument arg = new PythonNamedArgument (name, exp);
			SetNodeValue (ctx, new PythonArgumentList (arg));
		}

		public override void ExitPythonNamedArgumentListItem (OParser.PythonNamedArgumentListItemContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			PythonExpression exp = this.GetNodeValue<PythonExpression> (ctx.exp);
			PythonNamedArgument arg = new PythonNamedArgument (name, exp);
			PythonArgumentList items = this.GetNodeValue<PythonArgumentList> (ctx.items);
			items.Add (arg);
			SetNodeValue (ctx, items);
		}

		public override void ExitPythonOrdinalArgumentList (OParser.PythonOrdinalArgumentListContext ctx)
		{
			PythonExpression exp = this.GetNodeValue<PythonExpression> (ctx.item);
			PythonOrdinalArgument arg = new PythonOrdinalArgument (exp);
			SetNodeValue (ctx, new PythonArgumentList (arg));
		}


		public override void ExitPythonOrdinalArgumentListItem (OParser.PythonOrdinalArgumentListItemContext ctx)
		{
			PythonExpression exp = this.GetNodeValue<PythonExpression> (ctx.item);
			PythonOrdinalArgument arg = new PythonOrdinalArgument (exp);
			PythonArgumentList items = this.GetNodeValue<PythonArgumentList> (ctx.items);
			items.Add (arg);
			SetNodeValue (ctx, items);
		}

		public override void ExitPythonOrdinalOnlyArgumentList (OParser.PythonOrdinalOnlyArgumentListContext ctx)
		{
			PythonArgumentList ordinal = this.GetNodeValue<PythonArgumentList> (ctx.ordinal);
			SetNodeValue (ctx, ordinal);
		}

		public override void ExitPythonSelectorExpression (OParser.PythonSelectorExpressionContext ctx)
		{
			PythonExpression parent = this.GetNodeValue<PythonExpression> (ctx.parent);
			PythonSelectorExpression selector = this.GetNodeValue<PythonSelectorExpression> (ctx.child);
			selector.setParent (parent);
			SetNodeValue (ctx, selector);
		}

		public override void ExitPythonArgumentList (OParser.PythonArgumentListContext ctx)
		{
			PythonArgumentList ordinal = this.GetNodeValue<PythonArgumentList> (ctx.ordinal);
			PythonArgumentList named = this.GetNodeValue<PythonArgumentList> (ctx.named);
			ordinal.AddRange (named);
			SetNodeValue (ctx, ordinal);
		}

		public override void ExitPythonMethodExpression (OParser.PythonMethodExpressionContext ctx)
		{
			PythonMethodExpression exp = this.GetNodeValue<PythonMethodExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		public override void ExitPythonNamedOnlyArgumentList (OParser.PythonNamedOnlyArgumentListContext ctx)
		{
			PythonArgumentList named = this.GetNodeValue<PythonArgumentList> (ctx.named);
			SetNodeValue (ctx, named);
		}

		public override void ExitNativeCategoryBindingList (OParser.NativeCategoryBindingListContext ctx)
		{
			NativeCategoryBinding item = this.GetNodeValue<NativeCategoryBinding> (ctx.item);
			NativeCategoryBindingList items = new NativeCategoryBindingList (item);
			SetNodeValue (ctx, items);
		}

		public override void ExitNativeCategoryBindingListItem (OParser.NativeCategoryBindingListItemContext ctx)
		{
			NativeCategoryBinding item = this.GetNodeValue<NativeCategoryBinding> (ctx.item);
			NativeCategoryBindingList items = this.GetNodeValue<NativeCategoryBindingList> (ctx.items);
			items.Add (item);
			SetNodeValue (ctx, items);
		}

		public override void ExitNative_category_bindings (OParser.Native_category_bindingsContext ctx)
		{
			NativeCategoryBindingList items = this.GetNodeValue<NativeCategoryBindingList> (ctx.items);
			SetNodeValue (ctx, items);
		}

		public override void ExitNative_category_declaration (OParser.Native_category_declarationContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			IdentifierList attrs = this.GetNodeValue<IdentifierList> (ctx.attrs);
			NativeCategoryBindingList bindings = this.GetNodeValue<NativeCategoryBindingList> (ctx.bindings);
			MethodDeclarationList methods = this.GetNodeValue<MethodDeclarationList> (ctx.methods);
			NativeCategoryDeclaration decl = new NativeCategoryDeclaration (name, attrs, bindings, null, methods);
			decl.Storable = ctx.STORABLE () != null;
			SetNodeValue (ctx, decl);
		}

		public override void ExitNativeCategoryDeclaration (OParser.NativeCategoryDeclarationContext ctx)
		{
			IDeclaration decl = this.GetNodeValue<IDeclaration> (ctx.decl);
			SetNodeValue (ctx, decl);
		}

		public override void ExitNative_resource_declaration (OParser.Native_resource_declarationContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			IdentifierList attrs = this.GetNodeValue<IdentifierList> (ctx.attrs);
			NativeCategoryBindingList bindings = this.GetNodeValue<NativeCategoryBindingList> (ctx.bindings);
			MethodDeclarationList methods = this.GetNodeValue<MethodDeclarationList> (ctx.methods);
			SetNodeValue (ctx, new NativeResourceDeclaration (name, attrs, bindings, null, methods));
		}

		public override void ExitResource_declaration (OParser.Resource_declarationContext ctx)
		{
			IDeclaration decl = this.GetNodeValue<IDeclaration> (ctx.native_resource_declaration());
			SetNodeValue (ctx, decl);
		}

		public override void ExitParenthesis_expression (OParser.Parenthesis_expressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.expression());
			SetNodeValue (ctx, new ParenthesisExpression (exp));
		}

		public override void ExitParenthesisExpression (OParser.ParenthesisExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		public override void ExitNative_symbol_list (OParser.Native_symbol_listContext ctx)
		{
			NativeSymbolList items = new NativeSymbolList ();
			foreach(ParserRuleContext rule in ctx.native_symbol()) {
				NativeSymbol item = this.GetNodeValue<NativeSymbol> (rule);
				items.add (item);
			}
			SetNodeValue (ctx, items);
		}

		public override void ExitEnum_native_declaration (OParser.Enum_native_declarationContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			NativeType type = this.GetNodeValue<NativeType> (ctx.typ);
			NativeSymbolList symbols = this.GetNodeValue<NativeSymbolList> (ctx.symbols);
			SetNodeValue (ctx, new EnumeratedNativeDeclaration (name, type, symbols));
		}

		public override void ExitFor_each_statement (OParser.For_each_statementContext ctx)
		{
			String name1 = this.GetNodeValue<String> (ctx.name1);
			String name2 = this.GetNodeValue<String> (ctx.name2);
			IExpression source = this.GetNodeValue<IExpression> (ctx.source);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new ForEachStatement (name1, name2, source, stmts));
		}

		public override void ExitForEachStatement (OParser.ForEachStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		public override void ExitKey_token (OParser.Key_tokenContext ctx)
		{
			SetNodeValue (ctx, ctx.GetText ());
		}

		public override void ExitValue_token (OParser.Value_tokenContext ctx)
		{
			SetNodeValue (ctx, ctx.GetText ());
		}

		public override void ExitNamed_argument (OParser.Named_argumentContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.variable_identifier());
			UnresolvedArgument arg = new UnresolvedArgument(name);
			IExpression exp = this.GetNodeValue<IExpression>(ctx.literal_expression());
			arg.DefaultValue = exp;
			SetNodeValue(ctx, arg);
		}

		public override void ExitClosureStatement (OParser.ClosureStatementContext ctx)
		{
			ConcreteMethodDeclaration decl = this.GetNodeValue<ConcreteMethodDeclaration> (ctx.decl);
			SetNodeValue (ctx, new DeclarationStatement<ConcreteMethodDeclaration> (decl));
		}

		public override void ExitReturn_statement (OParser.Return_statementContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new ReturnStatement (exp));
		}

		public override void ExitReturnStatement (OParser.ReturnStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		public override void ExitClosure_expression (OParser.Closure_expressionContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new MethodExpression (name));
		}

		public override void ExitClosureExpression (OParser.ClosureExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		public override void ExitIf_statement (OParser.If_statementContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			IfElementList elseIfs = this.GetNodeValue<IfElementList> (ctx.elseIfs);
			StatementList elseStmts = this.GetNodeValue<StatementList> (ctx.elseStmts);
			SetNodeValue (ctx, new IfStatement (exp, stmts, elseIfs, elseStmts));
		}

		public override void ExitElseIfStatementList (OParser.ElseIfStatementListContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			IfElement elem = new IfElement (exp, stmts);
			SetNodeValue (ctx, new IfElementList (elem));
		}

		public override void ExitElseIfStatementListItem (OParser.ElseIfStatementListItemContext ctx)
		{
			IfElementList items = this.GetNodeValue<IfElementList> (ctx.items);
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			IfElement elem = new IfElement (exp, stmts);
			items.Add (elem);
			SetNodeValue (ctx, items);
		}

		public override void ExitIfStatement (OParser.IfStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		public override void ExitSwitchStatement (OParser.SwitchStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		public override void ExitAssignTupleStatement (OParser.AssignTupleStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		public override void ExitRaiseStatement (OParser.RaiseStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		public override void ExitWriteStatement (OParser.WriteStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}


		public override void ExitWith_singleton_statement(OParser.With_singleton_statementContext ctx) {
			String name = this.GetNodeValue<String>(ctx.typ);
			CategoryType type = new CategoryType(name);
			StatementList stmts = this.GetNodeValue<StatementList>(ctx.stmts);
			SetNodeValue(ctx, new WithSingletonStatement(type, stmts));
		}

		public override void ExitWithSingletonStatement(OParser.WithSingletonStatementContext ctx) {
			IStatement stmt = this.GetNodeValue<IStatement>(ctx.stmt);
			SetNodeValue(ctx, stmt);
		}


		public override void ExitWithResourceStatement (OParser.WithResourceStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		public override void ExitWhileStatement (OParser.WhileStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		public override void ExitDoWhileStatement (OParser.DoWhileStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		public override void ExitTryStatement (OParser.TryStatementContext ctx)
		{
			IStatement stmt = this.GetNodeValue<IStatement> (ctx.stmt);
			SetNodeValue (ctx, stmt);
		}

		public override void ExitEqualsExpression (OParser.EqualsExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new EqualsExpression (left, EqOp.EQUALS, right));
		}

		public override void ExitNotEqualsExpression (OParser.NotEqualsExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new EqualsExpression (left, EqOp.NOT_EQUALS, right));
		}

		public override void ExitGreaterThanExpression (OParser.GreaterThanExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new CompareExpression (left, CmpOp.GT, right));
		}

		public override void ExitGreaterThanOrEqualExpression (OParser.GreaterThanOrEqualExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new CompareExpression (left, CmpOp.GTE, right));
		}

		public override void ExitLessThanExpression (OParser.LessThanExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new CompareExpression (left, CmpOp.LT, right));
		}

		public override void ExitLessThanOrEqualExpression (OParser.LessThanOrEqualExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new CompareExpression (left, CmpOp.LTE, right));
		}

		public override void ExitAtomicSwitchCase (OParser.AtomicSwitchCaseContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new AtomicSwitchCase (exp, stmts));
		}

		public override void ExitCommentStatement(OParser.CommentStatementContext ctx) {
			SetNodeValue(ctx, this.GetNodeValue<Object>(ctx.comment_statement()));
		}

		public override void ExitComment_statement(OParser.Comment_statementContext ctx) {
			SetNodeValue(ctx, new CommentStatement(ctx.GetText()));
		}


		public override void ExitCollectionSwitchCase (OParser.CollectionSwitchCaseContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new CollectionSwitchCase (exp, stmts));
		}

		public override void ExitSwitch_case_statement_list (OParser.Switch_case_statement_listContext ctx)
		{
			SwitchCaseList items = new SwitchCaseList ();
			foreach(ParserRuleContext rule in ctx.switch_case_statement()) {
				SwitchCase item = this.GetNodeValue<SwitchCase> (rule);
				items.Add (item);
			}
			SetNodeValue (ctx, items);
		}

		public override void ExitSwitch_statement (OParser.Switch_statementContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SwitchCaseList cases = this.GetNodeValue<SwitchCaseList> (ctx.cases);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SwitchStatement stmt = new SwitchStatement (exp, cases, stmts);
			SetNodeValue (ctx, stmt);
		}

		public override void ExitLiteralRangeLiteral (OParser.LiteralRangeLiteralContext ctx)
		{
			IExpression low = this.GetNodeValue<IExpression> (ctx.low);
			IExpression high = this.GetNodeValue<IExpression> (ctx.high);
			SetNodeValue (ctx, new RangeLiteral (low, high));
		}

		public override void ExitLiteralSetLiteral (OParser.LiteralSetLiteralContext ctx)
		{
			ExpressionList items = this.GetNodeValue<ExpressionList>(ctx.literal_list_literal());
			SetNodeValue(ctx, new SetLiteral(items));
		}

		public override void ExitLiteralListLiteral (OParser.LiteralListLiteralContext ctx)
		{
			ExpressionList exp = this.GetNodeValue<ExpressionList> (ctx.literal_list_literal());
			SetNodeValue (ctx, new ListLiteral (exp, false));
		}

		public override void ExitLiteral_list_literal (OParser.Literal_list_literalContext ctx)
		{
			ExpressionList items = new ExpressionList ();
			foreach(ParserRuleContext rule in ctx.atomic_literal()) {
				IExpression item = this.GetNodeValue<IExpression> (rule);
				items.add (item);
			}
			SetNodeValue (ctx, items);
		}


		public override void ExitInExpression (OParser.InExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new ContainsExpression (left, ContOp.IN, right));
		}

		public override void ExitNotInExpression (OParser.NotInExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new ContainsExpression (left, ContOp.NOT_IN, right));
		}

		public override void ExitContainsAllExpression (OParser.ContainsAllExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new ContainsExpression (left, ContOp.CONTAINS_ALL, right));
		}

		public override void ExitNotContainsAllExpression (OParser.NotContainsAllExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new ContainsExpression (left, ContOp.NOT_CONTAINS_ALL, right));
		}

		public override void ExitContainsAnyExpression (OParser.ContainsAnyExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new ContainsExpression (left, ContOp.CONTAINS_ANY, right));
		}

		public override void ExitNotContainsAnyExpression (OParser.NotContainsAnyExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new ContainsExpression (left, ContOp.NOT_CONTAINS_ANY, right));
		}

		public override void ExitContainsExpression (OParser.ContainsExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new ContainsExpression (left, ContOp.CONTAINS, right));
		}

		public override void ExitNotContainsExpression (OParser.NotContainsExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new ContainsExpression (left, ContOp.NOT_CONTAINS, right));
		}

		public override void ExitDivideExpression (OParser.DivideExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new DivideExpression (left, right));
		}

		public override void ExitIntDivideExpression (OParser.IntDivideExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new IntDivideExpression (left, right));
		}

		public override void ExitModuloExpression (OParser.ModuloExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new ModuloExpression (left, right));
		}

		public override void ExitAndExpression (OParser.AndExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new AndExpression (left, right));
		}

		public override void ExitNullLiteral (OParser.NullLiteralContext ctx)
		{
			SetNodeValue (ctx, NullLiteral.Instance);
		}

		public override void ExitOperatorArgument(OParser.OperatorArgumentContext ctx) {
			bool mutable = ctx.MUTABLE () != null;
			IArgument arg = this.GetNodeValue<IArgument>(ctx.arg);
			arg.setMutable (mutable);
			SetNodeValue(ctx, arg);
		}

		public override void ExitOperatorPlus(OParser.OperatorPlusContext ctx) {
			SetNodeValue(ctx, Operator.PLUS);
		}

		public override void ExitOperatorMinus(OParser.OperatorMinusContext ctx) {
			SetNodeValue(ctx, Operator.MINUS);
		}

		public override void ExitOperatorMultiply(OParser.OperatorMultiplyContext ctx) {
			SetNodeValue(ctx, Operator.MULTIPLY);
		}

		public override void ExitOperatorDivide(OParser.OperatorDivideContext ctx) {
			SetNodeValue(ctx, Operator.DIVIDE);
		}

		public override void ExitOperatorIDivide(OParser.OperatorIDivideContext ctx) {
			SetNodeValue(ctx, Operator.IDIVIDE);
		}

		public override void ExitOperatorModulo(OParser.OperatorModuloContext ctx) {
			SetNodeValue(ctx, Operator.MODULO);
		}

		public override void ExitOperator_method_declaration(OParser.Operator_method_declarationContext ctx) {
			Operator op = this.GetNodeValue<Operator>(ctx.op);
			IArgument arg = this.GetNodeValue<IArgument>(ctx.arg);
			IType typ = this.GetNodeValue<IType>(ctx.typ);
			StatementList stmts = this.GetNodeValue<StatementList>(ctx.stmts);
			OperatorMethodDeclaration decl = new OperatorMethodDeclaration(op, arg, typ, stmts);
			SetNodeValue(ctx, decl);
		}

		public override void ExitOrder_by(OParser.Order_byContext ctx) {
			IdentifierList names = new IdentifierList();
			foreach(OParser.Variable_identifierContext ctx_ in ctx.variable_identifier())
				names.add(this.GetNodeValue<string>(ctx_));
			OrderByClause clause = new OrderByClause(names, ctx.DESC()!=null);
			SetNodeValue(ctx, clause);
		}

		public override void ExitOrder_by_list(OParser.Order_by_listContext ctx) {
			OrderByClauseList list = new OrderByClauseList();
			foreach(OParser.Order_byContext ctx_ in ctx.order_by())
				list.add(this.GetNodeValue<OrderByClause>(ctx_));
			SetNodeValue(ctx, list);
		}

	
		public override void ExitOrExpression (OParser.OrExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new OrExpression (left, right));
		}

		public override void ExitMultiplyExpression (OParser.MultiplyExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IExpression right = this.GetNodeValue<IExpression> (ctx.right);
			SetNodeValue (ctx, new MultiplyExpression (left, right));
		}

		public override void ExitMutable_category_type (OParser.Mutable_category_typeContext ctx)
		{
			CategoryType typ = this.GetNodeValue<CategoryType> (ctx.category_type ());
			typ.Mutable = ctx.MUTABLE() != null;
			SetNodeValue (ctx, typ);
		}

		public override void ExitMinusExpression (OParser.MinusExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new MinusExpression (exp));
		}

		public override void ExitNotExpression (OParser.NotExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new NotExpression (exp));
		}

		public override void ExitWhile_statement (OParser.While_statementContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new WhileStatement (exp, stmts));
		}

		public override void ExitDo_while_statement (OParser.Do_while_statementContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new DoWhileStatement (exp, stmts));
		}

		public override void ExitSliceFirstAndLast (OParser.SliceFirstAndLastContext ctx)
		{
			IExpression first = this.GetNodeValue<IExpression> (ctx.first);
			IExpression last = this.GetNodeValue<IExpression> (ctx.last);
			SetNodeValue (ctx, new SliceSelector (first, last));
		}

		public override void ExitSliceFirstOnly (OParser.SliceFirstOnlyContext ctx)
		{
			IExpression first = this.GetNodeValue<IExpression> (ctx.first);
			SetNodeValue (ctx, new SliceSelector (first, null));
		}

		public override void ExitSliceLastOnly (OParser.SliceLastOnlyContext ctx)
		{
			IExpression last = this.GetNodeValue<IExpression> (ctx.last);
			SetNodeValue (ctx, new SliceSelector (null, last));
		}

		public override void ExitSorted_expression (OParser.Sorted_expressionContext ctx)
		{
			IExpression source = this.GetNodeValue<IExpression> (ctx.source);
			IExpression key = this.GetNodeValue<IExpression> (ctx.key);
			SetNodeValue (ctx, new SortedExpression (source, key));
		}
			
		public override void ExitDocument_expression (OParser.Document_expressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression>(ctx.expression());
			SetNodeValue (ctx, new DocumentExpression (exp));
		}

		public override void ExitDocumentType (OParser.DocumentTypeContext ctx)
		{
			SetNodeValue (ctx, DocumentType.Instance);
		}

		public override void ExitFetch_list_expression (OParser.Fetch_list_expressionContext ctx)
		{
			String itemName = this.GetNodeValue<String> (ctx.name);
			IExpression source = this.GetNodeValue<IExpression> (ctx.source);
			IExpression filter = this.GetNodeValue<IExpression> (ctx.predicate);
			SetNodeValue (ctx, new FetchExpression (itemName, source, filter));
		}

		public override void ExitFetchOne (OParser.FetchOneContext ctx)
		{
			CategoryType category = this.GetNodeValue<CategoryType>(ctx.typ);
			IExpression filter = this.GetNodeValue<IExpression>(ctx.predicate);
			SetNodeValue(ctx, new FetchOneExpression(category, filter));
		}

		public override void ExitFetchMany (OParser.FetchManyContext ctx)
		{
			CategoryType category = this.GetNodeValue<CategoryType>(ctx.typ);
			IExpression filter = this.GetNodeValue<IExpression>(ctx.predicate);
			IExpression start = this.GetNodeValue<IExpression>(ctx.xstart);
			IExpression stop = this.GetNodeValue<IExpression>(ctx.xstop);
			OrderByClauseList orderBy = this.GetNodeValue<OrderByClauseList>(ctx.orderby);
			SetNodeValue(ctx, new FetchManyExpression(category, filter, start, stop, orderBy));
		}

		public override void ExitCode_type (OParser.Code_typeContext ctx)
		{
			SetNodeValue (ctx, CodeType.Instance);
		}

		public override void ExitExecuteExpression (OParser.ExecuteExpressionContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new ExecuteExpression (name));
		}

		public override void ExitCodeExpression (OParser.CodeExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new CodeExpression (exp));
		}

		public override void ExitCode_argument (OParser.Code_argumentContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new CodeArgument (name));
		}

		public override void ExitCategory_symbol (OParser.Category_symbolContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			ArgumentAssignmentList args = this.GetNodeValue<ArgumentAssignmentList> (ctx.args);
			SetNodeValue (ctx, new CategorySymbol (name, args));
		}

		public override void ExitCategory_symbol_list (OParser.Category_symbol_listContext ctx)
		{
			CategorySymbolList items = new CategorySymbolList ();
			foreach(ParserRuleContext rule in ctx.category_symbol()) {
				CategorySymbol item = this.GetNodeValue<CategorySymbol> (rule);
				items.add (item);
			}
			SetNodeValue (ctx, items);
		}



		public override void ExitEnum_category_declaration (OParser.Enum_category_declarationContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			IdentifierList attrs = this.GetNodeValue<IdentifierList> (ctx.attrs);
			String parent = this.GetNodeValue<String> (ctx.derived);
			IdentifierList derived = parent == null ? null : new IdentifierList (parent);
			CategorySymbolList symbols = this.GetNodeValue<CategorySymbolList> (ctx.symbols);
			SetNodeValue (ctx, new EnumeratedCategoryDeclaration (name, attrs, derived, symbols));
		}

		public override void ExitRead_expression (OParser.Read_expressionContext ctx)
		{
			IExpression source = this.GetNodeValue<IExpression> (ctx.source);
			SetNodeValue (ctx, new ReadExpression (source));
		}

		public override void ExitWrite_statement (OParser.Write_statementContext ctx)
		{
			IExpression what = this.GetNodeValue<IExpression> (ctx.what);
			IExpression target = this.GetNodeValue<IExpression> (ctx.target);
			SetNodeValue (ctx, new WriteStatement (what, target));
		}

		public override void ExitWith_resource_statement (OParser.With_resource_statementContext ctx)
		{
			AssignVariableStatement stmt = this.GetNodeValue<AssignVariableStatement> (ctx.stmt);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new WithResourceStatement (stmt, stmts));
		}

		public override void ExitAnyType (OParser.AnyTypeContext ctx)
		{
			SetNodeValue (ctx, AnyType.Instance);
		}

		public override void ExitAnyListType (OParser.AnyListTypeContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.any_type());
			SetNodeValue (ctx, new ListType (type));
		}

		public override void ExitAnyDictType (OParser.AnyDictTypeContext ctx)
		{
			IType type = this.GetNodeValue<IType> (ctx.any_type());
			SetNodeValue (ctx, new DictType (type));
		}

		public override void ExitCastExpression (OParser.CastExpressionContext ctx)
		{
			IExpression left = this.GetNodeValue<IExpression> (ctx.left);
			IType type = this.GetNodeValue<IType> (ctx.right);
			SetNodeValue (ctx, new CastExpression (left, type));
		}

		public override void ExitCatchAtomicStatement (OParser.CatchAtomicStatementContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new AtomicSwitchCase (new SymbolExpression (name), stmts));
		}

		public override void ExitCatchCollectionStatement (OParser.CatchCollectionStatementContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SetNodeValue (ctx, new CollectionSwitchCase (exp, stmts));
		}

		public override void ExitCatch_statement_list (OParser.Catch_statement_listContext ctx)
		{
			SwitchCaseList items = new SwitchCaseList ();
			foreach(ParserRuleContext rule in ctx.catch_statement()) {
				SwitchCase item = this.GetNodeValue<SwitchCase> (rule);
				items.Add (item);
			}
			SetNodeValue (ctx, items);
		}


		public override void ExitTry_statement (OParser.Try_statementContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			StatementList stmts = this.GetNodeValue<StatementList> (ctx.stmts);
			SwitchCaseList handlers = this.GetNodeValue<SwitchCaseList> (ctx.handlers);
			StatementList anyStmts = this.GetNodeValue<StatementList> (ctx.anyStmts);
			StatementList finalStmts = this.GetNodeValue<StatementList> (ctx.finalStmts);
			SwitchErrorStatement stmt = new SwitchErrorStatement (name, stmts, handlers, anyStmts, finalStmts);
			SetNodeValue (ctx, stmt);
		}

		public override void ExitRaise_statement (OParser.Raise_statementContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new RaiseStatement (exp));
		}

		public override void ExitMatchingList (OParser.MatchingListContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.source);
			SetNodeValue (ctx, new MatchingCollectionConstraint (exp));
		}

		public override void ExitMatchingRange (OParser.MatchingRangeContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.source);
			SetNodeValue (ctx, new MatchingCollectionConstraint (exp));
		}

		public override void ExitMatchingExpression (OParser.MatchingExpressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression> (ctx.exp);
			SetNodeValue (ctx, new MatchingExpressionConstraint (exp));
		}

		public override void ExitMatchingPattern (OParser.MatchingPatternContext ctx)
		{
			SetNodeValue (ctx, new MatchingPatternConstraint (new TextLiteral (ctx.text.Text)));
		}

		public override void ExitMatchingSet (OParser.MatchingSetContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression>(ctx.source);
			SetNodeValue(ctx, new MatchingCollectionConstraint(exp));
		}

		public override void ExitCsharp_item_expression (OParser.Csharp_item_expressionContext ctx)
		{
			CSharpExpression exp = this.GetNodeValue<CSharpExpression> (ctx.exp);
			SetNodeValue (ctx, new CSharpItemExpression (exp));
		}

		public override void ExitCsharp_method_expression (OParser.Csharp_method_expressionContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			CSharpExpressionList args = this.GetNodeValue<CSharpExpressionList> (ctx.args);
			SetNodeValue (ctx, new CSharpMethodExpression (name, args));
		}

		public override void ExitCSharpArgumentList (OParser.CSharpArgumentListContext ctx)
		{
			CSharpExpression item = this.GetNodeValue<CSharpExpression> (ctx.item);
			SetNodeValue (ctx, new CSharpExpressionList (item));
		}

		public override void ExitCSharpArgumentListItem (OParser.CSharpArgumentListItemContext ctx)
		{
			CSharpExpression item = this.GetNodeValue<CSharpExpression> (ctx.item);
			CSharpExpressionList items = this.GetNodeValue<CSharpExpressionList> (ctx.items);
			items.Add (item);
			SetNodeValue (ctx, items);
		}

		public override void ExitCSharpItemExpression (OParser.CSharpItemExpressionContext ctx)
		{
			CSharpExpression exp = this.GetNodeValue<CSharpExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		public override void ExitCSharpMethodExpression (OParser.CSharpMethodExpressionContext ctx)
		{
			CSharpExpression exp = this.GetNodeValue<CSharpExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		public override void ExitCSharpSelectorExpression (OParser.CSharpSelectorExpressionContext ctx)
		{
			CSharpExpression parent = this.GetNodeValue<CSharpExpression> (ctx.parent);
			CSharpSelectorExpression child = this.GetNodeValue<CSharpSelectorExpression> (ctx.child);
			child.SetParent (parent);
			SetNodeValue (ctx, child);
		}

		public override void ExitJavascript_category_binding (OParser.Javascript_category_bindingContext ctx)
		{
			String identifier = ctx.identifier ().GetText ();
			JavaScriptModule module = this.GetNodeValue<JavaScriptModule> (ctx.javascript_module ());
			JavaScriptNativeCategoryBinding map = new JavaScriptNativeCategoryBinding (identifier, module);
			SetNodeValue (ctx, map);
		}

		public override void ExitJavaScriptMemberExpression (OParser.JavaScriptMemberExpressionContext ctx)
		{
			String name = ctx.name.GetText ();
			SetNodeValue (ctx, new JavaScriptMemberExpression(name));
		}

		public override void ExitJavascript_primary_expression (OParser.Javascript_primary_expressionContext ctx)
		{
			JavaScriptExpression exp = this.GetNodeValue<JavaScriptExpression> (ctx.GetChild(0));
			SetNodeValue (ctx, exp);
		}

		public override void ExitJavaScriptMethodExpression (OParser.JavaScriptMethodExpressionContext ctx)
		{
			JavaScriptExpression method = this.GetNodeValue<JavaScriptExpression> (ctx.method);
			SetNodeValue (ctx, method);
		}

		public override void ExitJavascript_this_expression (OParser.Javascript_this_expressionContext ctx)
		{
			SetNodeValue (ctx, new JavaScriptThisExpression ());
		}


		public override void ExitJavascript_identifier (OParser.Javascript_identifierContext ctx)
		{
			String name = ctx.GetText ();
			SetNodeValue (ctx, name);
		}

		public override void ExitJavascript_method_expression (OParser.Javascript_method_expressionContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			JavaScriptMethodExpression method = new JavaScriptMethodExpression (name);
			JavaScriptExpressionList args = this.GetNodeValue<JavaScriptExpressionList> (ctx.args);
			method.setArguments (args);
			SetNodeValue (ctx, method);
		}

		public override void ExitCsharp_primary_expression (OParser.Csharp_primary_expressionContext ctx)
		{
			CSharpExpression exp = this.GetNodeValue<CSharpExpression> (ctx.GetChild(0));
			SetNodeValue (ctx, exp);
		}

		public override void ExitCsharp_this_expression (OParser.Csharp_this_expressionContext ctx)
		{
			SetNodeValue (ctx, new CSharpThisExpression());
		}


		public override void ExitJavascript_module (OParser.Javascript_moduleContext ctx)
		{
			List<String> ids = new List<String> ();
			foreach (OParser.Javascript_identifierContext ic in ctx.javascript_identifier())
				ids.Add (ic.GetText ());
			JavaScriptModule module = new JavaScriptModule (ids);
			SetNodeValue (ctx, module);
		}

		public override void ExitJavascript_native_statement (OParser.Javascript_native_statementContext ctx)
		{
			JavaScriptStatement stmt = this.GetNodeValue<JavaScriptStatement> (ctx.javascript_statement());
			JavaScriptModule module = this.GetNodeValue<JavaScriptModule> (ctx.javascript_module());
			stmt.setModule (module);
			SetNodeValue (ctx, stmt);
		}

		public override void ExitJavascript_new_expression (OParser.Javascript_new_expressionContext ctx)
		{
			JavaScriptMethodExpression method = this.GetNodeValue<JavaScriptMethodExpression> (ctx.javascript_method_expression());
			SetNodeValue (ctx, new JavaScriptNewExpression(method));
		}

		public override void ExitJavascriptArgumentList (OParser.JavascriptArgumentListContext ctx)
		{
			JavaScriptExpression exp = this.GetNodeValue<JavaScriptExpression> (ctx.item);
			JavaScriptExpressionList list = new JavaScriptExpressionList (exp);
			SetNodeValue (ctx, list);
		}

		public override void ExitJavascriptArgumentListItem (OParser.JavascriptArgumentListItemContext ctx)
		{
			JavaScriptExpression exp = this.GetNodeValue<JavaScriptExpression> (ctx.item);
			JavaScriptExpressionList list = this.GetNodeValue<JavaScriptExpressionList> (ctx.items);
			list.Add (exp);
			SetNodeValue (ctx, list);
		}

		public override void ExitJavascriptBooleanLiteral (OParser.JavascriptBooleanLiteralContext ctx)
		{
			string text = ctx.t.Text;
			SetNodeValue (ctx, new JavaScriptBooleanLiteral (text));
		}

		public override void ExitJavaScriptCategoryBinding (OParser.JavaScriptCategoryBindingContext ctx)
		{
			SetNodeValue (ctx, this.GetNodeValue<Object> (ctx.binding));
		}

		public override void ExitJavascriptCharacterLiteral (OParser.JavascriptCharacterLiteralContext ctx)
		{
			String text = ctx.t.Text;
			SetNodeValue (ctx, new JavaScriptCharacterLiteral (text));		
		}

		public override void ExitJavascriptDecimalLiteral (OParser.JavascriptDecimalLiteralContext ctx)
		{
			String text = ctx.t.Text;
			SetNodeValue (ctx, new JavaScriptDecimalLiteral (text));		
		}

		public override void ExitJavascript_identifier_expression (OParser.Javascript_identifier_expressionContext ctx)
		{
			String name = this.GetNodeValue<String> (ctx.name);
			SetNodeValue (ctx, new JavaScriptIdentifierExpression (name));
		}

		public override void ExitJavascriptIntegerLiteral (OParser.JavascriptIntegerLiteralContext ctx)
		{
			String text = ctx.t.Text;
			SetNodeValue (ctx, new JavaScriptIntegerLiteral (text));		
		}

		public override void ExitJavaScriptNativeStatement (OParser.JavaScriptNativeStatementContext ctx)
		{
			JavaScriptStatement stmt = this.GetNodeValue<JavaScriptStatement> (ctx.javascript_native_statement());
			SetNodeValue (ctx, new JavaScriptNativeCall (stmt));
		}

		public override void ExitJavascriptPrimaryExpression (OParser.JavascriptPrimaryExpressionContext ctx)
		{
			JavaScriptExpression exp = this.GetNodeValue<JavaScriptExpression> (ctx.exp);
			SetNodeValue (ctx, exp);
		}

		public override void ExitJavascriptReturnStatement (OParser.JavascriptReturnStatementContext ctx)
		{
			JavaScriptExpression exp = this.GetNodeValue<JavaScriptExpression> (ctx.exp);
			SetNodeValue (ctx, new JavaScriptStatement (exp, true));
		}

		public override void ExitJavascriptSelectorExpression (OParser.JavascriptSelectorExpressionContext ctx)
		{
			JavaScriptExpression parent = this.GetNodeValue<JavaScriptExpression> (ctx.parent);
			JavaScriptSelectorExpression child = this.GetNodeValue<JavaScriptSelectorExpression> (ctx.child);
			child.setParent (parent);
			SetNodeValue (ctx, child);
		}

		public override void ExitJavascriptTextLiteral (OParser.JavascriptTextLiteralContext ctx)
		{
			String text = ctx.t.Text;
			SetNodeValue (ctx, new JavaScriptTextLiteral (text));		
		}


		public override void ExitJavascriptStatement (OParser.JavascriptStatementContext ctx)
		{
			JavaScriptExpression exp = this.GetNodeValue<JavaScriptExpression> (ctx.exp);
			SetNodeValue (ctx, new JavaScriptStatement (exp, false));
		}

		public override void ExitLiteral_expression (OParser.Literal_expressionContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression>(ctx.GetChild(0));
			SetNodeValue (ctx, exp); 	
		}

		public override void ExitMethod_declaration (OParser.Method_declarationContext ctx)
		{
			IDeclaration exp = this.GetNodeValue<IDeclaration>(ctx.GetChild(0));
			SetNodeValue (ctx, exp); 	
		}

		public override void ExitMethod_identifier (OParser.Method_identifierContext ctx)
		{
			Object exp = this.GetNodeValue<Object>(ctx.GetChild(0));
			SetNodeValue (ctx, exp); 	
		}

		public override void ExitOperator_argument (OParser.Operator_argumentContext ctx)
		{
			IArgument exp = this.GetNodeValue<IArgument>(ctx.GetChild(0));
			SetNodeValue (ctx, exp); 	
		}

		public override void ExitCategory_or_any_type (OParser.Category_or_any_typeContext ctx)
		{
			IType exp = this.GetNodeValue<IType>(ctx.GetChild(0));
			SetNodeValue (ctx, exp); 	
		}

		public override void ExitCollection_literal (OParser.Collection_literalContext ctx)
		{
			IExpression exp = this.GetNodeValue<IExpression>(ctx.GetChild(0));
			SetNodeValue (ctx, exp); 	
		}

		public override void ExitCursorType (OParser.CursorTypeContext context)
		{
			throw new NotImplementedException();
		}

		public override void ExitEnum_declaration (OParser.Enum_declarationContext ctx)
		{
			IDeclaration exp = this.GetNodeValue<IDeclaration>(ctx.GetChild(0));
			SetNodeValue (ctx, exp); 	
		}

		public override void ExitSymbol_list (OParser.Symbol_listContext context)
		{
			throw new NotImplementedException();
		}
	}
}
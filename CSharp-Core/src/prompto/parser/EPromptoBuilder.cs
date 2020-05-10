using Antlr4.Runtime.Tree;
using Antlr4.Runtime;
using prompto.parser;
using System;
using prompto.grammar;
using prompto.expression;
using prompto.literal;
using prompto.type;
using prompto.declaration;
using prompto.statement;
using prompto.java;
using prompto.python;
using prompto.csharp;
using Antlr4.Runtime.Misc;
using System.Collections.Generic;
using prompto.javascript;
using prompto.utils;
using prompto.param;
using prompto.constraint;
using prompto.instance;
using prompto.jsx;
using prompto.css;
using System.Text;

namespace prompto.parser
{

    public class EPromptoBuilder : EParserBaseListener
    {

        ParseTreeProperty<object> nodeValues = new ParseTreeProperty<object>();
        BufferedTokenStream input;
        string path = "";

        public EPromptoBuilder(ECleverParser parser)
        {
            this.input = (BufferedTokenStream)parser.InputStream;
            this.path = parser.Path;
        }

        protected String getHiddenTokensBefore(ITerminalNode node)
        {
            return getHiddenTokensAfter(node.Symbol);
        }

        protected String getHiddenTokensBefore(IToken token)
        {
            IList<IToken> hidden = input.GetHiddenTokensToLeft(token.TokenIndex);
            return getHiddenTokensText(hidden);
        }

        protected String getHiddenTokensAfter(ITerminalNode node)
        {
            return getHiddenTokensAfter(node.Symbol);
        }

        protected String getHiddenTokensAfter(IToken token)
        {
            IList<IToken> hidden = input.GetHiddenTokensToRight(token.TokenIndex);
            return getHiddenTokensText(hidden);
        }


        private String getHiddenTokensText(IList<IToken> hidden)
        {
            if (hidden == null || hidden.Count == 0)
                return null;
            else
            {
                StringBuilder sb = new StringBuilder();
                foreach (IToken token in hidden)
                    sb.Append(token.Text);
                return sb.ToString();
            }
        }

        private String getWhiteSpacePlus(ParserRuleContext ctx)
        {
            if (ctx.ChildCount == 0)
                return null;
            StringBuilder sb = new StringBuilder();
            foreach (IParseTree child in ctx.children)
            {
                if (isIndent(child))
                    continue;
                sb.Append(child.GetText());
            }
            String within = sb.ToString();
            if (within.Length == 0)
                return null;
            String before = getHiddenTokensBefore(ctx.Start);
            if (before != null)
                within = before + within;
            String after = getHiddenTokensAfter(ctx.Stop);
            if (after != null)
                within = within + after;
            return within;
        }

        private static bool isIndent(IParseTree tree)
        {
            return tree is ITerminalNode && ((ITerminalNode)tree).Symbol.Type == EParser.INDENT;
        }

        public T GetNodeValue<T>(IParseTree node)
        {
            if (node == null)
                return default(T);
            object o = nodeValues.Get(node);
            if (o == null)
                return default(T);
            if (o is T)
                return (T)o;
            else
                throw new Exception("Unexpected");
        }

        public void SetNodeValue(IParseTree node, object value)
        {
            nodeValues.Put(node, value);
        }

        public void SetNodeValue(ParserRuleContext node, Section value)
        {
            nodeValues.Put(node, value);
            BuildSection(node, value);
        }

        public void BuildSection(ParserRuleContext node, Section section)
        {
            IToken first = FindFirstValidToken(node.Start.TokenIndex);
            IToken last = FindLastValidToken(node.Stop.TokenIndex);
            section.SetFrom(path, first, last, Dialect.E);
        }

        private IToken FindFirstValidToken(int idx)
        {
            if (idx == -1) // happens because input.index() is called before any other read operation (bug?)
                idx = 0;
            do
            {
                IToken token = ReadValidToken(idx++);
                if (token != null)
                    return token;
            } while (idx < input.Size);
            return null;
        }

        private IToken FindLastValidToken(int idx)
        {
            if (idx == -1) // happens because input.index() is called before any other read operation (bug?)
                idx = 0;
            while (idx >= 0)
            {
                IToken token = ReadValidToken(idx--);
                if (token != null)
                    return token;
            }
            return null;
        }

        private IToken ReadValidToken(int idx)
        {
            IToken token = input.Get(idx);
            string text = token.Text;
            if (text != null && text.Length > 0 && !Char.IsWhiteSpace(text[0]))
                return token;
            else
                return null;
        }


        List<Annotation> ReadAnnotations(EParser.Annotation_constructorContext[] contexts)
        {
            List<Annotation> annotations = null;
            foreach (RuleContext acs in contexts)
            {
                if (acs == null)
                    continue;
                if (annotations == null)
                    annotations = new List<Annotation>();
                annotations.add((Annotation)GetNodeValue<Annotation>(acs));
            }
            return annotations;
        }

        List<CommentStatement> ReadComments(EParser.Comment_statementContext[] contexts)
        {
            List<CommentStatement> comments = null;
            foreach (RuleContext csc in contexts)
            {
                if (csc == null)
                    continue;
                if (comments == null)
                    comments = new List<CommentStatement>();
                comments.add((CommentStatement)GetNodeValue<CommentStatement>(csc));
            }
            return comments;
        }


        public override void ExitIdentifierExpression(EParser.IdentifierExpressionContext ctx)
        {
            String name = GetNodeValue<String>(ctx.exp);
            SetNodeValue(ctx, new UnresolvedIdentifier(name, Dialect.E));
        }


        public override void ExitTypeIdentifier(EParser.TypeIdentifierContext ctx)
        {
            String name = GetNodeValue<String>(ctx.type_identifier());
            SetNodeValue(ctx, name);
        }


        public override void ExitTypeLiteral([NotNull] EParser.TypeLiteralContext ctx)
        {
            TypeLiteral type = GetNodeValue<TypeLiteral>(ctx.type_literal());
            SetNodeValue(ctx, type);
        }


        public override void ExitType_literal([NotNull] EParser.Type_literalContext ctx)
        {
            IType type = GetNodeValue<IType>(ctx.category_or_any_type());
            SetNodeValue(ctx, new TypeLiteral(type));
        }


        public override void ExitMethodCallExpression(EParser.MethodCallExpressionContext ctx)
        {
            IExpression exp = ctx.exp1 != null ? GetNodeValue<IExpression>(ctx.exp1) : GetNodeValue<IExpression>(ctx.exp2);
            ArgumentList args = GetNodeValue<ArgumentList>(ctx.args);
            Object call = new UnresolvedCall(exp, args);
            SetNodeValue(ctx, call);
        }


        public override void ExitUnresolvedExpression(EParser.UnresolvedExpressionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }


        public override void ExitUnresolvedIdentifier(EParser.UnresolvedIdentifierContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            SetNodeValue(ctx, new UnresolvedIdentifier(name, Dialect.E));
        }


        public override void ExitUnresolvedSelector(EParser.UnresolvedSelectorContext ctx)
        {
            IExpression parent = GetNodeValue<IExpression>(ctx.parent);
            SelectorExpression selector = GetNodeValue<SelectorExpression>(ctx.selector);
            selector.setParent(parent);
            SetNodeValue(ctx, selector);
        }


        public override void ExitUnresolved_selector(EParser.Unresolved_selectorContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            SetNodeValue(ctx, new MemberSelector(name));
        }


        public override void ExitUUIDLiteral(EParser.UUIDLiteralContext ctx)
        {
            SetNodeValue(ctx, new UUIDLiteral(ctx.GetText()));
        }


        public override void ExitUUIDType(EParser.UUIDTypeContext ctx)
        {
            SetNodeValue(ctx, UUIDType.Instance);
        }


        public override void ExitBlobExpression(EParser.BlobExpressionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }

        public override void ExitBlob_expression(EParser.Blob_expressionContext ctx)
        {
            IExpression source = GetNodeValue<IExpression>(ctx.expression());
            SetNodeValue(ctx, new BlobExpression(source));
        }

        public override void ExitBlobType(EParser.BlobTypeContext ctx)
        {
            SetNodeValue(ctx, BlobType.Instance);
        }

        public override void ExitBooleanLiteral(EParser.BooleanLiteralContext ctx)
        {
            SetNodeValue(ctx, new BooleanLiteral(ctx.GetText()));
        }


        public override void ExitBreakStatement(EParser.BreakStatementContext ctx)
        {
            SetNodeValue(ctx, new BreakStatement());
        }


        public override void ExitMinIntegerLiteral(EParser.MinIntegerLiteralContext ctx)
        {
            SetNodeValue(ctx, new MinIntegerLiteral());
        }


        public override void ExitMaxIntegerLiteral(EParser.MaxIntegerLiteralContext ctx)
        {
            SetNodeValue(ctx, new MaxIntegerLiteral());
        }


        public override void ExitIntegerLiteral(EParser.IntegerLiteralContext ctx)
        {
            SetNodeValue(ctx, new IntegerLiteral(ctx.GetText()));
        }


        public override void ExitDecimalLiteral(EParser.DecimalLiteralContext ctx)
        {
            SetNodeValue(ctx, new DecimalLiteral(ctx.GetText()));
        }


        public override void ExitHexadecimalLiteral(EParser.HexadecimalLiteralContext ctx)
        {
            SetNodeValue(ctx, new HexaLiteral(ctx.GetText()));
        }


        public override void ExitCharacterLiteral(EParser.CharacterLiteralContext ctx)
        {
            SetNodeValue(ctx, new CharacterLiteral(ctx.GetText()));
        }


        public override void ExitDateLiteral(EParser.DateLiteralContext ctx)
        {
            SetNodeValue(ctx, new DateLiteral(ctx.GetText()));
        }


        public override void ExitDateTimeLiteral(EParser.DateTimeLiteralContext ctx)
        {
            SetNodeValue(ctx, new DateTimeLiteral(ctx.GetText()));
        }

        public override void ExitTernaryExpression(EParser.TernaryExpressionContext ctx)
        {
            IExpression condition = GetNodeValue<IExpression>(ctx.test);
            IExpression ifTrue = GetNodeValue<IExpression>(ctx.ifTrue);
            IExpression ifFalse = GetNodeValue<IExpression>(ctx.ifFalse);
            TernaryExpression exp = new TernaryExpression(condition, ifTrue, ifFalse);
            SetNodeValue(ctx, exp);
        }


        public override void ExitTest_method_declaration(EParser.Test_method_declarationContext ctx)
        {
            String name = ctx.name.Text;
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            ExpressionList exps = GetNodeValue<ExpressionList>(ctx.exps);
            String errorName = GetNodeValue<String>(ctx.error);
            SymbolExpression error = errorName == null ? null : new SymbolExpression(errorName);
            SetNodeValue(ctx, new TestMethodDeclaration(name, stmts, exps, error));
        }

        public override void ExitTextLiteral(EParser.TextLiteralContext ctx)
        {
            SetNodeValue(ctx, new TextLiteral(ctx.GetText()));
        }


        public override void ExitTimeLiteral(EParser.TimeLiteralContext ctx)
        {
            SetNodeValue(ctx, new TimeLiteral(ctx.GetText()));
        }


        public override void ExitPeriodLiteral(EParser.PeriodLiteralContext ctx)
        {
            SetNodeValue(ctx, new PeriodLiteral(ctx.GetText()));
        }


        public override void ExitPeriodType(EParser.PeriodTypeContext ctx)
        {
            SetNodeValue(ctx, PeriodType.Instance);
        }


        public override void ExitVersionLiteral(EParser.VersionLiteralContext ctx)
        {
            SetNodeValue(ctx, new VersionLiteral(ctx.GetText()));
        }


        public override void ExitVersionType(EParser.VersionTypeContext ctx)
        {
            SetNodeValue(ctx, VersionType.Instance);
        }

        public override void ExitAttribute_identifier(EParser.Attribute_identifierContext ctx)
        {
            SetNodeValue(ctx, ctx.GetText());
        }


        public override void ExitVariable_identifier(EParser.Variable_identifierContext ctx)
        {
            SetNodeValue(ctx, ctx.GetText());
        }


        public override void ExitList_literal(EParser.List_literalContext ctx)
        {
            bool mutable = ctx.MUTABLE() != null;
            ExpressionList items = GetNodeValue<ExpressionList>(ctx.expression_list());
            IExpression value = items == null ? new ListLiteral(mutable) : new ListLiteral(items, mutable);
            SetNodeValue(ctx, value);
        }


        public override void ExitDict_literal(EParser.Dict_literalContext ctx)
        {
            bool mutable = ctx.MUTABLE() != null;
            DictEntryList items = GetNodeValue<DictEntryList>(ctx.dict_entry_list());
            IExpression value = items == null ? new DictLiteral(mutable) : new DictLiteral(items, mutable);
            SetNodeValue(ctx, value);
        }


        public override void ExitTuple_literal(EParser.Tuple_literalContext ctx)
        {
            bool mutable = ctx.MUTABLE() != null;
            ExpressionList items = GetNodeValue<ExpressionList>(ctx.expression_tuple());
            IExpression value = items == null ? new TupleLiteral(mutable) : new TupleLiteral(items, mutable);
            SetNodeValue(ctx, value);
        }


        public override void ExitRange_literal(EParser.Range_literalContext ctx)
        {
            IExpression low = GetNodeValue<IExpression>(ctx.low);
            IExpression high = GetNodeValue<IExpression>(ctx.high);
            SetNodeValue(ctx, new RangeLiteral(low, high));
        }

        public override void ExitDict_entry_list(EParser.Dict_entry_listContext ctx)
        {
            DictEntryList items = new DictEntryList();
            foreach (ParserRuleContext entry in ctx.dict_entry())
            {
                DictEntry item = GetNodeValue<DictEntry>(entry);
                items.add(item);
            }
            SetNodeValue(ctx, items);
        }


        public override void ExitDict_entry(EParser.Dict_entryContext ctx)
        {
            DictKey key = GetNodeValue<DictKey>(ctx.key);
            IExpression value = GetNodeValue<IExpression>(ctx.value);
            DictEntry entry = new DictEntry(key, value);
            SetNodeValue(ctx, entry);
        }


        public override void ExitLiteralExpression(EParser.LiteralExpressionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }


        public override void ExitVariableIdentifier(EParser.VariableIdentifierContext ctx)
        {
            String name = GetNodeValue<String>(ctx.variable_identifier());
            SetNodeValue(ctx, name);
        }


        public override void ExitExpression_list(EParser.Expression_listContext ctx)
        {
            ExpressionList items = new ExpressionList();
            foreach (ParserRuleContext rule in ctx.expression())
            {
                IExpression item = GetNodeValue<IExpression>(rule);
                items.add(item);
            }
            SetNodeValue(ctx, items);
        }


        public override void ExitExpression_tuple(EParser.Expression_tupleContext ctx)
        {
            ExpressionList items = new ExpressionList();
            foreach (ParserRuleContext rule in ctx.expression())
            {
                IExpression item = GetNodeValue<IExpression>(rule);
                items.add(item);
            }
            SetNodeValue(ctx, items);
        }


        public override void ExitSymbol_identifier(EParser.Symbol_identifierContext ctx)
        {
            SetNodeValue(ctx, ctx.GetText());
        }

        public override void ExitNative_member_method_declaration(EParser.Native_member_method_declarationContext ctx)
        {
            IMethodDeclaration decl = GetNodeValue<IMethodDeclaration>(ctx.GetChild(0));
            SetNodeValue(ctx, decl);
        }

        public override void ExitNative_symbol(EParser.Native_symbolContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, new NativeSymbol(name, exp));
        }


        public override void ExitSymbolIdentifier(EParser.SymbolIdentifierContext ctx)
        {
            String name = GetNodeValue<String>(ctx.symbol_identifier());
            SetNodeValue(ctx, name);
        }


        public override void ExitSymbolLiteral([NotNull] EParser.SymbolLiteralContext ctx)
        {
            String name = ctx.GetText();
            SetNodeValue(ctx, new SymbolExpression(name));
        }


        public override void ExitBooleanType(EParser.BooleanTypeContext ctx)
        {
            SetNodeValue(ctx, BooleanType.Instance);
        }


        public override void ExitCharacterType(EParser.CharacterTypeContext ctx)
        {
            SetNodeValue(ctx, CharacterType.Instance);
        }


        public override void ExitTextType(EParser.TextTypeContext ctx)
        {
            SetNodeValue(ctx, TextType.Instance);
        }


        public override void ExitHtmlType(EParser.HtmlTypeContext ctx)
        {
            SetNodeValue(ctx, HtmlType.Instance);
        }

        public override void ExitThisExpression(EParser.ThisExpressionContext ctx)
        {
            SetNodeValue(ctx, new ThisExpression());
        }

        public override void ExitIntegerType(EParser.IntegerTypeContext ctx)
        {
            SetNodeValue(ctx, IntegerType.Instance);
        }


        public override void ExitDecimalType(EParser.DecimalTypeContext ctx)
        {
            SetNodeValue(ctx, DecimalType.Instance);
        }


        public override void ExitDateType(EParser.DateTypeContext ctx)
        {
            SetNodeValue(ctx, DateType.Instance);
        }


        public override void ExitDateTimeType(EParser.DateTimeTypeContext ctx)
        {
            SetNodeValue(ctx, DateTimeType.Instance);
        }


        public override void ExitTimeType(EParser.TimeTypeContext ctx)
        {
            SetNodeValue(ctx, TimeType.Instance);
        }


        public override void ExitCodeType(EParser.CodeTypeContext ctx)
        {
            SetNodeValue(ctx, CodeType.Instance);
        }


        public override void ExitPrimaryType(EParser.PrimaryTypeContext ctx)
        {
            IType type = GetNodeValue<IType>(ctx.p);
            SetNodeValue(ctx, type);
        }


        public override void ExitAttribute_declaration(EParser.Attribute_declarationContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            IType type = GetNodeValue<IType>(ctx.typ);
            IAttributeConstraint match = GetNodeValue<IAttributeConstraint>(ctx.match);
            IdentifierList indices = ctx.INDEX() != null ? new IdentifierList() : null;
            if (ctx.indices != null)
                indices.AddRange(GetNodeValue<IdentifierList>(ctx.indices));
            if (ctx.index != null)
                indices.add(GetNodeValue<String>(ctx.index));
            AttributeDeclaration decl = new AttributeDeclaration(name, type, match, indices);
            decl.Storable = ctx.STORABLE() != null;
            SetNodeValue(ctx, decl);
        }


        public override void ExitNativeType(EParser.NativeTypeContext ctx)
        {
            IType type = GetNodeValue<IType>(ctx.n);
            SetNodeValue(ctx, type);
        }


        public override void ExitCategoryType(EParser.CategoryTypeContext ctx)
        {
            IType type = GetNodeValue<IType>(ctx.c);
            SetNodeValue(ctx, type);
        }


        public override void ExitCategory_type(EParser.Category_typeContext ctx)
        {
            String name = ctx.GetText();
            SetNodeValue(ctx, new CategoryType(name));
        }


        public override void ExitListType(EParser.ListTypeContext ctx)
        {
            IType type = GetNodeValue<IType>(ctx.l);
            SetNodeValue(ctx, new ListType(type));
        }


        public override void ExitDictKeyIdentifier(EParser.DictKeyIdentifierContext ctx)
        {
            String text = ctx.name.GetText();
            SetNodeValue(ctx, new DictIdentifierKey(text));
        }


        public override void ExitDictKeyText(EParser.DictKeyTextContext ctx)
        {
            String text = ctx.name.Text;
            SetNodeValue(ctx, new DictTextKey(text));
        }


        public override void ExitDictType(EParser.DictTypeContext ctx)
        {
            IType type = GetNodeValue<IType>(ctx.d);
            SetNodeValue(ctx, new DictType(type));
        }


        public override void ExitAttributeList(EParser.AttributeListContext ctx)
        {
            String item = GetNodeValue<String>(ctx.item);
            SetNodeValue(ctx, new IdentifierList(item));
        }


        public override void ExitAttributeListItem(EParser.AttributeListItemContext ctx)
        {
            IdentifierList items = GetNodeValue<IdentifierList>(ctx.items);
            String item = GetNodeValue<String>(ctx.item);
            items.Add(item);
            SetNodeValue(ctx, items);
        }


        public override void ExitAttribute_identifier_list(EParser.Attribute_identifier_listContext ctx)
        {
            IdentifierList list = new IdentifierList();
            foreach (EParser.Attribute_identifierContext c in ctx.attribute_identifier())
            {
                String item = GetNodeValue<String>(c);
                list.Add(item);
            }
            SetNodeValue(ctx, list);
        }

        public override void ExitVariable_identifier_list(EParser.Variable_identifier_listContext ctx)
        {
            IdentifierList list = new IdentifierList();
            foreach (EParser.Variable_identifierContext c in ctx.variable_identifier())
            {
                String item = GetNodeValue<String>(c);
                list.Add(item);
            }
            SetNodeValue(ctx, list);
        }


        public override void ExitConcrete_category_declaration(EParser.Concrete_category_declarationContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            IdentifierList attrs = GetNodeValue<IdentifierList>(ctx.attrs);
            IdentifierList derived = GetNodeValue<IdentifierList>(ctx.derived);
            MethodDeclarationList methods = GetNodeValue<MethodDeclarationList>(ctx.methods);
            ConcreteCategoryDeclaration decl = new ConcreteCategoryDeclaration(name, attrs, derived, methods);
            decl.Storable = ctx.STORABLE() != null;
            SetNodeValue(ctx, decl);
        }


        public override void ExitConcrete_widget_declaration(EParser.Concrete_widget_declarationContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            String derived = GetNodeValue<String>(ctx.derived);
            MethodDeclarationList methods = GetNodeValue<MethodDeclarationList>(ctx.methods);
            ConcreteWidgetDeclaration decl = new ConcreteWidgetDeclaration(name, derived, methods);
            SetNodeValue(ctx, decl);
        }

        public override void ExitConcreteCategoryDeclaration(EParser.ConcreteCategoryDeclarationContext ctx)
        {
            SetNodeValue(ctx, GetNodeValue<Object>(ctx.decl));
        }


        public override void ExitConcreteWidgetDeclaration(EParser.ConcreteWidgetDeclarationContext ctx)
        {
            SetNodeValue(ctx, GetNodeValue<Object>(ctx.decl));
        }


        public override void ExitNativeWidgetDeclaration(EParser.NativeWidgetDeclarationContext ctx)
        {
            SetNodeValue(ctx, GetNodeValue<Object>(ctx.decl));
        }


        public override void ExitType_identifier(EParser.Type_identifierContext ctx)
        {
            SetNodeValue(ctx, ctx.GetText());
        }


        public override void ExitDerivedList(EParser.DerivedListContext ctx)
        {
            IdentifierList items = GetNodeValue<IdentifierList>(ctx.items);
            SetNodeValue(ctx, items);
        }


        public override void ExitDerivedListItem(EParser.DerivedListItemContext ctx)
        {
            IdentifierList items = GetNodeValue<IdentifierList>(ctx.items);
            String item = GetNodeValue<String>(ctx.item);
            items.Add(item);
            SetNodeValue(ctx, items);
        }


        public override void ExitType_identifier_list(EParser.Type_identifier_listContext ctx)
        {
            IdentifierList items = new IdentifierList();
            foreach (ParserRuleContext rule in ctx.type_identifier())
            {
                String item = GetNodeValue<String>(rule);
                items.Add(item);
            }
            SetNodeValue(ctx, items);
        }


        public override void ExitInstanceExpression(EParser.InstanceExpressionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }


        public override void ExitSelectableExpression(EParser.SelectableExpressionContext ctx)
        {
            IExpression parent = GetNodeValue<IExpression>(ctx.parent);
            SetNodeValue(ctx, parent);
        }


        public override void ExitSelectorExpression(EParser.SelectorExpressionContext ctx)
        {
            IExpression parent = GetNodeValue<IExpression>(ctx.parent);
            SelectorExpression selector = GetNodeValue<SelectorExpression>(ctx.selector);
            selector.setParent(parent);
            SetNodeValue(ctx, selector);
        }

        public override void ExitSet_literal(EParser.Set_literalContext ctx)
        {
            ExpressionList items = GetNodeValue<ExpressionList>(ctx.expression_list());
            SetLiteral set = items == null ? new SetLiteral() : new SetLiteral(items);
            SetNodeValue(ctx, set);
        }


        public override void ExitMember_identifier(EParser.Member_identifierContext ctx)
        {
            String name = ctx.GetText();
            SetNodeValue(ctx, name);
        }


        public override void ExitMemberSelector(EParser.MemberSelectorContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            SetNodeValue(ctx, new UnresolvedSelector(name));
        }


        public override void ExitItemSelector(EParser.ItemSelectorContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, new ItemSelector(exp));
        }


        public override void ExitSliceSelector(EParser.SliceSelectorContext ctx)
        {
            IExpression slice = GetNodeValue<IExpression>(ctx.xslice);
            SetNodeValue(ctx, slice);
        }


        public override void ExitTyped_argument(EParser.Typed_argumentContext ctx)
        {
            IType type = GetNodeValue<IType>(ctx.typ);
            String name = GetNodeValue<String>(ctx.name);
            IdentifierList attrs = GetNodeValue<IdentifierList>(ctx.attrs);
            CategoryParameter arg = attrs == null ?
                new CategoryParameter(type, name) :
                new ExtendedParameter(type, name, attrs);
            IExpression exp = GetNodeValue<IExpression>(ctx.value);
            arg.DefaultValue = exp;
            SetNodeValue(ctx, arg);
        }


        public override void ExitCodeArgument(EParser.CodeArgumentContext ctx)
        {
            IParameter arg = GetNodeValue<IParameter>(ctx.arg);
            SetNodeValue(ctx, arg);
        }

        public override void ExitArgument_list(EParser.Argument_listContext ctx)
        {
            ParameterList items = new ParameterList();
            foreach (ParserRuleContext rule in ctx.argument())
            {
                IParameter item = GetNodeValue<IParameter>(rule);
                items.Add(item);
            }
            SetNodeValue(ctx, items);
        }


        public override void ExitFlush_statement(EParser.Flush_statementContext ctx)
        {
            SetNodeValue(ctx, new FlushStatement());
        }


        public override void ExitFlushStatement(EParser.FlushStatementContext ctx)
        {
            SetNodeValue(ctx, GetNodeValue<IStatement>(ctx.stmt));
        }

        public override void ExitFull_argument_list(EParser.Full_argument_listContext ctx)
        {
            ParameterList items = GetNodeValue<ParameterList>(ctx.items);
            IParameter item = GetNodeValue<IParameter>(ctx.item);
            if (item != null)
                items.Add(item);
            SetNodeValue(ctx, items);
        }


        public override void ExitArgument_assignment(EParser.Argument_assignmentContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            IParameter arg = new UnresolvedParameter(name);
            SetNodeValue(ctx, new Argument(arg, exp));
        }


        public override void ExitArgumentAssignmentListExpression(EParser.ArgumentAssignmentListExpressionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            ArgumentList items = GetNodeValue<ArgumentList>(ctx.items);
            if (items == null)
                items = new ArgumentList();
            items.Insert(0, new Argument(null, exp));
            Argument item = GetNodeValue<Argument>(ctx.item);
            if (item != null)
                items.Add(item);
            else
                items.CheckLastAnd();
            SetNodeValue(ctx, items);
        }


        public override void ExitArgumentAssignmentListNoExpression(EParser.ArgumentAssignmentListNoExpressionContext ctx)
        {
            ArgumentList items = GetNodeValue<ArgumentList>(ctx.items);
            Argument item = GetNodeValue<Argument>(ctx.item);
            if (item != null)
                items.Add(item);
            else
                items.CheckLastAnd();
            SetNodeValue(ctx, items);
        }


        public override void ExitArgumentAssignmentList(EParser.ArgumentAssignmentListContext ctx)
        {
            Argument item = GetNodeValue<Argument>(ctx.item);
            ArgumentList items = new ArgumentList();
            items.Add(item);
            SetNodeValue(ctx, items);
        }


        public override void ExitArgumentAssignmentListItem(EParser.ArgumentAssignmentListItemContext ctx)
        {
            Argument item = GetNodeValue<Argument>(ctx.item);
            ArgumentList items = GetNodeValue<ArgumentList>(ctx.items);
            items.Add(item);
            SetNodeValue(ctx, items);
        }


        public override void ExitUnresolvedWithArgsStatement(EParser.UnresolvedWithArgsStatementContext ctx)
        {
            IExpression exp = ctx.exp1!=null ? GetNodeValue<IExpression>(ctx.exp1) : GetNodeValue<IExpression>(ctx.exp2);
            ArgumentList args = GetNodeValue<ArgumentList>(ctx.args);
            String resultName = GetNodeValue<String>(ctx.name);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            if (resultName != null || stmts != null)
                SetNodeValue(ctx, new RemoteCall(exp, args, resultName, stmts));
            else
                SetNodeValue(ctx, new UnresolvedCall(exp, args));
        }


        public override void ExitArrow_prefix(EParser.Arrow_prefixContext ctx)
        {
            IdentifierList args = GetNodeValue<IdentifierList>(ctx.arrow_args());
            String argsSuite = getHiddenTokensBefore(ctx.EGT());
            String arrowSuite = getHiddenTokensAfter(ctx.EGT());
            SetNodeValue(ctx, new ArrowExpression(args, argsSuite, arrowSuite));
        }

        public override void ExitArrowExpression(EParser.ArrowExpressionContext ctx)
        {
            SetNodeValue(ctx, GetNodeValue<Object>(ctx.exp));
        }

        public override void ExitArrowExpressionBody(EParser.ArrowExpressionBodyContext ctx)
        {
            ArrowExpression arrow = GetNodeValue<ArrowExpression>(ctx.arrow_prefix());
            IExpression exp = GetNodeValue<IExpression>(ctx.expression());
            arrow.Expression = exp;
            SetNodeValue(ctx, arrow);
        }

        public override void ExitArrowListArg(EParser.ArrowListArgContext ctx)
        {
            IdentifierList list = GetNodeValue<IdentifierList>(ctx.variable_identifier_list());
            SetNodeValue(ctx, list);
        }

        public override void ExitArrowSingleArg(EParser.ArrowSingleArgContext ctx)
        {
            String arg = GetNodeValue<String>(ctx.variable_identifier());
            SetNodeValue(ctx, new IdentifierList(arg));
        }


        public override void ExitArrowStatementsBody(EParser.ArrowStatementsBodyContext ctx)
        {
            ArrowExpression arrow = GetNodeValue<ArrowExpression>(ctx.arrow_prefix());
            StatementList stmts = GetNodeValue<StatementList>(ctx.statement_list());
            arrow.Statements = stmts;
            SetNodeValue(ctx, arrow);
        }

        public override void ExitAddExpression(EParser.AddExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            IExpression exp = ctx.op.Type == EParser.PLUS ?
            (IExpression)new PlusExpression(left, right)
            : (IExpression)new SubtractExpression(left, right);
            SetNodeValue(ctx, exp);
        }

        public override void ExitMember_method_declaration_list(EParser.Member_method_declaration_listContext ctx)
        {
            MethodDeclarationList items = new MethodDeclarationList();
            foreach (ParserRuleContext rule in ctx.member_method_declaration())
            {
                IMethodDeclaration item = GetNodeValue<IMethodDeclaration>(rule);
                items.add(item);
            }
            SetNodeValue(ctx, items);
        }

        public override void ExitNative_member_method_declaration_list(EParser.Native_member_method_declaration_listContext ctx)
        {
            MethodDeclarationList items = new MethodDeclarationList();
            foreach (ParserRuleContext rule in ctx.native_member_method_declaration())
            {
                IMethodDeclaration item = GetNodeValue<IMethodDeclaration>(rule);
                items.add(item);
            }
            SetNodeValue(ctx, items);
        }


        public override void ExitSetter_method_declaration(EParser.Setter_method_declarationContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            SetNodeValue(ctx, new SetterMethodDeclaration(name, stmts));
        }


        public override void ExitSetType(EParser.SetTypeContext ctx)
        {
            IType type = GetNodeValue<IType>(ctx.s);
            SetNodeValue(ctx, new SetType(type));
        }

        public override void ExitGetter_method_declaration(EParser.Getter_method_declarationContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            SetNodeValue(ctx, new GetterMethodDeclaration(name, stmts));
        }

        public override void ExitNative_setter_declaration(EParser.Native_setter_declarationContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            SetNodeValue(ctx, new NativeSetterMethodDeclaration(name, stmts));
        }


        public override void ExitNative_getter_declaration(EParser.Native_getter_declarationContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            SetNodeValue(ctx, new NativeGetterMethodDeclaration(name, stmts));
        }


        public override void ExitMember_method_declaration(EParser.Member_method_declarationContext ctx)
        {
            List<CommentStatement> comments = ReadComments(ctx.comment_statement());
            List<Annotation> annotations = ReadAnnotations(ctx.annotation_constructor());
            IParseTree ctx_ = ctx.children[ctx.ChildCount - 1];
            IDeclaration decl = GetNodeValue<IDeclaration>(ctx_);
            if (decl != null)
            {
                decl.Comments = comments;
                decl.Annotations = annotations;
                SetNodeValue(ctx, decl);
            }
        }


        public override void ExitStatement_list(EParser.Statement_listContext ctx)
        {
            StatementList items = new StatementList();
            foreach (ParserRuleContext rule in ctx.statement())
            {
                IStatement item = GetNodeValue<IStatement>(rule);
                items.add(item);
            }
            SetNodeValue(ctx, items);
        }

        public override void ExitStoreStatement(EParser.StoreStatementContext ctx)
        {
            SetNodeValue(ctx, GetNodeValue<Object>(ctx.stmt));
        }

        public override void ExitStore_statement(EParser.Store_statementContext ctx)
        {
            ExpressionList del = GetNodeValue<ExpressionList>(ctx.to_del);
            ExpressionList add = GetNodeValue<ExpressionList>(ctx.to_add);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            StoreStatement stmt = new StoreStatement(del, add, stmts);
            SetNodeValue(ctx, stmt);
        }

        public override void ExitAbstract_method_declaration(EParser.Abstract_method_declarationContext ctx)
        {
            IType type = GetNodeValue<IType>(ctx.typ);
            if (type is CategoryType)
                ((CategoryType)type).Mutable = ctx.MUTABLE() != null;
            String name = GetNodeValue<String>(ctx.name);
            ParameterList args = GetNodeValue<ParameterList>(ctx.args);
            SetNodeValue(ctx, new AbstractMethodDeclaration(name, args, type));
        }


        public override void ExitConcrete_method_declaration(EParser.Concrete_method_declarationContext ctx)
        {
            IType type = GetNodeValue<IType>(ctx.typ);
            if (type is CategoryType)
                ((CategoryType)type).Mutable = ctx.MUTABLE() != null;
            String name = GetNodeValue<String>(ctx.name);
            ParameterList args = GetNodeValue<ParameterList>(ctx.args);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            SetNodeValue(ctx, new ConcreteMethodDeclaration(name, args, type, stmts));
        }


        public override void ExitMethodCallStatement(EParser.MethodCallStatementContext ctx)
        {
            IStatement stmt = GetNodeValue<IStatement>(ctx.stmt);
            SetNodeValue(ctx, stmt);
        }


        public override void ExitConstructorFrom(EParser.ConstructorFromContext ctx)
        {
            CategoryType type = GetNodeValue<CategoryType>(ctx.typ);
            IExpression copyFrom = GetNodeValue<IExpression>(ctx.copyExp);
            ArgumentList args = GetNodeValue<ArgumentList>(ctx.args);
            Argument arg = GetNodeValue<Argument>(ctx.arg);
            if (arg != null)
            {
                if (args == null)
                    args = new ArgumentList();
                args.add(arg);
            }
            else if (args != null)
            {
                args.CheckLastAnd();
            }
            SetNodeValue(ctx, new ConstructorExpression(type, copyFrom, args, true));
        }


        public override void ExitConstructorNoFrom(EParser.ConstructorNoFromContext ctx)
        {
            CategoryType type = GetNodeValue<CategoryType>(ctx.typ);
            ArgumentList args = GetNodeValue<ArgumentList>(ctx.args);
            Argument arg = GetNodeValue<Argument>(ctx.arg);
            if (arg != null)
            {
                if (args == null)
                    args = new ArgumentList();
                args.add(arg);
            }
            else if (args != null)
            {
                args.CheckLastAnd();
            }
            SetNodeValue(ctx, new ConstructorExpression(type, null, args, true));
        }

        public override void ExitAssertion(EParser.AssertionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }


        public override void ExitAssertion_list(EParser.Assertion_listContext ctx)
        {
            ExpressionList items = new ExpressionList();
            foreach (ParserRuleContext rule in ctx.assertion())
            {
                IExpression item = GetNodeValue<IExpression>(rule);
                items.add(item);
            }
            SetNodeValue(ctx, items);
        }

        public override void ExitAssign_instance_statement(EParser.Assign_instance_statementContext ctx)
        {
            IAssignableInstance inst = GetNodeValue<IAssignableInstance>(ctx.inst);
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, new AssignInstanceStatement(inst, exp));
        }


        public override void ExitAssignInstanceStatement(EParser.AssignInstanceStatementContext ctx)
        {
            IStatement stmt = GetNodeValue<IStatement>(ctx.stmt);
            SetNodeValue(ctx, stmt);
        }


        public override void ExitAssign_variable_statement(EParser.Assign_variable_statementContext ctx)
        {
            String name = GetNodeValue<String>(ctx.variable_identifier());
            IExpression exp = GetNodeValue<IExpression>(ctx.expression());
            SetNodeValue(ctx, new AssignVariableStatement(name, exp));
        }


        public override void ExitAssign_tuple_statement(EParser.Assign_tuple_statementContext ctx)
        {
            IdentifierList items = GetNodeValue<IdentifierList>(ctx.items);
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, new AssignTupleStatement(items, exp));
        }


        public override void ExitRootInstance(EParser.RootInstanceContext ctx)
        {
            String name = GetNodeValue<String>(ctx.variable_identifier());
            SetNodeValue(ctx, new VariableInstance(name));
        }

        public override void ExitRoughlyEqualsExpression(EParser.RoughlyEqualsExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            SetNodeValue(ctx, new EqualsExpression(left, EqOp.ROUGHLY, right));
        }


        public override void ExitChildInstance(EParser.ChildInstanceContext ctx)
        {
            IAssignableInstance parent = GetNodeValue<IAssignableInstance>(ctx.assignable_instance());
            IAssignableSelector child = GetNodeValue<IAssignableSelector>(ctx.child_instance());
            child.SetParent(parent);
            SetNodeValue(ctx, child);
        }


        public override void ExitMemberInstance(EParser.MemberInstanceContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            SetNodeValue(ctx, new MemberInstance(name));
        }


        public override void ExitItemInstance(EParser.ItemInstanceContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, new ItemInstance(exp));
        }


        public override void ExitConstructorExpression(EParser.ConstructorExpressionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }


        public override void ExitNative_statement_list(EParser.Native_statement_listContext ctx)
        {
            StatementList items = new StatementList();
            foreach (ParserRuleContext rule in ctx.native_statement())
            {
                IStatement item = GetNodeValue<IStatement>(rule);
                items.add(item);
            }
            SetNodeValue(ctx, items);
        }


        public override void ExitJava_identifier(EParser.Java_identifierContext ctx)
        {
            SetNodeValue(ctx, ctx.GetText());
        }


        public override void ExitCsharp_identifier(EParser.Csharp_identifierContext ctx)
        {
            SetNodeValue(ctx, ctx.GetText());
        }


        public override void ExitPython_identifier(EParser.Python_identifierContext ctx)
        {
            SetNodeValue(ctx, ctx.GetText());
        }


        public override void ExitJavaIdentifier(EParser.JavaIdentifierContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            SetNodeValue(ctx, new JavaIdentifierExpression(name));
        }


        public override void ExitCSharpIdentifier(EParser.CSharpIdentifierContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            SetNodeValue(ctx, new CSharpIdentifierExpression(name));
        }

        public override void ExitCSharpPromptoIdentifier(EParser.CSharpPromptoIdentifierContext ctx)
        {
            String name = ctx.DOLLAR_IDENTIFIER().GetText();
            SetNodeValue(ctx, new CSharpIdentifierExpression(name));
        }

        public override void ExitCSharpPrimaryExpression(EParser.CSharpPrimaryExpressionContext ctx)
        {
            CSharpExpression exp = GetNodeValue<CSharpExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }


        public override void ExitPythonIdentifier(EParser.PythonIdentifierContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            SetNodeValue(ctx, new PythonIdentifierExpression(name));
        }

        public override void ExitJava_primary_expression(EParser.Java_primary_expressionContext ctx)
        {
            JavaExpression exp = GetNodeValue<JavaExpression>(ctx.GetChild(0));
            SetNodeValue(ctx, exp);
        }

        public override void ExitJavaPrimaryExpression(EParser.JavaPrimaryExpressionContext ctx)
        {
            JavaExpression exp = GetNodeValue<JavaExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }

        public override void ExitJava_this_expression(EParser.Java_this_expressionContext ctx)
        {
            SetNodeValue(ctx, new JavaThisExpression());
        }

        public override void ExitPythonSelfExpression(EParser.PythonSelfExpressionContext ctx)
        {
            SetNodeValue(ctx, new PythonSelfExpression());
        }

        public override void ExitJavaChildIdentifier(EParser.JavaChildIdentifierContext ctx)
        {
            JavaIdentifierExpression parent = GetNodeValue<JavaIdentifierExpression>(ctx.parent);
            String name = GetNodeValue<String>(ctx.name);
            JavaIdentifierExpression child = new JavaIdentifierExpression(parent, name);
            SetNodeValue(ctx, child);
        }


        public override void ExitCSharpChildIdentifier(EParser.CSharpChildIdentifierContext ctx)
        {
            CSharpIdentifierExpression parent = GetNodeValue<CSharpIdentifierExpression>(ctx.parent);
            String name = GetNodeValue<String>(ctx.name);
            CSharpIdentifierExpression child = new CSharpIdentifierExpression(parent, name);
            SetNodeValue(ctx, child);
        }


        public override void ExitPythonChildIdentifier(EParser.PythonChildIdentifierContext ctx)
        {
            PythonIdentifierExpression parent = GetNodeValue<PythonIdentifierExpression>(ctx.parent);
            String name = GetNodeValue<String>(ctx.name);
            PythonIdentifierExpression child = new PythonIdentifierExpression(parent, name);
            SetNodeValue(ctx, child);
        }



        public override void ExitJavaClassIdentifier(EParser.JavaClassIdentifierContext ctx)
        {
            JavaIdentifierExpression klass = GetNodeValue<JavaIdentifierExpression>(ctx.klass);
            SetNodeValue(ctx, klass);
        }


        public override void ExitJavaChildClassIdentifier(EParser.JavaChildClassIdentifierContext ctx)
        {
            JavaIdentifierExpression parent = GetNodeValue<JavaIdentifierExpression>(ctx.parent);
            JavaIdentifierExpression child = new JavaIdentifierExpression(parent, ctx.name.Text);
            SetNodeValue(ctx, child);
        }


        public override void ExitCSharpSelectorExpression(EParser.CSharpSelectorExpressionContext ctx)
        {
            CSharpExpression parent = GetNodeValue<CSharpExpression>(ctx.parent);
            CSharpSelectorExpression child = GetNodeValue<CSharpSelectorExpression>(ctx.child);
            child.SetParent(parent);
            SetNodeValue(ctx, child);
        }


        public override void ExitJavaSelectorExpression(EParser.JavaSelectorExpressionContext ctx)
        {
            JavaExpression parent = GetNodeValue<JavaExpression>(ctx.parent);
            JavaSelectorExpression child = GetNodeValue<JavaSelectorExpression>(ctx.child);
            child.SetParent(parent);
            SetNodeValue(ctx, child);
        }


        public override void ExitCsharp_item_expression(EParser.Csharp_item_expressionContext ctx)
        {
            CSharpExpression exp = GetNodeValue<CSharpExpression>(ctx.exp);
            SetNodeValue(ctx, new CSharpItemExpression(exp));
        }


        public override void ExitJava_item_expression(EParser.Java_item_expressionContext ctx)
        {
            JavaExpression exp = GetNodeValue<JavaExpression>(ctx.exp);
            SetNodeValue(ctx, new JavaItemExpression(exp));
        }

        public override void ExitCsharp_method_expression(EParser.Csharp_method_expressionContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            CSharpExpressionList args = GetNodeValue<CSharpExpressionList>(ctx.args);
            SetNodeValue(ctx, new CSharpMethodExpression(name, args));
        }

        public override void ExitCsharp_primary_expression(EParser.Csharp_primary_expressionContext ctx)
        {
            CSharpExpression exp = GetNodeValue<CSharpExpression>(ctx.GetChild(0));
            SetNodeValue(ctx, exp);
        }

        public override void ExitCsharp_this_expression(EParser.Csharp_this_expressionContext ctx)
        {
            SetNodeValue(ctx, new CSharpThisExpression());
        }

        public override void ExitCSharpMethodExpression(EParser.CSharpMethodExpressionContext ctx)
        {
            CSharpExpression exp = GetNodeValue<CSharpExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }


        public override void ExitCSharpItemExpression(EParser.CSharpItemExpressionContext ctx)
        {
            CSharpExpression exp = GetNodeValue<CSharpExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }


        public override void ExitJavaItemExpression(EParser.JavaItemExpressionContext ctx)
        {
            JavaExpression exp = GetNodeValue<JavaExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }


        public override void ExitJavaStatement(EParser.JavaStatementContext ctx)
        {
            JavaExpression exp = GetNodeValue<JavaExpression>(ctx.exp);
            SetNodeValue(ctx, new JavaStatement(exp, false));
        }


        public override void ExitCSharpStatement(EParser.CSharpStatementContext ctx)
        {
            CSharpExpression exp = GetNodeValue<CSharpExpression>(ctx.exp);
            SetNodeValue(ctx, new CSharpStatement(exp, false));
        }


        public override void ExitPythonStatement(EParser.PythonStatementContext ctx)
        {
            PythonExpression exp = GetNodeValue<PythonExpression>(ctx.exp);
            SetNodeValue(ctx, new PythonStatement(exp, false));
        }


        public override void ExitJavaReturnStatement(EParser.JavaReturnStatementContext ctx)
        {
            JavaExpression exp = GetNodeValue<JavaExpression>(ctx.exp);
            SetNodeValue(ctx, new JavaStatement(exp, true));
        }


        public override void ExitCSharpReturnStatement(EParser.CSharpReturnStatementContext ctx)
        {
            CSharpExpression exp = GetNodeValue<CSharpExpression>(ctx.exp);
            SetNodeValue(ctx, new CSharpStatement(exp, true));
        }


        public override void ExitPythonReturnStatement(EParser.PythonReturnStatementContext ctx)
        {
            PythonExpression exp = GetNodeValue<PythonExpression>(ctx.exp);
            SetNodeValue(ctx, new PythonStatement(exp, true));
        }


        public override void ExitJavaNativeStatement(EParser.JavaNativeStatementContext ctx)
        {
            JavaStatement stmt = GetNodeValue<JavaStatement>(ctx.java_statement());
            SetNodeValue(ctx, new JavaNativeCall(stmt));
        }


        public override void ExitCSharpNativeStatement(EParser.CSharpNativeStatementContext ctx)
        {
            CSharpStatement stmt = GetNodeValue<CSharpStatement>(ctx.csharp_statement());
            SetNodeValue(ctx, new CSharpNativeCall(stmt));
        }


        public override void ExitPython2NativeStatement(EParser.Python2NativeStatementContext ctx)
        {
            PythonStatement stmt = GetNodeValue<PythonStatement>(ctx.python_native_statement());
            SetNodeValue(ctx, new Python2NativeCall(stmt));
        }


        public override void ExitPython3NativeStatement(EParser.Python3NativeStatementContext ctx)
        {
            PythonStatement stmt = GetNodeValue<PythonStatement>(ctx.python_native_statement());
            SetNodeValue(ctx, new Python3NativeCall(stmt));
        }


        public override void ExitNative_method_declaration(EParser.Native_method_declarationContext ctx)
        {
            IType type = GetNodeValue<IType>(ctx.typ);
            String name = GetNodeValue<String>(ctx.name);
            ParameterList args = GetNodeValue<ParameterList>(ctx.args);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            SetNodeValue(ctx, new NativeMethodDeclaration(name, args, type, stmts));
        }

        public override void ExitCSharpArgumentList(EParser.CSharpArgumentListContext ctx)
        {
            CSharpExpression item = GetNodeValue<CSharpExpression>(ctx.item);
            SetNodeValue(ctx, new CSharpExpressionList(item));
        }

        public override void ExitCSharpArgumentListItem(EParser.CSharpArgumentListItemContext ctx)
        {
            CSharpExpression item = GetNodeValue<CSharpExpression>(ctx.item);
            CSharpExpressionList items = GetNodeValue<CSharpExpressionList>(ctx.items);
            items.Add(item);
            SetNodeValue(ctx, items);
        }


        public override void ExitJavaArgumentList(EParser.JavaArgumentListContext ctx)
        {
            JavaExpression item = GetNodeValue<JavaExpression>(ctx.item);
            SetNodeValue(ctx, new JavaExpressionList(item));
        }


        public override void ExitJavaArgumentListItem(EParser.JavaArgumentListItemContext ctx)
        {
            JavaExpression item = GetNodeValue<JavaExpression>(ctx.item);
            JavaExpressionList items = GetNodeValue<JavaExpressionList>(ctx.items);
            items.Add(item);
            SetNodeValue(ctx, items);
        }


        public override void ExitJava_method_expression(EParser.Java_method_expressionContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            JavaExpressionList args = GetNodeValue<JavaExpressionList>(ctx.args);
            SetNodeValue(ctx, new JavaMethodExpression(name, args));
        }


        public override void ExitJavaMethodExpression(EParser.JavaMethodExpressionContext ctx)
        {
            JavaExpression exp = GetNodeValue<JavaExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }


        public override void ExitFullDeclarationList(EParser.FullDeclarationListContext ctx)
        {
            DeclarationList items = GetNodeValue<DeclarationList>(ctx.declarations());
            if (items == null)
                items = new DeclarationList();
            SetNodeValue(ctx, items);
        }


        public override void ExitDeclaration(EParser.DeclarationContext ctx)
        {
            List<CommentStatement> comments = ReadComments(ctx.comment_statement());
            List<Annotation> annotations = ReadAnnotations(ctx.annotation_constructor());
            IParseTree ctx_ = ctx.children[ctx.ChildCount - 1];
            IDeclaration decl = GetNodeValue<IDeclaration>(ctx_);
            if (decl != null)
            {
                decl.Comments = comments;
                decl.Annotations = annotations;
                SetNodeValue(ctx, decl);
            }
        }

        public override void ExitDeclarations(EParser.DeclarationsContext ctx)
        {
            DeclarationList items = new DeclarationList();
            foreach (ParserRuleContext rule in ctx.declaration())
            {
                IDeclaration item = GetNodeValue<IDeclaration>(rule);
                items.Add(item);
            }
            SetNodeValue(ctx, items);
        }


        public override void ExitIteratorExpression(EParser.IteratorExpressionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            string name = GetNodeValue<string>(ctx.name);
            IExpression source = GetNodeValue<IExpression>(ctx.source);
            SetNodeValue(ctx, new IteratorExpression(name, source, exp));
        }

        public override void ExitIteratorType(EParser.IteratorTypeContext ctx)
        {
            IType type = GetNodeValue<IType>(ctx.i);
            SetNodeValue(ctx, new IteratorType(type));
        }

        public override void ExitJavaBooleanLiteral(EParser.JavaBooleanLiteralContext ctx)
        {
            SetNodeValue(ctx, new JavaBooleanLiteral(ctx.GetText()));
        }


        public override void ExitJavaIntegerLiteral(EParser.JavaIntegerLiteralContext ctx)
        {
            SetNodeValue(ctx, new JavaIntegerLiteral(ctx.GetText()));
        }


        public override void ExitJavaDecimalLiteral(EParser.JavaDecimalLiteralContext ctx)
        {
            SetNodeValue(ctx, new JavaDecimalLiteral(ctx.GetText()));
        }


        public override void ExitJavaCharacterLiteral(EParser.JavaCharacterLiteralContext ctx)
        {
            SetNodeValue(ctx, new JavaCharacterLiteral(ctx.GetText()));
        }


        public override void ExitJavaTextLiteral(EParser.JavaTextLiteralContext ctx)
        {
            SetNodeValue(ctx, new JavaTextLiteral(ctx.GetText()));
        }


        public override void ExitCSharpBooleanLiteral(EParser.CSharpBooleanLiteralContext ctx)
        {
            SetNodeValue(ctx, new CSharpBooleanLiteral(ctx.GetText()));
        }


        public override void ExitCSharpIntegerLiteral(EParser.CSharpIntegerLiteralContext ctx)
        {
            SetNodeValue(ctx, new CSharpIntegerLiteral(ctx.GetText()));
        }


        public override void ExitCSharpDecimalLiteral(EParser.CSharpDecimalLiteralContext ctx)
        {
            SetNodeValue(ctx, new CSharpDecimalLiteral(ctx.GetText()));
        }


        public override void ExitCSharpCharacterLiteral(EParser.CSharpCharacterLiteralContext ctx)
        {
            SetNodeValue(ctx, new CSharpCharacterLiteral(ctx.GetText()));
        }


        public override void ExitCSharpTextLiteral(EParser.CSharpTextLiteralContext ctx)
        {
            SetNodeValue(ctx, new CSharpTextLiteral(ctx.GetText()));
        }


        public override void ExitPythonBooleanLiteral(EParser.PythonBooleanLiteralContext ctx)
        {
            SetNodeValue(ctx, new PythonBooleanLiteral(ctx.GetText()));
        }

        public override void ExitPythonCharacterLiteral(EParser.PythonCharacterLiteralContext ctx)
        {
            SetNodeValue(ctx, new PythonCharacterLiteral(ctx.t.Text));
        }


        public override void ExitPythonIntegerLiteral(EParser.PythonIntegerLiteralContext ctx)
        {
            SetNodeValue(ctx, new PythonIntegerLiteral(ctx.GetText()));
        }


        public override void ExitPythonDecimalLiteral(EParser.PythonDecimalLiteralContext ctx)
        {
            SetNodeValue(ctx, new PythonDecimalLiteral(ctx.GetText()));
        }


        public override void ExitPythonTextLiteral(EParser.PythonTextLiteralContext ctx)
        {
            SetNodeValue(ctx, new PythonTextLiteral(ctx.GetText()));
        }


        public override void ExitPythonLiteralExpression(EParser.PythonLiteralExpressionContext ctx)
        {
            PythonExpression exp = GetNodeValue<PythonExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }


        public override void ExitJavaCategoryBinding(EParser.JavaCategoryBindingContext ctx)
        {
            JavaIdentifierExpression map = GetNodeValue<JavaIdentifierExpression>(ctx.binding);
            SetNodeValue(ctx, new JavaNativeCategoryBinding(map));
        }


        public override void ExitCSharpCategoryBinding(EParser.CSharpCategoryBindingContext ctx)
        {
            CSharpIdentifierExpression map = GetNodeValue<CSharpIdentifierExpression>(ctx.binding);
            SetNodeValue(ctx, new CSharpNativeCategoryBinding(map));
        }



        public override void ExitPython_module(EParser.Python_moduleContext ctx)
        {
            List<String> ids = new List<String>();
            foreach (EParser.Python_identifierContext ic in ctx.python_identifier())
                ids.Add(ic.GetText());
            PythonModule module = new PythonModule(ids);
            SetNodeValue(ctx, module);
        }


        public override void ExitPython_native_statement(EParser.Python_native_statementContext ctx)
        {
            PythonStatement stmt = GetNodeValue<PythonStatement>(ctx.python_statement());
            PythonModule module = GetNodeValue<PythonModule>(ctx.python_module());
            stmt.setModule(module);
            SetNodeValue(ctx, stmt);
        }


        public override void ExitPython2CategoryBinding(EParser.Python2CategoryBindingContext ctx)
        {
            PythonNativeCategoryBinding map = GetNodeValue<PythonNativeCategoryBinding>(ctx.binding);
            SetNodeValue(ctx, new Python2NativeCategoryBinding(map));
        }


        public override void ExitPython3CategoryBinding(EParser.Python3CategoryBindingContext ctx)
        {
            PythonNativeCategoryBinding map = GetNodeValue<PythonNativeCategoryBinding>(ctx.binding);
            SetNodeValue(ctx, new Python3NativeCategoryBinding(map));
        }


        public override void ExitPython_category_binding(EParser.Python_category_bindingContext ctx)
        {
            String identifier = ctx.identifier().GetText();
            PythonModule module = GetNodeValue<PythonModule>(ctx.python_module());
            PythonNativeCategoryBinding map = new PythonNativeCategoryBinding(identifier, module);
            SetNodeValue(ctx, map);
        }


        public override void ExitPythonGlobalMethodExpression(EParser.PythonGlobalMethodExpressionContext ctx)
        {
            PythonMethodExpression exp = GetNodeValue<PythonMethodExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }


        public override void ExitPython_method_expression(EParser.Python_method_expressionContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            PythonArgumentList args = GetNodeValue<PythonArgumentList>(ctx.args);
            PythonMethodExpression method = new PythonMethodExpression(name);
            method.setArguments(args);
            SetNodeValue(ctx, method);
        }


        public override void ExitPythonIdentifierExpression(EParser.PythonIdentifierExpressionContext ctx)
        {
            PythonIdentifierExpression exp = GetNodeValue<PythonIdentifierExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }


        public override void ExitPythonNamedArgumentList(EParser.PythonNamedArgumentListContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            PythonExpression exp = GetNodeValue<PythonExpression>(ctx.exp);
            PythonNamedArgument arg = new PythonNamedArgument(name, exp);
            SetNodeValue(ctx, new PythonArgumentList(arg));
        }


        public override void ExitPythonNamedArgumentListItem(EParser.PythonNamedArgumentListItemContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            PythonExpression exp = GetNodeValue<PythonExpression>(ctx.exp);
            PythonNamedArgument arg = new PythonNamedArgument(name, exp);
            PythonArgumentList items = GetNodeValue<PythonArgumentList>(ctx.items);
            items.Add(arg);
            SetNodeValue(ctx, items);
        }

        public override void ExitPythonOrdinalArgumentList(EParser.PythonOrdinalArgumentListContext ctx)
        {
            PythonExpression exp = GetNodeValue<PythonExpression>(ctx.item);
            PythonOrdinalArgument arg = new PythonOrdinalArgument(exp);
            SetNodeValue(ctx, new PythonArgumentList(arg));
        }


        public override void ExitPythonOrdinalArgumentListItem(EParser.PythonOrdinalArgumentListItemContext ctx)
        {
            PythonExpression exp = GetNodeValue<PythonExpression>(ctx.item);
            PythonOrdinalArgument arg = new PythonOrdinalArgument(exp);
            PythonArgumentList items = GetNodeValue<PythonArgumentList>(ctx.items);
            items.Add(arg);
            SetNodeValue(ctx, items);
        }

        public override void ExitPythonOrdinalOnlyArgumentList(EParser.PythonOrdinalOnlyArgumentListContext ctx)
        {
            PythonArgumentList ordinal = GetNodeValue<PythonArgumentList>(ctx.ordinal);
            SetNodeValue(ctx, ordinal);
        }


        public override void ExitPythonSelectorExpression(EParser.PythonSelectorExpressionContext ctx)
        {
            PythonExpression parent = GetNodeValue<PythonExpression>(ctx.parent);
            PythonSelectorExpression selector = GetNodeValue<PythonSelectorExpression>(ctx.child);
            selector.setParent(parent);
            SetNodeValue(ctx, selector);
        }


        public override void ExitPythonArgumentList(EParser.PythonArgumentListContext ctx)
        {
            PythonArgumentList ordinal = GetNodeValue<PythonArgumentList>(ctx.ordinal);
            PythonArgumentList named = GetNodeValue<PythonArgumentList>(ctx.named);
            ordinal.AddRange(named);
            SetNodeValue(ctx, ordinal);
        }


        public override void ExitPythonMethodExpression(EParser.PythonMethodExpressionContext ctx)
        {
            PythonMethodExpression exp = GetNodeValue<PythonMethodExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }


        public override void ExitPythonNamedOnlyArgumentList(EParser.PythonNamedOnlyArgumentListContext ctx)
        {
            PythonArgumentList named = GetNodeValue<PythonArgumentList>(ctx.named);
            SetNodeValue(ctx, named);
        }


        public override void ExitPythonPromptoIdentifier(EParser.PythonPromptoIdentifierContext ctx)
        {
            String name = ctx.DOLLAR_IDENTIFIER().GetText();
            SetNodeValue(ctx, new PythonIdentifierExpression(name));
        }

        public override void ExitPythonPrimaryExpression(EParser.PythonPrimaryExpressionContext ctx)
        {
            PythonExpression exp = GetNodeValue<PythonExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }


        public override void ExitNativeCategoryBindingList(EParser.NativeCategoryBindingListContext ctx)
        {
            NativeCategoryBinding item = GetNodeValue<NativeCategoryBinding>(ctx.item);
            NativeCategoryBindingList items = new NativeCategoryBindingList(item);
            SetNodeValue(ctx, items);
        }


        public override void ExitNativeCategoryBindingListItem(EParser.NativeCategoryBindingListItemContext ctx)
        {
            NativeCategoryBinding item = GetNodeValue<NativeCategoryBinding>(ctx.item);
            NativeCategoryBindingList items = GetNodeValue<NativeCategoryBindingList>(ctx.items);
            items.Add(item);
            SetNodeValue(ctx, items);
        }


        public override void ExitNative_category_bindings(EParser.Native_category_bindingsContext ctx)
        {
            NativeCategoryBindingList items = GetNodeValue<NativeCategoryBindingList>(ctx.items);
            SetNodeValue(ctx, items);
        }


        public override void ExitNative_category_declaration(EParser.Native_category_declarationContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            IdentifierList attrs = GetNodeValue<IdentifierList>(ctx.attrs);
            NativeCategoryBindingList bindings = GetNodeValue<NativeCategoryBindingList>(ctx.bindings);
            MethodDeclarationList methods = GetNodeValue<MethodDeclarationList>(ctx.methods);
            NativeCategoryDeclaration decl = new NativeCategoryDeclaration(name, attrs, bindings, null, methods);
            decl.Storable = ctx.STORABLE() != null;
            SetNodeValue(ctx, decl);
        }


        public override void ExitNative_widget_declaration(EParser.Native_widget_declarationContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            NativeCategoryBindingList bindings = GetNodeValue<NativeCategoryBindingList>(ctx.bindings);
            MethodDeclarationList methods = GetNodeValue<MethodDeclarationList>(ctx.methods);
            SetNodeValue(ctx, new NativeWidgetDeclaration(name, bindings, methods));
        }


        public override void ExitNativeCategoryDeclaration(EParser.NativeCategoryDeclarationContext ctx)
        {
            IDeclaration decl = GetNodeValue<IDeclaration>(ctx.decl);
            SetNodeValue(ctx, decl);
        }


        public override void ExitNative_resource_declaration(EParser.Native_resource_declarationContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            IdentifierList attrs = GetNodeValue<IdentifierList>(ctx.attrs);
            NativeCategoryBindingList bindings = GetNodeValue<NativeCategoryBindingList>(ctx.bindings);
            MethodDeclarationList methods = GetNodeValue<MethodDeclarationList>(ctx.methods);
            NativeResourceDeclaration decl = new NativeResourceDeclaration(name, attrs, bindings, null, methods);
            decl.Storable = ctx.STORABLE() != null;
            SetNodeValue(ctx, decl);
        }


        public override void ExitResource_declaration(EParser.Resource_declarationContext ctx)
        {
            IDeclaration decl = GetNodeValue<IDeclaration>(ctx.native_resource_declaration());
            SetNodeValue(ctx, decl);
        }


        public override void ExitParenthesis_expression(EParser.Parenthesis_expressionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.expression());
            SetNodeValue(ctx, new ParenthesisExpression(exp));
        }


        public override void ExitParenthesisExpression(EParser.ParenthesisExpressionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }


        public override void ExitNative_symbol_list(EParser.Native_symbol_listContext ctx)
        {
            NativeSymbolList items = new NativeSymbolList();
            foreach (ParserRuleContext rule in ctx.native_symbol())
            {
                NativeSymbol item = GetNodeValue<NativeSymbol>(rule);
                items.add(item);
            }
            SetNodeValue(ctx, items);
        }


        public override void ExitEnum_native_declaration(EParser.Enum_native_declarationContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            NativeType type = GetNodeValue<NativeType>(ctx.typ);
            NativeSymbolList symbols = GetNodeValue<NativeSymbolList>(ctx.symbols);
            SetNodeValue(ctx, new EnumeratedNativeDeclaration(name, type, symbols));
        }


        public override void ExitFor_each_statement(EParser.For_each_statementContext ctx)
        {
            String name1 = GetNodeValue<String>(ctx.name1);
            String name2 = GetNodeValue<String>(ctx.name2);
            IExpression source = GetNodeValue<IExpression>(ctx.source);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            SetNodeValue(ctx, new ForEachStatement(name1, name2, source, stmts));
        }


        public override void ExitForEachStatement(EParser.ForEachStatementContext ctx)
        {
            IStatement stmt = GetNodeValue<IStatement>(ctx.stmt);
            SetNodeValue(ctx, stmt);
        }


        public override void ExitSymbols_token(EParser.Symbols_tokenContext ctx)
        {
            SetNodeValue(ctx, ctx.GetText());
        }


        public override void ExitKey_token(EParser.Key_tokenContext ctx)
        {
            SetNodeValue(ctx, ctx.GetText());
        }


        public override void ExitValue_token(EParser.Value_tokenContext ctx)
        {
            SetNodeValue(ctx, ctx.GetText());
        }


        public override void ExitNamed_argument(EParser.Named_argumentContext ctx)
        {
            String name = GetNodeValue<String>(ctx.variable_identifier());
            UnresolvedParameter arg = new UnresolvedParameter(name);
            IExpression exp = GetNodeValue<IExpression>(ctx.literal_expression());
            arg.DefaultValue = exp;
            SetNodeValue(ctx, arg);
        }


        public override void ExitClosureStatement(EParser.ClosureStatementContext ctx)
        {
            ConcreteMethodDeclaration decl = GetNodeValue<ConcreteMethodDeclaration>(ctx.decl);
            SetNodeValue(ctx, new DeclarationStatement<ConcreteMethodDeclaration>(decl));
        }


        public override void ExitReturn_statement(EParser.Return_statementContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, new ReturnStatement(exp));
        }


        public override void ExitReturnStatement(EParser.ReturnStatementContext ctx)
        {
            IStatement stmt = GetNodeValue<IStatement>(ctx.stmt);
            SetNodeValue(ctx, stmt);
        }


        public override void ExitClosureExpression(EParser.ClosureExpressionContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            SetNodeValue(ctx, new MethodExpression(name));
        }


        public override void ExitIf_statement(EParser.If_statementContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            IfElementList elseIfs = GetNodeValue<IfElementList>(ctx.elseIfs);
            StatementList elseStmts = GetNodeValue<StatementList>(ctx.elseStmts);
            SetNodeValue(ctx, new IfStatement(exp, stmts, elseIfs, elseStmts));
        }


        public override void ExitElseIfStatementList(EParser.ElseIfStatementListContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            IfElement elem = new IfElement(exp, stmts);
            SetNodeValue(ctx, new IfElementList(elem));
        }


        public override void ExitElseIfStatementListItem(EParser.ElseIfStatementListItemContext ctx)
        {
            IfElementList items = GetNodeValue<IfElementList>(ctx.items);
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            IfElement elem = new IfElement(exp, stmts);
            items.Add(elem);
            SetNodeValue(ctx, items);
        }


        public override void ExitIfStatement(EParser.IfStatementContext ctx)
        {
            IStatement stmt = GetNodeValue<IStatement>(ctx.stmt);
            SetNodeValue(ctx, stmt);
        }


        public override void ExitSuperExpression(EParser.SuperExpressionContext ctx)
        {
            SetNodeValue(ctx, new SuperExpression());
        }


        public override void ExitSwitchStatement(EParser.SwitchStatementContext ctx)
        {
            IStatement stmt = GetNodeValue<IStatement>(ctx.stmt);
            SetNodeValue(ctx, stmt);
        }


        public override void ExitAssignTupleStatement(EParser.AssignTupleStatementContext ctx)
        {
            IStatement stmt = GetNodeValue<IStatement>(ctx.stmt);
            SetNodeValue(ctx, stmt);
        }


        public override void ExitRaiseStatement(EParser.RaiseStatementContext ctx)
        {
            IStatement stmt = GetNodeValue<IStatement>(ctx.stmt);
            SetNodeValue(ctx, stmt);
        }


        public override void ExitWriteStatement(EParser.WriteStatementContext ctx)
        {
            IStatement stmt = GetNodeValue<IStatement>(ctx.stmt);
            SetNodeValue(ctx, stmt);
        }


        public override void ExitWithResourceStatement(EParser.WithResourceStatementContext ctx)
        {
            IStatement stmt = GetNodeValue<IStatement>(ctx.stmt);
            SetNodeValue(ctx, stmt);
        }


        public override void ExitWhileStatement(EParser.WhileStatementContext ctx)
        {
            IStatement stmt = GetNodeValue<IStatement>(ctx.stmt);
            SetNodeValue(ctx, stmt);
        }


        public override void ExitDoWhileStatement(EParser.DoWhileStatementContext ctx)
        {
            IStatement stmt = GetNodeValue<IStatement>(ctx.stmt);
            SetNodeValue(ctx, stmt);
        }


        public override void ExitTryStatement(EParser.TryStatementContext ctx)
        {
            IStatement stmt = GetNodeValue<IStatement>(ctx.stmt);
            SetNodeValue(ctx, stmt);
        }


        public override void ExitEqualsExpression(EParser.EqualsExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            SetNodeValue(ctx, new EqualsExpression(left, EqOp.EQUALS, right));
        }


        public override void ExitNotEqualsExpression(EParser.NotEqualsExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            SetNodeValue(ctx, new EqualsExpression(left, EqOp.NOT_EQUALS, right));
        }


        public override void ExitGreaterThanExpression(EParser.GreaterThanExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            SetNodeValue(ctx, new CompareExpression(left, CmpOp.GT, right));
        }


        public override void ExitGreaterThanOrEqualExpression(EParser.GreaterThanOrEqualExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            SetNodeValue(ctx, new CompareExpression(left, CmpOp.GTE, right));
        }


        public override void ExitLessThanExpression(EParser.LessThanExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            SetNodeValue(ctx, new CompareExpression(left, CmpOp.LT, right));
        }


        public override void ExitLessThanOrEqualExpression(EParser.LessThanOrEqualExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            SetNodeValue(ctx, new CompareExpression(left, CmpOp.LTE, right));
        }


        public override void ExitAtomicSwitchCase(EParser.AtomicSwitchCaseContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            SetNodeValue(ctx, new AtomicSwitchCase(exp, stmts));
        }


        public override void ExitCollectionSwitchCase(EParser.CollectionSwitchCaseContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            SetNodeValue(ctx, new CollectionSwitchCase(exp, stmts));
        }

        public override void ExitCommentStatement(EParser.CommentStatementContext ctx)
        {
            SetNodeValue(ctx, GetNodeValue<Object>(ctx.comment_statement()));
        }

        public override void ExitComment_statement(EParser.Comment_statementContext ctx)
        {
            SetNodeValue(ctx, new CommentStatement(ctx.GetText()));
        }

        public override void ExitSwitch_case_statement_list(EParser.Switch_case_statement_listContext ctx)
        {
            SwitchCaseList items = new SwitchCaseList();
            foreach (ParserRuleContext rule in ctx.switch_case_statement())
            {
                SwitchCase item = GetNodeValue<SwitchCase>(rule);
                items.Add(item);
            }
            SetNodeValue(ctx, items);
        }


        public override void ExitSwitch_statement(EParser.Switch_statementContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SwitchCaseList cases = GetNodeValue<SwitchCaseList>(ctx.cases);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            SwitchStatement stmt = new SwitchStatement(exp, cases, stmts);
            SetNodeValue(ctx, stmt);
        }


        public override void ExitLiteralRangeLiteral(EParser.LiteralRangeLiteralContext ctx)
        {
            IExpression low = GetNodeValue<IExpression>(ctx.low);
            IExpression high = GetNodeValue<IExpression>(ctx.high);
            SetNodeValue(ctx, new RangeLiteral(low, high));
        }


        public override void ExitLiteralSetLiteral(EParser.LiteralSetLiteralContext ctx)
        {
            ExpressionList items = GetNodeValue<ExpressionList>(ctx.literal_list_literal());
            SetNodeValue(ctx, new SetLiteral(items));
        }

        public override void ExitLiteralListLiteral(EParser.LiteralListLiteralContext ctx)
        {
            ExpressionList exp = GetNodeValue<ExpressionList>(ctx.literal_list_literal());
            SetNodeValue(ctx, new ListLiteral(exp, false));
        }


        public override void ExitLiteral_list_literal(EParser.Literal_list_literalContext ctx)
        {
            ExpressionList items = new ExpressionList();
            foreach (ParserRuleContext rule in ctx.atomic_literal())
            {
                IExpression item = GetNodeValue<IExpression>(rule);
                items.add(item);
            }
            SetNodeValue(ctx, items);
        }


        public override void ExitInExpression(EParser.InExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            SetNodeValue(ctx, new ContainsExpression(left, ContOp.IN, right));
        }


        public override void ExitNotInExpression(EParser.NotInExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            SetNodeValue(ctx, new ContainsExpression(left, ContOp.NOT_IN, right));
        }

        public override void ExitIsATypeExpression(EParser.IsATypeExpressionContext ctx)
        {
            IType type = GetNodeValue<IType>(ctx.category_or_any_type());
            IExpression exp = new TypeExpression(type);
            SetNodeValue(ctx, exp);
        }

        public override void ExitIsOtherExpression(EParser.IsOtherExpressionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.expression());
            SetNodeValue(ctx, exp);
        }

        public override void ExitIsExpression(EParser.IsExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            EqOp op = right is TypeExpression ? EqOp.IS_A : EqOp.IS;
            SetNodeValue(ctx, new EqualsExpression(left, op, right));
        }

        public override void ExitIsNotExpression(EParser.IsNotExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            EqOp op = right is TypeExpression ? EqOp.IS_NOT_A : EqOp.IS_NOT;
            SetNodeValue(ctx, new EqualsExpression(left, op, right));
        }


        public override void ExitCssType([NotNull] EParser.CssTypeContext ctx)
        {
            SetNodeValue(ctx, CssType.Instance);
        }

        public override void ExitHasExpression(EParser.HasExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            SetNodeValue(ctx, new ContainsExpression(left, ContOp.HAS, right));
        }


        public override void ExitHasAllExpression(EParser.HasAllExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            SetNodeValue(ctx, new ContainsExpression(left, ContOp.HAS_ALL, right));
        }


        public override void ExitNotHasAllExpression(EParser.NotHasAllExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            SetNodeValue(ctx, new ContainsExpression(left, ContOp.NOT_HAS_ALL, right));
        }


        public override void ExitHasAnyExpression(EParser.HasAnyExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            SetNodeValue(ctx, new ContainsExpression(left, ContOp.HAS_ANY, right));
        }


        public override void ExitNotHasAnyExpression(EParser.NotHasAnyExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            SetNodeValue(ctx, new ContainsExpression(left, ContOp.NOT_HAS_ANY, right));
        }


        public override void ExitContainsExpression(EParser.ContainsExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            SetNodeValue(ctx, new EqualsExpression(left, EqOp.CONTAINS, right));
        }


        public override void ExitNotContainsExpression(EParser.NotContainsExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            SetNodeValue(ctx, new EqualsExpression(left, EqOp.NOT_CONTAINS, right));
        }


        public override void ExitDivideExpression(EParser.DivideExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            SetNodeValue(ctx, new DivideExpression(left, right));
        }


        public override void ExitIntDivideExpression(EParser.IntDivideExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            SetNodeValue(ctx, new IntDivideExpression(left, right));
        }


        public override void ExitModuloExpression(EParser.ModuloExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            SetNodeValue(ctx, new ModuloExpression(left, right));
        }


        public override void ExitAnnotation_constructor(EParser.Annotation_constructorContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            DictEntryList args = new DictEntryList();
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            if (exp != null)
                args.add(new DictEntry(null, exp));
            foreach (RuleContext argCtx in ctx.annotation_argument())
            {
                DictEntry arg = GetNodeValue<DictEntry>(argCtx);
                args.add(arg);
            }
            SetNodeValue(ctx, new Annotation(name, args));
        }

        public override void ExitAnnotation_argument(EParser.Annotation_argumentContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, new DictEntry(new DictIdentifierKey(name), exp));
        }

        public override void ExitAnnotation_identifier(EParser.Annotation_identifierContext ctx)
        {
            SetNodeValue(ctx, ctx.GetText());
        }


        public override void ExitAnnotation_argument_name(EParser.Annotation_argument_nameContext ctx)
        {
            SetNodeValue(ctx, ctx.GetText());
        }

        public override void ExitAnnotationLiteralValue(EParser.AnnotationLiteralValueContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }

        public override void ExitAnnotationTypeValue(EParser.AnnotationTypeValueContext ctx)
        {
            IType type = GetNodeValue<IType>(ctx.typ);
            SetNodeValue(ctx, new TypeExpression(type));
        }

        public override void ExitAndExpression(EParser.AndExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            SetNodeValue(ctx, new AndExpression(left, right));
        }

        public override void ExitNullLiteral(EParser.NullLiteralContext ctx)
        {
            SetNodeValue(ctx, NullLiteral.Instance);
        }

        public override void ExitOperatorArgument(EParser.OperatorArgumentContext ctx)
        {
            bool mutable = ctx.MUTABLE() != null;
            IParameter arg = GetNodeValue<IParameter>(ctx.arg);
            arg.setMutable(mutable);
            SetNodeValue(ctx, arg);
        }

        public override void ExitOperatorPlus(EParser.OperatorPlusContext ctx)
        {
            SetNodeValue(ctx, Operator.PLUS);
        }

        public override void ExitOperatorMinus(EParser.OperatorMinusContext ctx)
        {
            SetNodeValue(ctx, Operator.MINUS);
        }

        public override void ExitOperatorMultiply(EParser.OperatorMultiplyContext ctx)
        {
            SetNodeValue(ctx, Operator.MULTIPLY);
        }

        public override void ExitOperatorDivide(EParser.OperatorDivideContext ctx)
        {
            SetNodeValue(ctx, Operator.DIVIDE);
        }

        public override void ExitOperatorIDivide(EParser.OperatorIDivideContext ctx)
        {
            SetNodeValue(ctx, Operator.IDIVIDE);
        }

        public override void ExitOperatorModulo(EParser.OperatorModuloContext ctx)
        {
            SetNodeValue(ctx, Operator.MODULO);
        }

        public override void ExitOperator_method_declaration(EParser.Operator_method_declarationContext ctx)
        {
            Operator op = GetNodeValue<Operator>(ctx.op);
            IParameter arg = GetNodeValue<IParameter>(ctx.arg);
            IType typ = GetNodeValue<IType>(ctx.typ);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            OperatorMethodDeclaration decl = new OperatorMethodDeclaration(op, arg, typ, stmts);
            SetNodeValue(ctx, decl);
        }

        public override void ExitOrder_by(EParser.Order_byContext ctx)
        {
            IdentifierList names = new IdentifierList();
            foreach (EParser.Variable_identifierContext ctx_ in ctx.variable_identifier())
                names.add(GetNodeValue<string>(ctx_));
            OrderByClause clause = new OrderByClause(names, ctx.DESC() != null);
            SetNodeValue(ctx, clause);
        }

        public override void ExitOrder_by_list(EParser.Order_by_listContext ctx)
        {
            OrderByClauseList list = new OrderByClauseList();
            foreach (EParser.Order_byContext ctx_ in ctx.order_by())
                list.add(GetNodeValue<OrderByClause>(ctx_));
            SetNodeValue(ctx, list);
        }

        public override void ExitOrExpression(EParser.OrExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            SetNodeValue(ctx, new OrExpression(left, right));
        }


        public override void ExitMultiplyExpression(EParser.MultiplyExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            SetNodeValue(ctx, new MultiplyExpression(left, right));
        }


        public override void ExitMutable_category_type(EParser.Mutable_category_typeContext ctx)
        {
            CategoryType typ = GetNodeValue<CategoryType>(ctx.category_type());
            typ.Mutable = ctx.MUTABLE() != null;
            SetNodeValue(ctx, typ);
        }


        public override void ExitMutableInstanceExpression(EParser.MutableInstanceExpressionContext ctx)
        {
            IExpression source = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, new MutableExpression(source));
        }

        public override void ExitMutableSelectableExpression(EParser.MutableSelectableExpressionContext ctx)
        {
            String name = GetNodeValue<String>(ctx.exp);
            SetNodeValue(ctx, new InstanceExpression(name));
        }


        public override void ExitMutableSelectorExpression(EParser.MutableSelectorExpressionContext ctx)
        {
            IExpression parent = GetNodeValue<IExpression>(ctx.parent);
            SelectorExpression selector = GetNodeValue<SelectorExpression>(ctx.selector);
            selector.setParent(parent);
            SetNodeValue(ctx, selector);
        }

        public override void ExitMinusExpression(EParser.MinusExpressionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, new MinusExpression(exp));
        }


        public override void ExitNotExpression(EParser.NotExpressionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, new NotExpression(exp));
        }


        public override void ExitWhile_statement(EParser.While_statementContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            SetNodeValue(ctx, new WhileStatement(exp, stmts));
        }


        public override void ExitDo_while_statement(EParser.Do_while_statementContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            SetNodeValue(ctx, new DoWhileStatement(exp, stmts));
        }

        public override void ExitSingleton_category_declaration(EParser.Singleton_category_declarationContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            IdentifierList attrs = GetNodeValue<IdentifierList>(ctx.attrs);
            MethodDeclarationList methods = GetNodeValue<MethodDeclarationList>(ctx.methods);
            SetNodeValue(ctx, new SingletonCategoryDeclaration(name, attrs, methods));
        }

        public override void ExitSingletonCategoryDeclaration(EParser.SingletonCategoryDeclarationContext ctx)
        {
            IDeclaration decl = GetNodeValue<IDeclaration>(ctx.decl);
            SetNodeValue(ctx, decl);
        }

        public override void ExitSliceFirstAndLast(EParser.SliceFirstAndLastContext ctx)
        {
            IExpression first = GetNodeValue<IExpression>(ctx.first);
            IExpression last = GetNodeValue<IExpression>(ctx.last);
            SetNodeValue(ctx, new SliceSelector(first, last));
        }


        public override void ExitSliceFirstOnly(EParser.SliceFirstOnlyContext ctx)
        {
            IExpression first = GetNodeValue<IExpression>(ctx.first);
            SetNodeValue(ctx, new SliceSelector(first, null));
        }


        public override void ExitSliceLastOnly(EParser.SliceLastOnlyContext ctx)
        {
            IExpression last = GetNodeValue<IExpression>(ctx.last);
            SetNodeValue(ctx, new SliceSelector(null, last));
        }


        public override void ExitSorted_expression(EParser.Sorted_expressionContext ctx)
        {
            IExpression source = GetNodeValue<IExpression>(ctx.source);
            bool descending = ctx.DESC() != null;
            IExpression key = GetNodeValue<IExpression>(ctx.key);
            SetNodeValue(ctx, new SortedExpression(source, descending, key));
        }


        public override void ExitSorted_key(EParser.Sorted_keyContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.GetChild(0));
            SetNodeValue(ctx, exp);
        }


        public override void ExitSortedExpression(EParser.SortedExpressionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }


        public override void ExitDocumentExpression(EParser.DocumentExpressionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }


        public override void ExitDocument_expression(EParser.Document_expressionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, new DocumentExpression(exp));
        }


        public override void ExitDocumentType(EParser.DocumentTypeContext ctx)
        {
            SetNodeValue(ctx, DocumentType.Instance);
        }


        public override void ExitDocument_literal(EParser.Document_literalContext ctx)
        {
            DictEntryList entries = GetNodeValue<DictEntryList>(ctx.dict_entry_list());
            DocEntryList items = new DocEntryList(entries);
            SetNodeValue(ctx, new DocumentLiteral(items));
        }


        public override void ExitFetchExpression(EParser.FetchExpressionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.GetChild(0));
            SetNodeValue(ctx, exp);
        }


        public override void ExitFetchStatement(EParser.FetchStatementContext ctx)
        {
            SetNodeValue(ctx, GetNodeValue<Object>(ctx.stmt));
        }

        public override void ExitFetchOne(EParser.FetchOneContext ctx)
        {
            CategoryType category = GetNodeValue<CategoryType>(ctx.typ);
            IExpression filter = GetNodeValue<IExpression>(ctx.predicate);
            SetNodeValue(ctx, new FetchOneExpression(category, filter));
        }

        public override void ExitFetchOneAsync(EParser.FetchOneAsyncContext ctx)
        {
            CategoryType category = GetNodeValue<CategoryType>(ctx.typ);
            IExpression filter = GetNodeValue<IExpression>(ctx.predicate);
            String name = GetNodeValue<String>(ctx.name);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            SetNodeValue(ctx, new FetchOneStatement(category, filter, name, stmts));
        }

        public override void ExitFetchMany(EParser.FetchManyContext ctx)
        {
            CategoryType category = GetNodeValue<CategoryType>(ctx.typ);
            IExpression filter = GetNodeValue<IExpression>(ctx.predicate);
            IExpression start = GetNodeValue<IExpression>(ctx.xstart);
            IExpression stop = GetNodeValue<IExpression>(ctx.xstop);
            OrderByClauseList orderBy = GetNodeValue<OrderByClauseList>(ctx.orderby);
            SetNodeValue(ctx, new FetchManyExpression(category, filter, start, stop, orderBy));
        }


        public override void ExitFetchManyAsync(EParser.FetchManyAsyncContext ctx)
        {
            CategoryType category = GetNodeValue<CategoryType>(ctx.typ);
            IExpression filter = GetNodeValue<IExpression>(ctx.predicate);
            IExpression start = GetNodeValue<IExpression>(ctx.xstart);
            IExpression stop = GetNodeValue<IExpression>(ctx.xstop);
            OrderByClauseList orderBy = GetNodeValue<OrderByClauseList>(ctx.orderby);
            String name = GetNodeValue<String>(ctx.name);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            SetNodeValue(ctx, new FetchManyStatement(category, filter, start, stop, orderBy, name, stmts));
        }


        public override void ExitFiltered_list_suffix(EParser.Filtered_list_suffixContext ctx)
        {
            String itemName = GetNodeValue<String>(ctx.name);
            IExpression predicate = GetNodeValue<IExpression>(ctx.predicate);
            SetNodeValue(ctx, new FilteredExpression(itemName, null, predicate));
        }


        public override void ExitFilteredListExpression(EParser.FilteredListExpressionContext ctx)
        {
            FilteredExpression fetch = GetNodeValue<FilteredExpression>(ctx.filtered_list_suffix());
            IExpression source = GetNodeValue<IExpression>(ctx.src);
            fetch.Source = source;
            SetNodeValue(ctx, fetch);
        }


        public override void ExitCode_type(EParser.Code_typeContext ctx)
        {
            SetNodeValue(ctx, CodeType.Instance);
        }


        public override void ExitExecuteExpression(EParser.ExecuteExpressionContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            SetNodeValue(ctx, new ExecuteExpression(name));
        }


        public override void ExitCodeExpression(EParser.CodeExpressionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, new CodeExpression(exp));
        }


        public override void ExitCode_argument(EParser.Code_argumentContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            SetNodeValue(ctx, new CodeParameter(name));
        }


        public override void ExitCategory_symbol(EParser.Category_symbolContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            ArgumentList args = GetNodeValue<ArgumentList>(ctx.args);
            Argument arg = GetNodeValue<Argument>(ctx.arg);
            if (arg != null)
                args.add(arg);
            SetNodeValue(ctx, new CategorySymbol(name, args));
        }


        public override void ExitCategory_symbol_list(EParser.Category_symbol_listContext ctx)
        {
            CategorySymbolList items = new CategorySymbolList();
            foreach (ParserRuleContext rule in ctx.category_symbol())
            {
                CategorySymbol item = GetNodeValue<CategorySymbol>(rule);
                items.add(item);
            }
            SetNodeValue(ctx, items);
        }


        public override void ExitEnum_category_declaration(EParser.Enum_category_declarationContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            IdentifierList attrs = GetNodeValue<IdentifierList>(ctx.attrs);
            String parent = GetNodeValue<String>(ctx.derived);
            IdentifierList derived = parent == null ? null : new IdentifierList(parent);
            CategorySymbolList symbols = GetNodeValue<CategorySymbolList>(ctx.symbols);
            SetNodeValue(ctx, new EnumeratedCategoryDeclaration(name, attrs, derived, symbols));
        }


        public override void ExitReadBlobExpression(EParser.ReadBlobExpressionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }

        public override void ExitRead_all_expression(EParser.Read_all_expressionContext ctx)
        {
            IExpression source = GetNodeValue<IExpression>(ctx.source);
            SetNodeValue(ctx, new ReadAllExpression(source));
        }


        public override void ExitRead_blob_expression(EParser.Read_blob_expressionContext ctx)
        {
            IExpression source = GetNodeValue<IExpression>(ctx.source);
            SetNodeValue(ctx, new ReadBlobExpression(source));
        }


        public override void ExitRead_one_expression(EParser.Read_one_expressionContext ctx)
        {
            IExpression source = GetNodeValue<IExpression>(ctx.source);
            SetNodeValue(ctx, new ReadOneExpression(source));
        }


        public override void ExitReadAllExpression(EParser.ReadAllExpressionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }


        public override void ExitReadOneExpression(EParser.ReadOneExpressionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }

        public override void ExitWrite_statement(EParser.Write_statementContext ctx)
        {
            IExpression what = GetNodeValue<IExpression>(ctx.what);
            IExpression target = GetNodeValue<IExpression>(ctx.target);
            SetNodeValue(ctx, new WriteStatement(what, target));
        }


        public override void ExitWith_resource_statement(EParser.With_resource_statementContext ctx)
        {
            AssignVariableStatement stmt = GetNodeValue<AssignVariableStatement>(ctx.stmt);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            SetNodeValue(ctx, new WithResourceStatement(stmt, stmts));
        }

        public override void ExitWith_singleton_statement(EParser.With_singleton_statementContext ctx)
        {
            String name = GetNodeValue<String>(ctx.typ);
            CategoryType type = new CategoryType(name);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            SetNodeValue(ctx, new WithSingletonStatement(type, stmts));
        }

        public override void ExitWithSingletonStatement(EParser.WithSingletonStatementContext ctx)
        {
            IStatement stmt = GetNodeValue<IStatement>(ctx.stmt);
            SetNodeValue(ctx, stmt);
        }


        public override void ExitAnyType(EParser.AnyTypeContext ctx)
        {
            SetNodeValue(ctx, AnyType.Instance);
        }


        public override void ExitAnyListType(EParser.AnyListTypeContext ctx)
        {
            IType type = GetNodeValue<IType>(ctx.any_type());
            SetNodeValue(ctx, new ListType(type));
        }


        public override void ExitAnyDictType(EParser.AnyDictTypeContext ctx)
        {
            IType type = GetNodeValue<IType>(ctx.any_type());
            SetNodeValue(ctx, new DictType(type));
        }


        public override void ExitCastExpression(EParser.CastExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IType type = GetNodeValue<IType>(ctx.right);
            SetNodeValue(ctx, new CastExpression(left, type, ctx.MUTABLE() != null));
        }

        public override void ExitCatchAtomicStatement(EParser.CatchAtomicStatementContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            SetNodeValue(ctx, new AtomicSwitchCase(new SymbolExpression(name), stmts));
        }


        public override void ExitCatchCollectionStatement(EParser.CatchCollectionStatementContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            SetNodeValue(ctx, new CollectionSwitchCase(exp, stmts));
        }

        public override void ExitCatch_statement_list(EParser.Catch_statement_listContext ctx)
        {
            SwitchCaseList items = new SwitchCaseList();
            foreach (ParserRuleContext rule in ctx.catch_statement())
            {
                SwitchCase item = GetNodeValue<SwitchCase>(rule);
                items.Add(item);
            }
            SetNodeValue(ctx, items);
        }


        public override void ExitTry_statement(EParser.Try_statementContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            SwitchCaseList handlers = GetNodeValue<SwitchCaseList>(ctx.handlers);
            StatementList anyStmts = GetNodeValue<StatementList>(ctx.anyStmts);
            StatementList finalStmts = GetNodeValue<StatementList>(ctx.finalStmts);
            SwitchErrorStatement stmt = new SwitchErrorStatement(name, stmts, handlers, anyStmts, finalStmts);
            SetNodeValue(ctx, stmt);
        }


        public override void ExitRaise_statement(EParser.Raise_statementContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, new RaiseStatement(exp));
        }


        public override void ExitMatchingList(EParser.MatchingListContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.source);
            SetNodeValue(ctx, new MatchingCollectionConstraint(exp));
        }


        public override void ExitMatchingRange(EParser.MatchingRangeContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.source);
            SetNodeValue(ctx, new MatchingCollectionConstraint(exp));
        }


        public override void ExitMatchingExpression(EParser.MatchingExpressionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, new MatchingExpressionConstraint(exp));
        }


        public override void ExitMatchingPattern(EParser.MatchingPatternContext ctx)
        {
            SetNodeValue(ctx, new MatchingPatternConstraint(new TextLiteral(ctx.text.Text)));
        }

        public override void ExitMatchingSet(EParser.MatchingSetContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.source);
            SetNodeValue(ctx, new MatchingCollectionConstraint(exp));
        }

        public override void ExitInvocation_expression(EParser.Invocation_expressionContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            MethodSelector select = new MethodSelector(name);
            SetNodeValue(ctx, new MethodCall(select));
        }


        public override void ExitInvocationExpression(EParser.InvocationExpressionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }


        public override void ExitInvokeStatement(EParser.InvokeStatementContext ctx)
        {
            IStatement exp = GetNodeValue<IStatement>(ctx.exp);
            SetNodeValue(ctx, exp);
        }

        public override void ExitJavascriptBooleanLiteral(EParser.JavascriptBooleanLiteralContext ctx)
        {
            String text = ctx.t.Text;
            SetNodeValue(ctx, new JavaScriptBooleanLiteral(text));
        }


        public override void ExitJavascript_category_binding(EParser.Javascript_category_bindingContext ctx)
        {
            StringBuilder sb = new StringBuilder();
            foreach (EParser.Javascript_identifierContext cx in ctx.javascript_identifier())
                sb.Append(cx.GetText());
            String identifier = sb.ToString();
            JavaScriptModule module = GetNodeValue<JavaScriptModule>(ctx.javascript_module());
            JavaScriptNativeCategoryBinding map = new JavaScriptNativeCategoryBinding(identifier, module);
            SetNodeValue(ctx, map);
        }

        public override void ExitJavaScriptMemberExpression(EParser.JavaScriptMemberExpressionContext ctx)
        {
            String name = ctx.name.GetText();
            SetNodeValue(ctx, new JavaScriptMemberExpression(name));
        }

        public override void ExitJavascript_primary_expression(EParser.Javascript_primary_expressionContext ctx)
        {
            JavaScriptExpression exp = GetNodeValue<JavaScriptExpression>(ctx.GetChild(0));
            SetNodeValue(ctx, exp);
        }

        public override void ExitJavaScriptMethodExpression(EParser.JavaScriptMethodExpressionContext ctx)
        {
            JavaScriptExpression method = GetNodeValue<JavaScriptExpression>(ctx.method);
            SetNodeValue(ctx, method);
        }

        public override void ExitJavascript_this_expression(EParser.Javascript_this_expressionContext ctx)
        {
            SetNodeValue(ctx, new JavaScriptThisExpression());
        }

        public override void ExitJavascript_identifier(EParser.Javascript_identifierContext ctx)
        {
            String name = ctx.GetText();
            SetNodeValue(ctx, name);
        }


        public override void ExitJavascript_method_expression(EParser.Javascript_method_expressionContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            JavaScriptMethodExpression method = new JavaScriptMethodExpression(name);
            JavaScriptExpressionList args = GetNodeValue<JavaScriptExpressionList>(ctx.args);
            method.setArguments(args);
            SetNodeValue(ctx, method);
        }


        public override void ExitJavascript_module(EParser.Javascript_moduleContext ctx)
        {
            List<String> ids = new List<String>();
            foreach (EParser.Javascript_identifierContext ic in ctx.javascript_identifier())
                ids.Add(ic.GetText());
            JavaScriptModule module = new JavaScriptModule(ids);
            SetNodeValue(ctx, module);
        }


        public override void ExitJavascript_native_statement(EParser.Javascript_native_statementContext ctx)
        {
            JavaScriptStatement stmt = GetNodeValue<JavaScriptStatement>(ctx.javascript_statement());
            JavaScriptModule module = GetNodeValue<JavaScriptModule>(ctx.javascript_module());
            stmt.setModule(module);
            SetNodeValue(ctx, stmt);
        }

        public override void ExitJavascript_new_expression(EParser.Javascript_new_expressionContext ctx)
        {
            JavaScriptMethodExpression method = GetNodeValue<JavaScriptMethodExpression>(ctx.javascript_method_expression());
            SetNodeValue(ctx, new JavaScriptNewExpression(method));
        }

        public override void ExitJavascriptArgumentList(EParser.JavascriptArgumentListContext ctx)
        {
            JavaScriptExpression exp = GetNodeValue<JavaScriptExpression>(ctx.item);
            JavaScriptExpressionList list = new JavaScriptExpressionList(exp);
            SetNodeValue(ctx, list);
        }


        public override void ExitJavascriptArgumentListItem(EParser.JavascriptArgumentListItemContext ctx)
        {
            JavaScriptExpression exp = GetNodeValue<JavaScriptExpression>(ctx.item);
            JavaScriptExpressionList list = GetNodeValue<JavaScriptExpressionList>(ctx.items);
            list.Add(exp);
            SetNodeValue(ctx, list);
        }


        public override void ExitJavaScriptCategoryBinding(EParser.JavaScriptCategoryBindingContext ctx)
        {
            SetNodeValue(ctx, GetNodeValue<Object>(ctx.binding));
        }

        public override void ExitJavascriptCharacterLiteral(EParser.JavascriptCharacterLiteralContext ctx)
        {
            String text = ctx.t.Text;
            SetNodeValue(ctx, new JavaScriptCharacterLiteral(text));
        }


        public override void ExitJavascriptDecimalLiteral(EParser.JavascriptDecimalLiteralContext ctx)
        {
            String text = ctx.t.Text;
            SetNodeValue(ctx, new JavaScriptDecimalLiteral(text));
        }

        public override void ExitJavascript_identifier_expression(EParser.Javascript_identifier_expressionContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            SetNodeValue(ctx, new JavaScriptIdentifierExpression(name));
        }


        public override void ExitJavascriptIntegerLiteral(EParser.JavascriptIntegerLiteralContext ctx)
        {
            String text = ctx.t.Text;
            SetNodeValue(ctx, new JavaScriptIntegerLiteral(text));
        }

        public override void ExitJavaScriptNativeStatement(EParser.JavaScriptNativeStatementContext ctx)
        {
            JavaScriptStatement stmt = GetNodeValue<JavaScriptStatement>(ctx.javascript_native_statement());
            SetNodeValue(ctx, new JavaScriptNativeCall(stmt));
        }


        public override void ExitJavascriptPrimaryExpression(EParser.JavascriptPrimaryExpressionContext ctx)
        {
            JavaScriptExpression exp = GetNodeValue<JavaScriptExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }


        public override void ExitJavascriptReturnStatement(EParser.JavascriptReturnStatementContext ctx)
        {
            JavaScriptExpression exp = GetNodeValue<JavaScriptExpression>(ctx.exp);
            SetNodeValue(ctx, new JavaScriptStatement(exp, true));
        }


        public override void ExitJavascriptSelectorExpression(EParser.JavascriptSelectorExpressionContext ctx)
        {
            JavaScriptExpression parent = GetNodeValue<JavaScriptExpression>(ctx.parent);
            JavaScriptSelectorExpression child = GetNodeValue<JavaScriptSelectorExpression>(ctx.child);
            child.setParent(parent);
            SetNodeValue(ctx, child);
        }

        public override void ExitJavascriptTextLiteral(EParser.JavascriptTextLiteralContext ctx)
        {
            String text = ctx.t.Text;
            SetNodeValue(ctx, new JavaScriptTextLiteral(text));
        }

        public override void ExitJavascriptStatement(EParser.JavascriptStatementContext ctx)
        {
            JavaScriptExpression exp = GetNodeValue<JavaScriptExpression>(ctx.exp);
            SetNodeValue(ctx, new JavaScriptStatement(exp, false));
        }

        public override void ExitLiteral_expression(EParser.Literal_expressionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.GetChild(0));
            SetNodeValue(ctx, exp);
        }

        public override void ExitMethod_declaration(EParser.Method_declarationContext ctx)
        {
            IDeclaration exp = GetNodeValue<IDeclaration>(ctx.GetChild(0));
            SetNodeValue(ctx, exp);
        }

        public override void ExitMethod_identifier(EParser.Method_identifierContext ctx)
        {
            Object exp = GetNodeValue<Object>(ctx.GetChild(0));
            SetNodeValue(ctx, exp);
        }

        public override void ExitOperator_argument(EParser.Operator_argumentContext ctx)
        {
            IParameter exp = GetNodeValue<IParameter>(ctx.GetChild(0));
            SetNodeValue(ctx, exp);
        }

        public override void ExitCategory_or_any_type(EParser.Category_or_any_typeContext ctx)
        {
            IType exp = GetNodeValue<IType>(ctx.GetChild(0));
            SetNodeValue(ctx, exp);
        }

        public override void ExitCollection_literal(EParser.Collection_literalContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.GetChild(0));
            SetNodeValue(ctx, exp);
        }

        public override void ExitCursorType(EParser.CursorTypeContext context)
        {
            throw new NotImplementedException();
        }

        public override void ExitEnum_declaration(EParser.Enum_declarationContext ctx)
        {
            IDeclaration exp = GetNodeValue<IDeclaration>(ctx.GetChild(0));
            SetNodeValue(ctx, exp);
        }

        public override void ExitSymbol_list(EParser.Symbol_listContext context)
        {
            throw new NotImplementedException();
        }

        public override void ExitJsxChild(EParser.JsxChildContext ctx)
        {
            SetNodeValue(ctx, GetNodeValue<Object>(ctx.jsx));
        }


        public override void ExitJsxCode(EParser.JsxCodeContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, new JsxCode(exp, null));
        }


        public override void ExitJsxExpression(EParser.JsxExpressionContext ctx)
        {
            SetNodeValue(ctx, GetNodeValue<Object>(ctx.exp));
        }


        public override void ExitJsxElement(EParser.JsxElementContext ctx)
        {
            JsxElement element = GetNodeValue<JsxElement>(ctx.opening);
            JsxClosing closing = GetNodeValue<JsxClosing>(ctx.closing);
            element.setClosing(closing);
            List<IJsxExpression> children = GetNodeValue<List<IJsxExpression>>(ctx.children_);
            element.setChildren(children);
            SetNodeValue(ctx, element);
        }


        public override void ExitJsxSelfClosing(EParser.JsxSelfClosingContext ctx)
        {
            SetNodeValue(ctx, GetNodeValue<Object>(ctx.jsx));
        }


        public override void ExitJsxText(EParser.JsxTextContext ctx)
        {
            String text = ParserUtils.GetFullText(ctx.text);
            SetNodeValue(ctx, new JsxText(text));
        }


        public override void ExitJsxValue(EParser.JsxValueContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, new JsxExpression(exp));
        }

        public override void ExitJsx_attribute(EParser.Jsx_attributeContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            IJsxValue value = GetNodeValue<IJsxValue>(ctx.value);
            String suite = getWhiteSpacePlus(ctx.ws_plus());
            SetNodeValue(ctx, new JsxProperty(name, value, suite));
        }


        public override void ExitJsx_children(EParser.Jsx_childrenContext ctx)
        {
            List<IJsxExpression> list = new List<IJsxExpression>();
            foreach (ParserRuleContext child in ctx.jsx_child())
                list.Add(GetNodeValue<IJsxExpression>(child));
            SetNodeValue(ctx, list);
        }

        public override void ExitJsx_element_name(EParser.Jsx_element_nameContext ctx)
        {
            String name = ctx.GetText();
            SetNodeValue(ctx, name);
        }

        public override void ExitJsx_expression(EParser.Jsx_expressionContext ctx)
        {
            SetNodeValue(ctx, GetNodeValue<Object>(ctx.GetChild(0)));
        }


        public override void ExitJsx_fragment(EParser.Jsx_fragmentContext ctx)
        {
            String suite = getWhiteSpacePlus(ctx.ws_plus(0));
            JsxFragment fragment = new JsxFragment(suite);
            List<IJsxExpression> children = GetNodeValue<List<IJsxExpression>>(ctx.children_);
            fragment.setChildren(children);
            SetNodeValue(ctx, fragment);
        }


        public override void ExitJsx_identifier(EParser.Jsx_identifierContext ctx)
        {
            String name = ctx.GetText();
            SetNodeValue(ctx, name);
        }

        public override void ExitJsxLiteral(EParser.JsxLiteralContext ctx)
        {
            String text = ctx.GetText();
            SetNodeValue(ctx, new JsxLiteral(text));
        }

        public override void ExitJsx_opening(EParser.Jsx_openingContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            String suite = getWhiteSpacePlus(ctx.ws_plus());
            List<JsxProperty> attributes = new List<JsxProperty>();
            foreach (ParserRuleContext child in ctx.jsx_attribute())
                attributes.Add(GetNodeValue<JsxProperty>(child));
            SetNodeValue(ctx, new JsxElement(name, suite, attributes, null));
        }

        public override void ExitJsx_closing(EParser.Jsx_closingContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            SetNodeValue(ctx, new JsxClosing(name, null));
        }

        public override void ExitJsx_self_closing(EParser.Jsx_self_closingContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            String suite = getWhiteSpacePlus(ctx.ws_plus());
            List<JsxProperty> attributes = new List<JsxProperty>();
            foreach (ParserRuleContext child in ctx.jsx_attribute())
                attributes.Add(GetNodeValue<JsxProperty>(child));
            SetNodeValue(ctx, new JsxSelfClosing(name, suite, attributes, null));
        }

        public override void ExitCssExpression(EParser.CssExpressionContext ctx)
        {
            SetNodeValue(ctx, GetNodeValue<Object>(ctx.exp));
        }


        public override void ExitCss_expression(EParser.Css_expressionContext ctx)
        {
            CssExpression exp = new CssExpression();
            foreach (ParserRuleContext child in ctx.css_field())
            {
                exp.AddField(GetNodeValue<CssField>(child));
            }
            SetNodeValue(ctx, exp);
        }


        public override void ExitCss_field(EParser.Css_fieldContext ctx)
        {
            String name = ctx.name.GetText();
            ICssValue value = GetNodeValue<ICssValue>(ctx.value);
            SetNodeValue(ctx, new CssField(name, value));
        }



        public override void ExitCssText(EParser.CssTextContext ctx)
        {
            String text = input.GetText(ctx.text.Start, ctx.text.Stop);
            SetNodeValue(ctx, new CssText(text));
        }


        public override void ExitCssValue(EParser.CssValueContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, new CssCode(exp));
        }



    }
}
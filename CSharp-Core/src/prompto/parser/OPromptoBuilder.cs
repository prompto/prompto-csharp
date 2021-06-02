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
using prompto.param;
using prompto.constraint;
using prompto.instance;
using prompto.jsx;
using prompto.css;
using System.Text;

namespace prompto.parser
{

    public class OPromptoBuilder : OParserBaseListener
    {

        ParseTreeProperty<object> nodeValues = new ParseTreeProperty<object>();
        BufferedTokenStream input;
        string path = "";

        public OPromptoBuilder(OCleverParser parser)
        {
            this.input = (BufferedTokenStream)parser.InputStream;
            this.path = parser.Path;
        }

        protected String getHiddenTokensAfter(ITerminalNode node)
        {
            return getHiddenTokensAfter(node.Symbol);
        }

        protected String getHiddenTokensAfter(IToken token)
        {
            IList<IToken> hidden = input.GetHiddenTokensToRight(token.TokenIndex);
            if (hidden == null || hidden.Count == 0)
                return null;
            else
            {
                StringBuilder sb = new StringBuilder();
                foreach (IToken t in hidden)
                    sb.Append(t.Text);
                return sb.ToString();
            }
        }

        protected String getHiddenTokensBefore(ITerminalNode node)
        {
            return getHiddenTokensBefore(node.Symbol);
        }

        protected String getHiddenTokensBefore(IToken token)
        {
            IList<IToken> hidden = input.GetHiddenTokensToLeft(token.TokenIndex);
            if (hidden == null || hidden.Count == 0)
                return null;
            else
            {
                StringBuilder sb = new StringBuilder();
                foreach (IToken t in hidden)
                    sb.Append(t.Text);
                return sb.ToString();
            }
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
            section.SetFrom(path, first, last, Dialect.O);
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


        List<Annotation> ReadAnnotations(OParser.Annotation_constructorContext[] contexts)
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

        List<CommentStatement> ReadComments(OParser.Comment_statementContext[] contexts)
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


        public override void ExitFlush_statement(OParser.Flush_statementContext ctx)
        {
            SetNodeValue(ctx, new FlushStatement());
        }


        public override void ExitFlushStatement(OParser.FlushStatementContext ctx)
        {
            SetNodeValue(ctx, GetNodeValue<IStatement>(ctx.stmt));
        }


        public override void ExitFullDeclarationList(OParser.FullDeclarationListContext ctx)
        {
            DeclarationList items = GetNodeValue<DeclarationList>(ctx.declarations());
            if (items == null)
                items = new DeclarationList();
            SetNodeValue(ctx, items);
        }

        public override void ExitSelectorExpression(OParser.SelectorExpressionContext ctx)
        {
            IExpression parent = GetNodeValue<IExpression>(ctx.parent);
            IExpression selector = GetNodeValue<IExpression>(ctx.selector);
            if (selector is SelectorExpression)
                ((SelectorExpression)selector).setParent(parent);
            else if (selector is UnresolvedCall)
                ((UnresolvedCall)selector).setParent(parent);
            SetNodeValue(ctx, selector);
        }

        public override void ExitSelectableExpression(OParser.SelectableExpressionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.parent);
            SetNodeValue(ctx, exp);
        }

        public override void ExitSet_literal(OParser.Set_literalContext ctx)
        {
            ExpressionList items = GetNodeValue<ExpressionList>(ctx.expression_list());
            SetLiteral set = items == null ? new SetLiteral() : new SetLiteral(items);
            SetNodeValue(ctx, set);
        }

        public override void ExitBlob_expression(OParser.Blob_expressionContext ctx)
        {
            IExpression source = GetNodeValue<IExpression>(ctx.expression());
            SetNodeValue(ctx, new BlobExpression(source));
        }

        public override void ExitBlobType(OParser.BlobTypeContext ctx)
        {
            SetNodeValue(ctx, BlobType.Instance);
        }

        public override void ExitBooleanLiteral(OParser.BooleanLiteralContext ctx)
        {
            SetNodeValue(ctx, new BooleanLiteral(ctx.GetText()));
        }

        public override void ExitBreakStatement(OParser.BreakStatementContext ctx)
        {
            SetNodeValue(ctx, new BreakStatement());
        }


        public override void ExitMinIntegerLiteral(OParser.MinIntegerLiteralContext ctx)
        {
            SetNodeValue(ctx, new MinIntegerLiteral());
        }

        public override void ExitMaxIntegerLiteral(OParser.MaxIntegerLiteralContext ctx)
        {
            SetNodeValue(ctx, new MaxIntegerLiteral());
        }

        public override void ExitIntegerLiteral(OParser.IntegerLiteralContext ctx)
        {
            SetNodeValue(ctx, new IntegerLiteral(ctx.GetText()));
        }

        public override void ExitDecimalLiteral(OParser.DecimalLiteralContext ctx)
        {
            SetNodeValue(ctx, new DecimalLiteral(ctx.GetText()));
        }

        public override void ExitHexadecimalLiteral(OParser.HexadecimalLiteralContext ctx)
        {
            SetNodeValue(ctx, new HexaLiteral(ctx.GetText())); ;
        }

        public override void ExitCharacterLiteral(OParser.CharacterLiteralContext ctx)
        {
            SetNodeValue(ctx, new CharacterLiteral(ctx.GetText()));
        }

        public override void ExitDateLiteral(OParser.DateLiteralContext ctx)
        {
            SetNodeValue(ctx, new DateLiteral(ctx.GetText()));
        }

        public override void ExitDateTimeLiteral(OParser.DateTimeLiteralContext ctx)
        {
            SetNodeValue(ctx, new DateTimeLiteral(ctx.GetText()));
        }

        public override void ExitTernaryExpression(OParser.TernaryExpressionContext ctx)
        {
            IExpression condition = GetNodeValue<IExpression>(ctx.test);
            IExpression ifTrue = GetNodeValue<IExpression>(ctx.ifTrue);
            IExpression ifFalse = GetNodeValue<IExpression>(ctx.ifFalse);
            TernaryExpression exp = new TernaryExpression(condition, ifTrue, ifFalse);
            SetNodeValue(ctx, exp);
        }

        public override void ExitTest_method_declaration(OParser.Test_method_declarationContext ctx)
        {
            String name = ctx.name.Text;
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            ExpressionList exps = GetNodeValue<ExpressionList>(ctx.exps);
            String errorName = GetNodeValue<String>(ctx.error);
            SymbolExpression error = errorName == null ? null : new SymbolExpression(errorName);
            SetNodeValue(ctx, new TestMethodDeclaration(name, stmts, exps, error));
        }

        public override void ExitTextLiteral(OParser.TextLiteralContext ctx)
        {
            SetNodeValue(ctx, new TextLiteral(ctx.GetText()));
        }

        public override void ExitTimeLiteral(OParser.TimeLiteralContext ctx)
        {
            SetNodeValue(ctx, new TimeLiteral(ctx.GetText())); ;
        }

        public override void ExitPeriodLiteral(OParser.PeriodLiteralContext ctx)
        {
            SetNodeValue(ctx, new PeriodLiteral(ctx.GetText()));
        }

        public override void ExitPeriodType(OParser.PeriodTypeContext ctx)
        {
            SetNodeValue(ctx, PeriodType.Instance);
        }


        public override void ExitVersionLiteral(OParser.VersionLiteralContext ctx)
        {
            SetNodeValue(ctx, new VersionLiteral(ctx.GetText()));
        }


        public override void ExitVersionType(OParser.VersionTypeContext ctx)
        {
            SetNodeValue(ctx, VersionType.Instance);
        }



        public override void ExitAttribute_identifier(OParser.Attribute_identifierContext ctx)
        {
            SetNodeValue(ctx, ctx.GetText());
        }


        public override void ExitUUIDLiteral(OParser.UUIDLiteralContext ctx)
        {
            SetNodeValue(ctx, new UUIDLiteral(ctx.GetText()));
        }


        public override void ExitUUIDType(OParser.UUIDTypeContext ctx)
        {
            SetNodeValue(ctx, UUIDType.Instance);
        }



        public override void ExitVariable_identifier(OParser.Variable_identifierContext ctx)
        {
            SetNodeValue(ctx, ctx.GetText());
        }

        public override void ExitList_literal(OParser.List_literalContext ctx)
        {
            bool mutable = ctx.MUTABLE() != null;
            ExpressionList items = GetNodeValue<ExpressionList>(ctx.expression_list());
            IExpression value = items == null ? new ListLiteral(mutable) : new ListLiteral(items, mutable);
            SetNodeValue(ctx, value);
        }

        public override void ExitDict_literal(OParser.Dict_literalContext ctx)
        {
            bool mutable = ctx.MUTABLE() != null;
            DictEntryList items = GetNodeValue<DictEntryList>(ctx.dict_entry_list());
            IExpression value = items == null ? new DictLiteral(mutable) : new DictLiteral(items, mutable);
            SetNodeValue(ctx, value);
        }

        public override void ExitTuple_literal(OParser.Tuple_literalContext ctx)
        {
            bool mutable = ctx.MUTABLE() != null;
            ExpressionList items = GetNodeValue<ExpressionList>(ctx.expression_tuple());
            IExpression value = items == null ? new TupleLiteral(mutable) : new TupleLiteral(items, mutable);
            SetNodeValue(ctx, value);
        }

        public override void ExitRange_literal(OParser.Range_literalContext ctx)
        {
            IExpression low = GetNodeValue<IExpression>(ctx.low);
            IExpression high = GetNodeValue<IExpression>(ctx.high);
            SetNodeValue(ctx, new RangeLiteral(low, high));
        }

        public override void ExitDict_entry_list(OParser.Dict_entry_listContext ctx)
        {
            DictEntryList items = new DictEntryList();
            foreach (ParserRuleContext entry in ctx.dict_entry())
            {
                DictEntry item = GetNodeValue<DictEntry>(entry);
                items.add(item);
            }
            SetNodeValue(ctx, items);
        }


        public override void ExitDict_entry(OParser.Dict_entryContext ctx)
        {
            DictKey key = GetNodeValue<DictKey>(ctx.key);
            IExpression value = GetNodeValue<IExpression>(ctx.value);
            DictEntry entry = new DictEntry(key, value);
            SetNodeValue(ctx, entry);
        }


        public override void ExitDoc_entry_list(OParser.Doc_entry_listContext ctx)
        {
            DocEntryList items = new DocEntryList();
            foreach (ParserRuleContext entry in ctx.doc_entry())
            {
                DocEntry item = GetNodeValue<DocEntry>(entry);
                items.add(item);
            }
            SetNodeValue(ctx, items);
        }

        public override void ExitDoc_entry(OParser.Doc_entryContext ctx)
        {
            DocKey key = GetNodeValue<DocKey>(ctx.key);
            IExpression value = GetNodeValue<IExpression>(ctx.value);
            DocEntry entry = new DocEntry(key, value);
            SetNodeValue(ctx, entry);
        }


        public override void ExitDocKeyIdentifier(OParser.DocKeyIdentifierContext ctx)
        {
            String text = ctx.name.GetText();
            SetNodeValue(ctx, new DocIdentifierKey(text));
        }


        public override void ExitDocKeyText(OParser.DocKeyTextContext ctx)
        {
            String text = ctx.name.Text;
            SetNodeValue(ctx, new DocTextKey(text));
        }



        public override void ExitLiteralExpression(OParser.LiteralExpressionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }

        public override void ExitIdentifierExpression(OParser.IdentifierExpressionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }

        public override void ExitVariableIdentifier(OParser.VariableIdentifierContext ctx)
        {
            String name = GetNodeValue<String>(ctx.variable_identifier());
            SetNodeValue(ctx, new InstanceExpression(name));
        }

        public override void ExitInstanceExpression(OParser.InstanceExpressionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }

        public override void ExitExpression_list(OParser.Expression_listContext ctx)
        {
            ExpressionList items = new ExpressionList();
            foreach (ParserRuleContext rule in ctx.expression())
            {
                IExpression item = GetNodeValue<IExpression>(rule);
                items.add(item);
            }
            SetNodeValue(ctx, items);
        }


        public override void ExitExpression_tuple(OParser.Expression_tupleContext ctx)
        {
            ExpressionList items = new ExpressionList();
            foreach (ParserRuleContext rule in ctx.expression())
            {
                IExpression item = GetNodeValue<IExpression>(rule);
                items.add(item);
            }
            SetNodeValue(ctx, items);
        }

        public override void ExitSymbol_identifier(OParser.Symbol_identifierContext ctx)
        {
            SetNodeValue(ctx, ctx.GetText());
        }

        public override void ExitNative_symbol(OParser.Native_symbolContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, new NativeSymbol(name, exp));
        }

        public override void ExitNative_member_method_declaration(OParser.Native_member_method_declarationContext ctx)
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


        public override void ExitTypeIdentifier(OParser.TypeIdentifierContext ctx)
        {
            String name = GetNodeValue<String>(ctx.type_identifier());
            SetNodeValue(ctx, new UnresolvedIdentifier(name, Dialect.O));
        }


        public override void ExitTypeLiteral(OParser.TypeLiteralContext ctx)
        {
            TypeLiteral type = GetNodeValue<TypeLiteral>(ctx.type_literal());
            SetNodeValue(ctx, type);
        }


        public override void ExitType_literal(OParser.Type_literalContext ctx)
        {
            IType type = GetNodeValue<IType>(ctx.category_or_any_type());
            SetNodeValue(ctx, new TypeLiteral(type));
        }



        public override void ExitSymbolIdentifier(OParser.SymbolIdentifierContext ctx)
        {
            String name = GetNodeValue<String>(ctx.symbol_identifier());
            SetNodeValue(ctx, new SymbolExpression(name));
        }


        public override void ExitSymbolLiteral(OParser.SymbolLiteralContext ctx)
        {
            String name = ctx.GetText();
            SetNodeValue(ctx, new SymbolExpression(name));
        }


        public override void ExitBooleanType(OParser.BooleanTypeContext ctx)
        {
            SetNodeValue(ctx, BooleanType.Instance);
        }

        public override void ExitCharacterType(OParser.CharacterTypeContext ctx)
        {
            SetNodeValue(ctx, CharacterType.Instance);
        }

        public override void ExitTextType(OParser.TextTypeContext ctx)
        {
            SetNodeValue(ctx, TextType.Instance);
        }

        public override void ExitHtmlType(OParser.HtmlTypeContext ctx)
        {
            SetNodeValue(ctx, HtmlType.Instance);
        }

        public override void ExitThisExpression(OParser.ThisExpressionContext ctx)
        {
            SetNodeValue(ctx, new ThisExpression());
        }

        public override void ExitIntegerType(OParser.IntegerTypeContext ctx)
        {
            SetNodeValue(ctx, IntegerType.Instance);
        }

        public override void ExitDecimalType(OParser.DecimalTypeContext ctx)
        {
            SetNodeValue(ctx, DecimalType.Instance);
        }

        public override void ExitDateType(OParser.DateTypeContext ctx)
        {
            SetNodeValue(ctx, DateType.Instance);
        }

        public override void ExitDateTimeType(OParser.DateTimeTypeContext ctx)
        {
            SetNodeValue(ctx, DateTimeType.Instance);
        }

        public override void ExitTimeType(OParser.TimeTypeContext ctx)
        {
            SetNodeValue(ctx, TimeType.Instance);
        }

        public override void ExitCodeType(OParser.CodeTypeContext ctx)
        {
            SetNodeValue(ctx, CodeType.Instance);
        }

        public override void ExitPrimaryType(OParser.PrimaryTypeContext ctx)
        {
            IType type = GetNodeValue<IType>(ctx.p);
            SetNodeValue(ctx, type);
        }

        public override void ExitAttribute_declaration(OParser.Attribute_declarationContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            IType type = GetNodeValue<IType>(ctx.typ);
            IAttributeConstraint match = GetNodeValue<IAttributeConstraint>(ctx.match);
            IdentifierList indices = ctx.INDEX() != null ? new IdentifierList() : null;
            if (ctx.indices != null)
                indices.AddRange(GetNodeValue<IdentifierList>(ctx.indices));
            AttributeDeclaration decl = new AttributeDeclaration(name, type, match, indices);
            decl.Storable = ctx.STORABLE() != null;
            SetNodeValue(ctx, decl);
        }

        public override void ExitNativeType(OParser.NativeTypeContext ctx)
        {
            IType type = GetNodeValue<IType>(ctx.n);
            SetNodeValue(ctx, type);
        }

        public override void ExitCategoryType(OParser.CategoryTypeContext ctx)
        {
            IType type = GetNodeValue<IType>(ctx.c);
            SetNodeValue(ctx, type);
        }

        public override void ExitCategory_type(OParser.Category_typeContext ctx)
        {
            String name = ctx.GetText();
            SetNodeValue(ctx, new CategoryType(name));
        }

        public override void ExitListType(OParser.ListTypeContext ctx)
        {
            IType type = GetNodeValue<IType>(ctx.l);
            SetNodeValue(ctx, new ListType(type));
        }


        public override void ExitDictKeyIdentifier(OParser.DictKeyIdentifierContext ctx)
        {
            String text = ctx.name.GetText();
            SetNodeValue(ctx, new DictIdentifierKey(text));
        }


        public override void ExitDictKeyText(OParser.DictKeyTextContext ctx)
        {
            String text = ctx.name.Text;
            SetNodeValue(ctx, new DictTextKey(text));
        }


        public override void ExitDictType(OParser.DictTypeContext ctx)
        {
            IType type = GetNodeValue<IType>(ctx.d);
            SetNodeValue(ctx, new DictType(type));
        }

        public override void ExitAttribute_identifier_list(OParser.Attribute_identifier_listContext ctx)
        {
            IdentifierList list = new IdentifierList();
            foreach (OParser.Attribute_identifierContext c in ctx.attribute_identifier())
            {
                String item = GetNodeValue<String>(c);
                list.Add(item);
            }
            SetNodeValue(ctx, list);
        }

        public override void ExitVariable_identifier_list(OParser.Variable_identifier_listContext ctx)
        {
            IdentifierList list = new IdentifierList();
            foreach (OParser.Variable_identifierContext c in ctx.variable_identifier())
            {
                String item = GetNodeValue<String>(c);
                list.Add(item);
            }
            SetNodeValue(ctx, list);
        }

        public override void ExitConcrete_category_declaration(OParser.Concrete_category_declarationContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            IdentifierList attrs = GetNodeValue<IdentifierList>(ctx.attrs);
            IdentifierList derived = GetNodeValue<IdentifierList>(ctx.derived);
            MethodDeclarationList methods = GetNodeValue<MethodDeclarationList>(ctx.methods);
            ConcreteCategoryDeclaration decl = new ConcreteCategoryDeclaration(name, attrs, derived, methods);
            decl.Storable = ctx.STORABLE() != null;
            SetNodeValue(ctx, decl);
        }


        public override void ExitConcrete_widget_declaration(OParser.Concrete_widget_declarationContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            String derived = GetNodeValue<String>(ctx.derived);
            MethodDeclarationList methods = this.GetNodeValue<MethodDeclarationList>(ctx.methods);
            ConcreteWidgetDeclaration decl = new ConcreteWidgetDeclaration(name, derived, methods);
            SetNodeValue(ctx, decl);
        }

        public override void ExitConcreteCategoryDeclaration(OParser.ConcreteCategoryDeclarationContext ctx)
        {
            SetNodeValue(ctx, GetNodeValue<Object>(ctx.decl));
        }


        public override void ExitConcreteWidgetDeclaration(OParser.ConcreteWidgetDeclarationContext ctx)
        {
            SetNodeValue(ctx, GetNodeValue<Object>(ctx.decl));
        }


        public override void ExitNativeWidgetDeclaration(OParser.NativeWidgetDeclarationContext ctx)
        {
            SetNodeValue(ctx, GetNodeValue<Object>(ctx.decl));
        }


        public override void ExitDerivedList(OParser.DerivedListContext ctx)
        {
            String item = GetNodeValue<String>(ctx.item);
            IdentifierList items = new IdentifierList(item);
            SetNodeValue(ctx, items);
        }

        public override void ExitDerivedListItem(OParser.DerivedListItemContext ctx)
        {
            IdentifierList items = GetNodeValue<IdentifierList>(ctx.items);
            String item = GetNodeValue<String>(ctx.item);
            items.Add(item);
            SetNodeValue(ctx, items);
        }

        public override void ExitType_identifier(OParser.Type_identifierContext ctx)
        {
            SetNodeValue(ctx, ctx.GetText());
        }

        public override void ExitType_identifier_list(OParser.Type_identifier_listContext ctx)
        {
            IdentifierList items = new IdentifierList();
            foreach (ParserRuleContext rule in ctx.type_identifier())
            {
                String item = GetNodeValue<String>(rule);
                items.Add(item);
            }
            SetNodeValue(ctx, items);
        }


        public override void ExitMember_identifier(OParser.Member_identifierContext ctx)
        {
            String name = ctx.GetText();
            SetNodeValue(ctx, name);
        }


        public override void ExitMemberSelector(OParser.MemberSelectorContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            SetNodeValue(ctx, new MemberSelector(name));
        }

        public override void ExitIsAnExpression(OParser.IsAnExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IType type = GetNodeValue<IType>(ctx.right);
            IExpression right = new TypeExpression(type);
            EqOp eqOp = ctx.NOT() == null ? EqOp.IS_A : EqOp.IS_NOT_A;
            SetNodeValue(ctx, new EqualsExpression(left, eqOp, right));
        }

        public override void ExitIsExpression(OParser.IsExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            EqOp eqOp = ctx.NOT() == null ? EqOp.IS : EqOp.IS_NOT;
            SetNodeValue(ctx, new EqualsExpression(left, eqOp, right));
        }

        public override void ExitItemSelector(OParser.ItemSelectorContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, new ItemSelector(exp));
        }

        public override void ExitSliceSelector(OParser.SliceSelectorContext ctx)
        {
            IExpression slice = GetNodeValue<IExpression>(ctx.xslice);
            SetNodeValue(ctx, slice);
        }

        public override void ExitTyped_argument(OParser.Typed_argumentContext ctx)
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


        public override void ExitCodeArgument(OParser.CodeArgumentContext ctx)
        {
            IParameter arg = GetNodeValue<IParameter>(ctx.arg);
            SetNodeValue(ctx, arg);
        }


        public override void ExitArgument_list(OParser.Argument_listContext ctx)
        {
            ParameterList items = new ParameterList();
            foreach (ParserRuleContext rule in ctx.argument())
            {
                IParameter item = GetNodeValue<IParameter>(rule);
                items.Add(item);
            }
            SetNodeValue(ctx, items);
        }


        public override void ExitMethod_call_expression(OParser.Method_call_expressionContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            IExpression caller = new UnresolvedIdentifier(name, Dialect.O);
            ArgumentList args = GetNodeValue<ArgumentList>(ctx.args);
            SetNodeValue(ctx, new UnresolvedCall(caller, args));
        }


        public override void ExitMethod_call_statement(OParser.Method_call_statementContext ctx)
        {
            IExpression parent = GetNodeValue<IExpression>(ctx.parent);
            UnresolvedCall call = GetNodeValue<UnresolvedCall>(ctx.method);
            call.setParent(parent);
            String resultName = GetNodeValue<String>(ctx.name);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            if (resultName != null || stmts != null)
                SetNodeValue(ctx, new RemoteCall(call, resultName, stmts));
            else
                SetNodeValue(ctx, call);
        }


        public override void ExitMethodSelector(OParser.MethodSelectorContext ctx)
        {
            UnresolvedCall call = this.GetNodeValue<UnresolvedCall>(ctx.method);
            if (call.getCaller() is UnresolvedIdentifier)
            {
                String name = ((UnresolvedIdentifier)call.getCaller()).getName();
                call.setCaller(new UnresolvedSelector(name));
            }
            SetNodeValue(ctx, call);
        }


        public override void ExitExpressionAssignmentList(OParser.ExpressionAssignmentListContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            Argument item = new Argument(null, exp);
            ArgumentList items = new ArgumentList();
            items.Add(item);
            SetNodeValue(ctx, items);
        }


        public override void ExitArgument_assignment(OParser.Argument_assignmentContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            IParameter arg = new UnresolvedParameter(name);
            SetNodeValue(ctx, new Argument(arg, exp));
        }


        public override void ExitArgumentAssignmentList(OParser.ArgumentAssignmentListContext ctx)
        {
            Argument item = GetNodeValue<Argument>(ctx.item);
            ArgumentList items = new ArgumentList();
            items.Add(item);
            SetNodeValue(ctx, items);
        }


        public override void ExitArgumentAssignmentListItem(OParser.ArgumentAssignmentListItemContext ctx)
        {
            Argument item = GetNodeValue<Argument>(ctx.item);
            ArgumentList items = GetNodeValue<ArgumentList>(ctx.items);
            items.add(item);
            SetNodeValue(ctx, items);
        }

        public override void ExitArrow_prefix(OParser.Arrow_prefixContext ctx)
        {
            IdentifierList args = GetNodeValue<IdentifierList>(ctx.arrow_args());
            String argsSuite = getHiddenTokensBefore(ctx.EGT());
            String arrowSuite = getHiddenTokensAfter(ctx.EGT());
            SetNodeValue(ctx, new ArrowExpression(args, argsSuite, arrowSuite));
        }

        public override void ExitArrowExpression(OParser.ArrowExpressionContext ctx)
        {
            SetNodeValue(ctx, GetNodeValue<Object>(ctx.exp));
        }

        public override void ExitArrowExpressionBody(OParser.ArrowExpressionBodyContext ctx)
        {
            ArrowExpression arrow = GetNodeValue<ArrowExpression>(ctx.arrow_prefix());
            IExpression exp = GetNodeValue<IExpression>(ctx.expression());
            arrow.Expression = exp;
            SetNodeValue(ctx, arrow);
        }

        public override void ExitArrowListArg(OParser.ArrowListArgContext ctx)
        {
            IdentifierList list = GetNodeValue<IdentifierList>(ctx.variable_identifier_list());
            SetNodeValue(ctx, list);
        }

        public override void ExitArrowSingleArg(OParser.ArrowSingleArgContext ctx)
        {
            String arg = GetNodeValue<String>(ctx.variable_identifier());
            SetNodeValue(ctx, new IdentifierList(arg));
        }


        public override void ExitArrowStatementsBody(OParser.ArrowStatementsBodyContext ctx)
        {
            ArrowExpression arrow = GetNodeValue<ArrowExpression>(ctx.arrow_prefix());
            StatementList stmts = GetNodeValue<StatementList>(ctx.statement_list());
            arrow.Statements = stmts;
            SetNodeValue(ctx, arrow);
        }

        public override void ExitAddExpression(OParser.AddExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            IExpression exp = ctx.op.Type == OParser.PLUS ?
                    (IExpression)new PlusExpression(left, right)
                    : (IExpression)new SubtractExpression(left, right);
            SetNodeValue(ctx, exp);
        }

        public override void ExitMember_method_declaration_list(OParser.Member_method_declaration_listContext ctx)
        {
            MethodDeclarationList items = new MethodDeclarationList();
            foreach (ParserRuleContext rule in ctx.member_method_declaration())
            {
                IMethodDeclaration item = GetNodeValue<IMethodDeclaration>(rule);
                items.add(item);
            }
            SetNodeValue(ctx, items);
        }

        public override void ExitNative_member_method_declaration_list(OParser.Native_member_method_declaration_listContext ctx)
        {
            MethodDeclarationList items = new MethodDeclarationList();
            foreach (ParserRuleContext rule in ctx.native_member_method_declaration())
            {
                IMethodDeclaration item = GetNodeValue<IMethodDeclaration>(rule);
                items.add(item);
            }
            SetNodeValue(ctx, items);
        }

        public override void ExitEmptyCategoryMethodList(OParser.EmptyCategoryMethodListContext ctx)
        {
            SetNodeValue(ctx, new MethodDeclarationList());
        }

        public override void ExitCurlyCategoryMethodList(OParser.CurlyCategoryMethodListContext ctx)
        {
            MethodDeclarationList items = GetNodeValue<MethodDeclarationList>(ctx.items);
            SetNodeValue(ctx, items);
        }

        public override void ExitSetter_method_declaration(OParser.Setter_method_declarationContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            SetNodeValue(ctx, new SetterMethodDeclaration(name, stmts));
        }

        public override void ExitGetter_method_declaration(OParser.Getter_method_declarationContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            SetNodeValue(ctx, new GetterMethodDeclaration(name, stmts));
        }

        public override void ExitNative_setter_declaration(OParser.Native_setter_declarationContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            SetNodeValue(ctx, new NativeSetterMethodDeclaration(name, stmts));
        }


        public override void ExitNative_getter_declaration(OParser.Native_getter_declarationContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            SetNodeValue(ctx, new NativeGetterMethodDeclaration(name, stmts));
        }



        public override void ExitMember_method_declaration(OParser.Member_method_declarationContext ctx)
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

        public override void ExitSetType(OParser.SetTypeContext ctx)
        {
            IType itemType = GetNodeValue<IType>(ctx.s);
            SetNodeValue(ctx, new SetType(itemType));
        }


        public override void ExitSingleton_category_declaration(OParser.Singleton_category_declarationContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            IdentifierList attrs = GetNodeValue<IdentifierList>(ctx.attrs);
            MethodDeclarationList methods = GetNodeValue<MethodDeclarationList>(ctx.methods);
            SetNodeValue(ctx, new SingletonCategoryDeclaration(name, attrs, methods));
        }

        public override void ExitSingletonCategoryDeclaration(OParser.SingletonCategoryDeclarationContext ctx)
        {
            IDeclaration decl = GetNodeValue<IDeclaration>(ctx.decl);
            SetNodeValue(ctx, decl);
        }

        public override void ExitSingleStatement(OParser.SingleStatementContext ctx)
        {
            IStatement stmt = GetNodeValue<IStatement>(ctx.stmt);
            SetNodeValue(ctx, new StatementList(stmt));
        }

        public override void ExitCurlyStatementList(OParser.CurlyStatementListContext ctx)
        {
            StatementList items = GetNodeValue<StatementList>(ctx.items);
            SetNodeValue(ctx, items);
        }

        public override void ExitStatement_list(OParser.Statement_listContext ctx)
        {
            StatementList items = new StatementList();
            foreach (ParserRuleContext rule in ctx.statement())
            {
                IStatement item = GetNodeValue<IStatement>(rule);
                items.add(item);
            }
            SetNodeValue(ctx, items);
        }

        public override void ExitStoreStatement(OParser.StoreStatementContext ctx)
        {
            SetNodeValue(ctx, GetNodeValue<Object>(ctx.stmt));
        }

        public override void ExitStore_statement(OParser.Store_statementContext ctx)
        {
            ExpressionList del = GetNodeValue<ExpressionList>(ctx.to_del);
            ExpressionList add = GetNodeValue<ExpressionList>(ctx.to_add);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            StoreStatement stmt = new StoreStatement(del, add, stmts);
            SetNodeValue(ctx, stmt);
        }

        public override void ExitAbstract_method_declaration(OParser.Abstract_method_declarationContext ctx)
        {
            IType type = GetNodeValue<IType>(ctx.typ);
            if (type is CategoryType)
                ((CategoryType)type).Mutable = ctx.MUTABLE() != null;
            String name = GetNodeValue<String>(ctx.name);
            ParameterList args = GetNodeValue<ParameterList>(ctx.args);
            SetNodeValue(ctx, new AbstractMethodDeclaration(name, args, type));
        }

        public override void ExitConcrete_method_declaration(OParser.Concrete_method_declarationContext ctx)
        {
            IType type = GetNodeValue<IType>(ctx.typ);
            if (type is CategoryType)
                ((CategoryType)type).Mutable = ctx.MUTABLE() != null;
            String name = GetNodeValue<String>(ctx.name);
            ParameterList args = GetNodeValue<ParameterList>(ctx.args);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            SetNodeValue(ctx, new ConcreteMethodDeclaration(name, args, type, stmts));
        }


        public override void ExitMethod_expression(OParser.Method_expressionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.GetChild(0));
            SetNodeValue(ctx, exp);
        }


        public override void ExitMethodCallStatement(OParser.MethodCallStatementContext ctx)
        {
            IStatement stmt = GetNodeValue<IStatement>(ctx.stmt);
            SetNodeValue(ctx, stmt);
        }


        public override void ExitConstructorFrom(OParser.ConstructorFromContext ctx)
        {
            CategoryType type = GetNodeValue<CategoryType>(ctx.typ);
            IExpression copyFrom = GetNodeValue<IExpression>(ctx.copyExp);
            ArgumentList args = GetNodeValue<ArgumentList>(ctx.args);
            SetNodeValue(ctx, new ConstructorExpression(type, copyFrom, args));
        }

        public override void ExitConstructorNoFrom(OParser.ConstructorNoFromContext ctx)
        {
            CategoryType type = GetNodeValue<CategoryType>(ctx.typ);
            ArgumentList args = GetNodeValue<ArgumentList>(ctx.args);
            SetNodeValue(ctx, new ConstructorExpression(type, null, args));
        }


        public override void ExitCopy_from(OParser.Copy_fromContext ctx)
        {
            SetNodeValue(ctx, GetNodeValue<IExpression>(ctx.exp));
        }


        public override void ExitAn_expression(OParser.An_expressionContext ctx)
        {
            IType type = GetNodeValue<IType>(ctx.typ);
            SetNodeValue(ctx, type);
        }


        public override void ExitAssertion(OParser.AssertionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }


        public override void ExitAssertion_list(OParser.Assertion_listContext ctx)
        {
            ExpressionList items = new ExpressionList();
            foreach (ParserRuleContext rule in ctx.assertion())
            {
                IExpression item = GetNodeValue<IExpression>(rule);
                items.add(item);
            }
            SetNodeValue(ctx, items);
        }


        public override void ExitAssign_instance_statement(OParser.Assign_instance_statementContext ctx)
        {
            IAssignableInstance inst = GetNodeValue<IAssignableInstance>(ctx.inst);
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, new AssignInstanceStatement(inst, exp));
        }

        public override void ExitAssignInstanceStatement(OParser.AssignInstanceStatementContext ctx)
        {
            IStatement stmt = GetNodeValue<IStatement>(ctx.stmt);
            SetNodeValue(ctx, stmt);
        }

        public override void ExitAssign_variable_statement(OParser.Assign_variable_statementContext ctx)
        {
            String name = GetNodeValue<String>(ctx.variable_identifier());
            IExpression exp = GetNodeValue<IExpression>(ctx.expression());
            SetNodeValue(ctx, new AssignVariableStatement(name, exp));
        }

        public override void ExitAssign_tuple_statement(OParser.Assign_tuple_statementContext ctx)
        {
            IdentifierList items = GetNodeValue<IdentifierList>(ctx.items);
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, new AssignTupleStatement(items, exp));
        }

        public override void ExitRootInstance(OParser.RootInstanceContext ctx)
        {
            String name = GetNodeValue<String>(ctx.variable_identifier());
            SetNodeValue(ctx, new VariableInstance(name));
        }

        public override void ExitChildInstance(OParser.ChildInstanceContext ctx)
        {
            IAssignableInstance parent = GetNodeValue<IAssignableInstance>(ctx.assignable_instance());
            IAssignableSelector child = GetNodeValue<IAssignableSelector>(ctx.child_instance());
            child.SetParent(parent);
            SetNodeValue(ctx, child);
        }

        public override void ExitMemberInstance(OParser.MemberInstanceContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            SetNodeValue(ctx, new MemberInstance(name));
        }

        public override void ExitItemInstance(OParser.ItemInstanceContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, new ItemInstance(exp));
        }

        public override void ExitMethodExpression(OParser.MethodExpressionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }

        public override void ExitNative_statement_list(OParser.Native_statement_listContext ctx)
        {
            StatementList items = new StatementList();
            foreach (ParserRuleContext rule in ctx.native_statement())
            {
                IStatement item = GetNodeValue<IStatement>(rule);
                items.add(item);
            }
            SetNodeValue(ctx, items);
        }

        public override void ExitIteratorExpression(OParser.IteratorExpressionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            string name = GetNodeValue<string>(ctx.name);
            IExpression source = GetNodeValue<IExpression>(ctx.source);
            SetNodeValue(ctx, new IteratorExpression(name, source, exp));
        }

        public override void ExitIteratorType(OParser.IteratorTypeContext ctx)
        {
            IType type = GetNodeValue<IType>(ctx.i);
            SetNodeValue(ctx, new IteratorType(type));
        }


        public override void ExitJava_identifier(OParser.Java_identifierContext ctx)
        {
            SetNodeValue(ctx, ctx.GetText());
        }

        public override void ExitCsharp_identifier(OParser.Csharp_identifierContext ctx)
        {
            SetNodeValue(ctx, ctx.GetText());
        }

        public override void ExitCSharpPromptoIdentifier(OParser.CSharpPromptoIdentifierContext ctx)
        {
            String name = ctx.DOLLAR_IDENTIFIER().GetText();
            SetNodeValue(ctx, new CSharpIdentifierExpression(name));
        }

        public override void ExitCSharpPrimaryExpression(OParser.CSharpPrimaryExpressionContext ctx)
        {
            CSharpExpression exp = GetNodeValue<CSharpExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }

        public override void ExitPython_identifier(OParser.Python_identifierContext ctx)
        {
            SetNodeValue(ctx, ctx.GetText());
        }

        public override void ExitPythonPromptoIdentifier(OParser.PythonPromptoIdentifierContext ctx)
        {
            String name = ctx.DOLLAR_IDENTIFIER().GetText();
            SetNodeValue(ctx, new PythonIdentifierExpression(name));
        }


        public override void ExitPythonPrimaryExpression(OParser.PythonPrimaryExpressionContext ctx)
        {
            PythonExpression exp = GetNodeValue<PythonExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }

        public override void ExitJavaIdentifier(OParser.JavaIdentifierContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            SetNodeValue(ctx, new JavaIdentifierExpression(name));
        }

        public override void ExitJava_primary_expression(OParser.Java_primary_expressionContext ctx)
        {
            JavaExpression exp = GetNodeValue<JavaExpression>(ctx.GetChild(0));
            SetNodeValue(ctx, exp);
        }

        public override void ExitJavaPrimaryExpression(OParser.JavaPrimaryExpressionContext ctx)
        {
            JavaExpression exp = GetNodeValue<JavaExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }

        public override void ExitJava_this_expression(OParser.Java_this_expressionContext ctx)
        {
            SetNodeValue(ctx, new JavaThisExpression());
        }


        public override void ExitPythonSelfExpression(OParser.PythonSelfExpressionContext ctx)
        {
            SetNodeValue(ctx, new PythonSelfExpression());
        }


        public override void ExitCSharpIdentifier(OParser.CSharpIdentifierContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            SetNodeValue(ctx, new CSharpIdentifierExpression(name));
        }

        public override void ExitPythonIdentifier(OParser.PythonIdentifierContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            SetNodeValue(ctx, new PythonIdentifierExpression(name));
        }

        public override void ExitJavaChildIdentifier(OParser.JavaChildIdentifierContext ctx)
        {
            JavaIdentifierExpression parent = GetNodeValue<JavaIdentifierExpression>(ctx.parent);
            String name = GetNodeValue<String>(ctx.name);
            JavaIdentifierExpression child = new JavaIdentifierExpression(parent, name);
            SetNodeValue(ctx, child);
        }

        public override void ExitCSharpChildIdentifier(OParser.CSharpChildIdentifierContext ctx)
        {
            CSharpIdentifierExpression parent = GetNodeValue<CSharpIdentifierExpression>(ctx.parent);
            String name = GetNodeValue<String>(ctx.name);
            CSharpIdentifierExpression child = new CSharpIdentifierExpression(parent, name);
            SetNodeValue(ctx, child);
        }

        public override void ExitPythonCharacterLiteral(OParser.PythonCharacterLiteralContext ctx)
        {
            SetNodeValue(ctx, new PythonCharacterLiteral(ctx.t.Text));
        }

        public override void ExitPythonChildIdentifier(OParser.PythonChildIdentifierContext ctx)
        {
            PythonIdentifierExpression parent = GetNodeValue<PythonIdentifierExpression>(ctx.parent);
            String name = GetNodeValue<String>(ctx.name);
            PythonIdentifierExpression child = new PythonIdentifierExpression(parent, name);
            SetNodeValue(ctx, child);
        }


        public override void ExitJavaClassIdentifier(OParser.JavaClassIdentifierContext ctx)
        {
            JavaIdentifierExpression klass = GetNodeValue<JavaIdentifierExpression>(ctx.klass);
            SetNodeValue(ctx, klass);
        }

        public override void ExitJavaChildClassIdentifier(OParser.JavaChildClassIdentifierContext ctx)
        {
            JavaIdentifierExpression parent = GetNodeValue<JavaIdentifierExpression>(ctx.parent);
            JavaIdentifierExpression child = new JavaIdentifierExpression(parent, ctx.name.Text);
            SetNodeValue(ctx, child);
        }

        public override void ExitJavaSelectorExpression(OParser.JavaSelectorExpressionContext ctx)
        {
            JavaExpression parent = GetNodeValue<JavaExpression>(ctx.parent);
            JavaSelectorExpression child = GetNodeValue<JavaSelectorExpression>(ctx.child);
            child.SetParent(parent);
            SetNodeValue(ctx, child);
        }

        public override void ExitJavaStatement(OParser.JavaStatementContext ctx)
        {
            JavaExpression exp = GetNodeValue<JavaExpression>(ctx.exp);
            SetNodeValue(ctx, new JavaStatement(exp, false));
        }

        public override void ExitCSharpStatement(OParser.CSharpStatementContext ctx)
        {
            CSharpExpression exp = GetNodeValue<CSharpExpression>(ctx.exp);
            SetNodeValue(ctx, new CSharpStatement(exp, false));
        }

        public override void ExitPythonStatement(OParser.PythonStatementContext ctx)
        {
            PythonExpression exp = GetNodeValue<PythonExpression>(ctx.exp);
            SetNodeValue(ctx, new PythonStatement(exp, false));
        }

        public override void ExitJavaReturnStatement(OParser.JavaReturnStatementContext ctx)
        {
            JavaExpression exp = GetNodeValue<JavaExpression>(ctx.exp);
            SetNodeValue(ctx, new JavaStatement(exp, true));
        }

        public override void ExitCSharpReturnStatement(OParser.CSharpReturnStatementContext ctx)
        {
            CSharpExpression exp = GetNodeValue<CSharpExpression>(ctx.exp);
            SetNodeValue(ctx, new CSharpStatement(exp, true));
        }

        public override void ExitPythonReturnStatement(OParser.PythonReturnStatementContext ctx)
        {
            PythonExpression exp = GetNodeValue<PythonExpression>(ctx.exp);
            SetNodeValue(ctx, new PythonStatement(exp, true));
        }

        public override void ExitJavaNativeStatement(OParser.JavaNativeStatementContext ctx)
        {
            JavaStatement stmt = GetNodeValue<JavaStatement>(ctx.java_statement());
            SetNodeValue(ctx, new JavaNativeCall(stmt));
        }

        public override void ExitCSharpNativeStatement(OParser.CSharpNativeStatementContext ctx)
        {
            CSharpStatement stmt = GetNodeValue<CSharpStatement>(ctx.csharp_statement());
            SetNodeValue(ctx, new CSharpNativeCall(stmt));
        }

        public override void ExitPython2NativeStatement(OParser.Python2NativeStatementContext ctx)
        {
            PythonStatement stmt = GetNodeValue<PythonStatement>(ctx.python_native_statement());
            SetNodeValue(ctx, new Python2NativeCall(stmt));
        }

        public override void ExitPython3NativeStatement(OParser.Python3NativeStatementContext ctx)
        {
            PythonStatement stmt = GetNodeValue<PythonStatement>(ctx.python_native_statement());
            SetNodeValue(ctx, new Python3NativeCall(stmt));
        }

        public override void ExitNative_method_declaration(OParser.Native_method_declarationContext ctx)
        {
            IType type = GetNodeValue<IType>(ctx.typ);
            String name = GetNodeValue<String>(ctx.name);
            ParameterList args = GetNodeValue<ParameterList>(ctx.args);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            SetNodeValue(ctx, new NativeMethodDeclaration(name, args, type, stmts));
        }

        public override void ExitJavaArgumentList(OParser.JavaArgumentListContext ctx)
        {
            JavaExpression item = GetNodeValue<JavaExpression>(ctx.item);
            SetNodeValue(ctx, new JavaExpressionList(item));
        }

        public override void ExitJavaArgumentListItem(OParser.JavaArgumentListItemContext ctx)
        {
            JavaExpression item = GetNodeValue<JavaExpression>(ctx.item);
            JavaExpressionList items = GetNodeValue<JavaExpressionList>(ctx.items);
            items.Add(item);
            SetNodeValue(ctx, items);
        }

        public override void ExitJava_method_expression(OParser.Java_method_expressionContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            JavaExpressionList args = GetNodeValue<JavaExpressionList>(ctx.args);
            SetNodeValue(ctx, new JavaMethodExpression(name, args));
        }

        public override void ExitJavaMethodExpression(OParser.JavaMethodExpressionContext ctx)
        {
            JavaExpression exp = GetNodeValue<JavaExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }

        public override void ExitDeclaration(OParser.DeclarationContext ctx)
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

        public override void ExitDeclarations(OParser.DeclarationsContext ctx)
        {
            DeclarationList items = new DeclarationList();
            foreach (ParserRuleContext rule in ctx.declaration())
            {
                IDeclaration item = GetNodeValue<IDeclaration>(rule);
                items.Add(item);
            }
            SetNodeValue(ctx, items);
        }


        public override void ExitJavaBooleanLiteral(OParser.JavaBooleanLiteralContext ctx)
        {
            SetNodeValue(ctx, new JavaBooleanLiteral(ctx.GetText()));
        }

        public override void ExitJavaIntegerLiteral(OParser.JavaIntegerLiteralContext ctx)
        {
            SetNodeValue(ctx, new JavaIntegerLiteral(ctx.GetText()));
        }

        public override void ExitJavaDecimalLiteral(OParser.JavaDecimalLiteralContext ctx)
        {
            SetNodeValue(ctx, new JavaDecimalLiteral(ctx.GetText()));
        }

        public override void ExitJavaCharacterLiteral(OParser.JavaCharacterLiteralContext ctx)
        {
            SetNodeValue(ctx, new JavaCharacterLiteral(ctx.GetText()));
        }

        public override void ExitJavaTextLiteral(OParser.JavaTextLiteralContext ctx)
        {
            SetNodeValue(ctx, new JavaTextLiteral(ctx.GetText()));
        }

        public override void ExitCSharpBooleanLiteral(OParser.CSharpBooleanLiteralContext ctx)
        {
            SetNodeValue(ctx, new CSharpBooleanLiteral(ctx.GetText()));
        }

        public override void ExitCSharpIntegerLiteral(OParser.CSharpIntegerLiteralContext ctx)
        {
            SetNodeValue(ctx, new CSharpIntegerLiteral(ctx.GetText()));
        }

        public override void ExitCSharpDecimalLiteral(OParser.CSharpDecimalLiteralContext ctx)
        {
            SetNodeValue(ctx, new CSharpDecimalLiteral(ctx.GetText()));
        }

        public override void ExitCSharpCharacterLiteral(OParser.CSharpCharacterLiteralContext ctx)
        {
            SetNodeValue(ctx, new CSharpCharacterLiteral(ctx.GetText()));
        }

        public override void ExitCSharpTextLiteral(OParser.CSharpTextLiteralContext ctx)
        {
            SetNodeValue(ctx, new CSharpTextLiteral(ctx.GetText()));
        }

        public override void ExitPythonBooleanLiteral(OParser.PythonBooleanLiteralContext ctx)
        {
            SetNodeValue(ctx, new PythonBooleanLiteral(ctx.GetText()));
        }

        public override void ExitPythonIntegerLiteral(OParser.PythonIntegerLiteralContext ctx)
        {
            SetNodeValue(ctx, new PythonIntegerLiteral(ctx.GetText()));
        }

        public override void ExitPythonDecimalLiteral(OParser.PythonDecimalLiteralContext ctx)
        {
            SetNodeValue(ctx, new PythonDecimalLiteral(ctx.GetText()));
        }

        public override void ExitPythonTextLiteral(OParser.PythonTextLiteralContext ctx)
        {
            SetNodeValue(ctx, new PythonTextLiteral(ctx.GetText()));
        }

        public override void ExitPythonLiteralExpression(OParser.PythonLiteralExpressionContext ctx)
        {
            PythonExpression exp = GetNodeValue<PythonExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }

        public override void ExitJavaCategoryBinding(OParser.JavaCategoryBindingContext ctx)
        {
            JavaIdentifierExpression map = GetNodeValue<JavaIdentifierExpression>(ctx.binding);
            SetNodeValue(ctx, new JavaNativeCategoryBinding(map));
        }

        public override void ExitCSharpCategoryBinding(OParser.CSharpCategoryBindingContext ctx)
        {
            CSharpIdentifierExpression map = GetNodeValue<CSharpIdentifierExpression>(ctx.binding);
            SetNodeValue(ctx, new CSharpNativeCategoryBinding(map));
        }

        public override void ExitPython_module(OParser.Python_moduleContext ctx)
        {
            List<String> ids = new List<String>();
            foreach (OParser.Python_identifierContext ic in ctx.python_identifier())
                ids.Add(ic.GetText());
            PythonModule module = new PythonModule(ids);
            SetNodeValue(ctx, module);
        }

        public override void ExitPython_native_statement(OParser.Python_native_statementContext ctx)
        {
            PythonStatement stmt = GetNodeValue<PythonStatement>(ctx.python_statement());
            PythonModule module = GetNodeValue<PythonModule>(ctx.python_module());
            stmt.setModule(module);
            SetNodeValue(ctx, stmt);
        }

        public override void ExitPython2CategoryBinding(OParser.Python2CategoryBindingContext ctx)
        {
            PythonNativeCategoryBinding map = GetNodeValue<PythonNativeCategoryBinding>(ctx.binding);
            SetNodeValue(ctx, new Python2NativeCategoryBinding(map));
        }

        public override void ExitPython3CategoryBinding(OParser.Python3CategoryBindingContext ctx)
        {
            PythonNativeCategoryBinding map = GetNodeValue<PythonNativeCategoryBinding>(ctx.binding);
            SetNodeValue(ctx, new Python3NativeCategoryBinding(map));
        }

        public override void ExitPython_category_binding(OParser.Python_category_bindingContext ctx)
        {
            String identifier = ctx.identifier().GetText();
            PythonModule module = GetNodeValue<PythonModule>(ctx.python_module());
            PythonNativeCategoryBinding map = new PythonNativeCategoryBinding(identifier, module);
            SetNodeValue(ctx, map);
        }

        override
    public void ExitPythonGlobalMethodExpression(OParser.PythonGlobalMethodExpressionContext ctx)
        {
            PythonMethodExpression exp = GetNodeValue<PythonMethodExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }

        public override void ExitPython_method_expression(OParser.Python_method_expressionContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            PythonArgumentList args = GetNodeValue<PythonArgumentList>(ctx.args);
            PythonMethodExpression method = new PythonMethodExpression(name);
            method.setArguments(args);
            SetNodeValue(ctx, method);
        }

        public override void ExitPythonIdentifierExpression(OParser.PythonIdentifierExpressionContext ctx)
        {
            PythonIdentifierExpression exp = GetNodeValue<PythonIdentifierExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }

        public override void ExitPythonNamedArgumentList(OParser.PythonNamedArgumentListContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            PythonExpression exp = GetNodeValue<PythonExpression>(ctx.exp);
            PythonNamedArgument arg = new PythonNamedArgument(name, exp);
            SetNodeValue(ctx, new PythonArgumentList(arg));
        }

        public override void ExitPythonNamedArgumentListItem(OParser.PythonNamedArgumentListItemContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            PythonExpression exp = GetNodeValue<PythonExpression>(ctx.exp);
            PythonNamedArgument arg = new PythonNamedArgument(name, exp);
            PythonArgumentList items = GetNodeValue<PythonArgumentList>(ctx.items);
            items.Add(arg);
            SetNodeValue(ctx, items);
        }

        public override void ExitPythonOrdinalArgumentList(OParser.PythonOrdinalArgumentListContext ctx)
        {
            PythonExpression exp = GetNodeValue<PythonExpression>(ctx.item);
            PythonOrdinalArgument arg = new PythonOrdinalArgument(exp);
            SetNodeValue(ctx, new PythonArgumentList(arg));
        }


        public override void ExitPythonOrdinalArgumentListItem(OParser.PythonOrdinalArgumentListItemContext ctx)
        {
            PythonExpression exp = GetNodeValue<PythonExpression>(ctx.item);
            PythonOrdinalArgument arg = new PythonOrdinalArgument(exp);
            PythonArgumentList items = GetNodeValue<PythonArgumentList>(ctx.items);
            items.Add(arg);
            SetNodeValue(ctx, items);
        }

        public override void ExitPythonOrdinalOnlyArgumentList(OParser.PythonOrdinalOnlyArgumentListContext ctx)
        {
            PythonArgumentList ordinal = GetNodeValue<PythonArgumentList>(ctx.ordinal);
            SetNodeValue(ctx, ordinal);
        }

        public override void ExitPythonSelectorExpression(OParser.PythonSelectorExpressionContext ctx)
        {
            PythonExpression parent = GetNodeValue<PythonExpression>(ctx.parent);
            PythonSelectorExpression selector = GetNodeValue<PythonSelectorExpression>(ctx.child);
            selector.setParent(parent);
            SetNodeValue(ctx, selector);
        }

        public override void ExitPythonArgumentList(OParser.PythonArgumentListContext ctx)
        {
            PythonArgumentList ordinal = GetNodeValue<PythonArgumentList>(ctx.ordinal);
            PythonArgumentList named = GetNodeValue<PythonArgumentList>(ctx.named);
            ordinal.AddRange(named);
            SetNodeValue(ctx, ordinal);
        }

        public override void ExitPythonMethodExpression(OParser.PythonMethodExpressionContext ctx)
        {
            PythonMethodExpression exp = GetNodeValue<PythonMethodExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }

        public override void ExitPythonNamedOnlyArgumentList(OParser.PythonNamedOnlyArgumentListContext ctx)
        {
            PythonArgumentList named = GetNodeValue<PythonArgumentList>(ctx.named);
            SetNodeValue(ctx, named);
        }

        public override void ExitNativeCategoryBindingList(OParser.NativeCategoryBindingListContext ctx)
        {
            NativeCategoryBinding item = GetNodeValue<NativeCategoryBinding>(ctx.item);
            NativeCategoryBindingList items = new NativeCategoryBindingList(item);
            SetNodeValue(ctx, items);
        }

        public override void ExitNativeCategoryBindingListItem(OParser.NativeCategoryBindingListItemContext ctx)
        {
            NativeCategoryBinding item = GetNodeValue<NativeCategoryBinding>(ctx.item);
            NativeCategoryBindingList items = GetNodeValue<NativeCategoryBindingList>(ctx.items);
            items.Add(item);
            SetNodeValue(ctx, items);
        }

        public override void ExitNative_category_bindings(OParser.Native_category_bindingsContext ctx)
        {
            NativeCategoryBindingList items = GetNodeValue<NativeCategoryBindingList>(ctx.items);
            SetNodeValue(ctx, items);
        }

        public override void ExitNative_category_declaration(OParser.Native_category_declarationContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            IdentifierList attrs = GetNodeValue<IdentifierList>(ctx.attrs);
            NativeCategoryBindingList bindings = GetNodeValue<NativeCategoryBindingList>(ctx.bindings);
            MethodDeclarationList methods = GetNodeValue<MethodDeclarationList>(ctx.methods);
            NativeCategoryDeclaration decl = new NativeCategoryDeclaration(name, attrs, bindings, null, methods);
            decl.Storable = ctx.STORABLE() != null;
            SetNodeValue(ctx, decl);
        }


        public override void ExitNative_widget_declaration(OParser.Native_widget_declarationContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            NativeCategoryBindingList bindings = GetNodeValue<NativeCategoryBindingList>(ctx.bindings);
            MethodDeclarationList methods = GetNodeValue<MethodDeclarationList>(ctx.methods);
            SetNodeValue(ctx, new NativeWidgetDeclaration(name, bindings, methods));
        }


        public override void ExitNativeCategoryDeclaration(OParser.NativeCategoryDeclarationContext ctx)
        {
            IDeclaration decl = GetNodeValue<IDeclaration>(ctx.decl);
            SetNodeValue(ctx, decl);
        }

        public override void ExitNative_resource_declaration(OParser.Native_resource_declarationContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            IdentifierList attrs = GetNodeValue<IdentifierList>(ctx.attrs);
            NativeCategoryBindingList bindings = GetNodeValue<NativeCategoryBindingList>(ctx.bindings);
            MethodDeclarationList methods = GetNodeValue<MethodDeclarationList>(ctx.methods);
            NativeResourceDeclaration decl = new NativeResourceDeclaration(name, attrs, bindings, null, methods);
            decl.Storable = ctx.STORABLE() != null;
            SetNodeValue(ctx, decl);
        }

        public override void ExitResource_declaration(OParser.Resource_declarationContext ctx)
        {
            IDeclaration decl = GetNodeValue<IDeclaration>(ctx.native_resource_declaration());
            SetNodeValue(ctx, decl);
        }

        public override void ExitParenthesis_expression(OParser.Parenthesis_expressionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.expression());
            SetNodeValue(ctx, new ParenthesisExpression(exp));
        }

        public override void ExitParenthesisExpression(OParser.ParenthesisExpressionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }

        public override void ExitNative_symbol_list(OParser.Native_symbol_listContext ctx)
        {
            NativeSymbolList items = new NativeSymbolList();
            foreach (ParserRuleContext rule in ctx.native_symbol())
            {
                NativeSymbol item = GetNodeValue<NativeSymbol>(rule);
                items.add(item);
            }
            SetNodeValue(ctx, items);
        }

        public override void ExitEnum_native_declaration(OParser.Enum_native_declarationContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            NativeType type = GetNodeValue<NativeType>(ctx.typ);
            NativeSymbolList symbols = GetNodeValue<NativeSymbolList>(ctx.symbols);
            SetNodeValue(ctx, new EnumeratedNativeDeclaration(name, type, symbols));
        }

        public override void ExitFor_each_statement(OParser.For_each_statementContext ctx)
        {
            String name1 = GetNodeValue<String>(ctx.name1);
            String name2 = GetNodeValue<String>(ctx.name2);
            IExpression source = GetNodeValue<IExpression>(ctx.source);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            SetNodeValue(ctx, new ForEachStatement(name1, name2, source, stmts));
        }

        public override void ExitForEachStatement(OParser.ForEachStatementContext ctx)
        {
            IStatement stmt = GetNodeValue<IStatement>(ctx.stmt);
            SetNodeValue(ctx, stmt);
        }


        public override void ExitJsxChild(OParser.JsxChildContext ctx)
        {
            SetNodeValue(ctx, GetNodeValue<Object>(ctx.jsx));
        }


        public override void ExitJsxCode(OParser.JsxCodeContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            String suite = getHiddenTokensAfter(ctx.RCURL());
            SetNodeValue(ctx, new JsxCode(exp, suite));
        }


        public override void ExitJsxExpression(OParser.JsxExpressionContext ctx)
        {
            SetNodeValue(ctx, GetNodeValue<Object>(ctx.exp));
        }


        public override void ExitJsxElement(OParser.JsxElementContext ctx)
        {
            JsxElement elem = GetNodeValue<JsxElement>(ctx.opening);
            JsxClosing closing = GetNodeValue<JsxClosing>(ctx.closing);
            elem.setClosing(closing);
            List<IJsxExpression> children = GetNodeValue<List<IJsxExpression>>(ctx.children_);
            elem.setChildren(children);
            SetNodeValue(ctx, elem);
        }

        public override void ExitJsxSelfClosing(OParser.JsxSelfClosingContext ctx)
        {
            SetNodeValue(ctx, GetNodeValue<Object>(ctx.jsx));
        }


        public override void ExitJsxText(OParser.JsxTextContext ctx)
        {
            String text = ParserUtils.GetFullText(ctx.text);
            SetNodeValue(ctx, new JsxText(text));
        }


        public override void ExitJsxValue(OParser.JsxValueContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, new JsxExpression(exp));
        }

        public override void ExitJsx_attribute(OParser.Jsx_attributeContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            IJsxValue value = GetNodeValue<IJsxValue>(ctx.value);
            IToken stop = value != null ? ctx.value.Stop : ctx.name.Stop;
            String suite = getHiddenTokensAfter(stop);
            SetNodeValue(ctx, new JsxProperty(name, value, suite));
        }


        public override void ExitJsx_children(OParser.Jsx_childrenContext ctx)
        {
            List<IJsxExpression> list = new List<IJsxExpression>();
            foreach (ParserRuleContext child in ctx.jsx_child())
                list.Add(GetNodeValue<IJsxExpression>(child));
            SetNodeValue(ctx, list);
        }

        public override void ExitJsx_element_name(OParser.Jsx_element_nameContext ctx)
        {
            String name = ctx.GetText();
            SetNodeValue(ctx, name);
        }

        public override void ExitJsx_expression(OParser.Jsx_expressionContext ctx)
        {
            SetNodeValue(ctx, GetNodeValue<Object>(ctx.GetChild(0)));
        }


        public override void ExitJsx_fragment(OParser.Jsx_fragmentContext ctx)
        {
            String suite = getHiddenTokensAfter(ctx.jsx_fragment_start().Stop);
            JsxFragment fragment = new JsxFragment(suite);
            List<IJsxExpression> children = GetNodeValue<List<IJsxExpression>>(ctx.children_);
            fragment.setChildren(children);
            SetNodeValue(ctx, fragment);
        }


        public override void ExitJsx_identifier(OParser.Jsx_identifierContext ctx)
        {
            String name = ctx.GetText();
            SetNodeValue(ctx, name);
        }

        public override void ExitJsxLiteral(OParser.JsxLiteralContext ctx)
        {
            String text = ctx.GetText();
            SetNodeValue(ctx, new JsxLiteral(text));
        }

        public override void ExitJsx_opening(OParser.Jsx_openingContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            String nameSuite = getHiddenTokensAfter(ctx.name.Stop);
            List<JsxProperty> attributes = new List<JsxProperty>();
            foreach (ParserRuleContext child in ctx.jsx_attribute())
                attributes.Add(GetNodeValue<JsxProperty>(child));
            String openingSuite = getHiddenTokensAfter(ctx.GT());
            SetNodeValue(ctx, new JsxElement(name, nameSuite, attributes, openingSuite));
        }


        public override void ExitJsx_closing(OParser.Jsx_closingContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            String suite = getHiddenTokensAfter(ctx.GT());
            SetNodeValue(ctx, new JsxClosing(name, suite));
        }

        public override void ExitJsx_self_closing(OParser.Jsx_self_closingContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            String nameSuite = getHiddenTokensAfter(ctx.name.Stop);
            List<JsxProperty> attributes = new List<JsxProperty>();
            foreach (ParserRuleContext child in ctx.jsx_attribute())
                attributes.Add(GetNodeValue<JsxProperty>(child));
            String suite = getHiddenTokensAfter(ctx.GT());
            SetNodeValue(ctx, new JsxSelfClosing(name, nameSuite, attributes, suite));
        }



        public override void ExitCssExpression(OParser.CssExpressionContext ctx)
        {
            SetNodeValue(ctx, GetNodeValue<Object>(ctx.exp));
        }


        public override void ExitCss_expression(OParser.Css_expressionContext ctx)
        {
            CssExpression exp = new CssExpression();
            foreach (ParserRuleContext child in ctx.css_field())
            {
                exp.AddField(GetNodeValue<CssField>(child));
            }
            SetNodeValue(ctx, exp);
        }


        public override void ExitCss_field(OParser.Css_fieldContext ctx)
        {
            String name = ctx.name.GetText();
            ICssValue value = GetNodeValue<ICssValue>(ctx.value);
            SetNodeValue(ctx, new CssField(name, value));
        }



        public override void ExitCssText(OParser.CssTextContext ctx)
        {
            String text = input.GetText(ctx.text.Start, ctx.text.Stop);
            SetNodeValue(ctx, new CssText(text));
        }


        public override void ExitCssValue(OParser.CssValueContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, new CssCode(exp));
        }


        public override void ExitKey_token(OParser.Key_tokenContext ctx)
        {
            SetNodeValue(ctx, ctx.GetText());
        }

        public override void ExitValue_token(OParser.Value_tokenContext ctx)
        {
            SetNodeValue(ctx, ctx.GetText());
        }

        public override void ExitNamed_argument(OParser.Named_argumentContext ctx)
        {
            String name = GetNodeValue<String>(ctx.variable_identifier());
            UnresolvedParameter arg = new UnresolvedParameter(name);
            IExpression exp = GetNodeValue<IExpression>(ctx.literal_expression());
            arg.DefaultValue = exp;
            SetNodeValue(ctx, arg);
        }

        public override void ExitClosureStatement(OParser.ClosureStatementContext ctx)
        {
            ConcreteMethodDeclaration decl = GetNodeValue<ConcreteMethodDeclaration>(ctx.decl);
            SetNodeValue(ctx, new DeclarationStatement<ConcreteMethodDeclaration>(decl));
        }

        public override void ExitReturn_statement(OParser.Return_statementContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, new ReturnStatement(exp));
        }

        public override void ExitReturnStatement(OParser.ReturnStatementContext ctx)
        {
            IStatement stmt = GetNodeValue<IStatement>(ctx.stmt);
            SetNodeValue(ctx, stmt);
        }

        public override void ExitType_expression(OParser.Type_expressionContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            SetNodeValue(ctx, new TypeExpression(new CategoryType(name)));
        }

        public override void ExitTypeExpression(OParser.TypeExpressionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }

        public override void ExitIf_statement(OParser.If_statementContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            IfElementList elseIfs = GetNodeValue<IfElementList>(ctx.elseIfs);
            StatementList elseStmts = GetNodeValue<StatementList>(ctx.elseStmts);
            SetNodeValue(ctx, new IfStatement(exp, stmts, elseIfs, elseStmts));
        }

        public override void ExitElseIfStatementList(OParser.ElseIfStatementListContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            IfElement elem = new IfElement(exp, stmts);
            SetNodeValue(ctx, new IfElementList(elem));
        }

        public override void ExitElseIfStatementListItem(OParser.ElseIfStatementListItemContext ctx)
        {
            IfElementList items = GetNodeValue<IfElementList>(ctx.items);
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            IfElement elem = new IfElement(exp, stmts);
            items.Add(elem);
            SetNodeValue(ctx, items);
        }

        public override void ExitIfStatement(OParser.IfStatementContext ctx)
        {
            IStatement stmt = GetNodeValue<IStatement>(ctx.stmt);
            SetNodeValue(ctx, stmt);
        }


        public override void ExitSuperExpression(OParser.SuperExpressionContext ctx)
        {
            SetNodeValue(ctx, new SuperExpression());
        }


        public override void ExitSwitchStatement(OParser.SwitchStatementContext ctx)
        {
            IStatement stmt = GetNodeValue<IStatement>(ctx.stmt);
            SetNodeValue(ctx, stmt);
        }

        public override void ExitAssignTupleStatement(OParser.AssignTupleStatementContext ctx)
        {
            IStatement stmt = GetNodeValue<IStatement>(ctx.stmt);
            SetNodeValue(ctx, stmt);
        }

        public override void ExitRaiseStatement(OParser.RaiseStatementContext ctx)
        {
            IStatement stmt = GetNodeValue<IStatement>(ctx.stmt);
            SetNodeValue(ctx, stmt);
        }

        public override void ExitWriteStatement(OParser.WriteStatementContext ctx)
        {
            IStatement stmt = GetNodeValue<IStatement>(ctx.stmt);
            SetNodeValue(ctx, stmt);
        }


        public override void ExitWith_singleton_statement(OParser.With_singleton_statementContext ctx)
        {
            String name = GetNodeValue<String>(ctx.typ);
            CategoryType type = new CategoryType(name);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            SetNodeValue(ctx, new WithSingletonStatement(type, stmts));
        }

        public override void ExitWithSingletonStatement(OParser.WithSingletonStatementContext ctx)
        {
            IStatement stmt = GetNodeValue<IStatement>(ctx.stmt);
            SetNodeValue(ctx, stmt);
        }


        public override void ExitWithResourceStatement(OParser.WithResourceStatementContext ctx)
        {
            IStatement stmt = GetNodeValue<IStatement>(ctx.stmt);
            SetNodeValue(ctx, stmt);
        }

        public override void ExitWhileStatement(OParser.WhileStatementContext ctx)
        {
            IStatement stmt = GetNodeValue<IStatement>(ctx.stmt);
            SetNodeValue(ctx, stmt);
        }

        public override void ExitDoWhileStatement(OParser.DoWhileStatementContext ctx)
        {
            IStatement stmt = GetNodeValue<IStatement>(ctx.stmt);
            SetNodeValue(ctx, stmt);
        }

        public override void ExitTryStatement(OParser.TryStatementContext ctx)
        {
            IStatement stmt = GetNodeValue<IStatement>(ctx.stmt);
            SetNodeValue(ctx, stmt);
        }

        public override void ExitEqualsExpression(OParser.EqualsExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            EqOp eqOp;
            switch (ctx.op.Type)
            {
                case OLexer.EQ2:
                    eqOp = EqOp.EQUALS;
                    break;
                case OLexer.XEQ:
                    eqOp = EqOp.NOT_EQUALS;
                    break;
                case OLexer.TEQ:
                    eqOp = EqOp.ROUGHLY;
                    break;
                default:
                    throw new Exception("Operator " + ctx.op.Type);
            }
            SetNodeValue(ctx, new EqualsExpression(left, eqOp, right));
        }

       public override void ExitCompareExpression(OParser.CompareExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            CmpOp cmpOp;
            switch (ctx.op.Type)
            {
                case OLexer.LT:
                    cmpOp = CmpOp.LT;
                    break;
                case OLexer.LTE:
                    cmpOp = CmpOp.LTE;
                    break;
                case OLexer.GT:
                    cmpOp = CmpOp.GT;
                    break;
                case OLexer.GTE:
                    cmpOp = CmpOp.GTE;
                    break;
                default:
                    throw new Exception("Operator " + ctx.op.Type);
            }
            SetNodeValue(ctx, new CompareExpression(left, cmpOp, right));
        }

  
        public override void ExitAtomicSwitchCase(OParser.AtomicSwitchCaseContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            SetNodeValue(ctx, new AtomicSwitchCase(exp, stmts));
        }

        public override void ExitCommentStatement(OParser.CommentStatementContext ctx)
        {
            SetNodeValue(ctx, GetNodeValue<Object>(ctx.comment_statement()));
        }

        public override void ExitComment_statement(OParser.Comment_statementContext ctx)
        {
            SetNodeValue(ctx, new CommentStatement(ctx.GetText()));
        }


        public override void ExitCollectionSwitchCase(OParser.CollectionSwitchCaseContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            SetNodeValue(ctx, new CollectionSwitchCase(exp, stmts));
        }

        public override void ExitSwitch_case_statement_list(OParser.Switch_case_statement_listContext ctx)
        {
            SwitchCaseList items = new SwitchCaseList();
            foreach (ParserRuleContext rule in ctx.switch_case_statement())
            {
                SwitchCase item = GetNodeValue<SwitchCase>(rule);
                items.Add(item);
            }
            SetNodeValue(ctx, items);
        }

        public override void ExitSwitch_statement(OParser.Switch_statementContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SwitchCaseList cases = GetNodeValue<SwitchCaseList>(ctx.cases);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            SwitchStatement stmt = new SwitchStatement(exp, cases, stmts);
            SetNodeValue(ctx, stmt);
        }

        public override void ExitLiteralRangeLiteral(OParser.LiteralRangeLiteralContext ctx)
        {
            IExpression low = GetNodeValue<IExpression>(ctx.low);
            IExpression high = GetNodeValue<IExpression>(ctx.high);
            SetNodeValue(ctx, new RangeLiteral(low, high));
        }

        public override void ExitLiteralSetLiteral(OParser.LiteralSetLiteralContext ctx)
        {
            ExpressionList items = GetNodeValue<ExpressionList>(ctx.literal_list_literal());
            SetNodeValue(ctx, new SetLiteral(items));
        }

        public override void ExitLiteralListLiteral(OParser.LiteralListLiteralContext ctx)
        {
            ExpressionList exp = GetNodeValue<ExpressionList>(ctx.literal_list_literal());
            SetNodeValue(ctx, new ListLiteral(exp, false));
        }

        public override void ExitLiteral_list_literal(OParser.Literal_list_literalContext ctx)
        {
            ExpressionList items = new ExpressionList();
            foreach (ParserRuleContext rule in ctx.atomic_literal())
            {
                IExpression item = GetNodeValue<IExpression>(rule);
                items.add(item);
            }
            SetNodeValue(ctx, items);
        }


        public override void ExitInExpression(OParser.InExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            ContOp contOp = ctx.NOT() == null ? ContOp.IN : ContOp.NOT_IN;
            SetNodeValue(ctx, new ContainsExpression(left, contOp, right));
        }

        public override void ExitCssType(OParser.CssTypeContext ctx)
        {
            SetNodeValue(ctx, CssType.Instance);
        }


        public override void ExitHasExpression(OParser.HasExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            ContOp contOp = ctx.NOT() == null ? ContOp.HAS : ContOp.NOT_HAS;
            SetNodeValue(ctx, new ContainsExpression(left, contOp, right));
        }

        public override void ExitHasAllExpression(OParser.HasAllExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            ContOp contOp = ctx.NOT() == null ? ContOp.HAS_ALL : ContOp.NOT_HAS_ALL;
            SetNodeValue(ctx, new ContainsExpression(left, contOp, right));
        }


        public override void ExitHasAnyExpression(OParser.HasAnyExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            ContOp contOp = ctx.NOT() == null ? ContOp.HAS_ANY : ContOp.NOT_HAS_ANY;
            SetNodeValue(ctx, new ContainsExpression(left, contOp, right));
        }


       public override void ExitContainsExpression(OParser.ContainsExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            EqOp eqOp = ctx.NOT() == null ? EqOp.CONTAINS : EqOp.NOT_CONTAINS;
            SetNodeValue(ctx, new EqualsExpression(left, eqOp, right));
        }


        public override void ExitDivideExpression(OParser.DivideExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            SetNodeValue(ctx, new DivideExpression(left, right));
        }

        public override void ExitIntDivideExpression(OParser.IntDivideExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            SetNodeValue(ctx, new IntDivideExpression(left, right));
        }

        public override void ExitModuloExpression(OParser.ModuloExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            SetNodeValue(ctx, new ModuloExpression(left, right));
        }

        public override void ExitAnnotation_constructor(OParser.Annotation_constructorContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            DocEntryList args = new DocEntryList();
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            if (exp != null)
                args.add(new DocEntry(null, exp));
            foreach (RuleContext argCtx in ctx.annotation_argument())
            {
                DocEntry arg = GetNodeValue<DocEntry>(argCtx);
                args.add(arg);
            }
            SetNodeValue(ctx, new Annotation(name, args));
        }

        public override void ExitAnnotation_argument(OParser.Annotation_argumentContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, new DocEntry(new DocIdentifierKey(name), exp));
        }

        public override void ExitAnnotation_identifier(OParser.Annotation_identifierContext ctx)
        {
            SetNodeValue(ctx, ctx.GetText());
        }


        public override void ExitAnnotation_argument_name(OParser.Annotation_argument_nameContext ctx)
        {
            SetNodeValue(ctx, ctx.GetText());
        }

        public override void ExitAnnotationLiteralValue(OParser.AnnotationLiteralValueContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }

        public override void ExitAnnotationTypeValue(OParser.AnnotationTypeValueContext ctx)
        {
            IType type = GetNodeValue<IType>(ctx.typ);
            SetNodeValue(ctx, new TypeExpression(type));
        }

        public override void ExitAndExpression(OParser.AndExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            SetNodeValue(ctx, new AndExpression(left, right));
        }

        public override void ExitNullLiteral(OParser.NullLiteralContext ctx)
        {
            SetNodeValue(ctx, NullLiteral.Instance);
        }

        public override void ExitOperatorArgument(OParser.OperatorArgumentContext ctx)
        {
            bool mutable = ctx.MUTABLE() != null;
            IParameter arg = GetNodeValue<IParameter>(ctx.arg);
            arg.setMutable(mutable);
            SetNodeValue(ctx, arg);
        }

        public override void ExitOperatorPlus(OParser.OperatorPlusContext ctx)
        {
            SetNodeValue(ctx, Operator.PLUS);
        }

        public override void ExitOperatorMinus(OParser.OperatorMinusContext ctx)
        {
            SetNodeValue(ctx, Operator.MINUS);
        }

        public override void ExitOperatorMultiply(OParser.OperatorMultiplyContext ctx)
        {
            SetNodeValue(ctx, Operator.MULTIPLY);
        }

        public override void ExitOperatorDivide(OParser.OperatorDivideContext ctx)
        {
            SetNodeValue(ctx, Operator.DIVIDE);
        }

        public override void ExitOperatorIDivide(OParser.OperatorIDivideContext ctx)
        {
            SetNodeValue(ctx, Operator.IDIVIDE);
        }

        public override void ExitOperatorModulo(OParser.OperatorModuloContext ctx)
        {
            SetNodeValue(ctx, Operator.MODULO);
        }

        public override void ExitOperator_method_declaration(OParser.Operator_method_declarationContext ctx)
        {
            Operator op = GetNodeValue<Operator>(ctx.op);
            IParameter arg = GetNodeValue<IParameter>(ctx.arg);
            IType typ = GetNodeValue<IType>(ctx.typ);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            OperatorMethodDeclaration decl = new OperatorMethodDeclaration(op, arg, typ, stmts);
            SetNodeValue(ctx, decl);
        }

        public override void ExitOrder_by(OParser.Order_byContext ctx)
        {
            IdentifierList names = new IdentifierList();
            foreach (OParser.Variable_identifierContext ctx_ in ctx.variable_identifier())
                names.add(GetNodeValue<string>(ctx_));
            OrderByClause clause = new OrderByClause(names, ctx.DESC() != null);
            SetNodeValue(ctx, clause);
        }

        public override void ExitOrder_by_list(OParser.Order_by_listContext ctx)
        {
            OrderByClauseList list = new OrderByClauseList();
            foreach (OParser.Order_byContext ctx_ in ctx.order_by())
                list.add(GetNodeValue<OrderByClause>(ctx_));
            SetNodeValue(ctx, list);
        }


        public override void ExitOrExpression(OParser.OrExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            SetNodeValue(ctx, new OrExpression(left, right));
        }

        public override void ExitMultiplyExpression(OParser.MultiplyExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IExpression right = GetNodeValue<IExpression>(ctx.right);
            SetNodeValue(ctx, new MultiplyExpression(left, right));
        }

        public override void ExitMutable_category_type(OParser.Mutable_category_typeContext ctx)
        {
            CategoryType typ = GetNodeValue<CategoryType>(ctx.category_type());
            typ.Mutable = ctx.MUTABLE() != null;
            SetNodeValue(ctx, typ);
        }


        public override void ExitMutableInstanceExpression(OParser.MutableInstanceExpressionContext ctx)
        {
            IExpression source = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, new MutableExpression(source));
        }

        public override void ExitMutableSelectableExpression(OParser.MutableSelectableExpressionContext ctx)
        {
            SetNodeValue(ctx, GetNodeValue<InstanceExpression>(ctx.exp));
        }


        public override void ExitMutableSelectorExpression(OParser.MutableSelectorExpressionContext ctx)
        {
            IExpression parent = GetNodeValue<IExpression>(ctx.parent);
            SelectorExpression selector = GetNodeValue<SelectorExpression>(ctx.selector);
            selector.setParent(parent);
            SetNodeValue(ctx, selector);
        }


        public override void ExitMinusExpression(OParser.MinusExpressionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, new MinusExpression(exp));
        }

        public override void ExitNotExpression(OParser.NotExpressionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, new NotExpression(exp));
        }

        public override void ExitWhile_statement(OParser.While_statementContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            SetNodeValue(ctx, new WhileStatement(exp, stmts));
        }

        public override void ExitDo_while_statement(OParser.Do_while_statementContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            SetNodeValue(ctx, new DoWhileStatement(exp, stmts));
        }

        public override void ExitSliceFirstAndLast(OParser.SliceFirstAndLastContext ctx)
        {
            IExpression first = GetNodeValue<IExpression>(ctx.first);
            IExpression last = GetNodeValue<IExpression>(ctx.last);
            SetNodeValue(ctx, new SliceSelector(first, last));
        }

        public override void ExitSliceFirstOnly(OParser.SliceFirstOnlyContext ctx)
        {
            IExpression first = GetNodeValue<IExpression>(ctx.first);
            SetNodeValue(ctx, new SliceSelector(first, null));
        }

        public override void ExitSliceLastOnly(OParser.SliceLastOnlyContext ctx)
        {
            IExpression last = GetNodeValue<IExpression>(ctx.last);
            SetNodeValue(ctx, new SliceSelector(null, last));
        }

        public override void ExitSorted_expression(OParser.Sorted_expressionContext ctx)
        {
            IExpression source = GetNodeValue<IExpression>(ctx.source);
            bool descending = ctx.DESC() != null;
            IExpression key = GetNodeValue<IExpression>(ctx.key);
            SetNodeValue(ctx, new SortedExpression(source, descending, key));
        }

        public override void ExitSorted_key(OParser.Sorted_keyContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.GetChild(0));
            SetNodeValue(ctx, exp);
        }

        public override void ExitDocument_expression(OParser.Document_expressionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, new DocumentExpression(exp));
        }

        public override void ExitDocumentType(OParser.DocumentTypeContext ctx)
        {
            SetNodeValue(ctx, DocumentType.Instance);
        }

        public override void ExitDocument_literal(OParser.Document_literalContext ctx)
        {
            DocEntryList entries = GetNodeValue<DocEntryList>(ctx.doc_entry_list());
            if (entries == null)
                entries = new DocEntryList();
            SetNodeValue(ctx, new DocumentLiteral(entries));
        }

        public override void ExitFetchStatement(OParser.FetchStatementContext ctx)
        {
            SetNodeValue(ctx, GetNodeValue<object>(ctx.stmt));
        }

        public override void ExitFetchOne(OParser.FetchOneContext ctx)
        {
            CategoryType category = GetNodeValue<CategoryType>(ctx.typ);
            IExpression filter = GetNodeValue<IExpression>(ctx.predicate);
            SetNodeValue(ctx, new FetchOneExpression(category, filter));
        }

        public override void ExitFetchOneAsync(OParser.FetchOneAsyncContext ctx)
        {
            CategoryType category = GetNodeValue<CategoryType>(ctx.typ);
            IExpression filter = GetNodeValue<IExpression>(ctx.predicate);
            ThenWith thenWith = ThenWith.OrEmpty(GetNodeValue<ThenWith>(ctx.then()));
            SetNodeValue(ctx, new FetchOneStatement(category, filter, thenWith));
        }

        public override void ExitFetchMany(OParser.FetchManyContext ctx)
        {
            CategoryType category = GetNodeValue<CategoryType>(ctx.typ);
            IExpression filter = GetNodeValue<IExpression>(ctx.predicate);
            IExpression start = GetNodeValue<IExpression>(ctx.xstart);
            IExpression stop = GetNodeValue<IExpression>(ctx.xstop);
            OrderByClauseList orderBy = GetNodeValue<OrderByClauseList>(ctx.orderby);
            SetNodeValue(ctx, new FetchManyExpression(category, filter, start, stop, orderBy));
        }

        public override void ExitFetchManyAsync(OParser.FetchManyAsyncContext ctx)
        {
            CategoryType category = GetNodeValue<CategoryType>(ctx.typ);
            IExpression filter = GetNodeValue<IExpression>(ctx.predicate);
            IExpression start = GetNodeValue<IExpression>(ctx.xstart);
            IExpression stop = GetNodeValue<IExpression>(ctx.xstop);
            OrderByClauseList orderBy = GetNodeValue<OrderByClauseList>(ctx.orderby);
            ThenWith thenWith = ThenWith.OrEmpty(GetNodeValue<ThenWith>(ctx.then()));
            SetNodeValue(ctx, new FetchManyStatement(category, filter, start, stop, orderBy, thenWith));
        }

        public override void ExitThen(OParser.ThenContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            SetNodeValue(ctx, new ThenWith(name, stmts));
        }

        public override void ExitFiltered_list_expression(OParser.Filtered_list_expressionContext ctx)
        {
            IExpression source = GetNodeValue<IExpression>(ctx.source);
             String itemName = GetNodeValue<String>(ctx.name);
            IExpression predicate = GetNodeValue<IExpression>(ctx.predicate);
            PredicateExpression expression = null;
            if (itemName != null)
                expression = new ExplicitPredicateExpression(itemName, predicate);
            else if (predicate is PredicateExpression)
                expression = (PredicateExpression)predicate;
            else
                throw new Exception();
            SetNodeValue(ctx, new FilteredExpression(source, expression));
        }

        public override void ExitArrowFilterExpression(OParser.ArrowFilterExpressionContext ctx)
        {
            SetNodeValue(ctx, GetNodeValue<IExpression>(ctx.arrow_expression()));
        }


        public override void ExitExplicitFilterExpression(OParser.ExplicitFilterExpressionContext ctx)
        {
            String name = GetNodeValue<string>(ctx.variable_identifier());
            IExpression predicate = GetNodeValue<IExpression>(ctx.expression());
            SetNodeValue(ctx, new ExplicitPredicateExpression(name, predicate));
        }

        public override void ExitOtherFilterExpression(OParser.OtherFilterExpressionContext ctx)
        {
            SetNodeValue(ctx, GetNodeValue<IExpression>(ctx.expression()));
        }

        public override void ExitCode_type(OParser.Code_typeContext ctx)
        {
            SetNodeValue(ctx, CodeType.Instance);
        }

        public override void ExitExecuteExpression(OParser.ExecuteExpressionContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            SetNodeValue(ctx, new ExecuteExpression(name));
        }

        public override void ExitCodeExpression(OParser.CodeExpressionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, new CodeExpression(exp));
        }

        public override void ExitCode_argument(OParser.Code_argumentContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            SetNodeValue(ctx, new CodeParameter(name));
        }

        public override void ExitCategory_symbol(OParser.Category_symbolContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            ArgumentList args = GetNodeValue<ArgumentList>(ctx.args);
            SetNodeValue(ctx, new CategorySymbol(name, args));
        }

        public override void ExitCategory_symbol_list(OParser.Category_symbol_listContext ctx)
        {
            CategorySymbolList items = new CategorySymbolList();
            foreach (ParserRuleContext rule in ctx.category_symbol())
            {
                CategorySymbol item = GetNodeValue<CategorySymbol>(rule);
                items.add(item);
            }
            SetNodeValue(ctx, items);
        }



        public override void ExitEnum_category_declaration(OParser.Enum_category_declarationContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            IdentifierList attrs = GetNodeValue<IdentifierList>(ctx.attrs);
            String parent = GetNodeValue<String>(ctx.derived);
            IdentifierList derived = parent == null ? null : new IdentifierList(parent);
            CategorySymbolList symbols = GetNodeValue<CategorySymbolList>(ctx.symbols);
            SetNodeValue(ctx, new EnumeratedCategoryDeclaration(name, attrs, derived, symbols));
        }

        public override void ExitRead_all_expression(OParser.Read_all_expressionContext ctx)
        {
            IExpression source = GetNodeValue<IExpression>(ctx.source);
            SetNodeValue(ctx, new ReadAllExpression(source));
        }


        public override void ExitRead_blob_expression(OParser.Read_blob_expressionContext ctx)
        {
            IExpression source = GetNodeValue<IExpression>(ctx.source);
            SetNodeValue(ctx, new ReadBlobExpression(source));
        }


        public override void ExitRead_one_expression(OParser.Read_one_expressionContext ctx)
        {
            IExpression source = GetNodeValue<IExpression>(ctx.source);
            SetNodeValue(ctx, new ReadOneExpression(source));
        }

        public override void ExitRead_statement(OParser.Read_statementContext ctx)
        {
            IExpression source = GetNodeValue<IExpression>(ctx.source);
            ThenWith thenWith = ThenWith.OrEmpty(GetNodeValue<ThenWith>(ctx.then()));
            SetNodeValue(ctx, new ReadStatement(source, thenWith));
        }

        public override void ExitReadStatement(OParser.ReadStatementContext ctx)
        {
            ReadStatement stmt = GetNodeValue<ReadStatement>(ctx.stmt);
            SetNodeValue(ctx, stmt);
        }


        public override void ExitWrite_statement(OParser.Write_statementContext ctx)
        {
            IExpression what = GetNodeValue<IExpression>(ctx.what);
            IExpression target = GetNodeValue<IExpression>(ctx.target);
            ThenWith thenWith = GetNodeValue<ThenWith>(ctx.then());
            SetNodeValue(ctx, new WriteStatement(what, target, thenWith));
        }

        public override void ExitWith_resource_statement(OParser.With_resource_statementContext ctx)
        {
            AssignVariableStatement stmt = GetNodeValue<AssignVariableStatement>(ctx.stmt);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            SetNodeValue(ctx, new WithResourceStatement(stmt, stmts));
        }

        public override void ExitAnyType(OParser.AnyTypeContext ctx)
        {
            SetNodeValue(ctx, AnyType.Instance);
        }

        public override void ExitAnyListType(OParser.AnyListTypeContext ctx)
        {
            IType type = GetNodeValue<IType>(ctx.any_type());
            SetNodeValue(ctx, new ListType(type));
        }

        public override void ExitAnyDictType(OParser.AnyDictTypeContext ctx)
        {
            IType type = GetNodeValue<IType>(ctx.any_type());
            SetNodeValue(ctx, new DictType(type));
        }

        public override void ExitCastExpression(OParser.CastExpressionContext ctx)
        {
            IExpression left = GetNodeValue<IExpression>(ctx.left);
            IType type = GetNodeValue<IType>(ctx.right);
            SetNodeValue(ctx, new CastExpression(left, type, ctx.MUTABLE() != null));
        }

        public override void ExitCatchAtomicStatement(OParser.CatchAtomicStatementContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            SetNodeValue(ctx, new AtomicSwitchCase(new SymbolExpression(name), stmts));
        }

        public override void ExitCatchCollectionStatement(OParser.CatchCollectionStatementContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            SetNodeValue(ctx, new CollectionSwitchCase(exp, stmts));
        }

        public override void ExitCatch_statement_list(OParser.Catch_statement_listContext ctx)
        {
            SwitchCaseList items = new SwitchCaseList();
            foreach (ParserRuleContext rule in ctx.catch_statement())
            {
                SwitchCase item = GetNodeValue<SwitchCase>(rule);
                items.Add(item);
            }
            SetNodeValue(ctx, items);
        }


        public override void ExitTry_statement(OParser.Try_statementContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            StatementList stmts = GetNodeValue<StatementList>(ctx.stmts);
            SwitchCaseList handlers = GetNodeValue<SwitchCaseList>(ctx.handlers);
            StatementList anyStmts = GetNodeValue<StatementList>(ctx.anyStmts);
            StatementList finalStmts = GetNodeValue<StatementList>(ctx.finalStmts);
            SwitchErrorStatement stmt = new SwitchErrorStatement(name, stmts, handlers, anyStmts, finalStmts);
            SetNodeValue(ctx, stmt);
        }

        public override void ExitRaise_statement(OParser.Raise_statementContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, new RaiseStatement(exp));
        }

        public override void ExitMatchingList(OParser.MatchingListContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.source);
            SetNodeValue(ctx, new MatchingCollectionConstraint(exp));
        }

        public override void ExitMatchingRange(OParser.MatchingRangeContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.source);
            SetNodeValue(ctx, new MatchingCollectionConstraint(exp));
        }

        public override void ExitMatchingExpression(OParser.MatchingExpressionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.exp);
            SetNodeValue(ctx, new MatchingExpressionConstraint(exp));
        }

        public override void ExitMatchingPattern(OParser.MatchingPatternContext ctx)
        {
            SetNodeValue(ctx, new MatchingPatternConstraint(new TextLiteral(ctx.text.Text)));
        }

        public override void ExitMatchingSet(OParser.MatchingSetContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.source);
            SetNodeValue(ctx, new MatchingCollectionConstraint(exp));
        }

        public override void ExitCsharp_item_expression(OParser.Csharp_item_expressionContext ctx)
        {
            CSharpExpression exp = GetNodeValue<CSharpExpression>(ctx.exp);
            SetNodeValue(ctx, new CSharpItemExpression(exp));
        }

        public override void ExitCsharp_method_expression(OParser.Csharp_method_expressionContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            CSharpExpressionList args = GetNodeValue<CSharpExpressionList>(ctx.args);
            SetNodeValue(ctx, new CSharpMethodExpression(name, args));
        }

        public override void ExitCSharpArgumentList(OParser.CSharpArgumentListContext ctx)
        {
            CSharpExpression item = GetNodeValue<CSharpExpression>(ctx.item);
            SetNodeValue(ctx, new CSharpExpressionList(item));
        }

        public override void ExitCSharpArgumentListItem(OParser.CSharpArgumentListItemContext ctx)
        {
            CSharpExpression item = GetNodeValue<CSharpExpression>(ctx.item);
            CSharpExpressionList items = GetNodeValue<CSharpExpressionList>(ctx.items);
            items.Add(item);
            SetNodeValue(ctx, items);
        }

        public override void ExitCSharpItemExpression(OParser.CSharpItemExpressionContext ctx)
        {
            CSharpExpression exp = GetNodeValue<CSharpExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }

        public override void ExitCSharpMethodExpression(OParser.CSharpMethodExpressionContext ctx)
        {
            CSharpExpression exp = GetNodeValue<CSharpExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }

        public override void ExitCSharpSelectorExpression(OParser.CSharpSelectorExpressionContext ctx)
        {
            CSharpExpression parent = GetNodeValue<CSharpExpression>(ctx.parent);
            CSharpSelectorExpression child = GetNodeValue<CSharpSelectorExpression>(ctx.child);
            child.SetParent(parent);
            SetNodeValue(ctx, child);
        }

        public override void ExitJavascript_category_binding(OParser.Javascript_category_bindingContext ctx)
        {
            StringBuilder sb = new StringBuilder();
            foreach (OParser.Javascript_identifierContext cx in ctx.javascript_identifier())
                sb.Append(cx.GetText());
            String identifier = sb.ToString();
            JavaScriptModule module = GetNodeValue<JavaScriptModule>(ctx.javascript_module());
            JavaScriptNativeCategoryBinding map = new JavaScriptNativeCategoryBinding(identifier, module);
            SetNodeValue(ctx, map);
        }

        public override void ExitJavascriptItemExpression(OParser.JavascriptItemExpressionContext ctx)
        {
            JavaScriptExpression exp = GetNodeValue<JavaScriptExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }

        public override void ExitJavascript_item_expression(OParser.Javascript_item_expressionContext ctx)
        {
            JavaScriptExpression exp = GetNodeValue<JavaScriptExpression>(ctx.exp);
            SetNodeValue(ctx, new JavaScriptItemExpression(exp));
        }

        public override void ExitJavascriptMemberExpression(OParser.JavascriptMemberExpressionContext ctx)
        {
            String name = ctx.name.GetText();
            SetNodeValue(ctx, new JavaScriptMemberExpression(name));
        }

        public override void ExitJavascript_primary_expression(OParser.Javascript_primary_expressionContext ctx)
        {
            JavaScriptExpression exp = GetNodeValue<JavaScriptExpression>(ctx.GetChild(0));
            SetNodeValue(ctx, exp);
        }

        public override void ExitJavascriptMethodExpression(OParser.JavascriptMethodExpressionContext ctx)
        {
            JavaScriptExpression method = GetNodeValue<JavaScriptExpression>(ctx.method);
            SetNodeValue(ctx, method);
        }

        public override void ExitJavascript_this_expression(OParser.Javascript_this_expressionContext ctx)
        {
            SetNodeValue(ctx, new JavaScriptThisExpression());
        }


        public override void ExitJavascript_identifier(OParser.Javascript_identifierContext ctx)
        {
            String name = ctx.GetText();
            SetNodeValue(ctx, name);
        }

        public override void ExitJavascript_method_expression(OParser.Javascript_method_expressionContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            JavaScriptMethodExpression method = new JavaScriptMethodExpression(name);
            JavaScriptExpressionList args = GetNodeValue<JavaScriptExpressionList>(ctx.args);
            method.setArguments(args);
            SetNodeValue(ctx, method);
        }

        public override void ExitCsharp_primary_expression(OParser.Csharp_primary_expressionContext ctx)
        {
            CSharpExpression exp = GetNodeValue<CSharpExpression>(ctx.GetChild(0));
            SetNodeValue(ctx, exp);
        }

        public override void ExitCsharp_this_expression(OParser.Csharp_this_expressionContext ctx)
        {
            SetNodeValue(ctx, new CSharpThisExpression());
        }


        public override void ExitJavascript_module(OParser.Javascript_moduleContext ctx)
        {
            List<String> ids = new List<String>();
            foreach (OParser.Javascript_identifierContext ic in ctx.javascript_identifier())
                ids.Add(ic.GetText());
            JavaScriptModule module = new JavaScriptModule(ids);
            SetNodeValue(ctx, module);
        }

        public override void ExitJavascript_native_statement(OParser.Javascript_native_statementContext ctx)
        {
            JavaScriptStatement stmt = GetNodeValue<JavaScriptStatement>(ctx.javascript_statement());
            JavaScriptModule module = GetNodeValue<JavaScriptModule>(ctx.javascript_module());
            stmt.setModule(module);
            SetNodeValue(ctx, stmt);
        }

        public override void ExitJavascript_new_expression(OParser.Javascript_new_expressionContext ctx)
        {
            JavaScriptMethodExpression method = GetNodeValue<JavaScriptMethodExpression>(ctx.javascript_method_expression());
            SetNodeValue(ctx, new JavaScriptNewExpression(method));
        }

        public override void ExitJavascriptArgumentList(OParser.JavascriptArgumentListContext ctx)
        {
            JavaScriptExpression exp = GetNodeValue<JavaScriptExpression>(ctx.item);
            JavaScriptExpressionList list = new JavaScriptExpressionList(exp);
            SetNodeValue(ctx, list);
        }

        public override void ExitJavascriptArgumentListItem(OParser.JavascriptArgumentListItemContext ctx)
        {
            JavaScriptExpression exp = GetNodeValue<JavaScriptExpression>(ctx.item);
            JavaScriptExpressionList list = GetNodeValue<JavaScriptExpressionList>(ctx.items);
            list.Add(exp);
            SetNodeValue(ctx, list);
        }

        public override void ExitJavascriptBooleanLiteral(OParser.JavascriptBooleanLiteralContext ctx)
        {
            string text = ctx.t.Text;
            SetNodeValue(ctx, new JavaScriptBooleanLiteral(text));
        }

        public override void ExitJavaScriptCategoryBinding(OParser.JavaScriptCategoryBindingContext ctx)
        {
            SetNodeValue(ctx, GetNodeValue<Object>(ctx.binding));
        }

        public override void ExitJavascriptCharacterLiteral(OParser.JavascriptCharacterLiteralContext ctx)
        {
            String text = ctx.t.Text;
            SetNodeValue(ctx, new JavaScriptCharacterLiteral(text));
        }

        public override void ExitJavascriptDecimalLiteral(OParser.JavascriptDecimalLiteralContext ctx)
        {
            String text = ctx.t.Text;
            SetNodeValue(ctx, new JavaScriptDecimalLiteral(text));
        }

        public override void ExitJavascript_identifier_expression(OParser.Javascript_identifier_expressionContext ctx)
        {
            String name = GetNodeValue<String>(ctx.name);
            SetNodeValue(ctx, new JavaScriptIdentifierExpression(name));
        }

        public override void ExitJavascriptIntegerLiteral(OParser.JavascriptIntegerLiteralContext ctx)
        {
            String text = ctx.t.Text;
            SetNodeValue(ctx, new JavaScriptIntegerLiteral(text));
        }

        public override void ExitJavaScriptNativeStatement(OParser.JavaScriptNativeStatementContext ctx)
        {
            JavaScriptStatement stmt = GetNodeValue<JavaScriptStatement>(ctx.javascript_native_statement());
            SetNodeValue(ctx, new JavaScriptNativeCall(stmt));
        }

        public override void ExitJavascriptPrimaryExpression(OParser.JavascriptPrimaryExpressionContext ctx)
        {
            JavaScriptExpression exp = GetNodeValue<JavaScriptExpression>(ctx.exp);
            SetNodeValue(ctx, exp);
        }

        public override void ExitJavascriptReturnStatement(OParser.JavascriptReturnStatementContext ctx)
        {
            JavaScriptExpression exp = GetNodeValue<JavaScriptExpression>(ctx.exp);
            SetNodeValue(ctx, new JavaScriptStatement(exp, true));
        }

        public override void ExitJavascriptSelectorExpression(OParser.JavascriptSelectorExpressionContext ctx)
        {
            JavaScriptExpression parent = GetNodeValue<JavaScriptExpression>(ctx.parent);
            JavaScriptSelectorExpression child = GetNodeValue<JavaScriptSelectorExpression>(ctx.child);
            child.setParent(parent);
            SetNodeValue(ctx, child);
        }

        public override void ExitJavascriptTextLiteral(OParser.JavascriptTextLiteralContext ctx)
        {
            String text = ctx.t.Text;
            SetNodeValue(ctx, new JavaScriptTextLiteral(text));
        }


        public override void ExitJavascriptStatement(OParser.JavascriptStatementContext ctx)
        {
            JavaScriptExpression exp = GetNodeValue<JavaScriptExpression>(ctx.exp);
            SetNodeValue(ctx, new JavaScriptStatement(exp, false));
        }

        public override void ExitLiteral_expression(OParser.Literal_expressionContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.GetChild(0));
            SetNodeValue(ctx, exp);
        }

        public override void ExitMethod_declaration(OParser.Method_declarationContext ctx)
        {
            IDeclaration exp = GetNodeValue<IDeclaration>(ctx.GetChild(0));
            SetNodeValue(ctx, exp);
        }

        public override void ExitMethod_identifier(OParser.Method_identifierContext ctx)
        {
            Object exp = GetNodeValue<Object>(ctx.GetChild(0));
            SetNodeValue(ctx, exp);
        }

        public override void ExitOperator_argument(OParser.Operator_argumentContext ctx)
        {
            IParameter exp = GetNodeValue<IParameter>(ctx.GetChild(0));
            SetNodeValue(ctx, exp);
        }

        public override void ExitCategory_or_any_type(OParser.Category_or_any_typeContext ctx)
        {
            IType exp = GetNodeValue<IType>(ctx.GetChild(0));
            SetNodeValue(ctx, exp);
        }

        public override void ExitCollection_literal(OParser.Collection_literalContext ctx)
        {
            IExpression exp = GetNodeValue<IExpression>(ctx.GetChild(0));
            SetNodeValue(ctx, exp);
        }

        public override void ExitCursorType(OParser.CursorTypeContext context)
        {
            throw new NotImplementedException();
        }

        public override void ExitEnum_declaration(OParser.Enum_declarationContext ctx)
        {
            IDeclaration exp = GetNodeValue<IDeclaration>(ctx.GetChild(0));
            SetNodeValue(ctx, exp);
        }

        public override void ExitSymbol_list(OParser.Symbol_listContext context)
        {
            throw new NotImplementedException();
        }
    }
}
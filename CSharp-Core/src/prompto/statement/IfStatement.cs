using prompto.error;
using prompto.runtime;
using System.Collections.Generic;
using System;
using BooleanValue = prompto.value.BooleanValue;
using prompto.parser;
using prompto.type;
using prompto.expression;
using prompto.utils;
using prompto.value;

namespace prompto.statement
{

    public class IfStatement : BaseStatement
    {

        IfElementList elements = new IfElementList();

        public IfStatement(IExpression condition, StatementList statements)
        {
            elements.Add(new IfElement(condition, statements));
        }

        public IfStatement(IExpression condition, StatementList statements, IfElementList elseIfs, StatementList elseStmts)
        {
            elements.Add(new IfElement(condition, statements));
            if (elseIfs != null)
                elements.AddRange(elseIfs);
            if (elseStmts != null)
                elements.Add(new IfElement(null, elseStmts));
        }

        
        public override void ToDialect(CodeWriter writer)
        {
            switch (writer.getDialect())
            {
                case Dialect.E:
                    ToEDialect(writer);
                    break;
                case Dialect.O:
                    ToODialect(writer);
                    break;
                case Dialect.M:
                    toMDialect(writer);
                    break;
            }
        }


        private void toMDialect(CodeWriter writer)
        {
            bool first = true;
            foreach (IfElement elem in elements)
            {
                if (!first)
                    writer.append("else ");
                elem.ToDialect(writer);
                first = false;
            }
            writer.newLine();
        }


        private void ToODialect(CodeWriter writer)
        {
            bool first = true;
            bool curly = false;
            foreach (IfElement elem in elements)
            {
                if (!first)
                {
                    if (curly)
                        writer.append(" ");
                    writer.append("else ");
                }
                curly = elem.statements.Count > 1;
                elem.ToDialect(writer);
                first = false;
            }
            writer.newLine();
        }


        private void ToEDialect(CodeWriter writer)
        {
            bool first = true;
            foreach (IfElement elem in elements)
            {
                if (!first)
                    writer.append("else ");
                elem.ToDialect(writer);
                first = false;
            }
            writer.newLine();
        }

        public void addAdditional(IExpression condition, StatementList instructions)
        {
            elements.Add(new IfElement(condition, instructions));
        }

        public void setFinal(StatementList instructions)
        {
            elements.Add(new IfElement(null, instructions));
        }


        public override IType check(Context context)
        {
            TypeMap types = new TypeMap();
            foreach (IfElement element in elements)
            {
                IType type = element.check(context);
                if(type != VoidType.Instance)
                    types.add(type);
            }
            return types.inferType(context);
        }


        public override IValue interpret(Context context)
        {
            foreach (IfElement element in elements)
            {
                IExpression condition = element.getCondition();
                IValue test = condition == null ? BooleanValue.TRUE : condition.interpret(context);
                if (test == BooleanValue.TRUE)
                    return element.interpret(context);
            }
            return null;
        }


        public override bool CanReturn
        {
            get
            {
                return true;
            }
        }


    }

    public class IfElement : BaseStatement
    {

        IExpression condition;
        public StatementList statements;

        public IfElement(IExpression condition, StatementList statements)
        {
            this.condition = condition;
            this.statements = statements;
        }

        
        public override void ToDialect(CodeWriter writer)
        {
            switch (writer.getDialect())
            {
                case Dialect.E:
                    ToEDialect(writer);
                    break;
                case Dialect.O:
                    ToODialect(writer);
                    break;
                case Dialect.M:
                    toPDialect(writer);
                    break;
            }
        }

        public void toPDialect(CodeWriter writer)
        {
            ToEDialect(writer);
        }

        public void ToEDialect(CodeWriter writer)
        {
            Context context = writer.getContext();
            if (condition != null)
            {
                writer.append("if ");
                condition.ToDialect(writer);
                context = downCast(context, false);
                if (context != writer.getContext())
                    writer = writer.newChildWriter(context);
            }
            writer.append(":\n");
            writer.indent();
            statements.ToDialect(writer);
            writer.dedent();
        }

        public void ToODialect(CodeWriter writer)
        {
            Context context = writer.getContext();
            if (condition != null)
            {
                writer.append("if (");
                condition.ToDialect(writer);
                writer.append(") ");
                context = downCast(context, false);
                if (context != writer.getContext())
                    writer = writer.newChildWriter(context);
            }
            bool curly = needsCurlyBraces();
            if (curly)
                writer.append("{\n");
            else
                writer.newLine();
            writer.indent();
            statements.ToDialect(writer);
            writer.dedent();
            if (curly)
                writer.append("}");
        }

        bool needsCurlyBraces()
        {
            if (statements == null)
                return false;
            if (statements.Count > 1)
                return true;
            else
                return !statements[0].IsSimple;

        }

        public IExpression getCondition()
        {
            return condition;
        }

        public StatementList getInstructions()
        {
            return statements;
        }

        override
        public IType check(Context context)
        {
            if (condition != null)
            {
                IType cond = condition.check(context);
                if (cond != BooleanType.Instance)
                    throw new SyntaxError("Expected a bool condition!");
            }
            context = downCast(context, false);
            return statements.check(context, null);
        }

        override
        public IValue interpret(Context context)
        {
            context = downCast(context, true);
            return statements.interpret(context);
        }

        private Context downCast(Context context, bool setValue)
        {
            Context parent = context;
            if (condition is EqualsExpression)
                context = ((EqualsExpression)condition).downcast(context, setValue);
            context = parent != context ? context : context.newChildContext();
            return context;
        }

    }

    public class IfElementList : List<IfElement>
    {

        public IfElementList()
        {

        }

        public IfElementList(IfElement elem)
        {
            this.Add(elem);
        }


    }

}
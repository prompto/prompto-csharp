using System;
using prompto.runtime;
using prompto.parser;
using prompto.statement;
using prompto.type;
using prompto.utils;
using prompto.value;
using prompto.param;
using prompto.error;

namespace prompto.declaration
{

    public class ConcreteMethodDeclaration : BaseMethodDeclaration
    {

        protected StatementList statements;
        bool beingChecked;

        public ConcreteMethodDeclaration(String name, ParameterList parameters, IType returnType, StatementList statements)
             : base(name, parameters, returnType)
        {
            if (statements == null)
                statements = new StatementList();
            this.statements = statements;
            foreach (IStatement s in statements)
            {
                if (s is BaseDeclarationStatement)
                    ((BaseDeclarationStatement)s).getDeclaration().ClosureOf = this;
            }
        }

        
        public override void ToDialect(CodeWriter writer)
        {
            if (writer.isGlobalContext())
                writer = writer.newLocalWriter();
            registerParameters(writer.getContext());
            switch (writer.getDialect())
            {
                case Dialect.E:
                    ToEDialect(writer);
                    break;
                case Dialect.O:
                    ToODialect(writer);
                    break;
                case Dialect.M:
                    ToMDialect(writer);
                    break;
            }
        }

        protected virtual void ToMDialect(CodeWriter writer)
        {
            writer.append("def ");
            writer.append(name);
            writer.append(" (");
            parameters.ToDialect(writer);
            writer.append(")");
            if (returnType != null && returnType != VoidType.Instance)
            {
                writer.append("->");
                returnType.ToDialect(writer);
            }
            writer.append(":\n");
            writer.indent();
            statements.ToDialect(writer);
            writer.dedent();
        }

        protected virtual void ToEDialect(CodeWriter writer)
        {
            writer.append("define ");
            writer.append(name);
            writer.append(" as method ");
            parameters.ToDialect(writer);
            if (returnType != null && returnType != VoidType.Instance)
            {
                writer.append("returning ");
                returnType.ToDialect(writer);
                writer.append(" ");
            }
            writer.append("doing:\n");
            writer.indent();
            statements.ToDialect(writer);
            writer.dedent();
        }

        protected virtual void ToODialect(CodeWriter writer)
        {
            if (returnType != null && returnType != VoidType.Instance)
            {
                returnType.ToDialect(writer);
                writer.append(" ");
            }
            writer.append("method ");
            writer.append(name);
            writer.append(" (");
            parameters.ToDialect(writer);
            writer.append(") {\n");
            writer.indent();
            statements.ToDialect(writer);
            writer.dedent();
            writer.append("}\n");
        }

        public StatementList getStatements()
        {
            return statements;
        }


        public override IType check(Context context, bool isStart)
        {
            if (canBeChecked(context, isStart))
                return recursiveCheck(context, isStart);
            else
                return VoidType.Instance;
        }

        private bool canBeChecked(Context context, bool isStart)
        {
            if (isStart)
                return !isTemplate();
            else
                return true;
        }

        public override bool isTemplate()
        {
            // if at least one argument is 'Code'
            if (parameters == null)
                return false;
            foreach (IParameter arg in parameters)
            {
                if (arg is CodeParameter)
                    return true;
            }
            return false;
        }


       protected virtual IType recursiveCheck(Context context, bool isStart)
        {
            if(beingChecked)
            {
                if (returnType != null)
                    return returnType;
                else
                    throw new SyntaxError("Cannot check recursive method " + this.name + " without a return type!");
            } else
            {
                beingChecked = true;
                try
                {
                    return fullCheck(context, isStart);
                }
                finally
                {
                    beingChecked = false;
                }
            }
        }

       protected virtual IType fullCheck(Context context, bool isStart)
        {
            if (isStart)
            {
                context = context.newLocalContext();
                registerParameters(context);
            }
            if (parameters != null)
                parameters.check(context);
            return statements.check(context, returnType);
        }

        public override IType checkChild(Context context)
        {
            if (parameters != null)
                parameters.check(context);
            Context child = context.newChildContext();
            registerParameters(child);
            return checkStatements(child);
        }

        protected virtual IType checkStatements(Context context)
        {
            return statements.check(context, returnType);
        }


        public override IValue interpret(Context context)
        {
            context.enterMethod(this);
            try
            {
                return statements.interpret(context);
            }
            catch(SyntaxError e)
            {
                e.Suffix = " in method '" + this.name + "'";
                throw e;
            }
            finally
            {
                context.leaveMethod(this);
            }
        }

        override
        public bool isEligibleAsMain()
        {
            if (parameters.Count == 0)
                return true;
            if (parameters.Count == 1)
            {
                IParameter arg = parameters[0];
                if (arg is CategoryParameter)
                {
                    IType type = ((CategoryParameter)arg).getType();
                    if (type is DictType)
                        return ((DictType)type).GetItemType() == TextType.Instance;
                }
            }
            return base.isEligibleAsMain();
        }

    }

}
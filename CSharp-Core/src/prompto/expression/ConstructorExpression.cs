using prompto.type;
using prompto.grammar;
using System;
using prompto.parser;
using prompto.runtime;
using prompto.declaration;
using prompto.error;
using prompto.value;
using prompto.utils;
using prompto.param;

namespace prompto.expression
{

    public class ConstructorExpression : BaseExpression, IExpression
    {

        CategoryType type;
        IExpression copyFrom;
        ArgumentList arguments;
        bool xchecked;

        public ConstructorExpression(CategoryType type, IExpression copyFrom, ArgumentList arguments, bool xchecked)
        {
            this.type = type;
            this.copyFrom = copyFrom;
            this.arguments = arguments;
            this.xchecked = xchecked;
        }

        public CategoryType getType()
        {
            return type;
        }

        public ArgumentList getArguments()
        {
            return arguments;
        }

        public void setCopyFrom(IExpression copyFrom)
        {
            this.copyFrom = copyFrom;
        }

        public IExpression getCopyFrom()
        {
            return copyFrom;
        }

        public override void ToDialect(CodeWriter writer)
        {
            Context context = writer.getContext();
            CategoryDeclaration cd = context.getRegisteredDeclaration<CategoryDeclaration>(this.type.GetTypeName());
            if (cd == null)
                throw new SyntaxError("Unknown category " + this.type.GetTypeName());
            checkFirstHomonym(context, cd);
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

        void checkFirstHomonym(Context context, CategoryDeclaration decl)
        {
            if (xchecked)
                return;
            if (arguments != null && arguments.Count > 0)
                checkFirstHomonym(context, decl, arguments[0]);
            xchecked = true;
        }

        void checkFirstHomonym(Context context, CategoryDeclaration decl, Argument argument)
        {
            if (argument.Parameter == null)
            {
                IExpression exp = argument.getExpression();
                // when coming from UnresolvedCall, could be an homonym
                string name = null;
                if (exp is UnresolvedIdentifier)
                    name = ((UnresolvedIdentifier)exp).getName();
                else if (exp is InstanceExpression)
                    name = ((InstanceExpression)exp).getName();
                if (name != null && decl.hasAttribute(context, name))
                {
                    // convert expression to name to avoid translation issues
                    argument.Parameter = new AttributeParameter(name);
                    argument.Expression = null;
                }
            }
        }

        private void toPDialect(CodeWriter writer)
        {
            ToODialect(writer);
        }

        private void ToODialect(CodeWriter writer)
        {
            type.ToDialect(writer);
            ArgumentList arguments = new ArgumentList();
            if (copyFrom != null)
                arguments.Add(new Argument(new AttributeParameter("from"), copyFrom));
            if (this.arguments != null)
                arguments.AddRange(this.arguments);
            arguments.ToDialect(writer);
        }

        private void ToEDialect(CodeWriter writer)
        {
            type.ToDialect(writer);
            if (copyFrom != null)
            {
                writer.append(" from ");
                writer.append(copyFrom.ToString());
                if (arguments != null && arguments.Count > 0)
                    writer.append(",");
            }
            if (arguments != null)
                arguments.ToDialect(writer);
        }

        public override IType check(Context context)
        {
            CategoryDeclaration cd = context.getRegisteredDeclaration<CategoryDeclaration>(this.type.GetTypeName());
            if (cd == null)
                throw new SyntaxError("Unknown category " + this.type.GetTypeName());
            checkFirstHomonym(context, cd);
            cd.checkConstructorContext(context);
            if (copyFrom != null)
            {
                IType cft = copyFrom.check(context);
                if (!(cft is CategoryType) && (cft != DocumentType.Instance))
                    throw new SyntaxError("Cannot copy from " + cft.GetTypeName());
            }
            if (arguments != null)
            {
                foreach (Argument argument in arguments)
                {
                    if (!cd.hasAttribute(context, argument.GetName()))
                        throw new SyntaxError("\"" + argument.GetName() +
                            "\" is not an attribute of " + type.GetTypeName());
                    argument.check(context);
                }
            }
            return cd.GetIType(context);
        }

        public override IValue interpret(Context context)
        {
            CategoryDeclaration cd = context.getRegisteredDeclaration<CategoryDeclaration>(this.type.GetTypeName());
            if (cd == null)
                throw new SyntaxError("Unknown category " + this.type.GetTypeName());
            checkFirstHomonym(context, cd);
            IInstance instance = type.newInstance(context);
            instance.setMutable(true);
            try
            {
                if (copyFrom != null)
                {
                    Object copyObj = copyFrom.interpret(context);
                    if (copyObj is IInstance)
                    {
                        IInstance copyInstance = (IInstance)copyObj;
                        foreach (String name in copyInstance.GetMemberNames())
                        {
                            if (name == "dbId")
                                continue;
                            else if (cd.hasAttribute(context, name))
                            {
                                IValue value = copyInstance.GetMemberValue(context, name, false);
                                if (value != null && value.IsMutable() && !this.type.Mutable)
                                    throw new NotMutableError();
                                instance.SetMemberValue(context, name, value);
                            }
                        }
                    }
                    else if (copyObj is Document)
                    {
                        Document copyDoc = (Document)copyObj;
                        foreach (String name in copyDoc.GetMemberNames())
                        {
                            if (name == "dbId")
                                continue;
                            else if (cd.hasAttribute(context, name))
                            {
                                IValue value = copyDoc.GetMemberValue(context, name, false);
                                if (value != null && value.IsMutable() && !this.type.Mutable)
                                    throw new NotMutableError();
                                // TODO convert to attribute type, see Java version
                                instance.SetMemberValue(context, name, value);
                            }
                        }
                    }
                }
                if (arguments != null)
                {
                    foreach (Argument argument in arguments)
                    {
                        IValue value = argument.getExpression().interpret(context);
                        if (value != null && value.IsMutable() && !this.type.Mutable)
                            throw new NotMutableError();
                        instance.SetMemberValue(context, argument.GetName(), value);
                    }
                }
            }
            finally
            {
                instance.setMutable(this.type.Mutable);
            }
            return instance;
        }

    }
}
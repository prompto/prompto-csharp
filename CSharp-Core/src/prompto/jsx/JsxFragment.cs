using System;
using System.Collections.Generic;
using prompto.declaration;
using prompto.error;
using prompto.runtime;
using prompto.type;
using prompto.utils;
using prompto.value;

namespace prompto.jsx
{
    public class JsxFragment : IJsxExpression
    {
        String openingSuite;
        List<IJsxExpression> children;

        public JsxFragment(String openingSuite)
        {
            this.openingSuite = openingSuite;
        }

        public void setChildren(List<IJsxExpression> children)
        {
            this.children = children;
        }

        public void ToDialect(CodeWriter writer)
        {
            writer.append("<>");
            if (openingSuite != null)
                writer.appendRaw(openingSuite);
            if (children != null)
                foreach (IJsxExpression child in children)
                    child.ToDialect(writer);
            writer.append("</>");
        }

        public void ParentToDialect(CodeWriter writer)
        {
            ToDialect(writer);
        }

        public IType check(Context context)
        {
            if (children != null)
                foreach (IJsxExpression child in children)
                    child.check(context);
            return JsxType.Instance;
        }

        public virtual IType checkReference(Context context)
        {
            return check(context);
        }

        public AttributeDeclaration CheckAttribute(Context context)
        {
            throw new SyntaxError("Expected an attribute, found: " + this.ToString());
        }

        public IValue interpret(Context context)
        {
            return new JsxValue(this);
        }

        public virtual IValue interpretReference(Context context)
        {
            return interpret(context);
        }

    }
}

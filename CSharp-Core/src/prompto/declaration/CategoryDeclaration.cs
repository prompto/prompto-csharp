using System;
using prompto.error;
using prompto.runtime;
using prompto.type;
using prompto.grammar;
using prompto.value;
using prompto.utils;
using prompto.parser;
using prompto.store;
using System.Collections.Generic;
using prompto.statement;

namespace prompto.declaration
{

    public abstract class CategoryDeclaration : BaseDeclaration
    {

        protected IWidgetDeclaration widget;
        protected IdentifierList attributes;

        public CategoryDeclaration(String name)
            : base(name)
        {
        }

        public CategoryDeclaration(String name, IdentifierList attributes)
            : base(name)
        {
            this.attributes = attributes;
        }

        public bool Storable { get; set; }

        public void setAttributes(IdentifierList attributes)
        {
            this.attributes = attributes;
        }

        public IdentifierList GetLocalAttributes()
        {
            return attributes;
        }


        public virtual MethodDeclarationList GetLocalMethods()
        {
            throw new NotImplementedException(); // TODO abstract
        }

        public virtual bool IsAWidget(Context context)
        {
            return false;
        }


        public virtual IWidgetDeclaration AsWidget()
        {
            if (widget == null)
                widget = new WrappingWidgetDeclaration(this);
            return widget;
        }

        public virtual HashSet<String> GetAllAttributes(Context context)
        {
            if (attributes != null)
                return new HashSet<String>(attributes);
            else
                return null;
        }

        public List<Annotation> GetAllAnnotations(Context context)
        {
            List<Annotation> result = new List<Annotation>();
            IdentifierList derivedFrom = getDerivedFrom();
            if(derivedFrom!=null)
            {
                foreach(String name in derivedFrom)
                {
                    CategoryDeclaration decl = context.getRegisteredDeclaration<CategoryDeclaration>(name);
                    if (decl != null)
                        result.AddRange(decl.GetAllAnnotations(context));
                }
            }
            if (Annotations != null)
                result.AddRange(Annotations);
            return result;
        }

        public override void register(Context context)
        {
            context.registerDeclaration(this);
            registerMethods(context);
        }

        protected abstract void registerMethods(Context context);
        public virtual MethodDeclarationMap getMemberMethods(Context context, string name)
        {
            throw new NotImplementedException();
        }


        public override IType check(Context context)
        {
            if (attributes != null)
                foreach (String attribute in attributes)
                {
                    AttributeDeclaration ad = context.getRegisteredDeclaration<AttributeDeclaration>(attribute);
                    if (ad == null && attribute != "text")
                        throw new SyntaxError("Unknown attribute: \"" + attribute + "\"");
                }
            return new CategoryType(this.GetName());
        }


        public override IType GetIType(Context context)
        {
            return new CategoryType(name);
        }

        public virtual bool hasAttribute(Context context, String name)
        {
            if (name == "dbId")
                return this.Storable;
            else
                return attributes != null && attributes.Contains(name);
        }

        public virtual bool hasMethod(Context context, String name)
        {
            return false;
        }

        public virtual bool isDerivedFrom(Context context, CategoryType categoryType)
        {
            return false;
        }

        public virtual IdentifierList getDerivedFrom()
        {
            return null;
        }

        public abstract IInstance newInstance(Context context);

        public IInstance newInstance(Context context, IStored stored)
        {
            IInstance instance = newInstance(context);
            instance.setMutable(true);
            try
            {
                PopulateInstance(context, stored, instance);
            }
            finally
            {
                instance.setMutable(false);
            }
            return instance;
        }

        void PopulateInstance(Context context, IStored stored, IInstance instance)
        {
            SetDbId(context, instance, stored.DbId);
            foreach (String name in GetAllAttributes(context))
                PopulateMember(context, stored, instance, name);
            if (instance.getStorable() != null)
                instance.getStorable().Dirty = false;
        }

        private void SetDbId(Context context, IInstance instance, object dbId)
        {
            if (instance is ConcreteInstance)
                ((ConcreteInstance)instance).SetDbId(dbId);
            else
                throw new InvalidOperationException("SetDbId on native instance");
        }

        void PopulateMember(Context context, IStored stored, IInstance instance, String name)
        {
            AttributeDeclaration decl = context.getRegisteredDeclaration<AttributeDeclaration>(name);
            if (!decl.Storable)
                return;
            if(stored.HasData(name))
            {
                Object data = stored.GetData(name);
                IValue value = decl.getIType().ConvertCSharpValueToIValue(context, data);
                instance.SetMemberValue(context, name, value);
            }
        }

        public virtual void checkConstructorContext(Context context)
        {
            // nothing to do
        }


        public override void ToDialect(CodeWriter writer)
        {
            CategoryType type = (CategoryType)GetIType(writer.getContext());
            writer = writer.newInstanceWriter(type);
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

        public void processAnnotations(Context context, bool processDerivedFrom)
        {
            if (processDerivedFrom)
            {
                IdentifierList derivedFrom = getDerivedFrom();
                if (derivedFrom != null)
                {
                    foreach (String name in derivedFrom)
                    {
                        CategoryDeclaration decl = context.getRegisteredDeclaration<CategoryDeclaration>(name);
                        if(decl!=null)
                            decl.processAnnotations(context, true);
                    }
                }
                if (Annotations != null)
                {
                    foreach (Annotation a in Annotations)
                    {
                        a.processCategory(context, this);
                    }
                }
            }
        }


        protected abstract void ToEDialect(CodeWriter writer);

        protected virtual void protoToEDialect(CodeWriter writer, bool hasMethods, bool hasBindings)
        {
            bool hasAttributes = attributes != null && attributes.Count > 0;
            writer.append("define ");
            writer.append(name);
            writer.append(" as ");
            if (this.Storable)
                writer.append("storable ");
            categoryTypeToEDialect(writer);
            if (hasAttributes)
            {
                if (attributes.Count == 1)
                    writer.append(" with attribute ");
                else
                    writer.append(" with attributes ");
                attributes.ToDialect(writer, true);
            }
            if (hasMethods)
            {
                if (hasAttributes)
                    writer.append(", and methods:");
                else
                    writer.append(" with methods:");
            }
            else if (hasBindings)
            {
                if (hasAttributes)
                    writer.append(", and bindings:");
                else
                    writer.append(" with bindings:");
            }
            writer.newLine();
        }

        protected virtual void methodsToEDialect(CodeWriter writer, MethodDeclarationList methods)
        {
            writer.indent();
            foreach (IDeclaration decl in methods)
            {
                writer.newLine();
                if (decl.Comments != null)
                    foreach (CommentStatement comment in decl.Comments)
                        comment.ToDialect(writer);
                if (decl.Annotations != null)
                    foreach (Annotation annotation in decl.Annotations)
                        annotation.ToDialect(writer);
                CodeWriter w = writer.newMemberWriter();
                decl.ToDialect(w);
            }
            writer.dedent();
        }


        protected abstract void categoryTypeToEDialect(CodeWriter writer);

        protected abstract void ToODialect(CodeWriter writer);

        protected virtual void ToODialect(CodeWriter writer, bool hasBody)
        {
            categoryTypeToODialect(writer);
            writer.append(" ");
            writer.append(name);
            if (attributes != null)
            {
                writer.append('(');
                attributes.ToDialect(writer, true);
                writer.append(')');
            }
            categoryExtensionToODialect(writer);
            if (hasBody)
            {
                writer.append(" {\n");
                writer.newLine();
                writer.indent();
                bodyToODialect(writer);
                writer.dedent();
                writer.append('}');
                writer.newLine();
            }
            else
                writer.append(';');
        }

        protected virtual void methodsToODialect(CodeWriter writer, MethodDeclarationList methods)
        {
            foreach (IDeclaration decl in methods)
            {
                if (decl.Comments != null)
                    foreach (CommentStatement comment in decl.Comments)
                        comment.ToDialect(writer);
                if (decl.Annotations != null)
                    foreach (Annotation annotation in decl.Annotations)
                        annotation.ToDialect(writer);
                CodeWriter w = writer.newMemberWriter();
                decl.ToDialect(w);
                w.newLine();
            }
        }

        protected abstract void categoryTypeToODialect(CodeWriter writer);

        protected virtual void categoryExtensionToODialect(CodeWriter writer)
        {
            // by default no extension
        }

        protected abstract void bodyToODialect(CodeWriter writer);

        protected abstract void ToMDialect(CodeWriter writer);

        protected void protoToMDialect(CodeWriter writer, IdentifierList derivedFrom)
        {
            if (this.Storable)
                writer.append("storable ");
            categoryTypeToMDialect(writer);
            writer.append(" ");
            writer.append(name);
            writer.append("(");
            if (derivedFrom != null)
            {
                derivedFrom.ToDialect(writer, false);
                if (attributes != null)
                    writer.append(", ");
            }
            if (attributes != null)
                attributes.ToDialect(writer, false);
            writer.append("):\n");
            writer.newLine();
        }

        protected abstract void categoryTypeToMDialect(CodeWriter writer);


    }

}

using System;
using System.Text;
using presto.error;
using System.Collections.Generic;
using presto.runtime;
using presto.parser;
using presto.type;
using presto.value;
using presto.grammar;
using presto.utils;

namespace presto.declaration
{

    public class ConcreteCategoryDeclaration : CategoryDeclaration
    {

        protected IdentifierList derivedFrom;
        CategoryMethodDeclarationList methods;
        Dictionary<String, IDeclaration> methodsMap = null;

        public ConcreteCategoryDeclaration(String name)
            : base(name)
        {
        }

        public ConcreteCategoryDeclaration(String name, IdentifierList attrs, IdentifierList derivedFrom, CategoryMethodDeclarationList methods)
            : base(name, attrs)
        {
            this.derivedFrom = derivedFrom;
			this.methods = methods!=null ? methods : new CategoryMethodDeclarationList();
        }

        public void setDerivedFrom(IdentifierList derivedFrom)
        {
            this.derivedFrom = derivedFrom;
        }

        override
        public IdentifierList getDerivedFrom()
        {
            return derivedFrom;
        }

        public void setMethods(CategoryMethodDeclarationList methods)
        {
			this.methods = methods;
        }

        public CategoryMethodDeclarationList getMethods()
        {
			return methods;
        }

		protected override void toEDialect(CodeWriter writer) {
			bool hasMethods = methods!=null && methods.Count>0;
			protoToEDialect(writer, hasMethods, false); // no mappings
			if(hasMethods)
				methodsToEDialect(writer, methods);
		}

		protected override void categoryTypeToEDialect(CodeWriter writer) {
			if(derivedFrom==null)
				writer.append("category");
			else
				derivedFrom.ToDialect(writer, true);
		}


		protected override void toODialect(CodeWriter writer) {
			bool hasMethods = methods!=null && methods.Count>0;
			toODialect(writer, hasMethods);
		}

		protected override void categoryTypeToODialect(CodeWriter writer) {
			writer.append("category");
		}

		protected override void categoryExtensionToODialect(CodeWriter writer) {
			if(derivedFrom!=null) {
				writer.append(" extends ");
				derivedFrom.ToDialect(writer, true);
			}
		}

		protected override void bodyToODialect(CodeWriter writer) {
			foreach(IDeclaration decl in methods) {
				decl.ToDialect(writer);
				writer.newLine();
			}
		}

		protected override void toPDialect(CodeWriter writer) {
			protoToPDialect(writer, derivedFrom);
			methodsToPDialect(writer);
		}

		protected override void categoryTypeToPDialect(CodeWriter writer) {
			writer.append("class");
		}

		private void methodsToPDialect(CodeWriter writer) {
			writer.indent();
			if(methods==null || methods.Count==0)
				writer.append("pass\n");
			else foreach(IDeclaration decl in methods) {
					decl.ToDialect(writer);
					writer.newLine();
				}
			writer.dedent();
		}


        override
        public bool hasAttribute(Context context, String name)
        {
            if (base.hasAttribute(context, name))
                return true;
            if (hasDerivedAttribute(context, name))
                return true;
            return false;
        }

        private bool hasDerivedAttribute(Context context, String name)
        {
            if (derivedFrom == null)
                return false;
            foreach (String ancestor in derivedFrom)
            {
                if (ancestorHasAttribute(ancestor, context, name))
                    return true;
            }
            return false;
        }

        private static bool ancestorHasAttribute(String ancestor, Context context, String name)
        {
            CategoryDeclaration actual = context.getRegisteredDeclaration<CategoryDeclaration>(ancestor);
            if (actual == null)
                return false;
            return actual.hasAttribute(context, name);
        }

        override
        public IType check(Context context)
        {
            checkDerived(context);
            checkMethods(context);
            return base.check(context);
        }

        private void checkMethods(Context context)
        {
            if (methodsMap == null)
            {
				methodsMap = new Dictionary<String, IDeclaration> ();
                foreach (ICategoryMethodDeclaration method in methods)
                {
                    register(method, context);
                }
            }
        }


        private void register(ICategoryMethodDeclaration method, Context context)
        {
            IDeclaration actual;
            if (method is SetterMethodDeclaration)
            {
				if (methodsMap.TryGetValue("setter:" + method.getName(), out actual))
                    throw new SyntaxError("Duplicate setter: \"" + method.getName() + "\"");
				methodsMap["setter:" + method.getName()] = method;
            }
            else if (method is GetterMethodDeclaration)
            {
				if (methodsMap.TryGetValue("getter:" + method.getName(), out actual))
                    throw new SyntaxError("Duplicate getter: \"" + method.getName() + "\"");
				methodsMap["getter:" + method.getName()] = method;
            }
            else
            {
				if (!methodsMap.TryGetValue(method.getName(), out actual))
                {
                    actual = new MethodDeclarationMap(method.getName());
					methodsMap[method.getName()] = (MethodDeclarationMap)actual;
                }
                ((MethodDeclarationMap)actual).register(method, context);
            }
            method.check(this, context);
        }

        private void checkDerived(Context context)
        {
            if (derivedFrom != null) foreach (String category in derivedFrom)
                {
                    ConcreteCategoryDeclaration cd = context.getRegisteredDeclaration<ConcreteCategoryDeclaration>(category);
                    if (cd == null)
                        throw new SyntaxError("Unknown category: \"" + category + "\"");
                }
        }

        override
        public bool isDerivedFrom(Context context, CategoryType categoryType)
        {
            if (derivedFrom == null)
                return false;
            foreach (String ancestor in derivedFrom)
            {
                if (ancestor.Equals(categoryType.getName()))
                    return true;
                if (isAncestorDerivedFrom(ancestor, context, categoryType))
                    return true;
            }
            return false;
        }

        private static bool isAncestorDerivedFrom(String ancestor, Context context, CategoryType categoryType)
        {
            IDeclaration actual = context.getRegisteredDeclaration<IDeclaration>(ancestor);
            if (actual == null || !(actual is CategoryDeclaration))
                return false;
            CategoryDeclaration cd = (CategoryDeclaration)actual;
            return cd.isDerivedFrom(context, categoryType);
        }

        override
        public IInstance newInstance()
        {
            return new ConcreteInstance(this);
        }

        public GetterMethodDeclaration findGetter(Context context, String attrName)
        {
			if (methodsMap == null)
                return null;
            IDeclaration method;
			if (methodsMap.TryGetValue("getter:" + attrName, out method))
            {
                if (method is GetterMethodDeclaration)
                    return (GetterMethodDeclaration)method;
                else
                    throw new SyntaxError("Not a getter method!");
            }
            return findDerivedGetter(context, attrName);
        }

        private GetterMethodDeclaration findDerivedGetter(Context context, String attrName)
        {
            if (derivedFrom == null)
                return null;
            foreach (String ancestor in derivedFrom)
            {
                GetterMethodDeclaration method = findAncestorGetter(ancestor, context, attrName);
                if (method != null)
                    return method;
            }
            return null;
        }

        private static GetterMethodDeclaration findAncestorGetter(String ancestor, Context context, String attrName)
        {
            IDeclaration actual = context.getRegisteredDeclaration<IDeclaration>(ancestor);
            if (actual == null || !(actual is ConcreteCategoryDeclaration))
                return null;
            ConcreteCategoryDeclaration cd = (ConcreteCategoryDeclaration)actual;
            return cd.findGetter(context, attrName);
        }

        public SetterMethodDeclaration findSetter(Context context, String attrName)
        {
			if (methodsMap == null)
                return null;
            IDeclaration method;
			if (methodsMap.TryGetValue("setter:" + attrName, out method))
            {
                if (method is SetterMethodDeclaration)
                    return (SetterMethodDeclaration)method;
                else
                    throw new SyntaxError("Not a setter method!");
            }
            return findDerivedSetter(context, attrName);
        }

        private SetterMethodDeclaration findDerivedSetter(Context context, String attrName)
        {
            if (derivedFrom == null)
                return null;
            foreach (String ancestor in derivedFrom)
            {
                SetterMethodDeclaration method = findAncestorSetter(ancestor, context, attrName);
                if (method != null)
                    return method;
            }
            return null;
        }

        private static SetterMethodDeclaration findAncestorSetter(String ancestor, Context context, String attrName)
        {
            IDeclaration actual = context.getRegisteredDeclaration<IDeclaration>(ancestor);
            if (actual == null || !(actual is ConcreteCategoryDeclaration))
                return null;
            ConcreteCategoryDeclaration cd = (ConcreteCategoryDeclaration)actual;
            return cd.findSetter(context, attrName);
        }

        public Dictionary<String, IMethodDeclaration>.ValueCollection findMemberMethods(Context context, String name)
        {
            MethodDeclarationMap result = new MethodDeclarationMap(name);
            registerMemberMethods(context, result);
            return result.Values;
        }


        private void registerMemberMethods(Context context, MethodDeclarationMap result)
        {
            registerThisMemberMethods(context, result);
            registerDerivedMemberMethods(context, result);
        }


        private void registerThisMemberMethods(Context context, MethodDeclarationMap result)
        {
			if (methodsMap == null)
                return;
            IDeclaration actual;
			if (!methodsMap.TryGetValue(result.getName(), out actual))
                return;
            if (!(actual is MethodDeclarationMap))
                throw new SyntaxError("Not a member method!");
            foreach (IMethodDeclaration method in ((MethodDeclarationMap)actual).Values)
                result.registerIfMissing(method, context);
        }

        private void registerDerivedMemberMethods(Context context, MethodDeclarationMap result)
        {
            if (derivedFrom == null)
                return;
            foreach (String ancestor in derivedFrom)
                registerAncestorMemberMethods(ancestor, context, result);
        }

        private void registerAncestorMemberMethods(String ancestor, Context context, MethodDeclarationMap result)
        {
            IDeclaration actual = context.getRegisteredDeclaration<IDeclaration>(ancestor);
            if (actual == null || !(actual is ConcreteCategoryDeclaration))
                return;
            ConcreteCategoryDeclaration cd = (ConcreteCategoryDeclaration)actual;
            cd.registerMemberMethods(context, result);
        }

		public IMethodDeclaration findOperator(Context context, Operator oper, IType type) {
			String methodName = "operator_" + oper.ToString();
			Dictionary<String, IMethodDeclaration>.ValueCollection methods = findMemberMethods(context, methodName);
			if(methods==null)
				return null;
			// find best candidate
			IMethodDeclaration candidate = null;
			foreach(IMethodDeclaration method in methods) {
				IType potential = method.getArguments()[0].GetType(context);
				if(!type.isAssignableTo(context, potential))
					continue;
				if(candidate==null)
					candidate = method;
				else {
					IType currentBest = candidate.getArguments()[0].GetType(context);
					if(currentBest.isAssignableTo(context, potential))
						candidate = method;
				}
			}
			return candidate;
		}
    }


}
using System;
using System.Text;
using prompto.error;
using System.Collections.Generic;
using prompto.runtime;
using prompto.parser;
using prompto.type;
using prompto.value;
using prompto.grammar;
using prompto.utils;

namespace prompto.declaration
{

	public class ConcreteCategoryDeclaration : CategoryDeclaration
	{

		protected IdentifierList derivedFrom;
		protected MethodDeclarationList methods;
		Dictionary<String, IDeclaration> methodsMap = null;

		public ConcreteCategoryDeclaration (String name)
			: base (name)
		{
		}

		public ConcreteCategoryDeclaration (String name, IdentifierList attrs, IdentifierList derivedFrom, MethodDeclarationList methods)
			: base (name, attrs)
		{
			this.derivedFrom = derivedFrom;
			this.methods = methods != null ? methods : new MethodDeclarationList ();
		}

		internal List<string> CollectCategories(Context context)
		{
			HashSet<String> set = new HashSet<String>();
			List<String> list = new List<String>();
			DoCollectCategories(context, set, list);
			return list;
		}

		private void DoCollectCategories(Context context, HashSet<String> set, List<String> list)
		{
			if (derivedFrom != null) derivedFrom.ForEach((cat) =>
			{
				ConcreteCategoryDeclaration cd = context.getRegisteredDeclaration<ConcreteCategoryDeclaration>(cat);
				cd.DoCollectCategories(context, set, list);
			});
			if(!set.Contains(this.GetName())) {
				set.Add(this.GetName());
				list.add(this.GetName());
			}
		}

		public override HashSet<String> GetAllAttributes(Context context)
		{
			HashSet<String> all = new HashSet<String>();
			HashSet<String> more = base.GetAllAttributes(context);
			if (more != null)
				all.UnionWith(more);
			if (derivedFrom != null)
			{
				derivedFrom.ForEach((id)=> {
					HashSet<String> ids = GetAncestorAttributes(context, id);
					if (ids != null)
						all.UnionWith(ids);
				});
			}
			return all.Count==0 ? null : all;
		}

		private HashSet<String> GetAncestorAttributes(Context context, String ancestor)
		{
			CategoryDeclaration actual = context.getRegisteredDeclaration<CategoryDeclaration>(ancestor);
			if(actual==null)
				return null;
			else
				return actual.GetAllAttributes(context);
		}


		public void setDerivedFrom (IdentifierList derivedFrom)
		{
			this.derivedFrom = derivedFrom;
		}

		override
        public IdentifierList getDerivedFrom ()
		{
			return derivedFrom;
		}

		public void setMethods (MethodDeclarationList methods)
		{
			this.methods = methods;
		}

		public MethodDeclarationList getMethods ()
		{
			return methods;
		}

		protected override void ToEDialect (CodeWriter writer)
		{
			bool hasMethods = methods != null && methods.Count > 0;
			protoToEDialect (writer, hasMethods, false); // no bindings
			if (hasMethods)
				methodsToEDialect (writer, methods);
		}

		protected override void categoryTypeToEDialect (CodeWriter writer)
		{
			if (derivedFrom == null)
				writer.append ("category");
			else
				derivedFrom.ToDialect (writer, true);
		}


		protected override void ToODialect (CodeWriter writer)
		{
			bool hasMethods = methods != null && methods.Count > 0;
			ToODialect (writer, hasMethods);
		}

		protected override void categoryTypeToODialect (CodeWriter writer)
		{
			if(this.Storable)
				writer.append("storable ");
			writer.append ("category");
		}

		protected override void categoryExtensionToODialect (CodeWriter writer)
		{
			if (derivedFrom != null) {
				writer.append (" extends ");
				derivedFrom.ToDialect (writer, true);
			}
		}

		protected override void bodyToODialect (CodeWriter writer)
		{
			methodsToODialect (writer, methods);
		}

		protected override void ToMDialect (CodeWriter writer)
		{
			protoToMDialect (writer, derivedFrom);
			methodsToMDialect (writer);
		}

		protected override void categoryTypeToMDialect (CodeWriter writer)
		{
			writer.append ("class");
		}

		private void methodsToMDialect (CodeWriter writer)
		{
			writer.indent ();
			if (methods == null || methods.Count == 0)
				writer.append ("pass\n");
			else {
				foreach (IDeclaration decl in methods) {
					CodeWriter w = writer.newMemberWriter ();
					decl.ToDialect (w);
					writer.newLine ();
				}
			}
			writer.dedent ();
		}


        
		public override bool hasAttribute (Context context, String name)
		{
			if (base.hasAttribute (context, name))
				return true;
			if (hasDerivedAttribute (context, name))
				return true;
			return false;
		}

		private bool hasDerivedAttribute (Context context, String name)
		{
			if (derivedFrom == null)
				return false;
			foreach (String ancestor in derivedFrom) {
				if (ancestorHasAttribute (ancestor, context, name))
					return true;
			}
			return false;
		}

		private static bool ancestorHasAttribute (String ancestor, Context context, String name)
		{
			CategoryDeclaration actual = context.getRegisteredDeclaration<CategoryDeclaration> (ancestor);
			if (actual == null)
				return false;
			return actual.hasAttribute (context, name);
		}

		override
        public IType check (Context context)
		{
			checkDerived (context);
			checkMethods (context);
			return base.check (context);
		}

		private void checkMethods (Context context)
		{
			registerMethods (context);
			foreach (IMethodDeclaration method in methods) {
				method.check (this, context);
			}
		}


		protected override void registerMethods (Context context)
		{
			if (methodsMap == null) {
				methodsMap = new Dictionary<String, IDeclaration> ();
				foreach (IMethodDeclaration method in methods) {
					method.setMemberOf (this);
					registerMethod (method, context);
				}
			}
		}


		private void registerMethod (IMethodDeclaration method, Context context)
		{
			IDeclaration actual;
			if (method is SetterMethodDeclaration) {
				if (methodsMap.TryGetValue ("setter:" + method.GetName (), out actual))
					throw new SyntaxError ("Duplicate setter: \"" + method.GetName () + "\"");
				methodsMap ["setter:" + method.GetName ()] = method;
			} else if (method is GetterMethodDeclaration) {
				if (methodsMap.TryGetValue ("getter:" + method.GetName (), out actual))
					throw new SyntaxError ("Duplicate getter: \"" + method.GetName () + "\"");
				methodsMap ["getter:" + method.GetName ()] = method;
			} else {
				if (!methodsMap.TryGetValue (method.GetName (), out actual)) {
					actual = new MethodDeclarationMap (method.GetName ());
					methodsMap [method.GetName ()] = (MethodDeclarationMap)actual;
				}
				((MethodDeclarationMap)actual).register (method, context);
			}
		}

		private void checkDerived (Context context)
		{
			if (derivedFrom != null)
				foreach (String category in derivedFrom) {
					ConcreteCategoryDeclaration cd = context.getRegisteredDeclaration<ConcreteCategoryDeclaration> (category);
					if (cd == null)
						throw new SyntaxError ("Unknown category: \"" + category + "\"");
				}
		}

		override
        public bool isDerivedFrom (Context context, CategoryType categoryType)
		{
			if (derivedFrom == null)
				return false;
			foreach (String ancestor in derivedFrom) {
				if (ancestor.Equals (categoryType.GetTypeName ()))
					return true;
				if (isAncestorDerivedFrom (ancestor, context, categoryType))
					return true;
			}
			return false;
		}

		private static bool isAncestorDerivedFrom (String ancestor, Context context, CategoryType categoryType)
		{
			IDeclaration actual = context.getRegisteredDeclaration<IDeclaration> (ancestor);
			if (actual == null || !(actual is CategoryDeclaration))
				return false;
			CategoryDeclaration cd = (CategoryDeclaration)actual;
			return cd.isDerivedFrom (context, categoryType);
		}


        public override IInstance newInstance (Context context)
		{
			return new ConcreteInstance (context, this);
		}

		public GetterMethodDeclaration findGetter (Context context, String attrName)
		{
			if (methodsMap == null)
				return null;
			IDeclaration method;
			if (methodsMap.TryGetValue ("getter:" + attrName, out method)) {
				if (method is GetterMethodDeclaration)
					return (GetterMethodDeclaration)method;
				else
					throw new SyntaxError ("Not a getter method!");
			}
			return findDerivedGetter (context, attrName);
		}

		private GetterMethodDeclaration findDerivedGetter (Context context, String attrName)
		{
			if (derivedFrom == null)
				return null;
			foreach (String ancestor in derivedFrom) {
				GetterMethodDeclaration method = findAncestorGetter (ancestor, context, attrName);
				if (method != null)
					return method;
			}
			return null;
		}

		private static GetterMethodDeclaration findAncestorGetter (String ancestor, Context context, String attrName)
		{
			IDeclaration actual = context.getRegisteredDeclaration<IDeclaration> (ancestor);
			if (actual == null || !(actual is ConcreteCategoryDeclaration))
				return null;
			ConcreteCategoryDeclaration cd = (ConcreteCategoryDeclaration)actual;
			return cd.findGetter (context, attrName);
		}

		public SetterMethodDeclaration findSetter (Context context, String attrName)
		{
			if (methodsMap == null)
				return null;
			IDeclaration method;
			if (methodsMap.TryGetValue ("setter:" + attrName, out method)) {
				if (method is SetterMethodDeclaration)
					return (SetterMethodDeclaration)method;
				else
					throw new SyntaxError ("Not a setter method!");
			}
			return findDerivedSetter (context, attrName);
		}

		private SetterMethodDeclaration findDerivedSetter (Context context, String attrName)
		{
			if (derivedFrom == null)
				return null;
			foreach (String ancestor in derivedFrom) {
				SetterMethodDeclaration method = findAncestorSetter (ancestor, context, attrName);
				if (method != null)
					return method;
			}
			return null;
		}

		private static SetterMethodDeclaration findAncestorSetter (String ancestor, Context context, String attrName)
		{
			IDeclaration actual = context.getRegisteredDeclaration<IDeclaration> (ancestor);
			if (actual == null || !(actual is ConcreteCategoryDeclaration))
				return null;
			ConcreteCategoryDeclaration cd = (ConcreteCategoryDeclaration)actual;
			return cd.findSetter (context, attrName);
		}

		public MethodDeclarationMap getMemberMethods (Context context, String name)
		{
			registerMethods(context);
			MethodDeclarationMap result = new MethodDeclarationMap (name);
			registerMemberMethods (context, result);
			return result;
		}


		private void registerMemberMethods (Context context, MethodDeclarationMap result)
		{
			registerThisMemberMethods (context, result);
			registerDerivedMemberMethods (context, result);
		}


		private void registerThisMemberMethods (Context context, MethodDeclarationMap result)
		{
			if (methodsMap == null)
				return;
			IDeclaration actual;
			if (!methodsMap.TryGetValue (result.GetName (), out actual))
				return;
			if (!(actual is MethodDeclarationMap))
				throw new SyntaxError ("Not a member method!");
			foreach (IMethodDeclaration method in ((MethodDeclarationMap)actual).Values)
				result.registerIfMissing (method, context);
		}

		private void registerDerivedMemberMethods (Context context, MethodDeclarationMap result)
		{
			if (derivedFrom == null)
				return;
			foreach (String ancestor in derivedFrom)
				registerAncestorMemberMethods (ancestor, context, result);
		}

		private void registerAncestorMemberMethods (String ancestor, Context context, MethodDeclarationMap result)
		{
			IDeclaration actual = context.getRegisteredDeclaration<IDeclaration> (ancestor);
			if (actual == null || !(actual is ConcreteCategoryDeclaration))
				return;
			ConcreteCategoryDeclaration cd = (ConcreteCategoryDeclaration)actual;
			cd.registerMemberMethods (context, result);
		}

		public IMethodDeclaration findOperator (Context context, Operator oper, IType type)
		{
			String methodName = "operator_" + oper.ToString ();
			Dictionary<String, IMethodDeclaration> methods = getMemberMethods (context, methodName);
			if (methods == null)
				return null;
			// find best candidate
			IMethodDeclaration candidate = null;
			foreach (IMethodDeclaration method in methods.Values) {
				IType potential = method.getArguments () [0].GetIType (context);
				if (!potential.isAssignableFrom (context, type))
					continue;
				if (candidate == null)
					candidate = method;
				else {
					IType currentBest = candidate.getArguments () [0].GetIType (context);
					if (potential.isAssignableFrom (context, currentBest))
						candidate = method;
				}
			}
			return candidate;
		}
	}


}
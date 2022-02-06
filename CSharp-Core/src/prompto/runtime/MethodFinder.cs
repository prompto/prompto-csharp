using prompto.grammar;
using prompto.error;
using System.Collections.Generic;
using System;
using prompto.statement;
using prompto.declaration;
using prompto.type;
using prompto.value;
using prompto.param;
using prompto.expression;

namespace prompto.runtime
{

    public class MethodFinder
    {

        Context context;
        MethodCall methodCall;

        public MethodFinder(Context context, MethodCall methodCall)
        {
            this.context = context;
            this.methodCall = methodCall;
        }

        public IMethodDeclaration findBest(bool checkInstance)
        {
            IMethodDeclaration decl = findBestReference(checkInstance);
            if (decl != null)
                return decl;
            decl = findBestMethod(checkInstance);
            if (decl != null)
                return decl;
            else
                throw new SyntaxError("No such method: " + methodCall.ToString());
        }

        public IMethodDeclaration findBestReference(bool checkInstance)
        {
            IMethodDeclaration candidate = findCandidateReference(checkInstance);
            if (candidate == null)
                return null;
            ISet<IMethodDeclaration> candidates = new HashSet<IMethodDeclaration>();
            candidates.Add(candidate);
            IEnumerator<IMethodDeclaration> compatibles = filterCompatible(candidates, checkInstance).GetEnumerator();
            if (compatibles.MoveNext())
                return compatibles.Current;
            else
                return null;
        }

        public IMethodDeclaration findCandidateReference(bool checkInstance)
        {
            MethodSelector selector = methodCall.getSelector();
            if (selector.getParent() != null)
                return null;
            if (checkInstance)
            {
                if (context.hasValue(selector.getName()))
                {
                    IValue value = context.getValue(selector.getName());
                    if (value is ClosureValue)
					return getClosureDeclaration(context, (ClosureValue)value);

                else if (value is ArrowValue)
					return getArrowDeclaration((ArrowValue)value);
                }
            }
            else
            {
                INamed named = context.getInstance(selector.getName(), true);
                if (named == null)
                    return null;
                IType type = named.GetIType(context).Resolve(context);
                if (type is MethodType)
				    return ((MethodType)type).Method.AsReference();
            }
            return null;
        }

        private IMethodDeclaration getArrowDeclaration(ArrowValue value)
        {
            return new ArrowDeclaration(value);
        }


        private IMethodDeclaration getClosureDeclaration(Context context, ClosureValue closure)
        {
            IMethodDeclaration decl = closure.Method;
            if (decl.getMemberOf() != null)
            {
                // the closure references a member method (useful when a method reference is needed)
                // in which case we may simply want to return that method to avoid spilling context into method body
                // this is only true if the closure comes straight from the method's instance context
                // if the closure comes from an accessible context that is not the instance context
                // then it is a local variable that needs the closure context to be interpreted
                MethodSelector selector = methodCall.getSelector();
                Context declaring = context.contextForValue(selector.getName());
                if (declaring == closure.getContext())
                    return decl;
            }
            return new ClosureDeclaration(closure);
        }

        public ISet<IMethodDeclaration> findPotential()
        {
            ISet<IMethodDeclaration> candidates = null;
            IMethodDeclaration candidate = findCandidateReference(false);
            if (candidate != null)
            {
                candidates = new HashSet<IMethodDeclaration>();
                candidates.Add(candidate);
            }
            else
                candidates = findCandidates(false);
            if (candidates.Count == 0)
                throw new SyntaxError("No such method: " + methodCall.ToString());
            return filterPotential(candidates);
        }

        ISet<IMethodDeclaration> filterPotential(ISet<IMethodDeclaration> candidates)
        {
            ISet<IMethodDeclaration> potential = new HashSet<IMethodDeclaration>();
            foreach (IMethodDeclaration declaration in candidates)
            {
                try
                {
                    ArgumentList args = methodCall.makeArguments(context, declaration);
                    if (declaration.isAssignableFrom(context, args))
                        potential.Add(declaration);
                }
                catch (SyntaxError e)
                {
                    // OK
                }
            }
            return potential;
        }


        public IMethodDeclaration findBestMethod(bool checkInstance)
        {
            ISet<IMethodDeclaration> candidates = findCandidates(checkInstance);
            ISet<IMethodDeclaration> compatibles = filterCompatible(candidates, checkInstance);
            IEnumerator<IMethodDeclaration> enumerator = compatibles.GetEnumerator();
            switch (compatibles.Count)
            {
                case 0:
                    return null;
                case 1:
                    enumerator.MoveNext();
                    return enumerator.Current;
                default:
                    return findMostSpecific(compatibles, checkInstance);
            }
        }

        public ISet<IMethodDeclaration> findCandidates(bool checkInstance)
        {
            ISet<IMethodDeclaration> candidates = new HashSet<IMethodDeclaration>();
            candidates.UnionWith(findMemberCandidates(checkInstance));
            candidates.UnionWith(findGlobalCandidates(checkInstance));
            return candidates;
        }

        private ISet<IMethodDeclaration> findGlobalCandidates(bool checkInstance)
        {
            MethodSelector selector = methodCall.getSelector();
            if (selector.getParent() != null)
                return new HashSet<IMethodDeclaration>();
            MethodDeclarationMap globals = context.getRegisteredDeclaration<MethodDeclarationMap>(selector.getName());
		    return globals != null ? new HashSet<IMethodDeclaration>(globals.Values) : new HashSet<IMethodDeclaration>();
	    }

        private ISet<IMethodDeclaration> findMemberCandidates(bool checkInstance)
        {
            MethodSelector selector = methodCall.getSelector();
            if (selector.getParent() == null)
            {
                // if called from a member method, could be a member method called without this/self
                InstanceContext instance = context.getClosestInstanceContext();
                if (instance != null)
                {
                    IType type = instance.getInstanceType();
                    ConcreteCategoryDeclaration cd = context.getRegisteredDeclaration<ConcreteCategoryDeclaration>(type.GetTypeName());
				    if(cd!=null) {
					    MethodDeclarationMap members = cd.getMemberMethods(context, selector.getName());
					    if(members!=null)
						    return new HashSet<IMethodDeclaration>(members.Values);
				    }
			    }
                return new HashSet<IMethodDeclaration>();
            }
            else
            {
                IType parentType = selector.checkParentType(context, checkInstance);
                return parentType != null ? parentType.getMemberMethods(context, selector.getName()) : new HashSet<IMethodDeclaration>();
            }
	    }

        IMethodDeclaration findMostSpecific(ISet<IMethodDeclaration> candidates, bool useInstance)
        {
            IMethodDeclaration candidate = null;
            List<IMethodDeclaration> ambiguous = new List<IMethodDeclaration>();
            foreach (IMethodDeclaration declaration in candidates)
            {
                if (candidate == null)
                    candidate = declaration;
                else
                {
                    Score score = scoreMostSpecific(candidate, declaration, useInstance);
                    switch (score)
                    {
                        case Score.WORSE:
                            candidate = declaration;
                            ambiguous.Clear();
                            break;
                        case Score.BETTER:
                            break;
                        case Score.SIMILAR:
                            ambiguous.Add(declaration);
                            break;
                    }
                }
            }
            if (ambiguous.Count > 0)
                throw new SyntaxError("Too many prototypes!"); // TODO refine
            return candidate;
        }

        Score scoreMostSpecific(IMethodDeclaration d1, IMethodDeclaration d2, bool useInstance)
        {
            try
            {
                Context s1 = context.newLocalContext();
                d1.registerParameters(s1);
                Context s2 = context.newLocalContext();
                d2.registerParameters(s2);
                IEnumerator<Argument> it1 = methodCall.makeArguments(context, d1).GetEnumerator();
                IEnumerator<Argument> it2 = methodCall.makeArguments(context, d2).GetEnumerator();
                while (it1.MoveNext() && it2.MoveNext())
                {
                    Argument as1 = it1.Current;
                    Argument as2 = it2.Current;
                    IParameter ar1 = d1.getParameters().find(as1.GetName());
                    IParameter ar2 = d2.getParameters().find(as2.GetName());
                    if (as1.GetName().Equals(as2.GetName()))
                    {
                        // the general case with named arguments
                        IType t1 = ar1.GetIType(s1);
                        IType t2 = ar2.GetIType(s2);
                        // try resolving runtime type
                        if (useInstance && t1 is CategoryType && t2 is CategoryType)
                        {
                            Object value = as1.getExpression().interpret(context); // in the named case as1==as2, so only interpret 1
                            if (value is IInstance)
                            {
                                CategoryType actual = ((IInstance)value).getType();
                                Score score = actual.scoreMostSpecific(context, (CategoryType)t1, (CategoryType)t2);
                                if (score != Score.SIMILAR)
                                    return score;
                            }
                        }
                        if (t1.isMoreSpecificThan(s2, t2))
                            return Score.BETTER;
                        if (t2.isMoreSpecificThan(s1, t1))
                            return Score.WORSE;
                    }
                    else
                    {
                        // specific case for single anonymous argument
                        Specificity? sp1 = d1.computeSpecificity(s1, ar1, as1, useInstance);
                        Specificity? sp2 = d2.computeSpecificity(s2, ar2, as2, useInstance);
                        if (sp1 > sp2)
                            return Score.BETTER;
                        if (sp2 > sp1)
                            return Score.WORSE;
                    }
                }
            }
            catch (PromptoError)
            {
            }
            return Score.SIMILAR;
        }

        ISet<IMethodDeclaration> filterCompatible(ICollection<IMethodDeclaration> candidates, bool useInstance)
        {
            ISet<IMethodDeclaration> compatibles = new HashSet<IMethodDeclaration>();
            foreach (IMethodDeclaration declaration in candidates)
            {
                try
                {
                    ArgumentList args = methodCall.makeArguments(context, declaration);
                    if (declaration.isAssignableTo(context, args, useInstance))
                        compatibles.Add(declaration);
                }
                catch (SyntaxError)
                {
                    // OK
                }
            }
            return compatibles;
        }

    }

}
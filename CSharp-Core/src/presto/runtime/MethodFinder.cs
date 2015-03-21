using presto.grammar;
using presto.error;
using System.Collections.Generic;
using System;
using presto.statement;
using presto.declaration;
using presto.type;
using presto.value;
namespace presto.runtime
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

        public IMethodDeclaration findMethod(bool checkInstance)
        {
            Dictionary<String, IMethodDeclaration>.ValueCollection candidates = methodCall.getMethod().getCandidates(context);
            List<IMethodDeclaration> compatibles = filterCompatible(candidates, checkInstance);
            switch (compatibles.Count)
            {
                case 0:
                    // TODO refine
                    throw new SyntaxError("No matching prototype for:" + methodCall.ToString());
                case 1:
                    return compatibles[0];
                default:
                    return findMostSpecific(compatibles, checkInstance);
            }
        }

        IMethodDeclaration findMostSpecific(List<IMethodDeclaration> candidates, bool checkInstance)
        {
            IMethodDeclaration candidate = null;
            List<IMethodDeclaration> ambiguous = new List<IMethodDeclaration>();
            foreach (IMethodDeclaration declaration in candidates)
            {
                if (candidate == null)
                    candidate = declaration;
                else
                {
                    Score score = scoreMostSpecific(candidate, declaration, checkInstance);
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

        Score scoreMostSpecific(IMethodDeclaration d1, IMethodDeclaration d2, bool checkInstance)
        {
            try
            {
                Context s1 = context.newLocalContext();
                d1.registerArguments(s1);
                Context s2 = context.newLocalContext();
                d2.registerArguments(s2);
                IEnumerator<ArgumentAssignment> it1 = methodCall.makeAssignments(context, d1).GetEnumerator();
                IEnumerator<ArgumentAssignment> it2 = methodCall.makeAssignments(context, d2).GetEnumerator();
                while (it1.MoveNext() && it2.MoveNext())
                {
                    ArgumentAssignment as1 = it1.Current;
                    ArgumentAssignment as2 = it2.Current;
                    IArgument ar1 = d1.getArguments().find(as1.getName());
                    IArgument ar2 = d2.getArguments().find(as2.getName());
                    if (as1.getName().Equals(as2.getName()))
                    {
                        // the general case with named arguments
                        IType t1 = ar1.GetType(s1);
                        IType t2 = ar2.GetType(s2);
                        // try resolving runtime type
                        if (checkInstance && t1 is CategoryType && t2 is CategoryType)
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
                        Specificity? sp1 = d1.computeSpecificity(s1, ar1, as1, checkInstance);
                        Specificity? sp2 = d2.computeSpecificity(s2, ar2, as2, checkInstance);
                        if (sp1 > sp2)
                            return Score.BETTER;
                        if (sp2 > sp1)
                            return Score.WORSE;
                    }
                }
            }
            catch (PrestoError)
            {
            }
            return Score.SIMILAR;
        }

        List<IMethodDeclaration> filterCompatible(Dictionary<String, IMethodDeclaration>.ValueCollection candidates, bool checkInstance)
        {
            List<IMethodDeclaration> compatibles = new List<IMethodDeclaration>();
            foreach (IMethodDeclaration declaration in candidates)
            {
                try
                {
                    ArgumentAssignmentList args = methodCall.makeAssignments(context, declaration);
                    if (declaration.isAssignableTo(context, args, checkInstance))
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
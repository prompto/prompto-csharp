using System;
using prompto.runtime;
using prompto.value;

namespace prompto.declaration
{

    /* a dummy declaration to interpret arrow expressions in context */
    public class ArrowDeclaration : AbstractMethodDeclaration
    {

        ArrowValue arrow;


        public ArrowDeclaration(ArrowValue arrow)
            : base("%Arrow", arrow.getMethod().getParameters(), arrow.getMethod().getReturnType())
        {

            this.arrow = arrow;
        }


        public override IValue interpret(Context context)
        {
            return arrow.interpret(context);
        }

    }

}

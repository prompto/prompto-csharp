using System;

namespace prompto.declaration
{
    public class WrappingWidgetDeclaration : ConcreteWidgetDeclaration
    {
        public WrappingWidgetDeclaration(CategoryDeclaration wrapped)
         : base(wrapped.GetName(), wrapped.getDerivedFrom()[0], wrapped.GetLocalMethods())
        {
        }
    }
}

using System;

namespace prompto.runtime
{
    public class ApplicationContext
    {
        private static Context instance;

        public static Context Set(Context context) 
        {
            Context replaced = instance;
            instance = context;
            return replaced;
        }

        public static Context Get()
        {
            return instance;
        }

        public static Context Init()
        {
            instance = Context.newGlobalsContext();
            return instance;
        }
    }
}

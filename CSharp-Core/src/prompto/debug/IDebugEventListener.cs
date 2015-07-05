using prompto.runtime;
using prompto.parser;
namespace prompto.debug
{

    public interface IDebugEventListener
    {
        void handleResumeEvent(ResumeReason reason, IContext context, ISection section);
        void handleSuspendEvent(SuspendReason reason, IContext context, ISection section);
        void handleTerminateEvent();
    }

}
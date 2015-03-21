using presto.runtime;
using presto.parser;
namespace presto.debug
{

    public interface IDebugEventListener
    {
        void handleResumeEvent(ResumeReason reason, IContext context, ISection section);
        void handleSuspendEvent(SuspendReason reason, IContext context, ISection section);
        void handleTerminateEvent();
    }

}
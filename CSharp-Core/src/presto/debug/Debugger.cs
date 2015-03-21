
using System;
using presto.error;
using presto.runtime;
using presto.parser;
using presto.grammar;
using System.Threading;
using presto.declaration;
namespace presto.debug
{

    public enum Status
    {
        STARTING,
        RUNNING,
        SUSPENDED,
        TERMINATING,
        TERMINATED /*;
		
	override
	public String ToString() {
		return name().Substring(0,1) + name().Substring(1).toLowerCase();
	} */
    }

    public class Debugger
    {

        volatile Stack stack = new Stack();
        AutoResetEvent blocker = new AutoResetEvent(false);
        volatile Status status = Status.STARTING;
        volatile ResumeReason resumeReason;
        IDebugEventListener listener;
        // positive for stepping on enterXXX
        // negative for stepping on leaveXXX
        // necessary to avoid stepping twice on the same statement
        volatile int stepDepth = 1;
        volatile bool suspended = false;
        volatile bool terminated = false;


        public Stack getStack()
        {
            return stack;
        }

        public Status getStatus()
        {
            return status;
        }

        public void suspend()
        {
            suspended = true;
        }

        public bool isTerminated()
        {
            return status == Status.TERMINATED;
        }

        public void terminate()
        {
            terminated = true;
        }

        public IDebugEventListener getListener()
        {
            return listener;
        }

        public void setListener(IDebugEventListener listener)
        {
            this.listener = listener;
        }

        public void enterMethod(Context context, IMethodDeclaration method)
        {
            terminateIfRequested();
            stack.Push(new StackFrame(context, method.getName(), method));
            if (stack.Count > 0 && stack.Count <= stepDepth)
                suspend(SuspendReason.STEPPING, context, method);
            else if (method.Breakpoint)
                suspend(SuspendReason.BREAKPOINT, context, method);
            else
                suspendIfRequested(context, method);
            terminateIfRequested();
        }

        public void leaveMethod(Context context, ISection section)
        {
            terminateIfRequested();
            if (stack.Count > 0 && stack.Count == -stepDepth)
                suspend(SuspendReason.STEPPING, context, section);
            else
                suspendIfRequested(context, section);
            stack.Pop();
            terminateIfRequested();
        }

        public void enterStatement(Context context, ISection section)
        {
            terminateIfRequested();
            StackFrame previous = stack.Pop();
            stack.Push(new StackFrame(context, previous.getMethodName(), section));
            if (stack.Count > 0 && stack.Count <= stepDepth)
                suspend(SuspendReason.STEPPING, context, section);
            else if (section.Breakpoint)
                suspend(SuspendReason.BREAKPOINT, context, section);
            else
                suspendIfRequested(context, section);
            terminateIfRequested();
        }

        public void leaveStatement(Context context, ISection section)
        {
            terminateIfRequested();
            if (stack.Count > 0 && stack.Count == -stepDepth)
                suspend(SuspendReason.STEPPING, context, section);
            else
                suspendIfRequested(context, section);
            terminateIfRequested();
        }

        private void terminateIfRequested()
        {
            if (terminated)
            {
                status = Status.TERMINATING;
                throw new TerminatedError();
            }
        }

        private void suspendIfRequested(Context context, ISection section)
        {
            if (suspended)
            {
                suspended = false;
                suspend(SuspendReason.SUSPENDED, context, section);
            }

        }

        public void suspend(SuspendReason reason, Context context, ISection section)
        {
            status = Status.SUSPENDED;
            if (listener != null)
                listener.handleSuspendEvent(reason, context, section);
            try
            {
                blocker.WaitOne();
            }
            finally
            {
                status = Status.RUNNING;
                if (listener != null)
                    listener.handleResumeEvent(resumeReason, context, section);
            }
        }

        public bool isStepping()
        {
            return stepDepth != 0;
        }

        public bool canSuspend()
        {
            return !isSuspended();
        }

        public bool isSuspended()
        {
            return status == Status.SUSPENDED;
        }

        public bool canResume()
        {
            return isSuspended();
        }

        public void resume()
        {
            stepDepth = 0;
            doResume(ResumeReason.RESUMED);
        }

        public bool canStepOver()
        {
            return isSuspended();
        }

        public void stepOver()
        {
            stepDepth = stack.Count;
            doResume(ResumeReason.STEP_OVER);
        }

        public bool canStepInto()
        {
            return isSuspended();
        }

        public void stepInto()
        {
            stepDepth = Math.Abs(stepDepth) + 1;
            doResume(ResumeReason.STEP_INTO);
        }

        public bool canStepOut()
        {
            return isSuspended();
        }

        public void stepOut()
        {
            stepDepth = -(Math.Abs(stepDepth) - 1);
            doResume(ResumeReason.STEP_OUT);
        }

        public void doResume(ResumeReason reason)
        {
            this.resumeReason = reason;
            blocker.Set();
        }

        public int getLine()
        {
            StackFrame frame = stack.Peek();
            return frame == null ? -1 : frame.getLine();
        }

        public void whenTerminated()
        {
            status = Status.TERMINATED;
            if (listener != null)
                listener.handleTerminateEvent();
        }

    }

}
using System;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.TextManager.Interop;

namespace CMcG.TrimLineEnds
{
    public abstract class CommandFilter : IOleCommandTarget
    {
        protected abstract Guid CommandGuid { get; }

        public IOleCommandTarget Next { get; set; }

        public int Exec(ref Guid pguidCmdGroup, uint nCmdID, uint nCmdexecopt, IntPtr pvaIn, IntPtr pvaOut)
        {
            if (pguidCmdGroup != CommandGuid)
                return Next.Exec(ref pguidCmdGroup, nCmdID, nCmdexecopt, pvaIn, pvaOut);

            Execute(nCmdID);
            return VSConstants.S_OK;
        }

        public int QueryStatus(ref Guid pguidCmdGroup, uint cCmds, OLECMD[] prgCmds, IntPtr pCmdText)
        {
            if (pguidCmdGroup != CommandGuid)
                return Next.QueryStatus(ref pguidCmdGroup, cCmds, prgCmds, pCmdText);

            prgCmds[0].cmdf = (uint)(CanExecute(cCmds)
                            ? (OLECMDF.OLECMDF_ENABLED | OLECMDF.OLECMDF_SUPPORTED)
                            : OLECMDF.OLECMDF_SUPPORTED);

            return VSConstants.S_OK;
        }

        public virtual bool CanExecute(uint cCmds)
        {
            return true;
        }

        public abstract void Execute(uint nCmdID);

        public static void Register(IVsTextView textViewAdapter, CommandFilter filter)
        {
            IOleCommandTarget next;
            if (ErrorHandler.Succeeded(textViewAdapter.AddCommandFilter(filter, out next)))
                filter.Next = next;
        }
    }
}
using System;
using TrimLineEnds;
using Microsoft.VisualStudio.Text.Editor;

namespace CMcG.TrimLineEnds
{
    sealed class TrimLineEndsCommandFilter : CommandFilter
    {
        IWpfTextView m_view;

        public TrimLineEndsCommandFilter(IWpfTextView view)
        {
            m_view = view;
        }

        protected override Guid CommandGuid
        {
            get { return GuidList.guidTrimLineEndsCmdSet; }
        }

        public override void Execute(uint nCmdID)
        {
            if (nCmdID != PkgCmdIDList.TrimLineEnds)
                return;

            var snapshot = m_view.TextSnapshot;
            using (var edit = snapshot.TextBuffer.CreateEdit())
            {
                foreach (var line in snapshot.Lines)
                {
                    var text     = line.GetText();
                    var endChars = text.Length - text.TrimEnd().Length;
                    if (endChars > 0)
                        edit.Delete(line.End.Position - endChars, endChars);
                }

                edit.Apply();
            }
        }
    }
}
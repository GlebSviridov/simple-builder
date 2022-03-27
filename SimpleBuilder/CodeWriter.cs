using System;
using System.Text;

namespace SimpleBuilder
{
    internal class CodeWriter
    {
        StringBuilder Content { get; } = new();

        int IndentLevel { get; set; }

        ScopeTracker MScopeTracker { get; }

        public CodeWriter()
        {
            MScopeTracker = new ScopeTracker(this);
        }

        public void Append(string line) => Content.Append(line);

        public void AppendLine(string line) => Content.Append(new string('\t', IndentLevel)).AppendLine(line);

        public void AppendLine() => Content.AppendLine();

        public IDisposable BeginScope(string line, bool closing = false)
        {
            AppendLine(line);
            return BeginScope(closing);
        }

        public void EndLine() => Content.AppendLine();

        public void StartLine() => Content.Append(new string('\t', IndentLevel));

        public override string ToString() => Content.ToString();

        private IDisposable BeginScope(bool closing)
        {
            MScopeTracker.Closing = closing;
            Content.Append(new string('\t', IndentLevel)).AppendLine("{");
            IndentLevel += 1;
            return new ScopeTracker(this, closing);
        }

        private void EndScope(bool closing)
        {
            IndentLevel -= 1;
            Content.Append(new string('\t', IndentLevel)).AppendLine(
                closing
                    ? "};"
                    : "}");
        }

        class ScopeTracker : IDisposable
        {
            private CodeWriter Parent { get; }

            public bool Closing { get; set; }

            public ScopeTracker(CodeWriter parent)
            {
                Parent = parent;
            }

            public ScopeTracker(CodeWriter parent, bool closing)
            {
                Parent = parent;
                Closing = closing;
            }

            public void Dispose()
            {
                Parent.EndScope(Closing);
            }
        }
    }
}
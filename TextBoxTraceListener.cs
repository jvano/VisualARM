using FastColoredTextBoxNS;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Vano.Tools.Azure
{
    public class TextBoxTraceListener : TraceListener
    {
        private TextBox _textbox;

        public TextBoxTraceListener(TextBox textbox) 
        {
            _textbox = textbox;
        }

        public override void Write(string message)
        {
            _textbox.AppendText(message);
        }

        public override void WriteLine(string message)
        {
            _textbox.AppendText(message);
            _textbox.AppendText(Environment.NewLine);
        }
    }

    public class ColoredTextBoxTraceListener : TraceListener
    {
        private FastColoredTextBox _textbox;

        public ColoredTextBoxTraceListener(FastColoredTextBox textbox)
        {
            _textbox = textbox;
        }

        public override void Write(string message)
        {
            _textbox.AppendText(message);
        }

        public override void WriteLine(string message)
        {
            _textbox.AppendText(message);
            _textbox.AppendText(Environment.NewLine);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformApp1.UserControls
{
    public enum ResultType { OK, NG, None }
    public struct ResultTypeText
    {
        public const string OK = "OK";
        public const string NG = "NG";
    }
    public partial class ResultHighlighter : UserControl
    {
        readonly Color OkColor = Color.Blue;
        readonly Color NgColor = Color.Red;
        readonly Color NoneColor = Color.Gray;
        ResultType _result;
        public ResultType Result
        {
            get => _result;
            set
            {
                switch (value)
                {
                    case ResultType.OK:
                        lblOk.BackColor = OkColor;
                        lblNG.BackColor = NoneColor;
                        break;
                    case ResultType.NG:
                        lblOk.BackColor = NoneColor;
                        lblNG.BackColor = NgColor;
                        break;
                    case ResultType.None:
                        lblOk.BackColor = NoneColor;
                        lblNG.BackColor = NoneColor;
                        break;
                }
                _result = value;
            }
        }
        public ResultHighlighter()
        {
            InitializeComponent();
            _result = ResultType.None;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace CodeEditor
{
    public partial class Editor : Form
    {
        public Editor()
        {
            InitializeComponent();
        }

        private ChromiumWebBrowser _chromium;
        private void Editor_Load(object sender, EventArgs e)
        {
            CefSettings settings=new CefSettings();
            Cef.Initialize(settings);
            _chromium=new ChromiumWebBrowser("http://localhost:55321/");
            this.panel1.Controls.Add(_chromium);
            _chromium.Dock = DockStyle.Fill;


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

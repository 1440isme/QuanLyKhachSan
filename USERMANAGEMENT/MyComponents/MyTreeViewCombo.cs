using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace USERMANAGEMENT.MyComponents
{
    public partial class MyTreeViewCombo : System.Windows.Forms.ComboBox
    {
        ToolStripControlHost treeViewHost;
        public ToolStripDropDown dropDown;
        TreeView treeView;
        public MyTreeViewCombo(int _width, int _height)
        {
            treeView = new TreeView();
            treeView.BorderStyle = BorderStyle.None;
            treeView.Width = _width;
            treeView.Height = _height;
            treeView.Font = new Font("Tahoma", 10, FontStyle.Bold);
            treeViewHost = new ToolStripControlHost(treeView);
            dropDown = new ToolStripDropDown();
            dropDown.Items.Add(treeViewHost);
        }
        public void sizeChanged(int _width, int _height)
        {
            treeView.Width = _width;
            treeView.Height = _height;
            
        }
        public TreeView TreeView
        {
            get { return treeViewHost.Control as TreeView; }
        }
         public void ShowDropDown()
        {
            if (dropDown != null)
            {
                treeViewHost.Width = DropDownWidth;
                treeViewHost.Height = DropDownHeight;
                dropDown.Show(this, 0, this.Height);
            }
        }
        public const int WM_USER = 0x0400,
                                WM_REFLECT = WM_USER + 0x1C00,
                                WM_COMMAND = 0x0111,
                                CBN_DROPDOWN = 7;
        public static int HIWORD(int n)
        {
            return (n  >> 16) & 0xffff;
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (WM_REFLECT + WM_COMMAND))
            {
                if (HIWORD((int)m.WParam) == CBN_DROPDOWN)
                {
                    ShowDropDown();
                    return;
                }
            }
            base.WndProc(ref m);
        }
        

    }
    
}

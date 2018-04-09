using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using turizm.Lib;
using turizm.Lib.DB;
using turizm.Lib.VK;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model.RequestParams;

namespace turizm
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void buttonUpdateDB_Click(object sender, EventArgs e)
        {
            Options opt = new Options();
            CommentDatabase db = new CommentDatabase(opt);
            VK vk = new VK(opt);
            vk.UpdateDB(db, opt, new Action<int>((arg) => { }));
        }
    }
}

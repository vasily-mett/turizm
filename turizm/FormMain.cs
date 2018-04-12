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
        Options options;

        /// <summary>
        /// конструктор, настройка внешнего вида
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
            options = new Options();
            foreach (string link in options.Topics)
            {
                ListViewItem item = new ListViewItem(link);
                listViewTopics.Items.Add(item);
            }
        }

        /// <summary>
        /// кнопка обновить БД
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUpdateDB_Click(object sender, EventArgs e)
        {

            CommentDatabase db = new CommentDatabase(options);
            VK vk = new VK(options);
            vk.UpdateDB(db, options, new Action<int>((arg) => { }));
        }
    }
}

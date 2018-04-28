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
using turizm.Lib.Classes;
using turizm.Lib.DB;
using turizm.Lib.Neuro;
using turizm.Lib.VK;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model.RequestParams;

namespace turizm
{
    public partial class FormMain : Form
    {
        Options options;
        CommentDatabase db;
        VK vk;

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

            db = new CommentDatabase(options.DatabaseFileName);
            vk = new VK(options, db);
        }

        /// <summary>
        /// кнопка обновить БД
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUpdateDB_Click(object sender, EventArgs e)
        {
            labelProgress.Visible = true;
            vk.UpdateDB(db, options, labelProgress);
            labelProgress.Visible = false;
            labelTotalComments.Visible = true;
            labelTotalUsers.Visible = true;
            labelTotalComments.Text = "Количество комментариев в базе: " + db.TotalComments.ToString();
            labelTotalUsers.Text = "Количество пользователей в базе: " + db.TotalUsers.ToString();

        }

        /// <summary>
        /// кнопка поиска
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFilter_Click(object sender, EventArgs e)
        {
            List<string> find;
            List<string> exclude;

            if (textBoxFind.Text.Trim().Length > 0)
                find = textBoxFind.Text.Trim().ToLower().Replace(" ", "").Split(',').ToList();
            else
                find = null;
            if (textBoxExclude.Text.Trim().Length > 0)
                exclude = textBoxExclude.Text.Trim().ToLower().Replace(" ", "").Split(',').ToList();
            else
                exclude = null;

            List<Comment> comments = db.FindComments(find, exclude);
            FormShowComments fsc = new FormShowComments(comments, db);
            fsc.Show(this);
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            db.Close();
        }
    }
}

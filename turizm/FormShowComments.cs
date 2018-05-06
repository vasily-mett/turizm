using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using turizm.Lib.Classes;
using turizm.Lib.DB;

namespace turizm
{
    /// <summary>
    /// окно вывода комментариев
    /// </summary>
    public partial class FormShowComments : Form
    {
        private List<Comment> comments;
        private CommentDatabase db;
        private ToolTip tooltipmask;

        private FormShowComments()
        {
            InitializeComponent();
            tooltipmask = new ToolTip() { ToolTipIcon = ToolTipIcon.Info, ToolTipTitle = "Совпадающие маски:" };

        }

        /// <summary>
        /// создает окно вывода списка комментариев
        /// </summary>
        /// <param name="comments">список комментариев для вывода</param>
        public FormShowComments(List<Comment> comments, CommentDatabase db)
            : this()
        {
            this.comments = comments != null ? comments : new List<Comment>();
            this.db = db;
        }

        /// <summary>
        /// загрузка комментариев в таблицу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormShowComments_Load(object sender, EventArgs e)
        {
            //сортировка массива комментариев по лайкам и рекламным словам
            comments.Sort((x, y) =>
            {
                int weightX = x.Likes - x.AdvertMasks.Count * 3;
                int weightY = y.Likes - y.AdvertMasks.Count * 3;

                if (weightX < weightY) return 1;
                else if (weightX > weightY) return -1;
                else return 0;
            });

            //заполнение таблицы
            DataTable dt = new DataTable();
            dt.Columns.Add("Комментарий");
            foreach (Comment comm in this.comments)
            {
                DataRow dr = dt.NewRow();
                dr[0] = comm.Text;
                dt.Rows.Add(dr);
            }
            dataGridViewComments.DataSource = dt;
            dataGridViewComments.Columns[0].Width = 200;
            labelTotalComments.Text = "Найдено комментариев: " + comments.Count + ", из них с рекламой: " + comments.Count((arg) => { return arg.AdvertMasks.Count > 1; });
        }

        /// <summary>
        /// вывод текста комментария при изменении выделенной строки в таблице
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewComments_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewComments.SelectedCells.Count > 0)
            {
                int ind = dataGridViewComments.SelectedCells[0].RowIndex;
                if (ind < comments.Count)
                {
                    Comment comm = comments[ind];
                    textBoxCommentText.Text = comm.Text;
                    labelLikes.Text = "Количество лайков: " + comm.Likes.ToString();
                    labelAdv.Text = "Количество рекламных слов: " + comm.AdvertMasks.Count.ToString();

                    //заполнение маски

                    string mask_list = "";
                    foreach (string m in comm.AdvertMasks)
                        mask_list += m + "\r\n  ";
                    tooltipmask.SetToolTip(labelAdv, mask_list);


                    User user = db.GetUser(comm.UserID);
                    labelName.Text = "Пользователь: " + user.FirstName + " " + user.LastName;
                }
            }
        }

        /// <summary>
        /// закрашивание рекламных 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewComments_Paint(object sender, PaintEventArgs e)
        {
            if (comments.Count == 0)
                return;

            int i = 0;
            foreach (DataGridViewRow dr in dataGridViewComments.Rows)
            {
                if (i == comments.Count)
                    continue;
                Comment tf = comments[i];
                if (tf.AdvertMasks.Count > 1)
                    dr.Cells[0].Style.BackColor = Color.Yellow;
                i++;
            }
        }
    }
}

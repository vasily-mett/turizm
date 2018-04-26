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

namespace turizm
{
    /// <summary>
    /// окно вывода комментариев
    /// </summary>
    public partial class FormShowComments : Form
    {
        private List<Comment> comments;

        private FormShowComments()
        {
            InitializeComponent();
        }

        /// <summary>
        /// создает окно вывода списка комментариев
        /// </summary>
        /// <param name="comments">список комментариев для вывода</param>
        public FormShowComments(List<Comment> comments)
            :this()
        {
            this.comments = comments!=null?comments:new List<Comment>();
        }

        /// <summary>
        /// загрузка комментариев в таблицу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormShowComments_Load(object sender, EventArgs e)
        {
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
                textBoxCommentText.Text = comments[ind].Text;
            }
        }
    }
}

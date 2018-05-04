﻿using System;
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

        private FormShowComments()
        {
            InitializeComponent();
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
            labelTotalComments.Text = "Найдено комментариев: " + comments.Count;
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
                    User user = db.GetUser(comm.UserID);
                    labelName.Text = "Пользователь: " + user.FirstName + " " + user.LastName;
                }
            }
        }
    }
}

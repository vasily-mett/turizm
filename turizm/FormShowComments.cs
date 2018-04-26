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
            this.comments = comments;
        }
    }
}

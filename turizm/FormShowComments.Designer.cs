namespace turizm
{
    partial class FormShowComments
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridViewComments = new System.Windows.Forms.DataGridView();
            this.textBoxCommentText = new System.Windows.Forms.TextBox();
            this.labelLikes = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.labelTotalComments = new System.Windows.Forms.Label();
            this.labelAdv = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewComments)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewComments
            // 
            this.dataGridViewComments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewComments.Location = new System.Drawing.Point(12, 24);
            this.dataGridViewComments.Name = "dataGridViewComments";
            this.dataGridViewComments.Size = new System.Drawing.Size(290, 444);
            this.dataGridViewComments.TabIndex = 0;
            this.dataGridViewComments.SelectionChanged += new System.EventHandler(this.dataGridViewComments_SelectionChanged);
            this.dataGridViewComments.Paint += new System.Windows.Forms.PaintEventHandler(this.dataGridViewComments_Paint);
            // 
            // textBoxCommentText
            // 
            this.textBoxCommentText.Location = new System.Drawing.Point(308, 24);
            this.textBoxCommentText.Multiline = true;
            this.textBoxCommentText.Name = "textBoxCommentText";
            this.textBoxCommentText.Size = new System.Drawing.Size(243, 158);
            this.textBoxCommentText.TabIndex = 1;
            // 
            // labelLikes
            // 
            this.labelLikes.AutoSize = true;
            this.labelLikes.Location = new System.Drawing.Point(308, 185);
            this.labelLikes.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelLikes.Name = "labelLikes";
            this.labelLikes.Size = new System.Drawing.Size(19, 13);
            this.labelLikes.TabIndex = 2;
            this.labelLikes.Text = "    ";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(308, 198);
            this.labelName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(19, 13);
            this.labelName.TabIndex = 3;
            this.labelName.Text = "    ";
            // 
            // labelTotalComments
            // 
            this.labelTotalComments.AutoSize = true;
            this.labelTotalComments.Location = new System.Drawing.Point(10, 7);
            this.labelTotalComments.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTotalComments.Name = "labelTotalComments";
            this.labelTotalComments.Size = new System.Drawing.Size(35, 13);
            this.labelTotalComments.TabIndex = 4;
            this.labelTotalComments.Text = "label1";
            // 
            // labelAdv
            // 
            this.labelAdv.AutoSize = true;
            this.labelAdv.Location = new System.Drawing.Point(308, 211);
            this.labelAdv.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelAdv.Name = "labelAdv";
            this.labelAdv.Size = new System.Drawing.Size(19, 13);
            this.labelAdv.TabIndex = 3;
            this.labelAdv.Text = "    ";
            // 
            // FormShowComments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 481);
            this.Controls.Add(this.labelTotalComments);
            this.Controls.Add(this.labelAdv);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.labelLikes);
            this.Controls.Add(this.textBoxCommentText);
            this.Controls.Add(this.dataGridViewComments);
            this.Name = "FormShowComments";
            this.Text = "Результаты поиска";
            this.Load += new System.EventHandler(this.FormShowComments_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewComments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewComments;
        private System.Windows.Forms.TextBox textBoxCommentText;
        private System.Windows.Forms.Label labelLikes;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelTotalComments;
        private System.Windows.Forms.Label labelAdv;
    }
}
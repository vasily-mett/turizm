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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewComments)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewComments
            // 
            this.dataGridViewComments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewComments.Location = new System.Drawing.Point(16, 15);
            this.dataGridViewComments.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridViewComments.Name = "dataGridViewComments";
            this.dataGridViewComments.Size = new System.Drawing.Size(387, 562);
            this.dataGridViewComments.TabIndex = 0;
            this.dataGridViewComments.SelectionChanged += new System.EventHandler(this.dataGridViewComments_SelectionChanged);
            // 
            // textBoxCommentText
            // 
            this.textBoxCommentText.Location = new System.Drawing.Point(411, 15);
            this.textBoxCommentText.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxCommentText.Multiline = true;
            this.textBoxCommentText.Name = "textBoxCommentText";
            this.textBoxCommentText.Size = new System.Drawing.Size(323, 111);
            this.textBoxCommentText.TabIndex = 1;
            // 
            // labelLikes
            // 
            this.labelLikes.AutoSize = true;
            this.labelLikes.Location = new System.Drawing.Point(410, 130);
            this.labelLikes.Name = "labelLikes";
            this.labelLikes.Size = new System.Drawing.Size(24, 17);
            this.labelLikes.TabIndex = 2;
            this.labelLikes.Text = "    ";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(410, 147);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(24, 17);
            this.labelName.TabIndex = 3;
            this.labelName.Text = "    ";
            // 
            // FormShowComments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 592);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.labelLikes);
            this.Controls.Add(this.textBoxCommentText);
            this.Controls.Add(this.dataGridViewComments);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
    }
}
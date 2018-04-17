namespace turizm
{
    partial class FormMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonUpdateDB = new System.Windows.Forms.Button();
            this.listViewTopics = new System.Windows.Forms.ListView();
            this.labelTotalComments = new System.Windows.Forms.Label();
            this.labelTotalUsers = new System.Windows.Forms.Label();
            this.labelProgress = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonUpdateDB
            // 
            this.buttonUpdateDB.Location = new System.Drawing.Point(356, 12);
            this.buttonUpdateDB.Name = "buttonUpdateDB";
            this.buttonUpdateDB.Size = new System.Drawing.Size(91, 23);
            this.buttonUpdateDB.TabIndex = 0;
            this.buttonUpdateDB.Text = "Обновить БД";
            this.buttonUpdateDB.UseVisualStyleBackColor = true;
            this.buttonUpdateDB.Click += new System.EventHandler(this.buttonUpdateDB_Click);
            // 
            // listViewTopics
            // 
            this.listViewTopics.Location = new System.Drawing.Point(12, 12);
            this.listViewTopics.Name = "listViewTopics";
            this.listViewTopics.Size = new System.Drawing.Size(325, 221);
            this.listViewTopics.TabIndex = 1;
            this.listViewTopics.UseCompatibleStateImageBehavior = false;
            this.listViewTopics.View = System.Windows.Forms.View.List;
            // 
            // labelTotalComments
            // 
            this.labelTotalComments.AutoSize = true;
            this.labelTotalComments.Location = new System.Drawing.Point(12, 236);
            this.labelTotalComments.Name = "labelTotalComments";
            this.labelTotalComments.Size = new System.Drawing.Size(35, 13);
            this.labelTotalComments.TabIndex = 2;
            this.labelTotalComments.Text = "label1";
            // 
            // labelTotalUsers
            // 
            this.labelTotalUsers.AutoSize = true;
            this.labelTotalUsers.Location = new System.Drawing.Point(12, 249);
            this.labelTotalUsers.Name = "labelTotalUsers";
            this.labelTotalUsers.Size = new System.Drawing.Size(35, 13);
            this.labelTotalUsers.TabIndex = 2;
            this.labelTotalUsers.Text = "label1";
            // 
            // labelProgress
            // 
            this.labelProgress.AutoSize = true;
            this.labelProgress.Location = new System.Drawing.Point(12, 262);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(35, 13);
            this.labelProgress.TabIndex = 2;
            this.labelProgress.Text = "label1";
            this.labelProgress.Visible = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 432);
            this.Controls.Add(this.labelProgress);
            this.Controls.Add(this.labelTotalUsers);
            this.Controls.Add(this.labelTotalComments);
            this.Controls.Add(this.listViewTopics);
            this.Controls.Add(this.buttonUpdateDB);
            this.Name = "FormMain";
            this.Text = "Туризм";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonUpdateDB;
        private System.Windows.Forms.ListView listViewTopics;
        private System.Windows.Forms.Label labelTotalComments;
        private System.Windows.Forms.Label labelTotalUsers;
        private System.Windows.Forms.Label labelProgress;
    }
}


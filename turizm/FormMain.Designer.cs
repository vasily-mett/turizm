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
            this.components = new System.ComponentModel.Container();
            this.buttonUpdateDB = new System.Windows.Forms.Button();
            this.listViewTopics = new System.Windows.Forms.ListView();
            this.labelTotalComments = new System.Windows.Forms.Label();
            this.labelTotalUsers = new System.Windows.Forms.Label();
            this.labelProgress = new System.Windows.Forms.Label();
            this.buttonFilter = new System.Windows.Forms.Button();
            this.textBoxFind = new System.Windows.Forms.TextBox();
            this.textBoxExclude = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // buttonUpdateDB
            // 
            this.buttonUpdateDB.Location = new System.Drawing.Point(356, 25);
            this.buttonUpdateDB.Name = "buttonUpdateDB";
            this.buttonUpdateDB.Size = new System.Drawing.Size(91, 23);
            this.buttonUpdateDB.TabIndex = 0;
            this.buttonUpdateDB.Text = "Обновить БД";
            this.buttonUpdateDB.UseVisualStyleBackColor = true;
            this.buttonUpdateDB.Click += new System.EventHandler(this.buttonUpdateDB_Click);
            // 
            // listViewTopics
            // 
            this.listViewTopics.Location = new System.Drawing.Point(12, 25);
            this.listViewTopics.Name = "listViewTopics";
            this.listViewTopics.Size = new System.Drawing.Size(325, 208);
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
            this.labelTotalComments.Visible = false;
            // 
            // labelTotalUsers
            // 
            this.labelTotalUsers.AutoSize = true;
            this.labelTotalUsers.Location = new System.Drawing.Point(12, 249);
            this.labelTotalUsers.Name = "labelTotalUsers";
            this.labelTotalUsers.Size = new System.Drawing.Size(35, 13);
            this.labelTotalUsers.TabIndex = 2;
            this.labelTotalUsers.Text = "label1";
            this.labelTotalUsers.Visible = false;
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
            // buttonFilter
            // 
            this.buttonFilter.Location = new System.Drawing.Point(356, 276);
            this.buttonFilter.Name = "buttonFilter";
            this.buttonFilter.Size = new System.Drawing.Size(91, 23);
            this.buttonFilter.TabIndex = 3;
            this.buttonFilter.Text = "Найти";
            this.buttonFilter.UseVisualStyleBackColor = true;
            this.buttonFilter.Click += new System.EventHandler(this.buttonFilter_Click);
            // 
            // textBoxFind
            // 
            this.textBoxFind.Location = new System.Drawing.Point(86, 278);
            this.textBoxFind.Name = "textBoxFind";
            this.textBoxFind.Size = new System.Drawing.Size(251, 20);
            this.textBoxFind.TabIndex = 4;
            this.toolTip1.SetToolTip(this.textBoxFind, "Введите слова, разделяя запятыми");
            // 
            // textBoxExclude
            // 
            this.textBoxExclude.Location = new System.Drawing.Point(86, 304);
            this.textBoxExclude.Name = "textBoxExclude";
            this.textBoxExclude.Size = new System.Drawing.Size(251, 20);
            this.textBoxExclude.TabIndex = 5;
            this.toolTip1.SetToolTip(this.textBoxExclude, "Введите слова, разделяя запятыми");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 281);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Искать:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 307);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Исключить:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(174, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Список обсуждений для анализа";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 432);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxExclude);
            this.Controls.Add(this.textBoxFind);
            this.Controls.Add(this.buttonFilter);
            this.Controls.Add(this.labelProgress);
            this.Controls.Add(this.labelTotalUsers);
            this.Controls.Add(this.labelTotalComments);
            this.Controls.Add(this.listViewTopics);
            this.Controls.Add(this.buttonUpdateDB);
            this.Name = "FormMain";
            this.Text = "Туризм";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonUpdateDB;
        private System.Windows.Forms.ListView listViewTopics;
        private System.Windows.Forms.Label labelTotalComments;
        private System.Windows.Forms.Label labelTotalUsers;
        private System.Windows.Forms.Label labelProgress;
        private System.Windows.Forms.Button buttonFilter;
        private System.Windows.Forms.TextBox textBoxFind;
        private System.Windows.Forms.TextBox textBoxExclude;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}


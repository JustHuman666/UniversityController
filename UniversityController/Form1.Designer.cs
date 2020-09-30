namespace UniversityController
{
    partial class Form1
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
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.StudentSelectButton = new System.Windows.Forms.Button();
            this.TeacherSelectButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // StudentSelectButton
            // 
            this.StudentSelectButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.StudentSelectButton.Location = new System.Drawing.Point(464, 0);
            this.StudentSelectButton.Name = "StudentSelectButton";
            this.StudentSelectButton.Size = new System.Drawing.Size(320, 661);
            this.StudentSelectButton.TabIndex = 0;
            this.StudentSelectButton.Text = "Student";
            this.StudentSelectButton.UseVisualStyleBackColor = true;
            this.StudentSelectButton.Click += new System.EventHandler(this.StudentSelectButton_Click);
            // 
            // TeacherSelectButton
            // 
            this.TeacherSelectButton.AutoSize = true;
            this.TeacherSelectButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.TeacherSelectButton.Location = new System.Drawing.Point(0, 0);
            this.TeacherSelectButton.Name = "TeacherSelectButton";
            this.TeacherSelectButton.Size = new System.Drawing.Size(271, 661);
            this.TeacherSelectButton.TabIndex = 1;
            this.TeacherSelectButton.Text = "Teacher";
            this.TeacherSelectButton.UseVisualStyleBackColor = true;
            this.TeacherSelectButton.Click += new System.EventHandler(this.TeacherSelectButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(784, 661);
            this.Controls.Add(this.TeacherSelectButton);
            this.Controls.Add(this.StudentSelectButton);
            this.MaximumSize = new System.Drawing.Size(800, 700);
            this.MinimumSize = new System.Drawing.Size(800, 700);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.Button TeacherSelectButton;
        private System.Windows.Forms.Button StudentSelectButton;
    }
}


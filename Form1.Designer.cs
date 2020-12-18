namespace ChristmasWallpaper
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.removeStartupBtn = new System.Windows.Forms.Button();
            this.datesGroupBox = new System.Windows.Forms.GroupBox();
            this.endDatePicker = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.startDatePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.saveChangesButton = new System.Windows.Forms.Button();
            this.cancelChangesButton = new System.Windows.Forms.Button();
            this.datesGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // removeStartupBtn
            // 
            this.removeStartupBtn.Location = new System.Drawing.Point(21, 214);
            this.removeStartupBtn.Name = "removeStartupBtn";
            this.removeStartupBtn.Size = new System.Drawing.Size(130, 23);
            this.removeStartupBtn.TabIndex = 0;
            this.removeStartupBtn.Text = "Do not run on startup";
            this.removeStartupBtn.UseVisualStyleBackColor = true;
            this.removeStartupBtn.Click += new System.EventHandler(this.RemoveStartupBtn_Click);
            // 
            // datesGroupBox
            // 
            this.datesGroupBox.Controls.Add(this.cancelChangesButton);
            this.datesGroupBox.Controls.Add(this.saveChangesButton);
            this.datesGroupBox.Controls.Add(this.endDatePicker);
            this.datesGroupBox.Controls.Add(this.label2);
            this.datesGroupBox.Controls.Add(this.startDatePicker);
            this.datesGroupBox.Controls.Add(this.label1);
            this.datesGroupBox.Location = new System.Drawing.Point(12, 12);
            this.datesGroupBox.Name = "datesGroupBox";
            this.datesGroupBox.Size = new System.Drawing.Size(314, 147);
            this.datesGroupBox.TabIndex = 1;
            this.datesGroupBox.TabStop = false;
            this.datesGroupBox.Text = "Dates";
            // 
            // endDatePicker
            // 
            this.endDatePicker.Location = new System.Drawing.Point(79, 72);
            this.endDatePicker.Name = "endDatePicker";
            this.endDatePicker.Size = new System.Drawing.Size(200, 20);
            this.endDatePicker.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "End Date:";
            // 
            // startDatePicker
            // 
            this.startDatePicker.CustomFormat = "\"dd/mm\"";
            this.startDatePicker.Location = new System.Drawing.Point(79, 20);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.Size = new System.Drawing.Size(200, 20);
            this.startDatePicker.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Start Date:";
            // 
            // saveChangesButton
            // 
            this.saveChangesButton.Location = new System.Drawing.Point(233, 118);
            this.saveChangesButton.Name = "saveChangesButton";
            this.saveChangesButton.Size = new System.Drawing.Size(75, 23);
            this.saveChangesButton.TabIndex = 4;
            this.saveChangesButton.Text = "Save";
            this.saveChangesButton.UseVisualStyleBackColor = true;
            this.saveChangesButton.Click += new System.EventHandler(this.SaveChangesButton_Click);
            // 
            // cancelChangesButton
            // 
            this.cancelChangesButton.Location = new System.Drawing.Point(152, 118);
            this.cancelChangesButton.Name = "cancelChangesButton";
            this.cancelChangesButton.Size = new System.Drawing.Size(75, 23);
            this.cancelChangesButton.TabIndex = 5;
            this.cancelChangesButton.Text = "Cancel";
            this.cancelChangesButton.UseVisualStyleBackColor = true;
            this.cancelChangesButton.Click += new System.EventHandler(this.CancelChangesButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 249);
            this.Controls.Add(this.datesGroupBox);
            this.Controls.Add(this.removeStartupBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "ChristmasWallpaper Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.datesGroupBox.ResumeLayout(false);
            this.datesGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button removeStartupBtn;
        private System.Windows.Forms.GroupBox datesGroupBox;
        private System.Windows.Forms.DateTimePicker startDatePicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker endDatePicker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cancelChangesButton;
        private System.Windows.Forms.Button saveChangesButton;
    }
}


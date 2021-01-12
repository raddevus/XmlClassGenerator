namespace XMLClassGenerator
{
    partial class MainForm
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
            this.fileSelectorCtrl = new QueryXML.FileSelector();
            this.ProcessXMLButton = new System.Windows.Forms.Button();
            this.NamespaceTextBox = new System.Windows.Forms.TextBox();
            this.TargetFileTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // fileSelectorCtrl
            // 
            this.fileSelectorCtrl.Location = new System.Drawing.Point(11, 40);
            this.fileSelectorCtrl.Name = "fileSelectorCtrl";
            this.fileSelectorCtrl.Size = new System.Drawing.Size(378, 28);
            this.fileSelectorCtrl.TabIndex = 0;
            // 
            // ProcessXMLButton
            // 
            this.ProcessXMLButton.Location = new System.Drawing.Point(256, 197);
            this.ProcessXMLButton.Name = "ProcessXMLButton";
            this.ProcessXMLButton.Size = new System.Drawing.Size(88, 23);
            this.ProcessXMLButton.TabIndex = 1;
            this.ProcessXMLButton.Text = "Process XML";
            this.ProcessXMLButton.UseVisualStyleBackColor = true;
            this.ProcessXMLButton.Click += new System.EventHandler(this.ProcessXMLButton_Click);
            // 
            // NamespaceTextBox
            // 
            this.NamespaceTextBox.Location = new System.Drawing.Point(11, 93);
            this.NamespaceTextBox.Name = "NamespaceTextBox";
            this.NamespaceTextBox.Size = new System.Drawing.Size(222, 20);
            this.NamespaceTextBox.TabIndex = 2;
            // 
            // TargetFileTextBox
            // 
            this.TargetFileTextBox.Location = new System.Drawing.Point(11, 154);
            this.TargetFileTextBox.Name = "TargetFileTextBox";
            this.TargetFileTextBox.Size = new System.Drawing.Size(333, 20);
            this.TargetFileTextBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Namespace:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "CSharp Target Filename (.cs):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Source File (xml):";
            // 
            // MainForm
            // 
            this.AcceptButton = this.ProcessXMLButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 231);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TargetFileTextBox);
            this.Controls.Add(this.NamespaceTextBox);
            this.Controls.Add(this.ProcessXMLButton);
            this.Controls.Add(this.fileSelectorCtrl);
            this.Name = "MainForm";
            this.Text = "XML Class Generator";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private QueryXML.FileSelector fileSelectorCtrl;
        private System.Windows.Forms.Button ProcessXMLButton;
        private System.Windows.Forms.TextBox NamespaceTextBox;
        private System.Windows.Forms.TextBox TargetFileTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}


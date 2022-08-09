namespace PascalTranslator
{
    partial class MainForm
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
            this.buttonAnalise = new System.Windows.Forms.Button();
            this.tbInput = new System.Windows.Forms.TextBox();
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.tbKeyWords = new System.Windows.Forms.TextBox();
            this.tbVariables = new System.Windows.Forms.TextBox();
            this.tbNumbers = new System.Windows.Forms.TextBox();
            this.tbSeparators = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tbBauerOuput = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonAnalise
            // 
            this.buttonAnalise.Location = new System.Drawing.Point(7, 9);
            this.buttonAnalise.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAnalise.Name = "buttonAnalise";
            this.buttonAnalise.Size = new System.Drawing.Size(245, 36);
            this.buttonAnalise.TabIndex = 1;
            this.buttonAnalise.Text = "Анализировать";
            this.buttonAnalise.UseVisualStyleBackColor = true;
            this.buttonAnalise.Click += new System.EventHandler(this.buttonAnalise_Click);
            // 
            // tbInput
            // 
            this.tbInput.Location = new System.Drawing.Point(13, 32);
            this.tbInput.Margin = new System.Windows.Forms.Padding(4);
            this.tbInput.Multiline = true;
            this.tbInput.Name = "tbInput";
            this.tbInput.Size = new System.Drawing.Size(424, 370);
            this.tbInput.TabIndex = 2;
            // 
            // tbOutput
            // 
            this.tbOutput.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbOutput.Location = new System.Drawing.Point(7, 97);
            this.tbOutput.Margin = new System.Windows.Forms.Padding(4);
            this.tbOutput.Multiline = true;
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.ReadOnly = true;
            this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbOutput.Size = new System.Drawing.Size(245, 252);
            this.tbOutput.TabIndex = 3;
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(7, 53);
            this.buttonLoad.Margin = new System.Windows.Forms.Padding(4);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(245, 36);
            this.buttonLoad.TabIndex = 4;
            this.buttonLoad.Text = "Загрузить файл";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // tbKeyWords
            // 
            this.tbKeyWords.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbKeyWords.Location = new System.Drawing.Point(260, 9);
            this.tbKeyWords.Margin = new System.Windows.Forms.Padding(4);
            this.tbKeyWords.Multiline = true;
            this.tbKeyWords.Name = "tbKeyWords";
            this.tbKeyWords.ReadOnly = true;
            this.tbKeyWords.Size = new System.Drawing.Size(86, 340);
            this.tbKeyWords.TabIndex = 7;
            // 
            // tbVariables
            // 
            this.tbVariables.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbVariables.Location = new System.Drawing.Point(448, 9);
            this.tbVariables.Margin = new System.Windows.Forms.Padding(4);
            this.tbVariables.Multiline = true;
            this.tbVariables.Name = "tbVariables";
            this.tbVariables.ReadOnly = true;
            this.tbVariables.Size = new System.Drawing.Size(86, 340);
            this.tbVariables.TabIndex = 8;
            // 
            // tbNumbers
            // 
            this.tbNumbers.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbNumbers.Location = new System.Drawing.Point(542, 9);
            this.tbNumbers.Margin = new System.Windows.Forms.Padding(4);
            this.tbNumbers.Multiline = true;
            this.tbNumbers.Name = "tbNumbers";
            this.tbNumbers.ReadOnly = true;
            this.tbNumbers.Size = new System.Drawing.Size(86, 340);
            this.tbNumbers.TabIndex = 9;
            // 
            // tbSeparators
            // 
            this.tbSeparators.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbSeparators.Location = new System.Drawing.Point(354, 9);
            this.tbSeparators.Margin = new System.Windows.Forms.Padding(4);
            this.tbSeparators.Multiline = true;
            this.tbSeparators.Name = "tbSeparators";
            this.tbSeparators.ReadOnly = true;
            this.tbSeparators.Size = new System.Drawing.Size(86, 340);
            this.tbSeparators.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(42, 353);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 18);
            this.label1.TabIndex = 11;
            this.label1.Text = "Результирующая таблица";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(566, 353);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "Числа";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(253, 352);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 17);
            this.label3.TabIndex = 13;
            this.label3.Text = "Ключевые слова";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(461, 353);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 17);
            this.label4.TabIndex = 14;
            this.label4.Text = "Переменные";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(366, 352);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 17);
            this.label5.TabIndex = 15;
            this.label5.Text = "Разделители";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tbBauerOuput);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(637, 377);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Таблица арифметического разбора";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tbBauerOuput
            // 
            this.tbBauerOuput.BackColor = System.Drawing.SystemColors.Window;
            this.tbBauerOuput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbBauerOuput.Location = new System.Drawing.Point(6, 6);
            this.tbBauerOuput.Multiline = true;
            this.tbBauerOuput.Name = "tbBauerOuput";
            this.tbBauerOuput.ReadOnly = true;
            this.tbBauerOuput.Size = new System.Drawing.Size(625, 359);
            this.tbBauerOuput.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(444, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(645, 406);
            this.tabControl1.TabIndex = 16;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.buttonAnalise);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.tbOutput);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.buttonLoad);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.tbKeyWords);
            this.tabPage1.Controls.Add(this.tbSeparators);
            this.tabPage1.Controls.Add(this.tbVariables);
            this.tabPage1.Controls.Add(this.tbNumbers);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(637, 377);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Основная форма";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1094, 414);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.tbInput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAnalise;
        private System.Windows.Forms.TextBox tbInput;
        private System.Windows.Forms.TextBox tbOutput;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.TextBox tbKeyWords;
        private System.Windows.Forms.TextBox tbVariables;
        private System.Windows.Forms.TextBox tbNumbers;
        private System.Windows.Forms.TextBox tbSeparators;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox tbBauerOuput;
    }
}


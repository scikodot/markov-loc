namespace MarkovLS
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.generate = new System.Windows.Forms.Button();
            this.clear = new System.Windows.Forms.Button();
            this.width_set = new System.Windows.Forms.TextBox();
            this.height_set = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.title = new System.Windows.Forms.Label();
            this.place_robot = new System.Windows.Forms.Button();
            this.help = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.color = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pHit_set = new System.Windows.Forms.TextBox();
            this.pMiss_set = new System.Windows.Forms.TextBox();
            this.pExact_set = new System.Windows.Forms.TextBox();
            this.pUndershoot_set = new System.Windows.Forms.TextBox();
            this.pOvershoot_set = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.SenseProbs_set = new System.Windows.Forms.Button();
            this.MoveProbs_set = new System.Windows.Forms.Button();
            this.pHit_label = new System.Windows.Forms.Label();
            this.pMiss_label = new System.Windows.Forms.Label();
            this.pExact_label = new System.Windows.Forms.Label();
            this.pUndershoot_label = new System.Windows.Forms.Label();
            this.pOvershoot_label = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.dist = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.dist_set = new System.Windows.Forms.Button();
            this.distance_label = new System.Windows.Forms.Label();
            this.fill_default = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // generate
            // 
            this.generate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.generate.Location = new System.Drawing.Point(1075, 113);
            this.generate.Name = "generate";
            this.generate.Size = new System.Drawing.Size(75, 23);
            this.generate.TabIndex = 0;
            this.generate.Text = "Generate";
            this.generate.UseVisualStyleBackColor = true;
            this.generate.Click += new System.EventHandler(this.generate_Click);
            // 
            // clear
            // 
            this.clear.ForeColor = System.Drawing.SystemColors.ControlText;
            this.clear.Location = new System.Drawing.Point(1075, 171);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(75, 23);
            this.clear.TabIndex = 3;
            this.clear.Text = "Clear";
            this.clear.UseVisualStyleBackColor = true;
            this.clear.Click += new System.EventHandler(this.clear_Click);
            // 
            // width_set
            // 
            this.width_set.Location = new System.Drawing.Point(1062, 61);
            this.width_set.Name = "width_set";
            this.width_set.Size = new System.Drawing.Size(100, 22);
            this.width_set.TabIndex = 4;
            // 
            // height_set
            // 
            this.height_set.Location = new System.Drawing.Point(1062, 87);
            this.height_set.Name = "height_set";
            this.height_set.Size = new System.Drawing.Size(100, 22);
            this.height_set.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(1026, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "W:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(1026, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "H:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // title
            // 
            this.title.Location = new System.Drawing.Point(1032, 5);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(152, 53);
            this.title.TabIndex = 8;
            this.title.Text = "Enter width and height\r\n(WxH >= 4; W, H <= 15)";
            this.title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // place_robot
            // 
            this.place_robot.Location = new System.Drawing.Point(1075, 142);
            this.place_robot.Name = "place_robot";
            this.place_robot.Size = new System.Drawing.Size(75, 23);
            this.place_robot.TabIndex = 9;
            this.place_robot.Text = "Place robot";
            this.place_robot.UseVisualStyleBackColor = true;
            this.place_robot.Click += new System.EventHandler(this.place_robot_Click);
            // 
            // help
            // 
            this.help.Location = new System.Drawing.Point(784, 491);
            this.help.Name = "help";
            this.help.Size = new System.Drawing.Size(400, 286);
            this.help.TabIndex = 10;
            this.help.Text = resources.GetString("help.Text");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(784, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Current tile color:";
            // 
            // color
            // 
            this.color.AutoSize = true;
            this.color.Location = new System.Drawing.Point(876, 162);
            this.color.Name = "color";
            this.color.Size = new System.Drawing.Size(40, 17);
            this.color.TabIndex = 12;
            this.color.Text = "none";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(1040, 210);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(144, 40);
            this.label4.TabIndex = 13;
            this.label4.Text = "Sensing probabilities\r\n(pH, pM >= 0; ∑ = 1)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pHit_set
            // 
            this.pHit_set.Location = new System.Drawing.Point(1062, 253);
            this.pHit_set.Name = "pHit_set";
            this.pHit_set.Size = new System.Drawing.Size(100, 22);
            this.pHit_set.TabIndex = 14;
            // 
            // pMiss_set
            // 
            this.pMiss_set.Location = new System.Drawing.Point(1062, 279);
            this.pMiss_set.Name = "pMiss_set";
            this.pMiss_set.Size = new System.Drawing.Size(100, 22);
            this.pMiss_set.TabIndex = 15;
            // 
            // pExact_set
            // 
            this.pExact_set.Location = new System.Drawing.Point(1062, 387);
            this.pExact_set.Name = "pExact_set";
            this.pExact_set.Size = new System.Drawing.Size(100, 22);
            this.pExact_set.TabIndex = 16;
            // 
            // pUndershoot_set
            // 
            this.pUndershoot_set.Location = new System.Drawing.Point(1062, 413);
            this.pUndershoot_set.Name = "pUndershoot_set";
            this.pUndershoot_set.Size = new System.Drawing.Size(100, 22);
            this.pUndershoot_set.TabIndex = 17;
            // 
            // pOvershoot_set
            // 
            this.pOvershoot_set.Location = new System.Drawing.Point(1062, 439);
            this.pOvershoot_set.Name = "pOvershoot_set";
            this.pOvershoot_set.Size = new System.Drawing.Size(100, 22);
            this.pOvershoot_set.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(1016, 253);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 20);
            this.label5.TabIndex = 19;
            this.label5.Text = "pHit:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(1016, 279);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 20);
            this.label6.TabIndex = 20;
            this.label6.Text = "pMiss:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(1040, 344);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(144, 40);
            this.label7.TabIndex = 21;
            this.label7.Text = "Moving probabilities\r\n(pE, pU, pO >= 0; ∑ = 1)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(976, 387);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 20);
            this.label8.TabIndex = 22;
            this.label8.Text = "pExact:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(976, 413);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 20);
            this.label9.TabIndex = 23;
            this.label9.Text = "pUndershoot:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(976, 439);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 20);
            this.label10.TabIndex = 24;
            this.label10.Text = "pOvershoot:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // SenseProbs_set
            // 
            this.SenseProbs_set.Location = new System.Drawing.Point(1075, 305);
            this.SenseProbs_set.Name = "SenseProbs_set";
            this.SenseProbs_set.Size = new System.Drawing.Size(75, 23);
            this.SenseProbs_set.TabIndex = 25;
            this.SenseProbs_set.Text = "Set";
            this.SenseProbs_set.UseVisualStyleBackColor = true;
            this.SenseProbs_set.Click += new System.EventHandler(this.SenseProbs_set_Click);
            // 
            // MoveProbs_set
            // 
            this.MoveProbs_set.Location = new System.Drawing.Point(1075, 465);
            this.MoveProbs_set.Name = "MoveProbs_set";
            this.MoveProbs_set.Size = new System.Drawing.Size(75, 23);
            this.MoveProbs_set.TabIndex = 26;
            this.MoveProbs_set.Text = "Set";
            this.MoveProbs_set.UseVisualStyleBackColor = true;
            this.MoveProbs_set.Click += new System.EventHandler(this.MoveProbs_set_Click);
            // 
            // pHit_label
            // 
            this.pHit_label.AutoSize = true;
            this.pHit_label.Location = new System.Drawing.Point(841, 22);
            this.pHit_label.Name = "pHit_label";
            this.pHit_label.Size = new System.Drawing.Size(37, 17);
            this.pHit_label.TabIndex = 27;
            this.pHit_label.Text = "pHit:";
            this.pHit_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pMiss_label
            // 
            this.pMiss_label.AutoSize = true;
            this.pMiss_label.Location = new System.Drawing.Point(833, 42);
            this.pMiss_label.Name = "pMiss_label";
            this.pMiss_label.Size = new System.Drawing.Size(48, 17);
            this.pMiss_label.TabIndex = 28;
            this.pMiss_label.Text = "pMiss:";
            this.pMiss_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pExact_label
            // 
            this.pExact_label.AutoSize = true;
            this.pExact_label.Location = new System.Drawing.Point(827, 62);
            this.pExact_label.Name = "pExact_label";
            this.pExact_label.Size = new System.Drawing.Size(54, 17);
            this.pExact_label.TabIndex = 29;
            this.pExact_label.Text = "pExact:";
            this.pExact_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pUndershoot_label
            // 
            this.pUndershoot_label.AutoSize = true;
            this.pUndershoot_label.Location = new System.Drawing.Point(799, 82);
            this.pUndershoot_label.Name = "pUndershoot_label";
            this.pUndershoot_label.Size = new System.Drawing.Size(94, 17);
            this.pUndershoot_label.TabIndex = 30;
            this.pUndershoot_label.Text = "pUndershoot:";
            this.pUndershoot_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pOvershoot_label
            // 
            this.pOvershoot_label.AutoSize = true;
            this.pOvershoot_label.Location = new System.Drawing.Point(805, 102);
            this.pOvershoot_label.Name = "pOvershoot_label";
            this.pOvershoot_label.Size = new System.Drawing.Size(86, 17);
            this.pOvershoot_label.TabIndex = 31;
            this.pOvershoot_label.Text = "pOvershoot:";
            this.pOvershoot_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(818, 210);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(144, 40);
            this.label11.TabIndex = 32;
            this.label11.Text = "Presence distance\r\n(d > 0)";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dist
            // 
            this.dist.Location = new System.Drawing.Point(840, 253);
            this.dist.Name = "dist";
            this.dist.Size = new System.Drawing.Size(100, 22);
            this.dist.TabIndex = 33;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(784, 252);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(50, 20);
            this.label12.TabIndex = 34;
            this.label12.Text = "distance:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dist_set
            // 
            this.dist_set.Location = new System.Drawing.Point(853, 279);
            this.dist_set.Name = "dist_set";
            this.dist_set.Size = new System.Drawing.Size(75, 23);
            this.dist_set.TabIndex = 35;
            this.dist_set.Text = "Set";
            this.dist_set.UseVisualStyleBackColor = true;
            this.dist_set.Click += new System.EventHandler(this.dist_set_Click);
            // 
            // distance_label
            // 
            this.distance_label.AutoSize = true;
            this.distance_label.Location = new System.Drawing.Point(820, 123);
            this.distance_label.Name = "distance_label";
            this.distance_label.Size = new System.Drawing.Size(65, 17);
            this.distance_label.TabIndex = 36;
            this.distance_label.Text = "distance:";
            this.distance_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // fill_default
            // 
            this.fill_default.Location = new System.Drawing.Point(853, 385);
            this.fill_default.Name = "fill_default";
            this.fill_default.Size = new System.Drawing.Size(75, 75);
            this.fill_default.TabIndex = 37;
            this.fill_default.Text = "Fill default values";
            this.fill_default.UseVisualStyleBackColor = true;
            this.fill_default.Click += new System.EventHandler(this.fill_default_Click);
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1196, 786);
            this.Controls.Add(this.fill_default);
            this.Controls.Add(this.distance_label);
            this.Controls.Add(this.dist_set);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.dist);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.pOvershoot_label);
            this.Controls.Add(this.pUndershoot_label);
            this.Controls.Add(this.pExact_label);
            this.Controls.Add(this.pMiss_label);
            this.Controls.Add(this.pHit_label);
            this.Controls.Add(this.MoveProbs_set);
            this.Controls.Add(this.SenseProbs_set);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pOvershoot_set);
            this.Controls.Add(this.pUndershoot_set);
            this.Controls.Add(this.pExact_set);
            this.Controls.Add(this.pMiss_set);
            this.Controls.Add(this.pHit_set);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.color);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.help);
            this.Controls.Add(this.place_robot);
            this.Controls.Add(this.title);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.height_set);
            this.Controls.Add(this.width_set);
            this.Controls.Add(this.clear);
            this.Controls.Add(this.generate);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button generate;
        private System.Windows.Forms.Button clear;
        private System.Windows.Forms.TextBox width_set;
        private System.Windows.Forms.TextBox height_set;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Button place_robot;
        private System.Windows.Forms.Label help;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label color;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox pHit_set;
        private System.Windows.Forms.TextBox pMiss_set;
        private System.Windows.Forms.TextBox pExact_set;
        private System.Windows.Forms.TextBox pUndershoot_set;
        private System.Windows.Forms.TextBox pOvershoot_set;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button SenseProbs_set;
        private System.Windows.Forms.Button MoveProbs_set;
        private System.Windows.Forms.Label pHit_label;
        private System.Windows.Forms.Label pMiss_label;
        private System.Windows.Forms.Label pExact_label;
        private System.Windows.Forms.Label pUndershoot_label;
        private System.Windows.Forms.Label pOvershoot_label;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox dist;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button dist_set;
        private System.Windows.Forms.Label distance_label;
        private System.Windows.Forms.Button fill_default;
    }
}


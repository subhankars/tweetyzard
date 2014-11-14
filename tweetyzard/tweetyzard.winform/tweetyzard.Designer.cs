namespace tweetyzard.utility
{
    partial class tweetyzard
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(tweetyzard));
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.userNameLabel = new System.Windows.Forms.Label();
            this.versionInfo = new System.Windows.Forms.Label();
            this.thisTimer = new System.Windows.Forms.Timer(this.components);
            this.currentTime = new System.Windows.Forms.Label();
            this.backgroundWorkerSearch = new System.ComponentModel.BackgroundWorker();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pbUser = new System.Windows.Forms.PictureBox();
            this.toolTipTw = new System.Windows.Forms.ToolTip(this.components);
            this.btnExport = new System.Windows.Forms.Button();
            this.ExportToDb = new System.Windows.Forms.Button();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.StopStreamButton = new System.Windows.Forms.Button();
            this.StartStreamButton = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.geoFlag = new System.Windows.Forms.CheckBox();
            this.searchPhraseTextBox = new MetroFramework.Controls.MetroTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tweetCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblStream = new System.Windows.Forms.Label();
            this.tweetsTextBox = new System.Windows.Forms.TextBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.CopyTrendToSearch = new System.Windows.Forms.Button();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.trendTimer = new System.Windows.Forms.Timer(this.components);
            this.trendingTopics = new System.Windows.Forms.Label();
            this.trendRefreshTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(232, 65);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // userNameLabel
            // 
            this.userNameLabel.AutoSize = true;
            this.userNameLabel.Font = new System.Drawing.Font("Segoe UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userNameLabel.ForeColor = System.Drawing.Color.DimGray;
            this.userNameLabel.Location = new System.Drawing.Point(593, 12);
            this.userNameLabel.Name = "userNameLabel";
            this.userNameLabel.Size = new System.Drawing.Size(69, 17);
            this.userNameLabel.TabIndex = 13;
            this.userNameLabel.Text = "User Name";
            // 
            // versionInfo
            // 
            this.versionInfo.AutoSize = true;
            this.versionInfo.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versionInfo.ForeColor = System.Drawing.Color.DimGray;
            this.versionInfo.Location = new System.Drawing.Point(24, 564);
            this.versionInfo.Name = "versionInfo";
            this.versionInfo.Size = new System.Drawing.Size(31, 13);
            this.versionInfo.TabIndex = 17;
            this.versionInfo.Text = "v. 0.0";
            // 
            // thisTimer
            // 
            this.thisTimer.Enabled = true;
            this.thisTimer.Interval = 1000;
            this.thisTimer.Tick += new System.EventHandler(this.thisTimer_Tick);
            // 
            // currentTime
            // 
            this.currentTime.AutoSize = true;
            this.currentTime.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentTime.ForeColor = System.Drawing.Color.DimGray;
            this.currentTime.Location = new System.Drawing.Point(572, 563);
            this.currentTime.Name = "currentTime";
            this.currentTime.Size = new System.Drawing.Size(37, 13);
            this.currentTime.TabIndex = 18;
            this.currentTime.Text = "Now is";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(556, 563);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.TabIndex = 19;
            this.pictureBox2.TabStop = false;
            // 
            // pbUser
            // 
            this.pbUser.Image = ((System.Drawing.Image)(resources.GetObject("pbUser.Image")));
            this.pbUser.Location = new System.Drawing.Point(575, 13);
            this.pbUser.Name = "pbUser";
            this.pbUser.Size = new System.Drawing.Size(16, 16);
            this.pbUser.TabIndex = 20;
            this.pbUser.TabStop = false;
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.Transparent;
            this.btnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExport.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.ForeColor = System.Drawing.Color.Transparent;
            this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
            this.btnExport.Location = new System.Drawing.Point(726, 88);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(25, 25);
            this.btnExport.TabIndex = 35;
            this.toolTipTw.SetToolTip(this.btnExport, "Export to Excel");
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // ExportToDb
            // 
            this.ExportToDb.BackColor = System.Drawing.Color.Transparent;
            this.ExportToDb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ExportToDb.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.ExportToDb.FlatAppearance.BorderSize = 0;
            this.ExportToDb.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.ExportToDb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExportToDb.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExportToDb.ForeColor = System.Drawing.Color.Transparent;
            this.ExportToDb.Image = ((System.Drawing.Image)(resources.GetObject("ExportToDb.Image")));
            this.ExportToDb.Location = new System.Drawing.Point(757, 88);
            this.ExportToDb.Name = "ExportToDb";
            this.ExportToDb.Size = new System.Drawing.Size(25, 25);
            this.ExportToDb.TabIndex = 36;
            this.toolTipTw.SetToolTip(this.ExportToDb, "Export to Database");
            this.ExportToDb.UseVisualStyleBackColor = false;
            this.ExportToDb.Click += new System.EventHandler(this.ExportToDb_Click);
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(7, 562);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(16, 16);
            this.pictureBox5.TabIndex = 21;
            this.pictureBox5.TabStop = false;
            // 
            // StopStreamButton
            // 
            this.StopStreamButton.BackColor = System.Drawing.Color.White;
            this.StopStreamButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.StopStreamButton.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.StopStreamButton.FlatAppearance.BorderSize = 0;
            this.StopStreamButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.StopStreamButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StopStreamButton.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StopStreamButton.ForeColor = System.Drawing.Color.Transparent;
            this.StopStreamButton.Image = ((System.Drawing.Image)(resources.GetObject("StopStreamButton.Image")));
            this.StopStreamButton.Location = new System.Drawing.Point(673, 88);
            this.StopStreamButton.Name = "StopStreamButton";
            this.StopStreamButton.Size = new System.Drawing.Size(25, 25);
            this.StopStreamButton.TabIndex = 27;
            this.StopStreamButton.UseVisualStyleBackColor = false;
            this.StopStreamButton.Click += new System.EventHandler(this.StopStreamButton_Click);
            // 
            // StartStreamButton
            // 
            this.StartStreamButton.BackColor = System.Drawing.Color.White;
            this.StartStreamButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.StartStreamButton.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.StartStreamButton.FlatAppearance.BorderSize = 0;
            this.StartStreamButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.StartStreamButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartStreamButton.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartStreamButton.ForeColor = System.Drawing.Color.Transparent;
            this.StartStreamButton.Image = ((System.Drawing.Image)(resources.GetObject("StartStreamButton.Image")));
            this.StartStreamButton.Location = new System.Drawing.Point(645, 88);
            this.StartStreamButton.Name = "StartStreamButton";
            this.StartStreamButton.Size = new System.Drawing.Size(25, 25);
            this.StartStreamButton.TabIndex = 26;
            this.StartStreamButton.Tag = "";
            this.StartStreamButton.UseVisualStyleBackColor = false;
            this.StartStreamButton.Click += new System.EventHandler(this.StartStreamClicked);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.White;
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.Transparent;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(613, 88);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(25, 25);
            this.btnSearch.TabIndex = 33;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.White;
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.ForeColor = System.Drawing.Color.Transparent;
            this.btnReset.Image = ((System.Drawing.Image)(resources.GetObject("btnReset.Image")));
            this.btnReset.Location = new System.Drawing.Point(700, 88);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(25, 25);
            this.btnReset.TabIndex = 32;
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // geoFlag
            // 
            this.geoFlag.AutoSize = true;
            this.geoFlag.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.geoFlag.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.geoFlag.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.geoFlag.Image = ((System.Drawing.Image)(resources.GetObject("geoFlag.Image")));
            this.geoFlag.Location = new System.Drawing.Point(577, 93);
            this.geoFlag.Name = "geoFlag";
            this.geoFlag.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.geoFlag.Size = new System.Drawing.Size(28, 16);
            this.geoFlag.TabIndex = 31;
            this.geoFlag.UseVisualStyleBackColor = true;
            // 
            // searchPhraseTextBox
            // 
            this.searchPhraseTextBox.Lines = new string[0];
            this.searchPhraseTextBox.Location = new System.Drawing.Point(577, 65);
            this.searchPhraseTextBox.MaxLength = 32767;
            this.searchPhraseTextBox.Name = "searchPhraseTextBox";
            this.searchPhraseTextBox.PasswordChar = '\0';
            this.searchPhraseTextBox.PromptText = "Search keyword";
            this.searchPhraseTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.searchPhraseTextBox.SelectedText = "";
            this.searchPhraseTextBox.Size = new System.Drawing.Size(208, 22);
            this.searchPhraseTextBox.Style = MetroFramework.MetroColorStyle.Teal;
            this.searchPhraseTextBox.TabIndex = 34;
            this.searchPhraseTextBox.UseSelectable = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.tweetCount);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblStream);
            this.panel1.Controls.Add(this.tweetsTextBox);
            this.panel1.Location = new System.Drawing.Point(7, 119);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(781, 437);
            this.panel1.TabIndex = 37;
            // 
            // tweetCount
            // 
            this.tweetCount.AutoSize = true;
            this.tweetCount.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tweetCount.Location = new System.Drawing.Point(744, 7);
            this.tweetCount.Name = "tweetCount";
            this.tweetCount.Size = new System.Drawing.Size(13, 13);
            this.tweetCount.TabIndex = 22;
            this.tweetCount.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(668, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Tweet Count :";
            // 
            // lblStream
            // 
            this.lblStream.AutoSize = true;
            this.lblStream.Font = new System.Drawing.Font("Segoe UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStream.Location = new System.Drawing.Point(4, 5);
            this.lblStream.Name = "lblStream";
            this.lblStream.Size = new System.Drawing.Size(72, 17);
            this.lblStream.TabIndex = 20;
            this.lblStream.Text = "Live Stream";
            // 
            // tweetsTextBox
            // 
            this.tweetsTextBox.BackColor = System.Drawing.Color.Teal;
            this.tweetsTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tweetsTextBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tweetsTextBox.ForeColor = System.Drawing.Color.White;
            this.tweetsTextBox.Location = new System.Drawing.Point(6, 24);
            this.tweetsTextBox.Multiline = true;
            this.tweetsTextBox.Name = "tweetsTextBox";
            this.tweetsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tweetsTextBox.Size = new System.Drawing.Size(770, 410);
            this.tweetsTextBox.TabIndex = 19;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(167, 22);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(23, 20);
            this.pictureBox3.TabIndex = 38;
            this.pictureBox3.TabStop = false;
            // 
            // CopyTrendToSearch
            // 
            this.CopyTrendToSearch.BackColor = System.Drawing.Color.White;
            this.CopyTrendToSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CopyTrendToSearch.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.CopyTrendToSearch.FlatAppearance.BorderSize = 0;
            this.CopyTrendToSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.CopyTrendToSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CopyTrendToSearch.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CopyTrendToSearch.ForeColor = System.Drawing.Color.Transparent;
            this.CopyTrendToSearch.Image = ((System.Drawing.Image)(resources.GetObject("CopyTrendToSearch.Image")));
            this.CopyTrendToSearch.Location = new System.Drawing.Point(7, 88);
            this.CopyTrendToSearch.Name = "CopyTrendToSearch";
            this.CopyTrendToSearch.Size = new System.Drawing.Size(25, 25);
            this.CopyTrendToSearch.TabIndex = 39;
            this.toolTipTw.SetToolTip(this.CopyTrendToSearch, "Send this topic for search");
            this.CopyTrendToSearch.UseVisualStyleBackColor = false;
            this.CopyTrendToSearch.Click += new System.EventHandler(this.CopyTrendToSearch_Click);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.LabelMode = MetroFramework.Controls.MetroLabelMode.Selectable;
            this.metroLabel1.Location = new System.Drawing.Point(35, 91);
            this.metroLabel1.MaximumSize = new System.Drawing.Size(200, 0);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(99, 19);
            this.metroLabel1.TabIndex = 40;
            this.metroLabel1.Text = "Trending Now: ";
            // 
            // trendTimer
            // 
            this.trendTimer.Enabled = true;
            this.trendTimer.Interval = 2500;
            this.trendTimer.Tick += new System.EventHandler(this.trendTimer_Tick);
            // 
            // trendingTopics
            // 
            this.trendingTopics.AutoSize = true;
            this.trendingTopics.Font = new System.Drawing.Font("Segoe WP SemiLight", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trendingTopics.ForeColor = System.Drawing.Color.Teal;
            this.trendingTopics.Location = new System.Drawing.Point(130, 92);
            this.trendingTopics.Name = "trendingTopics";
            this.trendingTopics.Size = new System.Drawing.Size(83, 17);
            this.trendingTopics.TabIndex = 41;
            this.trendingTopics.Text = "Trending now";
            // 
            // trendRefreshTimer
            // 
            this.trendRefreshTimer.Enabled = true;
            this.trendRefreshTimer.Interval = 27000;
            this.trendRefreshTimer.Tick += new System.EventHandler(this.trendRefreshTimer_Tick);
            // 
            // tweetyzard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 582);
            this.Controls.Add(this.trendingTopics);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.CopyTrendToSearch);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ExportToDb);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.searchPhraseTextBox);
            this.Controls.Add(this.StopStreamButton);
            this.Controls.Add(this.StartStreamButton);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.pbUser);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.geoFlag);
            this.Controls.Add(this.currentTime);
            this.Controls.Add(this.versionInfo);
            this.Controls.Add(this.userNameLabel);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "tweetyzard";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Text = "tweetyzard";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label userNameLabel;
        private System.Windows.Forms.Label versionInfo;
        private System.Windows.Forms.Timer thisTimer;
        private System.Windows.Forms.Label currentTime;
        private System.ComponentModel.BackgroundWorker backgroundWorkerSearch;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pbUser;
        private System.Windows.Forms.ToolTip toolTipTw;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Button StopStreamButton;
        private System.Windows.Forms.Button StartStreamButton;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.CheckBox geoFlag;
        private MetroFramework.Controls.MetroTextBox searchPhraseTextBox;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button ExportToDb;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label tweetCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblStream;
        private System.Windows.Forms.TextBox tweetsTextBox;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button CopyTrendToSearch;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.Timer trendTimer;
        private System.Windows.Forms.Label trendingTopics;
        private System.Windows.Forms.Timer trendRefreshTimer;

    }
}


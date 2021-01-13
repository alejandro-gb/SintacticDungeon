namespace Proyecto2
{
    partial class Principal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MSArchivo = new System.Windows.Forms.ToolStripMenuItem();
            this.MSOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.MSSave = new System.Windows.Forms.ToolStripMenuItem();
            this.MSSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.MSAnalizar = new System.Windows.Forms.ToolStripMenuItem();
            this.MSAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.MSExit = new System.Windows.Forms.ToolStripMenuItem();
            this.TextArea = new System.Windows.Forms.RichTextBox();
            this.tableroDeJuegoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MSArchivo,
            this.MSAnalizar,
            this.tableroDeJuegoToolStripMenuItem,
            this.MSAbout,
            this.MSExit});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(539, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MSArchivo
            // 
            this.MSArchivo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MSOpen,
            this.MSSave,
            this.MSSaveAs});
            this.MSArchivo.Name = "MSArchivo";
            this.MSArchivo.Size = new System.Drawing.Size(60, 20);
            this.MSArchivo.Text = "Archivo";
            // 
            // MSOpen
            // 
            this.MSOpen.Name = "MSOpen";
            this.MSOpen.Size = new System.Drawing.Size(152, 22);
            this.MSOpen.Text = "Abrir";
            this.MSOpen.Click += new System.EventHandler(this.MSOpen_Click);
            // 
            // MSSave
            // 
            this.MSSave.Name = "MSSave";
            this.MSSave.Size = new System.Drawing.Size(152, 22);
            this.MSSave.Text = "Guardar";
            this.MSSave.Click += new System.EventHandler(this.MSSave_Click);
            // 
            // MSSaveAs
            // 
            this.MSSaveAs.Name = "MSSaveAs";
            this.MSSaveAs.Size = new System.Drawing.Size(152, 22);
            this.MSSaveAs.Text = "Guardar Como";
            this.MSSaveAs.Click += new System.EventHandler(this.MSSaveAs_Click);
            // 
            // MSAnalizar
            // 
            this.MSAnalizar.Name = "MSAnalizar";
            this.MSAnalizar.Size = new System.Drawing.Size(61, 20);
            this.MSAnalizar.Text = "Analizar";
            this.MSAnalizar.Click += new System.EventHandler(this.MSAnalizar_Click);
            // 
            // MSAbout
            // 
            this.MSAbout.Name = "MSAbout";
            this.MSAbout.Size = new System.Drawing.Size(72, 20);
            this.MSAbout.Text = "Acerca De";
            this.MSAbout.Click += new System.EventHandler(this.MSAbout_Click);
            // 
            // MSExit
            // 
            this.MSExit.Name = "MSExit";
            this.MSExit.Size = new System.Drawing.Size(41, 20);
            this.MSExit.Text = "Salir";
            this.MSExit.Click += new System.EventHandler(this.MSExit_Click);
            // 
            // TextArea
            // 
            this.TextArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextArea.Location = new System.Drawing.Point(12, 28);
            this.TextArea.Name = "TextArea";
            this.TextArea.Size = new System.Drawing.Size(515, 327);
            this.TextArea.TabIndex = 1;
            this.TextArea.Text = "";
            // 
            // tableroDeJuegoToolStripMenuItem
            // 
            this.tableroDeJuegoToolStripMenuItem.Name = "tableroDeJuegoToolStripMenuItem";
            this.tableroDeJuegoToolStripMenuItem.Size = new System.Drawing.Size(108, 20);
            this.tableroDeJuegoToolStripMenuItem.Text = "Tablero de Juego";
            this.tableroDeJuegoToolStripMenuItem.Click += new System.EventHandler(this.tableroDeJuegoToolStripMenuItem_Click);
            // 
            // Principal
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 367);
            this.Controls.Add(this.TextArea);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Proyecto2";
            this.Load += new System.EventHandler(this.Principal_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MSArchivo;
        private System.Windows.Forms.ToolStripMenuItem MSOpen;
        private System.Windows.Forms.ToolStripMenuItem MSSave;
        private System.Windows.Forms.ToolStripMenuItem MSSaveAs;
        private System.Windows.Forms.ToolStripMenuItem MSAnalizar;
        private System.Windows.Forms.ToolStripMenuItem MSAbout;
        private System.Windows.Forms.ToolStripMenuItem MSExit;
        private System.Windows.Forms.RichTextBox TextArea;
        private System.Windows.Forms.ToolStripMenuItem tableroDeJuegoToolStripMenuItem;
    }
}


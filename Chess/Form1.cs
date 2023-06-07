using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chessVisualized
{
    public partial class Form1 : Form
    {
        public delegate void updateButtonsDelegate(int row, int col, int piece); 
        public updateButtonsDelegate updatebuttons; 
        public delegate void updategamestatDelegate(string stat, int whitetotal, int blacktotal); 
        public updategamestatDelegate updatestat; 
        public delegate void promoteDelegate();
        public promoteDelegate promote; 
        public delegate void promoteDelDelegate();
        public promoteDelDelegate promoteDelete; 
        public delegate void capturedDelegate(List<int> cw, List<int> cb); 
        public capturedDelegate captured; 
        public capturedDelegate delCaptured;
        Button[] bbuttons;
        Button[] wbuttons;
        void clearCaptured(List<int> cw, List<int> cb)
        {
            for (int i = 0; i < cw.Count; i++)
                Controls.Remove(wbuttons[i]);
            for (int i = 0; i < cb.Count; i++)
                Controls.Remove(bbuttons[i]);
        }
        void dc(List<int> cw, List<int> cb) 
        {
            cw.Sort();
            cb.Sort();
            cb.Reverse();
            bbuttons = new Button[cb.Count];
            for (int i = 0; i < cb.Count; i++)
            {
                bbuttons[i] = new Button();
                bbuttons[i].Size = new Size(30,30);
                bbuttons[i].Enabled = false;
                bbuttons[i].Location = new Point(80 + 30 * i, 5);
                switch (cb[i])
                {
                    case -1:
                        bbuttons[i].Image = Image.FromFile(@"..\..\bpc.png");
                        break;
                    case -2:
                        bbuttons[i].Image = Image.FromFile(@"..\..\brc.png");
                        break;
                    case -3:
                        bbuttons[i].Image = Image.FromFile(@"..\..\bknc.png");
                        break;
                    case -4:
                        bbuttons[i].Image = Image.FromFile(@"..\..\bbc.png");
                        break;
                    case -5:
                        bbuttons[i].Image = Image.FromFile(@"..\..\bqc.png");
                        break;
                }
                Controls.Add(bbuttons[i]);
            }
            wbuttons = new Button[cw.Count];
            for (int i = 0; i < cw.Count; i++)
            {
                wbuttons[i] = new Button();
                wbuttons[i].Size = new Size(30, 30);
                wbuttons[i].Enabled = false;
                wbuttons[i].Location = new Point(80 + 30 * i, 539);
                switch (cw[i])
                {
                    case 1:
                        wbuttons[i].Image = Image.FromFile(@"..\..\wpc.png");
                        break;
                    case 2:
                        wbuttons[i].Image = Image.FromFile(@"..\..\wrc.png");
                        break;
                    case 3:
                        wbuttons[i].Image = Image.FromFile(@"..\..\wknc.png");
                        break;
                    case 4:
                        wbuttons[i].Image = Image.FromFile(@"..\..\wbc.png");
                        break;
                    case 5:
                        wbuttons[i].Image = Image.FromFile(@"..\..\wqc.png");
                        break;
                }
                Controls.Add(wbuttons[i]);
            }
        }
        Button queen, rook, knight, bishop;
        void promo()
        {
            queen = new Button();
            queen.Text = "Bolorova";
            queen.Location = new Point(3, 80);
            queen.Name = "Queen";
            rook = new Button();
            rook.Text = "Rook";
            rook.Location = new Point(3, 110);
            rook.Name = "Rook";
            knight = new Button();
            knight.Text = "Knight";
            knight.Location = new Point(3, 140);
            knight.Name = "Knight";
            bishop = new Button();
            bishop.Text = "Bishop";
            bishop.Location = new Point(3, 170);
            bishop.Name = "Bishop";
            queen.BackColor = Color.White;
            rook.BackColor = Color.White;
            knight.BackColor = Color.White;
            bishop.BackColor = Color.White;
            Controls.Add(queen);
            Controls.Add(rook);
            Controls.Add(knight);
            Controls.Add(bishop);
            queen.Click += new System.EventHandler(b_Click);
            rook.Click += new System.EventHandler(b_Click);
            knight.Click += new System.EventHandler(b_Click);
            bishop.Click += new System.EventHandler(b_Click);
            init.Enabled = false;
            load.Enabled = false;
            save.Enabled = false;
            pictureBox1.Enabled = false;
        }
        void promoBegone()
        {
            Controls.Remove(queen);
            Controls.Remove(rook);
            Controls.Remove(knight);
            Controls.Remove(bishop);
            init.Enabled = true;
            load.Enabled = true;
            save.Enabled = true;
            pictureBox1.Enabled = true;
        }
        void uds(string stat, int whitetotal, int blacktotal)
        {
            gamestat.Text = stat;
            if (whitetotal > blacktotal)
            {
                whitescore.Text = "+" + (whitetotal - blacktotal).ToString();
                blackscore.Text = "";
            }
            else if (blacktotal > whitetotal)
            {
                blackscore.Text = "+" + (blacktotal - whitetotal).ToString();
                whitescore.Text = "";
            }
            else
            {
                whitescore.Text = "";
                blackscore.Text = "";
            }
            if (stat == "Checkmate" || stat == "Stalemate")
            {
                pictureBox1.Enabled = false;
            }
        }
        void udb(int row, int col, int piece)
        {
            switch (piece)
            {
                case -1:
                    cells[row,col].Image = bp;
                    break;
                case -2:
                    cells[row, col].Image = br;
                    break;
                case -3:
                    cells[row, col].Image = bkn;
                    break;
                case -4:
                    cells[row, col].Image = bb;
                    break;
                case -5:
                    cells[row, col].Image = bq;
                    break;
                case -6:
                    cells[row, col].Image = bk;
                    break;
                case 1:
                    cells[row, col].Image = wp;
                    break;
                case 2:
                    cells[row, col].Image = wr;
                    break;
                case 3:
                    cells[row, col].Image = wkn;
                    break;
                case 4:
                    cells[row, col].Image = wb;
                    break;
                case 5:
                    cells[row, col].Image = wq;
                    break;
                case 6:
                    cells[row, col].Image = wk;
                    break;
                default:
                    cells[row, col].Image = null;
                    break;
            }
        }
        Image bp = Image.FromFile(@"..\..\bp.png");
        Image br = Image.FromFile(@"..\..\br.png");
        Image bkn = Image.FromFile(@"..\..\bkn.png");
        Image bb = Image.FromFile(@"..\..\bb.png");
        Image bq = Image.FromFile(@"..\..\bq.png");
        Image bk = Image.FromFile(@"..\..\bk.png");
        Image wp = Image.FromFile(@"..\..\wp.png");
        Image wr = Image.FromFile(@"..\..\wr.png");
        Image wkn = Image.FromFile(@"..\..\wkn.png");
        Image wb = Image.FromFile(@"..\..\wb.png");
        Image wq = Image.FromFile(@"..\..\wq.png");
        Image wk = Image.FromFile(@"..\..\wk.png");
        public Form1()
        {
            InitializeComponent();
        }
        chess c1;
        Button[,] cells;
        private void Form1_Load(object sender, EventArgs e)
        {
            int num = 8;
            int cellheight, cellwidth;
            cellheight = cellwidth = (pictureBox1.Width / num) + 1;
            cells = new Button[num, num];
            for (int row = 0; row < num; row++)
                for (int col = 0; col < num; col++)
                {
                    cells[row, col] = new Button();
                    if (row == 7)
                    {
                        char c = (char)(col + 65);
                        cells[row, col].Text = c.ToString();
                    }
                    if (col == 0)
                        cells[row, col].Text += (8 - row).ToString();
                    cells[row, col].TextAlign = ContentAlignment.TopRight;
                    cells[row, col].Top = row * cellheight;
                    cells[row, col].Left = col * cellwidth;
                    cells[row, col].Width = cellwidth;
                    cells[row, col].Height = cellheight;
                    cells[row, col].FlatStyle = FlatStyle.Popup;
                    cells[row, col].Font = new Font(cells[row, col].Font.FontFamily, 8);
                    cells[row, col].Name = "Button_" + row.ToString() + "_" + col.ToString();

                    if ((row + col) % 2 == 1)
                        cells[row, col].BackColor = Color.Gray;
                    else
                        cells[row, col].BackColor = Color.White;
                    pictureBox1.Controls.Add(cells[row, col]);
                    cells[row, col].Click += new System.EventHandler(b_Click);
                }
            c1 = new chess(pictureBox1.CreateGraphics(), pictureBox1.Width, pictureBox1.Height);
            c1.passform(this);
            updatebuttons = new updateButtonsDelegate(udb);
            updatestat = new updategamestatDelegate(uds);
            promote = new promoteDelegate(promo);
            promoteDelete = new promoteDelDelegate(promoBegone);
            captured = new capturedDelegate(dc);
            delCaptured = new capturedDelegate(clearCaptured);
        }
        private void init_Click(object sender, EventArgs e)
        {
           
            multiplayer.Enabled = false;
            pictureBox1.Enabled = true;
            pieceSelected = false;
            c1.mode = gamemode;
            c1.ReverseBoard();
            c1.initializeBoard();
            c1.displayBoard();
        }
        int fromrow = 0;
        int fromcol = 0;
        bool pieceSelected = false;
        private void b_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            string namestring = b.Name;
            if (namestring == "Queen" || namestring == "Rook" || namestring == "Knight" || namestring == "Bishop")
            {
                switch (namestring)
                {
                    case "Queen":
                        c1.promotionChoice = 'Q';
                        break;
                    case "Rook":
                        c1.promotionChoice = 'R';
                        break;
                    case "Knight":
                        c1.promotionChoice = 'K';
                        break;
                    case "Bishop":
                        c1.promotionChoice = 'B';
                        break;
                }
                c1.promotePawn();
                init.Enabled = true;
                load.Enabled = true;
                save.Enabled = true;
                if (gamestat.Text != "Checkmate" && gamestat.Text != "Stalemate")
                    pictureBox1.Enabled = true;
                c1.ReverseBoard();
                c1.displayBoard();
                pieceSelected = false;
            }
            else if (namestring == "save1" || namestring == "save2" || namestring == "save3" || namestring == "cancel")
            {
                switch (namestring)
                {
                    case "save1":
                        c1.saveFile = @"..\..\savedgame1.txt";
                        break;
                    case "save2":
                        c1.saveFile = @"..\..\savedgame2.txt";
                        break;
                    case "save3":
                        c1.saveFile = @"..\..\savedgame3.txt";
                        break;
                    case "cancel":
                        break;
                }
                Controls.Remove(save1);
                Controls.Remove(save2);
                Controls.Remove(save3);
                Controls.Remove(cancel);
                init.Enabled = true;
                load.Enabled = true;
                save.Enabled = true;
                if (gamestat.Text != "Checkmate" && gamestat.Text != "Stalemate")
                    pictureBox1.Enabled = true;
                if (namestring != "cancel") c1.saveGame();
            }
            else if (namestring == "saved1" || namestring == "saved2" || namestring == "saved3" || namestring == "cancell")
            {
                switch (namestring)
                {
                    case "saved1":
                        c1.saveFile = @"..\..\savedgame1.txt";
                        break;
                    case "saved2":
                        c1.saveFile = @"..\..\savedgame2.txt";
                        break;
                    case "saved3":
                        c1.saveFile = @"..\..\savedgame3.txt";
                        break;
                    case "cancell":
                        break;
                }
                Controls.Remove(saved1);
                Controls.Remove(saved2);
                Controls.Remove(saved3);
                Controls.Remove(cancell);
                if (namestring != "cancell")
                {
                    c1.mode = gamemode;
                    c1.loadGame();
                    pieceSelected = false;
                    c1.ReverseBoard();
                    c1.displayBoard();
                 
                    multiplayer.Enabled = false;
                }
                init.Enabled = true;
                load.Enabled = true;
                save.Enabled = true;
                if (gamestat.Text != "Checkmate" && gamestat.Text != "Stalemate")
                    pictureBox1.Enabled = true;
            }
            else
            {
                string[] s = namestring.ToString().Split('_');

                int row = Convert.ToInt32(s[1]);
                int col = Convert.ToInt32(s[2]);
                if (c1.selectPiece(row, col))
                {
                    if ((fromrow + fromcol) % 2 == 1)
                        cells[fromrow, fromcol].BackColor = Color.Gray;
                    else
                        cells[fromrow, fromcol].BackColor = Color.White;
                    b.BackColor = Color.Red;
                    fromrow = Convert.ToInt32(s[1]);
                    fromcol = Convert.ToInt32(s[2]);
                    pieceSelected = true;
                }
                else if (pieceSelected)
                {
                    if (c1.humanMove(row, col))
                    {
                        if ((fromrow + fromcol) % 2 == 1)
                            cells[fromrow, fromcol].BackColor = Color.Gray;
                        else
                            cells[fromrow, fromcol].BackColor = Color.White;
                        c1.ReverseBoard();
                        c1.displayBoard();
                        pieceSelected = false;
                    }
                }
            }
        }
        int gamemode = 0; // 0 for single player
        private void singleplayer_CheckedChanged(object sender, EventArgs e)
        {
            gamemode = 0;
        }
        private void multiplayer_CheckedChanged(object sender, EventArgs e)
        {
            gamemode = 1;
        }
        Button saved1, saved2, saved3, cancell;
        private void load_Click(object sender, EventArgs e)
        {
            saved1 = new Button();
            saved2 = new Button();
            saved3 = new Button();
            cancell = new Button();
            saved1.Text = "Saved 1";
            saved2.Text = "Saved 2";
            saved3.Text = "Saved 3";
            cancell.Text = "Cancel";
            saved1.Name = "saved1";
            saved2.Name = "saved2";
            saved3.Name = "saved3";
            cancell.Name = "cancell";
            saved1.Location = new Point(597, 150);
            saved2.Location = new Point(597, 180);
            saved3.Location = new Point(597, 210);
            cancell.Location = new Point(597, 240);
            saved1.BackColor = Color.White;
            saved2.BackColor = Color.White;
            saved3.BackColor = Color.White;
            cancell.BackColor = Color.White;
            Controls.Add(saved1);
            Controls.Add(saved2);
            Controls.Add(saved3);
            Controls.Add(cancell);
            saved1.Click += new System.EventHandler(b_Click);
            saved2.Click += new System.EventHandler(b_Click);
            saved3.Click += new System.EventHandler(b_Click);
            cancell.Click += new System.EventHandler(b_Click);
            init.Enabled = false;
            load.Enabled = false;
            save.Enabled = false;
            pictureBox1.Enabled = false;
        }
        Button save1, save2, save3, cancel;
        private void save_Click(object sender, EventArgs e)
        {
            save1 = new Button();
            save2 = new Button();
            save3 = new Button();
            cancel = new Button();
            save1.Text = "Save 1";
            save2.Text = "Save 2";
            save3.Text = "Save 3";
            cancel.Text = "Cancel";
            save1.Name = "save1";
            save2.Name = "save2";
            save3.Name = "save3";
            cancel.Name = "cancel";
            save1.BackColor = Color.White;
            save2.BackColor = Color.White;
            save3.BackColor = Color.White;
            cancel.BackColor = Color.White;
            save1.Location = new Point(597, 150);
            save2.Location = new Point(597, 180);
            save3.Location = new Point(597, 210);
            cancel.Location = new Point(597, 240);
            Controls.Add(save1);
            Controls.Add(save2);
            Controls.Add(save3);
            Controls.Add(cancel);
            save1.Click += new System.EventHandler(b_Click);
            save2.Click += new System.EventHandler(b_Click);
            save3.Click += new System.EventHandler(b_Click);
            cancel.Click += new System.EventHandler(b_Click);
            init.Enabled = false;
            load.Enabled = false;
            save.Enabled = false;
            pictureBox1.Enabled = false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ran_san_moi
{
    public partial class maingame : Form
    {
        private List<PictureBox> kuc = new List<PictureBox>();
        private List<PictureBox> kuc2 = new List<PictureBox>();
        
        private System.Windows.Forms.Timer sk;
        private Random rd = new Random();
        private PictureBox moi;
        private PictureBox ran;
        private bool ske = true;        
        private bool c1 = false;
        private System.Windows.Forms.Timer s;
        private enum huong
        {
            Up,
            Down,
            Left,
            Right
        }
        private huong khoidau = huong.Right;
        private huong khoidau2 = huong.Left;
        private int se = 15;
        private int KT = 15;
        private int KTM = 15;
        public maingame()
        {
            InitializeComponent();
            this.KeyDown += maingame_KeyDown;
        }

        private void maingame_Load(object sender, EventArgs e)
        {
            s = new System.Windows.Forms.Timer();
            s.Interval = 200;
            s.Tick += s_Tick;
            s.Start();

            

            sk = new System.Windows.Forms.Timer();
            sk.Interval = 10000;
            sk.Tick += sk_Tick;
            this.Focus();


            taomoi();
            

            PictureBox ran = khucran(120, 120);
            ran.Image = Properties.Resources.dau;
            ran.SizeMode = PictureBoxSizeMode.StretchImage;
            kuc.Add(ran);

           


        }

        //------------------------------------------------------------------------//
        private void maingame_KeyDown(object sender, KeyEventArgs e)
        {
            if       (e.KeyCode == Keys.W && khoidau != huong.Down)   { khoidau = huong.Up; }
            else if  (e.KeyCode == Keys.S && khoidau != huong.Up)     { khoidau = huong.Down;}
            else if  (e.KeyCode == Keys.A && khoidau != huong.Right)  { khoidau = huong.Left;}
            else if  (e.KeyCode == Keys.D && khoidau != huong.Left)    {khoidau = huong.Right;}

            if       (e.KeyCode == Keys.Up && khoidau2 != huong.Down)     {khoidau2 = huong.Up;}
            else if  (e.KeyCode == Keys.Down && khoidau2 != huong.Up)     {khoidau2 = huong.Down;}
            else if  (e.KeyCode == Keys.Left && khoidau2 != huong.Right)  { khoidau2 = huong.Left;}
            else if  (e.KeyCode == Keys.Right && khoidau2 != huong.Left)  { khoidau2 = huong.Right;}

            if  (e.KeyCode == Keys.Space && kuc.Count > 2)
                {
                    c1 = true;
                    int half = kuc.Count / 2;               
                    for (int i = kuc.Count - 1; i > +half; i--)
                    {
                        kuc2.Insert(0, kuc[i]);
                        kuc.RemoveAt(i);
                    }
                if (kuc2.Count > 0)
                    {
                        kuc2[0].Image = Properties.Resources.dau;
                    }
                        sk.Start();
                 }

            if (e.KeyCode == Keys.E && ske == true )
                {
                    if (s.Interval > 90)
                        {
                            s.Interval -= 80;
                            label1.BackColor = Color.Red;
                            ske = false;
                        }
                    else
                        {
                    s.Interval += 40;
                    label1.BackColor = Color.Yellow;
                    ske = false;

                        }
                }

        }
        //------------------------------------------------------------------------//
        private PictureBox khucran(int x, int y)
        {
            PictureBox ahalf = new PictureBox();
            ahalf.Size = new Size(KT, KT);
            ahalf.Image = Properties.Resources.than;
            ahalf.SizeMode = PictureBoxSizeMode.StretchImage;
            ahalf.Location = new Point(x, y);
            this.Controls.Add(ahalf);
            ahalf.BringToFront();
            return ahalf;
        }
        //------------------------------------------------------------------------//        
        private void tangtoc()
        {
          if (s != null && s.Interval > 40)
             {
                KTM += 2;        
                s.Interval -= 6;
             }
        }
        //------------------------------------------------------------------------//
        private void taomoi()
        {
            int max = Math.Max(0, this.ClientSize.Width - KT)/se;
            int may = Math.Max(0, this.ClientSize.Height - KT)/se;
            int ranx = rd.Next(0, max)*se;
            int rany = rd.Next(0, may)*se;
            if (moi == null)
            {
                moi = new PictureBox();
                moi.Size = new Size(KTM, KTM);
                moi.SizeMode = PictureBoxSizeMode.StretchImage;
                moi.Image = Properties.Resources.chuong;
                
                this.Controls.Add(moi);
            }
            moi.Location = new Point(ranx, rany);
            moi.BringToFront();
        }
        //-----------------------------------------------------------------------//
        private void sk_Tick(object sender, EventArgs e)
        {
            sk.Stop();
            if (kuc2.Count > 0)
            {
                kuc2[0].Image = Properties.Resources.than;

            }
            foreach (var half in kuc2)
            {
                half.Location = kuc[kuc.Count - 1].Location;
                kuc.Add(half);
            }
            kuc2.Clear();
            c1 = false;

        }
        private void end()
        {
            s.Stop();
            if ((kuc2.Count + kuc.Count - 1) < 5)
            {
                MessageBox.Show(" ban ngu vai l deo co cc j cung thua ");
                this.Hide();
                main1 f = new main1();
                f.ShowDialog();
            }

            else
            {
                DialogResult result = MessageBox.Show("ban da co gang co muon luu lai ten cua ban khong", "Xác nhận", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    newscroor f = new newscroor(); f.ShowDialog();
                    this.Close();
                }
                else
                {   
                    this.Close();
                    main1 f = new main1();
                    f.ShowDialog();
                }
            } 
        }
        private void s_Tick(object sender, EventArgs e)
        {

            
            if (kuc.Count == 0) return;

            for (int i = kuc.Count - 1; i > 0; i--)
            {
                kuc[i].Location = kuc[i - 1].Location;
            }
            int htx = kuc[0].Location.X;
            int hty = kuc[0].Location.Y;

            switch (khoidau)
            {
                case huong.Up:hty -= se;     break;                
                case huong.Down:hty += se;   break;
                case huong.Left:htx -= se;   break;
                case huong.Right:htx += se;  break;
            }
            kuc[0].Location = new Point(htx, hty);

            if (moi != null && kuc[0].Bounds.IntersectsWith(moi.Bounds))
            {
                Point dit = kuc[kuc.Count - 1].Location;
                PictureBox newran = khucran(dit.X, dit.Y);
                kuc.Add(newran);
                label2.Text = (kuc2.Count + kuc.Count - 1).ToString();

                taomoi();
                tangtoc();

            }
            

            if (kuc2.Count > 0)
            {
                for (int i = kuc2.Count - 1; i > 0; i--)
                {
                    kuc2[i].Location = kuc2[i - 1].Location;
                }
                int htx2 = kuc2[0].Location.X, hty2 = kuc2[0].Location.Y;

                switch (khoidau2)
                {
                    case huong.Up:hty2 -= se;    break;
                    case huong.Down:hty2 += se;  break;
                    case huong.Left:htx2 -= se;  break;
                    case huong.Right:htx2 += se; break;
                }
                kuc2[0].Location = new Point(htx2, hty2);

                if (moi != null && kuc2[0].Bounds.IntersectsWith(moi.Bounds))
                {
                    Point dit2 = kuc2[kuc2.Count - 1].Location;
                    PictureBox newran2 = khucran(dit2.X, dit2.Y);
                    kuc2.Add(newran2);
                    label2.Text = (kuc2.Count + kuc.Count - 1).ToString();

                    taomoi();
                    tangtoc();

                }
            }
            
            if (kuc[0].Location.X < 0 || kuc[0].Location.X >= this.Width || kuc[0].Location.Y < 0 || kuc[0].Location.Y >= this.Height)
                {              
                end();
                }

            for (int z = 4; z < kuc.Count; z++)
                {
                    if (kuc[0].Bounds.IntersectsWith(kuc[z].Bounds))
                        {
                            end();
                        }

                }
            }
        
        
        

        private void label2_Click(object sender, EventArgs e)
        {
            
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

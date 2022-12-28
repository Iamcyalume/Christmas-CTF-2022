using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security;
using System.Security.Cryptography;

//christmasCTF{cEr7iF13d_ChristmaS_F1Ag}

namespace _ChristmasCTF__Light_Up_My_Christmas_
{
    public partial class Form1 : Form
    {
        string input;

        public Form1()
        {
            InitializeComponent();

            pictureBox1.BackColor = Color.Transparent;
            // pictureBox1.Parent = 

            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.Parent = pictureBox1;

            pictureBox3.BackColor = Color.Transparent;
            pictureBox3.Parent = pictureBox1;

            pictureBox4.BackColor = Color.Transparent;
            pictureBox4.Parent = pictureBox1;

            pictureBox5.BackColor = Color.Transparent;
            pictureBox5.Parent = pictureBox1;

            pictureBox6.BackColor = Color.Transparent;
            pictureBox6.Parent = pictureBox1;

            pictureBox7.BackColor = Color.Transparent;
            pictureBox7.Parent = pictureBox1;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            input = null;
        }

        private void pictureBox1_Click(object sender, EventArgs e) //tree
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e) //star
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e) //bulb
        {

        }
        private void pictureBox4_Click(object sender, EventArgs e) //cane
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e) //sock
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e) //ru
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e) //snwman
        {

        }
        private void button1_Click(object sender, EventArgs e) //snwman
        {
            input = null;
            input = textBox.Text;
            if (!Bulb(input))
            {
                return;
            }

            Validation(input);

        }

        private bool Validation(string input)
        {
            if(Star(Cane(input) && Sock(input) && Ru(input) && Snwman(input)))
            {
                MessageBox.Show("Merry Christmas!");
                return true;
            }

            return false;
        }

        private bool Bulb(string input) // length
        {
            if (input.Length != 0x26)
                return false;
            else pictureBox3.Image = Properties.Resources.bulb_on;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            return true;
        }

        private bool Cane(string input) // flag format
        {
            if (input.Contains("christmasCTF{") && input.Contains("}")
                && (input[input.Length - 1] == '}'))
            {
                pictureBox4.Image = Properties.Resources.cane_on;
                return true;
            }
            return false;

        }

        private bool Sock(string input) // equation 1 ~ (christmas) base64 구현, christmaS_
        {
            byte[] data = new byte[input.Length];
            for(int i = 23; i<33; i++)
            {
                data[i-23] = (byte)input[i];
            }

            string str = null;

            for(int i = 0; i<((input.Length * 30 - input.Length ^ 1233) / 87 + input.Length * 2 - 27); i++)
            {
                str = Convert.ToBase64String(data);
            }
            
            if(str == "Q2hyaXN0bWFTXwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=")
            {
                pictureBox5.Image = Properties.Resources.sock_on;
                return true;
            }
            return false;
        }


        private bool Ru(string input) // 
        {
            char[] str = new char[4];
            int[] arr = new int[4] {2, 2, 4, 53};
            char[] deer = new char[] { 'D', '3', 'E', 'R' };

            for(int i = 33; i<37; i++)
            {
                str[i-33] = input[i];
            }

            for(int i = 0; i<4; i++)
            {
                if((str[i]^deer[i]) != arr[i])
                {
                    return false;
                }
            }

            pictureBox6.Image = Properties.Resources.ru_on;
            return true;
        }

        private bool Snwman(string input)
        {
            int i;
            char[] str = new char[38];
            int[] arr = new int[] { 99, 68, 112, 52, 101, 65, 43, 44, 92, 86 };

            for (i = 0; i < input.Length; i++)
            {
                str[i] = input[i];
            }

            for (i = 13; i < 23; i++)
            {
                if (
                    ((str[i] - (i - 13) + ((((str[i] >> i)) | ((str[i] << 8 - i) & 255)) ^
                   (((str[i] << i) & 255) | str[i] >> 8 - i))) != arr[i-13])
                    )
                {
                    return false;

                }else{
                    continue;
                }
            }

            pictureBox7.Image = Properties.Resources.snwman_on;
            return true;

        }


        private bool Star(bool flags) // if all ornaments has true value - &&
        {
            if(flags == true)
            {
                pictureBox2.Image = Properties.Resources.star_on;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

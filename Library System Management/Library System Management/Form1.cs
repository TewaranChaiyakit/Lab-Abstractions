namespace Library_System_Management
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (textBoxUsername.Text !="naphapilan")  
            {
                MessageBox.Show("Usename ���١��ͧ");
            }
            if (textBoxPassword.Text != "Nn.21042546")
            {
                MessageBox.Show("Password ���١��ͧ");
            }
            if (textBoxUsername.Text == "naphapilan" && textBoxPassword.Text == "Nn.21042546")
            {
                Form4 form4 = new Form4();
                form4.Show();
                this.Hide();
            }          
        }

        private void buttonStaftRegis_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }
    }
}
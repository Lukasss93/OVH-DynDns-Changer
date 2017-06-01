using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OVHDD.Windows
{
    public partial class NewProfile : Window
    {
        public NewProfile()
        {
            InitializeComponent();
            tb_newprofile.Focus();

            bt_cancel.Click += (s, e) => { DialogResult = false; };
            bt_save.Click += (s, e) => 
            {
                if(tb_newprofile.Text.Length == 0)
                {
                    MessageBox.Show("Please enter a profile name!","Warning");
                }
                else
                {
                    DialogResult = true;
                }
            };

            tb_newprofile.KeyUp += (s, e) =>
            {
                if(e.Key==Key.Enter)
                {
                    if (tb_newprofile.Text.Length == 0)
                    {
                        MessageBox.Show("Please enter a profile name!", "Warning");
                    }
                    else
                    {
                        DialogResult = true;
                    }
                }
            };
        }

        public string ResponseText
        {
            get { return tb_newprofile.Text; }
            set { tb_newprofile.Text = value; }
        }
    }
}

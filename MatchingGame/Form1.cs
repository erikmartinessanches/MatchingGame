using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingGame
{
    public partial class Form1 : Form
    {
        Random random = new Random();
        List<string> icons = new List<string>() { "!", "!", "N", "N", ",", ",", "k", "k", "b", "b", "v", "v", "w", "w", "z", "z" };
        
        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();
        }
        
        /// <summary>
        /// Assign each icon from the list of icons to a random square.
        /// </summary>
        private void AssignIconsToSquares()
        {
            /* The TableLayoutPanel has 16 labels, and the icon list has 16 icons, so an icon is pulled at random from the list and added to each label.*/
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if(iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }
        }
        /*Because you hooked up different label control Click event to a single event handler method, the same method is called no matter which label the user chooses. The event handler method needs to know which label was chosen, so it uses the name sender to identify the label control. The first line of the method tells the program that it's not just a generic object, but specifically a label control, and that it uses the name clickedLabel to access the label's properties and methods.*/
        private void label_Click(object sender, EventArgs e)
        {
            Label clickedLabel = sender as Label;
            if (clickedLabel != null)
            {
                if (clickedLabel.ForeColor == Color.Black)
                {
                    return;
                }
                clickedLabel.ForeColor = Color.Black;
            }
        }
    }
}

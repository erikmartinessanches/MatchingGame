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
        Label firstClicked = null;
        Label secondClicked = null;
        int clickCounter = 0;
        
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
        /// <summary>
        /// Every label's Click event is handled by this event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_Click(object sender, EventArgs e)
        {
            // The timer is only on after two non-matching 
            // icons have been shown to the player, 
            // so ignore any clicks if the timer is running.
            if (timer1.Enabled == true)
            {
                return;
            }
            Label clickedLabel = sender as Label;
            if (clickedLabel != null)
            {
                if (clickedLabel.ForeColor == Color.Black)
                {
                    return;
                }
                /*If firstClicked is null, this is the first icon in the pair that the player clicked, so set firstClicked to the label that the player clicked, change its color to black, and return. */
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    clickCounter++;
                    return;
                }
                // If the player gets this far, the timer isn't
                // running and firstClicked isn't null,
                // so this must be the second icon the player clicked.
                // Set its color to black.
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;
                clickCounter++;
                CheckForWinner();
                if(firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }
                // If the player gets this far, the player 
                // clicked two different icons, so start the 
                // timer (which will wait three quarters of 
                // a second, and then hide the icons).
                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            // Hide both icons
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;
            // Reset firstClicked and secondClicked 
            // so the next time a label is
            // clicked, the program knows it's the first click
            firstClicked = null;
            secondClicked = null;
        }
        /// <summary>
        /// Check every icon to see if it is matched, by 
        /// comparing its foreground color to its background color. 
        /// If all of the icons are matched, the player wins.
        /// </summary>
        private void CheckForWinner()
        {
            foreach(Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if(iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor) //See if there are invisible icons left, in which case we aren't finished with the game.
                    {
                        return; //Drops us out of the method.
                    }
                }
            }
            // If the loop didn’t return, it didn't find
            // any unmatched icons.
            // That means the user won. Show a message and close the form.
            MessageBox.Show("You matched them all! Number of clicks: " + clickCounter + ".", "Congrats");
            Close();
        }
    }
}

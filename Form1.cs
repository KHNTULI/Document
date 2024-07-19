using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Matching_Game
{
    public partial class Form1 : Form
    {
        //Use a Random Object to choose random icons from the squares
        Random random = new Random();

        //carete a list that will store letters of icons that will appear in the form
        List<String> icons = new List<string>()
        {
        "!", "!", "N", "N", ",", ",", "k", "k",
       "b", "b", "v", "v", "w", "w", "z", "z"};

        //create a variable(    firstClicked) to point the first label control

        Label firstClicked = null;


        //create a variable(secondClicked) to point the second label control
        Label secondClicked = null;

        private void AssignIconsToSquares()
        {

            //Pull the icons randomly from the the list and add it to each label
            //
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];

                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }

        }
        public Form1()
        {
            InitializeComponent();

            AssignIconsToSquares();
        }

        private void label1_Click(object sender, EventArgs e)
        {

            //the timer is only on after two non-matching icons have been shown to the player
            //so ignore any clicks if the timer is running
            if (timer1.Enabled == true)
                return;


            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                //if the clicked label is black,the player clicked
                //an icon that's already been revealed--
                //ignore the click

                if (clickedLabel.ForeColor == Color.Black)
                    return;

                // clickedLabel.ForeColor = Color.Black;
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;

                    return;
                }
                secondClicked= clickedLabel;
                secondClicked.ForeColor = Color.Black;

                CheckForWinner();

                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }
                timer1.Start();


            }



        }

        private void CheckForWinner()
        {

            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)

                        return;

                }
            }    
                MessageBox.Show("You matched all the icons!! ", "Congratulations");
                Close();
            
        }
            private void timer1_Tick(object sender, EventArgs e)
            {
                //stop the timer

                timer1.Stop();

                //hide both icons

                firstClicked.ForeColor = firstClicked.BackColor;
                secondClicked.ForeColor = secondClicked.BackColor;

                //reset firstClicked and secondClicked
                //so the next time a lebel is selected/clicked,the program knows it's the first click

                firstClicked = null;
                secondClicked = null;
            }
        
    } }

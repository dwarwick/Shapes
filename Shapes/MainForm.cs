using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shapes
{// Beginning of namespace
    partial class MainForm : Form
    {//Beginning of class

        // m_myBoxes is available to the entire MainForm class
        private List<Box> m_myBoxes;

        public MainForm()
        {
            //Default Constructor
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //all code contained in this method 
            //will run when the form has loaded.
            m_myBoxes = new List<Box>();

            /* m_myBoxes is now an empty
             * usable list that can be used
             * everywhere in the class.
             * 
             * If it were declared in this
             * method, it could only be 
             * used in this method.
             * 
             * For example, if we did this:
             * List<Box> m_myBoxes; 
             * m_myBoxes = new List<Box>();
             * or 
             * List<Box> m_myBoxes = new List<Box>();
             * 
             * then this variable could only be used inside
             * this method
             */
        }

        private void CreateNewBox()
        {
            /*
             * Note that in newer versions of
             * Visual Studio, these variables
             * can be "in-lined" below, but
             * I am declaring them this way
             * for backwards compatibility.
             */

            int iLength;
            int iHeight;
            int iWidth;
            int iOriginX;
            int iOriginY;

            if (int.TryParse(txtLength.Text, out iLength)
                && int.TryParse(txtWidth.Text, out iWidth)
                && int.TryParse(txtHeight.Text, out iHeight)
                && int.TryParse(txtBox_X.Text, out iOriginX)
                && int.TryParse(txtBox_Y.Text, out iOriginY)
                )
            {
                Box box = new Box(iHeight, iWidth, iLength, iOriginX, iOriginY);
                m_myBoxes.Add(box);
            }
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            // all code in this method will execute 
            // when the Draw button is clicked

            CreateNewBox();
            this.Refresh();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            // All code inside this method when the
            // form is repainted.


            if (m_myBoxes.Count > 0)
            {
                DrawBox(e); 
            }
        }

        private void DrawBox(PaintEventArgs e)
        {
            foreach (Box myBox in m_myBoxes)
                myBox.Draw(e.Graphics);
        }

    } // Closes MainForm class

    class Box
    {
        private int m_iHeight; //Class Member Variables
        private int m_iWidth;
        private int m_iLength;
        private int m_iOriginX;
        private int m_iOriginY;

        public Box(int iHeight, int iWidth, int iLength, int iOriginX, int iOriginY)
        {
            m_iHeight = iHeight; // Constructor
            m_iWidth = iWidth;
            m_iLength = iLength;
            m_iOriginX = iOriginX;
            m_iOriginY = iOriginY;
        }

        public void Draw(Graphics graphics) //Method
        {
            /*
                * Obtained from 
                * https://www.daniweb.com/programming/software-development/threads/369044/drawing-cubes-in-c-using-system-drawing
                */
            Point origin = new Point(m_iOriginX, m_iOriginY); //Location of Box
            Pen pencil = new Pen(Color.Blue, 1f); //Blue pencil
            Rectangle R = new Rectangle(origin.X, origin.Y, m_iWidth, m_iHeight);
            graphics.DrawRectangle(pencil, R); // Draw Rectangle

            graphics.DrawLine(pencil,
                                origin.X, origin.Y,
                                origin.X + m_iLength,
                                origin.Y - m_iLength);// Top left edge

            graphics.DrawLine(pencil,
                                origin.X + m_iLength,
                                origin.Y - m_iLength,
                                origin.X + m_iWidth + m_iLength, origin.Y - m_iLength); //Top back width

            graphics.DrawLine(pencil,
                                origin.X + m_iWidth + m_iLength,
                                origin.Y - m_iLength,
                                origin.X + m_iWidth,
                                origin.Y); //Top Right Edge

            graphics.DrawLine(pencil,
                                origin.X + m_iWidth + m_iLength,
                                origin.Y - m_iLength,
                                origin.X + m_iWidth + m_iLength,
                                origin.Y - m_iLength + m_iHeight); //Back Right Edge

            graphics.DrawLine(pencil,
                                origin.X + m_iWidth + m_iLength,
                                origin.Y - m_iLength + m_iHeight,
                                origin.X + m_iWidth,
                                origin.Y + m_iHeight); //Bottom Right Edge
        } //Closes Draw Method
    } //Closes Box class
} // Closes Shapes Namespace

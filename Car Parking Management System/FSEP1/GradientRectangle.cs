using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FSEP1
{
    public class GradientRectangle: Panel
    {

        //Fields
        private int broderRadius = 30;
        private float gradientAngle = 90F;
        private Color gradientTopColor = Color.DodgerBlue;
        private Color gradientBottomColor = Color.CadetBlue;


        //Constructor
        public GradientRectangle()
        {
            this.BackColor = Color.White;
            this.ForeColor = Color.Black;
            this.Size = new Size(350, 200);
        }
        

        //Properties
        public int BroderRadius 
        { 
            get => broderRadius;
            set { broderRadius = value; this.Invalidate(); } 
        }
        public float GradientAngle 
        { 
            get => gradientAngle;
            set { gradientAngle = value; this.Invalidate(); } 
        }
        public Color GradientTopColor 
        { 
            get => gradientTopColor;
            set { gradientTopColor = value; this.Invalidate(); }
        }
        public Color GradientBottomColor 
        { 
            get => gradientBottomColor;
            set { gradientBottomColor = value; this.Invalidate(); }
        }


        //Methods
        private GraphicsPath GetGradientRectangle(RectangleF rectangle, float radius)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.StartFigure();
            graphicsPath.AddArc(rectangle.Width - radius, rectangle.Height - radius, radius, radius, 0, 90);
            graphicsPath.AddArc(rectangle.X, rectangle.Height - radius, radius, radius, 90, 90);
            graphicsPath.AddArc(rectangle.X, rectangle.Y, radius, radius, 180, 90);
            graphicsPath.AddArc(rectangle.Width - radius, rectangle.Y, radius, radius, 270, 90);
            graphicsPath.CloseFigure();
            return graphicsPath;
        }

        //OverRidden Methods
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);


            //Gradient 
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            LinearGradientBrush brushGradient = new LinearGradientBrush( this.ClientRectangle, this.gradientTopColor, this.gradientBottomColor, this.gradientAngle);
            Graphics graphicsGradient = e.Graphics;
            graphicsGradient.FillRectangle(brushGradient, ClientRectangle);

            //BorderRadius
            RectangleF rectangleF = new RectangleF(0, 0, this.Width, this.Height);
            if (BroderRadius > 2)
            {
                using (GraphicsPath graphicsPath = GetGradientRectangle(rectangleF, BroderRadius))
                using (Pen pen = new Pen(this.Parent.BackColor, 2))
                {
                    this.Region = new Region(graphicsPath);
                    e.Graphics.DrawPath(pen, graphicsPath);
                }

            }
            else this.Region = new Region(rectangleF);
        }
        
    }
}

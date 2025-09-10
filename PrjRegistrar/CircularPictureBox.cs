using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PrjRegistrar
{
    public class CircularPictureBox : PictureBox
    {

        protected override void OnPaint(PaintEventArgs pe)
        {
            GraphicsPath graphicsPath = new GraphicsPath();

            graphicsPath.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height);
            this.Region = new System.Drawing.Region(graphicsPath);

            Pen pen = new Pen(Color.White, 6);
            pe.Graphics.DrawPath(pen, graphicsPath);

            base.OnPaint(pe);
        }
    }
}

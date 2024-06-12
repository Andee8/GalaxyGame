using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class RoundButton : Button
{
    protected override void OnPaint(PaintEventArgs pevent)
    {
        GraphicsPath graphicsPath = new GraphicsPath();
        graphicsPath.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height);
        this.Region = new Region(graphicsPath);

        base.OnPaint(pevent);

        // Draw the button text centered
        StringFormat stringFormat = new StringFormat();
        stringFormat.Alignment = StringAlignment.Center;
        stringFormat.LineAlignment = StringAlignment.Center;
        pevent.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), ClientRectangle, stringFormat);
    }
}

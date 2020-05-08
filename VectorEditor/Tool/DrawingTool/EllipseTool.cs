﻿using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Controls;

namespace VectorEditorApplication
{
    public class EllipseTool : DrawingTool
    {
        private ConturColorConfig conturColor;
        private FillColorConfig fillColor;
        private DashCapConfig dashStyle;
        private HatchBrushConfig hatchStyle;
        private ThicknessConfig thickness;


        public ConturColorConfig ConturColor { get { return conturColor; } set { ConturColor = value; } }
        public FillColorConfig FillColor { get { return fillColor; } set { FillColor = value; } }
        public DashCapConfig DashStyle { get { return dashStyle; } set { DashStyle = value; } }
        public HatchBrushConfig HatchStyle { get { return hatchStyle; } set { HatchStyle = value; } }
        public ThicknessConfig Thickness { get { return thickness; } set { Thickness = value; } }

        public EllipseTool()
        {
            conturColor = new ConturColorConfig(System.Windows.Media.Colors.Black);
            fillColor = new FillColorConfig(System.Windows.Media.Colors.White);
            dashStyle = new DashCapConfig(System.Drawing.Drawing2D.DashStyle.Solid);
            hatchStyle = new HatchBrushConfig(System.Drawing.Drawing2D.HatchStyle.ZigZag);
            thickness = new ThicknessConfig(1);
        }

        public override void MouseDownHandler(int x, int y)
        {
            Pen pen = new Pen(conturColor.colorDrawing);
            pen.DashStyle = dashStyle.dashStyle;
            pen.Width = thickness.Thickness;

            VectorEditorApp.figures.AddLast(CreateFigure(x, y, x, y, pen));
            Invalidate();
            currentState = States.mouseClick;
        }
        public override void MouseMoveHandler(int x, int y)
        {
            if (currentState == States.mouseClick)
            {
                VectorEditorApp.figures.Last.Value.EditSize(x, y);
                Invalidate();
            }
        }

        protected override Figure CreateFigure(int x1, int y1, int x2, int y2, Pen pen)
        {
            return new Ellipse(x1, y1, x2, y2, pen, new HatchBrush(hatchStyle.fillStyle,
                (hatchStyle.Configurator as ComboBox).SelectedItem.ToString() == "None" ? fillColor.colorDrawing : conturColor.colorDrawing,
                fillColor.colorDrawing));
        }
    }
}

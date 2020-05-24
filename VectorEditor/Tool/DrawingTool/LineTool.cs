﻿using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

namespace VectorEditorApplication
{
    public class LineTool : DrawingTool
    {
        private ConturColorConfig conturColor;
        private DashStyleConfig dashStyle;
        private ThicknessConfig thickness;

        public ConturColorConfig ConturColor { get => conturColor; set => ConturColor = value; }
        public DashStyleConfig DashStyle { get => dashStyle; set => DashStyle = value; }
        public ThicknessConfig Thickness { get => thickness; set => Thickness = value; }

        public LineTool()
        {
            conturColor = new ConturColorConfig(Colors.Black);
            dashStyle = new DashStyleConfig(typeof(SolidPen));
            thickness = new ThicknessConfig(1);

            ToolForm = new Button()
            {
                Width = 60,
                Height = 30,
                Content = "Line",
                Margin = new Thickness(5)
            };

        }


        public override void MouseDownHandler(Point firstPoint)
        {
            Pen pen = PenPicker.GetPen(DashStyle.Pencil).GetPen(conturColor.Color, thickness.Thickness);

            PaintController.figures.AddLast(CreateFigure(firstPoint, firstPoint, pen));
            currentState = States.mouseClick;
        }
        public override void MouseMoveHandler(Point secondPoint)
        {
            if (currentState == States.mouseClick)
            {
                PaintController.figures.Last.Value.EditSize(secondPoint);
            }
        }

        protected override Figure CreateFigure(Point firstPoint, Point secondPoint, Pen pen)
        {
            return new Line(firstPoint, secondPoint, pen, new SolidColorBrush(Colors.White));
        }
    }
}

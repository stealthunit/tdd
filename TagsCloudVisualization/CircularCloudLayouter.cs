using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TagsCloudVisualization
{
    class CircularCloudLayouter
    {
        private Point _center = new Point();
        List<Rectangle> allFigures = new List<Rectangle>();
        public CircularCloudLayouter(Point center)
        {
            _center = center;
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            Rectangle newRectangle = new Rectangle(_center, rectangleSize);
            allFigures.Add(newRectangle);
            return newRectangle;
        }
        //private double DistancePointFromCenter(int x, int y)
        //{
        //    //double result = Math.Sqrt((_center.X - x) ^ 2 + (_center.Y - y) ^ 2);
        //    //double dx = (2 * (_center.X + 2)- _center.Y) /();
        //    //return result; // для нахождения
        //}
    }

    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void PutOneRectangle()
        {
            var cloud = new CircularCloudLayouter(new Point(100, 100));
            var figure = cloud.PutNextRectangle(new Size(50,20));
            Assert.AreEqual(figure.X, 100);
            Assert.AreEqual(figure.Y, 100);
            Assert.AreEqual(figure.Width, 50);
            Assert.AreEqual(figure.Height, 20);
        }
    }
}
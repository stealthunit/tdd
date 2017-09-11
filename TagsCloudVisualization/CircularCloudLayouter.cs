using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TagsCloudVisualization
{
    class CircularCloudLayouter
    {
        private Point _center = new Point();
        public List<Rectangle> allFigures = new List<Rectangle>();
        public CircularCloudLayouter(Point center)
        {
            _center = center;
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            Point nextRectangleCenter = GetNewRectangleCoords(rectangleSize);
            Rectangle newRectangle = new Rectangle(nextRectangleCenter, rectangleSize);
            allFigures.Add(newRectangle);
            return newRectangle;
        }

        private Point GetNewRectangleCoords(Size rectangleSize)
        {
            if (allFigures.Count == 0)
                return new Point(_center.X - rectangleSize.Width / 2, _center.Y - rectangleSize.Height / 2);
            int radius = allFigures.Max(i => i.Height) + rectangleSize.Height;
            // циклом идти по окружности и искать точку
            for (int step = -radius; step <= radius; step++)
            {
                int x1 = radius * (int)Math.Cos(radius) + _center.X;
                int y1 = radius * (int)Math.Sin(radius) + _center.Y;
                if (!CheckAreaIsFilled(new Point(x1, y1), rectangleSize))
                    return new Point(x1 - rectangleSize.Width/2,y1 - rectangleSize.Height / 2);
            }
            return new Point();
        }

        /// <summary>
        /// Проверяет, свободно ли пространство под ключевыми точками, куда хотим поставить фигуру
        /// </summary>
        /// <param name="supposedCenter"></param>
        /// <param name="rectangleSize"></param>
        /// <returns></returns>
        private bool CheckAreaIsFilled(Point supposedCenter, Size rectangleSize)
        {
            if (!CheckPointIsFilled(supposedCenter) &&
                !CheckPointIsFilled(new Point(supposedCenter.X + rectangleSize.Width / 2)) &&
                !CheckPointIsFilled(new Point(supposedCenter.X - rectangleSize.Width / 2)) &&
            !CheckPointIsFilled(new Point(supposedCenter.Y + rectangleSize.Height / 2)) &&
            !CheckPointIsFilled(new Point(supposedCenter.Y - rectangleSize.Height / 2)))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Проверяет, занято ли другой фигурой место, куда хотим поставить точку
        /// </summary>
        /// <param name="exPoint"></param>
        /// <returns></returns>
        private bool CheckPointIsFilled(Point exPoint)
        {
            return allFigures.Any(i =>
                i.X <= exPoint.X &&
                i.X + i.Width >= exPoint.X &&
                i.Y <= exPoint.Y &&
                i.Y + i.Height >= exPoint.Y);
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
            Assert.AreEqual(75, figure.X);
            Assert.AreEqual(90, figure.Y);
            Assert.AreEqual(figure.Width, 50);
            Assert.AreEqual(figure.Height, 20);
        }

        [TestMethod]
        public void PutFirstLevelNeigbors()
        {
            var cloud = new CircularCloudLayouter(new Point(100, 100));
            cloud.PutNextRectangle(new Size(50, 20));
            var figure2 = cloud.PutNextRectangle(new Size(40, 16));
            Assert.AreEqual(2, cloud.allFigures.Count);
            Assert.AreEqual(100, figure2.X);
            Assert.AreEqual(72, figure2.Y);
            Assert.AreEqual(40, figure2.Width);
            Assert.AreEqual(16, figure2.Height);
        }
    }
}
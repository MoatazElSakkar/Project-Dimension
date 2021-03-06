﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dimension.data;

namespace Dimension.LinearAlgebra
{
    public enum Direction
    {
        upwards,
        downwards,
        left,
        right
    }

    public class LineEquation
    {
        //y=mx+c

        public float Slope; //(y2-y1)/(x2-x1)

        public float intercept; //Intersection with Y axis (or X axis if inverted)

        bool vertical = false;

        Point intialPoint;
        Point FinalPoint;

        public int length;

        Direction monotony;
        int direction;

        public LineEquation(Point a, Point b)
        {
            intialPoint = a;
            FinalPoint = b;
            int yDelta=(int)(b.y - a.y);
            int xDelta=(int)(b.x - a.x);
            if (xDelta != 0)
            {
                Slope = (float) yDelta / xDelta;
            }
            else
            {
                vertical = true;
                Slope = 0;
            }
            CalculateIntercept();
            CalculateLength(a,b);
            CalculateDirection(a, b);
        }

        void CalculateIntercept()
        {
            if (!vertical)
            {
                intercept = (int)(intialPoint.y - (Slope * intialPoint.x));
            }
            else
            {
                intercept = 0;
            }
        }

        void CalculateLength(Point a,Point b) //Getting the distance between 2 points
        {
            if (!vertical)
            {
                length = (int)Math.Abs((a.x - b.x));
            }
            else
            {
                length = (int)Math.Abs((a.y - b.y));
            }

            //length = (int)Math.Max(Math.Abs(a.x - b.x), Math.Abs(a.y - b.y));
        }

        void CalculateDirection(Point a, Point b)
        {
            if (!vertical)
            {
                if (a.x > b.x)
                {
                    monotony = Direction.right;
                    direction = -1;
                }
                else if (a.x < b.x)
                {
                    monotony = Direction.left;
                    direction = 1;
                }
                else
                {
                    throw new Exception("Duplicate point exception");
                }

            }
            else
            {
                if (a.y > b.y)
                {
                    monotony = Direction.downwards;
                    direction = -1;
                }
                else if (a.y < b.y)
                {
                    monotony = Direction.upwards;
                    direction = 1;
                }
                else
                {
                    //throw new Exception("Duplicate point exception");
                }
            }
        }

        public Point calculateNextPoint(Point P)
        {
            float nY, nX;
            if (!vertical)
            {
                if (P.x == FinalPoint.x)
                {
                    return P;
                }
                nX = P.x + direction;
                nY = Map(nX);
            }
            else
            {
                if (P.y == FinalPoint.y)
                {
                    return P;
                }
                nY = P.y + direction;
                nX = P.x;
            }
            return new Point(nX, nY, 0);
        }

        float Map(float i)
        {
            return i * Slope + intercept;
        }
    }
}

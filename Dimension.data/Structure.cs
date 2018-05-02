﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dimension.data
{
    public class Structure
    {
        public Dictionary<int, object> WireFrame;
        public bool Composite;
        public Point CenterPoint=new Point(0,0,0); //avg of all points a 3D space center

        public int ID;

        //in case of a shape these 2 functions operate on morphs/Lines
        //However in case of structure composition they operate on shapes

        public float getAvgZScore()
        {
            float ZScore = 0.0F;
            int count = 0;

            foreach (object obj in WireFrame.Values)
            {
                Bound B = (Bound)obj;
                foreach (Point P in B.wireFrameSegment)
                {
                    ZScore += P.z;
                    count++;
                }
            }
            return ZScore / count;
        }

        public float getMaxZScore()
        {
            float curMax = float.MinValue;

            foreach (object obj in WireFrame.Values)
            {
                Bound B = (Bound)obj;
                foreach (Point P in B.wireFrameSegment)
                {
                    if (P.z > curMax)
                    {
                        curMax = P.z;
                    }
                }
            }

            return curMax;
        }

        public void updateCenterPoint() //assigns the average of all points to centerPoint
        {
            throw new NotImplementedException();
        }

        public virtual int bind(object S) //You create a shape/Bound you get a refrence ID
        {
            throw new NotImplementedException();
        }

        public virtual object unbind(int i) //you destroy a shape/Bound you get a copy
        {
            throw new NotImplementedException();
        }

        //Lists to track transformations - easier to work with when applying textures
        //Alternative is to store 2 wireframes an original and a modified
        public Dictionary<Dimension, Angle> Rotation = new Dictionary<Dimension, Angle>
        {
            {Dimension.X,new Angle(0)},
            {Dimension.Y,new Angle(0)},
            {Dimension.Z,new Angle(0)}
        };

        public Dictionary<Dimension, int> Scaling = new Dictionary<Dimension, int>
        {
            {Dimension.X,0},
            {Dimension.Y,0},
            {Dimension.Z,0}
        };
    }
}
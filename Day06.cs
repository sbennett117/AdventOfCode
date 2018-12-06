using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day06
    {
        int ClosestPoint(List<Coordinate> points, int targetx, int targety)
        {
            int minID = -1;
            int minDist = int.MaxValue;

            foreach(Coordinate point in points)
            {
                int manDist = Math.Abs(point.x - targetx) + Math.Abs(point.y - targety);
                if (minDist == manDist)
                {
                    minID = '.';
                    continue;
                }
                minDist = Math.Min(minDist, manDist);
                minID = minDist == manDist ? point.id : minID;
            }

            return minID;
        }

        public int LargestArea()
        {
            string[] lines = System.IO.File.ReadAllLines(@"Day06.txt");
            List<Coordinate> points = new List<Coordinate>();
            for (int i = 0; i < lines.Length; i++)
            {
                string[] ps = lines[i].Split(',');
                points.Add(new Coordinate(i, int.Parse(ps[0]), int.Parse(ps[1].Trim())));
            }

            Dictionary<int, int> rankedPoints = new Dictionary<int, int>();

            for (int i = 0; i < 400; i++)
            {
                for (int j = 0; j < 400; j++)
                {
                    int closestID = ClosestPoint(points, i, j);
                    if(!rankedPoints.ContainsKey(closestID))
                    {
                        rankedPoints.Add(closestID, 0);
                    }
                    if(i == 0 || j == 0 || i == 399 || j == 399 || rankedPoints[closestID] == -1)
                    {
                        rankedPoints[closestID] = -1;
                    }
                    else
                    {
                        rankedPoints[closestID]++;
                    }
                }
            }

            int maxArea = 0;
            foreach(int key in rankedPoints.Keys)
            {
                maxArea = Math.Max(maxArea, rankedPoints[key]);
            }

            return maxArea;
        }

        int TotalManhattan(List<Coordinate> points, int targetx, int targety)
        {
            int distance = 0;

            foreach (Coordinate point in points)
            {
                distance += Math.Abs(point.x - targetx) + Math.Abs(point.y - targety);
            }

            return distance;
        }

        public int CloseRegion()
        {
            string[] lines = System.IO.File.ReadAllLines(@"Day06.txt");
            List<Coordinate> points = new List<Coordinate>();
            for (int i = 0; i < lines.Length; i++)
            {
                string[] ps = lines[i].Split(',');
                points.Add(new Coordinate(i, int.Parse(ps[0]), int.Parse(ps[1].Trim())));
            }

            int regionArea = 0;

            for (int i = 0; i < 400; i++)
            {
                for (int j = 0; j < 400; j++)
                {
                    if (TotalManhattan(points, i, j) < 10000)
                    {
                        regionArea++;
                    }
                }
            }

            return regionArea; 
        }
    }

    class Coordinate
    {
        internal int id;
        internal int x;
        internal int y;

        public Coordinate(int id, int x, int y)
        {
            this.id = id;
            this.x = x;
            this.y = y;
        }
    }
}

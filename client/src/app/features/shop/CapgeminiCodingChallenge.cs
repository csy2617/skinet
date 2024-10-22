using System;
using System.Collections.Generic;

public class Sampler {

    // ---- 2.
    /*
      Write a function that accepts two Paths and returns the portion of the first Path that is not
      common with the second, which is to say portion of the first path starting from where the two
      paths diverged.
  
      For example, RelativeToCommonBase("/home/daniel/memes", "/home/daniel/work") should
      produce "/home/daniel".
    */
    /// <summary>
    /// Finds the common base path between two given file paths.
    /// </summary>
    /// <param name="path1">The first file path.</param>
    /// <param name="path2">The second file path.</param>
    /// <returns>
    /// The common base path as a string, or an empty string if either <paramref name="path1"/> or <paramref name="path2"/> is null or empty,
    /// or if no common base path exists.
    /// </returns>
    public static String RelativeToCommonBase(String path1, String path2)
    {
        if (string.IsNullOrEmpty(path1) || string.IsNullOrEmpty(path2))
        {
            return string.Empty;
        }

        path1 = path1.Trim('/');
        path2 = path2.Trim('/');

        string[] path1Split = path1.Split('/');
        string[] path2Split = path2.Split('/');

        List<String> commonBase = new List<string>();

        for(int i=0; i < Math.Min(path1Split.Length, path2Split.Length); i++)
        {
            if(path1Split[i] == path2Split[i])
            {
                commonBase.Add(path1Split[i]);
            }else
            {
                break;
            }
        }

        return commonBase.Count == 0 ? string.Empty : "/" +  string.Join("/", commonBase);
    }

    // ---- 2.
    /*
      Write a function that accepts a string as the first parameter, and a
      list of strings as the second parameter, and returns a string from the
      list that is "most like" the first string. The choice of algorithm 
      is yours.
    */
    /// <summary>
    /// Finds the string from a list that is "most like" to the given input string.
    /// Uses the Levenshtein distance algorithm to determine similarity.
    /// </summary>
    /// <param name="word">The input string to compare against.</param>
    /// <param name="possibilities">An array of possible strings to compare with.</param>
    /// <returns>
    /// The string from the list that is most similar to the input string, 
    /// or an empty string if the input string or possibilities list is invalid.
    /// </returns>
    public static String ClosestWord(String word, String[] possibilities)
    {
        if (string.IsNullOrEmpty(word) || possibilities == null || possibilities.Length == 0)
        {
            return string.Empty; 
        }

        string closestMatch = possibilities[0];
        int minDistance = int.MaxValue;

        foreach (string possibility in possibilities)
        {
            int distance = calcLevenshteinDistance(word, possibility);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestMatch = possibility;
            }
        }

        return closestMatch;
    }

    /// <summary>
    /// Calculates the Levenshtein distance between two strings.
    /// This distance is the minimum number of single-character edits (insertions, deletions, or substitutions)
    /// required to change one string into the other.
    /// </summary>
    /// <param name="a">The first string.</param>
    /// <param name="b">The second string.</param>
    /// <returns>
    /// The Levenshtein distance between the two strings.
    /// </returns>
    private static int calcLevenshteinDistance(string a, string b)
    {
        int[,] editDistanceMatrix = new int[a.Length + 1, b.Length + 1];

        for (int i = 0; i <= a.Length; i++)
        {
            for (int j = 0; j <= b.Length; j++)
            {
                if (i == 0)
                {
                    editDistanceMatrix[i, j] = j; 
                }
                else if (j == 0)
                {
                    editDistanceMatrix[i, j] = i; 
                }
                else
                {
                    int cost = (a[i - 1] == b[j - 1]) ? 0 : 1;
                    int deletionCost = editDistanceMatrix[i - 1, j] + 1; 
                    int insertionCost = editDistanceMatrix[i, j - 1] + 1; 
                    int substitutionCost = editDistanceMatrix[i - 1, j - 1] + cost; 

                    editDistanceMatrix[i, j] = Math.Min(Math.Min(deletionCost, insertionCost), substitutionCost);
                }
            }
        }

        return editDistanceMatrix[a.Length, b.Length];
    }


    // ----3.
    /*
      Pretend there is a vehicle traveling along a path. The path is represented
      by a list of x, y points and a unix timestamp at that point 
      (the PointIntime struct).  The vehicle travels
      in straight lines between those points and passes through each point at
      the corresponding timestamp. Given this list of points and timestamps,
      and a time seconds (relative to the first timestamp), write a function
      that returns the instantaneous speed of the fake vehicle at that timestamp.
    */

    public struct PointInTime
    {
    /// <summary>
    /// Initializes a new instance of the <see cref="PointInTime"/> struct.
    /// </summary>
    /// <param name="x">The x-coordinate of the point.</param>
    /// <param name="y">The y-coordinate of the point.</param>
    /// <param name="timestamp">The Unix timestamp for the point.</param>
        public PointInTime(double x, double y, double timestamp)
        {
            X = x;
            Y = y;
            Timestamp = timestamp;
        }
  
        public double X { get; }
        public double Y { get; }
        public double Timestamp { get; }
  
        public override string ToString() => $"({X}, {Y}, {Timestamp})";
    }

    /// <summary>
    /// Calculates the instantaneous speed of a vehicle at a given time based on its path.
    /// The speed is calculated as the distance between two points divided by the time taken 
    /// to travel between them.
    /// </summary>
    /// <param name="atTime">The time at which to calculate the speed, relative to the first timestamp.</param>
    /// <param name="path">An array of <see cref="PointInTime"/> representing the vehicle's path.</param>
    /// <returns>
    /// The speed of the vehicle at the specified time, or 0 if the time is outside the path range.
    /// </returns>
    public static double SpeedAtTime(double atTime, PointInTime[] path)
    {
        for (int i = 0; i < path.Length - 1; i++)
        {
            if (atTime >= path[i].Timestamp && atTime <= path[i + 1].Timestamp)
            {
                PointInTime point1 = path[i];
                PointInTime point2 = path[i + 1];

                double timeDifference = point2.Timestamp - point1.Timestamp;
                double distance = Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2));

                if (timeDifference == 0)
                {
                    return 0; 
                }

                double speed = distance / timeDifference;

                return speed;
            }
        }

        return 0;
    }
}
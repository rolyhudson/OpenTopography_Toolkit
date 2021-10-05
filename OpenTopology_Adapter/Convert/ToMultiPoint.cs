using BH.oM.Geospatial;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH.Adapter.OpenTopology
{
    
    public static partial class Convert
    {
        [Description("Convert an Arc ASCII Grid to MultiPoint.")]
        public static MultiPoint ToMultiPoint(string aSCIIGrid)
        {
            //if it is a file
            if (Directory.Exists(aSCIIGrid))
            {
                using (StreamReader sr = new StreamReader(aSCIIGrid))
                    return ToMultiPoint(sr.ReadToEnd());
            }
            //or it is the single string from an http response
            return ToMultiPoint(aSCIIGrid.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None));
        }

        private static MultiPoint ToMultiPoint(string[] lines)
        {
            double xll = 0;
            double yll = 0;
            double cell = 0;
            double nrows = 0;
            int row = 0;
            MultiPoint dtm = new MultiPoint();
            foreach (string line in lines)
            {
                if (String.IsNullOrEmpty (line))
                    continue;
                string current = line;
                //remove first char if space
                if (line[0] == ' ')
                    current = line.Substring(1);
                string[] parts = current.Split(' ');
                
                double num = 0;

                if (parts[0] == "nrows")
                    nrows = GetNumeric(parts);

                if (parts[0] == "xllcorner")
                    xll = GetNumeric(parts);

                if (parts[0] == "yllcorner")
                    yll = GetNumeric(parts);

                if (parts[0] == "cellsize")
                    cell = GetNumeric(parts);


                if (Double.TryParse(parts[0], out num))
                {
                    for (int col = 0; col < parts.Length; col++)
                    {
                        double lon = col * cell + xll;
                        double lat = (nrows - row) * cell + yll;
                        double alt = 0;
                        Double.TryParse(parts[col], out alt);
                        dtm.Points.Add(new Point() { Longitude = lon, Latitude = lat, Altitude = alt });

                    }
                    row++;
                }
            }
            return dtm;
        }

        private static double GetNumeric(string[] parts)
        {
            double num = 0;
            foreach (string p in parts)
            {
                if (Double.TryParse(p, out num))
                    return num;
            }
            return Double.MinValue;
        }
    }
}

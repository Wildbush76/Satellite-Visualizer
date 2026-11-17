using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;


namespace AFSPC.AstroStds.Models.DisplayModel
{
   /// <summary>
   /// A helper class to assist in sorting Colors from the .Net Base Class Library
   /// </summary>
   public static class Colors 
   {
      private static List<Color> namedColorsCache;

      /// <summary>
      /// Gets the colors from <see cref="System.Drawing.KnownColor"/> without the values of <see cref="System.Drawing.SystemColors"/>
      /// </summary>
      /// <value>The named colors.</value>
      public static List<Color> NamedColors
      {
         get
         {
            if (namedColorsCache == null)
            {
               string[] allKnownColorNames = ((KnownColor[])Enum.GetValues(typeof(KnownColor))).Select(color => color.ToString()).ToArray();
               string[] systemColorNames = typeof(SystemColors).GetProperties(BindingFlags.Public | BindingFlags.Static).Select(property => property.Name).ToArray();
               namedColorsCache = allKnownColorNames.Except(systemColorNames).Select(name => Color.FromName(name)).ToList();
            }
            return namedColorsCache;
         }
      }

      /// <summary>
      /// Shades the specified basecolor.
      /// </summary>
      /// <param name="basecolor">The basecolor.</param>
      /// <param name="shadeval">The shade value (higher value means lighter).</param>
      /// <returns></returns>
      public static Color Shade(this Color basecolor, double shadeval)
      {
         int graycolorvalue = (int)((shadeval * 1.54) + 50);

         int redVal;
         int greenVal;
         int blueVal;

         if (graycolorvalue < 128)
         {
            redVal = (int)((basecolor.R * graycolorvalue) / 127);
            greenVal = (int)((basecolor.G * graycolorvalue) / 127);
            blueVal = (int)((basecolor.B * graycolorvalue) / 127);
         }
         else
         {
            redVal = (int)(255 - ((255 - basecolor.R) * (255 - graycolorvalue) / 127));
            greenVal = (int)(255 - ((255 - basecolor.G) * (255 - graycolorvalue) / 127));
            blueVal = (int)(255 - ((255 - basecolor.B) * (255 - graycolorvalue) / 127));
         }

         System.Drawing.Color retval = System.Drawing.Color.FromArgb(redVal, greenVal, blueVal);
         return retval;
      }
      /// <summary>
      /// List of default colors to use for randomly assigning colors.
      /// </summary>
      public static List<Color> defaultColors = Colors.NamedColors
          .Where(color => color.GetBrightness() > .3)
          .Where(color => color.GetBrightness() < .8)
          .ToList();
      /// <summary>
      /// Used the randomly pick a color from the defaultColors
      /// </summary>
      public static Random random = new Random();
      /// <summary>
      /// List to keep track of the different colors assigned to a specific type of model and specific key
      /// </summary>
      private static SortedList<string, SortedList<string, Color>> assignedColorsPerType = new SortedList<string, SortedList<string, Color>>();

   }
   // Summary:
   //     Specifies the known system colors.

}
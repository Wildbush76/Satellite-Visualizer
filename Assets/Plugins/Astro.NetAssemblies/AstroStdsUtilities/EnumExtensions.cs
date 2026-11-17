using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AFSPC.AstroStds.Utilities
{
   public static class EnumExtensions
   {
      public static string ToDescription(this System.Enum value)
      {
         FieldInfo fi = value.GetType().GetField(value.ToString());
         var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
         return attributes.Length > 0 ? attributes[0].Description : value.ToString();
      }

      /// <summary>
      /// Enumerates all enum values
      /// </summary>
      /// <typeparam name="T">Enum type</typeparam>
      /// <returns>IEnumerable containing all enum values</returns>
      /// <see cref="http://stackoverflow.com/questions/972307/can-you-loop-through-all-enum-values"/>
      public static IEnumerable<T> GetValues<T>()
      {
         return Enum.GetValues(typeof(T)).Cast<T>();
      }

      /*
      public static Enum GetSelectedValue(this System.Enum value, ComboBox comboBox)
      {
         Enum.TryParse(cmbEpochPlacement.SelectedValue.ToString(), out BatchDCWrapper.EpochPlacementCtrl selVal);
      }
      */
      
   }
}

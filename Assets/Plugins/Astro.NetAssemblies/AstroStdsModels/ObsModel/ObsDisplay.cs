using AFSPC.AstroStds.Models.DisplayModel;
using System;


namespace AFSPC.AstroStds.Models.ObsModel
{
   [Serializable]
   public class ObsDisplay : ObjectDisplay
   {
      //public bool Selected { get; set; } // for selected, primary, or reference
      public Obs Ob { get; set; }


      public ObsDisplay(long obsKey)
         : base() // call base constructor to generate a random color
      {
         this.ID = obsKey;
         this.Label = " "; 

         this.Selected = true;
         this.Ob = new Obs(obsKey);
      }


      public ObsDisplay(long obsKey, bool checkedState)
         : this(obsKey) // call base constructor to get the random color
      {
         this.Selected = checkedState;
      }



      public ObsDisplay(ObjectDisplay objDisp)
         : this((int)objDisp.ID)
      {
         this.Color = objDisp.Color; // Copy objDisp's current color
         this.Label = objDisp.Label;
      }

   }

}

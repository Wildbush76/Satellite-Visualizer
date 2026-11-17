using AFSPC.AstroStds.Wrappers;
using AFSPC.AstroStds.Models.DisplayModel;
using System;

namespace AFSPC.AstroStds.Models.ObsModel
{
   [Serializable]
   // class for space objects (satellite, sensor, obs) that are being used to communicate between GUI controls
   public class ObsFileDisplay : ObjectDisplay
   {
      public int NumOfObs { get; set; } // number of obs in the obs group id

      public ObsFileDisplay(long gid, string fileName)
         : base() // call base constructor to generate a random color
      {
         this.ID = gid; // group id
         this.Label = fileName;

         this.NumOfObs = ObsWrapper.ObsGetCountGID((int)gid);
      }


      public ObsFileDisplay(ObjectDisplay objDisp)
         : this(objDisp.ID, objDisp.Label)
      {
         this.Color = objDisp.Color; // Copy objDisp's current color
      }

   }

}

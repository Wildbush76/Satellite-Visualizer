//using System.Threading.Tasks;

namespace AFSPC.AstroStds.Models
{
   public enum DisplayModeEnum
   {
      SelectionOnly,    // Select one column only
      RefPredMode,      // Reference and Predicted checkbox columns for EVE (Reference column is implemented as radiobutton column
      PrimSecMode,       // Primary and Secondary checkbox columns for Combo
      MasterList,       // more fields
   };

   // Different checkboxes for different application (Main satellite list, 
   // Combo primary/secondary satellite list, SuperEVE  base/predicted satellite list
   public enum CheckedColumnEnum
   {
      Selected,
      Primary,
      Secondary,
      Reference,
      Predicted,
   };

   public enum SpaceObjectType
   {
      Satellites,
      Sensors,
      Obs,
   };

   // Different types of loading orbital elements
   public enum LoadSatType
   {
      TLEs,
      VCMs,
      SPVECs,
      ExtEphems,
      AllTypes,
      XMLs,
   };
}

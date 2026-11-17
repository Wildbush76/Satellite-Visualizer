using System.Collections.Generic;
using AFSPC.AstroStds.Wrappers;

namespace AFSPC.AstroStds.Models
{
   /// <summary>
   /// Class to collect info messages received from the AstroStds dlls
   /// </summary>
   public static class InfoMessages
   {
      private static List<string> infoMessages = new List<string>();

      /// <summary>
      /// Reset the info messages list
      /// </summary>
      public static void Reset()
      {
         infoMessages.Clear();
      }

      /// <summary>
      /// Retrieve the last info message and add it to the list of info messages
      /// </summary>
      public static void SaveInfoMessage()
      {
         infoMessages.Add(DllMainWrapper.GetLastInfoMsgStr());
      }

      /// <summary>
      /// Add an external message to the list of messages
      /// </summary>
      /// <param name="inputMessage">an info message</param>
      public static void SaveInfoMessage(string inputMessage)
      {
         infoMessages.Add(inputMessage);
      }


      /// <summary>
      /// Return the list of stored messages
      /// </summary>
      /// <returns>The current list of stored messages</returns>
      public static List<string> GetInfoMessages()
      {
         return infoMessages;
      }


      /// <summary>
      /// Check to see if the current message list is empty
      /// </summary>
      /// <returns></returns>
      public static bool IsEmpty()
      {
         return infoMessages.Count == 0;
      }
   }
}

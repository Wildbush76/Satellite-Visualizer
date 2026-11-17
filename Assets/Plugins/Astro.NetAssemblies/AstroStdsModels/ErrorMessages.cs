// ***********************************************************************
// Assembly         : AFSPC.AstroStds.Models
// Author           : nguyend
// Created          : 05-16-2017
//
// Last Modified By : nguyend
// Last Modified On : 05-16-2017
// ***********************************************************************
// <copyright file="ErrorMessages.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using AFSPC.AstroStds.Wrappers;

namespace AFSPC.AstroStds.Models
{
   /// <summary>
   /// Class to collect error messages received from the AstroStds dlls
   /// </summary>
   public static class ErrorMessages
   {
      private static List<string> errorMessages = new List<string>();

      /// <summary>
      /// Reset the error messages list
      /// </summary>
      public static void Reset()
      {
         errorMessages.Clear();
      }

      /// <summary>
      /// Retrieve the last error message and add it to the list of error messages
      /// </summary>
      public static void SaveErrorMessage()
      {
         errorMessages.Add(DllMainWrapper.GetLastErrMsgStr());
      }

      /// <summary>
      /// Add an external message to the list of messages
      /// </summary>
      /// <param name="inputMessage">an error message</param>
      public static void SaveErrorMessage(string inputMessage)
      {
         errorMessages.Add(inputMessage);
      }


      /// <summary>
      /// Return the list of stored messages
      /// </summary>
      /// <returns>The current list of stored messages</returns>
      public static List<string> GetErrorMessages()
      {
         return errorMessages;
      }


      /// <summary>
      /// Check to see if the current message list is empty
      /// </summary>
      /// <returns></returns>
      public static bool IsEmpty()
      {
         return errorMessages.Count == 0;
      }
   }
}

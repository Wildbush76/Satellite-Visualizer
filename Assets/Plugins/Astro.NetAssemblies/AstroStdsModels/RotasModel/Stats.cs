using LinqStatistics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFSPC.AstroStds.Models.RotasModel
{
   [Serializable]
   public class Stats
	{
		public Stats() 
		{
			numResids = 0;
			biasCalc = null;
			sigmaCalc = null;

		}

		public int numResids { get; set; }
		public double? biasCalc { get; set;}
		public double? sigmaCalc { get; set;}

		public Stats(List<double> inList)
		{
			List<double> residList = inList.Where(x => x!= 0).ToList();
			this.numResids = residList.Count();
			numResids = residList.Count;
			if (residList.Count > 0)
			{
				biasCalc = residList.Average();
				sigmaCalc = residList.StandardDeviation();
			}
			else { biasCalc = null; sigmaCalc = null; }
		}
	}
}

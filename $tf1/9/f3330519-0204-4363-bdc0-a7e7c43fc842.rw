/*
 * Created by SharpDevelop.
 * User: miniatas
 * Date: 7/20/2011
 * Time: 5:27 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace ShopFloor
{
	using System;
	
	/// <summary>
	/// Converts a number to its corresponding capital letter - i.e. 1=A, etc.
	/// </summary>
	public static class ConvertClass
	{
		public static string GetLaneName(int laneNumber) 
		{     
			int dividend = laneNumber;     
			string laneName = string.Empty;     
			int modulo;      
			while (dividend > 0)     
			{         
				modulo = (dividend - 1) % 26;         
				laneName += Convert.ToChar(65 + modulo).ToString();         
				dividend = (int)((dividend - modulo) / 26);     
			}     
			
			return laneName; 
		}
	}
}

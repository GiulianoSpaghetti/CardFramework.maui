/*
  *  This code is distribuited under GPL 3.0 or, at your opinion, any later version
 *  CBriscola 1.1.3
 *
 *  Created by Giulio Sorrentino (numerone) on 29/01/23.
 *  Copyright 2023 Some rights reserved.
 *
 */

namespace org.altervista.numerone.framework
{
	public interface CartaHelper
	{
		UInt16 GetSeme(UInt16 Carta);
		UInt16 GetValore(UInt16 Carta);
		UInt16 GetPunteggio(UInt16 Carta);
		string GetSemeStr(UInt16 Carta, string s0, string s1, string s2, string s3);
		UInt16 GetNumero(UInt16 seme, UInt16 valore);

		public int CompareTo(ushort carta, ushort carta1);
	};
}
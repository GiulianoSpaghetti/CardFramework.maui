/*
  *  This code is distribuited under GPL 3.0 or, at your opinion, any later version
 *  CBriscola 1.1.3
 *
 *  Created by Giulio Sorrentino (numerone) on 29/01/23.
 *  Copyright 2023 Some rights reserved.
 *
 */

using System;
namespace org.altervista.numerone.framework

{
    public class ElaboratoreCarteBriscola : ElaboratoreCarte
	{
		private UInt16 numeroCarte = 40;
		private bool[] doppione;
		private UInt16 CartaBriscola, min, max;
		private bool inizio,
				 briscolaDaPunti;
		public static Random r = new Random();
		public ElaboratoreCarteBriscola(bool punti = true, UInt16 a=40, UInt16 m=0, UInt16 n=39)
		{
			inizio = true;
			briscolaDaPunti = punti;
			doppione = new bool[a+min];
			min = m;
			max = n;
			numeroCarte = a;
			if (numeroCarte != max - min + 1)
				throw new ArgumentOutOfRangeException("Chiamata ad elaboratorecartebriscola con numeroCarte!=max-min+1");
            for (int i = 0; i < numeroCarte; i++)
                doppione[i] = false;
        }
        public UInt16 GetCarta()
		{
			UInt16 fine = (UInt16)(r.Next(min, max)),
			Carta = (UInt16)((fine + 1) % numeroCarte);
			while (doppione[Carta] && Carta != fine)
			{
				Carta = (UInt16)((Carta + 1) % numeroCarte);
				if (Carta < min)
					Carta = min;
			}
			if (doppione[Carta])
				throw new ArgumentException("Chiamato elaboratoreCarteItaliane::getCarta() quando non ci sono più carte da elaborare");
			else
			{
				if (inizio)
				{
					UInt16 valore = (UInt16)(Carta % 10);
					if (!briscolaDaPunti && (valore == 0 || valore == 2 || valore > 6))
					{
						Carta = (UInt16)(Carta - valore + 1);
					}
					CartaBriscola = Carta;
					inizio = false;
				}
				doppione[Carta] = true;
				return Carta;
			}
		}

		public UInt16 GetCartaBriscola() { return CartaBriscola; }

        public ushort GetNumeroCarte()
        {
            return numeroCarte;
        }
    }
}
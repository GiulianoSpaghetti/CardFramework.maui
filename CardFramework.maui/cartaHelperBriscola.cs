/*
 *  This code is distribuited under GPL 3.0 or, at your opinion, any later version
 *
  *  Created by Giulio Sorrentino (numerone, Giuliano Spaghetti).
*  Copyright 2023-2025 Some rights reserved.
 *
 */

namespace org.altervista.numerone.framework.briscola
{
    /// <summary>
    /// Interfaccia per modificare il comportamento della classe carta in base al gioco della briscola
    /// </summary>
    public class CartaHelper : org.altervista.numerone.framework.CartaHelper
    {
        /// <summary>
        /// numer intero indicante la carta di briscola, viene salvato per il compareto
        /// </summary>
        private readonly UInt16 CartaBriscola;
        /// <summary>
        /// costruttore che inizializza l'intero indicante la carta di briscola
        /// </summary>
        /// <param name="briscola">intero di briscola</param>
        public CartaHelper(UInt16 briscola) { CartaBriscola = briscola; }
        /// <summary>
        /// retituisce il seme della carta indicata
        /// </summary>
        /// <param name="Carta">carta di cui prendere il seme</param>
        /// <returns>un numero intero, verosimilmemnte da 0 a 4</returns>
        public UInt16 GetSeme(UInt16 Carta) {
			return (UInt16)(Carta / 10);
		}
        /// <summary>
        /// rettuisce il valore della carta indicata
        /// </summary>
        /// <param name="Carta">carta di cui prendere il valore</param>
        /// <returns>un numero intero, verosimilmente da 0 a 9</returns>
        public UInt16 GetValore(UInt16 Carta) {
			return (UInt16)(Carta % 10);
		}
        /// <summary>
        /// resituisce il punteggio della carta indicata
        /// </summary>
        /// <param name="Carta">carta di cui prendere il punteggio</param>
        /// <returns>restituisce il punteggio sulla base del gioco che si sta facendo</returns>
        public UInt16 GetPunteggio(UInt16 Carta) {
			UInt16 valore = 0;
			switch (Carta % 10) {
				case 0: valore = 11; break;
				case 2: valore = 10; break;
				case 9: valore = 4; break;
				case 8: valore = 3; break;
				case 7: valore = 2; break;
			}
			return valore;
		}
        /// <summary>
        /// retituisce il seme in formato stringa della carta presa in esame (uno tra s0 e s7)
        /// </summary>
        /// <param name="Carta">carta da prendere in esame</param>
        /// <param name="s0">primo seme italiano</param>
        /// <param name="s1">secondo seme italiano</param>
        /// <param name="s2">terzo seme italiano</param>
        /// <param name="s3">quarto seme italiano</param>
        public string GetSemeStr(UInt16 Carta, string s0, string s1, string s2, string s3)
		{
			string s = "";
			switch (Carta / 10)
			{
				case 0: s = s0 ; break;
				case 1: s = s1; break;
				case 2: s = s2; break;
				case 3: s = s3; break;
				default: throw new ArgumentException("Chiamato getsemestr con seme > 3");
			}
			return s;
		}
        /// <summary>
        /// Accoppia seme e valore per tornare l'intero indicante la carta
        /// </summary>
        /// <param name="seme">seme della carta</param>
        /// <param name="valore">valore della carta</param>
        /// <returns>intero indicante la carta</returns>
        public UInt16 GetNumero(UInt16 seme, UInt16 valore) {
			if (seme > 4 || valore > 9)
				throw new ArgumentException($"Chiamato CartaHelperBriscola::getNumero con seme={seme} e valore={valore}");
			return (UInt16)(seme * 10 + valore);
		}
        /// <summary>
        /// Retituisce la struttura indicante la carta di briscola
        /// </summary>
        /// <returns></returns>
        public Carta GetCartaBriscola() { return Carta.GetCarta(CartaBriscola); }
        /// <summary>
        /// Compara le due carte per stabilire ch sia la maggiore
        /// </summary>
        /// <param name="Carta">prima carta presa in esame</param>
        /// <param name="Carta1">seconda carta presa in esame</param>
        /// <returns>-1 se maggiore la prima, zero se uguale, 1 se maggiore la seconda</returns>
        public int CompareTo(UInt16 Carta, UInt16 Carta1) {
			UInt16 punteggio = GetPunteggio(Carta),
				   punteggio1 = GetPunteggio(Carta1),
				   valore = GetValore(Carta),
				   valore1 = GetValore(Carta1),
				   semeBriscola = GetSeme(CartaBriscola),
				   semeCarta = GetSeme(Carta),
					  semeCarta1 = GetSeme(Carta1);
			if (punteggio < punteggio1)
				return 1;
			else if (punteggio > punteggio1)
				return -1;
			else {
				if (valore < valore1 || (semeCarta1 == semeBriscola && semeCarta != semeBriscola))
					return 1;
				else if (valore > valore1 || (semeCarta == semeBriscola && semeCarta1 != semeBriscola))
					return -1;
				else
					return 0;
			}
		}
	}
}
/*
 *  This code is distribuited under GPL 3.0 or, at your opinion, any later version
 *
 *  Created by Giulio Sorrentino (numerone, Giuliano Spaghetti).
 *  Copyright 2023-2025 Some rights reserved.
 *
 */

namespace org.altervista.numerone.framework
{   
	/// <summary>
    /// Interfaccia per modificare il comportamento della classe carta
    /// </summary>
    public interface CartaHelper
	{
        /// <summary>
        /// Interfaccia per modificare il comportamento della classe carta
        /// </summary>
        UInt16 GetSeme(UInt16 Carta);
        /// <summary>
        /// rettuisce il valore della carta indicata
        /// </summary>
        /// <param name="Carta">carta di cui prendere il valore</param>
        /// <returns>un numero intero, verosimilmente da 0 a 9</returns>
        UInt16 GetValore(UInt16 Carta);
        /// <summary>
        /// resituisce il punteggio della carta indicata
        /// </summary>
        /// <param name="Carta">carta di cui prendere il punteggio</param>
        /// <returns>restituisce il punteggio sulla base del gioco che si sta facendo</returns>
        UInt16 GetPunteggio(UInt16 Carta);
        /// <summary>
        /// retituisce il seme in formato stringa della carta presa in esame (uno tra s0 e s7)
        /// </summary>
        /// <param name="m">mazzo col nome</param>
        /// <param name="Carta">carta da prendere in esame</param>
        /// <param name="s0">primo seme italiano</param>
        /// <param name="s1">secondo seme italiano</param>
        /// <param name="s2">terzo seme italiano</param>
        /// <param name="s3">quarto seme italiano</param>
        /// <param name="s4">prima delle 4 stringhe indicante il seme framcese</param>
        /// <param name="s5">seconda delle 4 stringhe indicante il seme francese</param>
        /// <param name="s6">terza delle 4 stringhe indicante il seme francese</param>
        /// <param name="s7">quarta delle 4 stringhe indicante il seme francese</param>
        string GetSemeStr(Mazzo m, UInt16 Carta, string s0, string s1, string s2, string s3, string s4, string s5, string s6, string s7);
        /// <summary>
        /// Accoppia seme e valore per tornare l'intero indicante la carta
        /// </summary>
        /// <param name="seme">seme della carta</param>
        /// <param name="valore">valore della carta</param>
        /// <returns>intero indicante la carta</returns>
        UInt16 GetNumero(UInt16 seme, UInt16 valore);
        /// <summary>
        /// Compara le due carte per stabilire ch sia la maggiore
        /// </summary>
        /// <param name="carta">prima carta presa in esame</param>
        /// <param name="carta1">seconda carta presa in esame</param>
        /// <returns>-1 se maggiore la prima, zero se uguale, 1 se maggiore la seconda</returns>
        public int CompareTo(ushort carta, ushort carta1);
	};
}
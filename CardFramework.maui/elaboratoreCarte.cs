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
    /// Interfaccia per modificare il comportamento della classe mazzo
    /// </summary>
    public interface ElaboratoreCarte
    {
        /// <summary>
        /// restituisce la prossima carta del mazzo
        /// </summary>
        /// <returns>prende la prima carta non ancora uscita</returns>
        UInt16 GetCarta();
        /// <summary>
        /// Dice quante carte sono rimaste nel mazzo
        /// </summary>
        /// <returns>Retituisce il numero di carte nel mazzo</returns>
        UInt16 GetNumeroCarte();
    };
}
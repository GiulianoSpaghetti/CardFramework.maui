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
    /// <summary>
    /// Classe per modificare il comportamento della classe giocatore in base all'esperienza di gioco dell'utente
    /// </summary>
    public class GiocatoreHelperUtente : GiocatoreHelper
	{
		public GiocatoreHelperUtente()
		{
			;
		}
        /// <summary>
        /// restituisce la carta scelta dall'utente, cio� i, sempre se papabile
        /// </summary>
        /// <param name="i">carta scelta dall'utente</param>
        /// <param name="v">vettore delle carte</param>
        /// <param name="numeroCarte">dimensione del vettore delle carte</param>
        /// <returns>semplicemente i</returns>
        /// <exception cref="ArgumentException">se i non � nel range</exception>
        public UInt16 Gioca(UInt16 i, Carta[] v, UInt16 numeroCarte)
		{
			if (i < numeroCarte)
				return i;
			else
				throw new ArgumentException("");
		}
        /// <summary>
        /// Restituisce i se � una carta papabile, facendo eventualmente il controllo se � dello stesso seme della carta gi� giocata
        /// </summary>
        /// <param name="i">indice della carta da giocare</param>
        /// <param name="v">vettore delle carte</param>
        /// <param name="numeroCarte">dimensione del vettore delle carte</param>
        /// <param name="c">carta gi� giocata</param>
        /// <param name="stessoSeme">dice se bisogna rispondere al seme oppure no</param>
        /// <returns>i se � pababile</returns>
        /// <exception cref="Exception">la carta indicata non � pababile</exception>
        public UInt16 Gioca(UInt16 i, Carta[] v, UInt16 numeroCarte, Carta c, bool stessoSeme)
		{
            UInt16 carta = Gioca(i, v, numeroCarte);
            if (stessoSeme)
            {
                UInt16 j = 0;
                if (!c.StessoSeme(v[carta]) && !v[i].StessoSeme(Carta.Briscola))
                {
                    for (j = 0; j < numeroCarte && !c.StessoSeme(v[j]); j++) ;
                    if (j != numeroCarte)
                        throw new Exception("Operazione non valida");
                }
            }
            return carta;
        }
        /// <summary>
        /// Aggiorna il punteggio della struttura sulla base delle due carte giocate
        /// </summary>
        /// <param name="punteggioAttuale">il punteggio attuale</param>
        /// <param name="c">la prima carta giocata</param>
        /// <param name="c1">la seconda carta giocata</param>
        public void AggiornaPunteggio(ref UInt16 punteggioAttuale, Carta c, Carta c1)
		{
			punteggioAttuale = (UInt16)(punteggioAttuale + c.Punteggio + c1.Punteggio);
		}
        /// <summary>
        /// metodo da richiamare in presenza del piatto, deve costruirsi i grafi di gioco
        /// </summary>
        /// <param name="i">indice dellc arta da giocare, da non considerare se � la cpu</param>
        /// <param name="v">vettore delle carte da guicare</param>
        /// <param name="numeroCarte">dimensione di v</param>
        /// <param name="piatto">lista delle carte per terra</param>
        /// <returns>indice di v indicante la carta scelta</returns>
        public ushort Gioca(ushort i, Carta[] v, ushort numeroCarte, List<Carta> piatto)
        {
            throw new NotImplementedException();
        }
    };
}
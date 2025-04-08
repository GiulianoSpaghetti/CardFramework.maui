/*
  *  This code is distribuited under GPL 3.0 or, at your opinion, any later version
 *  CBriscola 1.1.3
 *
 *  Created by Giulio Sorrentino (numerone) on 29/01/23.
 *  Copyright 2023 Some rights reserved.
 *
 */


using Microsoft.Maui.Controls.Internals;
using System;

namespace org.altervista.numerone.framework
{
    /// <summary>
    /// Interfaccia per modificare il comportamento della classe cpu
    /// </summary>
    public abstract class GiocatoreHelperCpu : GiocatoreHelper
    {
        /// <summary>
        /// struttura indicante la carta di briscola
        /// </summary>
        protected readonly Carta briscola;
        /// <summary>
        /// Retituisce la prima carta di briscola (la più piccola in mano al giocatore)
        /// </summary>
        /// <param name="mano">mano del giocatore</param>
        /// <param name="numeroCarte">numero di carte</param>
        /// <returns>indice della carta giocata indicante la briscoola più piccola</returns>
        protected UInt16 GetBriscola(Carta[] mano, UInt16 numeroCarte)
        {
            int i= Array.FindIndex(mano, 0, numeroCarte, q => q.Seme == briscola.Seme);
            if (i>numeroCarte)
                i = (UInt16)mano.Length;
            return (UInt16) i;
        }
        /// <summary>
        /// Inizializza la struttura portandosi il seme di briscola
        /// </summary>
        /// <param name="b">intero indicante la carta di briscola</param>
        public GiocatoreHelperCpu(UInt16 b)
        {
            briscola = Carta.GetCarta(b);
        }

        /// <summary>
        /// Retituisce una carta di valore maggiore di quella giocata se c'è dipendentemente se deve essere la più alta o la subito superiore
        /// </summary>
        /// <param name="mano">mano da prendere in esame</param>
        /// <param name="numeroCarte">numero di carte in mano</param>
        /// <param name="c">carta giocata</param>
        /// <param name="maggiore">stabilisce se devee essere quella subito superiore o la più grande in assoluto</param>
        /// <returns>indice della carta se trovata, numeroCarte altrimenti</returns>
        protected UInt16 getSoprataglio(Carta[] mano, UInt16 numeroCarte, Carta c, bool maggiore)
        {
            int i;
            if (maggiore)
                i=Array.FindLastIndex(mano, 0, numeroCarte-1, q => (q.Punteggio>c.Punteggio && q.Seme==c.Seme));
            else
                i = Array.FindIndex(mano, 0, numeroCarte-1, q => (q.Punteggio > c.Punteggio && q.Seme == c.Seme));

            if (i == -1)
                return (UInt16)mano.Length;
            if (i>numeroCarte)
                i = (UInt16) mano.Length;
            return (UInt16) i;
        }
        /// <summary>
        /// Retituisce la prima carta dello stesso seme in mano al giocatore indipendentemente se sia più grande o no
        /// </summary>
        /// <param name="mano">vettore delle carte da prendere in esame</param>
        /// <param name="numeroCarte">dimensione del vettore</param>
        /// <param name="c">carta giocata dall'altro giocatore</param>
        /// <returns>indice della carta se trovata, numeroCarte altrimenti</returns>
        protected UInt16 GetPrimaCartaConSeme(Carta[] mano, UInt16 numeroCarte, Carta c)
        {
            int i=Array.FindIndex(mano, 0, numeroCarte - 1, q => q.Seme == c.Seme);
            if (i >= numeroCarte)
                i = (UInt16)mano.Length;
            return (UInt16)i;
        }
        /// <summary>
        /// se è il primo di mano cerca la carta più difficile da prendere
        /// </summary>
        /// <param name="x">indice della carta da giocare, qui non considerato</param>
        /// <param name="mano">vettore delle carte da prendere in esame</param>
        /// <param name="numeroCarte">dimensione del vettore delle carte</param>
        /// <returns>l'indice dela carta da giocare</returns>
        public UInt16 Gioca(UInt16 x, Carta[] mano, UInt16 numeroCarte)
        {
           int i = Array.FindIndex(mano, 0, numeroCarte - 1, q => (q.Punteggio > 1 && q.Punteggio<5) && q.Seme!=briscola.Seme);
            if (i == -1)
                i = Array.FindIndex(mano, 0, numeroCarte - 1, q => q.Punteggio==0);
            if (i==-1)
                i = Array.FindIndex(mano,0, numeroCarte - 1, q => q.Seme==briscola.Seme);

            if (i >= numeroCarte || i<0)
                i = 0;
            return (UInt16)i;

        }
        /// <summary>
        /// Se è il secondo di mano sceglie quale carta giocare sulla base della carta giocata e se bisogna rispondere al seme
        /// </summary>
        /// <param name="x">indice della carta da giocare, qui non considerato</param>
        /// <param name="mano">vettore delle carte da giocare</param>
        /// <param name="numeroCarte">dimensione del vettore dellke carte</param>
        /// <param name="c">carta giocata dall'altro giocatore</param>
        /// <param name="stessoSeme">indica se bisogna rispondere al seme</param>
        /// <returns>indice di mano indicante la carta da giocare</returns>
        public abstract UInt16 Gioca(UInt16 x, Carta[] mano, UInt16 numeroCarte, Carta c, bool stessoSeme);
        /// <summary>
        /// Deve aggiornare il punteggio del giocatore
        /// </summary>
        /// <param name="punteggioAttuale">il punteggio da aggiornare</param>
        /// <param name="c">la prima carta giocata</param>
        /// <param name="c1">la seconda carta giocata</param>
        public void AggiornaPunteggio(ref UInt16 punteggioAttuale, Carta c, Carta c1) { punteggioAttuale = (UInt16)(punteggioAttuale + c.Punteggio + c1.Punteggio); }
        /// <summary>
        /// retituisce il livello di gioco
        /// </summary>
        /// <returns>il livello di gioco</returns>
        public abstract UInt16 GetLivello();
        /// <summary>
        /// metodo da richiamare in presenza del piatto, deve costruirsi i grafi di gioco
        /// </summary>
        /// <param name="i">indice dellc arta da giocare, da non considerare se è la cpu</param>
        /// <param name="v">vettore delle carte da guicare</param>
        /// <param name="numeroCarte">dimensione di v</param>
        /// <param name="piatto">lista delle carte per terra</param>
        /// <returns>indice di v indicante la carta scelta</returns>
        /// <exception cref="NotImplementedException">nella briscola questo metodo non è inizializzato</exception>
        public ushort Gioca(ushort i, Carta[] v, ushort numeroCarte, List<Carta> piatto)
        {
            throw new NotImplementedException();
        }
    }
}
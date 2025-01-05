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
    /// Interfaccia per modificare il comportamento della classe cpu
    /// </summary>
    public abstract class GiocatoreHelperCpu : GiocatoreHelper
    {
        /// <summary>
        /// struttura indicante la carta di briscola
        /// </summary>
        protected readonly Carta briscola;
        /// <summary>
        /// Retituisce la prima carta di briscola (la pi� piccola in mano al giocatore)
        /// </summary>
        /// <param name="mano">mano del giocatore</param>
        /// <param name="numeroCarte">numero di carte</param>
        /// <returns>indice della carta giocata indicante la briscoola pi� piccola</returns>
        protected UInt16 GetBriscola(Carta[] mano, UInt16 numeroCarte)
        {
            UInt16 i;
            for (i = 0; i < numeroCarte; i++)
                if (briscola.StessoSeme(mano[i]))
                    break;
            return i;
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
        /// Retituisce una carta di valore maggiore di quella giocata se c'� dipendentemente se deve essere la pi� alta o la subito superiore
        /// </summary>
        /// <param name="mano">mano da prendere in esame</param>
        /// <param name="numeroCarte">numero di carte in mano</param>
        /// <param name="c">carta giocata</param>
        /// <param name="maggiore">stabilisce se devee essere quella subito superiore o la pi� grande in assoluto</param>
        /// <returns>indice della carta se trovata, numeroCarte altrimenti</returns>
        protected UInt16 getSoprataglio(Carta[] mano, UInt16 numeroCarte, Carta c, bool maggiore)
        {
            bool trovata = false;
            UInt16 i;
            if (maggiore)
            {
                for (i = (UInt16)(numeroCarte - 1); i < numeroCarte; i--)
                    if (c.StessoSeme(mano[i]) && c.CompareTo(mano[i]) > 0)
                    {
                        trovata = true;
                        break;
                    }
                    else if (c.StessoSeme(mano[i]) && mano[i].CompareTo(c) > 0)
                        break;
            }
            else
            {
                for (i = 0; i < numeroCarte; i++)
                    if (c.StessoSeme(mano[i]) && c.CompareTo(mano[i]) > 0)
                    {
                        trovata = true;
                        break;
                    }
            }
            if (trovata)
                return i;
            else
                return (UInt16)mano.Length;
        }
        /// <summary>
        /// Retituisce la prima carta dello stesso seme in mano al giocatore indipendentemente se sia pi� grande o no
        /// </summary>
        /// <param name="mano">vettore delle carte da prendere in esame</param>
        /// <param name="numeroCarte">dimensione del vettore</param>
        /// <param name="c">carta giocata dall'altro giocatore</param>
        /// <returns>indice della carta se trovata, numeroCarte altrimenti</returns>
        protected UInt16 GetPrimaCartaConSeme(Carta[] mano, UInt16 numeroCarte, Carta c)
        {
            UInt16 ca = numeroCarte;
            for (UInt16 i = 0; i < numeroCarte && ca == numeroCarte; i++)
                if (c.StessoSeme(mano[i]))
                    ca = i;
            return ca;

        }
        /// <summary>
        /// se � il primo di mano cerca la carta pi� difficile da prendere
        /// </summary>
        /// <param name="x">indice della carta da giocare, qui non considerato</param>
        /// <param name="mano">vettore delle carte da prendere in esame</param>
        /// <param name="numeroCarte">dimensione del vettore delle carte</param>
        /// <returns>l'indice dela carta da giocare</returns>
        public UInt16 Gioca(UInt16 x, Carta[] mano, UInt16 numeroCarte)
        {
            UInt16 i;
            for (i = (UInt16)(numeroCarte - 1); i > 0; i--) ;
            if ((mano[i].GetPunteggio() > 4 || briscola.StessoSeme(mano[i])))
                i = 0;
            return i;

        }
        /// <summary>
        /// Se � il secondo di mano sceglie quale carta giocare sulla base della carta giocata e se bisogna rispondere al seme
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
        public void AggiornaPunteggio(ref UInt16 punteggioAttuale, Carta c, Carta c1) { punteggioAttuale = (UInt16)(punteggioAttuale + c.GetPunteggio() + c1.GetPunteggio()); }
        /// <summary>
        /// retituisce il livello di gioco
        /// </summary>
        /// <returns>il livello di gioco</returns>
        public abstract UInt16 GetLivello();
        /// <summary>
        /// metodo da richiamare in presenza del piatto, deve costruirsi i grafi di gioco
        /// </summary>
        /// <param name="i">indice dellc arta da giocare, da non considerare se � la cpu</param>
        /// <param name="v">vettore delle carte da guicare</param>
        /// <param name="numeroCarte">dimensione di v</param>
        /// <param name="piatto">lista delle carte per terra</param>
        /// <returns>indice di v indicante la carta scelta</returns>
        /// <exception cref="NotImplementedException">nella briscola questo metodo non � inizializzato</exception>
        public ushort Gioca(ushort i, Carta[] v, ushort numeroCarte, List<Carta> piatto)
        {
            throw new NotImplementedException();
        }
    }
}
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
    /// Identifica il giocatore
    /// </summary>
    public class Giocatore
	{
        /// <summary>
        /// nome del giocatore
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// la mano del giocatire
        /// </summary>
        private Carta[] mano;
        /// <summary>
        /// se la mano deve essere ordinata
        /// </summary>
        public bool OrdinaMano { get; set; }
		/// <summary>
		/// indice della carta presa in esame
		/// </summary>
        private UInt16 iCarta;
        public Carta CartaGiocata { get => mano[ICartaGiocata]; }
        public UInt16 Punteggio { get; private set; }
        public UInt16 NumeroCarte { get; private set; }
        public UInt16 ICartaGiocata { get; private set; }
        /// <summary>
        /// numero totale di carte presenti nella mano
        /// </summary>
        private readonly UInt16 totaleCarte;
		private readonly GiocatoreHelper helper;
        /// <summary>
        /// helper che condiziona il comportamento della struttura
        /// </summary>
        public enum CARTA_GIOCATA { NESSUNA_CARTA_GIOCATA = UInt16.MaxValue };
        /// <summary>
        /// inzializza la struttura
        /// </summary>
        /// <param name="h">modifica il comportamento della struttura</param>
        /// <param name="n">nome del giocatore</param>
        /// <param name="carte">numero di carte in mano al giocatore</param>
        /// <param name="ordina">se la mano deve essere ordinata</param>
        public Giocatore(GiocatoreHelper h, string n, UInt16 carte, bool ordina = true)
		{
			totaleCarte = carte;
			OrdinaMano = ordina;
			NumeroCarte = carte;
			ICartaGiocata = (UInt16)(CARTA_GIOCATA.NESSUNA_CARTA_GIOCATA);
			Punteggio = 0;
			helper = h;
			Nome = n;
			mano = new Carta[totaleCarte];
			iCarta = 0;
		}
        /// <summary>
        /// Aggiunge una carta nella mano del giocatore, eventualmente ordinando e sostituendo la carta gi� giocata in precedenza se esiste. Se il mazzo non ha carte compatta la mano e diminuisce numeroCarte, ma non totaleCarte
        /// </summary>
        /// <param name="m">mazzo da cui prendere la carta</param>
        /// <exception cref="ArgumentException">Se si supera il massimo delle carte contenibili</exception>
        public void AddCarta(Mazzo m)
		{
			UInt16 i = 0;
			Carta temp;
			if (iCarta == NumeroCarte && ICartaGiocata == (UInt16)CARTA_GIOCATA.NESSUNA_CARTA_GIOCATA)
				throw new ArgumentException($"Chiamato Giocatore::setCarta con mano.size()==numeroCarte=={NumeroCarte}");
			if (ICartaGiocata != (UInt16)CARTA_GIOCATA.NESSUNA_CARTA_GIOCATA)
			{
				for (i = ICartaGiocata; i < NumeroCarte - 1; i++)
					mano[i] = mano[i + 1];
				mano[i] = null;
				ICartaGiocata = (UInt16)CARTA_GIOCATA.NESSUNA_CARTA_GIOCATA;
				mano[iCarta - 1] = SostituisciCartaGiocata(m);
				for (i = (UInt16)(iCarta - 2); i < UInt16.MaxValue && iCarta > 1 && mano[i].CompareTo(mano[i + 1]) < 0; i--)
				{
					temp = mano[i];
					mano[i] = mano[i+1];
					mano[i+1] = temp;
				}
				return;
			}
			Ordina(m);


		}
        /// <summary>
        /// Ordina la mano con l'insertion sort
        /// </summary>
        /// <param name="m">mazzo da cui prendere la carta</param>
        private void Ordina(Mazzo m)
		{
			UInt16 i = 0;
			UInt16 j = 0;
			Carta c = SostituisciCartaGiocata(m);
			for (i = 0; i < iCarta && mano[i] != null && c.CompareTo(mano[i]) < 0; i++) ;
			for (j = (UInt16)(NumeroCarte - 1); j > i; j--)
				mano[j] = mano[j - 1];
			mano[i] = c;
			iCarta++;
		}
        /// <summary>
        /// Sostituisce la carta giocata con una fresca presa dal mazzo, se non ci sono pi� carrte nel mazzo diminuisce numeroCarte ed iCarta
        /// </summary>
        /// <param name="m">mazzo da cui prendere la carta giocata</param>
        /// <returns>la carta da aggiungere</returns>
        private Carta SostituisciCartaGiocata(Mazzo m)
		{
			Carta c;
			try
			{
				c = Carta.GetCarta(m.GetCarta());
			}
			catch (IndexOutOfRangeException e)
			{
				NumeroCarte--;
				iCarta--;
				if (NumeroCarte == 0)
					throw e;
				return mano[NumeroCarte];
			}
			return c;
		}
        /// <summary>
        /// Se � l'utente imposta la carta giocata come i, se � il computer elabora una carta da giocare essendo il primo di mano
        /// </summary>
        /// <param name="i">vale solo come utente, indice della carta da giocare</param>
        public void Gioca(UInt16 i, bool stessoSeme=false)
		{
			ICartaGiocata = helper.Gioca(i, mano, NumeroCarte, stessoSeme);
		}
        /// <summary>
        /// Se � l'utente imposta la carta giocata come i, se � il computer elabora una carta da giocare essendo il secondo di mano
        /// </summary>
        /// <param name="i">vale solo come utente, indice della carta da giocae</param>
        /// <param name="g1">primo di mano</param>
        /// <param name="stessoSeme">stabilisce se buosgna rispondere al seme</param>
        public void Gioca(UInt16 i, Giocatore g1, bool stessoSeme=false)
		{
			ICartaGiocata = helper.Gioca(i, mano, NumeroCarte, g1.CartaGiocata, stessoSeme);
		}
        /// <summary>
        /// Aggiorna il puntegio sulla base delle carte giocate
        /// </summary>
        /// <param name="g">l'altro giocatore</param>
        /// <returns>il punteggio attuale</returns>
        public UInt16 AggiornaPunteggio(Giocatore g)
		{
			Punteggio=helper.AggiornaPunteggio(Punteggio, CartaGiocata, g.CartaGiocata);
            return Punteggio;
		}
        /// <summary>
        /// Se � l'utente deve impostare iCartaGiocata ad i, se � il computer deve costruirsi i grafi di presa sulla base delle carte in mano e del piatto per stabilirsi cosa giocare
        /// </summary>
        /// <param name="i">vale solo se � l'utente, numero della carta da giocare</param>
        /// <param name="piatto">il piatto</param>
        public void Gioca(UInt16 i, List<Carta> piatto)
        {
            ICartaGiocata = helper.Gioca(i, mano, NumeroCarte, piatto);
        }
        /// <summary>
        /// Restituisce l'id in xaml della carta presa in esame per associarla all'immagine
        /// </summary>
        /// <param name="quale">indice della carta</param>
        /// <returns>una stringa indicante la carta che sia esistente in xaml</returns>
        public String GetID(UInt16 quale)
		{
			String s = mano[quale].Id;
			return s;
		}
	}

}
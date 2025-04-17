
/*
 *  This code is distribuited under GPL 3.0 or, at your opinion, any later version
 *
 *  Created by Giulio Sorrentino (numerone, Giuliano Spaghetti).
 *  Copyright 2023-2025 Some rights reserved.
 *
 */

namespace org.altervista.numerone.framework
{    /// <summary>
     /// Indica la struttura carta che identifica una carta del mazzo
     /// </summary>
    public class Carta
    {
        /// <summary>
        /// Prefisso delle immagini delle carte in xaml
        /// </summary>
        private const char prefisso= 'n';
        /// <summary>
        /// Seme della carta
        /// </summary>
        public UInt16 Seme { get; init; }
        /// <summary>
        /// Valore, non punteggio, della carta
        /// </summary>
        public UInt16 Valore { get; init; }
        /// <summary>
        /// Puntegguio della carta
        /// </summary>
        public UInt16 Punteggio { get; init; }
        /// <summary>
        /// Seme della carta in formato stringa
        /// </summary>
        public string SemeStr { get; private set; }
        /// <summary>
        /// Id che identifica la figura della carta: deve esistere nell'xaml
        /// </summary>
        public string Id { get => $"{prefisso}{Helper.GetNumero(Seme, Valore)}";}
        /// <summary>
        /// Struttura della carta che designa il seme di briscola
        /// </summary>
        public static Carta Briscola { get => (Helper as org.altervista.numerone.framework.briscola.CartaHelper).GetCartaBriscola(); }
        /// <summary>
        /// Helper per personalizzare il comportamento della classe carta
        /// </summary>
        /// <summary>
        /// Helper per personalizzare il comportamento della classe carta
        /// </summary>
        public static CartaHelper Helper { get; private set; }
        /// <summary>
        /// Vettore dlle carte
        /// </summary>
        private static Carta[] carte;
        /// <summary>
        /// Costruttore privato perché le carte devono essere immutabili.
        /// </summary>
        /// <param name="n">numero intero indicante il numero intero della carta</param>
        /// <param name="s0">prima delle 4 stringhe indicante il seme italiano</param>
        /// <param name="s1">seconda delle 4 stringhe indicante il seme italiano</param>
        /// <param name="s2">terza delle 4 stringhe indicante il seme italiano</param>
        /// <param name="s3">quarta delle 4 stringhe indicante il seme italiano</param>
        private Carta(UInt16 n, string s0, string s1, string s2, string s3)
        {
            Seme = Helper.GetSeme(n);
            Valore = Helper.GetValore(n);
            Punteggio = Helper.GetPunteggio(n);
            SemeStr = Helper.GetSemeStr(n, s0, s1, s2, s3);
        }
        /// <summary>
        /// Vero costruttore, identifica un numero di carte pari ad n ed inizializza il vettore delle carte
        /// </summary>
        /// <param name="n">numero delle carte</param>
        /// <param name="h">per evitare una ereditarietà selvaggia, si è scelto di usare una classe a parte per il comportamento specifico della classe</param>
        /// <param name="s0">prima delle 4 stringhe indicante il seme italiano</param>
        /// <param name="s1">seconda delle 4 stringhe indicante il seme italiano</param>
        /// <param name="s2">terza delle 4 stringhe indicante il seme italiano</param>
        /// <param name="s3">quarta delle 4 stringhe indicante il seme italiano</param>
        public static void Inizializza(UInt16 n, CartaHelper h, string s0, string s1, string s2, string s3)
        {
            Helper = h;
            carte = new Carta[n];
            for (UInt16 i = 0; i < n; i++)
            {
                carte[i] = new Carta(i, s0, s1, s2, s3);

            }
        }
        /// <summary>
        /// Restituisce la struttura indicante la carta numero quale
        /// </summary>
        /// <param name="quale">numero della carta da prendere</param>
        /// <returns>la struttura indicante la carta presa</returns>
        public static Carta GetCarta(UInt16 quale) { return carte[quale]; }
        /// <summary>
        /// Dice se due carte hanno lo stesso seme
        /// </summary>
        /// <param name="c1">carta con cui confrontare il seme, può essere null</param>
        /// <returns>true se la carta chiamante ha lo stesso seme di c1</returns>
        public bool StessoSeme(Carta c1) { if (c1 == null) return false; else return Seme == c1.Seme; }
        /// <summary>
        /// Compara due carte
        /// </summary>
        /// <param name="c1">carta con cui confrontare il seme, può essere null</param>
		/// <returns>-1 se maggiore la prima, zero se uguale, 1 se maggiore la seconda</returns>
        public int CompareTo(Carta c1)
        {
            if (c1 == null)
                return 1;
            else
                return Helper.CompareTo(Helper.GetNumero(Seme, Valore), Helper.GetNumero(c1.Seme, c1.Valore));
        }
        public override string ToString()
        {
            string s = $"{Valore + 1} di {SemeStr}";
            if (Helper is org.altervista.numerone.framework.briscola.CartaHelper)
                s += StessoSeme((Helper as org.altervista.numerone.framework.briscola.CartaHelper).GetCartaBriscola()) ? "*" : " ";
            else
                s += " ";
            return s;
        }
        /// <summary>
        /// Imposta l'helper della classe carta, se ad inizio partita
        /// </summary>
        /// <param name="h">L'helper da importare</param>
        /// <param name="m">Mazzo per certificare che si è all'inizio della partita</param>
        public static void SetHelper(CartaHelper h, Mazzo m)
        {
            if (m.GetNumeroCarte() == carte.Length)
            {
                Helper = h;
            }
        }
    }
}

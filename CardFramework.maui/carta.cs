
/*
  *  This code is distribuited under GPL 3.0 or, at your opinion, any later version
 *  CBriscola 1.1.3
 *
 *  Created by Giulio Sorrentino (numerone) on 29/01/23.
 *  Copyright 2023 Some rights reserved.
 *
 */

namespace org.altervista.numerone.framework
{    /// <summary>
     /// Indica la struttura carta che identifica una carta del mazzo
     /// </summary>
    public class Carta
    {
        private readonly UInt16 seme,
                   valore,
                   punteggio;
        private readonly string semeStr;
        public UInt16 Seme { get => seme; }
        public UInt16 Valore { get => valore; }
        public UInt16 Punteggio { get => punteggio; }
        public string SemeStr { get => semeStr; }
        public string Id { get => $"n{Helper.GetNumero(seme, valore)}";}
        public static Carta Briscola { get => (Helper as org.altervista.numerone.framework.briscola.CartaHelper).GetCartaBriscola(); }
        private static CartaHelper helper;
        public static CartaHelper Helper { get => helper; }
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
            seme = Helper.GetSeme(n);
            valore = Helper.GetValore(n);
            punteggio = Helper.GetPunteggio(n);
            semeStr = Helper.GetSemeStr(n, s0, s1, s2, s3);
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
            helper = h;
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
        public bool StessoSeme(Carta c1) { if (c1 == null) return false; else return seme == c1.Seme; }
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
            string s = $"{valore + 1} di {semeStr}";
            if (Helper is org.altervista.numerone.framework.briscola.CartaHelper)
                s += StessoSeme((Helper as org.altervista.numerone.framework.briscola.CartaHelper).GetCartaBriscola()) ? "*" : " ";
            else
                s += " ";
            return s;
        }
        public static void SetHelper(CartaHelper h, Mazzo m)
        {
            if (m.GetNumeroCarte() == carte.Length)
            {
                helper = h;
            }
        }
    }
}

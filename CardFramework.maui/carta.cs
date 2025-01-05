
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
        private static CartaHelper helper;
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
            seme = helper.GetSeme(n);
            valore = helper.GetValore(n);
            punteggio = helper.GetPunteggio(n);
            semeStr = helper.GetSemeStr(n, s0, s1, s2, s3);
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
        /// Getter che ritorna il seme della carta
        /// </summary>
        /// <returns>seme della carta</returns>
        public UInt16 GetSeme() { return seme; }
        /// <summary>
        /// Getter che ritorna il valore della carta
        /// </summary>
        /// <returns>valore della carta</returns>
        public UInt16 GetValore() { return valore; }
        /// <summary>
        /// Getter che restuistuisce il punteggio della carta
        /// </summary>
        /// <returns>punteggio della carta</returns>
        public UInt16 GetPunteggio() { return punteggio; }
        /// <summary>
        /// Getter che restituisce il seme in valore stringa, uno degli 8 passati ad inizializza
        /// </summary>
        /// <returns>il seme in formato stringa della carta</returns>
        public string GetSemeStr() { return semeStr; }
        /// <summary>
        /// Dice se due carte hanno lo stesso seme
        /// </summary>
        /// <param name="c1">carta con cui confrontare il seme, può essere null</param>
        /// <returns>true se la carta chiamante ha lo stesso seme di c1</returns>
        public bool StessoSeme(Carta c1) { if (c1 == null) return false; else return seme == c1.GetSeme(); }
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
                return helper.CompareTo(helper.GetNumero(GetSeme(), GetValore()), helper.GetNumero(c1.GetSeme(), c1.GetValore()));
        }
        /// <summary>
        /// Restituiasce l'id carta che deve corrisondere a quella in xaml.
        /// </summary>
        /// <returns>il valore mumerico della carta</returns>
        public String GetID()
        {
            return $"n{helper.GetNumero(seme, valore)}";
        }
        /// <summary>
        /// restituisce la struttura che identifica il valore di briscola
        /// </summary>
        /// <returns></returns>
        public static Carta GetCartaBriscola() { return (helper as org.altervista.numerone.framework.briscola.CartaHelper).GetCartaBriscola(); }
        /// <summary>
        /// setter dela classe che identifica il comportamento che le carte devono avere
        /// </summary>
        /// <param name="h">classe che incapsula il comportamti dekke caere</param>
        public static void SetHelper(CartaHelper h) { helper = h; }

        public override string ToString()
        {
            string s = $"{valore + 1} di {semeStr}";
            if (helper is org.altervista.numerone.framework.briscola.CartaHelper)
                s += StessoSeme((helper as org.altervista.numerone.framework.briscola.CartaHelper).GetCartaBriscola()) ? "*" : " ";
            else
                s += " ";
            return s;
        }
    }
}

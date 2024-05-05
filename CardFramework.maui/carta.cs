
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
    public class Carta
    {
        private readonly UInt16 seme,
                   valore,
                   punteggio;
        private readonly string semeStr;
        private static CartaHelper helper;
        private static Carta[] carte;
        private Carta(UInt16 n, string s0, string s1, string s2, string s3)
        {
            seme = helper.GetSeme(n);
            valore = helper.GetValore(n);
            punteggio = helper.GetPunteggio(n);
            semeStr = helper.GetSemeStr(n, s0, s1, s2, s3);
        }
        public static void Inizializza(UInt16 n, CartaHelper h, string s0, string s1, string s2, string s3)
        {
            helper = h;
            carte = new Carta[n];
            for (UInt16 i = 0; i < n; i++)
            {
                carte[i] = new Carta(i, s0, s1, s2, s3);

            }
        }
        public static Carta GetCarta(UInt16 quale) { return carte[quale]; }
        public UInt16 GetSeme() { return seme; }
        public UInt16 GetValore() { return valore; }
        public UInt16 GetPunteggio() { return punteggio; }
        public string GetSemeStr() { return semeStr; }
        public bool StessoSeme(Carta c1) { if (c1 == null) return false; else return seme == c1.GetSeme(); }
        public int CompareTo(Carta c1)
        {
            if (c1 == null)
                return 1;
            else
                return helper.CompareTo(helper.GetNumero(GetSeme(), GetValore()), helper.GetNumero(c1.GetSeme(), c1.GetValore()));
        }

        public String GetID()
        {
            return $"n{seme * 10 + valore}";
        }

        public static Carta GetCartaBriscola() { return (helper as org.altervista.numerone.framework.briscola.CartaHelper).GetCartaBriscola(); }

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

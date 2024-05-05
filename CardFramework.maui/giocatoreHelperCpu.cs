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
    public abstract class GiocatoreHelperCpu : GiocatoreHelper
    {
        protected readonly Carta briscola;
        protected UInt16 GetBriscola(Carta[] mano, UInt16 numeroCarte)
        {
            UInt16 i;
            for (i = 0; i < numeroCarte; i++)
                if (briscola.StessoSeme(mano[i]))
                    break;
            return i;
        }
        public GiocatoreHelperCpu(UInt16 b)
        {
            briscola = Carta.GetCarta(b);
        }
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
        protected UInt16 GetPrimaCartaConSeme(Carta[] mano, UInt16 numeroCarte, Carta c)
        {
            UInt16 ca = numeroCarte;
            for (UInt16 i = 0; i < numeroCarte && ca == numeroCarte; i++)
                if (c.StessoSeme(mano[i]))
                    ca = i;
            return ca;

        }
        public UInt16 Gioca(UInt16 x, Carta[] mano, UInt16 numeroCarte)
        {
            UInt16 i;
            for (i = (UInt16)(numeroCarte - 1); i > 0; i--) ;
            if ((mano[i].GetPunteggio() > 4 || briscola.StessoSeme(mano[i])))
                i = 0;
            return i;

        }
        public abstract UInt16 Gioca(UInt16 x, Carta[] mano, UInt16 numeroCarte, Carta c, bool stessoSemr);
        public void AggiornaPunteggio(ref UInt16 punteggioAttuale, Carta c, Carta c1) { punteggioAttuale = (UInt16)(punteggioAttuale + c.GetPunteggio() + c1.GetPunteggio()); }

        public abstract UInt16 GetLivello();

        public ushort Gioca(ushort i, Carta[] v, ushort numeroCarte, List<Carta> piatto)
        {
            throw new NotImplementedException();
        }
    }
}
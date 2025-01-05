namespace org.altervista.numerone.framework
{
    /// <summary>
    /// Classe che implementa il comportamento di un giocatore CPU di livello 1
    /// </summary>
    public class GiocatoreHelperCpu0 : GiocatoreHelperCpu
    {
        /// <summary>
        /// Inizializza la struttura portandosi il seme di briscola
        /// </summary>
        /// <param name="b">intero indicante la carta di briscola</param>
        public GiocatoreHelperCpu0(ushort b) : base(b)
        {
        }
        /// <summary>
        /// retituisce il livello di gioco
        /// </summary>
        /// <returns>il livello di gioco</returns>
        public override ushort GetLivello()
        {
            return 1;
        }
        /// <summary>
        /// Se è il secondo di mano e ha briscola gioca quella, altrimenti se deve rispondere al seme cerca lka carta più piccola dello stesso seme, se no gioca la carta più piccola in assoluto
        /// </summary>
        /// <param name="x">indice della carta da giocare, qui non considerato</param>
        /// <param name="mano">vettore delle carte da giocare</param>
        /// <param name="numeroCarte">dimensione del vettore dellke carte</param>
        /// <param name="c">carta giocata dall'altro giocatore</param>
        /// <param name="stessoSeme">indica se bisogna rispondere al seme</param>
        /// <returns>indice di mano indicante la carta da giocare</returns>
        public override UInt16 Gioca(UInt16 x, Carta[] mano, UInt16 numeroCarte, Carta c, bool stessoSeme)
        {
            UInt16 carta = numeroCarte;
            for (UInt16 i = 0; i < numeroCarte - 1 && carta == numeroCarte; i++)
                if (briscola.StessoSeme(mano[i]))
                    carta = i;

            if (carta == numeroCarte)
                if (stessoSeme)
                    carta = GetPrimaCartaConSeme(mano, numeroCarte, c);
                else
                    carta = 0;
            return carta;
        }
    }

}

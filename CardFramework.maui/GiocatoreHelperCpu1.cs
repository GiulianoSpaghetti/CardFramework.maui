namespace org.altervista.numerone.framework
{
    /// <summary>
    /// Classe che implementa il comportamento della cpu di livello 2
    /// </summary>
    public class GiocatoreHelperCpu1 : GiocatoreHelperCpu
    {
        /// <summary>
        /// Inizializza la struttura portandosi il seme di briscola
        /// </summary>
        /// <param name="b">intero indicante la carta di briscola</param>
        public GiocatoreHelperCpu1(ushort b) : base(b)
        {
        }
        /// <summary>
        /// retituisce il livello di gioco
        /// </summary>
        /// <returns>il livello di gioco</returns>
        public override ushort GetLivello()
        {
            return 2;
        }
        /// <summary>
        /// Se è il secondo di mano si cerca la carta di maggior valore con lo stesso seme, se non la si trova e si ha briscola e la carta ha peso si gioca briscola, se non può prendere se deve rispondere al seme cerca la carta più piccola dello stesso seme, se no gioca la carta più piccola in assoluto
        /// </summary>
        /// <param name="x">indice della carta da giocare, qui non considerato</param>
        /// <param name="mano">vettore delle carte da giocare</param>
        /// <param name="numeroCarte">dimensione del vettore dellke carte</param>
        /// <param name="c">carta giocata dall'altro giocatore</param>
        /// <param name="stessoSeme">indica se bisogna rispondere al seme</param>
        /// <returns>indice di mano indicante la carta da giocare</returns>
        public override ushort Gioca(ushort x, Carta[] mano, ushort numeroCarte, Carta c, bool stessoSeme)
        {
            UInt16 carta = numeroCarte;
            UInt16 i = (UInt16)ElaboratoreCarteBriscola.r.Next(0, UInt16.MaxValue);
            if (!briscola.StessoSeme(c))
            {
                if ((i = getSoprataglio(mano, numeroCarte, c, true)) < numeroCarte)
                    carta = i;
                else
                    for (i = 0; i < (numeroCarte - 1) && carta == numeroCarte; i++)
                        if (briscola.StessoSeme(mano[i]))
                            carta = i;
            }
            if (carta == numeroCarte)
                if (stessoSeme)
                    carta = GetPrimaCartaConSeme(mano, numeroCarte, c);
                else
                    carta = 0;
            return carta;
        }
    }
}

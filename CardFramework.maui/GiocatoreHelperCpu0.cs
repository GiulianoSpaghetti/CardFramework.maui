namespace org.altervista.numerone.framework
{
    public class GiocatoreHelperCpu0 : GiocatoreHelperCpu
    {
        public GiocatoreHelperCpu0(ushort b) : base(b)
        {
        }

        public override ushort GetLivello()
        {
            return 1;
        }

        public override UInt16 Gioca(UInt16 x, Carta[] mano, UInt16 numeroCarte, Carta c)
        {
            for (UInt16 i = 0; i < mano.Length - 1; i++)
                if (briscola.StessoSeme(mano[i]))
                    return i;
            return 0;
        }
    }

}

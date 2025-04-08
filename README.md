:it: Made in Italy. Il secondo framework italiano ad avere la documentazione inline.

![Napoli-Logo](https://github.com/user-attachments/assets/fc1773c1-6823-429d-8760-e7d7e79f7d8f)
![made in parco grifeo](https://github.com/user-attachments/assets/c1d40b56-101a-462f-9970-006c81937300)

Framework di numerone in maui per la realizzazione di giochi di carte equi.
La codebase è in .net, con l'aggiunta di un resourcedictionary da passare.
Il resource dictionary deve includere 4 campi: bastoni, coppe, spade e denari da tradurre dall'italiano nella lingua desiderata, i 4 semi dei mazzi di carte italiane, o eventualmente francesi.
Il codice di apertura deve essere:

	e = new ElaboratoreCarteBriscola(briscolaDaPunti, 0, 39, 40);
	m = new Mazzo(e);
	Carta.Inizializza(numerocarte,new CartaHelperBriscola(e.GetCartaBriscola()), "bastoni", "coppe", "denari", "spade");
	g = new Giocatore(new GiocatoreHelperUtente(), nomegiocatore, dimensionemano);
	switch (indicatore di livello)
	{
    		case 1: helper = new GiocatoreHelperCpu0(e.CartaBriscola); break;
    		xase 2: helper = new GiocatoreHelperCpu1(e.CartaBriscola); break;
    		default: helper = new GiocatoreHelperCpu2(e.CartaBriscola); break;
	}
	cpu = new Giocatore(helper, nomegiocatore, dimensionemano);
	briscola = Carta.GetCarta(ElaboratoreCarteBriscola.GetCartaBriscola());
	for (UInt16 i = 0; i < dimensionemano; i++)
	{
    		g.AddCarta(m);
    		cpu.AddCarta(m);
	}
 
una volta fatto questo, in carta si avrà un vettore di numerocarte elementi, in g e cpu si avrà un vettore di tre dimensionemanoelementi corrispondenti alle prime 2*dimensionemano carte del mazzo, 
che saranno riempite con addcarta.
Quando addcarta restituisce un IndexOutOfRangeException exception si avrà la fine del mazzo.

Se avete un gioco di carte sul piatto, il modo di agire dei giocatori professionisti è quello di crearsi mentalmente il grafo di presa. Mi spiego: per la scopa in mano ho un 8, è prendibile tramite 7+1 e 6+2, entrambi vanno bene. Vanno messi in un grafo e va così scomposto l'8, per poi prendere sulla base del piatto quello che consuma il maggior numero di carte del piatto stesso. Quindi se ho 8 e 10 e posso prendere più carte che con l'8 invece che col 10, va giocato l'8.

Al momento copre la briscola ed il poker, col poker che si fissa sul seme "primo di mano".

Siete liberi di espanderlo con i grafi di presa.

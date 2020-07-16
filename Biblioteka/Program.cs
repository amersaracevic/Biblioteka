using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Biblioteka
{
	class Program
	{
		//TODO izmena (takodje uz izmenu, placanje clanarine) i uklanjanje korisnika za domaci
		//isto za knjige
		//Dodati datuma na racune u POSu 
		//Kod ispisa, treba napraviti da kada se prikazuju knjige prikazu kod kojih su sve clanova
		//kopije. Kod clanova, treba da ispisemo knjige koje su kod njih
		//Treba implemetirati zapisivanje u fajlove. Posto korisnici imaju list knjiga, kao racun u pos-u
		//otrpilike, treba da zapisemo sifru svake knjige koja je kod korisnika

		static List<Clan> Clanovi = new List<Clan>();
		static List<Knjiga> Knjige = new List<Knjiga>();

		static void Main(string[] args)
		{
			string unos;
			do
			{
				Meni();
				unos = Console.ReadKey().KeyChar.ToString();
				Console.WriteLine();
				switch (unos)
				{
					case "0":
						Izdaj();
						break;
					case "1":
						DodajKnjigu();
						break;
					case "2":
						IzmeniKnjigu();
						break;
					case "3":
						UkloniKnjigu();
						break;
					case "4":
						IspisKnjiga();
						break;
					case "5":
						UnosClana();
						break;
					case "6":
						IzmenaClana();
						break;
					case "7":
						BrisanjeClana();
						break;
					case "8":
						IspisClanova();
						break;
					case "9":
						Console.WriteLine("Bye :)");
						break;
					default:
						Console.WriteLine("Unos nije razumljiv :(");
						break;
				}
			} while (unos != "9");
			Console.ReadKey();
		}

		static void BrisanjeClana()
		{
			Console.Write("Unesite clanski broj clana kojeg zelite da uklonite: ");
			string sifra = Console.ReadLine();
			Clan obrisi = null;
			foreach (Clan c in Clanovi)
			{
				if (c.ClanskiBroj == sifra)
				{
					obrisi = c;
					break;
				}
			}
			if (Clanovi.Remove(obrisi))
			{
				Console.WriteLine("Clan je uspesno uklonjen");
			}
			else
			{
				Console.WriteLine("Clan sa unetim clanskim brojem nije pronadjen");
			}

		}

		static void IzmenaClana()
		{
			Console.Write("Unesite sifru clana: ");
			string sifra = Console.ReadLine();

			foreach (Clan c in Clanovi)
			{
				if (c.ClanskiBroj == sifra)
				{
					Console.WriteLine("Unesite novi clanski broj ili niista ako ne zelite da menjate clanski broj");
					sifra = Console.ReadLine();
					if (sifra != "")
					{
						c.ClanskiBroj = sifra;
					}

					Console.WriteLine("Unesite novo ime ili niista ako ne zelite da menjate ime");
					sifra = Console.ReadLine();
					if (sifra != "")
					{
						c.Ime = sifra;
					}

					Console.WriteLine("Unesite novo prezime ili niista ako ne zelite da menjate prezime");
					sifra = Console.ReadLine();
					if (sifra != "")
					{
						c.Prezime = sifra;
					}

					Console.WriteLine("Uspesno ste dodali izmene");
					return;
				}

			}
			Console.WriteLine("Ne postoji clan sa unetom sifrom");

		}
		static void UkloniKnjigu()
		{
			Console.Write("Unesite sifru knjige koju zelite da uklonite: ");
			string sifra = Console.ReadLine();
			Knjiga b = null;
			foreach (Knjiga k in Knjige)
			{
				if (k.Sifra == sifra)
				{
					b = k;
					break;
				}
			}
			if (Knjige.Remove(b))
			{
				Console.WriteLine("Uspesno uklonjena knjiga");
			}
			else
			{
				Console.WriteLine("Knjiga sa unetom sifrom nije pronadjena");
			}

		}
		static void IzmeniKnjigu()
		{
			Console.Write("Unesite sifru knjige: ");
			string sifra = Console.ReadLine();

			foreach (Knjiga k in Knjige)
			{
				if (k.Sifra == sifra)
				{
					Console.WriteLine("Unesite novu sifru za knjigu ili niista ako ne zelite da menjate sifru");
					sifra = Console.ReadLine();
					if (sifra != "")
					{
						k.Sifra = sifra;
					}

					Console.WriteLine("Unesite novi naziv za knjigu ili niista ako ne zelite da menjate naziv");
					sifra = Console.ReadLine();
					if (sifra != "")
					{
						k.Naziv = sifra;
					}

					Console.WriteLine("Unesite novi znar za knjigu ili niista ako ne zelite da menjate zanr");
					sifra = Console.ReadLine();
					if (sifra != "")
					{
						k.Zanr = sifra;
					}

					Console.WriteLine("Unesite novu koliciinu za knjigu ili niista ako ne zelite da menjate kolicinu");
					sifra = Console.ReadLine();
					if (sifra != "")
					{
						k.BrojPrimeraka = int.Parse(sifra);
					}
					Console.WriteLine("Uspesno ste dodali izmene");
					return;
				}

			}
			Console.WriteLine("Ne postoji knjiga sa unetom sifrom");
		}

		static void Izdaj()
		{
			Console.Write("Unesite sifru clana: ");
			string sifra = Console.ReadLine();

			Clan c = null;
			foreach (Clan cl in Clanovi)
			{
				if (cl.ClanskiBroj == sifra)
				{
					c = cl;
					break;
				}
			}

			if (c == null)
			{
				Console.WriteLine("Pogresan broj");
				return;
			}

			TimeSpan kas = c.ProveriClanarinu();
			if (kas.Days != 0)
			{
				Console.WriteLine($"Clanarina kasni {kas.Days}");
				return;
			}

			Console.Write("Unesite sifru knjige: ");
			sifra = Console.ReadLine();

			Knjiga k = null;
			foreach (Knjiga kg in Knjige)
			{
				if (kg.Sifra == sifra)
				{
					k = kg;
					break;
				}
			}

			if (k == null)
			{
				Console.WriteLine("Pogresan broj knjige");
				return;
			}

			if (k.BrojPrimeraka == 0)
			{
				Console.WriteLine("Nema slobodan primerak :(");
			}

			c.Knjige.Add(k);
			k.BrojPrimeraka--;

		}

		static void IspisKnjiga()
		{
			foreach (Knjiga k in Knjige)
			{
				Console.WriteLine($"{k.Sifra} - {k.Naziv} {k.Zanr} {k.BrojPrimeraka}");
			}
		}

		static void DodajKnjigu()
		{
			Knjiga k = new Knjiga();
			string unos;
			do
			{
				Console.WriteLine("Unesite sifru: ");
				unos = Console.ReadLine();
				if (unos != "")
				{
					k.Sifra = unos;
					break;
				}
				Console.WriteLine("sifra mora sadrzati neki karakter");
			} while (true);
			do
			{
				Console.WriteLine("Unesite naziv: ");
				unos = Console.ReadLine();
				if (unos != "")
				{
					k.Naziv = unos;
					break;
				}
				Console.WriteLine("naziv mora sadrzati neki karakter");

			} while (true);
			do
			{
				Console.WriteLine("Unesite zanr: ");
				unos = Console.ReadLine();
				if (unos != "")
				{
					k.Zanr = unos;
					break;
				}
				Console.WriteLine("zanr mora sadrzati neki karakter");
			} while (true);
			do
			{
				Console.WriteLine("Unesite broj primeraka: ");
				int kol = int.Parse(Console.ReadLine());
				if (kol < 1)
				{
					k.BrojPrimeraka = kol;
					break;
				}
				Console.WriteLine("Broj primeraka biti veci od 0");

			} while (true);
			Knjige.Add(k);
		}

		static void IspisClanova()
		{
			foreach (Clan c in Clanovi)
			{
				Console.Write($"{c.ClanskiBroj} -- {c.Ime} {c.Prezime} ");
				DateTime sada = DateTime.Now;
				TimeSpan kasnjenje = c.ProveriClanarinu();
				if (kasnjenje.Days == 0)
				{
					Console.WriteLine("-- Clanarina OK");
				}
				else
				{
					Console.WriteLine($"Kasni {kasnjenje.Days} dana");
				}
			}
		}

		static void UnosClana()
		{
			string ImeIPrezime;
			while (true)
			{
				Console.Write("Unesite ime i prezime: ");
				ImeIPrezime = Console.ReadLine();
				if (ImeIPrezime.Split(' ').Length == 2)
				{
					break;
				}
				Console.WriteLine("Los unos :(");
			}

			string clanska;
			while (true)
			{
				Console.Write("Unesite broj clanske: ");
				clanska = Console.ReadLine();
				bool Problem = false;

				if (string.IsNullOrEmpty(clanska))
				{
					Problem = true;
					Console.WriteLine("Los unos :(");
				}

				foreach (Clan c in Clanovi)
				{
					if (c.ClanskiBroj == clanska)
					{
						Problem = true;
						Console.WriteLine("Broj je duplikat!");
					}
				}
				if (!Problem)
				{
					break;
				}
			}

			string[] iIp = ImeIPrezime.Split(' ');

			Clanovi.Add(new Clan(iIp[0], iIp[1], clanska));
		}

		static void Meni()
		{
			Console.WriteLine("0 - Izdaj knjigu");
			Console.WriteLine("1 - Dodaj knjigu");
			Console.WriteLine("2 - Izmeni knjigu");
			Console.WriteLine("3 - Ukloni knjigu");
			Console.WriteLine("4 - Ispis knjiga");
			Console.WriteLine("5 - Unos clana");
			Console.WriteLine("6 - Izmena clana");
			Console.WriteLine("7 - Uklanjanje clana");
			Console.WriteLine("8 - Ispis clanova");
			Console.WriteLine("9 - Izlaz");
			Console.WriteLine("---------------------");
			Console.Write("Unesite izbor: ");
		}
	}
}

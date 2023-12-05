using System;
using AsciiArt;

namespace jeu_du_pendu
{
    class Program
    {
        // permet d'afficher le nombre de mot d'elephant
        static void AfficherMot(string mot, List<char> lettres)
        {
            for (int i = 0;i < mot.Length; i++)
            {
                char lettre = mot[i];
                if (lettres.Contains(lettre))
                {
                    Console.Write(lettre+" ");
                }
                else
                {
                    Console.Write("_ ");
                }
                
            }
            Console.WriteLine();
        }
        
        static char DemanderUneLettre(string message = " Entrer une lettre : ")
        {
            while (true) {
                Console.Write(message);
                string lettre = Console.ReadLine();

            if(lettre.Length == 1)
            {
                lettre = lettre.ToUpper();
                return lettre[0];
            }
            Console.WriteLine("Erreur: Vous devez rentrer une lettre");

            }
        }
        static void DevinerMot ( string mot)
        {
            var lettreDeviner = new List<char>();
            var lettreExclues = new List<char>();
            const int NB_VIES = 6;
            int viesRestantes = NB_VIES;

            while (viesRestantes > 0)
            {
                //por afficher le dessin
                Console.WriteLine(Ascii.PENDU[NB_VIES - viesRestantes]);
                Console.WriteLine();

                AfficherMot(mot, lettreDeviner);
                Console.WriteLine();
                var lettre = DemanderUneLettre();
                Console.Clear();

                if(mot.Contains(lettre))
                {
                    Console.WriteLine("Cette lettre est dans le mot");
                    lettreDeviner.Add(lettre);
                    //gagner
                    if(ToutesLettresDevinees(mot,lettreDeviner))
                    {
                        
                        break;
                    }
                }
                else
                {
                    //pour eviter les doublons de mots
                    if (!lettreExclues.Contains(lettre))
                    {
                        viesRestantes--;
                        lettreExclues.Add(lettre);
                    }
                    
                    Console.WriteLine("Vies restantes : " +viesRestantes);
                }

                if(lettreExclues.Count > 0) 
                {

                    Console.WriteLine("Le mot ne contient les lettres : " + string.Join(", ", lettreExclues));
                }

                Console.WriteLine();
            }

            Console.WriteLine(Ascii.PENDU[NB_VIES - viesRestantes]);

            if (viesRestantes == 0)
            {
                Console.WriteLine(" PERDU ! le mot etait : " +mot);
            }
            else
            {
                AfficherMot(mot, lettreDeviner);
                Console.WriteLine();

                Console.WriteLine("GAGNE");
            }
        }
        
        static bool ToutesLettresDevinees( string mot, List<char> lettres)
        {
            foreach ( var lettre in lettres)
            {
                mot = mot.Replace(lettre.ToString(), "");
            }

            if(mot.Length == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        static string[] ChargerLeMot( string monFichier)
        {
            //gestion des errers de lecture du fichier
            try
            {
                return File.ReadAllLines(monFichier);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Erreur de lecture du fichier : " + monFichier + " ( " +ex.Message + ")" );
            }
            return null;
           
        }
        
        static bool DemanderDeRejouer()
        {
            
            char rep = DemanderUneLettre(" Voulez-vous rejouez ? ");
            if((rep == 'o') || (rep == 'O'))
            {
                return true;
            }
            else if ((rep == 'n') || (rep == 'N'))
            {
                return false;
            }
            else
            {
                Console.WriteLine("Erreur : Vous devez repondre avce o ou n");
                return DemanderDeRejouer();
            }
        }
        
        static void Main(string[] args) 
        {
            while(true) {
                Random rand = new Random();
                var mots = ChargerLeMot("mots.txt");

                if((mots == null ) || (mots.Length == 0))
                {
                    Console.WriteLine("la liste des mots est vide");
                }
                else
                {
                   int i = rand.Next(mots.Length);
                    string mot =  mots[0].Trim().ToUpper();

                    DevinerMot(mot);
                    if(!DemanderDeRejouer())
                    {
                        break;
                    }
                    Console.Clear();
                }

                Console.WriteLine("Merci et à bientot");

            }


            /*char lettre = DemanderUneLettre();
            AfficherMot(mot, new List<char> { lettre});*/


        }

    }
}
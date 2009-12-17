using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace VideoPlayer
{
    class Traitement
    {

        private static Traitement _instance = null;
        public static Traitement Instance 
        { 
            get
            {
                if(_instance==null)
                    _instance = new Traitement();
                return _instance;
            }
        }
        
        private Traitement()
        {
            Brillance = 0;
            Contraste = 1;
            Convolution = new [] {0,0,0,0,1,0,0,0,0};
        }

        public int Brillance { get; set; }
        public double Contraste { get; set; }
        public int[] Convolution { get; set; }

        public void Traiter(Bitmap bitmap)
        {
            Matrice m;
            m.tableau = Convolution;
            m.facteur = 1;
            m.offset = 127;

            CalculMatrice(bitmap,m);
        }
        private static void CalculMatrice(Bitmap bitmap, Matrice m)
        {

            if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
                throw new Exception("le format de l'image n'est pas en 32bppArgb !");

            //calcul du bord qu'il faut rajouter à l'image pour appliquer la matrice
            int n = (int)((Math.Sqrt(m.tableau.Length) - 1) / 2);

            //on crée un bord de n pixel sur l'image avec effet mirroir
            Bitmap imageMirror = CreateBitmapWithMirrorBorder(bitmap, n);

            //Grâce aux variables locales, on évite des instructions IL supplémentaires
            //par exemple la mise de l'adresse du champ dans la pile d'évaluation de la méthode
            int facteur = m.facteur;//division
            int moffset = m.offset;//ajout
            int widthMirror = imageMirror.Width;
            int heightMirror = imageMirror.Height;
            int width = bitmap.Width;
            int height = bitmap.Height;
            int tailleMatrice = m.tableau.Length;

            unsafe
            {
                //utilisation tableau sur la pile
                int* tabParam = stackalloc int[tailleMatrice];
                //initialisation du tableau de paramètres
                for (int i = 0; i < tailleMatrice; i++)
                    tabParam[i] = m.tableau[i] * 256 / facteur;

                BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                BitmapData bmpDataMirror = imageMirror.LockBits(new Rectangle(0, 0, widthMirror, heightMirror), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

                //pointeur sur l'image fournie (déplacement par 4 octets)
                int* newPixel = (int*)(void*)bmpData.Scan0;
                //pointeur sur l'image avec bords en miroir (déplacement par octets)
                byte* mPixel = (byte*)(void*)bmpDataMirror.Scan0;


                int r, g, b;//composantes rouge, verte et bleue
                int loc;//localisation du pointeur
                int cst2 = 2 * n + 1;//longueur d'une ligne

                int indice;//indice dans le tableau
                int tab;//stockage intermédiaire du tableau
                int indicemax;// indice maximum dans le tableau avant le passage à la ligne dans la matrice

                //Zone initiale par rapport à la position du pointeur sur l'image avec miroir
                int locIni = (1 - widthMirror) * (4 * n);
                //Zone maximum par rapport à la position du pointeur sur l'image avec miroir
                int locMax = (widthMirror + 1) * (4 * n);
                //incrémentation de la localisation par rapport à la position du pointeur
                int locIncrement = (widthMirror - (cst2 + 1)) * 4;
                //incrément pour le déplacement du pointeur sur l'image avec miroir
                int mPixelIncrement = (2 * n * 4);

                //on positionne le pointeur sur l'image avec bords en miroir
                mPixel += (widthMirror + 1) * (n * 4);

                //calcul de l'adresse maximum possible du pointeur sur l'image fournie
                //L'adresse ne change pas car les données de l'image ne sont pas en zone gérée 
                //(donc non affecté par le garbage collector)
                int AdressMax = (int)newPixel + (height * width * 4);

                //On économise l'incrémentation d'une variable locale simplement en comparant l'adressage.
                while ((int)newPixel < AdressMax)
                {
                    //parcours de la hauteur de l'imae
                    for (int x = 0; x < width; x++)
                    {
                        //parcours de la largeur de l'image

                        //initialisations à chaque pixel
                        r = 0;
                        g = 0;
                        b = 0;
                        indice = 0;
                        loc = locIni;
                        do
                        {
                            //calcul de l'indice maximum de la matrice avant de franchir une nouvelle ligne
                            indicemax = indice + cst2;
                            do
                            {
                                //parcours d'une ligne de matrice
                                tab = tabParam[indice];
                                b += mPixel[loc] * tab; //composante bleue
                                g += mPixel[loc + 1] * tab; //composante verte
                                r += mPixel[loc + 2] * tab; //composante rouge
                                loc += 4;
                            }
                            while (indice++ < indicemax);
                            //on passe à la ligne suivante de la matrice
                            loc += locIncrement;
                        } while (loc < locMax);

                        //calcul des composantes
                        r = (r >> 8) + moffset;
                        b = (b >> 8) + moffset;
                        g = (g >> 8) + moffset;

                        //bornage des composantes
                        r = (r < 0) ? 0 : (r > 255) ? 255 : r;
                        b = (b < 0) ? 0 : (b > 255) ? 255 : b;
                        g = (g < 0) ? 0 : (g > 255) ? 255 : g;

                        //on garde la transparence et on évite de placer 255 sur la pile
                        newPixel[0] = mPixel[3] << 24 | r << 16 | g << 8 | b;

                        //deplacement des pointeurs vers le prochain pixel
                        newPixel++;
                        mPixel += 4;
                    }
                    mPixel += mPixelIncrement;//Saut du pointeur de l'image aux bords en miroir
                }
                bitmap.UnlockBits(bmpData);
                imageMirror.UnlockBits(bmpDataMirror);
            }
            imageMirror.Dispose();
        }

        private static Bitmap CreateBitmapWithMirrorBorder(Bitmap bitmap, int n)
        {
            //Mise en variable locale de différentes valeurs afin de limiter les instructions IL

            //largeur, hauteur de l'image à traiter
            int width = bitmap.Width;
            int height = bitmap.Height;

            //largeur, hauteur de l'image avec les bords en miroir
            int widthMirror = width + (n * 2);
            int heightMirror = height + (n * 2);

            //Création de l'image mirroir, elle est au format 32Arbg
            Bitmap bitmapWithMirror = new Bitmap(widthMirror, heightMirror);

            //copie de l'image au centre de la nouvelle
            Graphics g = Graphics.FromImage(bitmapWithMirror);
            g.DrawImage(bitmap, n, n, width, height);
            g.Dispose();

            //on va copier les lignes manquantes (recopie des lignes Nord, Sud, Ouest, Est de l'image)
            unsafe
            {
                BitmapData bmpDataMirror = bitmapWithMirror.LockBits(new Rectangle(0, 0, widthMirror, heightMirror), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                //récupération du pointeur sur les données de l'image.
                int* newPixel = (int*)(void*)bmpDataMirror.Scan0;

                //définition d'une table de pré-calcul 
                //servant à la localisation du pointeur 
                //sur les régions Ouest et Est de l'image.
                int* tabOuestEst = stackalloc int[heightMirror];
                int oe = 0;
                int cpt = 0;
                do
                {
                    tabOuestEst[cpt] = oe;
                    oe += widthMirror;
                    cpt++;
                }
                while (cpt < heightMirror);

                //Définition de deux tables de pré-calcul
                //servant à la localisation du pointeur
                //sur les régions Nord et Sud de l'image
                int* tabNordSud1 = stackalloc int[n + 1];
                int* tabNordSud2 = stackalloc int[n + 1];
                int ns1 = n * widthMirror;
                int ns2 = ns1 - widthMirror;
                cpt = 1;
                do
                {
                    tabNordSud1[cpt] = ns1 + n;
                    tabNordSud2[cpt] = ns2 + n;
                    ns1 += widthMirror;
                    ns2 -= widthMirror;
                    cpt++;
                }
                while (cpt <= n);

                int newloc;//localisation du pointeur pour l'écriture
                int loc;//localisation du pointeur pour la lecture

                int max;//localisation maximum dans les boucles

                //Recopie de la zone Nord en Miroir
                for (int i = 1; i <= n; ++i)
                {
                    loc = tabNordSud1[i];
                    newloc = tabNordSud2[i];
                    max = loc + width;
                    do
                    {
                        *(newPixel + newloc) = *(newPixel + loc);
                        loc++;
                        newloc++;
                    }
                    while (loc < max);
                }

                //recopie de la zone Sud en miroir
                int taille = widthMirror * height;
                for (int i = 1; i <= n; ++i)
                {
                    loc = tabNordSud2[i] + taille;
                    newloc = tabNordSud1[i] + taille;
                    max = loc + width;
                    do
                    {
                        *(newPixel + newloc) = *(newPixel + loc);
                        newloc++;
                        loc++;
                    }
                    while (loc < max);
                }

                //recopie des zones Ouest et Est en miroir
                for (int y = 0; y < heightMirror; y++)
                {
                    //zone Ouest
                    newloc = n + tabOuestEst[y];
                    loc = newloc - 1;
                    max = loc + n;
                    do
                    {
                        newloc--;
                        loc++;
                        *(newPixel + newloc) = *(newPixel + loc);
                    }
                    while (loc < max);

                    //Zone Est
                    loc = widthMirror - n + tabOuestEst[y];
                    newloc = loc - 1;
                    max = newloc + n;
                    do
                    {
                        newloc++;
                        loc--;
                        newPixel[newloc] = newPixel[loc];
                    }
                    while (newloc < max);
                }
                bitmapWithMirror.UnlockBits(bmpDataMirror);
            }
            //l'image renvoyee peut être exploitee via des matrices (2*n)+1 X (2*n)+1
            //pas de réduction donc de la taille initiale pour l'image traitée.
            //Une copie en miroir évite les effets de bords
            return bitmapWithMirror;
        }

        [StructLayout(LayoutKind.Auto)]
        private struct Matrice
        {
            internal int[] tableau;
            internal int facteur;
            internal int offset;
        }
    }
}

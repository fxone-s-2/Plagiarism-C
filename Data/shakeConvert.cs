
using System.Threading.Tasks;
using System.Linq;
using System;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Collections;
using System.Collections.Generic;




    public class shakeConvert
    {
        private string file;
        
        public shakeConvert(string inputFile)
        {
            file = inputFile;

        }


        public static ArrayList getArray()
        {
            
            StreamReader file = new StreamReader("Data/data.txt");


            ArrayList text = new ArrayList();

            string line;
            while ((line = file.ReadLine()) != null)
            {
                text.Add(line);
            }




            return text;
        }

        
    }



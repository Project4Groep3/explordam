﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using prjct4app.WebServiceDetails;


namespace prjct4app
{
    class RefineResults
    {
        Random random = new Random();
        int aankomsttijd { get; set; }
        int vertrektijd { get; set; }
        int day { get; set; }
        int totaletijd { get; set; }
        int huidigetijd { get; set; }
        RootObject rootobject { get; set; }
        bool museum = true;
        bool park = true;
        bool shopping = true;
        bool restaurant = true;
        bool nightclub = true;
        int limit = 10;
        
        PlaceDetails placedetails = new PlaceDetails();

        public RefineResults(int aankomsttijd, int vertrektijd, int day)
        {
            this.day = day;
            this.aankomsttijd = aankomsttijd;
            this.vertrektijd = vertrektijd;
            this.totaletijd = vertrektijd - aankomsttijd;
            this.huidigetijd = aankomsttijd;
        }

        public async Task FilterAsync(List<Resultaat> resultaatlijst, List<string> placeids)
        {
            int tries = 0;
            while (resultaatlijst.Count < 5 && tries < limit )
            {
                Debug.WriteLine(placeids[random.Next(0, placeids.Count - 1)]);

                rootobject = await placedetails.PlaceDetailsWebRequest(placeids[random.Next(0, placeids.Count-1)]);


                try
                {
                    Debug.WriteLine(Convert.ToInt32(rootobject.result.opening_hours.periods[0].open.time).ToString() + " " + aankomsttijd.ToString());
                    Debug.WriteLine(Convert.ToInt32(rootobject.result.opening_hours.periods[0].close.time).ToString() + " " + vertrektijd.ToString());
                    if (aankomsttijd <= Convert.ToInt32(rootobject.result.opening_hours.periods[day].open.time))
                    {
                        resultaatlijst.Add(new Resultaat(rootobject));
                    }
                }

                catch { }
                tries++;
            }
            
        }
    }
}